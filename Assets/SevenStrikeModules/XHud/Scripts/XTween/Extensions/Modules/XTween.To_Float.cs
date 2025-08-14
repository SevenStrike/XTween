namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前浮点数值到目标浮点数值的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="endValue">目标浮点数值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 true</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<float> getter, XTween_Setter<float> setter, float endValue, float duration, bool autokill = true)
        {
            float startValue = getter();

            if (Application.isPlaying)
            {
                // 使用对象池获取实例创建动画
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                // 初始化动画参数
                tweener.Initialize(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                //注册回调
                tweener.OnUpdate((value, linearProgress, time) =>
                {
                    setter(value);
                }).OnRewind(() =>
                {
                    setter(startValue);
                }).SetAutokill(autokill);

                return tweener;
            }
            else
            {
                //编辑器预览方式创建动画
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply)
                        .OnUpdate((value, linearProgress, time) =>
                        {
                            setter(value);
                        }).OnRewind(() =>
                        {
                            setter(startValue);
                        }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前浮点数值到目标浮点数值的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="endValue">目标浮点数值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 true</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<float> getter, XTween_Setter<float> setter, float endValue, float duration, bool autokill = true, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            float startValue = getter();

            if (Application.isPlaying)
            {
                // 使用对象池获取实例创建动画
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                // 初始化动画参数
                tweener.Initialize(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetFrom(fromval).SetAutokill(autokill);
                    }
                    else
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetFrom(fromval).SetAutokill(autokill);
                    }
                }
                // 从当前值开始
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                return tweener;
            }
            else
            {
                //编辑器预览方式创建动画
                XTween_Interface tweener;
                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();

                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetFrom(fromval).SetAutokill(false);
                    }
                    else// 使用缓动参数
                    {
                        tweener = new XTween_Specialized_Float(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetFrom(fromval).SetAutokill(false);
                    }
                }
                // 从当前值开始
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetAutokill(false);
                    }
                    else// 使用缓动参数
                    {
                        tweener = new XTween_Specialized_Float(startValue, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}
