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
        /// 创建一个从当前音源混响滤波器原始声音（干声）音量值到目标音源混响原始声音（干声）音量值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbFilter 组件</param>
        /// <param name="endValue">目标音源混响滤波器原始声音（干声）音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_DryLevel_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.dryLevel;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.dryLevel = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.dryLevel = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.dryLevel = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.dryLevel = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.dryLevel = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.dryLevel = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器原始声音（干声）音量值到目标音源混响滤波器原始声音（干声）音量值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbFilter 组件</param>
        /// <param name="endValue">目标音源混响滤波器原始声音（干声）音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_DryLevel_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.dryLevel;

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
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
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
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.dryLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器Room值到目标音源混响滤波器Room值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器Room值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_Room_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.room;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.room = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.room = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.room = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.room = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.room = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.room = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器Room值到目标音源混响滤波器Room值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器Room值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_Room_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.room;

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
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
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
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.room = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.room = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器RoomHF值到目标音源混响滤波器RoomHF值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器RoomHF值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_RoomHF_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.roomHF;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.roomHF = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.roomHF = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.roomHF = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.roomHF = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.roomHF = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.roomHF = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器RoomHF值到目标音源混响滤波器RoomHF值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器RoomHF值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_RoomHF_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.roomHF;

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
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
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
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomHF = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器RoomLF值到目标音源混响滤波器RoomLF值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器RoomLF值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_RoomLF_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.roomLF;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.roomLF = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.roomLF = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.roomLF = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.roomLF = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.roomLF = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.roomLF = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器RoomLF值到目标音源混响滤波器RoomLF值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器RoomLF值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_RoomLF_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.roomLF;

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
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
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
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.roomLF = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器衰减值到目标音源混响滤波器衰减值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器衰减值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_DecayTime_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.decayTime;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.decayTime = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.decayTime = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.decayTime = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.decayTime = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.decayTime = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.decayTime = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器衰减值到目标音源混响滤波器衰减值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器衰减值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_DecayTime_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.decayTime;

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
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
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
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayTime = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器高频衰减比值到目标音源混响滤波器高频衰减比值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器高频衰减比值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_DecayHFRatio_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.decayHFRatio;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.decayHFRatio = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.decayHFRatio = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.decayHFRatio = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.decayHFRatio = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.decayHFRatio = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.decayHFRatio = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器高频衰减比值到目标音源混响滤波器高频衰减比值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器高频衰减比值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_DecayHFRatio_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.decayHFRatio;

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
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
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
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器早期反射值到目标音源混响滤波器早期反射值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器早期反射值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReflectionsLevel_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.reflectionsLevel;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.reflectionsLevel = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.reflectionsLevel = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.reflectionsLevel = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.reflectionsLevel = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.reflectionsLevel = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.reflectionsLevel = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器早期反射值到目标音源混响滤波器早期反射值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器早期反射值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReflectionsLevel_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<int> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.reflectionsLevel;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    int fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
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
                    int fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reflectionsLevel = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器早期反射延迟值到目标音源混响滤波器早期反射延迟值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器早期反射延迟值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReflectionsDelay_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.decayHFRatio;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.decayHFRatio = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.decayHFRatio = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.decayHFRatio = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.decayHFRatio = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.decayHFRatio = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.decayHFRatio = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器早期反射延迟值到目标音源混响滤波器早期反射延迟值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器早期反射延迟值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReflectionsDelay_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.decayHFRatio;

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
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
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
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.decayHFRatio = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器音量值到目标音源混响滤波器音量值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReverbLevel_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.reverbLevel;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.reverbLevel = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.reverbLevel = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.reverbLevel = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.reverbLevel = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.reverbLevel = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.reverbLevel = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器音量值到目标音源混响滤波器音量值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReverbLevel_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.reverbLevel;

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
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
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
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbLevel = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器延迟值到目标音源混响滤波器延迟值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器延迟值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReverbDelay_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.reverbDelay;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.reverbDelay = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.reverbDelay = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.reverbDelay = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.reverbDelay = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.reverbDelay = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.reverbDelay = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器延迟值到目标音源混响滤波器延迟值的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器延迟值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_ReverbDelay_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.reverbDelay;

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
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
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
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.reverbDelay = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器高频参考频率到目标音源混响滤波器高频参考频率的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器高频参考频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_HFReference_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.hfReference;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.hfReference = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.hfReference = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.hfReference = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.hfReference = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.hfReference = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.hfReference = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器高频参考频率到目标音源混响滤波器高频参考频率的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器高频参考频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_HFReference_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.hfReference;

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
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
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
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.hfReference = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器低频参考频率到目标音源混响滤波器低频参考频率的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器低频参考频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_LFReference_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.lfReference;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.lfReference = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.lfReference = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.lfReference = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.lfReference = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.lfReference = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.lfReference = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器低频参考频率到目标音源混响滤波器低频参考频率的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器低频参考频率</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_LFReference_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.lfReference;

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
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
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
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.lfReference = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器扩散度到目标音源混响滤波器扩散度的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器扩散度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_Diffusion_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.diffusion;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.diffusion = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.diffusion = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.diffusion = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.diffusion = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.diffusion = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.diffusion = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器扩散度到目标音源混响滤波器扩散度的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器扩散度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_Diffusion_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.diffusion;

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
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
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
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.diffusion = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器密度到目标音源混响滤波器密度的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器密度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_Density_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.density;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.density = val;
                })
                .OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.density = start;
                })
                .OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.density = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (zone == null)
                        return;
                    zone.density = val;
                }).OnRewind(() =>
                {
                    if (zone == null)
                        return;
                    if (rewind_set_startvalue)
                        zone.density = start;
                }).OnComplete((duration) =>
                {
                    if (zone == null)
                        return;
                    if (complete_set_endvalue)
                        zone.density = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响滤波器密度到目标音源混响滤波器密度的动画
        /// </summary>
        /// <param name="zone">目标 AudioReverbZone 组件</param>
        /// <param name="endValue">目标音源混响滤波器密度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AudioReverbFilter_Density_To(this AudioReverbFilter zone, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (zone == null)
            {
                Debug.LogError("AudioReverbFilter component is null!");
                return null;
            }

            float start = zone.density;

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
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
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
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = val;
                        }).OnRewind(() =>
                        {
                            if (zone == null)
                                return;
                            zone.density = start;
                        }).OnComplete((duration) =>
                        {
                            if (zone == null)
                                return;
                            zone.density = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}