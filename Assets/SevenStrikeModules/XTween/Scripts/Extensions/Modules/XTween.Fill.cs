namespace SevenStrikeModules.XTween
{
    using UnityEngine;
    using UnityEngine.UI;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前填充量到目标填充量的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="image">目标 Image组件 组件</param>
        /// <param name="endValue">目标填充量</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Fill_To(this Image image, float endValue, float duration, bool autokill = false)
        {
            if (image == null)
            {
                Debug.LogError("Image component is null!");
                return null;
            }

            float startalpha = image.fillAmount;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((fill, linearProgres, time) =>
                {
                    image.fillAmount = fill;
                })
                .OnRewind(() =>
                {
                    image.fillAmount = startalpha;
                })
                .OnComplete((duration) =>
                {
                    image.fillAmount = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((fill, linearProgres, time) =>
                        {
                            image.fillAmount = fill;
                        })
                        .OnRewind(() =>
                        {
                            image.fillAmount = startalpha;
                        })
                        .OnComplete((duration) =>
                        {
                            image.fillAmount = endValue;
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前填充量到目标填充量的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="image">目标 Image组件 组件</param>
        /// <param name="endValue">目标填充量</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Fill_To(this Image image, float endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (image == null)
            {
                Debug.LogError("Image component is null!");
                return null;
            }

            float startalpha = image.fillAmount;

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
                        tweener.OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startalpha, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((fill, linearProgres, time) => { image.fillAmount = fill; }).OnRewind(() => { image.fillAmount = startalpha; }).OnComplete((duration) => { image.fillAmount = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
