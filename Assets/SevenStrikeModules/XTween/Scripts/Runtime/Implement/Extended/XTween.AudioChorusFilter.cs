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
        /// 创建一个从当前合唱效果器干声值到目标合唱效果器干声值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器干声值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_DryMix_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.dryMix;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.dryMix = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.dryMix = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.dryMix = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.dryMix = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.dryMix = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.dryMix = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器干声值到目标合唱效果器干声值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器干声值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_DryMix_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.dryMix;

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
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.dryMix = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器第一个延迟副本的音量值到目标合唱效果器第一个延迟副本的音量值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器第一个延迟副本的音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_WetMix1_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.wetMix1;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.wetMix1 = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.wetMix1 = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.wetMix1 = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.wetMix1 = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.wetMix1 = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.wetMix1 = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器第一个延迟副本的音量值到目标合唱效果器第一个延迟副本的音量值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器第一个延迟副本的音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_WetMix1_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.wetMix1;

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
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix1 = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器第二个延迟副本的音量值到目标合唱效果器第二个延迟副本的音量值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器第二个延迟副本的音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_WetMix2_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.wetMix2;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.wetMix2 = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.wetMix2 = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.wetMix2 = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.wetMix2 = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.wetMix2 = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.wetMix2 = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器第二个延迟副本的音量值到目标合唱效果器第二个延迟副本的音量值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器第二个延迟副本的音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_WetMix2_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.wetMix2;

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
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix2 = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器第三个延迟副本的音量值到目标合唱效果器第三个延迟副本的音量值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器第三个延迟副本的音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_WetMix3_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.wetMix3;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.wetMix3 = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.wetMix3 = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.wetMix3 = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.wetMix3 = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.wetMix3 = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.wetMix3 = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器第三个延迟副本的音量值到目标合唱效果器第三个延迟副本的音量值的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器第三个延迟副本的音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_WetMix3_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.wetMix3;

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
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.wetMix3 = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器延迟时间到目标合唱效果器延迟时间的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器延迟时间</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_Delay_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.delay;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.delay = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.delay = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.delay = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.delay = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.delay = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.delay = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器延迟时间到目标合唱效果器延迟时间的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器延迟时间</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_Delay_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.delay;

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
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.delay = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器调制频率到目标合唱效果器调制频率的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器调制频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_Rate_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.rate;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.rate = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.rate = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.rate = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.rate = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.rate = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.rate = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器调制频率到目标合唱效果器调制频率的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器调制频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_Rate_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.rate;

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
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.rate = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器调制深度到目标合唱效果器调制深度的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器调制深度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_Depth_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.depth;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.depth = val;
                })
                .OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.depth = start;
                })
                .OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.depth = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (chorus == null)
                        return;
                    chorus.depth = val;
                }).OnRewind(() =>
                {
                    if (chorus == null)
                        return;
                    if (rewind_set_startvalue)
                        chorus.depth = start;
                }).OnComplete((duration) =>
                {
                    if (chorus == null)
                        return;
                    if (complete_set_endvalue)
                        chorus.depth = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前合唱效果器调制深度到目标合唱效果器调制深度的动画
        /// </summary>
        /// <param name="chorus">目标 AudioChorusFilter 组件</param>
        /// <param name="endValue">目标合唱效果器调制深度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioChorusFilter_Depth_To(this AudioChorusFilter chorus, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (chorus == null)
            {
                Debug.LogError("AudioChorusFilter component is null!");
                return null;
            }

            float start = chorus.depth;

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
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
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
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = val;
                        }).OnRewind(() =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = start;
                        }).OnComplete((duration) =>
                        {
                            if (chorus == null)
                                return;
                            chorus.depth = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
