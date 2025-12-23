namespace SevenStrikeModules.XTween
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Events;

    /*  XTween_Previewer 生命周期流程图

                   [InitializeOnLoadMethod]
                                     ▼
     ┌─────────────────────────────┐
                    静态初始化 Initialize()                                
     
             • 注册EditorApplication.update事件               
             • 重置播放状态 Played = false                         
             • 重置最大时长 MaxTotalDuration = 0             
     └──────────── ▼ ───────────────┘
                                     
     ┌─────────────────────────────┐
                       编辑器更新循环 Update                 
     └─────────────────────────────┘
                                      │ 
                                      ├───────────────────┬──────────────────────┐
                                       |                                               |                                                      |
                                      ▼                                            ▼                                                    ▼
     ┌───────────────────┐      ┌───────────────────┐      ┌───────────────────┐
               添加动画 Append()                         批量添加 Prepare()                                 播放 Play()               
     
                  • 添加单个动画                 |              • 添加多个动画                  |              • 开始播放         
                  • 计算最大时长                 |              • 计算最大时长                  |              • 设置Played=true  
                  • 检测无限循环                 |              • 检测无限循环                  |              • 记录开始时间       
     
     └──────────────── ▼ ────────────────────────────────────────────────┘
                                                                                 
     ┌─────────────────────────────────┐
                                 Update() 核心逻辑                  
     
                       • 自动停止检测（时长/无限循环）                       
                       • 动画状态更新                                                   
                       • 场景视图重绘
         
     └──────────────── ▼ ────────────────┘
                                               │ 
                                               ├────────────────────────┬──────────────────────┐
                                               │                                                          │                                                     |
                                               ▼                                                        ▼                                                    ▼
                           ┌───────────────────┐      ┌───────────────────┐      ┌───────────────────┐
                                        倒带 Rewind()                                        终止 Kill()                                       清空 Clear()  
                           
                                     • 重置所有动画                                       • 可选倒带                                    • 可选终止动画
                                     • 设置Played=false                                • 终止所有动画                             • 清空列表
                                     • 重置最大时长                                       • 可选清空列表                             • 重置最大时长
                                     • 重置状态                              └───────────────────┘       └───────────────────┘
                           └───────────────────┘            
     
     关键状态：
     • Played：播放时为true，停止/倒带时为false
     • MaxTotalDuration：添加/播放时计算，清空时重置
     • AutoKillWithDuration：控制自动终止行为
     • PreviewTweens：被管理的动画列表
     */

    /// <summary>
    /// XTween编辑器动画预览系统（非运行时）
    /// 
    /// - 核心功能 -
    /// 1. 在编辑器模式下预览 XTween_Interface 动画
    /// 2. 提供播放/暂停/倒带等控制功能
    /// 3. 自动管理动画生命周期
    /// 
    /// - 典型使用场景 -
    /// - 在Inspector面板调试动画曲线
    /// - 验证复杂动画序列的时序
    /// - 快速迭代UI动效设计
    /// 
    /// - 注意事项 -
    /// 1. 所有功能仅在编辑器模式下生效
    /// 2. 无限循环动画会阻止自动停止
    /// 3. 修改Time.timeScale不影响预览
    /// </summary>
    public static class XTween_Previewer
    {
        #region 成员变量
        /// <summary>
        /// 存储当前所有正在预览的动画实例的列表
        /// - 通过 Append()/Prepare() 方法添加动画
        /// - 通过 Clear()/Kill() 方法移除动画
        /// - 列表中可能包含不同状态的动画（播放中/暂停/已完成）
        /// </summary>
        [SerializeField] private static readonly List<XTween_Interface> PreviewTweens = new List<XTween_Interface>();
        /// <summary>
        /// 标记预览器当前是否处于播放状态
        /// 状态变化：
        /// - Play() 设置为 true
        /// - Rewind()/Kill() 设置为 false
        /// 作用：
        /// 1. 控制 Update() 中的超时检测逻辑
        /// 2. 外部可通过该状态判断预览器是否在运行
        /// </summary>
        [SerializeField] internal static bool Played;
        /// <summary>
        /// 存储所有动画中最长的总时长（Duration + Delay）
        /// </summary>
        [SerializeField] private static float MaxTotalDuration;
        /// <summary>
        /// 记录动画预览开始的绝对时间戳（单位：秒，基于EditorApplication.timeSinceStartup）
        /// 
        /// [核心作用]
        /// 1. 作为自动停止功能的计时基准点
        /// 2. 精确计算动画已运行时长：currentTime - PlayStartTime
        /// 3. 避免累计误差（相比使用deltaTime累加更精确）
        /// 
        /// [生命周期]
        /// - 设置时机：每次调用Play()方法时更新
        /// - 重置时机：调用Rewind()/Kill()/Clear()时不清除（保持最后一次播放时间）
        /// 
        /// [技术特性]
        /// - 使用double类型保证长时间运行的精度
        /// - 基于编辑器运行时间（不受Time.timeScale影响）
        /// - 仅在编辑器模式下有效
        /// 
        /// [典型使用场景]
        /// 1. 自动停止检测：if (currentTime - PlayStartTime >= MaxTotalDuration)
        /// 2. 调试日志输出运行时长
        /// 3. 计算动画进度百分比
        /// 
        /// [注意事项]
        /// - 与Played状态同步更新（但生命周期不同）
        /// - 和MaxTotalDuration配合实现超时停止
        /// - 不受动画暂停/继续影响（始终记录初始播放时间）
        /// </summary>
        [SerializeField] private static double PlayStartTime;
        /// <summary>
        /// 当前预览中的动画数量（只读）
        /// 
        /// 示例：
        /// if (PreviewCount > 10) sp_DebugMode.Log("警告：同时预览的动画过多");
        /// </summary>
        [SerializeField] public static int PreviewCount => PreviewTweens.Count;

        /// <summary>
        /// 是否启用根据最大耗时自动终止预览
        /// </summary>
        [SerializeField] private static bool autoKillWithDuration = true;
        /// <summary>
        /// 获取或设置是否启用【根据动画时长自动终止预览】功能
        /// 
        /// 功能说明：
        /// - 当设置为 true 时：
        ///   1. 动画播放完成后会自动调用 Kill(true)
        ///   2. 根据所有动画的【Duration + Delay + 循环时长】计算最大持续时间
        ///   3. 超过最大持续时间时强制终止（含10%容差）
        /// - 当设置为 false 时：
        ///   完全禁用自动终止功能，需手动调用 Kill/Rewind
        /// 
        /// 默认值：
        /// - 初始值为 true（默认启用）
        /// 
        /// 特殊处理：
        /// - 当存在无限循环动画（LoopCount=-1）时：
        ///   自动忽略超时检测（即使启用也无效）
        /// 
        /// 使用示例：
        /// // 禁用自动停止
        /// XTween_Previewer.AutoKillWithDuration = false;
        /// 
        /// // 启用自动停止（默认）
        /// XTween_Previewer.AutoKillWithDuration = true;
        /// 
        /// 注意事项：
        /// 1. 该设置仅影响预览模式，不影响运行时行为
        /// 2. 状态变更会立即生效（无需重新Play）
        /// 3. 与手动Kill/Rewind调用无冲突
        /// </summary>
        public static bool AutoKillWithDuration
        {
            get => autoKillWithDuration;
            set => autoKillWithDuration = value;
        }

        /// <summary>
        /// Kill预览动画的时候是否先Rewind？
        /// </summary>
        [SerializeField] private static bool beforeKillRewind = true;
        /// <summary>
        /// Kill预览动画的时候是否先Rewind？
        /// </summary>
        public static bool BeforeKillRewind
        {
            get => beforeKillRewind;
            set => beforeKillRewind = value;
        }

        /// <summary>
        /// Kill预览动画之后是否Clear预览动画列表？
        /// </summary>
        [SerializeField] private static bool afterKillClear = true;
        /// <summary>
        /// Kill预览动画之后是否Clear预览动画列表？
        /// </summary>
        public static bool AfterKillClear
        {
            get => afterKillClear;
            set => afterKillClear = value;
        }

        public static UnityAction act_on_editor_play;
        public static UnityAction act_on_editor_autokill;
        public static UnityAction act_on_editor_kill;
        public static UnityAction act_on_editor_rewind;
        public static UnityAction act_on_editor_clear;
        #endregion

        /// <summary>
        /// 静态初始化方法（通过[InitializeOnLoadMethod]特性自动调用）
        /// 
        /// 核心功能：
        /// 1. 注册编辑器更新事件（EditorApplication.update）
        /// 2. 初始化预览器默认状态：
        ///    - 重置播放状态标志（Played = false）
        ///    - 清空最大时长记录（MaxTotalDuration = 0）
        /// 
        /// 执行时机：
        /// - Unity编辑器启动时
        /// - 脚本重新编译后
        /// - 进入编辑器模式时
        /// 
        /// 注意事项：
        /// 1. 仅在编辑器环境下生效（运行时通过Application.isPlaying过滤）
        /// 2. 与播放模式完全隔离
        /// 3. 通过静态构造函数模式保证只初始化一次
        /// 
        /// 关联系统：
        /// - 与EditorApplication.update形成持久化更新循环
        /// - 依赖Unity的脚本编译回调系统
        /// </summary>
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.update += Update;
            Played = false;
            MaxTotalDuration = 0f;

            autoKillWithDuration = XTween_Utilitys.PlayerPrefs_ReadValue_Bool_ForEditor("PreviewTween_AutoKillWithDuration");

        }
        /// <summary>
        /// 每帧更新方法（由EditorApplication.update驱动）
        /// 功能说明：
        /// 1. 执行动画状态更新和进度计算
        /// 2. 处理自动停止检测逻辑
        /// 3. 管理场景视图的重绘
        /// 
        /// 执行流程：
        /// 1. 基础状态检查（运行模式/活跃状态）
        /// 2. 自动停止条件检测（包含循环次数验证）
        /// 3. 逐个更新动画状态
        /// 4. 触发必要的界面刷新
        /// 
        /// 注意事项：
        /// - 仅在编辑器模式下生效（通过Application.isPlaying判断）
        /// - 对时间缩放（timeScale）免疫
        /// - 使用EditorApplication.timeSinceStartup保证时间精度
        /// 
        /// 核心逻辑：
        /// - 对于单次播放（LoopCount=0）：检查IsCompleted状态
        /// - 对于有限循环（LoopCount>0）：验证CurrentLoop <= LoopCount
        /// - 对于无限循环（LoopCount=-1）：跳过自动停止检测
        /// - 超时保护：超过理论时长110%时强制终止
        /// </summary>
        private static void Update()
        {
            // 保持原有所有基础检查
            if (Application.isPlaying || !TweenIsPreviewing()) return;

            double currentTime = EditorApplication.timeSinceStartup;

            // 自动停止逻辑（仅修改循环次数判断部分）
            if (AutoKillWithDuration && Played && MaxTotalDuration > 0)
            {
                bool hasInfiniteLoop = PreviewTweens.Any(t => t != null && t.LoopCount < 0); // 修改点1：统一用<0判断无限循环

                if (!hasInfiniteLoop)
                {
                    // 修改点2：精确化完成条件判断
                    bool allCompleted = PreviewTweens.All(t => t == null || t.IsKilled || (t.LoopCount == 0 ? t.IsCompleted : t.CurrentLoop >= t.LoopCount && t.IsCompleted));

                    if (allCompleted || (currentTime - PlayStartTime) >= MaxTotalDuration * 0.999f)
                    {
                        Kill(AfterKillClear, BeforeKillRewind);
                        if (act_on_editor_autokill != null)
                            act_on_editor_autokill();
                        return;
                    }
                }
            }

            // 保持原有动画更新逻辑（仅修正循环活跃判断）
            bool hasActiveTweens = false;
            foreach (var t in PreviewTweens)
            {
                if (t == null || t.IsKilled) continue;

                bool isActive = t.IsPlaying && !t.IsPaused;
                if (isActive)
                {
                    // 修改点3：精确循环次数判断
                    if (t.LoopCount < 0) // 无限循环
                    {
                        isActive = t.Update((float)currentTime);
                    }
                    else if (t.LoopCount == 0) // 单次播放
                    {
                        isActive = !t.IsCompleted && t.Update((float)currentTime);
                    }
                    else // 有限循环
                    {
                        isActive = (t.CurrentLoop <= t.LoopCount) && t.Update((float)currentTime);
                    }
                }
                hasActiveTweens |= isActive;
            }

            if (hasActiveTweens)
            {
                EditorApplication.QueuePlayerLoopUpdate();
                SceneView.RepaintAll();
            }
        }

        #region 准备预览动画
        /// <summary>
        /// 添加单个动画到预览队列
        /// 
        /// - 典型调用流程：
        /// 1. 在OnEnable()中调用Append()
        /// 2. 在OnDisable()中调用Kill()
        /// 
        /// - 参数说明：
        /// <param name="tween">需要预览的动画实例</param>
        /// 
        /// - 异常情况：
        /// - 运行时调用会显示警告
        /// - 重复添加相同实例会被忽略
        /// </summary>
        public static void Append(XTween_Interface tween, bool debug = false)
        {
            if (Application.isPlaying)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return;
            }

            // 确保没有重复添加
            if (PreviewTweens.Exists(p => p == tween))
                return;

            PreviewTweens.Add(tween);
            // 添加动画时重新计算最大时长
            CalculateMaxTotalDuration(debug);

