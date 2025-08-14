namespace SevenStrikeModules.XTween
{
    using UnityEngine;
    using UnityEngine.UI;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="image">目标 Image组件 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Color_To(this Image image, Color endValue, float duration, bool autokill = false)
        {
            if (image == null)
            {
                Debug.LogError("Image component is null!");
                return null;
            }

            // 获取当前颜色
            Color startColor = image.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((color, linearProgress, time) =>
                {
                    image.color = color;
                })
                .OnRewind(() =>
                {
                    image.color = startColor;
                })
                .OnComplete((duration) =>
                {
                    image.color = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((color, linearProgress, time) =>
                        {
                            image.color = color;
                        })
                        .OnRewind(() =>
                        {
                            image.color = startColor;
                        })
                        .OnComplete((duration) =>
                        {
                            image.color = endValue;
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="image">目标 Image组件 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Color_To(this Image image, Color endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Color> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (image == null)
            {
                Debug.LogError("Image component is null!");
                return null;
            }

            // 获取当前颜色
            Color startColor = image.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgress, time) => { image.color = color; }).OnRewind(() => { image.color = startColor; }).OnComplete((duration) => { image.color = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}