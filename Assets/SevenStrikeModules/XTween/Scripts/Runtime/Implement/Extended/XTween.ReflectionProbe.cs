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
        /// 创建一个从当前反射探针强度到目标反射探针强度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_Intensity_To(this ReflectionProbe probe, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            float start = probe.intensity;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.intensity = val;
                })
                .OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.intensity = start;
                })
                .OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.intensity = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.intensity = val;
                }).OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.intensity = start;
                }).OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.intensity = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针强度到目标反射探针强度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_Intensity_To(this ReflectionProbe probe, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            float start = probe.intensity;

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
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
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
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.intensity = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针混合距离到目标反射探针混合距离的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针混合距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_BlendDistance_To(this ReflectionProbe probe, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            float start = probe.blendDistance;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.blendDistance = val;
                })
                .OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.blendDistance = start;
                })
                .OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.blendDistance = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.blendDistance = val;
                }).OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.blendDistance = start;
                }).OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.blendDistance = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针混合距离到目标反射探针混合距离的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针混合距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_BlendDistance_To(this ReflectionProbe probe, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            float start = probe.blendDistance;

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
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
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
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.blendDistance = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针尺寸到目标反射探针尺寸的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针尺寸</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_BoxSize_To(this ReflectionProbe probe, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            Vector3 start = probe.size;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.size = val;
                })
                .OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.size = start;
                })
                .OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.size = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.size = val;
                }).OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.size = start;
                }).OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.size = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针尺寸到目标反射探针尺寸的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针尺寸</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_BoxSize_To(this ReflectionProbe probe, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            Vector3 start = probe.size;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
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
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.size = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.size = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针偏移到目标反射探针偏移的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针偏移</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_BoxOffset_To(this ReflectionProbe probe, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            Vector3 start = probe.center;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.center = val;
                })
                .OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.center = start;
                })
                .OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.center = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (probe == null)
                        return;
                    probe.center = val;
                }).OnRewind(() =>
                {
                    if (probe == null)
                        return;
                    if (rewind_set_startvalue)
                        probe.center = start;
                }).OnComplete((duration) =>
                {
                    if (probe == null)
                        return;
                    if (complete_set_endvalue)
                        probe.center = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前反射探针偏移到目标反射探针偏移的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="probe">目标 ReflectionProbe 组件</param>
        /// <param name="endValue">目标反射探针偏移</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ReflectionProbe_BoxOffset_To(this ReflectionProbe probe, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (probe == null)
            {
                Debug.LogError("ReflectionProbe component is null!");
                return null;
            }

            Vector3 start = probe.center;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
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
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = val;
                        }).OnRewind(() =>
                        {
                            if (probe == null)
                                return;
                            probe.center = start;
                        }).OnComplete((duration) =>
                        {
                            if (probe == null)
                                return;
                            probe.center = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
