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

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个从当前旋转到目标旋转的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标旋转</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="rotationMode">旋转模式</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative, bool autokill, TweenSpace space, RotationMode rotationMode)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentRotation = space == TweenSpace.相对 ? rectTransform.localEulerAngles : rectTransform.eulerAngles;
            Vector3 targetRotation = isRelative ? currentRotation + endValue : endValue;

            // 在方法体内，计算 targetRotation 后调用：
            targetRotation = AdjustRotationByMode(currentRotation, targetRotation, rotationMode);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((rotation, linearProgress, time) =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                })
                .OnRewind(() =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                })
                .OnComplete((duration) =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                })
                .SetAutokill(autokill)
                .SetRelative(isRelative);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                }).OnRewind(() =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                }).OnComplete((duration) =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                })
                .SetAutokill(false)
                .SetRelative(isRelative);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前旋转到目标旋转的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标旋转</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="rotationMode">旋转模式</param>
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative, bool autokill, TweenSpace space, RotationMode rotationMode, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentRotation = space == TweenSpace.相对 ? rectTransform.localEulerAngles : rectTransform.eulerAngles;
            Vector3 targetRotation = isRelative ? currentRotation + endValue : endValue;

            // 在方法体内，计算 targetRotation 后调用：
            targetRotation = AdjustRotationByMode(currentRotation, targetRotation, rotationMode);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
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
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; }
                        }).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前旋转（四元数_Quaternion）到目标旋转的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标旋转（四元数_Quaternion）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="mode">旋转模式（Lerp 或 Slerp）</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Quaternion endValue, float duration, bool isRelative, bool autokill, TweenSpace space, RotateLerpType mode)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Quaternion currentRotation = space == TweenSpace.相对 ? rectTransform.localRotation : rectTransform.rotation;
            Quaternion targetRotation = isRelative ? currentRotation * endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Quaternion>();

                tweener.LerpType = mode;

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((rotation, linearProgress, time) =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                })
                .OnRewind(() =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                })
                .OnComplete((duration) =>
                {
                    if (rectTransform == null)
                        return;
                    if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                })
                .SetAutokill(autokill)
                .SetRelative(isRelative);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode)
                     .OnUpdate((rotation, linearProgress, time) =>
                     {
                         if (rectTransform == null)
                             return;
                         if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                     })
                     .OnRewind(() =>
                     {
                         if (rectTransform == null)
                             return;
                         if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                     })
                     .OnComplete((duration) =>
                     {
                         if (rectTransform == null)
                             return;
                         if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                     })
                     .SetAutokill(false)
                     .SetRelative(isRelative);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前旋转（四元数_Quaternion）到目标旋转的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标旋转（四元数_Quaternion）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="mode">旋转模式（Lerp 或 Slerp）</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Quaternion endValue, float duration, bool isRelative, bool autokill, TweenSpace space, RotateLerpType mode, EaseMode easeMode, bool isFromMode, XTween_Getter<Quaternion> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Quaternion currentRotation = space == TweenSpace.相对 ? rectTransform.localRotation : rectTransform.rotation;
            Quaternion targetRotation = isRelative ? currentRotation * endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Quaternion>();

                tweener.LerpType = mode;

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Quaternion fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
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
                    Quaternion fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; }
                        }).OnRewind(() =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; }
                        }).OnComplete((duration) =>
                        {
                            if (rectTransform == null)
                                return;
                            if (space == TweenSpace.相对) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; }
                        }).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                return tweener;
            }
        }

        /// <summary>
        /// 添加一个静态方法来根据 RotationMode 调整旋转角度
        /// </summary>
        /// <param name="currentEuler"></param>
        /// <param name="targetEuler"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static Vector3 AdjustRotationByMode(Vector3 currentEuler, Vector3 targetEuler, RotationMode mode)
        {
            switch (mode)
            {
                case RotationMode.Normal:
                    // 原有默认行为，直接返回目标角度
                    return targetEuler;
                case RotationMode.Shortest:
                    // 为每个轴计算最短路径
                    Vector3 adjusted = Vector3.zero;
                    for (int i = 0; i < 3; i++)
                    {
                        // 使用 Mathf.DeltaAngle 获取从当前角度到目标角度的最短角度差
                        float delta = Mathf.DeltaAngle(currentEuler[i], targetEuler[i]);
                        adjusted[i] = currentEuler[i] + delta;
                    }
                    return adjusted;
                case RotationMode.FullRotation:
                    // 完整旋转模式：允许完整的多圈旋转
                    // 这个模式直接使用目标角度，不进行角度规范化
                    // 如果用户输入360度，就会执行完整的360度旋转
                    return currentEuler + targetEuler;  // 使用加法来支持相对模式
                default:
                    return targetEuler;
            }
        }
        /// <summary>
        /// 将角度规范化到 [0, 360) 范围
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static float NormalizeAngleTo360(float angle)
        {
            angle %= 360f;
            if (angle < 0f) angle += 360f;
            return angle;
        }
    }
}
