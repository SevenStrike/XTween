namespace SevenStrikeModules.XTween
{
    using UnityEngine;
    using UnityEngine.UI;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前透明度到目标透明度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="image">目标 Image组件 组件</param>
        /// <param name="endValue">目标透明度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Alpha_To(this Image image, float endValue, float duration, bool autokill = false)
        {
            if (image == null)
            {
                Debug.LogError("Image component is null!");
                return null;
            }

            Color startColor = image.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((alpha, linearProgress, time) =>
                {
                    image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                })
                        .OnRewind(() =>
                        {
                            image.color = startColor;
                        })
                        .OnComplete((duration) =>
                        {
                            image.color = new Color(startColor.r, startColor.g, startColor.b, endValue);
                        })
                        .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((alpha, linearProgress, time) =>
                        {
                            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                        })
                        .OnRewind(() =>
                        {
                            image.color = startColor;
                        })
                        .OnComplete((duration) =>
                        {
                            image.color = new Color(startColor.r, startColor.g, startColor.b, endValue);
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前透明度到目标透明度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="image">目标 Image组件 组件</param>
        /// <param name="endValue">目标透明度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Alpha_To(this Image image, float endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (image == null)
            {
                Debug.LogError("Image component is null!");
                return null;
            }

            Color startColor = image.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a); }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a); }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetEase(easeMode).SetAutokill(autokill);
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
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a); }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a); }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startColor.a, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgress, time) => { image.color = new Color(startColor.r, startColor.g, startColor.b, alpha); }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = new Color(startColor.r, startColor.g, startColor.b, endValue); }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前透明度到目标透明度的动画过渡，适用于 CanvasGroup组件 组件
        /// </summary>
        /// <param name="canvasgroup">目标 CanvasGroup组件 组件</param>
        /// <param name="endValue">目标透明度值，范围为 0（完全透明）到 1（完全不透明）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁动画对象，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Alpha_To(this CanvasGroup canvasgroup, float endValue, float duration, bool autokill = false)
        {
            if (canvasgroup == null)
            {
                Debug.LogError("CanvasGroup component is null!");
                return null;
            }

            float startalpha = canvasgroup.alpha;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((alpha, linearProgres, time) =>
                {
                    canvasgroup.alpha = alpha;
                })
                .OnRewind(() =>
                {
                    canvasgroup.alpha = startalpha;
                })
                .OnComplete((duration) =>
                {
                    canvasgroup.alpha = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply)
                     .OnUpdate((alpha, linearProgres, time) =>
                     {
                         canvasgroup.alpha = alpha;
                     })
                     .OnRewind(() =>
                     {
                         canvasgroup.alpha = startalpha;
                     })
                     .OnComplete((duration) =>
                     {
                         canvasgroup.alpha = endValue;
                     })
                     .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前透明度到目标透明度的动画过渡，适用于 CanvasGroup组件 组件
        /// </summary>
        /// <param name="canvasgroup">目标 CanvasGroup组件 组件</param>
        /// <param name="endValue">目标透明度值，范围为 0（完全透明）到 1（完全不透明）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁动画对象，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Alpha_To(this CanvasGroup canvasgroup, float endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (canvasgroup == null)
            {
                Debug.LogError("CanvasGroup component is null!");
                return null;
            }

            float startalpha = canvasgroup.alpha;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((alpha, linearProgres, time) => { canvasgroup.alpha = alpha; }).OnRewind(() => { canvasgroup.alpha = startalpha; }).OnComplete((duration) => { canvasgroup.alpha = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }

    }
}
