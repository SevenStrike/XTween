namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    /// <summary>
    /// 专门处理 三维向量_Vector3 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<三维向量_Vector3>，实现了对 三维向量_Vector3 类型的插值计算
    /// 主要用于处理单个 三维向量_Vector3 的动画，例如3D位置、缩放_Scale、旋转等
    /// </remarks>
    public class XTween_Specialized_Vector3 : XTween_Base<Vector3>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Vector3(Vector3 defaultFromValue, Vector3 endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Vector3()
        {
            _DefaultValue = Vector3.zero;
            _EndValue = Vector3.zero;
            _Duration = 1;
            _StartValue = Vector3.zero;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线

            ResetState();
        }

        /// <summary>
        /// 执行 三维向量_Vector3 类型的插值计算。
        /// 使用 三维向量_Vector3.LerpUnclamped 方法，支持超出 [0, 1] 范围的插值系数。
        /// </summary>
        /// <param tweenName="a">起始值。</param>
        /// <param tweenName="b">目标值。</param>
        /// <param tweenName="t">插值系数。</param>
        /// <returns>插值结果。</returns>
        protected override Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            /// <summary>
            /// 使用 三维向量_Vector3.LerpUnclamped 方法计算插值
            /// 三维向量_Vector3.LerpUnclamped 是 Unity 提供的插值方法，适用于 三维向量_Vector3 类型
            /// 与 三维向量_Vector3.Lerp 不同，LerpUnclamped 不限制 t 的范围，允许 t 超出 [0, 1] 范围
            /// </summary>
            return Vector3.LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// 获取 三维向量_Vector3 类型的默认初始值。
        /// 默认值为 三维向量_Vector3.zero。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override Vector3 GetDefaultValue()
        {
            /// <summary>
            /// 返回 三维向量_Vector3 类型的默认值 三维向量_Vector3.zero
            /// </summary>
            return Vector3.zero;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<Vector3> ReturnSelf()
        {
            /// <summary>
            /// 返回当前实例，支持链式调用
            /// </summary>
            return this;
        }
    }
}