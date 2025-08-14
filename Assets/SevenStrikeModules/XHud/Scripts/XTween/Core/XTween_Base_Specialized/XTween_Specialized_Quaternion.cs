namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public enum HudRotateMode
    {
        LerpUnclamped,
        SlerpUnclamped
    }

    /// <summary>
    /// 专门处理 四元数_Quaternion 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<四元数_Quaternion>，实现了对 四元数_Quaternion 类型的插值计算
    /// 主要用于处理单个 四元数_Quaternion 的动画，例如3D旋转等
    /// </remarks>
    public class XTween_Specialized_Quaternion : XTween_Base<Quaternion>
    {
        public HudRotateMode HudRotateMode = HudRotateMode.LerpUnclamped;

        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <param name="mode"></param>
        public XTween_Specialized_Quaternion(Quaternion defaultFromValue, Quaternion endValue, float duration, HudRotateMode mode) : base(defaultFromValue, endValue, duration)
        {
            HudRotateMode = mode;
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Quaternion(Quaternion defaultFromValue, Quaternion endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            HudRotateMode = HudRotateMode.SlerpUnclamped;
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Quaternion()
        {
            _DefaultValue = Quaternion.identity;
            _EndValue = Quaternion.identity;
            _Duration = 1;
            _StartValue = Quaternion.identity;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线
            HudRotateMode = HudRotateMode.SlerpUnclamped;

            ResetState();
        }

        /// <summary>
        /// 执行 四元数_Quaternion 类型的插值计算。
        /// 使用 四元数_Quaternion.LerpUnclamped 方法，支持超出 [0, 1] 范围的插值系数
        /// </summary>
        /// <param tweenName="a">起始值。</param>
        /// <param tweenName="b">目标值。</param>
        /// <param tweenName="t">插值系数。</param>
        /// <returns>插值结果。</returns>
        protected override Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            if (HudRotateMode == HudRotateMode.SlerpUnclamped)
                /// <summary>
                /// 使用 四元数_Quaternion.SlerpUnclamped 方法计算插值
                /// 四元数_Quaternion.SlerpUnclamped 是 Unity 提供的插值方法，适用于 四元数_Quaternion 类型
                /// 与 四元数_Quaternion.SLerp 不同，SlerpUnclamped 不限制 t 的范围，允许 t 超出 [0, 1] 范围
                /// </summary>
                return Quaternion.SlerpUnclamped(a, b, t);
            else
                /// <summary>
                /// 使用 四元数_Quaternion.LerpUnclamped 方法计算插值
                /// 四元数_Quaternion.LerpUnclamped 是 Unity 提供的插值方法，适用于 四元数_Quaternion 类型
                /// 与 四元数_Quaternion.Lerp 不同，LerpUnclamped 不限制 t 的范围，允许 t 超出 [0, 1] 范围
                /// </summary>
                return Quaternion.LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// 获取 四元数_Quaternion 类型的默认初始值。
        /// 默认值为 四元数_Quaternion.identity
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override Quaternion GetDefaultValue()
        {
            /// <summary>
            /// 返回 四元数_Quaternion 类型的默认值 四元数_Quaternion.identity
            /// </summary>
            return Quaternion.identity;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<Quaternion> ReturnSelf()
        {
            /// <summary>
            /// 返回当前实例，支持链式调用
            /// </summary>
            return this;
        }
    }
}