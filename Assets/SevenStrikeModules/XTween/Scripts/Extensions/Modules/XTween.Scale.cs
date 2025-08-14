namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前缩放到目标缩放的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标缩放</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Scale_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative = false, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentScale = rectTransform.localScale;
            Vector3 targetScale = isRelative ? currentScale + endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((scale, linearProgress, time) =>
                {
                    rectTransform.localScale = scale;
                })
                .OnRewind(() =>
                {
                    rectTransform.localScale = currentScale;
                })
                .OnComplete((duration) =>
                {
                    rectTransform.localScale = targetScale;
                })
                .SetAutokill(autokill)
                .SetRelative(isRelative);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((scale, linearProgress, time) =>
                        {
                            rectTransform.localScale = scale;
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.localScale = currentScale;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.localScale = targetScale;
                        })
                        .SetAutokill(false)
                        .SetRelative(isRelative);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前缩放到目标缩放的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标缩放</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Scale_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative = false, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentScale = rectTransform.localScale;
            Vector3 targetScale = isRelative ? currentScale + endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                return tweener;
            }
            else
            {
                XTween_Interface tweener;

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply).OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetAutokill(false).SetFrom(fromval).SetEase(curve).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply).OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetAutokill(false).SetFrom(fromval).SetEase(easeMode).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply).OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetAutokill(false).SetEase(curve).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentScale, targetScale, duration * XTween_Dashboard.DurationMultiply).OnUpdate((scale, linearProgress, time) => { rectTransform.localScale = scale; }).OnRewind(() => { rectTransform.localScale = currentScale; }).OnComplete((duration) => { rectTransform.localScale = targetScale; }).SetAutokill(false).SetEase(easeMode).SetRelative(isRelative);
                    }
                }
                return tweener;
            }
        }
    }
}
