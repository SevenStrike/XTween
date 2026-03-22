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
        /// 创建一个从当前材质颜色到目标材质颜色的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Color_To(this Material mat, string keyname, Color endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Color start = mat.GetColor(keyname);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Color>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetColor(keyname, val);
                })
                .OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetColor(keyname, start);
                })
                .OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetColor(keyname, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetColor(keyname, val);
                }).OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetColor(keyname, start);
                }).OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetColor(keyname, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质颜色到目标材质颜色的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质颜色</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Color_To(this Material mat, string keyname, Color endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Color> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Color start = mat.GetColor(keyname);

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
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
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
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Color(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetColor(keyname, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质整数值到目标材质整数值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质整数值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Int_To(this Material mat, string key, int endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            int start = mat.GetInteger(key);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Int>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetInteger(key, val);
                })
                .OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetInteger(key, start);
                })
                .OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetInteger(key, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetInteger(key, val);
                }).OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetInteger(key, start);
                }).OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetInteger(key, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质整数值到目标材质整数值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质整数值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Int_To(this Material mat, string key, int endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<int> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            int start = mat.GetInteger(key);

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
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
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
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Int(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetInteger(key, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质浮点数值到目标材质浮点数值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质浮点数值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Float_To(this Material mat, string key, float endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            float start = mat.GetFloat(key);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Float>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetFloat(key, val);
                })
                .OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetFloat(key, start);
                })
                .OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetFloat(key, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetFloat(key, val);
                }).OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetFloat(key, start);
                }).OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetFloat(key, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质浮点数值到目标材质浮点数值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质浮点数值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Float_To(this Material mat, string key, float endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<float> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            float start = mat.GetFloat(key);

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
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
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
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Float(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetFloat(key, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质Vector2向量值到目标材质Vector2向量值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质Vector2向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Vector2_To(this Material mat, string key, Vector2 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Vector2 start = mat.GetVector(key);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetVector(key, val);
                })
                .OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetVector(key, start);
                })
                .OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetVector(key, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetVector(key, val);
                }).OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetVector(key, start);
                }).OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetVector(key, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质Vector2向量值到目标材质Vector2向量值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质Vector2向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Vector2_To(this Material mat, string key, Vector2 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector2> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Vector2 start = mat.GetVector(key);

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
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
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
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质Vector3向量值到目标材质Vector3向量值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质Vector3向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Vector3_To(this Material mat, string key, Vector3 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Vector3 start = mat.GetVector(key);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector3>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetVector(key, val);
                })
                .OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetVector(key, start);
                })
                .OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetVector(key, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetVector(key, val);
                }).OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetVector(key, start);
                }).OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetVector(key, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质Vector3向量值到目标材质Vector3向量值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质Vector3向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Vector3_To(this Material mat, string key, Vector3 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector3> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Vector3 start = mat.GetVector(key);

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
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
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
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector3(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质Vector4向量值到目标材质Vector4向量值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质Vector4向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Vector4_To(this Material mat, string key, Vector4 endValue, float duration, bool autokill = false, bool rewind_set_startvalue = true, bool complete_set_endvalue = true)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Vector4 start = mat.GetVector(key);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                tweener.OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetVector(key, val);
                })
                .OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetVector(key, start);
                })
                .OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetVector(key, endValue);
                })
                .SetAutokill(autokill);
                return tweener;
            }
            else
            {
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                {
                    if (mat == null)
                        return;
                    mat.SetVector(key, val);
                }).OnRewind(() =>
                {
                    if (mat == null)
                        return;
                    if (rewind_set_startvalue)
                        mat.SetVector(key, start);
                }).OnComplete((duration) =>
                {
                    if (mat == null)
                        return;
                    if (complete_set_endvalue)
                        mat.SetVector(key, endValue);
                }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前材质Vector4向量值到目标材质Vector4向量值的动画
        /// </summary>
        /// <param name="mat">目标 Material材质</param>
        /// <param name="endValue">目标材质Vector4向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁</param>
        /// <param name="easeMode">缓动模式</param>
        /// <param name="isFromMode">从模式</param>
        /// <param name="fromvalue">起始值</param>
        /// <param name="useCurve">使用曲线</param>
        /// <param name="curve">曲线</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface xt_Material_Vector4_To(this Material mat, string key, Vector4 endValue, float duration, bool autokill, EaseMode easeMode, bool isFromMode, XTween_Getter<Vector4> fromvalue, bool useCurve, AnimationCurve curve)
        {
            if (mat == null)
            {
                Debug.LogError("Material component is null!");
                return null;
            }

            Vector4 start = mat.GetVector(key);

            if (Application.isPlaying)
            {
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector4>();

                tweener.Initialize(start, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        tweener.OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
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
                    Vector4 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector4(start, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((val, linearProgres, time) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, val);
                        }).OnRewind(() =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, start);
                        }).OnComplete((duration) =>
                        {
                            if (mat == null)
                                return;
                            mat.SetVector(key, endValue);
                        }).SetEase(easeMode).SetAutokill(false);
                    }
                }

                return tweener;
            }
        }
    }
}
