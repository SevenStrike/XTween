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

    /// <summary>
    /// 缓动模式
    /// </summary>
    public enum EaseMode
    {
        /// <summary>
        /// 线性缓动（无缓动）
        /// </summary>
        Linear = 0,
        /// <summary>
        /// 正弦曲线缓动进入
        /// </summary>
        InSine = 1,
        /// <summary>
        /// 正弦曲线缓动退出
        /// </summary>
        OutSine = 2,
        /// <summary>
        /// 正弦曲线缓动进入然后退出
        /// </summary>
        InOutSine = 3,
        /// <summary>
        /// 二次曲线缓动进入
        /// </summary>
        InQuad = 4,
        /// <summary>
        /// 二次曲线缓动退出
        /// </summary>
        OutQuad = 5,
        /// <summary>
        /// 二次曲线缓动进入然后退出
        /// </summary>
        InOutQuad = 6,
        /// <summary>
        /// 三次曲线缓动进入
        /// </summary>
        InCubic = 7,
        /// <summary>
        /// 三次曲线缓动退出
        /// </summary>
        OutCubic = 8,
        /// <summary>
        /// 三次曲线缓动进入然后退出
        /// </summary>
        InOutCubic = 9,
        /// <summary>
        /// 四次曲线缓动进入
        /// </summary>
        InQuart = 10,
        /// <summary>
        /// 四次曲线缓动退出
        /// </summary>
        OutQuart = 11,
        /// <summary>
        /// 四次曲线缓动进入然后退出
        /// </summary>
        InOutQuart = 12,
        /// <summary>
        /// 五次曲线缓动进入
        /// </summary>
        InQuint = 13,
        /// <summary>
        /// 五次曲线缓动退出
        /// </summary>
        OutQuint = 14,
        /// <summary>
        /// 五次曲线缓动进入然后退出
        /// </summary>
        InOutQuint = 15,
        /// <summary>
        /// 指数曲线缓动进入
        /// </summary>
        InExpo = 16,
        /// <summary>
        /// 指数曲线缓动退出
        /// </summary>
        OutExpo = 17,
        /// <summary>
        /// 指数曲线缓动进入然后退出
        /// </summary>
        InOutExpo = 18,
        /// <summary>
        /// 圆形曲线缓动进入
        /// </summary>
        InCirc = 19,
        /// <summary>
        /// 圆形曲线缓动退出
        /// </summary>
        OutCirc = 20,
        /// <summary>
        /// 圆形曲线缓动进入然后退出
        /// </summary>
        InOutCirc = 21,
        /// <summary>
        /// 弹性曲线缓动进入
        /// </summary>
        InElastic = 22,
        /// <summary>
        /// 弹性曲线缓动退出
        /// </summary>
        OutElastic = 23,
        /// <summary>
        /// 弹性曲线缓动进入然后退出
        /// </summary>
        InOutElastic = 24,
        /// <summary>
        /// 回退曲线缓动进入
        /// </summary>
        InBack = 25,
        /// <summary>
        /// 回退曲线缓动退出
        /// </summary>
        OutBack = 26,
        /// <summary>
        /// 回退曲线缓动进入然后退出
        /// </summary>
        InOutBack = 27,
        /// <summary>
        /// 弹跳曲线缓动进入
        /// </summary>
        InBounce = 28,
        /// <summary>
        /// 弹跳曲线缓动退出
        /// </summary>
        OutBounce = 29,
        /// <summary>
        /// 弹跳曲线缓动进入然后退出
        /// </summary>
        InOutBounce = 30,
    }

    /// <summary>
    /// 提供缓动函数的实现，用于动画的非线性过渡效果
    /// </summary>
    /// <remarks>
    /// 本类包含多种缓动模式（Ease Modes），如线性、弹性、回退等，用于模拟不同的动画效果
    /// 每种缓动模式通过数学公式实现，确保动画的平滑性和自然感
    /// 设计要点：
    /// - 提供丰富的缓动模式，满足不同动画需求
    /// - 使用静态方法，便于在任何地方调用
    /// - 通过缓存和优化，提高性能
    /// </remarks>
    public static class XTween_EaseLibrary
    {
        /// <summary>
        /// 定义圆周率π的常量值，用于后续的数学计算
        /// </summary>
        /// <remarks>
        /// 使用 Mathf.PI 确保获取到精确的π值，避免手动输入可能带来的误差
        /// 这是一个基本的数学常量，广泛用于三角函数和其他几何计算中
        /// </remarks>
        const float PI = Mathf.PI;
        /// <summary>
        /// 定义π的一半（π/2）的常量值
        /// </summary>
        /// <remarks>
        /// 在三角函数等数学计算中，π/2 是一个常用的值，例如正弦函数在 π/2 处取得最大值 1
        /// 将其定义为常量，方便在代码中多次使用，避免重复计算，提高代码的可读性和可维护性
        /// </remarks>
        const float PI_OVER_2 = PI * 0.5f;
        /// <summary>
        /// 定义2π的常量值
        /// </summary>
        /// <remarks>
        /// 在涉及圆周运动或周期性变化的计算中，2π 是一个重要的值，例如一个完整的圆周运动的角度变化范围是 0 到 2π
        /// 定义为常量后，便于在相关计算中直接使用，减少硬编码，增强代码的通用性和可移植性
        /// </remarks>
        const float TWO_PI = PI * 2;
        /// <summary>
        /// 定义默认的回退系数，用于回退动画效果
        /// </summary>
        /// <remarks>
        /// 回退动画是一种常见的缓动效果，物体先超过目标位置，然后再回退到目标位置
        /// 1.70158f 是一个经验值，经过测试和调整后，能够产生较为自然和美观的回退效果
        /// 将其定义为常量，方便在回退动画的计算中统一使用，保证动画效果的一致性
        /// </remarks>
        const float DEFAULT_OVERSHOOT = 1.70158f;
        /// <summary>
        /// 定义默认的弹性周期，用于弹性动画效果
        /// </summary>
        /// <remarks>
        /// 弹性动画是一种模拟弹簧振动的动画效果，物体在接近目标位置时会像弹簧一样来回振动几次，最终稳定在目标位置
        /// 0.3f 是一个经验值，表示振动的周期长度，能够使弹性动画看起来更加真实和自然
        /// 定义为常量后，便于在弹性动画的计算中直接引用，减少代码冗余，提高可读性和可维护性
        /// </remarks>
        const float DEFAULT_ELASTIC_PERIOD = 0.3f;
        /// <summary>
        /// 定义一个静态只读数组，用于缓存2的幂次方计算结果
        /// </summary>
        /// <remarks>
        /// 在动画计算中，尤其是缓动曲线的计算，可能会频繁地进行幂次方运算
        /// 通过缓存2的幂次方结果，可以避免重复计算，提高程序的性能
        /// 数组大小为1024，这是一个经验值，根据实际需求和性能考虑确定
        /// 使用静态只读数组，所有实例共享同一个数组，节省内存空间，同时保证数组的安全性
        /// </remarks>
        private static readonly float[] _powCache = new float[1024];

        /// <summary>
        /// 静态构造函数，初始化幂次方缓存数组
        /// </summary>
        static XTween_EaseLibrary()
        {
            // 初始化幂次缓存
            for (int i = 0; i < _powCache.Length; i++)
            {
                _powCache[i] = Mathf.Pow(2, 10 * (i / (float)_powCache.Length - 1));
            }
        }
        /// <summary>
        /// 根据缓动模式和时间参数计算缓动值
        /// </summary>
        /// <param name="ease">缓动模式</param>
        /// <param name="time">当前时间</param>
        /// <param name="duration">动画总时长</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 使用 switch 语句，便于扩展和维护
        /// - 提供线性、正弦、二次、三次、四次、五次、指数、圆形、弹性、回退、弹跳等多种缓动模式
        /// - 对于复杂的缓动模式，如弹性、回退等，使用默认参数确保一致性
        /// </remarks>
        public static float EvaluateUncached(EaseMode ease, float time, float duration)
        {
            float t = Mathf.Clamp01(time / duration);

            switch (ease)
            {
                case EaseMode.Linear:
                    return t;

                case EaseMode.InSine:
                    return 1f - Mathf.Cos(t * PI_OVER_2);

                case EaseMode.OutSine:
                    return Mathf.Sin(t * PI_OVER_2);

                case EaseMode.InOutSine:
                    return -0.5f * (Mathf.Cos(PI * t) - 1f);

                case EaseMode.InQuad:
                    return t * t;

                case EaseMode.OutQuad:
                    return t * (2f - t);

                case EaseMode.InOutQuad:
                    return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;

                case EaseMode.InCubic:
                    return t * t * t;

                case EaseMode.OutCubic:
                    return (--t) * t * t + 1f;

                case EaseMode.InOutCubic:
                    return t < 0.5f ? 4f * t * t * t : (t - 1f) * (2f * t - 2f) * (2f * t - 2f) + 1f;

                case EaseMode.InQuart:
                    return t * t * t * t;

                case EaseMode.OutQuart:
                    return 1f - (--t) * t * t * t;

                case EaseMode.InOutQuart:
                    return t < 0.5f ? 8f * t * t * t * t : 1f - 8f * (--t) * t * t * t;

                case EaseMode.InQuint:
                    return t * t * t * t * t;

                case EaseMode.OutQuint:
                    return 1f + (--t) * t * t * t * t;

                case EaseMode.InOutQuint:
                    return t < 0.5f ? 16f * t * t * t * t * t : 1f + 16f * (--t) * t * t * t * t;

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

                case EaseMode.InCirc:
                    return 1f - Mathf.Sqrt(1f - t * t);

                case EaseMode.OutCirc:
                    return Mathf.Sqrt(1f - (--t) * t);

                case EaseMode.InOutCirc:
                    return t < 0.5f
                        ? 0.5f * (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2)))
                        : 0.5f * (Mathf.Sqrt(1f - Mathf.Pow(2f * (t - 1f), 2)) + 1f);

                case EaseMode.InElastic:
                    return ElasticEaseIn(t, DEFAULT_ELASTIC_PERIOD);

                case EaseMode.OutElastic:
                    return ElasticEaseOut(t, DEFAULT_ELASTIC_PERIOD);

                case EaseMode.InOutElastic:
                    return ElasticEaseInOut(t, DEFAULT_ELASTIC_PERIOD);

                case EaseMode.InBack:
                    return BackEaseIn(t, DEFAULT_OVERSHOOT);

                case EaseMode.OutBack:
                    return BackEaseOut(t, DEFAULT_OVERSHOOT);

                case EaseMode.InOutBack:
                    return BackEaseInOut(t, DEFAULT_OVERSHOOT * 1.525f);

                case EaseMode.InBounce:
                    return BounceEaseIn(t);

                case EaseMode.OutBounce:
                    return BounceEaseOut(t);

                case EaseMode.InOutBounce:
                    return BounceEaseInOut(t);

                default:
                    return t;
            }
        }
        /// <summary>
        /// 计算缓动函数的值
        /// 如果启用了缓存，则从缓存表中获取值；否则直接调用未缓存的缓动函数
        /// </summary>
        /// <param name="ease">缓动模式</param>
        /// <param name="time">当前时间，范围为 [0, duration]</param>
        /// <param name="duration">动画总时长</param>
        /// <returns>缓动后的值，范围为 [0, 1]</returns>
        public static float Evaluate(EaseMode ease, float time, float duration)
        {
            float t = Mathf.Clamp01(time / duration);
            return XTween_EaseCache.Evaluate(ease, t);
        }

        #region 复杂计算方式
        /// <summary>
        /// 弹性进入缓动函数
        /// </summary>
        /// <param name="t">时间参数</param>
        /// <param name="period">周期</param>
        /// <returns>缓动值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 使用正弦函数模拟弹性效果
        /// - 通过周期参数控制弹性振动的频率
        /// </remarks>
        private static float ElasticEaseIn(float t, float period)
        {
            if (t == 0f) return 0f;
            if (t == 1f) return 1f;

            float s = period / TWO_PI * Mathf.Asin(1f);
            return -Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - s) * TWO_PI / period);
        }
        /// <summary>
        /// 弹性退出缓动函数
        /// </summary>
        /// <param name="t">时间参数</param>
        /// <param name="period">周期</param>
        /// <returns>缓动值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 使用正弦函数模拟弹性效果
        /// - 通过周期参数控制弹性振动的频率
        /// </remarks>
        private static float ElasticEaseOut(float t, float period)
        {
            if (t == 0f) return 0f;
            if (t == 1f) return 1f;

            float s = period / TWO_PI * Mathf.Asin(1f);
            return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s) * TWO_PI / period) + 1f;
        }
        /// <summary>
        /// 弹性进出缓动函数，模拟物体在进入和退出时的弹性振动效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <param name="period">弹性振动的周期</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 结合了弹性进入和退出的效果，适用于需要在动画开始和结束时都添加弹性效果的场景
        /// - 使用正弦函数和指数衰减来模拟弹性振动，确保动画的平滑性和自然感
        /// - 通过周期参数控制振动的频率，使动画效果更加灵活
        /// </remarks>
        private static float ElasticEaseInOut(float t, float period)
        {
            if (t == 0f) return 0f;
            if (t == 1f) return 1f;

            t *= 2f;
            float s = period / TWO_PI * Mathf.Asin(1f);

            if (t < 1f)
                return -0.5f * Mathf.Pow(2f, 10f * (t -= 1f)) * Mathf.Sin((t - s) * TWO_PI / period);

            return 0.5f * Mathf.Pow(2f, -10f * (t -= 1f)) * Mathf.Sin((t - s) * TWO_PI / period) + 1f;
        }
        /// <summary>
        /// 回退进入缓动函数，模拟物体在进入时的回退效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <param name="overshoot">回退系数，控制回退的强度</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 模拟物体在接近目标位置时的回退效果，使动画更具动态感
        /// - 使用三次多项式函数来实现回退效果，确保动画的平滑性
        /// - 通过回退系数控制回退的强度，使动画效果更加灵活
        /// </remarks>
        private static float BackEaseIn(float t, float overshoot)
        {
            return t * t * ((overshoot + 1f) * t - overshoot);
        }
        /// <summary>
        /// 回退退出缓动函数，模拟物体在退出时的回退效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <param name="overshoot">回退系数，控制回退的强度</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 模拟物体在离开目标位置时的回退效果，使动画更具动态感
        /// - 使用三次多项式函数来实现回退效果，确保动画的平滑性
        /// - 通过回退系数控制回退的强度，使动画效果更加灵活
        /// </remarks>
        private static float BackEaseOut(float t, float overshoot)
        {
            return (t -= 1f) * t * ((overshoot + 1f) * t + overshoot) + 1f;
        }
        /// <summary>
        /// 回退进出缓动函数，模拟物体在进入和退出时的回退效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <param name="overshoot">回退系数，控制回退的强度</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 结合了回退进入和退出的效果，适用于需要在动画开始和结束时都添加回退效果的场景
        /// - 使用三次多项式函数来实现回退效果，确保动画的平滑性
        /// - 通过回退系数控制回退的强度，使动画效果更加灵活
        /// </remarks>
        private static float BackEaseInOut(float t, float overshoot)
        {
            t *= 2f;
            if (t < 1f)
                return 0.5f * t * t * ((overshoot + 1f) * t - overshoot);

            return 0.5f * ((t -= 2f) * t * ((overshoot + 1f) * t + overshoot) + 2f);
        }
        /// <summary>
        /// 弹跳进入缓动函数，模拟物体在进入时的弹跳效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 模拟物体在接近目标位置时的弹跳效果，使动画更具趣味性
        /// - 使用分段函数来实现弹跳效果，确保动画的平滑性和自然感
        /// - 通过多个阶段的弹跳，使动画效果更加生动
        /// </remarks>
        private static float BounceEaseIn(float t)
        {
            return 1f - BounceEaseOut(1f - t);
        }
        /// <summary>
        /// 弹跳退出缓动函数，模拟物体在退出时的弹跳效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 模拟物体在离开目标位置时的弹跳效果，使动画更具趣味性
        /// - 使用分段函数来实现弹跳效果，确保动画的平滑性和自然感
        /// - 通过多个阶段的弹跳，使动画效果更加生动
        /// </remarks>
        private static float BounceEaseOut(float t)
        {
            if (t < (1f / 2.75f))
                return 7.5625f * t * t;

            if (t < (2f / 2.75f))
                return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;

            if (t < (2.5f / 2.75f))
                return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;

            return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
        }
        /// <summary>
        /// 弹跳进出缓动函数，模拟物体在进入和退出时的弹跳效果
        /// </summary>
        /// <param name="t">归一化时间参数（0 到 1）</param>
        /// <returns>缓动后的值</returns>
        /// <remarks>
        /// 设计要点：
        /// - 结合了弹跳进入和退出的效果，适用于需要在动画开始和结束时都添加弹跳效果的场景
        /// - 使用分段函数来实现弹跳效果，确保动画的平滑性和自然感
        /// - 通过多个阶段的弹跳，使动画效果更加生动
        /// </remarks>
        private static float BounceEaseInOut(float t)
        {
            if (t < 0.5f)
                return BounceEaseIn(t * 2f) * 0.5f;

            return BounceEaseOut(t * 2f - 1f) * 0.5f + 0.5f;
        }
        #endregion
    }
}