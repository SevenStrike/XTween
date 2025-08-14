namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前二维向量值到目标二维向量值的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="endValue">目标二维向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 true</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<Vector2> getter, XTween_Setter<Vector2> setter, Vector2 endValue, float duration, bool autokill = true)
        {
            Vector2 startValue = getter();

            if (Application.isPlaying)
            {
                // 使用对象池获取实例创建动画
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                // 初始化动画参数
                tweener.Initialize(startValue, endValue, duration * XTween_Dashboard.DurationMultiply);

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
                tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply)
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
        /// 创建一个从当前二维向量值到目标二维向量值的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="endValue">目标二维向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 true</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<Vector2> getter, XTween_Setter<Vector2> setter, Vector2 endValue, float duration, bool autokill = true, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector2> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            Vector2 startValue = getter();

            if (Application.isPlaying)
            {
                // 使用对象池获取实例创建动画
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                // 初始化动画参数
                tweener.Initialize(startValue, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
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
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}