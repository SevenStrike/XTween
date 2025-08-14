namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    /// <summary>
    /// 专门处理 四维向量_Vector4 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<四维向量_Vector4>，实现了对 四维向量_Vector4 类型的插值计算
    /// 主要用于处理单个 四维向量_Vector4 的动画，例如3D位置、缩放_Scale、旋转等
    /// </remarks>
    public class XTween_Specialized_Vector4 : XTween_Base<Vector4>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Vector4(Vector4 defaultFromValue, Vector4 endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Vector4()
        {
            _DefaultValue = Vector4.zero;
            _EndValue = Vector4.zero;
            _Duration = 1;
            _StartValue = Vector4.zero;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线

            ResetState();
        }
        /// <summary>
        /// 执行 四维向量_Vector4 类型的插值计算。
        /// 使用 四维向量_Vector4.LerpUnclamped 方法，支持超出 [0, 1] 范围的插值系数。
        /// </summary>
        /// <param tweenName="a">起始值。</param>
        /// <param tweenName="b">目标值。</param>
        /// <param tweenName="t">插值系数。</param>
        /// <returns>插值结果。</returns>
        protected override Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            /// <summary>
            /// 使用 四维向量_Vector4.LerpUnclamped 方法计算插值
            /// 四维向量_Vector4.LerpUnclamped 是 Unity 提供的插值方法，适用于 四维向量_Vector4 类型
            /// 与 四维向量_Vector4.Lerp 不同，LerpUnclamped 不限制 t 的范围，允许 t 超出 [0, 1] 范围
            /// </summary>
            return Vector4.LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// 获取 四维向量_Vector4 类型的默认初始值。
        /// 默认值为 四维向量_Vector4.zero。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override Vector4 GetDefaultValue()
        {
            /// <summary>
            /// 返回 四维向量_Vector4 类型的默认值 四维向量_Vector4.zero
            /// </summary>
            return Vector4.zero;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<Vector4> ReturnSelf()
        {
            /// <summary>
            /// 返回当前实例，支持链式调用
            /// </summary>
            return this;
        }
    }
}