#if UNITY_EDITOR
            if (AutoKillWithDuration && HasInfiniteLoopTween())
            {
                //XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
            }
#endif
        }
        /// <summary>
        /// 【批量准备动画预览】
        /// 
        /// - 核心功能：
        /// 将多个动画一次性添加到预览系统，自动过滤重复实例
        /// 
        /// - 典型使用场景：
        /// 1. 复合动画序列初始化
        /// 2. 同时调试多个关联UI元素的动效
        ///   例如：
        ///   Prepare(buttonAnim, panelAnim, textAnim);
        /// 
        /// - 与Append()的区别：
        ///           │ Method  │ 参数类型  │ 重复检测 │ 性能开销 │
        ///           ├─────────┼──────────┼──────────┼─────────┤
        ///           │ Append  │ 单个实例  │   有     │   O(1)   │
        ///           │ Prepare │ 可变参数  │   有     │  O(n)    │
        /// 
        /// - 执行流程：
        ///    ┌───────────────┐    ┌───────────────┐
        ///    │ 遍历输入参数   │───▶│ 过滤已存在实例 │
        ///    └───────────────┘    └───────┬───────┘
        ///                                 ▼
        ///    ┌─────────────────────────────────────┐
        ///    │ 计算最大时长(MaxTotalDuration更新)  │
        ///    └─────────────────────────────────────┘
        /// 
        /// - 参数说明：
        /// <param name="tweens">可变参数，支持以下传参方式：
        ///   1. 直接列举：Prepare(anim1, anim2, anim3)
        ///   2. 数组传递：Prepare(animArray)
        ///   3. LINQ结果：Prepare(GetComponents<XTween_Interface>().ToArray())
        /// </param>
        /// 
        /// - 异常处理：
        /// - 运行时调用：打印警告日志
        /// - 空参数元素：自动跳过不报错
        /// 
        /// - 优化建议：
        /// 当添加超过5个动画时，建议使用Prepare而非多次调用Append
        /// </summary>
        /// <example>
        /// // 标准用法示例
        /// [InitializeOnLoadMethod]
        /// static void SetupPreview()
        /// {
        ///     var tweens = FindObjectsOfType<XTween_Interface>();
        ///     XTween_Previewer.Prepare(tweens);
        ///     EditorApplication.delayCall += () => XTween_Previewer.Play();
        /// }
        /// </example>
        public static void Prepare(params XTween_Interface[] tweens)
        {
            if (Application.isPlaying)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return;
            }

            foreach (var tween in tweens)
            {
                // 确保没有重复添加
                if (PreviewTweens.Exists(p => p == tween))
                    continue;

                PreviewTweens.Add(tween);
            }

            // 准备动画时重新计算最大时长
            CalculateMaxTotalDuration(true);

