namespace SevenStrikeModules.XTween
{
    using System;
    using UnityEngine;

    /// <summary>
    /// 定义动画的基本接口，提供动画的核心功能和状态管理
    /// 所有动画类型（如位置、缩放_Scale、颜色等）的实现类都必须实现此接口
    /// </summary>
    public interface XTween_Interface
    {
        #region  属性
        /// <summary>
        /// 获取或设置动画的唯一标识符（GUID）
        /// </summary>
        Guid UniqueId { get; set; }
        /// <summary>
        /// 获取或设置动画的短标识符（Short ID），用于快速查找
        /// </summary>
        string ShortId { get; set; }
        /// <summary>
        /// 获取动画的缓动模式，用于控制动画速度的变化
        /// </summary>
        EaseMode EaseMode { get; }
        /// <summary>
        /// 获取是否使用自定义缓动曲线
        /// </summary>
        bool UseCustomEaseCurve { get; }
        /// <summary>
        /// 获取自定义缓动曲线，仅当 UseCustomEaseCurve 为 true 时生效
        /// </summary>
        AnimationCurve CustomEaseCurve { get; }
        /// <summary>
        /// 获取动画的总持续时间，单位为秒
        /// </summary>
        float Duration { get; }
        /// <summary>
        /// 获取动画的延迟时间，单位为秒
        /// </summary>
        float Delay { get; }
        /// <summary>
        /// 获取动画的剩余延迟时间，单位为秒
        /// </summary>
        float RemainingDelay { get; }
        /// <summary>
        /// 获取动画的循环次数，-1 表示无限循环，0 表示单次播放，大于 0 按照次数循环
        /// </summary>
        int LoopCount { get; }
        /// <summary>
        /// 获取每次循环之间的延迟时间，单位为秒
        /// </summary>
        float LoopingDelay { get; }
        /// <summary>
        /// 获取当前循环次数
        /// </summary>
        int CurrentLoop { get; }
        /// <summary>
        /// 获取循环类型，例如重新开始或反转
        /// </summary>
        XTween_LoopType LoopType { get; }
        /// <summary>
        /// 获取动画的当前进度，范围为 [0, 1]
        /// </summary>
        float CurrentLoopProgress { get; }
        /// <summary>
        /// 获取动画的总进度，范围为 [0, 1]
        /// </summary>
        float Progress { get; }
        /// <summary>
        /// 获取是否自动销毁动画对象，当动画完成时生效
        /// </summary>
        bool AutoKill { get; }
        /// <summary>
        /// 获取动画是否已被终止
        /// </summary>
        bool IsKilled { get; }
        /// <summary>
        /// 获取动画是否正在播放
        /// </summary>
        bool IsPlaying { get; }
        /// <summary>
        /// 获取动画是否已暂停
        /// </summary>
        bool IsPaused { get; }
        /// <summary>
        /// 获取动画是否从初始值开始
        /// </summary>
        bool IsFromMode { get; }
        /// <summary>
        /// 获取是否使用相对值进行动画
        /// </summary>
        bool IsRelative { get; }
        /// <summary>
        /// 获取动画是否已完成
        /// </summary>
        bool IsCompleted { get; }
        /// <summary>
        /// 获取动画是否处于活动状态
        /// </summary>
        bool IsActive { get; }
        /// <summary>
        /// 获取动画的开始时间，单位为秒
        /// </summary>
        float StartTime { get; }
        /// <summary>
        /// 获取动画的结束时间，单位为秒
        /// </summary>
        float EndTime { get; }
        /// <summary>
        /// 获取动画的当前值
        /// </summary>
        object CurrentValue { get; }
        /// <summary>
        /// 获取动画的已耗时，单位为秒
        /// </summary>
        float ElapsedTime { get; }
        /// <summary>
        /// 获取动画的剩余时间，单位为秒
        /// </summary>
        float RemainingTime { get; }
        /// <summary>
        /// 获取动画的当前线性进度，范围为 [0, 1]
        /// </summary>
        float CurrentLinearProgress { get; }
        /// <summary>
        /// 获取动画的当前缓动进度，范围为 [0, 1]
        /// </summary>
        float CurrentEasedProgress { get; }
        /// <summary>
        /// 获取动画是否正在回绕
        /// </summary>
        bool IsRewinding { get; }
        /// <summary>
        /// 获取动画是否正在反转
        /// </summary>
        bool IsReversing { get; }
        /// <summary>
        /// 获取动画的暂停时间，单位为秒
        /// </summary>
        float PauseTime { get; }
        /// <summary>
        /// 获取动画是否已从对象池中回收
        /// </summary>
        bool IsPoolRecyled { get; set; }
        /// <summary>
        /// 获取动画的起始值
        /// </summary>
        object StartValue { get; set; }
        /// <summary>
        /// 获取动画是否正在等待循环延迟
        /// </summary>
        bool IsWaitingLoopDelay { get; }
        /// <summary>
        /// 获取动画是否正在循环
        /// </summary>
        bool IsLooping { get; }
        #endregion

