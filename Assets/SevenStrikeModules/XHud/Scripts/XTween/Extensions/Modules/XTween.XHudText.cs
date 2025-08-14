namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个Hud_Text从当前字体大小到目标大小的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="endValue">目标大小</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontSize_To(this Hud_Text text, int endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            int startSize = (int)text.TextStyleInfo.Size;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    text.TextStyleInfo.Size = size;
                })
                .OnRewind(() =>
                {
                    text.TextStyleInfo.Size = startSize;
                })
                .OnComplete((duration) =>
                {
                    text.TextStyleInfo.Size = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply)
                        .OnUpdate((size, linearProgres, time) =>
                        {
                            text.TextStyleInfo.Size = size;
                        })
                .OnRewind(() =>
                {
                    text.TextStyleInfo.Size = startSize;
                })
                .OnComplete((duration) =>
                {
                    text.TextStyleInfo.Size = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前字体大小到目标大小的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="endValue">目标大小</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontSize_To(this Hud_Text text, int endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<int> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            int startSize = (int)text.TextStyleInfo.Size;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    int fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                    int fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((size, linearProgres, time) => { text.TextStyleInfo.Size = size; }).OnRewind(() => { text.TextStyleInfo.Size = startSize; }).OnComplete((duration) => { text.TextStyleInfo.Size = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前行高到目标行高的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="endValue">目标行高</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontLineHeight_To(this Hud_Text text, float endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            float startLineheight = text.TextStyleInfo.LineHeight;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                tweener.OnUpdate((lineheight, linearProgres, time) =>
                {
                    text.TextStyleInfo.LineHeight = lineheight;
                })
                .OnRewind(() =>
                {
                    text.TextStyleInfo.LineHeight = startLineheight;
                })
                .OnComplete((duration) =>
                {
                    text.TextStyleInfo.LineHeight = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply)
                        .OnUpdate((lineheight, linearProgres, time) =>
                        {
                            text.TextStyleInfo.LineHeight = lineheight;
                        })
                .OnRewind(() =>
                {
                    text.TextStyleInfo.LineHeight = startLineheight;
                })
                .OnComplete((duration) =>
                {
                    text.TextStyleInfo.LineHeight = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前行高到目标行高的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="endValue">目标行高</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontLineHeight_To(this Hud_Text text, float endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            float startLineheight = text.TextStyleInfo.LineHeight;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.TextStyleInfo.LineHeight = lineheight; }).OnRewind(() => { text.TextStyleInfo.LineHeight = startLineheight; }).OnComplete((duration) => { text.TextStyleInfo.LineHeight = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontColor_To(this Hud_Text text, Color endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            Color startColor = text.TextStyleInfo.FontColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                tweener.OnUpdate((color, linearProgres, time) =>
                {
                    text.TextStyleInfo.FontColor = color;
                })
                .OnRewind(() =>
                {
                    text.TextStyleInfo.FontColor = startColor;
                })
                .OnComplete((duration) =>
                {
                    text.TextStyleInfo.FontColor = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply)
                        .OnUpdate((color, linearProgres, time) =>
                        {
                            text.TextStyleInfo.FontColor = color;
                        })
                .OnRewind(() =>
                {
                    text.TextStyleInfo.FontColor = startColor;
                })
                .OnComplete((duration) =>
                {
                    text.TextStyleInfo.FontColor = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontColor_To(this Hud_Text text, Color endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Color> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            Color startColor = text.TextStyleInfo.FontColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((color, linearProgres, time) => { text.TextStyleInfo.FontColor = color; }).OnRewind(() => { text.TextStyleInfo.FontColor = startColor; }).OnComplete((duration) => { text.TextStyleInfo.FontColor = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前文本到目标文本的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="extended">是否扩展文本</param>
        /// <param name="endValue">目标文本</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontText_To(this Hud_Text text, bool extended, string endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            string startText = text.text;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_String>();

                if (extended)
                {
                    endValue = startText + endValue;
                }

                tweener.Initialize(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                tweener.OnUpdate((currentText, linearProgress, time) =>
                {
                    if (extended)
                    {
                        text.text = startText + currentText;
                    }
                    else
                    {
                        text.text = currentText;
                    }
                })
                .OnRewind(() =>
                {
                    text.text = startText;
                })
                .OnComplete((duration) =>
                {
                    if (extended)
                    {
                        text.text = startText + endValue;
                    }
                    else
                    {
                        text.text = endValue;
                    }
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply)
                        .OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (extended)
                            {
                                text.text = startText + currentText;
                            }
                            else
                            {
                                text.text = currentText;
                            }
                        })
                        .OnRewind(() =>
                        {
                            text.text = startText;
                        })
                        .OnComplete((duration) =>
                        {
                            if (extended)
                            {
                                text.text = startText + endValue;
                            }
                            else
                            {
                                text.text = endValue;
                            }
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Hud_Text从当前文本到目标文本的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Hud_Text 组件</param>
        /// <param name="extended">是否扩展文本</param>
        /// <param name="endValue">目标文本</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontText_To(this Hud_Text text, bool extended, string endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<string> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Hud_Text component is null!");
                return null;
            }

            string startText = text.text;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_String>();

                if (extended)
                {
                    endValue = startText + endValue;
                }

                tweener.Initialize(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    string fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.HudManagerGet().DurationMultiply).OnUpdate((currentText, linearProgress, time) => { if (extended) { text.text = startText + currentText; } else { text.text = currentText; } }).OnRewind(() => { text.text = startText; }).OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