#if UNITY_EDITOR
            if (AutoKillWithDuration && HasInfiniteLoopTween())
            {
                //XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "检测到无限循环动画，自动停止功能将不会生效！", GUIMsgState.警告);
            }
#endif
        }
        #endregion

        #region 控制预览动画
        /// <summary>
        /// 播放/继续所有预览动画
        /// 
        /// - 内部逻辑：
        /// 1. 记录EditorApplication.timeSinceStartup作为基准时间
        /// 2. 激活所有非播放状态的动画
        /// 3. 标记Played = true
        /// 
        /// - 关联属性：
        /// - 受AutoKillWithDuration设置影响
        /// - 与MaxTotalDuration计算相关
        /// </summary>
        public static void Play(Action action = null, bool debug = false)
        {
            if (Application.isPlaying && debug)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return;
            }

#if UNITY_EDITOR
            if (AutoKillWithDuration && HasInfiniteLoopTween() && debug)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "检测到无限循环动画，自动停止功能将不会生效！", GUIMsgState.警告);
            }
#endif

            // 播放前重新计算最大时长
            CalculateMaxTotalDuration(debug);
            PlayStartTime = EditorApplication.timeSinceStartup; // 记录开始时间
            foreach (var tween in PreviewTweens)
            {
                if (!tween.IsPlaying)
                {
                    tween.Play();
                }
            }
            Played = true;

            if (action != null)
            {
                action();
            }
            if (act_on_editor_play != null)
                act_on_editor_play();
        }
        /// <summary>
        /// 回退所有预览动画
        /// </summary>
        public static void Rewind(Action action = null)
        {
            if (Application.isPlaying)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return;
            }

            foreach (var tween in PreviewTweens)
            {
                tween.Rewind();
            }
            Played = false;
            // 重置最大时长
            MaxTotalDuration = 0f;
            if (action != null)
            {
                action();
            }
            if (act_on_editor_rewind != null)
                act_on_editor_rewind();
        }
        /// <summary>
        /// 终止动画预览（可选清理）
        /// 
        /// - 参数组合示例：
        /// 1. Kill(true, true)  → 倒带后清理（推荐默认方式）
        /// 2. Kill(false, false) → 立即终止不清理（快速重置）
        /// 
        /// - 执行流程：
        /// 1. 可选执行Rewind()
        /// 2. 调用所有动画的Kill()
        /// 3. 可选清空PreviewTweens列表
        /// 
        /// - 设计意图：
        /// 提供灵活的终止策略，适应调试/正式工作流的不同需求
        /// </summary>
        public static void Kill(bool Clear, bool Rewind, Action action = null)
        {
            if (Application.isPlaying)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return;
            }

            foreach (var tween in PreviewTweens)
            {
                if (Rewind)
                    tween.Rewind();
                tween.Kill();
            }

            if (Clear)
            {
                PreviewTweens.Clear();
            }
            Played = false;
            // 重置最大时长
            MaxTotalDuration = 0f;
            if (action != null)
            {
                action();
            }
            if (act_on_editor_kill != null)
                act_on_editor_kill();
        }
        /// <summary>
        /// 清空预览列表并可选终止动画
        /// 
        /// 功能说明：
        /// 1. 可选终止所有动画（需配合Kill参数）
        /// 2. 强制清空预览列表
        /// 3. 重置预览器状态
        /// 
        /// 参数说明：
        /// <param name="Kill">是否终止动画：
        ///   - true: 先执行终止操作再清空列表
        ///   - false: 直接清空列表（可能导致动画泄漏）</param>
        /// <param name="Rewind">是否回退动画到初始状态：
        ///   - 仅在Kill=true时生效
        ///   - true: 终止前执行Rewind()
        ///   - false: 直接终止</param>
        /// 
        /// 与Kill()方法的区别：
        /// - 本方法强制清空列表（Clear参数固定为true）
        /// - 终止操作变为可选（通过Kill参数控制）
        /// 
        /// 危险操作：
        /// - 当Kill=false时直接清空列表，可能导致：
        ///   a) 动画实例未被正确释放
        ///   b) 资源泄漏风险
        /// 
        /// 推荐使用场景：
        /// - 完全重置预览器时使用 Kill=true, Rewind=true
        /// - 快速清空列表时使用 Kill=false（需确保外部已处理动画生命周期）
        /// </summary>
        public static void Clear(bool Kill, bool Rewind, Action action = null)
        {
            if (Application.isPlaying)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return;
            }

            if (Kill)
            {
                foreach (var tween in PreviewTweens)
                {
                    if (Rewind)
                        tween.Rewind();
                    tween.Kill();
                }
            }
            PreviewTweens.Clear();
            // 重置最大时长
            MaxTotalDuration = 0f;

            if (action != null)
            {
                action();
            }
            if (act_on_editor_clear != null)
                act_on_editor_clear();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 计算所有预览动画的最大总持续时间（包括延迟和循环）
        /// 计算规则：
        /// 1. 对于有限循环动画（LoopCount >= 0）：
        ///    - 总时长 = 初始延迟(Delay) + 动画时长(Duration) 
        ///             + 循环次数(LoopCount) × (循环延迟(LoopingDelay) + 动画时长(Duration))
        /// 2. 对于无限循环动画（LoopCount < 0）：
        ///    - 不计入最大时长计算
        /// 3. 最终取所有动画中的最大值
        ///
        /// 典型示例：
        /// - 单次播放（LoopCount=0）: Delay=1s, Duration=2s → 总时长=3s
        /// - 3次播放（LoopCount=2）: Delay=1s, Duration=2s, LoopingDelay=0.5s → 1+2+2*(0.5+2)=7s
        /// 
        /// 注意事项：
        /// - 会自动跳过无效（null）或无限循环的动画
        /// - 计算结果会存储在 MaxTotalDuration 字段中
        /// - 每次动画列表变更后需要重新调用
        /// </summary>
        private static void CalculateMaxTotalDuration(bool Debug = false)
        {
            MaxTotalDuration = 0f;
            foreach (var t in PreviewTweens)
            {
                if (t == null || t.LoopCount < 0) continue;

                float total = t.Delay + t.Duration;
                if (t.LoopCount > 0)
                {
                    total += t.LoopCount * (t.LoopingDelay + t.Duration);
                }

                MaxTotalDuration = Mathf.Max(MaxTotalDuration, total);
            }

            // 调试日志
            if (Debug)
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", $"(目前共有 {PreviewTweens.Count} 个动画，单个最大动画总时长：{MaxTotalDuration:F2}s", GUIMsgState.警告);
        }
        /// <summary>
        /// 检测当前预览列表中是否存在无限循环动画
        /// 判断标准：
        /// - 遍历所有有效动画实例（非null且未终止）
        /// - 检查 LoopCount < 0 的动画
        /// 
        /// 返回值：
        /// - true: 存在至少一个无限循环动画
        /// - false: 不存在无限循环动画
        /// 
        /// 使用场景：
        /// 1. 自动停止功能启用时跳过无限循环动画
        /// 2. 提供警告提示用户自动停止功能可能失效
        /// 
        /// 性能说明：
        /// - 使用LINQ的Any()方法实现短路评估
        /// - 对大型动画列表有较好性能
        /// </summary>
        private static bool HasInfiniteLoopTween()
        {
            return PreviewTweens.Any(t => t != null && t.LoopCount == -1);
        }
        /// <summary>
        /// 检查所有预览动画是否都已完成
        /// </summary>
        public static bool TweenIsPreviewing()
        {
            if (Application.isPlaying)
            {
                return false;
            }

            // 如果没有动画，视为未在预览
            if (PreviewTweens.Count == 0)
                return false;

            // 只要有一个动画还在播放中、等待延迟或未完成，就返回true
            return PreviewTweens.Any(tween => (tween.IsPlaying && !tween.IsCompleted) || (tween.IsWaitingLoopDelay));
        }
        /// <summary>
        /// 获取当前所有预览中的动画
        /// </summary>
        public static XTween_Interface[] CurrentPreviewTweens()
        {
            if (Application.isPlaying)
            {
                XTween_Utilitys.DebugInfo("XTweenPreview预览器消息", "动画预览器只有在非运行模式下才可使用！", GUIMsgState.警告);
                return null;
            }

            return PreviewTweens.Select(tween => tween).ToArray();
        }
        public static void ClearEditorActions()
        {
            act_on_editor_play = null;
            act_on_editor_autokill = null;
            act_on_editor_kill = null;
            act_on_editor_rewind = null;
            act_on_editor_clear = null;
        }
        public static int HasPreviewTweens()
        {
            return PreviewTweens.Count;
        }
        #endregion
    }
}