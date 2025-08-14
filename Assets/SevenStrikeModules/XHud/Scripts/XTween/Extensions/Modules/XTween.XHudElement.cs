namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前原始颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="element">目标 Hud_Manager 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Alpha_To(this Hud_Element element, float endValue, float duration, bool autokill = false)
        {
            if (element == null)
            {
                Debug.LogError("XHud_Element component is null!");
                return null;
            }

            // 获取当前颜色
            float startColor = element.Alpha;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                tweener.OnUpdate((color, linearProgress, time) =>
                {
                    element.Alpha = color;
                })
                .OnRewind(() =>
                {
                    element.Alpha = startColor;
                })
                .OnComplete((duration) =>
                {
                    element.Alpha = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply)
                        .OnUpdate((color, linearProgress, time) =>
                        {
                            element.Alpha = color;
                            element.element_AlphaSyncUpdate();
                        })
                        .OnRewind(() =>
                        {
                            element.Alpha = startColor;
                            element.element_AlphaSyncUpdate();
                        })
                        .OnComplete((duration) =>
                        {
                            element.Alpha = endValue;
                            element.element_AlphaSyncUpdate();
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前原始颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="element">目标 Hud_Manager 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Alpha_To(this Hud_Element element, float endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (element == null)
            {
                Debug.LogError("XHud_Element component is null!");
                return null;
            }

            // 获取当前颜色
            float startColor = element.Alpha;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { element.Alpha = color; }).OnRewind(() => { element.Alpha = startColor; }).OnComplete((duration) => { element.Alpha = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { element.Alpha = color; }).OnRewind(() => { element.Alpha = startColor; }).OnComplete((duration) => { element.Alpha = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { element.Alpha = color; }).OnRewind(() => { element.Alpha = startColor; }).OnComplete((duration) => { element.Alpha = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgress, time) => { element.Alpha = color; }).OnRewind(() => { element.Alpha = startColor; }).OnComplete((duration) => { element.Alpha = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Float(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgress, time) => { element.Alpha = color; element.element_AlphaSyncUpdate(); }).OnRewind(() => { element.Alpha = startColor; element.element_AlphaSyncUpdate(); }).OnComplete((duration) => { element.Alpha = endValue; element.element_AlphaSyncUpdate(); }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgress, time) => { element.Alpha = color; element.element_AlphaSyncUpdate(); }).OnRewind(() => { element.Alpha = startColor; element.element_AlphaSyncUpdate(); }).OnComplete((duration) => { element.Alpha = endValue; element.element_AlphaSyncUpdate(); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgress, time) => { element.Alpha = color; element.element_AlphaSyncUpdate(); }).OnRewind(() => { element.Alpha = startColor; element.element_AlphaSyncUpdate(); }).OnComplete((duration) => { element.Alpha = endValue; element.element_AlphaSyncUpdate(); }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgress, time) => { element.Alpha = color; element.element_AlphaSyncUpdate(); }).OnRewind(() => { element.Alpha = startColor; element.element_AlphaSyncUpdate(); }).OnComplete((duration) => { element.Alpha = endValue; element.element_AlphaSyncUpdate(); }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
