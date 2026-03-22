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
        /// 创建一个从当前拖尾时长到目标拖尾时长的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾时长</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_Time_To(this TrailRenderer trail, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.time;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.time = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.time = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.time = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.time = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.time = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.time = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾时长到目标拖尾时长的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾时长</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_Time_To(this TrailRenderer trail, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.time;

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
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
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
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.time = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.time = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾平滑度到目标拖尾平滑度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾平滑度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_MinVertexDistance_To(this TrailRenderer trail, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.minVertexDistance;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.minVertexDistance = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.minVertexDistance = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.minVertexDistance = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, minVertexDistance) =>
                {
                    if (trail == null)
                        return;
                    trail.minVertexDistance = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.minVertexDistance = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.minVertexDistance = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾平滑度到目标拖尾平滑度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾平滑度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_MinVertexDistance_To(this TrailRenderer trail, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.minVertexDistance;

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
                        tweener.OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
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
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, minVertexDistance) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.minVertexDistance = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾贴图缩放值到目标拖尾贴图缩放值的动画
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾贴图缩放值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_TextureScale_To(this TrailRenderer trail, Vector2 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            Vector2 start = trail.textureScale;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((size, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.textureScale = size;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.textureScale = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.textureScale = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((size, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.textureScale = size;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.textureScale = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.textureScale = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾贴图缩放值到目标拖尾贴图缩放值的动画
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾贴图缩放值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_TextureScale_To(this TrailRenderer trail, Vector2 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector2> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError(" TrailRenderer component is null!");
                return null;
            }

            Vector2 start = trail.textureScale;

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
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
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
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.textureScale = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾起始宽度到目标拖尾起始宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾起始宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_StartWidth_To(this TrailRenderer trail, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.startWidth;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.startWidth = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.startWidth = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.startWidth = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                {
                    if (trail == null)
                        return;
                    trail.startWidth = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.startWidth = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.startWidth = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾起始宽度到目标拖尾起始宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾起始宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_StartWidth_To(this TrailRenderer trail, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.startWidth;

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
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
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
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, startWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startWidth = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾结束宽度到目标拖尾结束宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾结束宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_EndWidth_To(this TrailRenderer trail, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.endWidth;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.endWidth = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.endWidth = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.endWidth = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                {
                    if (trail == null)
                        return;
                    trail.endWidth = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.endWidth = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.endWidth = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾结束宽度到目标拖尾结束宽度的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾结束宽度</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_EndWidth_To(this TrailRenderer trail, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.endWidth;

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
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
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
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, endWidth) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endWidth = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾宽度倍增值到目标拖尾宽度倍增值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾宽度倍增值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_WidthMultipler_To(this TrailRenderer trail, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.widthMultiplier;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.widthMultiplier = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.widthMultiplier = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.widthMultiplier = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                {
                    if (trail == null)
                        return;
                    trail.widthMultiplier = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.widthMultiplier = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.widthMultiplier = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾宽度倍增值到目标拖尾宽度倍增值的动画
        /// 支持相对变化和自动销毁
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾宽度倍增值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_WidthMultipler_To(this TrailRenderer trail, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            float start = trail.widthMultiplier;

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
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
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
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, widthMultiplier) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.widthMultiplier = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾颜色到目标拖尾颜色的动画
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_StartColor_To(this TrailRenderer trail, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            Color start = trail.startColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.startColor = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.startColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.startColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.startColor = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.startColor = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.startColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾颜色到目标拖尾颜色的动画
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_StartColor_To(this TrailRenderer trail, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            Color start = trail.startColor;

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
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
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
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.startColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾结束颜色到目标拖尾结束颜色的动画
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾结束颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_EndColor_To(this TrailRenderer trail, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            Color start = trail.endColor;

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.endColor = val;
                })
                .OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.endColor = start;
                })
                .OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.endColor = endValue;
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (trail == null)
                        return;
                    trail.endColor = val;
                }).OnRewind(() =>
                {
                    if (trail == null)
                        return;
                    if (rewind_set_startvalue)
                        trail.endColor = start;
                }).OnComplete((duration) =>
                {
                    if (trail == null)
                        return;
                    if (complete_set_endvalue)
                        trail.endColor = endValue;
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前拖尾结束颜色到目标拖尾结束颜色的动画
        /// </summary>
        /// <param name="trail">目标 TrailRenderer 组件</param>
        /// <param name="endValue">目标拖尾结束颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Trail_EndColor_To(this TrailRenderer trail, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (trail == null)
            {
                Debug.LogError("TrailRenderer component is null!");
                return null;
            }

            Color start = trail.endColor;

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
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
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
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = val;
                        }).OnRewind(() =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = start;
                        }).OnComplete((duration) =>
                        {
                            if (trail == null)
                                return;
                            trail.endColor = endValue;
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
