/*
 * ============================================================================
 * ⚠️ 版权声明（禁止删除、禁止修改、衍生作品必须保留此注释）⚠️
 * ============================================================================
 * 版权声明 Copyright (C) 2025-Present Nanjing SevenStrike Media Co., Ltd.
 * 中文名称：南京塞维斯传媒有限公司
 * 英文名称：SevenStrikeMedia
 * 项目作者：徐寅智
 * 项目名称：XTween - Unity 高性能动画架构插件
 * 项目启动：2025年8月
 * 官方网站：http://sevenstrike.com/
 * 授权协议：GNU Affero General Public License Version 3 (AGPL 3.0)
 * 协议说明：
 * 1. 你可以自由使用、修改、分发本插件的源代码，但必须保留此版权注释
 * 2. 基于本插件修改后的衍生作品，必须同样遵循 AGPL 3.0 授权协议
 * 3. 若将本插件用于网络服务（如云端Unity编辑器、在线动效生成工具），必须公开修改后的完整源代码
 * 4. 完整协议文本可查阅：https://www.gnu.org/licenses/agpl-3.0.html
 * ============================================================================
 * 违反本注释保留要求，将违反 AGPL 3.0 授权协议，需承担相应法律责任
 */
namespace SevenStrikeModules.XTween
{
    using UnityEngine;
    using UnityEngine.UI;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个Text从当前字体大小到目标大小的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="endValue">目标大小</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontSize_To(this Text text, int endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
                return null;
            }

            int startSize = (int)text.fontSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(startSize, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    text.fontSize = size;
                })
                .OnRewind(() =>
                {
                    text.fontSize = startSize;
                })
                .OnComplete((duration) =>
                {
                    text.fontSize = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((size, linearProgres, time) =>
                        {
                            text.fontSize = size;
                        })
                .OnRewind(() =>
                {
                    text.fontSize = startSize;
                })
                .OnComplete((duration) =>
                {
                    text.fontSize = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Text从当前字体大小到目标大小的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="endValue">目标大小</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontSize_To(this Text text, int endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<int> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
                return null;
            }

            int startSize = (int)text.fontSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(startSize, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    int fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) => { text.fontSize = size; }).OnRewind(() => { text.fontSize = startSize; }).OnComplete((duration) => { text.fontSize = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Text从当前行高到目标行高的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="endValue">目标行高</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontLineHeight_To(this Text text, float endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
                return null;
            }

            float startLineheight = text.lineSpacing;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((lineheight, linearProgres, time) =>
                {
                    text.lineSpacing = lineheight;
                })
                .OnRewind(() =>
                {
                    text.lineSpacing = startLineheight;
                })
                .OnComplete((duration) =>
                {
                    text.lineSpacing = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((lineheight, linearProgres, time) =>
                        {
                            text.lineSpacing = lineheight;
                        })
                .OnRewind(() =>
                {
                    text.lineSpacing = startLineheight;
                })
                .OnComplete((duration) =>
                {
                    text.lineSpacing = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Text从当前行高到目标行高的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="endValue">目标行高</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontLineHeight_To(this Text text, float endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<float> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
                return null;
            }

            float startLineheight = text.lineSpacing;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) => { text.lineSpacing = lineheight; }).OnRewind(() => { text.lineSpacing = startLineheight; }).OnComplete((duration) => { text.lineSpacing = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Text从当前颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontColor_To(this Text text, Color endValue, float duration, bool autokill = false)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
                return null;
            }

            Color startColor = text.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((color, linearProgres, time) =>
                {
                    text.color = color;
                })
                .OnRewind(() =>
                {
                    text.color = startColor;
                })
                .OnComplete((duration) =>
                {
                    text.color = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((color, linearProgres, time) =>
                        {
                            text.color = color;
                        })
                .OnRewind(() =>
                {
                    text.color = startColor;
                })
                .OnComplete((duration) =>
                {
                    text.color = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Text从当前颜色到目标颜色的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="endValue">目标颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontColor_To(this Text text, Color endValue, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Color> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
                return null;
            }

            Color startColor = text.color;

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
                        tweener.OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) => { text.color = color; }).OnRewind(() => { text.color = startColor; }).OnComplete((duration) => { text.color = endValue; }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个Text从当前文本到目标文本的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="extended">是否扩展文本</param>
        /// <param name="endValue">目标文本</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontText_To(this Text text, bool extended, string cursor, string endValue, float duration, bool autokill = false, float blinkInterval = 0.5f)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
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
                        text.text = startText + currentText + (showCursor ? cursor : "");
                    }
                    else
                    {
                        text.text = currentText + (showCursor ? cursor : "");
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
                        text.text = startText + currentText + (showCursor ? cursor : "");
                    }
                    else
                    {
                        text.text = currentText + (showCursor ? cursor : "");
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
        /// 创建一个Text从当前文本到目标文本的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 Text 组件</param>
        /// <param name="extended">是否扩展文本</param>
        /// <param name="endValue">目标文本</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontText_To(this Text text, bool extended, string cursor, string endValue, float duration, bool autokill = false, float blinkInterval = 0.5f, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<string> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (text == null)
            {
                Debug.LogError("Text component is null!");
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

                tweener.Initialize(startText, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    string fromval = fromvalue();
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
                        .OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } })
                        .SetFrom(fromval)
                        .SetEase(curve)
                        .SetAutokill(autokill);
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
                        .OnComplete((duration) => { if (extended) { text.text = startText + endValue; } else { text.text = endValue; } })
                        .SetFrom(fromval)
                        .SetEase(easeMode)
                        .SetAutokill(autokill);
                    }
                }
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
                        .OnComplete((duration) =>
                        {
                            if (extended) { text.text = startText + endValue; }
                            else
                            {
                                text.text = endValue;
                            }
                        })
                        .SetEase(curve)
                        .SetAutokill(autokill);
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
                        .OnComplete((duration) =>
                        {
                            if (extended) { text.text = startText + endValue; }
                            else
                            {
                                text.text = endValue;
                            }
                        })
                        .SetEase(easeMode)
                        .SetAutokill(autokill);
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
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
                        .SetFrom(fromval)
                        .SetEase(curve)
                        .SetAutokill(false);
                    }
                    else
                    {
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
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
                        .SetFrom(fromval)
                        .SetEase(easeMode)
                        .SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
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
                        .SetEase(curve)
                        .SetAutokill(false);
                    }
                    else
                    {
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
                                text.text = startText + currentText + (showCursor ? cursor : "");
                            }
                            else
                            {
                                text.text = currentText + (showCursor ? cursor : "");
                            }
                        })
                        .OnRewind(() => { text.text = startText; })
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
                        .SetEase(easeMode)
                        .SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
