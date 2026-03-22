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
        /// 创建一个从当前RGB通道调谐器Red强度到目标RGB通道调谐器Red强度的动画
        /// </summary>
        /// <param name="mixer">目标 ChannelMixer 组件</param>
        /// <param name="endValue">目标RGB通道调谐器Red强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ChannelMixer_Red_To(this ChannelMixer mixer, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mixer == null)
            {
                Debug.LogError("Channel Mixer component is null!");
                return null;
            }

            Vector3 start = new Vector3(mixer.redOutRedIn.value, mixer.redOutGreenIn.value, mixer.redOutBlueIn.value);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mixer == null)
                        return;
                    mixer.redOutRedIn.value = val.x;
                    mixer.redOutGreenIn.value = val.y;
                    mixer.redOutBlueIn.value = val.z;
                })
                .OnRewind(() =>
                {
                    if (mixer == null)
                        return;
                    if (rewind_set_startvalue)
                    {
                        mixer.redOutRedIn.value = start.x;
                        mixer.redOutGreenIn.value = start.y;
                        mixer.redOutBlueIn.value = start.z;
                    }
                })
                .OnComplete((duration) =>
                {
                    if (mixer == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        mixer.redOutRedIn.value = endValue.x;
                        mixer.redOutGreenIn.value = endValue.y;
                        mixer.redOutBlueIn.value = endValue.z;
                    }
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mixer == null)
                        return;
                    mixer.redOutRedIn.value = val.x;
                    mixer.redOutGreenIn.value = val.y;
                    mixer.redOutBlueIn.value = val.z;
                }).OnRewind(() =>
                {
                    if (mixer == null)
                        return;
                    if (rewind_set_startvalue)
                    {
                        mixer.redOutRedIn.value = start.x;
                        mixer.redOutGreenIn.value = start.y;
                        mixer.redOutBlueIn.value = start.z;
                    }
                }).OnComplete((duration) =>
                {
                    if (mixer == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        mixer.redOutRedIn.value = endValue.x;
                        mixer.redOutGreenIn.value = endValue.y;
                        mixer.redOutBlueIn.value = endValue.z;
                    }
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前RGB通道调谐器Red强度到目标RGB通道调谐器Red强度的动画
        /// </summary>
        /// <param name="mixer">目标 ChannelMixer 组件</param>
        /// <param name="endValue">目标RGB通道调谐器Red强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ChannelMixer_Red_To(this ChannelMixer mixer, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mixer == null)
            {
                Debug.LogError("Channel Mixer component is null!");
                return null;
            }

            Vector3 start = new Vector3(mixer.redOutRedIn.value, mixer.redOutGreenIn.value, mixer.redOutBlueIn.value);

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
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
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
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = val.x;
                            mixer.redOutGreenIn.value = val.y;
                            mixer.redOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = start.x;
                            mixer.redOutGreenIn.value = start.y;
                            mixer.redOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.redOutRedIn.value = endValue.x;
                            mixer.redOutGreenIn.value = endValue.y;
                            mixer.redOutBlueIn.value = endValue.z;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前RGB通道调谐器Green强度到目标RGB通道调谐器Green强度的动画
        /// </summary>
        /// <param name="mixer">目标 ChannelMixer 组件</param>
        /// <param name="endValue">目标RGB通道调谐器Green强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ChannelMixer_Green_To(this ChannelMixer mixer, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mixer == null)
            {
                Debug.LogError("Channel Mixer component is null!");
                return null;
            }

            Vector3 start = new Vector3(mixer.greenOutRedIn.value, mixer.greenOutGreenIn.value, mixer.greenOutBlueIn.value);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mixer == null)
                        return;
                    mixer.greenOutRedIn.value = val.x;
                    mixer.greenOutGreenIn.value = val.y;
                    mixer.greenOutBlueIn.value = val.z;
                })
                .OnRewind(() =>
                {
                    if (mixer == null)
                        return;
                    if (rewind_set_startvalue)
                    {
                        mixer.greenOutRedIn.value = start.x;
                        mixer.greenOutGreenIn.value = start.y;
                        mixer.greenOutBlueIn.value = start.z;
                    }
                })
                .OnComplete((duration) =>
                {
                    if (mixer == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        mixer.greenOutRedIn.value = endValue.x;
                        mixer.greenOutGreenIn.value = endValue.y;
                        mixer.greenOutBlueIn.value = endValue.z;
                    }
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mixer == null)
                        return;
                    mixer.greenOutRedIn.value = val.x;
                    mixer.greenOutGreenIn.value = val.y;
                    mixer.greenOutBlueIn.value = val.z;
                }).OnRewind(() =>
                {
                    if (mixer == null)
                        return;
                    if (rewind_set_startvalue)
                    {
                        mixer.greenOutRedIn.value = start.x;
                        mixer.greenOutGreenIn.value = start.y;
                        mixer.greenOutBlueIn.value = start.z;
                    }
                }).OnComplete((duration) =>
                {
                    if (mixer == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        mixer.greenOutRedIn.value = endValue.x;
                        mixer.greenOutGreenIn.value = endValue.y;
                        mixer.greenOutBlueIn.value = endValue.z;
                    }
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前RGB通道调谐器Green强度到目标RGB通道调谐器Green强度的动画
        /// </summary>
        /// <param name="mixer">目标 ChannelMixer 组件</param>
        /// <param name="endValue">目标RGB通道调谐器Green强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ChannelMixer_Green_To(this ChannelMixer mixer, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mixer == null)
            {
                Debug.LogError("Channel Mixer component is null!");
                return null;
            }

            Vector3 start = new Vector3(mixer.greenOutRedIn.value, mixer.greenOutGreenIn.value, mixer.greenOutBlueIn.value);

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
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
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
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = val.x;
                            mixer.greenOutGreenIn.value = val.y;
                            mixer.greenOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = start.x;
                            mixer.greenOutGreenIn.value = start.y;
                            mixer.greenOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.greenOutRedIn.value = endValue.x;
                            mixer.greenOutGreenIn.value = endValue.y;
                            mixer.greenOutBlueIn.value = endValue.z;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前RGB通道调谐器Blue强度到目标RGB通道调谐器Blue强度的动画
        /// </summary>
        /// <param name="mixer">目标 ChannelMixer 组件</param>
        /// <param name="endValue">目标RGB通道调谐器Blue强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ChannelMixer_Blue_To(this ChannelMixer mixer, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mixer == null)
            {
                Debug.LogError("Channel Mixer component is null!");
                return null;
            }

            Vector3 start = new Vector3(mixer.blueOutRedIn.value, mixer.blueOutGreenIn.value, mixer.blueOutBlueIn.value);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mixer == null)
                        return;
                    mixer.blueOutRedIn.value = val.x;
                    mixer.blueOutGreenIn.value = val.y;
                    mixer.blueOutBlueIn.value = val.z;
                })
                .OnRewind(() =>
                {
                    if (mixer == null)
                        return;
                    if (rewind_set_startvalue)
                    {
                        mixer.blueOutRedIn.value = start.x;
                        mixer.blueOutGreenIn.value = start.y;
                        mixer.blueOutBlueIn.value = start.z;
                    }
                })
                .OnComplete((duration) =>
                {
                    if (mixer == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        mixer.blueOutRedIn.value = endValue.x;
                        mixer.blueOutGreenIn.value = endValue.y;
                        mixer.blueOutBlueIn.value = endValue.z;
                    }
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mixer == null)
                        return;
                    mixer.blueOutRedIn.value = val.x;
                    mixer.blueOutGreenIn.value = val.y;
                    mixer.blueOutBlueIn.value = val.z;
                }).OnRewind(() =>
                {
                    if (mixer == null)
                        return;
                    if (rewind_set_startvalue)
                    {
                        mixer.blueOutRedIn.value = start.x;
                        mixer.blueOutGreenIn.value = start.y;
                        mixer.blueOutBlueIn.value = start.z;
                    }
                }).OnComplete((duration) =>
                {
                    if (mixer == null)
                        return;
                    if (complete_set_endvalue)
                    {
                        mixer.blueOutRedIn.value = endValue.x;
                        mixer.blueOutGreenIn.value = endValue.y;
                        mixer.blueOutBlueIn.value = endValue.z;
                    }
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前RGB通道调谐器Blue强度到目标RGB通道调谐器Blue强度的动画
        /// </summary>
        /// <param name="mixer">目标 ChannelMixer 组件</param>
        /// <param name="endValue">目标RGB通道调谐器Blue强度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ChannelMixer_Blue_To(this ChannelMixer mixer, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mixer == null)
            {
                Debug.LogError("Channel Mixer component is null!");
                return null;
            }

            Vector3 start = new Vector3(mixer.greenOutRedIn.value, mixer.greenOutGreenIn.value, mixer.greenOutBlueIn.value);

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
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
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
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = val.x;
                            mixer.blueOutGreenIn.value = val.y;
                            mixer.blueOutBlueIn.value = val.z;
                        }).OnRewind(() =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = start.x;
                            mixer.blueOutGreenIn.value = start.y;
                            mixer.blueOutBlueIn.value = start.z;
                        }).OnComplete((duration) =>
                        {
                            if (mixer == null)
                                return;
                            mixer.blueOutRedIn.value = endValue.x;
                            mixer.blueOutGreenIn.value = endValue.y;
                            mixer.blueOutBlueIn.value = endValue.z;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
