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
        public static XTween_Interface To(XTween_Getter<string> getter, XTween_Setter<string> setter, bool extended, float blinkInterval, string cursor, string endValue, float duration, bool autokill = true, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
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
                    if (rewind_set_startvalue)
                        setter(startText);
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                    {
                        if (extended)
                        {
                            setter(startText + endValue);
                        }
                        else
                        {
                            setter(endValue);
                        }
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
                            if (rewind_set_startvalue)
                                setter(startText);
                        })
                        .OnComplete((duration) =>
                        {
                            if (complete_set_endvalue)
                            {
                                if (extended)
                                {
                                    setter(startText + endValue);
                                }
                                else
                                {
                                    setter(endValue);
                                }
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
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<string> getter, XTween_Setter<string> setter, bool extended, float blinkInterval, string cursor, string endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<string> fromvalue, bool useCurve, AnimationCurve curve)
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
