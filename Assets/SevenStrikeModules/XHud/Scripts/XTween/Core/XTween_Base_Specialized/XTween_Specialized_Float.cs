namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    /// <summary>
    /// 专门处理 float 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<float>，实现了对 float 类型的插值计算
    /// 主要用于处理单个 浮点值 的动画，例如透明度、Image填充值等
    /// </remarks>
    public class XTween_Specialized_Float : XTween_Base<float>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Float(float defaultFromValue, float endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Float()
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
        /// 执行浮点值的插值计算。
        /// 使用 Mathf.Lerp 实现线性插值。
        /// </summary>
        /// <param tweenName="a">起始值。</param>
        /// <param tweenName="b">目标值。</param>
        /// <param tweenName="t">插值系数，范围通常为 [0, 1]。</param>
        /// <returns>插值结果。</returns>
        protected override float Lerp(float a, float b, float t)
        {
            return Mathf.Lerp(a, b, t);
        }

        /// <summary>
        /// 获取浮点值类型的默认初始值。
        /// 默认值为 0f。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override float GetDefaultValue()
        {
            return 0f;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<float> ReturnSelf()
        {
            return this;
        }
    }
}