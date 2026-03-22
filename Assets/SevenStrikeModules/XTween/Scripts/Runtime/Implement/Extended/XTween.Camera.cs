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
        /// 创建一个从当前相机背景颜色到目标相机背景颜色的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机背景颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_BackgroundColor_To(this Camera cam, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            Color start = cam.backgroundColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.backgroundColor = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.backgroundColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.backgroundColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.backgroundColor = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.backgroundColor = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.backgroundColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机背景颜色到目标相机背景颜色的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机背景颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_BackgroundColor_To(this Camera cam, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            Color start = cam.backgroundColor;

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
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
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
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.backgroundColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }

        // ============================================================================
        // CameraProjection
        // ============================================================================

        /// <summary>
        /// 创建一个从当前相机视场到目标相机视场的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机视场</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Fov_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.fieldOfView;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.fieldOfView = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.fieldOfView = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.fieldOfView = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.fieldOfView = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.fieldOfView = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.fieldOfView = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机视场到目标相机视场的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机视场</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Fov_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.fieldOfView;

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
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
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
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.fieldOfView = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机正交尺寸到目标相机正交尺寸的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机正交尺寸</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_OthorgraphicSize_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.orthographicSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.orthographicSize = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.orthographicSize = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.orthographicSize = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.orthographicSize = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.orthographicSize = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.orthographicSize = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机正交尺寸到目标相机正交尺寸的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机正交尺寸</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_OthorgraphicSize_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.orthographicSize;

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
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
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
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.orthographicSize = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机近距剪切到目标相机近距剪切的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机近距剪切</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ClippingNear_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.nearClipPlane;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.nearClipPlane = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.nearClipPlane = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.nearClipPlane = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.nearClipPlane = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.nearClipPlane = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.nearClipPlane = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机近距剪切到目标相机近距剪切的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机近距剪切</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ClippingNear_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.nearClipPlane;

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
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
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
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.nearClipPlane = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机远距剪切到目标相机远距剪切的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机远距剪切</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ClippingFar_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.farClipPlane;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.farClipPlane = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.farClipPlane = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.farClipPlane = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.farClipPlane = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.farClipPlane = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.farClipPlane = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机远距剪切到目标相机远距剪切的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机远距剪切</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ClippingFar_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.farClipPlane;

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
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
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
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.farClipPlane = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }

        // ============================================================================
        // CameraBody
        // ============================================================================

        /// <summary>
        /// 创建一个从当前相机物理特性-传感器尺寸到目标相机物理特性-传感器尺寸的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-传感器尺寸</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_SensorSize_To(this Camera cam, Vector2 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError("Camera component is null!");
                return null;
            }

            Vector2 start = cam.sensorSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.sensorSize = size;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.sensorSize = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.sensorSize = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.sensorSize = size;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.sensorSize = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.sensorSize = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-传感器尺寸到目标相机物理特性-传感器尺寸的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-传感器尺寸</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_SensorSize_To(this Camera cam, Vector2 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector2> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            Vector2 start = cam.sensorSize;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
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
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.sensorSize = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-ISO到目标相机物理特性-ISO的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-ISO</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ISO_To(this Camera cam, int endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            int start = cam.iso;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.iso = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.iso = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.iso = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.iso = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.iso = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.iso = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-ISO到目标相机物理特性-ISO的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机远距剪切</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ISO_To(this Camera cam, int endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<int> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            int start = cam.iso;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

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
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
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
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.iso = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机快门速度到目标相机快门速度的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机快门速度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ShutterSpeed_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.shutterSpeed;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.shutterSpeed = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.shutterSpeed = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.shutterSpeed = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.shutterSpeed = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.shutterSpeed = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.shutterSpeed = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机快门速度到目标相机快门速度的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机快门速度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_ShutterSpeed_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.shutterSpeed;

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
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
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
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.shutterSpeed = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }

        // ============================================================================
        // Lens
        // ============================================================================

        /// <summary>
        /// 创建一个从当前相机焦距到目标相机焦距的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机焦距</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_FocalLength_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.focalLength;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.focalLength = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.focalLength = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.focalLength = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.focalLength = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.focalLength = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.focalLength = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机焦距到目标相机焦距的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机焦距</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_FocalLength_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.focalLength;

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
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
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
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focalLength = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-传感器移轴值到目标相机物理特性-传感器移轴值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-传感器移轴值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_SensorShift_To(this Camera cam, Vector2 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError("Camera component is null!");
                return null;
            }

            Vector2 start = cam.lensShift;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.lensShift = size;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.lensShift = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.lensShift = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.lensShift = size;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.lensShift = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.lensShift = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-传感器移轴值到目标相机物理特性-传感器移轴值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-传感器移轴值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_SensorShift_To(this Camera cam, Vector2 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector2> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            Vector2 start = cam.lensShift;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
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
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.lensShift = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-光圈值到目标相机物理特性-光圈值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-光圈值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Aperture_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.aperture;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.aperture = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.aperture = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.aperture = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.aperture = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.aperture = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.aperture = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-光圈值到目标相机物理特性-光圈值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-光圈值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Aperture_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.aperture;

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
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
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
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.aperture = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-对焦距离值到目标相机物理特性-对焦距离值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-对焦距离值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_FocalDistance_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.focusDistance;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.focusDistance = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.focusDistance = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.focusDistance = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.focusDistance = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.focusDistance = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.focusDistance = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-对焦距离值到目标相机物理特性-对焦距离值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-对焦距离值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_FocalDistance_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.focusDistance;

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
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
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
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.focusDistance = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }

        // ============================================================================
        // Aperture Shape
        // ============================================================================

        /// <summary>
        /// 创建一个从当前相机物理特性-镜头曲率值到目标相机物理特性-镜头曲率值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-镜头曲率值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Curvature_To(this Camera cam, Vector2 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError("Camera component is null!");
                return null;
            }

            Vector2 start = cam.curvature;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.curvature = size;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.curvature = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.curvature = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.curvature = size;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.curvature = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.curvature = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-镜头曲率值到目标相机物理特性-镜头曲率值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-镜头曲率值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Curvature_To(this Camera cam, Vector2 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector2> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            Vector2 start = cam.curvature;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
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
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.curvature = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-桶形裁剪值到目标相机物理特性-桶形裁剪值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-桶形裁剪值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_BarrelClipping_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.barrelClipping;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.barrelClipping = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.barrelClipping = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.barrelClipping = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.barrelClipping = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.barrelClipping = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.barrelClipping = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-桶形裁剪值到目标相机物理特性-桶形裁剪值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-桶形裁剪值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_BarrelClipping_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.barrelClipping;

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
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
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
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.barrelClipping = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-控制水平压缩比例值到目标相机物理特性-控制水平压缩比例值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-控制水平压缩比例值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Anamorphism_To(this Camera cam, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.anamorphism;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.anamorphism = val;
                })
                .OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.anamorphism = start;
                })
                .OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.anamorphism = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (cam == null)
                        return;
                    cam.anamorphism = val;
                }).OnRewind(() =>
                {
                    if (cam == null)
                        return;
                    if (rewind_set_startvalue)
                        cam.anamorphism = start;
                }).OnComplete((duration) =>
                {
                    if (cam == null)
                        return;
                    if (complete_set_endvalue)
                        cam.anamorphism = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前相机物理特性-控制水平压缩比例值到目标相机物理特性-控制水平压缩比例值的动画
        /// </summary>
        /// <param name="cam">目标 Camera 组件</param>
        /// <param name="endValue">目标相机物理特性-控制水平压缩比例值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Camera_Anamorphism_To(this Camera cam, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (cam == null)
            {
                Debug.LogError(" Camera component is null!");
                return null;
            }

            float start = cam.anamorphism;

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
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
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
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = val;
                        }).OnRewind(() =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = start;
                        }).OnComplete((duration) =>
                        {
                            if (cam == null)
                                return;
                            cam.anamorphism = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
