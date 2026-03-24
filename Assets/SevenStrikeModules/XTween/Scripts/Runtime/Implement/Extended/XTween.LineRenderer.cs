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
        /// 创建一个从当前线渲染器平滑度到目标线渲染器平滑度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器平滑度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_CornerVertices_To(this LineRenderer line, int endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            int start = line.numCornerVertices;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.numCornerVertices = val;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.numCornerVertices = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.numCornerVertices = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, numCornerVertices) =>
                {
                    if (line == null)
                        return;
                    line.numCornerVertices = val;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.numCornerVertices = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.numCornerVertices = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器平滑度到目标线渲染器平滑度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器平滑度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_CornerVertices_To(this LineRenderer line, int endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<int> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            int start = line.numCornerVertices;

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
                        tweener.OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
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
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, numCornerVertices) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.numCornerVertices = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }

#if UNITY_2022_3_OR_NEWER
        /// <summary>
        /// 创建一个从当前线渲染器贴图缩放值到目标线渲染器贴图缩放值的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器贴图缩放值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_TextureScale_To(this LineRenderer line, Vector2 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Vector2 start = line.textureScale;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.textureScale = size;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.textureScale = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.textureScale = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.textureScale = size;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.textureScale = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.textureScale = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器贴图缩放值到目标线渲染器贴图缩放值的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器贴图缩放值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_TextureScale_To(this LineRenderer line, Vector2 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector2> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError(" LineRenderer component is null!");
                return null;
            }

            Vector2 start = line.textureScale;

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
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
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
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.textureScale = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
#endif

        /// <summary>
        /// 创建一个从当前线渲染器起始宽度到目标线渲染器起始宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器起始宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_StartWidth_To(this LineRenderer line, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            float start = line.startWidth;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.startWidth = val;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.startWidth = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.startWidth = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                {
                    if (line == null)
                        return;
                    line.startWidth = val;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.startWidth = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.startWidth = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器起始宽度到目标线渲染器起始宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器起始宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_StartWidth_To(this LineRenderer line, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            float start = line.startWidth;

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
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
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
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startWidth = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器结束宽度到目标线渲染器结束宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器结束宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_EndWidth_To(this LineRenderer line, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            float start = line.endWidth;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.endWidth = val;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.endWidth = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.endWidth = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                {
                    if (line == null)
                        return;
                    line.endWidth = val;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.endWidth = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.endWidth = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器结束宽度到目标线渲染器结束宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器结束宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_EndWidth_To(this LineRenderer line, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            float start = line.endWidth;

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
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
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
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endWidth = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器宽度倍增值到目标线渲染器宽度倍增值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器宽度倍增值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_WidthMultipler_To(this LineRenderer line, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            float start = line.widthMultiplier;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.widthMultiplier = val;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.widthMultiplier = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.widthMultiplier = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                {
                    if (line == null)
                        return;
                    line.widthMultiplier = val;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.widthMultiplier = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.widthMultiplier = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器宽度倍增值到目标线渲染器宽度倍增值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器宽度倍增值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_WidthMultipler_To(this LineRenderer line, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            float start = line.widthMultiplier;

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
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
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
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.widthMultiplier = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器指定点的坐标到目标线渲染器指定点的坐标的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器指定点的坐标</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_PointPosition_To(this LineRenderer line, int index, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Vector3 start = line.GetPosition(index);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.SetPosition(index, val);
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.SetPosition(index, start);
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.SetPosition(index, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.SetPosition(index, val);
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.SetPosition(index, start);
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.SetPosition(index, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器指定点的坐标到目标线渲染器指定点的坐标的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器指定点的坐标</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_PointPosition_To(this LineRenderer line, int index, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Vector2 start = line.GetPosition(index);

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
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
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
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, val);
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, start);
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.SetPosition(index, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器颜色到目标线渲染器颜色的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_StartColor_To(this LineRenderer line, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Color start = line.startColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.startColor = val;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.startColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.startColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.startColor = val;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.startColor = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.startColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器颜色到目标线渲染器颜色的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_StartColor_To(this LineRenderer line, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Color start = line.startColor;

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
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
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
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.startColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器结束颜色到目标线渲染器结束颜色的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器结束颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_EndColor_To(this LineRenderer line, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Color start = line.endColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.endColor = val;
                })
                .OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.endColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.endColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (line == null)
                        return;
                    line.endColor = val;
                }).OnRewind(() =>
                {
                    if (line == null)
                        return;
                    if (rewind_set_startvalue)
                        line.endColor = start;
                }).OnComplete((duration) =>
                {
                    if (line == null)
                        return;
                    if (complete_set_endvalue)
                        line.endColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前线渲染器结束颜色到目标线渲染器结束颜色的动画
        /// </summary>
        /// <param name="line">目标 LineRenderer 组件</param>
        /// <param name="endValue">目标线渲染器结束颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Line_EndColor_To(this LineRenderer line, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (line == null)
            {
                Debug.LogError("LineRenderer component is null!");
                return null;
            }

            Color start = line.endColor;

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
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
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
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (line == null)
                                return;
                            line.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (line == null)
                                return;
                            line.endColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
