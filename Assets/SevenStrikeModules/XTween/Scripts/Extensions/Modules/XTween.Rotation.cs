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
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative = false, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentRotation = isRelative ? rectTransform.localEulerAngles : rectTransform.eulerAngles;
            Vector3 targetRotation = isRelative ? currentRotation + endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((rotation, linearProgress, time) =>
                {
                    if (isRelative)
                    {
                        rectTransform.localEulerAngles = rotation;
                    }
                    else
                    {
                        rectTransform.eulerAngles = rotation;
                    }
                })
                .OnRewind(() =>
                {
                    if (isRelative)
                    {
                        rectTransform.localEulerAngles = currentRotation;
                    }
                    else
                    {
                        rectTransform.eulerAngles = currentRotation;
                    }
                })
                .OnComplete((duration) =>
                {
                    if (isRelative)
                    {
                        rectTransform.localEulerAngles = targetRotation;
                    }
                    else
                    {
                        rectTransform.eulerAngles = targetRotation;
                    }
                })
                .SetAutokill(autokill)
                .SetRelative(isRelative);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((rotation, linearProgress, time) =>
                        {
                            if (isRelative)
                            {
                                rectTransform.localEulerAngles = rotation;
                            }
                            else
                            {
                                rectTransform.eulerAngles = rotation;
                            }
                        })
                        .OnRewind(() =>
                        {
                            if (isRelative)
                            {
                                rectTransform.localEulerAngles = currentRotation;
                            }
                            else
                            {
                                rectTransform.eulerAngles = currentRotation;
                            }
                        })
                        .OnComplete((duration) =>
                        {
                            if (isRelative)
                            {
                                rectTransform.localEulerAngles = targetRotation;
                            }
                            else
                            {
                                rectTransform.eulerAngles = targetRotation;
                            }
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
        /// <param name="isRelative">是否为相对变化</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative = false, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentRotation = isRelative ? rectTransform.localEulerAngles : rectTransform.eulerAngles;
            Vector3 targetRotation = isRelative ? currentRotation + endValue : endValue;

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
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
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
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetFrom(fromval).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localEulerAngles = rotation; } else { rectTransform.eulerAngles = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localEulerAngles = currentRotation; } else { rectTransform.eulerAngles = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localEulerAngles = targetRotation; } else { rectTransform.eulerAngles = targetRotation; } }).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
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
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Quaternion endValue, float duration, bool isRelative = false, bool autokill = false, HudRotateMode mode = HudRotateMode.SlerpUnclamped)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Quaternion currentRotation = isRelative ? rectTransform.localRotation : rectTransform.rotation;
            Quaternion targetRotation = isRelative ? currentRotation * endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Quaternion>();

                tweener.HudRotateMode = mode;

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((rotation, linearProgress, time) =>
                {
                    if (isRelative)
                    {
                        rectTransform.localRotation = rotation;
                    }
                    else
                    {
                        rectTransform.rotation = rotation;
                    }
                })
                .OnRewind(() =>
                {
                    if (isRelative)
                    {
                        rectTransform.localRotation = currentRotation;
                    }
                    else
                    {
                        rectTransform.rotation = currentRotation;
                    }
                })
                .OnComplete((duration) =>
                {
                    if (isRelative)
                    {
                        rectTransform.localRotation = targetRotation;
                    }
                    else
                    {
                        rectTransform.rotation = targetRotation;
                    }
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
                         if (isRelative)
                         {
                             rectTransform.localRotation = rotation;
                         }
                         else
                         {
                             rectTransform.rotation = rotation;
                         }
                     })
                     .OnRewind(() =>
                     {
                         if (isRelative)
                         {
                             rectTransform.localRotation = currentRotation;
                         }
                         else
                         {
                             rectTransform.rotation = currentRotation;
                         }
                     })
                     .OnComplete((duration) =>
                     {
                         if (isRelative)
                         {
                             rectTransform.localRotation = targetRotation;
                         }
                         else
                         {
                             rectTransform.rotation = targetRotation;
                         }
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
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Rotate_To(this UnityEngine.RectTransform rectTransform, Quaternion endValue, float duration, bool isRelative = false, bool autokill = false, HudRotateMode mode = HudRotateMode.SlerpUnclamped, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Quaternion> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Quaternion currentRotation = isRelative ? rectTransform.localRotation : rectTransform.rotation;
            Quaternion targetRotation = isRelative ? currentRotation * endValue : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Quaternion>();

                tweener.HudRotateMode = mode;

                tweener.Initialize(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Quaternion fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
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
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetFrom(fromval).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetEase(curve).SetAutokill(false).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Quaternion(currentRotation, targetRotation, duration * XTween_Dashboard.DurationMultiply, mode).OnUpdate((rotation, linearProgress, time) => { if (isRelative) { rectTransform.localRotation = rotation; } else { rectTransform.rotation = rotation; } }).OnRewind(() => { if (isRelative) { rectTransform.localRotation = currentRotation; } else { rectTransform.rotation = currentRotation; } }).OnComplete((duration) => { if (isRelative) { rectTransform.localRotation = targetRotation; } else { rectTransform.rotation = targetRotation; } }).SetEase(easeMode).SetAutokill(false).SetRelative(isRelative);
                    }
                }
                return tweener;
            }
        }
    }
}
