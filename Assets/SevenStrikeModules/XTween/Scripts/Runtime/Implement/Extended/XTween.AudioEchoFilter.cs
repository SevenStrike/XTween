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
        /// 创建一个从当前回声效果器延迟时间到目标回声效果器延迟时间的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器延迟时间</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_Delay_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.delay;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.delay = val;
                })
                .OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.delay = start;
                })
                .OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.delay = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.delay = val;
                }).OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.delay = start;
                }).OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.delay = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器延迟时间到目标回声效果器延迟时间的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器延迟时间</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_Delay_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.delay;

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
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
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
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.delay = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器回声衰减比例到目标回声效果器回声衰减比例的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器回声衰减比例</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_DecayRatio_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.decayRatio;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.decayRatio = val;
                })
                .OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.decayRatio = start;
                })
                .OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.decayRatio = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.decayRatio = val;
                }).OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.decayRatio = start;
                }).OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.decayRatio = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器回声衰减比例到目标回声效果器回声衰减比例的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器回声衰减比例</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_DecayRatio_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.decayRatio;

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
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
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
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.decayRatio = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器效果声（回声）的音量比例到目标回声效果器效果声（回声）的音量比例的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器效果声（回声）的音量比例</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_DryMix_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.dryMix;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.dryMix = val;
                })
                .OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.dryMix = start;
                })
                .OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.dryMix = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.dryMix = val;
                }).OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.dryMix = start;
                }).OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.dryMix = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器效果声（回声）的音量比例到目标回声效果器效果声（回声）的音量比例的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器效果声（回声）的音量比例</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_DryMix_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.dryMix;

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
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
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
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.dryMix = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器原始声音（干声）的音量比例到目标回声效果器原始声音（干声）的音量比例的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器原始声音（干声）的音量比例</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_WetMix_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.wetMix;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.wetMix = val;
                })
                .OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.wetMix = start;
                })
                .OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.wetMix = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (echo == null)
                        return;
                    echo.wetMix = val;
                }).OnRewind(() =>
                {
                    if (echo == null)
                        return;
                    if (rewind_set_startvalue)
                        echo.wetMix = start;
                }).OnComplete((duration) =>
                {
                    if (echo == null)
                        return;
                    if (complete_set_endvalue)
                        echo.wetMix = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前回声效果器原始声音（干声）的音量比例到目标回声效果器原始声音（干声）的音量比例的动画
        /// </summary>
        /// <param name="echo">目标 AudioEchoFilter 组件</param>
        /// <param name="endValue">目标回声效果器原始声音（干声）的音量比例</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioEchoFilter_WetMix_To(this AudioEchoFilter echo, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (echo == null)
            {
                Debug.LogError("AudioEchoFilter component is null!");
                return null;
            }

            float start = echo.wetMix;

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
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
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
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = val;
                        }).OnRewind(() =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (echo == null)
                                return;
                            echo.wetMix = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
