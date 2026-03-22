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
        /// 创建一个从当前音源音量值到目标音源音量值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_Volume_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.volume;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.volume = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.volume = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.volume = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.volume = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.volume = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.volume = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源音量值到目标音源音量值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源音量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_Volume_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.volume;

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
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
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
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.volume = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源音高值到目标音源音高值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源音高值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_Pitch_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.pitch;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.volume = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.pitch = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.pitch = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.pitch = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.pitch = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.pitch = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源音高值到目标音源音高值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源音高值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_Pitch_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.pitch;

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
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
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
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.pitch = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源声道值到目标音源声道值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源声道值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_StereoPan_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.panStereo;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.panStereo = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.panStereo = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.panStereo = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.panStereo = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.panStereo = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.panStereo = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源声道值到目标音源声道值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源声道值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_StereoPan_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.panStereo;

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
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
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
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.panStereo = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源空间混合值到目标音源空间混合值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源空间混合值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_SpatialBlend_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.spatialBlend;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.spatialBlend = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.spatialBlend = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.spatialBlend = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.spatialBlend = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.spatialBlend = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.spatialBlend = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源空间混合值到目标音源空间混合值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源空间混合值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_SpatialBlend_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.spatialBlend;

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
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
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
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spatialBlend = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响混合值到目标音源混响混合值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源混响混合值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_ReverbZoneMix_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.reverbZoneMix;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.reverbZoneMix = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.reverbZoneMix = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.reverbZoneMix = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.reverbZoneMix = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.reverbZoneMix = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.reverbZoneMix = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源混响混合值到目标音源混响混合值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源混响混合值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_ReverbZoneMix_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.reverbZoneMix;

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
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
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
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.reverbZoneMix = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源多普勒级别值到目标音源多普勒级别值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源多普勒级别值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_DopplerLevel_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.dopplerLevel;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.dopplerLevel = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.dopplerLevel = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.dopplerLevel = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.dopplerLevel = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.dopplerLevel = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.dopplerLevel = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源多普勒级别值到目标音源多普勒级别值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源多普勒级别值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_DopplerLevel_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.dopplerLevel;

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
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
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
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.dopplerLevel = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源扩散值到目标音源扩散值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源扩散值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_Spread_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.spread;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.spread = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.spread = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.spread = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.spread = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.spread = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.spread = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源扩散值到目标音源扩散值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源扩散值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_Spread_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.spread;

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
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
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
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.spread = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源衰减最小距离到目标音源衰减最小距离的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源衰减最小距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_DistanceMin_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.minDistance;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.minDistance = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.minDistance = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.minDistance = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.minDistance = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.minDistance = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.minDistance = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源衰减最小距离到目标音源衰减最小距离的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源衰减最小距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_DistanceMin_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.minDistance;

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
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
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
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.minDistance = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源衰减最大距离到目标音源衰减最大距离的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源衰减最大距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_DistanceMax_To(this AudioSource audio, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.maxDistance;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.maxDistance = val;
                })
                .OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.maxDistance = start;
                })
                .OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.maxDistance = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (audio == null)
                        return;
                    audio.maxDistance = val;
                }).OnRewind(() =>
                {
                    if (audio == null)
                        return;
                    if (rewind_set_startvalue)
                        audio.maxDistance = start;
                }).OnComplete((duration) =>
                {
                    if (audio == null)
                        return;
                    if (complete_set_endvalue)
                        audio.maxDistance = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前音源衰减最大距离到目标音源衰减最大距离的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="audio">目标 AudioSource 组件</param>
        /// <param name="endValue">目标音源衰减最大距离</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Audio_DistanceMax_To(this AudioSource audio, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (audio == null)
            {
                Debug.LogError("AudioSource component is null!");
                return null;
            }

            float start = audio.maxDistance;

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
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
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
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = val;
                        }).OnRewind(() =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (audio == null)
                                return;
                            audio.maxDistance = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
