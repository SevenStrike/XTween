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
        /// 创建一个从当前风力学半径到目标风力学半径的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学半径</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_Radius_To(this WindZone wzone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.radius;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.radius = val;
                })
                .OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.radius = start;
                })
                .OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.radius = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.radius = val;
                }).OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.radius = start;
                }).OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.radius = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学半径到目标风力学半径的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学半径</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_Radius_To(this WindZone wzone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.radius;

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
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
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
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.radius = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学主风力到目标风力学主风力的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学主风力</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_Main_To(this WindZone wzone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windMain;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windMain = val;
                })
                .OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windMain = start;
                })
                .OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windMain = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windMain = val;
                }).OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windMain = start;
                }).OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windMain = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学主风力到目标风力学主风力的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学主风力</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_Main_To(this WindZone wzone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windMain;

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
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
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
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windMain = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学湍流强度到目标风力学湍流强度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学湍流强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_Turbulence_To(this WindZone wzone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windTurbulence;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windTurbulence = val;
                })
                .OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windTurbulence = start;
                })
                .OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windTurbulence = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windTurbulence = val;
                }).OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windTurbulence = start;
                }).OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windTurbulence = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学湍流强度到目标风力学湍流强度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学湍流强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_Turbulence_To(this WindZone wzone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windTurbulence;

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
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
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
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windTurbulence = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学脉冲强度到目标风力学脉冲强度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学脉冲强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_PulseMagnitude_To(this WindZone wzone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windPulseMagnitude;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windPulseMagnitude = val;
                })
                .OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windPulseMagnitude = start;
                })
                .OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windPulseMagnitude = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windPulseMagnitude = val;
                }).OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windPulseMagnitude = start;
                }).OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windPulseMagnitude = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学脉冲强度到目标风力学脉冲强度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学脉冲强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_PulseMagnitude_To(this WindZone wzone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windPulseMagnitude;

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
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
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
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseMagnitude = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学脉冲频率到目标风力学脉冲频率的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学脉冲频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_PulseFrequency_To(this WindZone wzone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windPulseFrequency;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windPulseFrequency = val;
                })
                .OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windPulseFrequency = start;
                })
                .OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windPulseFrequency = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (wzone == null)
                        return;
                    wzone.windPulseFrequency = val;
                }).OnRewind(() =>
                {
                    if (wzone == null)
                        return;
                    if (rewind_set_startvalue)
                        wzone.windPulseFrequency = start;
                }).OnComplete((duration) =>
                {
                    if (wzone == null)
                        return;
                    if (complete_set_endvalue)
                        wzone.windPulseFrequency = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前风力学脉冲频率到目标风力学脉冲频率的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="wzone">目标 WindZone 组件</param>
        /// <param name="endValue">目标风力学脉冲频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_WindZone_PulseFrequency_To(this WindZone wzone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (wzone == null)
            {
                Debug.LogError("WindZone component is null!");
                return null;
            }

            float start = wzone.windPulseFrequency;

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
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
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
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = val;
                        }).OnRewind(() =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = start;
                        }).OnComplete((duration) =>
                        {
                            if (wzone == null)
                                return;
                            wzone.windPulseFrequency = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
