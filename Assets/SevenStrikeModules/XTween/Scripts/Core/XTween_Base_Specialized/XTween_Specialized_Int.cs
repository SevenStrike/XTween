namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    /// <summary>
    /// 专门处理 整数_Int 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<int>，实现了对 int 类型的插值计算
    /// 主要用于处理单个 整型值 的动画
    /// </remarks>
    public class XTween_Specialized_Int : XTween_Base<int>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Int(int defaultFromValue, int endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Int()
        {
            _DefaultValue = 0;
            _EndValue = 0;
            _Duration = 1;
            _StartValue = 0;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线

            ResetState();
        }

        /// <summary>
        /// 执行整型值的插值计算。
        /// 使用自定义的插值方法，确保结果为整型。
        /// </summary>
        /// <param name="a">起始值。</param>
        /// <param name="b">目标值。</param>
        /// <param name="t">插值系数，范围通常为 [0, 1]。</param>
        /// <returns>插值结果。</returns>
        protected override int Lerp(int a, int b, float t)
        {
            // 使用 Mathf.Lerp 计算浮点插值结果，然后取整
            return Mathf.RoundToInt(Mathf.Lerp(a, b, t));
        }

        /// <summary>
        /// 获取整型值类型的默认初始值。
        /// 默认值为 0。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override int GetDefaultValue()
        {
            return 0;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<int> ReturnSelf()
        {
            return this;
        }
    }
}