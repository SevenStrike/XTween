namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    /// <summary>
    /// 专门处理 二维向量_Vector2 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<二维向量_Vector2>，实现了对 二维向量_Vector2 类型的插值计算
    /// 主要用于处理单个 二维向量_Vector2 的动画，例如尺寸、2D位移等
    /// </remarks>
    public class XTween_Specialized_Vector2 : XTween_Base<Vector2>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Vector2(Vector2 defaultFromValue, Vector2 endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Vector2()
        {
            _DefaultValue = Vector2.zero;
            _EndValue = Vector2.zero;
            _Duration = 1;
            _StartValue = Vector2.zero;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线

            ResetState();
        }

        /// <summary>
        /// 执行 二维向量_Vector2 类型的插值计算。
        /// 使用 二维向量_Vector2.LerpUnclamped 实现插值，支持超出 [0, 1] 范围的 t 值。
        /// </summary>
        /// <param tweenName="a">起始值。</param>
        /// <param tweenName="b">目标值。</param>
        /// <param tweenName="t">插值系数。</param>
        /// <returns>插值结果。</returns>
        protected override Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return Vector2.LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// 获取 二维向量_Vector2 类型的默认初始值。
        /// 默认值为 二维向量_Vector2.zero。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override Vector2 GetDefaultValue()
        {
            return Vector2.zero;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<Vector2> ReturnSelf()
        {
            return this;
        }
    }
}