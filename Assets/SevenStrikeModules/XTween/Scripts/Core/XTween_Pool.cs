/*
 * ============================================================================
 * ⚠️ 版权声明（禁止删除、禁止修改、衍生作品必须保留此注释）⚠️
 * ============================================================================
 * 版权声明 Copyright (C) 2025-Present Nanjing SevenStrike Media Co., Ltd.
 * 中文名称：南京塞维斯传媒有限公司
 * 英文名称：SevenStrikeMedia
 * 项目作者：徐寅智
 * 项目名称：XTween - Unity 高性能动画架构插件
 * 项目启动：2025年8月
 * 官方网站：http://sevenstrike.com/
 * 授权协议：GNU Affero General Public License Version 3 (AGPL 3.0)
 * 协议说明：
 * 1. 你可以自由使用、修改、分发本插件的源代码，但必须保留此版权注释
 * 2. 基于本插件修改后的衍生作品，必须同样遵循 AGPL 3.0 授权协议
 * 3. 若将本插件用于网络服务（如云端Unity编辑器、在线动效生成工具），必须公开修改后的完整源代码
 * 4. 完整协议文本可查阅：https://www.gnu.org/licenses/agpl-3.0.html
 * ============================================================================
 * 违反本注释保留要求，将违反 AGPL 3.0 授权协议，需承担相应法律责任
 */
