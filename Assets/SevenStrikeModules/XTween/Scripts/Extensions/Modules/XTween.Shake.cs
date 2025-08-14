namespace SevenStrikeModules.XTween
{
    using UnityEngine;

    public static partial class XTween
    {
        /// <summary>
        /// 创建一个位置抖动动画，使目标 RectTransform 在指定时间内随机抖动
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">抖动强度（三维向量_Vector3 类型，表示每个轴的最大抖动距离）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakePosition(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float vibrato, float randomness, float duration, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentPosition = rectTransform.anchoredPosition3D;
            Vector3 shakeStrength = endValue;

            float elapsed = 0f;
            Vector3 randomDirection = Vector3.zero;
            System.Random rand = new System.Random();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((pos, progress, time) =>
                {
                    elapsed += Time.deltaTime;

                    if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato)
                    {
                        elapsed = 0f;
                        randomDirection = new Vector3(
                                (float)(rand.NextDouble() * 2 - 1) * randomness,
                                (float)(rand.NextDouble() * 2 - 1) * randomness,
                                (float)(rand.NextDouble() * 2 - 1) * randomness
                        ).normalized;
                    }

                    float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress);
                    Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount;

                    rectTransform.anchoredPosition3D = currentPosition + shakeOffset;
                })
                        .OnRewind(() =>
                        {
                            rectTransform.anchoredPosition3D = currentPosition;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.anchoredPosition3D = currentPosition;
                        })
                        .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;

                tweener = new XTween_Specialized_Vector3(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply)
                     .OnUpdate((pos, progress, time) =>
                     {
                         elapsed += Time.deltaTime;

                         if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato)
                         {
                             elapsed = 0f;
                             randomDirection = new Vector3(
                                                     (float)(rand.NextDouble() * 2 - 1) * randomness,
                                                     (float)(rand.NextDouble() * 2 - 1) * randomness,
                                                     (float)(rand.NextDouble() * 2 - 1) * randomness
                                             ).normalized;
                         }

                         float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress);
                         Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount;

                         rectTransform.anchoredPosition3D = currentPosition + shakeOffset;
                     })
                     .OnRewind(() =>
                     {
                         rectTransform.anchoredPosition3D = currentPosition;
                     })
                     .OnComplete((duration) =>
                     {
                         rectTransform.anchoredPosition3D = currentPosition;
                     })
                     .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个位置抖动动画，使目标 RectTransform 在指定时间内随机抖动
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">抖动强度（三维向量_Vector3 类型，表示每个轴的最大抖动距离）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakePosition(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float vibrato, float randomness, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentPosition = rectTransform.anchoredPosition3D;
            Vector3 shakeStrength = endValue;

            float elapsed = 0f;
            Vector3 randomDirection = Vector3.zero;
            System.Random rand = new System.Random();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Vector3(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentPosition, currentPosition, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; randomDirection = new Vector3((float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness, (float)(rand.NextDouble() * 2 - 1) * randomness).normalized; } float shakeAmount = Mathf.Sin(time * vibrato * Mathf.PI * 2) * (1 - progress); Vector3 shakeOffset = Vector3.Scale(shakeStrength, randomDirection) * shakeAmount; rectTransform.anchoredPosition3D = currentPosition + shakeOffset; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).OnComplete((duration) => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个旋转抖动动画，使目标 RectTransform 在指定时间内随机旋转抖动
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="strength">抖动强度（三维向量_Vector3 类型，表示每个轴的最大旋转角度）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakeRotation(this UnityEngine.RectTransform rectTransform, Vector3 strength, float vibrato, float randomness, float duration, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Quaternion originalRotation = rectTransform.localRotation;
            Vector3 originalEuler = originalRotation.eulerAngles;

            float elapsed = 0f;
            Vector3 randomDirection = Vector3.zero;
            System.Random rand = new System.Random();
            Vector3[] perAxisRandomness = new Vector3[3];

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnStart(() =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        perAxisRandomness[i] = new Vector3(
                                (float)rand.NextDouble() * 2 - 1,
                                (float)rand.NextDouble() * 2 - 1,
                                (float)rand.NextDouble() * 2 - 1
                        ).normalized;
                    }
                })
                .OnUpdate((rotation, progress, time) =>
                {
                    elapsed += Time.deltaTime;

                    if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato)
                    {
                        elapsed = 0f;
                        for (int i = 0; i < 3; i++)
                        {
                            perAxisRandomness[i] = Vector3.Slerp(
                                                    perAxisRandomness[i],
                                                    new Vector3(
                                                            (float)(rand.NextDouble() * 2 - 1),
                                                            (float)(rand.NextDouble() * 2 - 1),
                                                            (float)(rand.NextDouble() * 2 - 1)
                                                    ).normalized,
                                                    randomness
                                            );
                        }
                    }

                    Vector3 shakeOffset = Vector3.zero;
                    float decay = 1 - progress;

                    for (int i = 0; i < 3; i++)
                    {
                        if (strength[i] > 0)
                        {
                            float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1;
                            shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i];
                        }
                    }

                    rectTransform.localRotation = Quaternion.Euler(
                                            originalEuler.x + shakeOffset.x,
                                            originalEuler.y + shakeOffset.y,
                                            originalEuler.z + shakeOffset.z
                                    );
                })
                .OnRewind(() =>
                {
                    rectTransform.localRotation = originalRotation;
                })
                .OnComplete((duration) =>
                {
                    rectTransform.localRotation = originalRotation;
                })
                .OnKill(() =>
                {
                    rectTransform.localRotation = originalRotation;
                })
                .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;

                tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply)
                        .OnStart(() =>
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                perAxisRandomness[i] = new Vector3(
                                                        (float)rand.NextDouble() * 2 - 1,
                                                        (float)rand.NextDouble() * 2 - 1,
                                                        (float)rand.NextDouble() * 2 - 1
                                                ).normalized;
                            }
                        })
                        .OnUpdate((rotation, progress, time) =>
                        {
                            elapsed += Time.deltaTime;

                            if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato)
                            {
                                elapsed = 0f;
                                for (int i = 0; i < 3; i++)
                                {
                                    perAxisRandomness[i] = Vector3.Slerp(
                                                            perAxisRandomness[i],
                                                            new Vector3(
                                                                    (float)(rand.NextDouble() * 2 - 1),
                                                                    (float)(rand.NextDouble() * 2 - 1),
                                                                    (float)(rand.NextDouble() * 2 - 1)
                                                            ).normalized,
                                                            randomness
                                                    );
                                }
                            }

                            Vector3 shakeOffset = Vector3.zero;
                            float decay = 1 - progress;

                            for (int i = 0; i < 3; i++)
                            {
                                if (strength[i] > 0)
                                {
                                    float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1;
                                    shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i];
                                }
                            }

                            rectTransform.localRotation = Quaternion.Euler(
                                                    originalEuler.x + shakeOffset.x,
                                                    originalEuler.y + shakeOffset.y,
                                                    originalEuler.z + shakeOffset.z
                                            );
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.localRotation = originalRotation;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.localRotation = originalRotation;
                        })
                        .OnKill(() =>
                        {
                            rectTransform.localRotation = originalRotation;
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个旋转抖动动画，使目标 RectTransform 在指定时间内随机旋转抖动
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="strength">抖动强度（三维向量_Vector3 类型，表示每个轴的最大旋转角度）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakeRotation(this UnityEngine.RectTransform rectTransform, Vector3 strength, float vibrato, float randomness, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Quaternion originalRotation = rectTransform.localRotation;
            Vector3 originalEuler = originalRotation.eulerAngles;

            float elapsed = 0f;
            Vector3 randomDirection = Vector3.zero;
            System.Random rand = new System.Random();
            Vector3[] perAxisRandomness = new Vector3[3];

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisRandomness[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; } }).OnUpdate((rotation, progress, time) => { elapsed += Time.deltaTime; if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / vibrato) { elapsed = 0f; for (int i = 0; i < 3; i++) { perAxisRandomness[i] = Vector3.Slerp(perAxisRandomness[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness); } } Vector3 shakeOffset = Vector3.zero; float decay = 1 - progress; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { float noise = Mathf.PerlinNoise(time * vibrato * 0.5f, i * 10f) * 2 - 1; shakeOffset[i] = strength[i] * noise * decay * perAxisRandomness[i][i]; } } rectTransform.localRotation = Quaternion.Euler(originalEuler.x + shakeOffset.x, originalEuler.y + shakeOffset.y, originalEuler.z + shakeOffset.z); }).OnRewind(() => { rectTransform.localRotation = originalRotation; }).OnComplete((duration) => { rectTransform.localRotation = originalRotation; }).OnKill(() => { rectTransform.localRotation = originalRotation; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个缩放抖动动画，使目标 RectTransform 在指定时间内随机缩放抖动
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="strength">抖动强度（三维向量_Vector3 类型，表示每个轴的最大缩放变化）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="fadeOut">是否在动画结束时逐渐减弱抖动</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakeScale(this UnityEngine.RectTransform rectTransform, Vector3 strength, float vibrato, float randomness, bool fadeOut, float duration, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 originalScale = rectTransform.localScale;

            float elapsed = 0f;
            Vector3[] perAxisDirections = new Vector3[3];
            float[] perAxisSpeeds = new float[3];
            System.Random rand = new System.Random();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnStart(() =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        perAxisDirections[i] = new Vector3(
                                (float)rand.NextDouble() * 2 - 1,
                                (float)rand.NextDouble() * 2 - 1,
                                (float)rand.NextDouble() * 2 - 1
                        ).normalized;

                        perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f;
                    }
                })
                        .OnUpdate((scale, progress, time) =>
                        {
                            elapsed += Time.deltaTime;
                            float decay = fadeOut ? (1 - progress) : 1f;

                            Vector3 scaleOffset = Vector3.zero;

                            for (int i = 0; i < 3; i++)
                            {
                                if (strength[i] > 0)
                                {
                                    if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i]))
                                    {
                                        elapsed = 0f;
                                        perAxisDirections[i] = Vector3.Lerp(
                                                                perAxisDirections[i],
                                                                new Vector3(
                                                                        (float)(rand.NextDouble() * 2 - 1),
                                                                        (float)(rand.NextDouble() * 2 - 1),
                                                                        (float)(rand.NextDouble() * 2 - 1)
                                                                ).normalized,
                                                                randomness
                                                        ).normalized;
                                    }

                                    float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]);

                                    scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i];
                                }
                            }

                            rectTransform.localScale = new Vector3(
                                                    Mathf.Max(0.1f, originalScale.x + scaleOffset.x),
                                                    Mathf.Max(0.1f, originalScale.y + scaleOffset.y),
                                                    Mathf.Max(0.1f, originalScale.z + scaleOffset.z)
                                            );
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.localScale = originalScale;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.localScale = originalScale;
                        })
                        .OnKill(() =>
                        {
                            rectTransform.localScale = originalScale;
                        })
                        .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;

                tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply)
                        .OnStart(() =>
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                perAxisDirections[i] = new Vector3(
                                                        (float)rand.NextDouble() * 2 - 1,
                                                        (float)rand.NextDouble() * 2 - 1,
                                                        (float)rand.NextDouble() * 2 - 1
                                                ).normalized;

                                perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f;
                            }
                        })
                        .OnUpdate((scale, progress, time) =>
                        {
                            elapsed += Time.deltaTime;
                            float decay = fadeOut ? (1 - progress) : 1f;

                            Vector3 scaleOffset = Vector3.zero;

                            for (int i = 0; i < 3; i++)
                            {
                                if (strength[i] > 0)
                                {
                                    if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i]))
                                    {
                                        elapsed = 0f;
                                        perAxisDirections[i] = Vector3.Lerp(
                                                                perAxisDirections[i],
                                                                new Vector3(
                                                                        (float)(rand.NextDouble() * 2 - 1),
                                                                        (float)(rand.NextDouble() * 2 - 1),
                                                                        (float)(rand.NextDouble() * 2 - 1)
                                                                ).normalized,
                                                                randomness
                                                        ).normalized;
                                    }

                                    float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]);

                                    scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i];
                                }
                            }

                            rectTransform.localScale = new Vector3(
                                                    Mathf.Max(0.1f, originalScale.x + scaleOffset.x),
                                                    Mathf.Max(0.1f, originalScale.y + scaleOffset.y),
                                                    Mathf.Max(0.1f, originalScale.z + scaleOffset.z)
                                            );
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.localScale = originalScale;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.localScale = originalScale;
                        })
                        .OnKill(() =>
                        {
                            rectTransform.localScale = originalScale;
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个缩放抖动动画，使目标 RectTransform 在指定时间内随机缩放抖动
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="strength">抖动强度（三维向量_Vector3 类型，表示每个轴的最大缩放变化）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="fadeOut">是否在动画结束时逐渐减弱抖动</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakeScale(this UnityEngine.RectTransform rectTransform, Vector3 strength, float vibrato, float randomness, bool fadeOut, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 originalScale = rectTransform.localScale;

            float elapsed = 0f;
            Vector3[] perAxisDirections = new Vector3[3];
            float[] perAxisSpeeds = new float[3];
            System.Random rand = new System.Random();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(Vector3.zero, Vector3.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 3; i++) { perAxisDirections[i] = new Vector3((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((scale, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector3 scaleOffset = Vector3.zero; for (int i = 0; i < 3; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector3.Lerp(perAxisDirections[i], new Vector3((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); scaleOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.localScale = new Vector3(Mathf.Max(0.1f, originalScale.x + scaleOffset.x), Mathf.Max(0.1f, originalScale.y + scaleOffset.y), Mathf.Max(0.1f, originalScale.z + scaleOffset.z)); }).OnRewind(() => { rectTransform.localScale = originalScale; }).OnComplete((duration) => { rectTransform.localScale = originalScale; }).OnKill(() => { rectTransform.localScale = originalScale; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个尺寸抖动动画，使目标 RectTransform 在指定时间内随机改变尺寸
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="strength">抖动强度（二维向量_Vector2 类型，表示每个轴的最大尺寸变化）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="fadeOut">是否在动画结束时逐渐减弱抖动</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakeSize(this UnityEngine.RectTransform rectTransform, Vector2 strength, float vibrato, float randomness, bool fadeOut, float duration, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector2 originalSize = rectTransform.sizeDelta;

            float elapsed = 0f;
            Vector2[] perAxisDirections = new Vector2[2];
            float[] perAxisSpeeds = new float[2];
            System.Random rand = new System.Random();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnStart(() =>
                {
                    for (int i = 0; i < 2; i++)
                    {
                        perAxisDirections[i] = new Vector2(
                                (float)rand.NextDouble() * 2 - 1,
                                (float)rand.NextDouble() * 2 - 1
                        ).normalized;

                        perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f;
                    }
                })
                        .OnUpdate((size, progress, time) =>
                        {
                            elapsed += Time.deltaTime;
                            float decay = fadeOut ? (1 - progress) : 1f;

                            Vector2 sizeOffset = Vector2.zero;

                            for (int i = 0; i < 2; i++)
                            {
                                if (strength[i] > 0)
                                {
                                    if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i]))
                                    {
                                        elapsed = 0f;
                                        perAxisDirections[i] = Vector2.Lerp(
                                                                                    perAxisDirections[i],
                                                                                    new Vector2(
                                                                                            (float)(rand.NextDouble() * 2 - 1),
                                                                                            (float)(rand.NextDouble() * 2 - 1)
                                                                                    ).normalized,
                                                                                    randomness
                                                                            ).normalized;
                                    }

                                    float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]);

                                    sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i];
                                }
                            }

                            rectTransform.sizeDelta = new Vector2(
                                                                        Mathf.Max(1f, originalSize.x + sizeOffset.x),
                                                                        Mathf.Max(1f, originalSize.y + sizeOffset.y)
                                                                );
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.sizeDelta = originalSize;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.sizeDelta = originalSize;
                        })
                        .OnKill(() =>
                        {
                            rectTransform.sizeDelta = originalSize;
                        })
                        .SetAutokill(autokill);

                return tweener;
            }
            else
            {
                XTween_Interface tweener;

                tweener = new XTween_Specialized_Vector2(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply)
                        .OnStart(() =>
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                perAxisDirections[i] = new Vector2(
                                                        (float)rand.NextDouble() * 2 - 1,
                                                        (float)rand.NextDouble() * 2 - 1
                                                ).normalized;

                                perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f;
                            }
                        })
                        .OnUpdate((size, progress, time) =>
                        {
                            elapsed += Time.deltaTime;
                            float decay = fadeOut ? (1 - progress) : 1f;

                            Vector2 sizeOffset = Vector2.zero;

                            for (int i = 0; i < 2; i++)
                            {
                                if (strength[i] > 0)
                                {
                                    if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i]))
                                    {
                                        elapsed = 0f;
                                        perAxisDirections[i] = Vector2.Lerp(
                                                                perAxisDirections[i],
                                                                new Vector2(
                                                                        (float)(rand.NextDouble() * 2 - 1),
                                                                        (float)(rand.NextDouble() * 2 - 1)
                                                                ).normalized,
                                                                randomness
                                                        ).normalized;
                                    }

                                    float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]);

                                    sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i];
                                }
                            }

                            rectTransform.sizeDelta = new Vector2(
                                                    Mathf.Max(1f, originalSize.x + sizeOffset.x),
                                                    Mathf.Max(1f, originalSize.y + sizeOffset.y)
                                            );
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.sizeDelta = originalSize;
                        })
                        .OnComplete((duration) =>
                        {
                            rectTransform.sizeDelta = originalSize;
                        })
                        .OnKill(() =>
                        {
                            rectTransform.sizeDelta = originalSize;
                        })
                        .SetAutokill(false);

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个尺寸抖动动画，使目标 RectTransform 在指定时间内随机改变尺寸
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="strength">抖动强度（二维向量_Vector2 类型，表示每个轴的最大尺寸变化）</param>
        /// <param name="vibrato">每秒的抖动频率</param>
        /// <param name="randomness">抖动的随机性（0 到 1 之间，值越大抖动越不规则）</param>
        /// <param name="fadeOut">是否在动画结束时逐渐减弱抖动</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 false</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_ShakeSize(this UnityEngine.RectTransform rectTransform, Vector2 strength, float vibrato, float randomness, bool fadeOut, float duration, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector2 originalSize = rectTransform.sizeDelta;

            float elapsed = 0f;
            Vector2[] perAxisDirections = new Vector2[2];
            float[] perAxisSpeeds = new float[2];
            System.Random rand = new System.Random();

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetEase(easeMode).SetAutokill(autokill);
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
                        tweener = new XTween_Specialized_Vector2(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(Vector2.zero, Vector2.zero, duration * XTween_Dashboard.DurationMultiply).OnStart(() => { for (int i = 0; i < 2; i++) { perAxisDirections[i] = new Vector2((float)rand.NextDouble() * 2 - 1, (float)rand.NextDouble() * 2 - 1).normalized; perAxisSpeeds[i] = 0.5f + (float)rand.NextDouble() * 0.5f; } }).OnUpdate((size, progress, time) => { elapsed += Time.deltaTime; float decay = fadeOut ? (1 - progress) : 1f; Vector2 sizeOffset = Vector2.zero; for (int i = 0; i < 2; i++) { if (strength[i] > 0) { if (elapsed >= (duration * XTween_Dashboard.DurationMultiply) / (vibrato * perAxisSpeeds[i])) { elapsed = 0f; perAxisDirections[i] = Vector2.Lerp(perAxisDirections[i], new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1)).normalized, randomness).normalized; } float wave = Mathf.Sin(time * vibrato * Mathf.PI * 2 * perAxisSpeeds[i]); sizeOffset[i] = strength[i] * wave * decay * perAxisDirections[i][i]; } } rectTransform.sizeDelta = new Vector2(Mathf.Max(1f, originalSize.x + sizeOffset.x), Mathf.Max(1f, originalSize.y + sizeOffset.y)); }).OnRewind(() => { rectTransform.sizeDelta = originalSize; }).OnComplete((duration) => { rectTransform.sizeDelta = originalSize; }).OnKill(() => { rectTransform.sizeDelta = originalSize; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}
