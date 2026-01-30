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
        /// 创建一个从当前位置到目标位置的动画
        /// 支持相对移动和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标位置</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对移动</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AnchoredPosition_To(this UnityEngine.RectTransform rectTransform, Vector2 endValue, float duration, bool isRelative = false, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector2 currentPosition = rectTransform.anchoredPosition;
            Vector2 targetPos = isRelative ? XTween_Utilitys.CalculateRelativePosition(rectTransform, currentPosition, endValue) : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((pos, linearProgress, time) =>
                {
                    rectTransform.anchoredPosition = pos;
                })
                        .OnRewind(() =>
                        {
                            rectTransform.anchoredPosition = currentPosition;
                        })
                        .SetAutokill(autokill)
                        .SetRelative(isRelative);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((pos, linearProgress, time) =>
                        {
                            rectTransform.anchoredPosition = pos;
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.anchoredPosition = currentPosition;
                        })
                        .SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前位置到目标位置的动画
        /// 支持相对移动和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标位置</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对移动</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AnchoredPosition_To(this UnityEngine.RectTransform rectTransform, Vector2 endValue, float duration, bool isRelative = false, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector2> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector2 currentPosition = rectTransform.anchoredPosition;
            Vector2 targetPos = isRelative ? XTween_Utilitys.CalculateRelativePosition(rectTransform, currentPosition, endValue) : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply);
                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
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
                        tweener = new XTween_Specialized_Vector2(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition = pos; }).OnRewind(() => { rectTransform.anchoredPosition = currentPosition; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前 3D 位置到目标 3D 位置的动画
        /// 支持相对移动和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标 3D 位置_Position</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对移动</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AnchoredPosition3D_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative = false, bool autokill = false)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentPosition = rectTransform.anchoredPosition3D;
            Vector3 targetPos = isRelative ? XTween_Utilitys.CalculateRelativePosition(rectTransform, currentPosition, endValue) : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((pos, linearProgress, time) =>
                {
                    rectTransform.anchoredPosition3D = pos;
                })
                        .OnRewind(() =>
                        {
                            rectTransform.anchoredPosition3D = currentPosition;
                        })
                        .SetAutokill(autokill)
                        .SetRelative(isRelative);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((pos, linearProgress, time) =>
                        {
                            rectTransform.anchoredPosition3D = pos;
                        })
                        .OnRewind(() =>
                        {
                            rectTransform.anchoredPosition3D = currentPosition;
                        })
                        .SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前 3D 位置到目标 3D 位置的动画
        /// 支持相对移动和自动销毁
        /// </summary>
        /// <param name="rectTransform">目标 RectTransform 组件</param>
        /// <param name="endValue">目标 3D 位置_Position</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="isRelative">是否为相对移动</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_AnchoredPosition3D_To(this UnityEngine.RectTransform rectTransform, Vector3 endValue, float duration, bool isRelative = false, bool autokill = false, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector3> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            if (rectTransform == null)
            {
                Debug.LogError("RectTransform is null!");
                return null;
            }

            Vector3 currentPosition = rectTransform.anchoredPosition3D;
            Vector3 targetPos = isRelative ? XTween_Utilitys.CalculateRelativePosition(rectTransform, currentPosition, endValue) : endValue;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector3 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(curve).SetAutokill(autokill).SetRelative(isRelative);
                    }
                    else
                    {
                        tweener.OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(easeMode).SetAutokill(autokill).SetRelative(isRelative);
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
                        tweener = new XTween_Specialized_Vector3(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(currentPosition, targetPos, duration * XTween_Dashboard.DurationMultiply).OnUpdate((pos, linearProgress, time) => { rectTransform.anchoredPosition3D = pos; }).OnRewind(() => { rectTransform.anchoredPosition3D = currentPosition; }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}