        #region  方法
        /// <summary>
        /// 播放动画
        /// </summary>
        XTween_Interface Play();
        /// <summary>
        /// 更新动画状态
        /// </summary>
        /// <param name="currentTime">当前时间，单位为秒</param>
        /// <returns>是否继续更新动画</returns>
        bool Update(float currentTime);
        /// <summary>
        /// 暂停动画
        /// </summary>
        void Pause();
        /// <summary>
        /// 恢复动画播放
        /// </summary>
        void Resume();
        /// <summary>
        /// 回绕动画到初始状态
        /// </summary>
        /// <param name="andKill">是否同时终止动画</param>
        void Rewind(bool andKill = false);
        /// <summary>
        /// 终止动画
        /// </summary>
        /// <param name="complete">是否完成动画</param>
        void Kill(bool complete = false);
        /// <summary>
        /// 重置动画状态
        /// </summary>
        void ResetState();
        /// <summary>
        /// 创建动画的唯一标识符和短标识符
        /// </summary>
        void CreateIDs();
        /// <summary>
        /// 清除所有回调函数
        /// </summary>
        void ClearCallbacks();
        #endregion

        #region 扩展
        /// <summary>
        /// 设置缓动模式
        /// </summary>
        /// <param name="easeMode">缓动模式</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetEase(EaseMode easeMode);
        /// <summary>
        /// 设置自定义缓动曲线
        /// </summary>
        /// <param name="curve">自定义缓动曲线</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetEase(AnimationCurve curve);
        /// <summary>
        /// 设置动画的延迟时间
        /// </summary>
        /// <param name="delay">延迟时间，单位为秒</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetDelay(float delay);
        /// <summary>
        /// 设置动画的循环次数
        /// </summary>
        /// <param name="loopCount">循环次数，-1 表示无限循环</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetLoop(int loopCount = 0);
        /// <summary>
        /// 设置动画的循环次数和循环类型
        /// </summary>
        /// <param name="loopCount">循环次数，-1 表示无限循环</param>
        /// <param name="loopType">循环类型</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetLoop(int loopCount = 0, XTween_LoopType loopType = XTween_LoopType.Restart);
        /// <summary>
        /// 设置动画的循环类型
        /// </summary>
        /// <param name="loopType">循环类型</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetLoopType(XTween_LoopType loopType = XTween_LoopType.Restart);
        /// <summary>
        /// 设置每次循环之间的延迟时间
        /// </summary>
        /// <param name="loopingDelay">循环延迟时间，单位为秒</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetLoopingDelay(float loopingDelay);
        /// <summary>
        /// 设置动画的起始值
        /// </summary>
        /// <param name="startValue">起始值</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetFrom(object startValue);
        /// <summary>
        /// 设置是否使用相对值进行动画
        /// </summary>
        /// <param name="relative">是否使用相对值</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetRelative(bool relative);
        /// <summary>
        /// 设置动画是否自动销毁
        /// </summary>
        /// <param name="autokill">是否自动销毁</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetAutoKill(bool autokill);
        /// <summary>
        /// 设置步长更新时间间隔
        /// </summary>
        /// <param name="interval">间隔时间（秒）</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetStepTimeInterval(float interval);
        /// <summary>
        /// 设置步长更新进度间隔
        /// </summary>
        /// <param name="interval">进度间隔（0-1）</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface SetStepProgressInterval(float interval);
        #endregion

        #region  回调
        /// <summary>
        /// 添加动画开始时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnStart(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画停止时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnStop(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画完成时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnComplete(Action<float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画终止时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnKill(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画暂停时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnPause(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画恢复播放时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnResume(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画回绕时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnRewind(Action callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画延迟更新时的回调函数
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnDelayUpdate(Action<float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画更新时的回调函数
        /// </summary>
        /// <typeparam name="TVal">动画值的类型</typeparam>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnUpdate<TVal>(Action<TVal, float, float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画步骤更新时的回调函数
        /// </summary>
        /// <typeparam name="TVal">动画值的类型</typeparam>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnStepUpdate<TVal>(Action<TVal, float, float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        /// <summary>
        /// 添加动画进度更新时的回调函数
        /// </summary>
        /// <typeparam name="TVal">动画值的类型</typeparam>
        /// <param name="callback">回调函数</param>
        /// <returns>当前动画对象</returns>
        XTween_Interface OnProgress<TVal>(Action<TVal, float> callback, XTweenActionOpration ActionOpration = XTweenActionOpration.Register);
        #endregion
    }
}