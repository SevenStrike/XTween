namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    /// <summary>
    /// 专门处理 颜色_Color 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<颜色_Color>，实现了对 颜色_Color 类型的插值计算
    /// 主要用于处理单个 颜色_Color 的动画，例如Image颜色改变
    /// </remarks>
    public class XTween_Specialized_Color : XTween_Base<Color>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Color(Color defaultFromValue, Color endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Color()
        {
            _DefaultValue = Color.white;
            _EndValue = Color.black;
            _Duration = 1;
            _StartValue = Color.white;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线

            ResetState();
        }

        /// <summary>
        /// 执行 颜色_Color 类型的插值计算。
        /// 使用 颜色_Color.LerpUnclamped 方法，支持超出 [0, 1] 范围的插值系数。
        /// </summary>
        /// <param tweenName="a">起始值。</param>
        /// <param tweenName="b">目标值。</param>
        /// <param tweenName="t">插值系数。</param>
        /// <returns>插值结果。</returns>
        protected override Color Lerp(Color a, Color b, float t)
        {
            /// <summary>
            /// 使用 颜色_Color.LerpUnclamped 方法计算插值
            /// 颜色_Color.LerpUnclamped 是 Unity 提供的插值方法，适用于 颜色_Color 类型
            /// 与 颜色_Color.Lerp 不同，颜色_Color 不限制 t 的范围，允许 t 超出 [0, 1] 范围
            /// </summary>
            return Color.LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// 获取 颜色_Color 类型的默认初始值。
        /// 默认值为 颜色_Color.white。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override Color GetDefaultValue()
        {
            /// <summary>
            /// 返回 颜色_Color 类型的默认值 颜色_Color.white
            /// </summary>
            return Color.white;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<Color> ReturnSelf()
        {
            /// <summary>
            /// 返回当前实例，支持链式调用
            /// </summary>
            return this;
        }
    }
}