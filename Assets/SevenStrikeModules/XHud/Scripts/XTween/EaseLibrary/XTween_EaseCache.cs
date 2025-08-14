namespace SevenStrikeModules.XTween
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// 缓动函数缓存类
    /// 提供缓动函数的缓存功能，以提高性能
    /// </summary>
    public static class XTween_EaseCache
    {
        /// <summary>
        /// 缓存的分辨率
        /// 决定了缓存的精度，值越高，缓存越精确，但占用内存也越多
        /// </summary>
        private const int Resolution = 4096;
        /// <summary>
        /// 缓存表
        /// 用于存储每种缓动模式的预计算值
        /// </summary>
        private static readonly float[][] Tables;
        /// <summary>
        /// 是否启用缓存
        /// 如果为 false，则直接调用未缓存的缓动函数
        /// </summary>
        public static bool UseCache = true;
        /// <summary>
        /// 默认的回退系数
        /// 用于回退动画效果，控制回退的强度
        /// 值越大，回退效果越明显
        /// </summary>
        const float DEFAULT_OVERSHOOT = 1.70158f;
        /// <summary>
        /// 默认的弹性周期
        /// 用于弹性动画效果，控制弹性振动的周期
        /// 值越小，振动越快；值越大，振动越慢
        /// </summary>
        const float DefaultPeriod = 0.3f;

        /// <summary>
        /// 静态构造函数，初始化缓存表
        /// 预计算所有缓动函数的值并存储到缓存表中
        /// </summary>
        static XTween_EaseCache()
        {
            int easeCount = Enum.GetValues(typeof(EaseMode)).Length;
            Tables = new float[easeCount][];

            // 预计算所有缓动函数
            for (int i = 0; i < easeCount; i++)
            {
                Tables[i] = new float[Resolution];
                var ease = (EaseMode)i;

                for (int j = 0; j < Resolution; j++)
                {
                    float t = j / (float)(Resolution - 1);
                    Tables[i][j] = CalculateOriginalEase(ease, t);
                }
            }
        }
        /// <summary>
        /// 计算原始缓动函数的值
        /// 用于预计算缓存表中的值
        /// </summary>
        /// <param name="ease">缓动模式</param>
        /// <param name="t">时间参数，范围为 [0, 1]</param>
        /// <returns>缓动后的值</returns>
        private static float CalculateOriginalEase(EaseMode ease, float t)
        {
            const float defaultOvershoot = DEFAULT_OVERSHOOT;
            const float defaultPeriod = DefaultPeriod;

            switch (ease)
            {
                // 线性
                case EaseMode.Linear:
                    return t;

                // 二次
                case EaseMode.InQuad:
                    return t * t;
                case EaseMode.OutQuad:
                    return t * (2f - t);
                case EaseMode.InOutQuad:
                    return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;

                // 三次
                case EaseMode.InCubic:
                    return t * t * t;
                case EaseMode.OutCubic:
                    return (--t) * t * t + 1f;
                case EaseMode.InOutCubic:
                    return t < 0.5f ? 4f * t * t * t : (t - 1f) * (2f * t - 2f) * (2f * t - 2f) + 1f;

                // 四次
                case EaseMode.InQuart:
                    return t * t * t * t;
                case EaseMode.OutQuart:
                    return 1f - (--t) * t * t * t;
                case EaseMode.InOutQuart:
                    return t < 0.5f ? 8f * t * t * t * t : 1f - 8f * (--t) * t * t * t;

                // 五次
                case EaseMode.InQuint:
                    return t * t * t * t * t;
                case EaseMode.OutQuint:
                    return 1f + (--t) * t * t * t * t;
                case EaseMode.InOutQuint:
                    return t < 0.5f ? 16f * t * t * t * t * t : 1f + 16f * (--t) * t * t * t * t;

                // 正弦
                case EaseMode.InSine:
                    return 1f - Mathf.Cos(t * Mathf.PI / 2f);
                case EaseMode.OutSine:
                    return Mathf.Sin(t * Mathf.PI / 2f);
                case EaseMode.InOutSine:
                    return -0.5f * (Mathf.Cos(Mathf.PI * t) - 1f);

                // 指数
                case EaseMode.InExpo:
                    return t == 0f ? 0f : Mathf.Pow(2f, 10f * (t - 1f));
                case EaseMode.OutExpo:
                    return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
                case EaseMode.InOutExpo:
                    if (t == 0f) return 0f;
                    if (t == 1f) return 1f;
                    return t < 0.5f
                        ? 0.5f * Mathf.Pow(2f, 20f * t - 10f)
                        : 1f - 0.5f * Mathf.Pow(2f, -20f * t + 10f);

                // 圆形
                case EaseMode.InCirc:
                    return 1f - Mathf.Sqrt(1f - t * t);
                case EaseMode.OutCirc:
                    return Mathf.Sqrt(1f - (--t) * t);
                case EaseMode.InOutCirc:
                    return t < 0.5f
                        ? 0.5f * (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2)))
                        : 0.5f * (Mathf.Sqrt(1f - Mathf.Pow(2f * (t - 1f), 2)) + 1f);

                // 弹性
                case EaseMode.InElastic:
                    if (t <= 0) return 0;
                    if (t >= 1) return 1;
                    float s1 = defaultPeriod / (2f * Mathf.PI) * Mathf.Asin(1f);
                    return -Mathf.Pow(2f, 10f * (t - 1)) * Mathf.Sin((t - 1 - s1) * (2f * Mathf.PI) / defaultPeriod);
                case EaseMode.OutElastic:
                    if (t <= 0) return 0;
                    if (t >= 1) return 1;
                    float s2 = defaultPeriod / (2f * Mathf.PI) * Mathf.Asin(1f);
                    return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s2) * (2f * Mathf.PI) / defaultPeriod) + 1f;
                case EaseMode.InOutElastic:
                    if (t <= 0) return 0;
                    if (t >= 1) return 1;
                    t *= 2f;
                    float s3 = defaultPeriod / (2f * Mathf.PI) * Mathf.Asin(1f);
                    if (t < 1f)
                        return -0.5f * Mathf.Pow(2f, 10f * (t - 1)) * Mathf.Sin((t - 1 - s3) * (2f * Mathf.PI) / defaultPeriod);
                    return 0.5f * Mathf.Pow(2f, -10f * (t - 1)) * Mathf.Sin((t - 1 - s3) * (2f * Mathf.PI) / defaultPeriod) + 1f;

                // 回弹
                case EaseMode.InBack:
                    return t * t * ((defaultOvershoot + 1f) * t - defaultOvershoot);
                case EaseMode.OutBack:
                    return (t -= 1f) * t * ((defaultOvershoot + 1f) * t + defaultOvershoot) + 1f;
                case EaseMode.InOutBack:
                    t *= 2f;
                    float overshoot = defaultOvershoot * 1.525f;
                    if (t < 1f)
                        return 0.5f * t * t * ((overshoot + 1f) * t - overshoot);
                    return 0.5f * ((t -= 2f) * t * ((overshoot + 1f) * t + overshoot) + 2f);

                // 弹跳
                case EaseMode.InBounce:
                    return 1f - BounceEaseOut(1f - t);
                case EaseMode.OutBounce:
                    return BounceEaseOut(t);
                case EaseMode.InOutBounce:
                    return t < 0.5f
                        ? BounceEaseIn(t * 2f) * 0.5f
                        : BounceEaseOut(t * 2f - 1f) * 0.5f + 0.5f;

                default:
                    return t;
            }
        }
        /// <summary>
        /// 弹性缓动的退出函数
        /// 用于计算弹跳效果
        /// </summary>
        /// <param name="t">时间参数</param>
        /// <returns>缓动后的值</returns>
        private static float BounceEaseOut(float t)
        {
            if (t < (1f / 2.75f))
                return 7.5625f * t * t;
            else if (t < (2f / 2.75f))
                return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
            else if (t < (2.5f / 2.75f))
                return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
            else
                return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
        }
        /// <summary>
        /// 弹性缓动的进入函数
        /// 用于计算弹跳效果
        /// </summary>
        /// <param name="t">时间参数</param>
        /// <returns>缓动后的值</returns>
        private static float BounceEaseIn(float t)
        {
            return 1f - BounceEaseOut(1f - t);
        }

        /// <summary>
        /// 计算缓动函数的值
        /// 如果启用了缓存，则从缓存表中获取值；否则直接调用未缓存的缓动函数
        /// </summary>
        /// <param name="ease">缓动模式</param>
        /// <param name="t">时间参数，范围为 [0, 1]</param>
        /// <returns>缓动后的值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Evaluate(EaseMode ease, float t)
        {
            if (!UseCache)
            {
                return XTween_EaseLibrary.EvaluateUncached(ease, t, 1f);
            }

            int easeIndex = (int)ease;
            float[] table = Tables[easeIndex];

            // 计算索引位置
            float position = t * (Resolution - 1);
            int index = (int)position;

            // 边界检查
            if (index >= Resolution - 1) return table[Resolution - 1];
            if (index <= 0) return table[0];

            // 线性插值
            float factor = position - index;
            return table[index] + factor * (table[index + 1] - table[index]);
        }
    }
}