namespace SevenStrikeModules.XTween
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class XTween_Pool
    {
        /// <summary>
        /// 动画 浮点数_Float 预加载数量
        /// </summary>
        private static int Float_Count = 250;
        /// <summary>
        /// 动画 字符串_String 预加载数量
        /// </summary>
        private static int String_Count = 250;
        /// <summary>
        /// 动画 整数_Int 预加载数量
        /// </summary>
        private static int Int_Count = 250;
        /// <summary>
        /// 动画 二维向量_Vector2 预加载数量
        /// </summary>
        private static int Vector2_Count = 250;
        /// <summary>
        /// 动画 三维向量_Vector3 预加载数量
        /// </summary>
        private static int Vector3_Count = 250;
        /// <summary>
        /// 动画 四维向量_Vector4 预加载数量
        /// </summary>
        private static int Vector4_Count = 250;
        /// <summary>
        /// 动画 颜色_Color 预加载数量
        /// </summary>
        private static int Color_Count = 250;
        /// <summary>
        /// 动画 四元数_Quaternion 预加载数量
        /// </summary>
        private static int Quaternion_Count = 250;

        /// <summary>
        /// 在生成动画时如果预加载的动画数量不够时会自动扩容
        /// </summary>
        public static bool AutoExpandPoolVolume = true;
        /// <summary>
        /// 是否开启动画池
        /// </summary>
        public static bool EnablePool = true;

        /// <summary>
        /// 动画类型池 / 键：动画类型，值：动画队列
        /// </summary>
        private static readonly Dictionary<Type, Queue<XTween_Interface>> TweenPool = new Dictionary<Type, Queue<XTween_Interface>>();
        /// <summary>
        /// 每种动画类型在对象池中自动扩容的数量
        /// 当对象池中的动画数量低于设定的阈值时，将自动扩容此数量
        /// 例如，如果某种动画类型（如 XTween_Specialized_Float）的对象池数量低于 ExpandTweenThreshold 中对应的值，
        /// 则会自动增加 ExpandTweenCount 中对应的数量到对象池中
        /// </summary>
        private static readonly Dictionary<Type, int> ExpandTweenCount = new Dictionary<Type, int>
        {
            { typeof(XTween_Specialized_Float), 100 },
            { typeof(XTween_Specialized_Int), 100 },
            { typeof(XTween_Specialized_String), 100 },
            { typeof(XTween_Specialized_Vector2), 100 },
            { typeof(XTween_Specialized_Vector3), 100 },
            { typeof(XTween_Specialized_Vector4), 100 },
            { typeof(XTween_Specialized_Color), 100 },
            { typeof(XTween_Specialized_Quaternion), 100 }
        };
        /// <summary>
        /// 每种动画类型在对象池中的自动扩容阈值
        /// 当对象池中的动画数量低于此阈值时，将触发自动扩容
        /// 例如，如果某种动画类型（如 XTween_Specialized_Float）的对象池数量低于 ExpandTweenThreshold 中对应的值，
        /// 则会自动增加 ExpandTweenCount 中对应的数量到对象池中
        /// </summary>
        private static readonly Dictionary<Type, int> ExpandTweenThreshold = new Dictionary<Type, int>
        {
            { typeof(XTween_Specialized_Float), 20 },
            { typeof(XTween_Specialized_Int), 20 },
            { typeof(XTween_Specialized_String), 20 },
            { typeof(XTween_Specialized_Vector2), 20 },
            { typeof(XTween_Specialized_Vector3), 20 },
            { typeof(XTween_Specialized_Vector4), 20 },
            { typeof(XTween_Specialized_Color), 20 },
            { typeof(XTween_Specialized_Quaternion), 20 }
        };
        /// <summary>
        /// 用于定义每种动画类型的对象池的初始预加载数量。它的作用是配置对象池在初始化时预加载的对象数量
        /// </summary>
        private static readonly Dictionary<Type, int> PreloadConfig = new Dictionary<Type, int>
        {
             //动画类型 : 浮点数_Float，数量：Float_Count
            { typeof(XTween_Specialized_Float), Float_Count},
            //动画类型 : 整数_Int，数量：Int_Count
            { typeof(XTween_Specialized_Int), Int_Count},
            //动画类型 : 浮点数_Float，数量：String_Count
            { typeof(XTween_Specialized_String), String_Count},
            //动画类型 : 二维向量_Vector2，数量：Vector2_Count
            { typeof(XTween_Specialized_Vector2), Vector2_Count},
            //动画类型 : 三维向量_Vector3，数量：Vector3_Count
            { typeof(XTween_Specialized_Vector3), Vector3_Count},
            //动画类型 : 四维向量_Vector4，数量：Vector4_Count
            { typeof(XTween_Specialized_Vector4), Vector4_Count},
            //动画类型 : 颜色_Color，数量：Color_Count
            { typeof(XTween_Specialized_Color), Color_Count},
            //动画类型 : 四元数_Quaternion，数量：Quaternion_Count
            { typeof(XTween_Specialized_Quaternion), Quaternion_Count}
        };
        /// <summary>
        /// 每种动画类型在对象池中预加载的数量
        /// 用于记录每种动画类型在对象池初始化时预加载的对象数量
        /// </summary>
        private static readonly Dictionary<Type, int> Count_Preloaded = new Dictionary<Type, int>();
        /// <summary>
        /// 每种动画类型从对象池中取出使用的次数
        /// 用于记录每种动画类型从对象池中取出使用的次数
        /// </summary>
        private static readonly Dictionary<Type, int> Count_Created = new Dictionary<Type, int>();

        #region  使用
        /// <summary>
        /// 从对象池中创建一个新的动画实例
        /// 如果对象池中没有可用对象，将自动扩容
        /// </summary>
        /// <typeparam name="T">动画类型</typeparam>
        /// <returns>创建的动画实例</returns>
        public static T CreateTween<T>() where T : XTween_Interface, new()
        {
            Type type = typeof(T);
            TweenTypeExistInPool(type);

            var queue = TweenPool[type];

            // 检查队列中是否有可用对象
            if (queue.Count > 0)
            {
                var tween = (T)queue.Dequeue();

                // 先重置状态确保安全
                tween.ResetState();

                tween.IsPoolRecyled = false;
                Count_Created[type]++;
                XTween_Manager.Instance.RegisterTween(tween);
                return tween;
            }

            if (AutoExpandPoolVolume)
            {
                // 检查闲置动画数量，如果闲置动画数量小于限定值，尝试扩展对象池
                if (queue.Count < ExpandTweenThreshold[type])
                {
                    int expandCount = ExpandTweenCount[type];
                    for (int i = 0; i < expandCount; i++)
                    {
                        var newTween = (XTween_Interface)Activator.CreateInstance(type);
                        newTween.ResetState();
                        TweenPool[type].Enqueue(newTween);
                        Count_Preloaded[type]++;
                    }
                }

                // 再次检查队列中是否有可用对象
                if (queue.Count > 0)
                {
                    var tween = (T)queue.Dequeue();
                    Count_Created[type]++;
                    XTween_Manager.Instance.RegisterTween(tween);
                    return tween;
                }
            }

            // 如果对象池已满，返回 null 或抛出异常
            Debug.LogWarning($"对象池中的 {type.Name} 已达到最大数量限制，无法创建新对象。请等待对象池中有对象被回收。");
            return default(T);
        }
        /// <summary>
        /// 将一个动画放回对象池
        /// </summary>
        /// <param name="tween">要回收的动画</param>
        public static void RecycleTween(XTween_Interface tween)
        {
            // 如果指定的回收动画是空的则不执行
            if (tween == null)
                return;

            if (tween.IsPoolRecyled)
                return;

            tween.IsPoolRecyled = true;

            // 获取即将回收的动画类型
            Type type = tween.GetType();
            // 检查池中是否已经存在此类型的记录，如果没有就增加一条
            TweenTypeExistInPool(type);

            // 将动画进行重置操作以被后续重复使用
            tween.ResetState();

            // 确保从管理器中注销动画记录
            XTween_Manager.Instance.UnregisterTween(tween);

            // 动画入列归位
            TweenPool[type].Enqueue(tween);
            Count_Created[type]--;
        }
        /// <summary>
        /// 立即强制回收所有正在使用的动画
        /// </summary>
        /// <param name="skipActiveAnimations">是否跳过当前正在播放的动画（默认false，即强制回收所有）</param>
        /// <param name="onForceRecycle">单个动画被回收时的回调（可选）</param>
        public static void ForceRecycleAll(bool skipActiveAnimations = false, Action<XTween_Interface> onForceRecycle = null)
        {
            bool isrecycled = false;
            // 1. 通过XTween_Manager获取所有活跃动画
            var activeTweens = XTween_Manager.Instance.Get_ActiveTweens();

            foreach (var tween in activeTweens)
            {
                // 2. 检查是否需要跳过正在播放的动画
                if (skipActiveAnimations && tween.IsPlaying)
                {
                    continue;
                }
                isrecycled = true;
                // 3. 执行回收前回调
                onForceRecycle?.Invoke(tween);

                // 4. 强制停止并回收动画
                tween.Kill();
                RecycleTween(tween);
            }

            if (!isrecycled)
                XTween_Utilitys.DebugInfo("XTween Pool动画池消息", "所有动画均已回收！", GUIMsgState.确认);
        }
        #endregion

        #region  预加载
        /// <summary>
        /// 预加载指定数量的动画对象
        /// </summary>
        public static void Preload<T>(int count) where T : XTween_Interface, new()
        {
            //识别动画类型
            Type type = typeof(T);

            //确保动画类型是否存在与池中
            TweenTypeExistInPool(type);

            //如果PreloadedCounts字典中没有找到目标type的记录项则添加一个
            if (!Count_Preloaded.ContainsKey(type))
                Count_Preloaded[type] = 0;

            //循环count次穿件新的实例化tween类型并重置Tween的状态且给其一个随机ID，PreloadedCounts字典中目标type的记录项递增
            for (int i = 0; i < count; i++)
            {
                var tween = new T();

                //获取即将回收的动画类型
                Type t = tween.GetType();
                ////检查池中是否已经存在此类型的记录，如果没有就增加一条
                TweenTypeExistInPool(t);

                //将该回收的动画进行重置操作
                tween.ResetState();
                tween.ClearCallbacks();

                //动画入列归位
                TweenPool[type].Enqueue(tween);

                Count_Preloaded[type]++;
            }

            Count_Created[type] = 0;
        }
        /// <summary>
        /// 根据配置预加载所有类型的动画对象
        /// </summary>
        public static void PreloadAll()
        {
            Preload<XTween_Specialized_Int>(PreloadConfig[typeof(XTween_Specialized_Int)]);
            Preload<XTween_Specialized_Float>(PreloadConfig[typeof(XTween_Specialized_Float)]);
            Preload<XTween_Specialized_String>(PreloadConfig[typeof(XTween_Specialized_String)]);
            Preload<XTween_Specialized_Vector2>(PreloadConfig[typeof(XTween_Specialized_Vector2)]);
            Preload<XTween_Specialized_Vector3>(PreloadConfig[typeof(XTween_Specialized_Vector3)]);
            Preload<XTween_Specialized_Vector4>(PreloadConfig[typeof(XTween_Specialized_Vector4)]);
            Preload<XTween_Specialized_Quaternion>(PreloadConfig[typeof(XTween_Specialized_Quaternion)]);
            Preload<XTween_Specialized_Color>(PreloadConfig[typeof(XTween_Specialized_Color)]);
        }
        /// <summary>
        /// 确保指定的 Type 在 TweenPool 中是存在的，如果不存在（即：TweenPool中没有一个键：Type值：XTween队列），它会为该类型初始化一个队列放入 TweenPool 中
        /// </summary>
        /// <param name="type"></param>
        private static void TweenTypeExistInPool(Type type)
        {
            //如果池中不存在 type 类型的队列键值对，那么就要初始化一个
            if (!TweenPool.ContainsKey(type))
            {
                //初始化 type 类型的队列到TweenPool 池中
                TweenPool[type] = new Queue<XTween_Interface>();

                //在已预加载的数量字典中加入 type 类型的数量的记录
                Count_Preloaded[type] = 0;
                //在已重用的数量字典中加入 type 类型的数量的记录
                Count_Created[type] = 0;
            }
        }
        #endregion

        #region 修改预加载数量
        /// <summary>
        /// 修改预加载配置
        /// </summary>
        public static void SetPreloadCount(Type type, int count)
        {
            PreloadConfig[type] = count;
        }
        /// <summary>
        /// 获取预加载的数量
        /// </summary>
        public static int GetPreloadCount(Type type)
        {
            return PreloadConfig.TryGetValue(type, out var count) ? count : 0;
        }
        #endregion

        /// <summary>
        /// 打印池统计信息
        /// </summary>
        public static void LogStatistics(bool debug = false)
        {
            if (!debug)
                return;

            Debug.Log("-------------Tween Pool Statistics-------------");

            foreach (var type in TweenPool.Keys)
            {
                int poolCount = TweenPool[type].Count;
                int preloaded = Count_Preloaded.TryGetValue(type, out var p) ? p : 0;
                int reused = Count_Created.TryGetValue(type, out var r) ? r : 0;

                float reuseRate = preloaded > 0 ? reused / (float)preloaded * 100 : 0;

                Debug.Log($"{type.Name}: " +
                          $"池数量={poolCount}, " +
                          $"预加载={preloaded}, " +
                          $"已使用={reused}, " +
                          $"使用率={reuseRate:F1}%");
            }
        }

        #region 查询
        /// <summary>
        /// 获取指定类型在池中的当前数量
        /// </summary>
        /// <param name="type">动画类型</param>
        /// <returns>池中该类型的数量</returns>
        public static int Get_PoolCount(Type type)
        {
            if (TweenPool.TryGetValue(type, out var queue))
            {
                return queue.Count;
            }
            return 0;
        }
        /// <summary>
        /// 获取所有类型的池中数量统计
        /// </summary>
        /// <returns>包含类型和对应数量的字典</returns>
        public static Dictionary<Type, int> Get_AllPoolCounts()
        {
            var counts = new Dictionary<Type, int>();
            foreach (var kvp in TweenPool)
            {
                counts[kvp.Key] = kvp.Value.Count;
            }
            return counts;
        }
        /// <summary>
        /// 获取指定类型的预加载数量
        /// </summary>
        /// <param name="type">动画类型</param>
        /// <returns>该类型的预加载数量</returns>
        public static int Get_PreloadedCount(Type type)
        {
            return Count_Preloaded.TryGetValue(type, out var count) ? count : 0;
        }
        /// <summary>
        /// 获取指定类型的已使用次数
        /// </summary>
        /// <param name="type">动画类型</param>
        /// <returns>该类型的已使用次数</returns>
        public static int Get_CreatedCount(Type type)
        {
            return Count_Created.TryGetValue(type, out var count) ? count : 0;
        }
        /// <summary>
        /// 获取所有类型的预加载数量统计
        /// </summary>
        /// <returns>包含类型和对应预加载数量的字典</returns>
        public static Dictionary<Type, int> Get_AllPreloadedCounts()
        {
            return new Dictionary<Type, int>(Count_Preloaded);
        }
        /// <summary>
        /// 获取所有类型的已使用次数统计
        /// </summary>
        /// <returns>包含类型和对应已使用次数的字典</returns>
        public static Dictionary<Type, int> Get_AllCreatedCounts()
        {
            return new Dictionary<Type, int>(Count_Created);
        }
        /// <summary>
        /// 获取指定类型的对象池使用百分比
        /// </summary>
        /// <param name="type">动画类型</param>
        /// <returns>使用百分比(0-100)，如果类型不存在或预加载数量为0则返回0</returns>
        public static float Get_UsagePercentage(Type type)
        {
            if (!Count_Preloaded.TryGetValue(type, out var preloaded) || preloaded == 0)
            {
                return 0f;
            }

            if (!Count_Created.TryGetValue(type, out var created))
            {
                return 0f;
            }

            // 计算使用百分比
            return Mathf.Clamp(created * 100f / preloaded, 0f, 100f);
        }
        /// <summary>
        /// 获取指定类型的对象池使用百分比
        /// </summary>
        /// <typeparam name="T">动画类型</typeparam>
        /// <returns>使用百分比(0-100)，如果类型不存在或预加载数量为0则返回0</returns>
        public static float Get_UsagePercentage<T>() where T : XTween_Interface
        {
            return Get_UsagePercentage(typeof(T));
        }
        /// <summary>
        /// 检查对象池中是否有任意类型的动画正在被使用
        /// </summary>
        /// <returns>如果有动画被使用返回true，否则返回false</returns>
        public static bool IsAnyTweenInUse()
        {
            foreach (var type in Count_Created.Keys)
            {
                if (Count_Created[type] > 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}