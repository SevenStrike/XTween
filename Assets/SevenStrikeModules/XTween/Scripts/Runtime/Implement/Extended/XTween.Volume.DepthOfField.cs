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
    using UnityEngine.Rendering.Universal;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前景深起始值到目标景深起始值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深起始值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Gaussian_Start_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.gaussianStart.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.gaussianStart.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.gaussianStart.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.gaussianStart.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.gaussianStart.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.gaussianStart.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.gaussianStart.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深起始值到目标景深起始值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深起始值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Gaussian_Start_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.gaussianStart.value;

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
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianStart.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深结束值到目标景深结束值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深结束值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Gaussian_End_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.gaussianEnd.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.gaussianEnd.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.gaussianEnd.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.gaussianEnd.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.gaussianEnd.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.gaussianEnd.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.gaussianEnd.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深结束值到目标景深结束值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深结束值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Gaussian_End_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.gaussianEnd.value;

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
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianEnd.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深最大半径值到目标景深最大半径值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深最大半径值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Gaussian_MaxRadius_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.gaussianMaxRadius.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.gaussianMaxRadius.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.gaussianMaxRadius.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.gaussianMaxRadius.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.gaussianMaxRadius.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.gaussianMaxRadius.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.gaussianMaxRadius.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深最大半径值到目标景深最大半径值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深最大半径值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Gaussian_MaxRadius_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.gaussianMaxRadius.value;

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
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.gaussianMaxRadius.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深焦点距离值到目标景深焦点距离值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深焦点距离值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_FocusDistance_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.focusDistance.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.focusDistance.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.focusDistance.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.focusDistance.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.focusDistance.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.focusDistance.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.focusDistance.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深焦点距离值到目标景深焦点距离值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深焦点距离值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_FocusDistance_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.focusDistance.value;

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
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focusDistance.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深焦距值到目标景深焦距值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深焦距值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_FocalLength_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.focalLength.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.focalLength.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.focalLength.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.focalLength.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.focalLength.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.focalLength.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.focalLength.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深焦距值到目标景深焦距值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深焦距值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_FocalLength_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.focalLength.value;

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
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.focalLength.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深光圈值到目标景深光圈值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深光圈值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_Aperture_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.aperture.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.aperture.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.aperture.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.aperture.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.aperture.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.aperture.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.aperture.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深光圈值到目标景深光圈值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深光圈值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_Aperture_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.aperture.value;

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
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.aperture.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深光圈叶片曲率值到目标景深光圈叶片曲率值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深光圈叶片曲率值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_BladeCurvature_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.bladeCurvature.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.bladeCurvature.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.bladeCurvature.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.bladeCurvature.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.bladeCurvature.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.bladeCurvature.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.bladeCurvature.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深光圈叶片曲率值到目标景深光圈叶片曲率值的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深光圈叶片曲率值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_BladeCurvature_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.bladeCurvature.value;

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
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeCurvature.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深光圈叶片旋转角度到目标景深光圈叶片旋转角度的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深光圈叶片旋转角度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_BladeRotation_To(this DepthOfField dof, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.bladeRotation.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.bladeRotation.value = val;
                })
                .OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.bladeRotation.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.bladeRotation.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (dof == null)
                        return;
                    dof.bladeRotation.value = val;
                }).OnRewind(() =>
                {
                    if (dof == null)
                        return;
                    if (rewind_set_startvalue)
                        dof.bladeRotation.value = start;
                }).OnComplete((duration) =>
                {
                    if (dof == null)
                        return;
                    if (complete_set_endvalue)
                        dof.bladeRotation.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前景深光圈叶片旋转角度到目标景深光圈叶片旋转角度的动画
        /// </summary>
        /// <param name="dof">目标 DepthOfField 组件</param>
        /// <param name="endValue">目标景深光圈叶片旋转角度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Dof_Bokeh_BladeRotation_To(this DepthOfField dof, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (dof == null)
            {
                Debug.LogError("DepthOfField component is null!");
                return null;
            }

            float start = dof.bladeRotation.value;

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
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
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
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = val;
                        }).OnRewind(() =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (dof == null)
                                return;
                            dof.bladeRotation.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
