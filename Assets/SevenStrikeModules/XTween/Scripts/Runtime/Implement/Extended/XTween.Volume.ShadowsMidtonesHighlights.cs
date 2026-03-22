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
        /// 创建一个从当前颜色平衡分级Shadows到目标颜色平衡分级Shadows的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级Shadows</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_Shadows_To(this ShadowsMidtonesHighlights smh, Vector4 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            Vector4 start = smh.shadows.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.shadows.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.shadows.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.shadows.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.shadows.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.shadows.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.shadows.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级Shadows到目标颜色平衡分级Shadows的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级Shadows</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_Shadows_To(this ShadowsMidtonesHighlights smh, Vector4 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector4> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            Vector4 start = smh.shadows.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
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
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadows.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级Midtones到目标颜色平衡分级Midtones的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级Midtones</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_Midtones_To(this ShadowsMidtonesHighlights smh, Vector4 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            Vector4 start = smh.midtones.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.midtones.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.midtones.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.midtones.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.midtones.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.midtones.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.midtones.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级Midtones到目标颜色平衡分级Midtones的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级Midtones</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_Midtones_To(this ShadowsMidtonesHighlights smh, Vector4 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector4> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            Vector4 start = smh.midtones.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
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
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.midtones.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级Highlights到目标颜色平衡分级Highlights的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级Highlights</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_Highlights_To(this ShadowsMidtonesHighlights smh, Vector4 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            Vector4 start = smh.highlights.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.highlights.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.highlights.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.highlights.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.highlights.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.highlights.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.highlights.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级Highlights到目标颜色平衡分级Highlights的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级Highlights</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_Highlights_To(this ShadowsMidtonesHighlights smh, Vector4 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector4> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            Vector4 start = smh.highlights.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
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
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlights.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的阴影限制起始值到目标颜色平衡分级ShadowMidtonesHighlights的阴影限制起始值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的阴影限制起始值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_ShadowLimits_Start_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.shadowsStart.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.shadowsStart.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.shadowsStart.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.shadowsStart.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.shadowsStart.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.shadowsStart.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.shadowsStart.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的阴影限制起始值到目标颜色平衡分级ShadowMidtonesHighlights的阴影限制起始值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的阴影限制起始值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_ShadowLimits_Start_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.shadowsStart.value;

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
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
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
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsStart.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的阴影限制结束值到目标颜色平衡分级ShadowMidtonesHighlights的阴影限制结束值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的阴影限制结束值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_ShadowLimits_End_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.shadowsEnd.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.shadowsEnd.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.shadowsEnd.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.shadowsEnd.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.shadowsEnd.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.shadowsEnd.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.shadowsEnd.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的阴影限制结束值到目标颜色平衡分级ShadowMidtonesHighlights的阴影限制结束值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的阴影限制结束值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_ShadowLimits_End_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.shadowsEnd.value;

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
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
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
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.shadowsEnd.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的高光限制起始值到目标颜色平衡分级ShadowMidtonesHighlights的高光限制起始值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的高光限制起始值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_HighlightLimits_Start_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.highlightsStart.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.highlightsStart.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.highlightsStart.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.highlightsStart.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.highlightsStart.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.highlightsStart.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.highlightsStart.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的高光限制起始值到目标颜色平衡分级ShadowMidtonesHighlights的高光限制起始值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的高光限制起始值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_HighlightLimits_Start_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.highlightsStart.value;

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
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
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
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsStart.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的高光限制结束值到目标颜色平衡分级ShadowMidtonesHighlights的高光限制结束值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的高光限制结束值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_HighlightLimits_End_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.highlightsEnd.value;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.highlightsEnd.value = val;
                })
                .OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.highlightsEnd.value = start;
                })
                .OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.highlightsEnd.value = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (smh == null)
                        return;
                    smh.highlightsEnd.value = val;
                }).OnRewind(() =>
                {
                    if (smh == null)
                        return;
                    if (rewind_set_startvalue)
                        smh.highlightsEnd.value = start;
                }).OnComplete((duration) =>
                {
                    if (smh == null)
                        return;
                    if (complete_set_endvalue)
                        smh.highlightsEnd.value = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前颜色平衡分级ShadowMidtonesHighlights的高光限制结束值到目标颜色平衡分级ShadowMidtonesHighlights的高光限制结束值的动画
        /// </summary>
        /// <param name="smh">目标 ShadowsMidtonesHighlights 组件</param>
        /// <param name="endValue">目标颜色平衡分级ShadowMidtonesHighlights的高光限制结束值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShadowsMidtonesHighlights_HighlightLimits_End_To(this ShadowsMidtonesHighlights smh, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (smh == null)
            {
                Debug.LogError("ShadowsMidtonesHighlights component is null!");
                return null;
            }

            float start = smh.highlightsEnd.value;

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
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
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
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = val;
                        }).OnRewind(() =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = start;
                        }).OnComplete((duration) =>
                        {
                            if (smh == null)
                                return;
                            smh.highlightsEnd.value = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
