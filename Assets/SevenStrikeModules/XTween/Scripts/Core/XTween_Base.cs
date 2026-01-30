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
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;

    /// <summary>
    /// 动画委托注册模式
    /// </summary>
    public enum XTweenActionOpration
    {
        /// <summary>
        /// 注册委托
        /// </summary>
        Register = 0,
        /// <summary>
        /// 注销委托
        /// </summary>
        Unregister = 1
    }

    public abstract class XTween_Base<TArg> : XTween_Interface
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        protected XTween_Base(TArg defaultValue, TArg endValue, float duration)
        {
            _DefaultValue = defaultValue;
            _EndValue = endValue;
            _Duration = duration;
            _StartValue = defaultValue;
            _CustomEaseCurve = null;
            _UseCustomEaseCurve = false;

            ResetState();
            CreateIDs();
        }
        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        protected XTween_Base()
        {
            ResetState();
            ClearCallbacks();
            CreateIDs();
        }
        /// <summary>
        /// 用于对象池预加载初始化
        /// </summary>
        public void Initialize(TArg startValue, TArg endValue, float duration)
        {
            _DefaultValue = startValue;
            _EndValue = endValue;
            _Duration = duration;
            _StartValue = startValue;
            _CustomEaseCurve = null;
            _UseCustomEaseCurve = false;
        }

        #region 私有字段
        /// <summary>
        /// 在编辑器模式下记录动画的开始时间，用于计算延迟和播放进度
        /// </summary>
        private double _editorStartTime;
        /// <summary>
        /// 在编辑器模式下记录动画的暂停时间，用于恢复播放时计算正确的进度
        /// </summary>
        private double _editorPauseTime;
        /// <summary>
        /// 标记动画是否正在等待循环延迟
        /// 当动画完成一次循环并且设置了循环延迟时，此标志将被设置为 true
        /// </summary>
        private bool _isWaitingLoopDelay;
        /// <summary>
        /// 获取动画是否正在等待循环延迟
        /// </summary>
        public bool IsWaitingLoopDelay => _isWaitingLoopDelay;
        #endregion

        #region 回调       
        public Action act_on_StartCallbacks;
        public Action act_on_StopCallbacks;
        public Action act_on_KillCallbacks;
        public Action act_on_PauseCallbacks;
        public Action act_on_ResumeCallbacks;
        public Action act_on_RewindCallbacks;
        public Action<float> act_on_CompleteCallbacks;
        public Action<float> act_on_DelayUpdateCallbacks;
        public Action<TArg, float, float> act_on_UpdateCallbacks;
        public Action<TArg, float, float> act_on_StepUpdateCallbacks;
        public Action<TArg, float> act_on_ProgressCallbacks;
        #endregion

        #region 受保护字段
        /// <summary>
        /// 动画的总持续时间，单位为秒
        /// </summary>
        internal float _Duration { get; set; }
        /// <summary>
        /// 动画的开始时间，单位为秒
        /// </summary>
        internal float _StartTime { get; set; }
        /// <summary>
        /// 标记动画是否已被终止
        /// </summary>
        internal bool _IsKilled { get; set; }
        /// <summary>
        /// 动画的循环次数，-1 表示无限循环
        /// </summary>
        internal int _LoopCount { get; set; } = 0;
        /// <summary>
        /// 当前循环的次数
        /// </summary>
        internal int _CurrentLoopCount { get; set; }
        /// <summary>
        /// 动画的延迟时间，单位为秒
        /// </summary>
        internal float _Delay { get; set; }
        /// <summary>
        /// 动画的起始值
        /// </summary>
        internal TArg _StartValue { get; set; }
        /// <summary>
        /// 动画的目标值
        /// </summary>
        internal TArg _EndValue { get; set; }
        /// <summary>
        /// 动画的缓动模式，默认为线性缓动
        /// </summary>
        internal EaseMode _easeMode { get; set; } = EaseMode.Linear;
        /// <summary>
        /// 标记动画是否已开始播放
        /// </summary>
        internal bool _hasStarted { get; set; }
        /// <summary>
        /// 动画的循环类型，默认为重新开始
        /// </summary>
        internal XTween_LoopType _loopType { get; set; } = XTween_LoopType.Restart;
        /// <summary>
        /// 标记动画是否处于反转状态
        /// </summary>
        internal bool _IsReversing { get; set; } = false;
        /// <summary>
        /// 是否在每次循环时应用延迟
        /// </summary>
        internal bool _ApplyDelayPerLoop { get; set; } = false;
        /// <summary>
        /// 每次循环之间的延迟时间，单位为秒
        /// </summary>
        internal float _LoopingDelay { get; set; } = 0f;
        /// <summary>
        /// 标记动画是否使用相对值进行动画
        /// </summary>
        internal bool _IsRelative { get; set; } = false;
        /// <summary>
        /// 动画的默认初始值
        /// </summary>
        internal TArg _DefaultValue { get; set; }
        /// <summary>
        /// 标记动画是否从初始值开始
        /// </summary>
        internal bool _IsFromMode { get; set; } = false;
        /// <summary>
        /// 自定义缓动曲线，仅当 UseCustomEaseCurve 为 true 时生效
        /// </summary>
        internal AnimationCurve _CustomEaseCurve { get; set; }
        /// <summary>
        /// 标记是否使用自定义缓动曲线
        /// </summary>
        internal bool _UseCustomEaseCurve { get; set; } = false;
        /// <summary>
        /// 标记动画是否自动销毁
        /// </summary>
        internal bool _AutoKill { get; set; }
        /// <summary>
        /// 标记动画是否已暂停
        /// </summary>
        internal bool _IsPaused { get; set; }
        /// <summary>
        /// 标记动画是否正在播放
        /// </summary>
        internal bool _IsPlaying { get; set; }
        /// <summary>
        /// 标记动画是否正在循环
        /// </summary>
        internal bool _IsLooping { get; set; }
        /// <summary>
        /// 标记动画是否已完成
        /// </summary>
        internal bool _IsCompleted { get; set; }
        /// <summary>
        /// 动画的当前值
        /// </summary>
        internal TArg _CurrentValue { get; set; }
        /// <summary>
        /// 动画的当前线性进度，范围为 [0, 1]
        /// </summary>
        internal float _CurrentLinearProgress { get; set; }
        /// <summary>
        /// 动画的当前缓动进度，范围为 [0, 1]
        /// </summary>
        internal float _CurrentEasedProgress { get; set; }
        /// <summary>
        /// 动画的已耗时，单位为秒
        /// </summary>
        internal float _ElapsedTime { get; set; }
        /// <summary>
        /// 标记动画是否正在回绕
        /// </summary>
        internal bool _IsRewinding { get; set; }
        /// <summary>
        /// 动画的暂停时间，单位为秒
        /// </summary>
        internal float _PauseTime { get; set; }

        internal StepUpdateMode _stepMode = StepUpdateMode.EveryFrame;
        internal float _stepInterval = 0f;          // 时间间隔（秒）
        internal float _stepProgressInterval = 0f;  // 进度间隔（0-1）
        internal float _lastStepTime = 0f;          // 上次时间间隔执行时间
        internal float _lastStepProgress = -1f;     // 上次进度间隔执行进度
        #endregion

        #region 接口属性实现
        /// <summary>
        /// 动画的唯一标识符（GUID）
        /// </summary>
        public Guid UniqueId { get; set; } = Guid.Empty;
        /// <summary>
        /// 动画的短标识符，用于快速查找
        /// </summary>
        public string ShortId { get; set; } = "";
        /// <summary>
        /// 动画的缓动模式
        /// </summary>
        public EaseMode EaseMode => _easeMode;
        /// <summary>
        /// 是否使用自定义缓动曲线
        /// </summary>
        public bool UseCustomEaseCurve => _UseCustomEaseCurve;
        /// <summary>
        /// 自定义缓动曲线，仅当 UseCustomEaseCurve 为 true 时生效
        /// </summary>
        public AnimationCurve CustomEaseCurve => _CustomEaseCurve;
        /// <summary>
        /// 动画的总持续时间，单位为秒
        /// </summary>
        public float Duration
        {
            get
            {
                return _Duration;
            }
            protected set
            {
                _Duration = value;
            }
        }
        /// <summary>
        /// 动画的延迟时间，单位为秒
        /// </summary>
        public float Delay => _Delay;
        /// <summary>
        /// 动画的剩余延迟时间，单位为秒
        /// </summary>
        public float RemainingDelay => Mathf.Max(0, _StartTime - Time.time);
        /// <summary>
        /// 动画的循环次数，-1 表示无限循环
        /// </summary>
        public int LoopCount => _LoopCount;
        /// <summary>
        /// 每次循环之间的延迟时间，单位为秒
        /// </summary>
        public float LoopingDelay => _LoopingDelay;
        /// <summary>
        /// 当前循环的次数
        /// </summary>
        public int CurrentLoop => _CurrentLoopCount;
        /// <summary>
        /// 动画的循环类型
        /// </summary>
        public XTween_LoopType LoopType => _loopType;
        /// <summary>
        /// 当前循环的进度，范围为 [0, 1]
        /// </summary>
        public float CurrentLoopProgress => Mathf.Clamp01(_ElapsedTime / Duration);
        /// <summary>
        /// 动画的总进度，范围为 [0, 1]
        /// </summary>
        public float Progress
        {
            get => Mathf.Clamp01(_ElapsedTime / Duration);
            set
            {
                if (_IsPlaying || _IsPaused)
                {
                    _ElapsedTime = value * Duration;
                    _CurrentValue = Lerp(_StartValue, _EndValue,
                        XTween_EaseLibrary.Evaluate(_easeMode, _ElapsedTime, Duration));
                }
            }
        }
        /// <summary>
        /// 是否自动销毁动画对象，当动画完成时生效
        /// </summary>
        public bool AutoKill => _AutoKill;
        /// <summary>
        /// 动画是否已被终止
        /// </summary>
        public bool IsKilled => _IsKilled;
        /// <summary>
        /// 动画是否正在播放
        /// </summary>
        public bool IsPlaying => _IsPlaying;
        /// <summary>
        /// 动画是否已暂停
        /// </summary>
        public bool IsPaused => _IsPaused;
        /// <summary>
        /// 动画是否从初始值开始
        /// </summary>
        public bool IsFromMode => !_IsFromMode;
        /// <summary>
        /// 是否使用相对值进行动画
        /// </summary>
        public bool IsRelative => _IsRelative;
        /// <summary>
        /// 动画是否正在循环
        /// </summary>
        public bool IsLooping => _IsLooping = _LoopCount != 0;
        /// <summary>
        /// 动画是否已完成
        /// </summary>
        public bool IsCompleted => _IsCompleted;
        /// <summary>
        /// 动画是否处于活动状态
        /// </summary>
        public bool IsActive => !_IsKilled;
        /// <summary>
        /// 动画的开始时间，单位为秒
        /// </summary>
        public float StartTime => _StartTime;
        /// <summary>
        /// 动画的结束时间，单位为秒
        /// </summary>
        public float EndTime => _StartTime + Duration;
        /// <summary>
        /// 动画的当前值
        /// </summary>
        public object CurrentValue => _CurrentValue;
        /// <summary>
        /// 动画的已耗时，单位为秒
        /// </summary>
        public float ElapsedTime => _ElapsedTime;
        /// <summary>
        /// 动画的剩余时间，单位为秒
        /// </summary>
        public float RemainingTime => _LoopCount switch
        {
            -1 => float.PositiveInfinity, // 无限循环
            0 => Mathf.Max(0f, Duration - _ElapsedTime), // 单次播放
            _ => Mathf.Max(0f, (_LoopCount - _CurrentLoopCount) * Duration - _ElapsedTime)
        };
        /// <summary>
        /// 动画的当前线性进度，范围为 [0, 1]
        /// </summary>
        public float CurrentLinearProgress => _CurrentLinearProgress;
        /// <summary>
        /// 动画的当前缓动进度，范围为 [0, 1]
        /// </summary>
        public float CurrentEasedProgress => _CurrentEasedProgress;
        /// <summary>
        /// 动画是否正在回绕
        /// </summary>
        public bool IsRewinding => _IsRewinding;
        /// <summary>
        /// 动画是否正在反转
        /// </summary>
        public bool IsReversing => _IsReversing;
        /// <summary>
        /// 动画的暂停时间，单位为秒
        /// </summary>
        public float PauseTime => _PauseTime;
        /// <summary>
        /// 动画是否已从对象池中回收
        /// </summary>
        public bool IsPoolRecyled { get; set; }
        /// <summary>
        /// 动画的起始值
        /// </summary>
        public object StartValue
        {
            get
            {
                return _StartValue;
            }
            set
            {
                _StartValue = (TArg)value;
            }
        }
        #endregion

        #region 接口方法实现 - 控制
        /// <summary>
        /// 播放动画
        /// 如果动画尚未开始，则设置初始延迟并开始播放
        /// 如果动画已暂停，则恢复播放
        /// </summary>
        /// <returns>当前动画对象</returns>
        public XTween_Interface Play()
        {
            if (_IsKilled)
            {
                Debug.LogWarning("Cannot play a killed animation. Create a new tween instead.");
                return this;
            }

            if (!_hasStarted)
            {
                // 设置初始延迟
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    _editorStartTime = EditorApplication.timeSinceStartup + _Delay;
#endif
                }
                else
                {
                    _StartTime = Time.time + _Delay;
                }
                // 首次播放时，确保 _CurrentValue 正确初始化
                _CurrentValue = _IsFromMode ? _StartValue/*显式设置的起始值*/: _DefaultValue;/*默认起始值*/
                _hasStarted = true;
            }

            if (_IsCompleted)
            {
                // 重置完成状态以便重新播放
                _IsCompleted = false;
                _ElapsedTime = 0f;
                _hasStarted = false;
            }

            if (!_IsPlaying)
            {
                _IsPlaying = true;
                _IsPaused = false;

                if (!_hasStarted)
                {
                    if (!Application.isPlaying)
                    {
#if UNITY_EDITOR
                        _editorStartTime = EditorApplication.timeSinceStartup;
#endif
                    }
                    else
                    {
                        _StartTime = Time.time;
                    }
                    _hasStarted = true;
                }
                else if (_ElapsedTime > 0)
                {
                    if (!Application.isPlaying)
                    {
#if UNITY_EDITOR
                        _editorStartTime = EditorApplication.timeSinceStartup - _ElapsedTime;
#endif
                    }
                    else
                    {
                        _StartTime = Time.time - _ElapsedTime;
                    }
                }
            }

            if (act_on_StartCallbacks != null)
                act_on_StartCallbacks();

            return this;
        }
        /// <summary>
        /// 更新动画状态
        /// 根据当前时间计算动画的进度，并更新动画值
        /// </summary>
        /// <param name="currentTime">当前时间，单位为秒</param>
        /// <returns>是否继续更新动画</returns>
        public bool Update(float currentTime)
        {
            // 基础状态检查
            if (_IsKilled || !_IsPlaying || _IsPaused)
                return false;

            // 处理初始延迟（只在第一次播放时应用）
            if (!_hasStarted && !_isWaitingLoopDelay)
            {
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    double editorCurrentTime = EditorApplication.timeSinceStartup;
                    if (editorCurrentTime < _editorStartTime)
                    {
                        float remainingDelay = (float)(_editorStartTime - editorCurrentTime);
                        float progress = 1 - Mathf.Clamp01(remainingDelay / _Delay);

                        if (act_on_DelayUpdateCallbacks != null)
                            act_on_DelayUpdateCallbacks(progress);
                        return true;
                    }
#endif
                }
                else
                {
                    if (currentTime < _StartTime)
                    {
                        float remainingDelay = _StartTime - currentTime;
                        float progress = 1 - Mathf.Clamp01(remainingDelay / _Delay);

                        if (act_on_DelayUpdateCallbacks != null)
                            act_on_DelayUpdateCallbacks(progress);
                        return true;
                    }
                }
            }

            // 处理循环延迟
            if (_isWaitingLoopDelay)
            {
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    double editorCurrentTime = EditorApplication.timeSinceStartup;
                    if (editorCurrentTime < _editorStartTime)
                    {
                        // 仍在等待循环延迟中
                        return true;
                    }
#endif
                }
                else
                {
                    if (currentTime < _StartTime)
                    {
                        // 仍在等待循环延迟中
                        return true;
                    }
                }

                // 循环延迟结束，重置动画状态
                _isWaitingLoopDelay = false;
                _ElapsedTime = 0f;
                _CurrentLinearProgress = 0f;
                _IsReversing = false;

                // 在循环重置前强制设置精确的起始值
                _CurrentValue = _IsFromMode ? _StartValue : _DefaultValue;

                // === 修复：重置步长状态 ===
                ResetStepState();

                // 触发重绕回调
                if (act_on_RewindCallbacks != null)
                    act_on_RewindCallbacks();
                // 设置新的开始时间（不使用延迟）
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    _editorStartTime = EditorApplication.timeSinceStartup;
#endif
                }
                else
                {
                    _StartTime = Time.time;
                }
            }

            // 计算经过的时间（跳过延迟处理）
            if (!Application.isPlaying)
            {
#if UNITY_EDITOR
                double editorCurrentTime = EditorApplication.timeSinceStartup;
                _ElapsedTime = (float)(editorCurrentTime - _editorStartTime);
                _CurrentLinearProgress = Duration > 0 ? Mathf.Clamp01(_ElapsedTime / Duration) : 1f;
#endif
            }
            else
            {
                _ElapsedTime = currentTime - _StartTime;
                _CurrentLinearProgress = Duration > 0 ? Mathf.Clamp01(_ElapsedTime / Duration) : 1f;
            }

            // 处理进度完成
            if (_ElapsedTime >= Duration)
            {
                _ElapsedTime = Duration; // 强制修正为精确值
                _CurrentLinearProgress = 1f;

                bool shouldContinue = false;

                if (_loopType == XTween_LoopType.Yoyo)
                {
                    shouldContinue = YoyoCompletion(currentTime);
                }
                else
                {
                    shouldContinue = RestartCompletion(currentTime);
                }

                if (!shouldContinue) return false;
            }

            // 计算当前帧的值
            _CurrentValue = CalculateCurrentValue();
            _CurrentEasedProgress = CalculateEasedProgress(_CurrentLinearProgress);

            // Yoyo 模式反转处理
            if (_loopType == XTween_LoopType.Yoyo && _IsReversing)
            {
                _CurrentEasedProgress = 1f - _CurrentEasedProgress;
            }

            // 总是调用 OnUpdate（保持每帧执行）
            if (act_on_UpdateCallbacks != null)
                act_on_UpdateCallbacks(_CurrentValue, _CurrentLinearProgress, _ElapsedTime);

            // 条件调用 OnStepUpdate（根据步长模式）
            if (act_on_StepUpdateCallbacks != null)
            {
                bool shouldCallStepUpdate = false;

                switch (_stepMode)
                {
                    case StepUpdateMode.TimeInterval:
                        // 时间间隔模式
                        if (_lastStepTime == 0 || currentTime - _lastStepTime >= _stepInterval)
                        {
                            shouldCallStepUpdate = true;
                            _lastStepTime = currentTime;
                        }
                        break;

                    case StepUpdateMode.ProgressStep:
                        // 进度步长模式
                        float currentProgress = _CurrentLinearProgress;
                        if (_lastStepProgress < 0 ||
                            currentProgress - _lastStepProgress >= _stepProgressInterval)
                        {
                            shouldCallStepUpdate = true;
                            _lastStepProgress = currentProgress;
                        }
                        break;

                    case StepUpdateMode.EveryFrame:
                    default:
                        // 每帧模式（默认，保持向后兼容）
                        shouldCallStepUpdate = true;
                        break;
                }

                // 添加回调执行代码
                if (shouldCallStepUpdate)
                {
                    act_on_StepUpdateCallbacks(_CurrentValue, _CurrentLinearProgress, _ElapsedTime);
                }
            }

            // OnProgress 保持原样
            if (act_on_ProgressCallbacks != null)
                act_on_ProgressCallbacks(_CurrentValue, _CurrentLinearProgress);

            return true;
        }
        /// <summary>
        /// 完成 Yoyo 模式下的循环
        /// 处理 Yoyo 模式下的动画反转逻辑
        /// </summary>
        /// <param name="currentTime">当前时间，单位为秒</param>
        /// <returns>是否继续动画</returns>
        private bool YoyoCompletion(float currentTime)
        {
            _IsReversing = !_IsReversing;
            _CurrentLoopCount++;

            bool isInfiniteLoop = _LoopCount == -1;
            bool isLoopComplete = _LoopCount >= 0 && _CurrentLoopCount > _LoopCount;

            if (isInfiniteLoop || !isLoopComplete)
            {
                // 只使用循环延迟，不使用初始延迟
                float loopDelay = _LoopingDelay;

                // 在Yoyo循环重置前强制设置精确的起始值
                if (_IsReversing)
                {
                    // 在Yoyo循环重置前强制设置精确的起始值
                    _CurrentValue = _EndValue;
                }
                else
                {
                    // 在Yoyo循环重置前强制设置精确的起始值
                    if (_IsFromMode)
                    {
                        // 在Yoyo循环重置前强制设置精确的起始值
                        _CurrentValue = (_StartValue);
                    }
                    else
                    {
                        // 在Yoyo循环重置前强制设置精确的起始值
                        _CurrentValue = (_StartValue);
                    }
                }
                // 修复编辑器模式下的时间重置问题
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    _editorStartTime = EditorApplication.timeSinceStartup + loopDelay;
#endif
                }
                else
                {
                    _StartTime = Time.time + loopDelay;
                }
                _ElapsedTime = 0f;
                _CurrentLinearProgress = 0f;

                // === 修复：重置步长状态 ===
                ResetStepState();

                if (act_on_RewindCallbacks != null)
                    act_on_RewindCallbacks();
                return true;
            }

            return FinalizeAnimationCompletion();
        }
        /// <summary>
        /// 完成 Restart 模式下的循环
        /// 处理 Restart 模式下的动画重新开始逻辑
        /// </summary>
        /// <param name="currentTime">当前时间，单位为秒</param>
        /// <returns>是否继续动画</returns>
        private bool RestartCompletion(float currentTime)
        {
            _CurrentLoopCount++;

            bool isInfiniteLoop = _LoopCount == -1;
            bool isLoopComplete = _LoopCount >= 0 && _CurrentLoopCount > _LoopCount;

            if (isInfiniteLoop || !isLoopComplete)
            {
                // 只使用循环延迟，不使用初始延迟
                float loopDelay = _LoopingDelay;

                // 设置延迟开始时间
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    _editorStartTime = EditorApplication.timeSinceStartup + loopDelay;
#endif
                }
                else
                {
                    _StartTime = Time.time + loopDelay;
                }

                // 标记为等待延迟状态
                _isWaitingLoopDelay = true;

                // 保持结束值
                _CurrentValue = _EndValue;

                // === 修复：重置步长状态 ===
                ResetStepState();

                return true;
            }

            return FinalizeAnimationCompletion();
        }
        /// <summary>
        /// 完成动画并执行最终处理
        /// 包括触发完成回调、自动销毁动画对象等
        /// </summary>
        /// <returns>是否停止动画</returns>
        private bool FinalizeAnimationCompletion()
        {
            // 添加时间修正
            _ElapsedTime = Duration;
            _CurrentLinearProgress = 1f;

            // === 修复Yoyo模式最终值 ===
            if (_loopType == XTween_LoopType.Yoyo)
            {
                // 根据总循环次数的奇偶性决定最终值
                if (_LoopCount % 2 == 1) // 奇数次循环
                {
                    _CurrentValue = _StartValue;
                }
                else // 偶数次循环
                {
                    _CurrentValue = _EndValue;
                }
            }
            else // 非Yoyo模式
            {
                _CurrentValue = _EndValue;
            }

            _IsCompleted = true;
            _IsPlaying = false;

            if (act_on_CompleteCallbacks != null)
                act_on_CompleteCallbacks(Duration);
            // 根据 _AutoKill 决定是否销毁
            if (_AutoKill)
            {
                Kill(false); // false=不重复触发完成回调
            }

            return false; // 停止动画
        }
        /// <summary>
        /// 暂停动画
        /// 记录暂停时间并停止动画更新
        /// </summary>
        public void Pause()
        {
            if (!_IsPlaying || _IsPaused) return;

            if (!Application.isPlaying)
            {
#if UNITY_EDITOR
                _editorPauseTime = EditorApplication.timeSinceStartup;
#endif
            }
            else
            {
                _PauseTime = Time.time;
            }
            _IsPaused = true;
            _IsPlaying = false;

            if (act_on_PauseCallbacks != null)
                act_on_PauseCallbacks();
        }
        /// <summary>
        /// 恢复动画播放
        /// 根据暂停时间调整动画进度并继续播放
        /// </summary>
        public void Resume()
        {
            if (_IsPlaying || !_IsPaused) return;

            if (!Application.isPlaying)
            {
#if UNITY_EDITOR
                double currentEditorTime = EditorApplication.timeSinceStartup;
                double pausedDuration = currentEditorTime - _editorPauseTime;
                _editorStartTime += pausedDuration;
#endif
            }
            else
            {
                // 情况1：动画已结束延迟（_ElapsedTime > 0），继续播放
                if (_ElapsedTime > 0)
                {
                    _StartTime = Time.time - _ElapsedTime;
                }
                // 情况2：动画仍在延迟中（_ElapsedTime == 0），恢复剩余延迟
                else
                {
                    float remainingDelay = Mathf.Max(0, _StartTime - _PauseTime);
                    _StartTime = Time.time + remainingDelay;
                }
            }

            _IsPaused = false;
            _IsPlaying = true;

            if (act_on_ResumeCallbacks != null)
                act_on_ResumeCallbacks();
        }
        /// <summary>
        /// 回绕动画到初始状态
        /// 可选地终止动画
        /// </summary>
        /// <param name="andKill">是否同时终止动画</param>
        public void Rewind(bool andKill = false)
        {
            if (_IsKilled && !andKill) return;

            _IsCompleted = false;
            _IsPlaying = false;
            _IsReversing = false;
            _ElapsedTime = 0f;
            _CurrentLoopCount = 0;

            // 线性进度归零
            _CurrentLinearProgress = 0f;
            // 缓动进度归零
            _CurrentEasedProgress = 0f;

            _CurrentValue = _IsFromMode ? _StartValue/*显式设置的起始值*/: _DefaultValue;/*默认起始值*/

            // === 修复：重置步长状态 ===
            ResetStepState();

            // 添加循环延迟重置
            if (_ApplyDelayPerLoop)
            {
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    _editorStartTime = EditorApplication.timeSinceStartup + _LoopingDelay;
#endif
                }
                else
                {
                    _StartTime = Time.time + _LoopingDelay;
                }
            }

            if (andKill)
            {
                Kill();
            }
            else
            {
                _hasStarted = false;
                if (!Application.isPlaying)
                {
#if UNITY_EDITOR
                    _editorStartTime = double.MaxValue;
#endif
                }
                else
                {
                    _StartTime = float.MaxValue;
                }
                if (_ApplyDelayPerLoop)
                {
                    if (!Application.isPlaying)
                    {
#if UNITY_EDITOR
                        _editorStartTime = EditorApplication.timeSinceStartup + _LoopingDelay;
#endif
                    }
                    else
                    {
                        _StartTime = Time.time + _LoopingDelay;
                    }
                }
            }

            if (act_on_RewindCallbacks != null)
                act_on_RewindCallbacks();
        }
        /// <summary>
        /// 终止动画
        /// 可选地触发完成回调
        /// </summary>
        /// <param name="complete">是否触发完成回调</param>
        public void Kill(bool complete = false)
        {
            if (_IsKilled) return;

            _IsKilled = true;
            _IsPlaying = false;
            _IsPaused = false;
            _IsCompleted = true;

            if (complete)
            {
                _CurrentValue = _EndValue;

                if (act_on_CompleteCallbacks != null)
                    act_on_CompleteCallbacks(Duration);
            }

            if (act_on_KillCallbacks != null)
                act_on_KillCallbacks();
            if (act_on_StopCallbacks != null)
                act_on_StopCallbacks();

            _LoopCount = 0; // 防止循环引用导致内存泄漏
            _CurrentLoopCount = 0;


            if (Application.isPlaying)
            {
                if (XTween_Pool.EnablePool)
                {
                    // 放回对象池
                    XTween_Pool.RecycleTween(this);
                }
            }
        }
        /// <summary>
        /// 重置动画状态
        /// 清除所有回调并重置动画参数
        /// </summary>
        public void ResetState()
        {
            ClearCallbacks();
            _IsKilled = false;
            _IsPlaying = false;
            _IsPaused = false;
            _IsCompleted = false;
            _hasStarted = false;
            _ElapsedTime = 0f;
            _CurrentLoopCount = 0;
            _CurrentLinearProgress = 0f;
            _CurrentEasedProgress = 0f;
            _IsReversing = false;
            _PauseTime = 0f;
            _StartTime = float.MaxValue;

            // 重置步长状态
            _stepMode = StepUpdateMode.EveryFrame;
            _stepInterval = 0f;
            _stepProgressInterval = 0f;
            _lastStepTime = 0f;
            _lastStepProgress = -1f;

            // 重置步长状态
            ResetStepState();
        }
        /// <summary>
        /// 清除所有回调函数
        /// </summary>
        public void ClearCallbacks()
        {
            act_on_StartCallbacks = null;
            act_on_StopCallbacks = null;
            act_on_CompleteCallbacks = null;
            act_on_UpdateCallbacks = null;
            act_on_StepUpdateCallbacks = null;
            act_on_ProgressCallbacks = null;
            act_on_KillCallbacks = null;
            act_on_PauseCallbacks = null;
            act_on_ResumeCallbacks = null;
            act_on_RewindCallbacks = null;
            act_on_DelayUpdateCallbacks = null;
        }
        /// <summary>
        /// 创建动画的唯一标识符和短标识符
        /// </summary>
        public void CreateIDs()
        {
            UniqueId = Guid.NewGuid();
            ShortId = GenerateShortId();
        }
        /// <summary>
        /// 重置步长更新状态
        /// 用于动画循环、重置等情况
        /// </summary>
        private void ResetStepState()
        {
            _lastStepTime = 0f;
            _lastStepProgress = -1f;

            // 如果是循环延迟等待状态，不重置步长模式
            if (!_isWaitingLoopDelay)
            {
                // 根据当前模式决定是否重置进度值
                if (_stepMode == StepUpdateMode.TimeInterval)
                {
                    _lastStepTime = 0f;
                }
                else if (_stepMode == StepUpdateMode.ProgressStep)
                {
                    _lastStepProgress = -1f;
                }
            }
        }
        #endregion

        #region 接口方法实现 - 链式方法
        /// <summary>
        /// 设置动画的缓动模式
        /// </summary>
        /// <param name="easeMode">缓动模式</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetEase(EaseMode ease)
        {
            return SetEase(ease);
        }
        /// <summary>
        /// 设置动画的缓动曲线
        /// </summary>
        /// <param name="curve">自定义缓动曲线</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetEase(AnimationCurve curve)
        {
            return SetEase(curve);
        }
        /// <summary>
        /// 设置动画的延迟时间
        /// </summary>
        /// <param name="delay">延迟时间，单位为秒</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetDelay(float delay)
        {
            return SetDelay(delay);
        }
        /// <summary>
        /// 设置动画的循环次数
        /// </summary>
        /// <param name="loopCount">循环次数，-1 表示无限循环</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetLoop(int loopCount)
        {
            return SetLoop(loopCount);
        }
        /// <summary>
        /// 设置动画的循环次数和循环类型
        /// </summary>
        /// <param name="loopCount">循环次数，-1 表示无限循环</param>
        /// <param name="loopType">循环类型</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetLoop(int loopCount, XTween_LoopType loopType)
        {
            return SetLoop(loopCount, loopType);
        }
        /// <summary>
        /// 设置动画的循环类型
        /// </summary>
        /// <param name="loopType">循环类型</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetLoopType(XTween_LoopType loopType)
        {
            return SetLoopType(loopType);
        }
        /// <summary>
        /// 设置每次循环之间的延迟时间
        /// </summary>
        /// <param name="loopingDelay">循环延迟时间，单位为秒</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetLoopingDelay(float loopDelay)
        {
            return SetLoopingDelay(loopDelay);
        }
        /// <summary>
        /// 设置动画的起始值
        /// </summary>
        /// <param name="startValue">起始值</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetFrom(object startValue)
        {
            return SetFrom((TArg)startValue);
        }
        /// <summary>
        /// 设置是否使用相对值进行动画
        /// </summary>
        /// <param name="relative">是否使用相对值</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetRelative(bool relative)
        {
            return SetRelative(relative);
        }
        /// <summary>
        /// 设置动画是否自动销毁
        /// </summary>
        /// <param name="autokill">是否自动销毁</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.SetAutoKill(bool autokill)
        {
            return SetAutokill(autokill);
        }
        /// <summary>
        /// 设置步长更新时间间隔（接口实现）
        /// </summary>
        XTween_Interface XTween_Interface.SetStepTimeInterval(float interval)
        {
            return SetStepTimeInterval(interval);
        }
        /// <summary>
        /// 设置步长更新进度间隔（接口实现）
        /// </summary>
        XTween_Interface XTween_Interface.SetStepProgressInterval(float interval)
        {
            return SetStepProgressInterval(interval);
        }

        #endregion

        #region 接口方法实现 - 链式回调
        /// <summary>
        /// 添加动画开始时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnStart(Action callback, XTweenActionOpration ActionOpration)
        {
            return OnStart(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画停止时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnStop(Action callback, XTweenActionOpration ActionOpration)
        {
            return OnStop(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画完成时的回调函数
        /// </summary>
        /// <param name="callback">回调函数，接收一个参数表示动画的总持续时间</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnComplete(Action<float> callback, XTweenActionOpration ActionOpration)
        {
            return OnComplete(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画更新时的回调函数
        /// </summary>
        /// <param name="callback">回调函数，接收三个参数：
        /// - 当前动画值
        /// - 当前线性进度（范围为 [0, 1]）
        /// - 当前已耗时（单位为秒）
        /// </param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnUpdate<TVal>(Action<TVal, float, float> callback, XTweenActionOpration ActionOpration)
        {
            if (callback is Action<TArg, float, float> typedCallback)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_UpdateCallbacks += typedCallback;
                else
                    act_on_UpdateCallbacks -= typedCallback;
            }
            else
            {
                Debug.LogError($"Type mismatch! Expected {typeof(TArg)}, got {typeof(TVal)}");
            }

            return this;
        }
        /// <summary>
        /// 移除动画步骤更新时的回调函数
        /// </summary>
        /// <param name="callback">回调函数，接收三个参数：
        /// - 当前动画值
        /// - 当前线性进度（范围为 [0, 1]）
        /// - 当前已耗时（单位为秒）
        /// </param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnStepUpdate<TVal>(Action<TVal, float, float> callback, XTweenActionOpration ActionOpration)
        {
            if (callback is Action<TArg, float, float> typedCallback)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_StepUpdateCallbacks += typedCallback;
                else
                    act_on_StepUpdateCallbacks -= typedCallback;
            }
            else
            {
                Debug.LogError($"Type mismatch! Expected {typeof(TArg)}, got {typeof(TVal)}");
            }
            return this;
        }
        /// <summary>
        /// 添加动画进度更新时的回调函数
        /// </summary>
        /// <param name="callback">回调函数，接收两个参数：
        /// - 当前动画值
        /// - 当前线性进度（范围为 [0, 1]）
        /// </param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnProgress<TVal>(Action<TVal, float> callback, XTweenActionOpration ActionOpration)
        {
            if (callback is Action<TArg, float> typedCallback)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_ProgressCallbacks += typedCallback;
                else
                    act_on_ProgressCallbacks -= typedCallback;
            }
            else
            {
                Debug.LogError($"Type mismatch! Expected {typeof(TArg)}, got {typeof(TVal)}");
            }
            return this;
        }
        /// <summary>
        /// 添加动画终止时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnKill(Action callback, XTweenActionOpration ActionOpration)
        {
            return OnKill(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画暂停时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnPause(Action callback, XTweenActionOpration ActionOpration)
        {
            return OnPause(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画恢复播放时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnResume(Action callback, XTweenActionOpration ActionOpration)
        {
            return OnResume(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画回绕时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnRewind(Action callback, XTweenActionOpration ActionOpration)
        {
            return OnRewind(callback, ActionOpration);
        }
        /// <summary>
        /// 添加动画延迟更新时的回调函数
        /// </summary>
        /// <param name="callback">回调函数，接收一个参数表示当前延迟的进度（范围为 [0, 1]）</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface XTween_Interface.OnDelayUpdate(Action<float> callback, XTweenActionOpration ActionOpration)
        {
            return OnDelayUpdate(callback, ActionOpration);
        }
        #endregion

        #region 链式配置方法 - 属性
        /// <summary>
        /// 设置动画的起始值
        /// 如果动画处于从初始值开始的模式（IsFromMode），此值将被立即应用
        /// </summary>
        /// <param name="startValue">起始值</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetFrom(TArg startValue)
        {
            _StartValue = startValue;
            // 标记为使用显式起始值
            _IsFromMode = true;
            // 立即更新当前值
            _CurrentValue = _StartValue;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的延迟时间
        /// 延迟时间必须是非负值
        /// </summary>
        /// <param name="delay">延迟时间，单位为秒</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetDelay(float delay)
        {
            _Delay = Mathf.Max(0, delay);
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的缓动模式
        /// 如果动画正在播放且设置了循环，将重新计算缓动进度
        /// </summary>
        /// <param name="easeMode">缓动模式</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetEase(EaseMode easeMode)
        {
            _easeMode = easeMode;
            // 明确不使用自定义曲线
            _UseCustomEaseCurve = false;
            // 循环时自动重新应用缓动曲线
            if (_IsPlaying && _LoopCount != 0)
            {
                _CurrentEasedProgress = XTween_EaseLibrary.Evaluate(_easeMode, _ElapsedTime, Duration);
            }
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的自定义缓动曲线
        /// 如果提供的曲线为空，将恢复为默认缓动模式
        /// </summary>
        /// <param name="curve">自定义缓动曲线</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetEase(AnimationCurve curve)
        {
            if (curve == null)
            {
                Debug.LogError("Provided AnimationCurve is null. Using default EaseMode instead.");
                _UseCustomEaseCurve = false;
                return ReturnSelf();
            }

            _CustomEaseCurve = new AnimationCurve(curve.keys); // 创建副本以避免外部修改
            _UseCustomEaseCurve = true;
            return ReturnSelf();
        }
        /// <summary>
        /// 清除自定义缓动曲线
        /// 清除后将恢复为默认缓动模式
        /// </summary>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> ClearCustomEase()
        {
            _UseCustomEaseCurve = false;
            _CustomEaseCurve = null;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的结束值
        /// 动画将从起始值过渡到此结束值
        /// </summary>
        /// <param name="newEndValue">结束值</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetEndValue(TArg newEndValue)
        {
            _EndValue = newEndValue;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的持续时间
        /// 持续时间必须是非负值
        /// </summary>
        /// <param name="newDuration">持续时间，单位为秒</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetDuration(float newDuration)
        {
            Duration = newDuration;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的循环次数
        /// 如果持续时间为 0，则无法设置循环
        /// 循环次数为 -1 表示无限循环，0 表示单次播放，大于 0 表示按指定次数循环
        /// </summary>
        /// <param name="loopCount">循环次数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetLoop(int loopCount = 0)
        {
            if (Mathf.Approximately(Duration, 0f))
            {
                Debug.LogWarning("Cannot set loop on zero-duration animation");
                return ReturnSelf();
            }

            // 添加明确的说明
            if (loopCount < -1)
            {
                Debug.LogWarning($"Invalid loopCount: {loopCount}. Auto-corrected to -1 (infinite)");
                loopCount = -1;
            }

            _LoopCount = loopCount;
            _CurrentLoopCount = 0; // 重置计数器
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的循环次数和循环类型
        /// 如果持续时间为 0，则无法设置循环
        /// 循环次数为 -1 表示无限循环，0 表示单次播放，大于 0 表示按指定次数循环
        /// </summary>
        /// <param name="loopCount">循环次数</param>
        /// <param name="loopType">循环类型</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetLoop(int loopCount = 0, XTween_LoopType loopType = XTween_LoopType.Restart)
        {
            if (Mathf.Approximately(Duration, 0f))
            {
                Debug.LogWarning("Cannot set loop on zero-duration animation");
                return ReturnSelf();
            }

            // 添加说明：0=播放一次，1=播放一次+重复一次，-1=无限循环
            _LoopCount = loopCount < -1 ? -1 : loopCount;
            _loopType = loopType;
            _CurrentLoopCount = 0;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画的循环类型
        /// </summary>
        /// <param name="loopType">循环类型</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetLoopType(XTween_LoopType loopType)
        {
            _loopType = loopType;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置每次循环之间的延迟时间
        /// 循环延迟时间必须是非负值
        /// 如果设置了循环延迟，将自动应用到每次循环中
        /// </summary>
        /// <param name="loopDelay">循环延迟时间，单位为秒</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetLoopingDelay(float loopDelay)
        {
            _LoopingDelay = Mathf.Max(0, loopDelay);
            _ApplyDelayPerLoop = loopDelay > 0; // 自动设置是否应用循环延迟
            return ReturnSelf();
        }
        /// <summary>
        /// 设置是否使用相对值进行动画
        /// 如果为 true，动画值将相对于起始值变化
        /// </summary>
        /// <param name="relative">是否使用相对值</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetRelative(bool relative)
        {
            _IsRelative = relative;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画是否自动销毁
        /// 如果为 true，动画完成时将自动销毁
        /// </summary>
        /// <param name="autokill">是否自动销毁</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetAutokill(bool autokill)
        {
            _AutoKill = autokill;
            return ReturnSelf();
        }
        /// <summary>
        /// 设置动画是否已完成
        /// 如果为 true，动画将被标记为已完成
        /// </summary>
        /// <param name="completed">是否已完成</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> SetCompleted(bool completed)
        {
            _IsCompleted = completed;
            return ReturnSelf();
        }
        #endregion

        #region 链式配置方法 - 步长控制
        /// <summary>
        /// 设置步长更新时间间隔
        /// </summary>
        /// <param name="interval">间隔时间（秒），0表示每帧执行</param>
        /// <returns>当前动画对象</returns>
        public XTween_Base<TArg> SetStepTimeInterval(float interval)
        {
            _stepMode = StepUpdateMode.TimeInterval;
            _stepInterval = Mathf.Max(0.001f, interval); // 最小1毫秒
            _lastStepTime = 0f;
            return ReturnSelf();
        }

        /// <summary>
        /// 设置步长更新进度间隔
        /// </summary>
        /// <param name="interval">进度间隔（0-1），例如0.1表示每10%进度执行一次</param>
        /// <returns>当前动画对象</returns>
        public XTween_Base<TArg> SetStepProgressInterval(float interval)
        {
            _stepMode = StepUpdateMode.ProgressStep;
            _stepProgressInterval = Mathf.Clamp(interval, 0.001f, 1f); // 最小0.1%进度
            _lastStepProgress = -1f;
            return ReturnSelf();
        }

        /// <summary>
        /// 重置为每帧更新（默认模式）
        /// </summary>
        /// <returns>当前动画对象</returns>
        public XTween_Base<TArg> SetStepEveryFrame()
        {
            _stepMode = StepUpdateMode.EveryFrame;
            return ReturnSelf();
        }
        #endregion

        #region 链式配置方法 - 状态回调
        /// <summary>
        /// 添加动画开始时的回调函数
        /// 当动画开始播放时，此回调将被触发
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnStart(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_StartCallbacks += callback;
                else
                    act_on_StartCallbacks -= callback;
            }
            return ReturnSelf();
        }
        /// <summary>
        /// 添加动画停止时的回调函数
        /// 当动画停止时，此回调将被触发
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnStop(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_StopCallbacks += callback;
                else
                    act_on_StopCallbacks -= callback;
            }
            return ReturnSelf();
        }
        /// <summary>
        /// 添加动画完成时的回调函数
        /// 当动画完成时，此回调将被触发
        /// 回调函数接收一个参数，表示动画的总持续时间
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnComplete(Action<float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_CompleteCallbacks += callback;
                else
                    act_on_CompleteCallbacks -= callback;
            }
            return ReturnSelf();
        }
        /// <summary>
        /// 添加动画更新时的回调函数
        /// 在动画的每一帧更新时，此回调将被触发
        /// 回调函数接收三个参数：
        /// - 当前动画值
        /// - 当前线性进度（范围为 [0, 1]）
        /// - 当前已耗时（单位为秒）
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnUpdate(Action<TArg, float, float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_UpdateCallbacks += callback;
                else
                    act_on_UpdateCallbacks -= callback;
            }
            return ReturnSelf();
        }
        /// <summary>
        /// 添加动画步骤更新时的回调函数
        /// 在动画的每一帧更新时，此回调将被触发
        /// 回调函数接收三个参数：
        /// - 当前动画值
        /// - 当前线性进度（范围为 [0, 1]）
        /// - 当前已耗时（单位为秒）
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnStepUpdate(Action<TArg, float, float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_StepUpdateCallbacks += callback;
                else
                    act_on_StepUpdateCallbacks -= callback;

            }
            return ReturnSelf();
        }
        /// <summary>
        /// 添加动画进度更新时的回调函数
        /// 在动画的每一帧更新时，此回调将被触发
        /// 回调函数接收两个参数：
        /// - 当前动画值
        /// - 当前线性进度（范围为 [0, 1]）
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnProgress(Action<TArg, float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_ProgressCallbacks += callback;
                else
                    act_on_ProgressCallbacks -= callback;
            }
            return ReturnSelf();
        }
        /// <summary>
        /// 添加动画终止时的回调函数
        /// 当动画被终止时，此回调将被触发
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnKill(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_KillCallbacks += callback;
                else
                    act_on_KillCallbacks -= callback;
            }
            return this;
        }
        /// <summary>
        /// 添加动画暂停时的回调函数
        /// 当动画暂停时，此回调将被触发
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnPause(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_PauseCallbacks += callback;
                else
                    act_on_PauseCallbacks -= callback;
            }
            return this;
        }
        /// <summary>
        /// 添加动画恢复播放时的回调函数
        /// 当动画从暂停状态恢复播放时，此回调将被触发
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnResume(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_ResumeCallbacks += callback;
                else
                    act_on_ResumeCallbacks -= callback;

            }
            return this;
        }
        /// <summary>
        /// 添加动画回绕时的回调函数
        /// 当动画回绕到初始状态时，此回调将被触发
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnRewind(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                // 保持原有的条件检查逻辑
                Action wrappedCallback = () =>
                {
                    if (_LoopCount != 0 || !_hasStarted)
                        callback();
                };

                if (callback != null)
                {
                    if (ActionOpration == XTweenActionOpration.Register)
                        act_on_RewindCallbacks += callback;
                    else
                        act_on_RewindCallbacks -= callback;
                }
            }
            return this;
        }
        /// <summary>
        /// 添加动画延迟更新时的回调函数
        /// 在动画的延迟阶段，此回调将被触发
        /// 回调函数接收一个参数，表示当前延迟的进度（范围为 [0, 1]）
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象，支持链式调用</returns>
        public XTween_Base<TArg> OnDelayUpdate(Action<float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register)
        {
            if (callback != null)
            {
                if (ActionOpration == XTweenActionOpration.Register)
                    act_on_DelayUpdateCallbacks += callback;
                else
                    act_on_DelayUpdateCallbacks -= callback;
            }
            return this;
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 抽象方法 - 执行类型特定的插值计算
        /// </summary>
        /// <param tweenName="a">起始值（类型 TArg）</param>
        /// <param tweenName="b">目标值（类型 TArg）</param>
        /// <param tweenName="t">
        /// 插值系数：
        /// - 通常范围 [0, 1]
        /// - 特殊缓动曲线可能超出该范围（如弹性效果）
        /// </param>
        /// <returns>计算后的插值结果（类型 TArg）</returns>
        /// <remarks>
        /// 设计要求：
        /// 1. 子类必须实现此方法以支持特定类型的插值
        /// 2. 建议使用对应类型的 LerpUnclamped 方法实现
        /// 3. 应保持插值的数学准确性
        /// 
        /// 典型实现示例：
        /// - 二维向量_Vector2: 二维向量_Vector2.LerpUnclamped(a, b, t)
        /// - float: Mathf.LerpUnclamped(a, b, t)
        /// - 颜色_Color: 颜色_Color.LerpUnclamped(a, b, t)
        /// </remarks>
        protected abstract TArg Lerp(TArg a, TArg b, float t);
        /// <summary>
        /// 抽象方法 - 获取该类型的默认初始值
        /// </summary>
        /// <returns>类型 TArg 的默认值</returns>
        /// <remarks>
        /// 设计要求：
        /// 1. 子类必须返回合理的类型默认值
        /// 2. 用于以下场景：
        ///    - 未设置自定义起始值时
        ///    - 调用 Rewind() 复位时
        /// 
        /// 典型实现示例：
        /// - 二维向量_Vector2: 二维向量_Vector2.zero
        /// - float: 0f
        /// - 颜色_Color: 颜色_Color.clear
        /// </remarks>
        protected abstract TArg GetDefaultValue();
        /// <summary>
        /// 抽象方法 - 返回当前实例（用于链式调用）
        /// </summary>
        /// <returns>当前具体子类实例</returns>
        /// <remarks>
        /// 设计要点：
        /// 1. 解决基类方法返回类型与子类不一致的问题
        /// 2. 每个子类应实现为：return this;
        /// 
        /// 典型实现示例：
        /// public override HudAnimation_TweenerBase<float> ReturnSelf() => this;
        /// 
        /// 使用场景：
        /// SetFrom(...).SetEase(...).Play() 等链式调用
        /// </remarks>
        public abstract XTween_Base<TArg> ReturnSelf();
        #endregion

        #region 辅助方法
        /// <summary>
        /// 计算当前的缓动进度
        /// 根据当前的线性进度和缓动模式，返回缓动后的进度值
        /// 如果使用了自定义缓动曲线，则直接使用曲线计算
        /// 如果启用了缓存，则使用缓存的缓动值
        /// 否则，使用未缓存的缓动库计算
        /// </summary>
        /// <param name="linearProgress">线性进度，范围为 [0, 1]</param>
        /// <returns>缓动后的进度值，范围为 [0, 1]</returns>
        internal float CalculateEasedProgress(float linearProgress)
        {
            if (_UseCustomEaseCurve && _CustomEaseCurve != null)
            {
                return _CustomEaseCurve.Evaluate(linearProgress);
            }
            else
            {
                if (XTween_EaseCache.UseCache)
                {
                    return XTween_EaseCache.Evaluate(_easeMode, linearProgress);
                }
                else
                {
                    return XTween_EaseLibrary.EvaluateUncached(_easeMode, linearProgress, 1f);
                }
            }
        }
        /// <summary>
        /// 计算当前的动画值
        /// 根据当前的进度和缓动模式，返回当前的动画值
        /// 如果接近完成进度（容差值 0.9999），直接返回结束值或起始值（根据 Yoyo 模式和反转状态）
        /// 否则，根据缓动进度插值计算当前值
        /// </summary>
        /// <returns>当前的动画值</returns>
        protected virtual TArg CalculateCurrentValue()
        {
            // 添加精度容差 - 当接近1.0时直接返回结束值
            float progress = _ElapsedTime / Duration;
            if (progress >= 0.9999f) // 使用容差值避免浮点精度问题
            {
                return _loopType == XTween_LoopType.Yoyo && _IsReversing ? (_IsFromMode ? _StartValue : _DefaultValue) : _EndValue;
            }

            float easedT = CalculateEasedProgress(progress);

            if (_loopType == XTween_LoopType.Yoyo && _IsReversing)
            {
                easedT = 1f - easedT; // 反向计算
            }

            TArg startVal = _IsFromMode ? _StartValue/*显式设置的起始值*/: _DefaultValue;/*默认起始值*/
            return Lerp(startVal, _EndValue, easedT);
        }
        /// <summary>
        /// 生成一个简短的唯一标识符
        /// 使用 Guid 生成一个 Base64 编码的字符串，并进行格式化以确保兼容性
        /// </summary>
        /// <returns>简短的唯一标识符，长度为 8 个字符</returns>
        private static string GenerateShortId()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                   .Replace("=", "")
                   .Replace("/", "_")
                   .Replace("+", "-")
                   .Substring(0, 8);
        }
        #endregion
    }
}