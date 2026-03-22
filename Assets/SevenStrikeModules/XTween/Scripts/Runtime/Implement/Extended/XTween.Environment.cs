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
        /// 创建一个从当前环境明度到目标环境明度的动画
        /// </summary>
        /// <param name="endValue">目标环境明度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Intensity_To(float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            float start = RenderSettings.ambientIntensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientIntensity = val;
                })
                .OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientIntensity = start;
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientIntensity = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientIntensity = val;
                }).OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientIntensity = start;
                }).OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientIntensity = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境明度到目标环境明度的动画
        /// </summary>
        /// <param name="endValue">目标环境明度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Intensity_To(float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            float start = RenderSettings.ambientIntensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
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
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientIntensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientIntensity = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境色天空色到目标环境色天空色的动画
        /// </summary>
        /// <param name="endValue">目标环境色天空色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Color_Sky_To(Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            Color start = RenderSettings.ambientSkyColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientSkyColor = val;
                })
                .OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientSkyColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientSkyColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientSkyColor = val;
                }).OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientSkyColor = start;
                }).OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientSkyColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境色天空色到目标环境色天空色的动画
        /// </summary>
        /// <param name="endValue">目标环境色天空色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Color_Sky_To(Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            Color start = RenderSettings.ambientSkyColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
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
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientSkyColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientSkyColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientSkyColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境色赤道色到目标环境色赤道色的动画
        /// </summary>
        /// <param name="endValue">目标环境色赤道色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Color_Equator_To(Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            Color start = RenderSettings.ambientEquatorColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientEquatorColor = val;
                })
                .OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientEquatorColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientEquatorColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientEquatorColor = val;
                }).OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientEquatorColor = start;
                }).OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientEquatorColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境色赤道色到目标环境色赤道色的动画
        /// </summary>
        /// <param name="endValue">目标环境色赤道色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Color_Equator_To(Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            Color start = RenderSettings.ambientEquatorColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
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
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientEquatorColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientEquatorColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientEquatorColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境色地面色到目标环境色地面色的动画
        /// </summary>
        /// <param name="endValue">目标环境色地面色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Color_Ground_To(Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            Color start = RenderSettings.ambientGroundColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientGroundColor = val;
                })
                .OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientGroundColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientGroundColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.ambientGroundColor = val;
                }).OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.ambientGroundColor = start;
                }).OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.ambientGroundColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境色地面色到目标环境色地面色的动画
        /// </summary>
        /// <param name="endValue">目标环境色地面色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Color_Ground_To(Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            Color start = RenderSettings.ambientGroundColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
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
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.ambientGroundColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.ambientGroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.ambientGroundColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境雾气颜色到目标环境雾气颜色的动画
        /// </summary>
        /// <param name="endValue">目标环境雾气颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Fog_Color_To(Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            Color start = RenderSettings.fogColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.fogColor = val;
                })
                .OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.fogColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.fogColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.fogColor = val;
                }).OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.fogColor = start;
                }).OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.fogColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境雾气颜色到目标环境雾气颜色的动画
        /// </summary>
        /// <param name="endValue">目标环境雾气颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Fog_Color_To(Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            Color start = RenderSettings.fogColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Color fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
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
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogColor = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogColor = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境雾气浓度到目标环境雾气浓度的动画
        /// </summary>
        /// <param name="endValue">目标环境雾气浓度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Fog_Density_To(float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            float start = RenderSettings.fogDensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.fogDensity = val;
                })
                .OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.fogDensity = start;
                })
                .OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.fogDensity = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    RenderSettings.fogDensity = val;
                }).OnRewind(() =>
                {
                    if (rewind_set_startvalue)
                        RenderSettings.fogDensity = start;
                }).OnComplete((duration) =>
                {
                    if (complete_set_endvalue)
                        RenderSettings.fogDensity = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前环境雾气浓度到目标环境雾气浓度的动画
        /// </summary>
        /// <param name="endValue">目标环境雾气浓度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Ambient_Fog_Density_To(float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            float start = RenderSettings.fogDensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    float fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
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
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            RenderSettings.fogDensity = val;
                        }).OnRewind(() =>
                        {
                            RenderSettings.fogDensity = start;
                        }).OnComplete((duration) =>
                        {
                            RenderSettings.fogDensity = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
