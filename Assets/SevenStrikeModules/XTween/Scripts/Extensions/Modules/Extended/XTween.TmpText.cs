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
    using TMPro;
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前字体大小到目标大小的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标大小</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontSize_To(this TextMeshProUGUI text, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            float startSize = text.fontSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startSize, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.fontSize = size;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.fontSize = startSize;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.fontSize = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.fontSize = size;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.fontSize = startSize;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.fontSize = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前字体大小到目标大小的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标大小</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontSize_To(this TextMeshProUGUI text, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            float startSize = text.fontSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startSize, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
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
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startSize, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = size;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = startSize;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.fontSize = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前行间距到目标行间距的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标行间距值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontLineHeight_To(this TextMeshProUGUI text, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            float startSpaceLineheight = text.lineSpacing;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((lineheight, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.lineSpacing = lineheight;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.lineSpacing = startSpaceLineheight;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.lineSpacing = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.lineSpacing = lineheight;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.lineSpacing = startSpaceLineheight;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.lineSpacing = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前行间距到目标行间距的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标行间距值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontLineHeight_To(this TextMeshProUGUI text, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            float startSpaceLineheight = text.lineSpacing;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
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
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startSpaceLineheight, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((lineheight, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = lineheight;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = startSpaceLineheight;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.lineSpacing = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前字符间距到目标字符间距的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标字符间距值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontCharacter_To(this TextMeshProUGUI text, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            float startSpaceCharacter = text.characterSpacing;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((character, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.characterSpacing = character;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.characterSpacing = startSpaceCharacter;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.characterSpacing = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((character, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.characterSpacing = character;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.characterSpacing = startSpaceCharacter;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.characterSpacing = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前字符间距到目标字符间距的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标字符间距值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontCharacter_To(this TextMeshProUGUI text, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            float startSpaceCharacter = text.characterSpacing;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
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
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(startSpaceCharacter, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((character, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = character;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = startSpaceCharacter;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.characterSpacing = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前外边距到目标外边距的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标外边距值（四维向量_Vector4 类型，分别表示左、右、上、下的边距）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontMargin_To(this TextMeshProUGUI text, Vector4 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            Vector4 startMargin = text.margin;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((margin, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.margin = margin;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.margin = startMargin;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.margin = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector4(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((margin, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.margin = margin;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.margin = startMargin;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.margin = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前外边距到目标外边距的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标外边距值（四维向量_Vector4 类型，分别表示左、右、上、下的边距）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontMargin_To(this TextMeshProUGUI text, Vector4 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector4> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            Vector4 startMargin = text.margin;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
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
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(startMargin, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((margin, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.margin = margin;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.margin = startMargin;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.margin = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前颜色到目标颜色的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标颜色值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontColor_To(this TextMeshProUGUI text, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
                return null;
            }

            Color startColor = text.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(startColor, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((color, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.color = color;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.color = startColor;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.color = endValue;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) =>
                {
                    if (text == null)
                        return;
                    text.color = color;
                })
                .OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.color = startColor;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                        text.color = endValue;
                })
                .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前颜色到目标颜色的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="endValue">目标颜色值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontColor_To(this TextMeshProUGUI text, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
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
                        tweener.OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
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
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(startColor, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((color, linearProgres, time) =>
                        {
                            if (text == null)
                                return;
                            text.color = color;
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.color = startColor;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            text.color = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前文本到目标文本的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="extended">是否扩展当前文本如果为 true，则在当前文本的基础上追加目标文本；如果为 false，则直接替换为目标文本</param>
        /// <param name="endValue">目标文本内容</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontText_To(this TextMeshProUGUI text, bool extended, string endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
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
                    if (text == null)
                        return;
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
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.text = startText;
                })
                .OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        if (extended)
                        {
                            text.text = startText + endValue;
                        }
                        else
                        {
                            text.text = endValue;
                        }
                    }
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                {
                    if (text == null)
                        return;
                    if (extended)
                    {
                        text.text = startText + currentText;
                    }
                    else
                    {
                        text.text = currentText;
                    }
                }).OnRewind(() =>
                {
                    if (text == null)
                        return;
                    if (rewind_set_startvalue)
                        text.text = startText;
                }).OnComplete((duration) =>
                {
                    if (text == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        if (extended)
                        {
                            text.text = startText + endValue;
                        }
                        else
                        {
                            text.text = endValue;
                        }
                    }
                }).SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个TextMeshProUGUI从当前文本到目标文本的动画过渡
        /// </summary>
        /// <param name="text">目标 TextMeshProUGUI 组件</param>
        /// <param name="extended">是否扩展当前文本如果为 true，则在当前文本的基础上追加目标文本；如果为 false，则直接替换为目标文本</param>
        /// <param name="endValue">目标文本内容</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_FontText_To(this TextMeshProUGUI text, bool extended, string endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<string> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshProUGUI component is null!");
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
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
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
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_String(startText, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((currentText, linearProgress, time) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + currentText; } else { text.text = currentText; }
                        }).OnRewind(() =>
                        {
                            if (text == null)
                                return;
                            text.text = startText;
                        }).OnComplete((duration) =>
                        {
                            if (text == null)
                                return;
                            if (extended) { text.text = startText + endValue; } else { text.text = endValue; }
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}