namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前文本到目标文本的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="extended">是否扩展当前文本如果为 true，则在当前文本的基础上追加目标文本；如果为 false，则直接替换为目标文本</param>
        /// <param name="endValue">目标文本内容</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<string> getter, XTween_Setter<string> setter, bool extended, string endValue, float duration, bool autokill = false, float blinkInterval = 0.5f, string cursor = "")
        {
            string startText = getter();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_String>();

                if (extended)
                {
                    endValue = startText + endValue;
                }

                tweener.Initialize(startText, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((currentText, linearProgress, time) =>
                {
                    bool showCursor = false;
                    if (!string.IsNullOrEmpty(cursor))
                    {
                        // 直接使用Time.time计算，每blinkInterval秒切换一次
                        float t = Time.time / blinkInterval;
                        showCursor = Mathf.FloorToInt(t) % 2 == 0;
                    }

                    if (extended)
                    {
                        setter(startText + currentText + (showCursor ? cursor : ""));
                    }
                    else
                    {
                        setter(currentText + (showCursor ? cursor : ""));
                    }
                })
                .OnRewind(() =>
                {
                    setter(startText);
                })
                .OnComplete((duration) =>
                {
                    if (extended)
                    {
                        setter(startText + endValue);
                    }
                    else
                    {
                        setter(endValue);
                    }
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }

                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        })
                        .OnRewind(() =>
                        {
                            setter(startText);
                        })
                        .OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前文本到目标文本的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="extended">是否扩展当前文本如果为 true，则在当前文本的基础上追加目标文本；如果为 false，则直接替换为目标文本</param>
        /// <param name="endValue">目标文本内容</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<string> getter, XTween_Setter<string> setter, bool extended, float blinkInterval, string cursor, string endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<string> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            string startText = getter();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_String>();

                if (extended)
                {
                    endValue = startText + endValue;
                }

                tweener.Initialize(startText, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    string fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        bool showCursor = false;
                        if (!string.IsNullOrEmpty(cursor))
                        {
                            // 直接使用Time.time计算，每blinkInterval秒切换一次
                            float t = Time.time / blinkInterval;
                            showCursor = Mathf.FloorToInt(t) % 2 == 0;
                        }
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {

                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                // 从当前值开始
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetEase(easeMode).SetAutokill(autokill);
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
                    string fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() => { setter(startText); }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                // 从当前值开始
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            bool showCursor = false;
                            if (!string.IsNullOrEmpty(cursor))
                            {
                                // 直接使用Time.time计算，每blinkInterval秒切换一次
                                float t = Time.time / blinkInterval;
                                showCursor = Mathf.FloorToInt(t) % 2 == 0;
                            }
                            if (extended)
                            {
                                setter(startText + currentText + (showCursor ? cursor : ""));
                            }
                            else
                            {
                                setter(currentText + (showCursor ? cursor : ""));
                            }
                        }).OnRewind(() =>
                        {
                            setter(startText);
                        }).OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                setter(startText + endValue);
                            }
                            else
                            {
                                setter(endValue);
                            }
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}
