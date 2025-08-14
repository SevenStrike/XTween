namespace SevenStrikeModules.XTween
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 动画循环模式
    /// </summary>
    public enum XTween_LoopType
    {
        /// <summary>
        /// 每次循环都从头开始
        /// </summary>
        Restart = 0,
        /// <summary>
        /// 来回往复播放
        /// </summary>
        Yoyo = 1
    }

    /// <summary>
    /// 动画管理器，负责管理和更新所有活跃的动画
    /// 提供动画的注册、注销、更新和终止功能
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class XTween_Manager : MonoBehaviour
    {
        #region 单例模式
        protected XTween_Manager() { }
        private static XTween_Manager _instance;
        public static XTween_Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    XTween_Utilitys.DebugInfo("XTween Manager动画管理器消息", "XTween Manager 还没有被实例化！", GUIMsgState.确认);
                }
                return _instance;
            }
        }
        /// <summary>
        /// 检测单例状态
        /// </summary>
        private void InstanceModeCheck()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;

            // 预加载所有动画对象
            if (XTween_Pool.EnablePool)
            {
                XTween_Pool.PreloadAll();
                XTween_Utilitys.DebugInfo("XTween Pool动画池消息", "XTween Pool动画池已预加载并就绪！", GUIMsgState.确认);
            }
            //DontDestroyOnLoad(gameObject);
            XTween_Utilitys.DebugInfo("XTween Manager动画管理器消息", "XTween Manager动画管理器已就绪！", GUIMsgState.确认);
        }
        #endregion

        public Texture2D[] EasePics;

        /// <summary>
        /// 存储所有活跃动画的ShortID
        /// 用于快速查找和管理动画
        /// </summary>
        private HashSet<string> activeShortIDs = new HashSet<string>();
        /// <summary>
        /// 活跃动画链表
        /// 存储所有正在播放或等待播放的动画
        /// </summary>
        private LinkedList<XTween_Interface> _ActiveTweens = new LinkedList<XTween_Interface>();
        /// <summary>
        /// 待添加到活跃列表的动画
        /// 用于在更新过程中添加新动画，避免直接修改活跃列表导致的迭代问题
        /// </summary>
        private readonly List<XTween_Interface> _PendingAdd = new List<XTween_Interface>();
        /// <summary>
        /// 待从活跃列表中移除的动画
        /// 用于在更新过程中移除已完成或终止的动画，避免直接修改活跃列表导致的迭代问题
        /// </summary>
        private readonly List<XTween_Interface> _PendingRemove = new List<XTween_Interface>();
        /// <summary>
        /// 标记迭代器是否需要更新
        /// 当活跃动画列表发生变化时，需要重新构建迭代器
        /// </summary>
        private bool _IteratorsDirty = true;
        /// <summary>
        /// 活跃动画链表的迭代器缓存
        /// 用于优化更新逻辑，减少重复遍历
        /// </summary>
        private LinkedListNode<XTween_Interface>[] _ActiveTweens_CachedIterators;
        /// <summary>
        /// 通过ShortID查找动画的字典
        /// 提供快速查找功能
        /// </summary>
        internal readonly Dictionary<string, XTween_Interface> _ShortID_ToTween = new Dictionary<string, XTween_Interface>();
        /// <summary>
        /// 通过GuidID查找动画的字典
        /// 提供快速查找功能
        /// </summary>
        internal readonly Dictionary<Guid, XTween_Interface> _GuidID_ToTween = new Dictionary<Guid, XTween_Interface>();

        private void Awake()
        {
            InstanceModeCheck();
        }

        #region 更新逻辑
        /// <summary>
        /// 更新所有活跃动画
        /// 每帧调用，更新动画的进度并处理状态变化
        /// </summary>
        private void Update()
        {
            ProcessPendingOperations();
            UpdateActiveTweens();
        }
        /// <summary>
        /// 处理待添加和待移除的动画
        /// 将待添加的动画加入活跃列表，将待移除的动画从活跃列表中移除
        /// </summary>
        private void ProcessPendingOperations()
        {
            // 优化 1：使用 RemoveRange 替代 Clear（避免内部数组缩容）
            if (_PendingRemove.Count > 0)
            {
                for (int i = 0; i < _PendingRemove.Count; i++)
                {
                    var tween = _PendingRemove[i];
                    var node = _ActiveTweens.First;
                    while (node != null)
                    {
                        var next = node.Next;
                        if (node.Value == tween)
                        {
                            _ActiveTweens.Remove(node);
                            UnregisterTween(tween);
                            break;
                        }
                        node = next;
                    }
                }
                _PendingRemove.RemoveRange(0, _PendingRemove.Count); // 无 GC
            }

            // 优化 2：直接遍历 _PendingAdd 避免临时迭代器
            if (_PendingAdd.Count > 0)
            {
                for (int i = 0; i < _PendingAdd.Count; i++)
                {
                    _ActiveTweens.AddLast(_PendingAdd[i]);
                }
                _PendingAdd.RemoveRange(0, _PendingAdd.Count); // 无 GC
            }

            _IteratorsDirty = true;
        }
        /// <summary>
        /// 更新所有活跃动画的进度
        /// 遍历活跃动画列表，调用每个动画的Update方法
        /// </summary>
        private void UpdateActiveTweens()
        {
            // 优化 3：仅在 _ActiveTweens 变化时重建缓存
            if (_IteratorsDirty || _ActiveTweens_CachedIterators == null || _ActiveTweens_CachedIterators.Length < _ActiveTweens.Count)
            {
                RebuildIteratorCache();
            }

            // 优化 4：避免对 null 或无效节点的冗余检查
            for (int i = 0; i < _ActiveTweens_CachedIterators.Length; i++)
            {
                var node = _ActiveTweens_CachedIterators[i];
                if (node?.List == null) continue; // 合并 null 检查

                var tween = node.Value;
                if (tween == null || tween.IsKilled)
                {
                    _ActiveTweens.Remove(node);
                    UnregisterTween(tween);
                    _IteratorsDirty = true;
                    continue;
                }

                tween.Update(Time.time); // 确保 XTween_Interface.Update 内部无 GC
            }
        }
        /// <summary>
        /// 重新构建活跃动画的迭代器缓存
        /// 优化更新逻辑，减少重复遍历
        /// </summary>
        private void RebuildIteratorCache()
        {
            // 优化 5：预分配足够大的数组（避免频繁扩容）
            if (_ActiveTweens_CachedIterators == null || _ActiveTweens_CachedIterators.Length < _ActiveTweens.Count)
            {
                int newSize = Math.Max(_ActiveTweens.Count * 2, 32); // 2倍扩容，最小容量 32
                _ActiveTweens_CachedIterators = new LinkedListNode<XTween_Interface>[newSize];
            }

            // 填充数据
            var node = _ActiveTweens.First;
            int index = 0;
            while (node != null)
            {
                _ActiveTweens_CachedIterators[index++] = node;
                node = node.Next;
            }

            // 清空多余位置（避免旧数据残留）
            for (int i = index; i < _ActiveTweens_CachedIterators.Length; i++)
            {
                _ActiveTweens_CachedIterators[i] = null;
            }

            _IteratorsDirty = false;
        }
        #endregion

        #region 动画注册与注销
        /// <summary>
        /// 注册一个动画到管理器
        /// 将动画加入活跃列表，并更新相关索引
        /// </summary>
        /// <param name="tweener">要注册的动画</param>
        internal XTween_Interface RegisterTween(XTween_Interface tweener)
        {
            if (tweener == null) return null;

            CleanDeadReferences();

            _PendingAdd.Add(tweener);

            // 注册GuidID索引
            _GuidID_ToTween[tweener.UniqueId] = tweener;

            // 注册ShortID索引
            if (!string.IsNullOrEmpty(tweener.ShortId))
            {
                _ShortID_ToTween[tweener.ShortId] = tweener;
            }
            // 记录活跃ShortID
            if (!string.IsNullOrEmpty(tweener.ShortId))
            {
                activeShortIDs.Add(tweener.ShortId);
            }
            return tweener;
        }
        /// <summary>
        /// 从管理器中注销一个动画
        /// 将动画从活跃列表中移除，并清理相关索引
        /// </summary>
        /// <param name="tween">要注销的动画</param>
        internal void UnregisterTween(XTween_Interface tween)
        {
            if (tween == null)
                return;

            // 从 GuidID 索引中移除
            if (_GuidID_ToTween.ContainsKey(tween.UniqueId))
            {
                _GuidID_ToTween.Remove(tween.UniqueId);
            }

            // 从 ShortID 索引中移除
            if (!string.IsNullOrEmpty(tween.ShortId) && _ShortID_ToTween.ContainsKey(tween.ShortId))
            {
                _ShortID_ToTween.Remove(tween.ShortId);
            }

            // 从活跃动画链表中移除
            var node = _ActiveTweens.First;
            while (node != null)
            {
                if (node.Value == tween)
                {
                    _ActiveTweens.Remove(node);
                    break;
                }
                node = node.Next;
            }
        }
        /// <summary>
        /// 清理无效的动画引用
        /// 遍历索引并移除已销毁的动画
        /// </summary>
        private void CleanDeadReferences()
        {
            // 清理短ID字典
            var deadShortIds = _ShortID_ToTween
                .Where(kvp => kvp.Value == null || kvp.Value.IsKilled)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var id in deadShortIds)
            {
                _ShortID_ToTween.Remove(id);
            }

            // 清理GUID字典
            var deadGuids = _GuidID_ToTween
                .Where(kvp => kvp.Value == null || kvp.Value.IsKilled)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var guid in deadGuids)
            {
                _GuidID_ToTween.Remove(guid);
            }
        }
        #endregion

        #region 动画统计
        /// <summary>
        /// 获取活跃动画的数量
        /// </summary>
        /// <returns>活跃动画的数量</returns>
        public int Get_TweenCount_ActiveTween() => _ActiveTweens.Count;
        /// <summary>
        /// 获取当前正在播放中的动画数量（不包括暂停的动画）
        /// </summary>
        /// <returns>正在播放中的动画数量</returns>
        public int Get_TweenCount_Playing()
        {
            int count = 0;
            var node = _ActiveTweens.First;

            while (node != null)
            {
                var tween = node.Value;
                if (tween != null && tween.IsPlaying && !tween.IsPaused)
                {
                    count++;
                }
                node = node.Next;
            }

            return count;
        }
        /// <summary>
        /// 获取当前暂停中的动画数量
        /// </summary>
        /// <returns>暂停中的动画数量</returns>
        public int Get_TweenCount_Paused()
        {
            int count = 0;
            var node = _ActiveTweens.First;

            while (node != null)
            {
                var tween = node.Value;
                if (tween != null && tween.IsPaused)
                {
                    count++;
                }
                node = node.Next;
            }

            return count;
        }
        /// <summary>
        /// 获取已完成的动画数量
        /// 注意：此方法会遍历所有活跃动画，性能敏感场景慎用
        /// </summary>
        /// <returns>已完成的动画数量</returns>
        public int Get_TweenCount_Completed()
        {
            int count = 0;
            var node = _ActiveTweens.First;

            while (node != null)
            {
                var tween = node.Value;
                if (tween != null && tween.IsCompleted)
                {
                    count++;
                }
                node = node.Next;
            }

            return count;
        }
        /// <summary>
        /// 获取循环次数不等于0的动画数量
        /// </summary>
        /// <returns>循环次数不等于0的动画数量</returns>
        public int Get_TweenCount_HasLoop()
        {
            int count = 0;
            var node = _ActiveTweens.First;

            while (node != null)
            {
                var tween = node.Value;
                if (tween != null && tween.LoopCount != 0)  // 假设XTween_Interface有LoopCount属性
                {
                    count++;
                }
                node = node.Next;
            }

            return count;
        }
        /// <summary>
        /// 获取所有活跃动画
        /// </summary>
        /// <returns>活跃动画列表</returns>
        public List<XTween_Interface> Get_ActiveTweens()
        {
            var result = new List<XTween_Interface>(_ActiveTweens.Count);
            var node = _ActiveTweens.First;

            while (node != null)
            {
                if (node.Value != null)
                    result.Add(node.Value);
                node = node.Next;
            }

            return result;
        }
        /// <summary>
        /// 获取待添加动画的数量
        /// </summary>
        /// <returns>待添加动画的数量</returns>
        public int Get_PendingAddCount() => _PendingAdd.Count;
        /// <summary>
        /// 获取待移除动画的数量
        /// </summary>
        /// <returns>待移除动画的数量</returns>
        public int Get_PendingRemoveCount() => _PendingRemove.Count;
        /// <summary>
        /// 获取待添加动画的快照
        /// </summary>
        /// <returns>待添加动画列表</returns>
        public List<XTween_Interface> Get_PendingAddTweens() => new List<XTween_Interface>(_PendingAdd);
        /// <summary>
        /// 获取待移除动画的快照
        /// </summary>
        /// <returns>待移除动画列表</returns>
        public List<XTween_Interface> Get_PendingRemoveTweens() => new List<XTween_Interface>(_PendingRemove);
        /// <summary>
        /// 获取活跃动画的快照
        /// </summary>
        /// <returns>活跃动画列表</returns>
        public IEnumerable<XTween_Interface> Get_ActiveTweensSnapshot()
        {
            // 改用线程安全的链表遍历
            var snapshot = new List<XTween_Interface>(_ActiveTweens.Count);
            var node = _ActiveTweens.First;

            while (node != null)
            {
                if (node.Value != null)
                    snapshot.Add(node.Value);
                node = node.Next;
            }

            return snapshot;
        }
        #endregion

        #region 动画控制
        /// <summary>
        /// 杀死所有正在播放的动画
        /// 可选地触发完成回调
        /// </summary>
        /// <param name="complete">是否触发完成回调</param>
        public void Kill_WithPlaying(bool complete = false)
        {
            var node = _ActiveTweens.First;
            while (node != null)
            {
                var next = node.Next;
                var wrapper = node.Value;

                if (wrapper != null && wrapper.IsPlaying && !wrapper.IsPaused)
                {
                    wrapper.Kill(complete);
                    UnregisterTween(node.Value);
                    _ActiveTweens.Remove(node);
                    _IteratorsDirty = true;
                }

                node = next;
            }
        }
        /// <summary>
        /// 杀死所有已暂停的动画
        /// 可选地触发完成回调
        /// </summary>
        /// <param name="complete">是否触发完成回调</param>
        public void Kill_WithPaused(bool complete = false)
        {
            var node = _ActiveTweens.First;
            while (node != null)
            {
                var next = node.Next;
                var wrapper = node.Value;

                if (wrapper != null && wrapper.IsPaused)
                {
                    wrapper.Kill(complete);
                    UnregisterTween(node.Value);
                    _ActiveTweens.Remove(node);
                    _IteratorsDirty = true;
                }

                node = next;
            }
        }
        /// <summary>
        /// 杀死所有动画
        /// 可选地触发完成回调
        /// </summary>
        /// <param name="complete">是否触发完成回调</param>
        public void Kill_All(bool complete = false)
        {
            // 杀死所有动画并清空链表
            var node = _ActiveTweens.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value != null)
                {
                    node.Value.Kill(complete);
                    UnregisterTween(node.Value);
                }
                node = next;
            }

            _ShortID_ToTween.Clear();
            _GuidID_ToTween.Clear();
            _ActiveTweens.Clear();

            _IteratorsDirty = true;
        }
        /// <summary>
        /// 播放所有动画
        /// </summary>
        public void Play_All()
        {
            // 杀死所有动画并清空链表
            var node = _ActiveTweens.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value != null)
                {
                    node.Value.Play();
                }
                node = next;
            }
        }
        /// <summary>
        /// 倒退所有动画
        /// </summary>
        public void Rewind_All()
        {
            // 杀死所有动画并清空链表
            var node = _ActiveTweens.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value != null)
                {
                    node.Value.Rewind();
                }
                node = next;
            }
        }
        #endregion

        #region 动画查找
        /// <summary>
        /// 根据 GuidID 查找动画
        /// </summary>
        /// <param name="id">动画的 GuidID</param>
        /// <returns>找到的动画，如果未找到则返回 null</returns>
        public XTween_Interface FindTween_By_GuidID(Guid id)
        {
            if (id == Guid.Empty)
            {
                Debug.LogError("FindTween_By_GuidID: GUID为空。");
                return null;
            }

            try
            {
                if (_GuidID_ToTween.TryGetValue(id, out var tween))
                {
                    return tween;
                }
                else
                {
                    Debug.LogWarning($"FindTween_By_GuidID: 未找到GUID为 '{id}' 的动画。");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"FindTween_By_GuidID: 在查找GUID为 '{id}' 的动画时发生异常。异常信息: {e.Message}");
                return null;
            }
        }
        /// <summary>
        /// 根据 ShortID 查找动画
        /// </summary>
        /// <param name="id">动画的 ShortID</param>
        /// <returns>找到的动画，如果未找到则返回 null</returns>
        public XTween_Interface FindTween_By_ShortID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Debug.LogWarning("FindTween_By_ShortID: 短ID为空或无效。");
                return null;
            }

            try
            {
                if (_ShortID_ToTween.TryGetValue(id, out var tween))
                {
                    return tween;
                }
                else
                {
                    Debug.LogWarning($"FindTween_By_ShortID: 未找到短ID为 '{id}' 的动画。");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"FindTween_By_ShortID: 在查找短ID为 '{id}' 的动画时发生异常。异常信息: {e.Message}");
                return null;
            }
        }
        #endregion
    }
}