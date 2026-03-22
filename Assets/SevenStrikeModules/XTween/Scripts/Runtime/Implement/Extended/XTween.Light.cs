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
        /// 创建一个从当前灯光强度到目标灯光强度的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Intensity_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.intensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.intensity = val;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.intensity = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.intensity = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.intensity = val;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.intensity = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.intensity = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光强度到目标灯光强度的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Intensity_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.intensity;

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
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
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
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.intensity = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光强度倍增到目标灯光强度倍增的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光强度倍增</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_IntensityMultiplier_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.bounceIntensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.bounceIntensity = val;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.bounceIntensity = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.bounceIntensity = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.bounceIntensity = val;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.bounceIntensity = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.bounceIntensity = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光强度倍增到目标灯光强度倍增的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光强度倍增</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_IntensityMultiplier_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.bounceIntensity;

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
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
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
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.bounceIntensity = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光阴影强度到目标灯光阴影强度的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光阴影强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_ShadowStrength_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.shadowStrength;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.shadowStrength = val;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.shadowStrength = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.shadowStrength = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.shadowStrength = val;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.shadowStrength = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.shadowStrength = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光阴影强度到目标灯光阴影强度的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光阴影强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_ShadowStrength_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.shadowStrength;

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
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
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
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowStrength = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光范围到目标灯光范围的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光范围</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Range_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.range;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.range = val;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.range = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.range = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.range = val;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.range = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.range = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光范围到目标灯光范围的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光范围</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Range_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.range;

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
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
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
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.range = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.range = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.range = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光颜色到目标灯光颜色的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Color_To(this Light light, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            Color start = light.color;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((System.Action<Color, float, float>)((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.color = val;
                }))
                .OnRewind((System.Action)(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.color = start;
                }))
                .OnComplete((System.Action<float>)((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.color = endValue;
                }))
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((System.Action<Color, float, float>)((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.color = val;
                })).OnRewind((System.Action)(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.color = start;
                })).OnComplete((System.Action<float>)((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.color = endValue;
                })).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光颜色到目标灯光颜色的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Color_To(this Light light, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            Color start = light.color;

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
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
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
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.color = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.color = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.color = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光色温到目标灯光色温的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光色温</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Temperature_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.colorTemperature;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.colorTemperature = val;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.colorTemperature = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.colorTemperature = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.colorTemperature = val;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.colorTemperature = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.colorTemperature = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光色温到目标灯光色温的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光色温</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_Temperature_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.colorTemperature;

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
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
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
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.colorTemperature = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光内衰减角范围到目标灯光内衰减角范围的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光内衰减角范围</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_SpotAngle_Inner_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.innerSpotAngle;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.innerSpotAngle = size;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.innerSpotAngle = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.innerSpotAngle = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.innerSpotAngle = size;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.innerSpotAngle = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.innerSpotAngle = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光内衰减角范围到目标灯光内衰减角范围的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光内衰减角范围</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_SpotAngle_Inner_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.innerSpotAngle;

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
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
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
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.innerSpotAngle = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光外衰减角范围到目标灯光外衰减角范围的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光外衰减角范围</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_SpotAngle_Outer_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.spotAngle;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.spotAngle = size;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.spotAngle = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.spotAngle = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.spotAngle = size;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.spotAngle = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.spotAngle = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光外衰减角范围到目标灯光外衰减角范围的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光外衰减角范围</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_SpotAngle_Outer_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.spotAngle;

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
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
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
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.spotAngle = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光阴影开始渲染的最小距离到目标灯光阴影开始渲染的最小距离的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光阴影开始渲染的最小距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_ShadowNearPlane_To(this Light light, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.shadowNearPlane;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.shadowNearPlane = size;
                })
                .OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.shadowNearPlane = start;
                })
                .OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.shadowNearPlane = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (light == null)
                        return;
                    light.shadowNearPlane = size;
                }).OnRewind(() =>
                {
                    if (light == null)
                        return;
                    if (rewind_set_startvalue)
                        light.shadowNearPlane = start;
                }).OnComplete((duration) =>
                {
                    if (light == null)
                        return;
                    if (complete_set_endvalue)
                        light.shadowNearPlane = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前灯光阴影开始渲染的最小距离到目标灯光阴影开始渲染的最小距离的动画
        /// </summary>
        /// <param name="light">目标 Light 组件</param>
        /// <param name="endValue">目标灯光阴影开始渲染的最小距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Light_ShadowNearPlane_To(this Light light, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (light == null)
            {
                Debug.LogError("Light component is null!");
                return null;
            }

            float start = light.shadowNearPlane;

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
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
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
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = val;
                        }).OnRewind(() =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (light == null)
                                return;
                            light.shadowNearPlane = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
