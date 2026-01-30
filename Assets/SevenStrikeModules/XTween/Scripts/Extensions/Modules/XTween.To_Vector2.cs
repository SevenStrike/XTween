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
        /// 创建一个从当前二维向量值到目标二维向量值的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="endValue">目标二维向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 true</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<Vector2> getter, XTween_Setter<Vector2> setter, Vector2 endValue, float duration, bool autokill = true)
        {
            Vector2 startValue = getter();

            if (Application.isPlaying)
            {
                // 使用对象池获取实例创建动画
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                // 初始化动画参数
                tweener.Initialize(startValue, endValue, duration * XTween_Dashboard.DurationMultiply);

                //注册回调
                tweener.OnUpdate((value, linearProgress, time) =>
                {
                    setter(value);
                }).OnRewind(() =>
                {
                    setter(startValue);
                }).SetAutokill(autokill);

                return tweener;
            }
            else
            {
                //编辑器预览方式创建动画
                XTween_Interface tweener;
                tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply)
                        .OnUpdate((value, linearProgress, time) =>
                        {
                            setter(value);
                        }).OnRewind(() =>
                        {
                            setter(startValue);
                        }).SetAutokill(false);
                return tweener;
            }
        }
        /// <summary>
        /// 创建一个从当前二维向量值到目标二维向量值的动画过渡
        /// </summary>
        /// <param name="getter">获取当前值的委托</param>
        /// <param name="setter">设置目标值的委托</param>
        /// <param name="endValue">目标二维向量值</param>
        /// <param name="duration">动画持续时间，单位为秒</param>
        /// <param name="autokill">动画完成后是否自动销毁，默认为 true</param>
        /// <returns>创建的动画对象</returns>
        public static XTween_Interface To(XTween_Getter<Vector2> getter, XTween_Setter<Vector2> setter, Vector2 endValue, float duration, bool autokill = true, EaseMode easeMode = EaseMode.InOutCubic, bool isFromMode = true, XTween_Getter<Vector2> fromvalue = null, bool useCurve = false, AnimationCurve curve = null)
        {
            Vector2 startValue = getter();

            if (Application.isPlaying)
            {
                // 使用对象池获取实例创建动画
                var tweener = XTween_Pool.CreateTween<XTween_Specialized_Vector2>();

                // 初始化动画参数
                tweener.Initialize(startValue, endValue, duration * XTween_Dashboard.DurationMultiply);

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetAutokill(autokill);
                    }
                    else
                    {
                        //注册回调
                        tweener.OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetAutokill(autokill);
                    }
                }
                return tweener;
            }
            else
            {
                //编辑器预览方式创建动画
                XTween_Interface tweener;

                // 从目标源值开始
                if (isFromMode)
                {
                    // 获取目标源值
                    Vector2 fromval = fromvalue();
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetFrom(fromval).SetEase(easeMode).SetAutokill(false);
                    }
                }
                else
                {
                    if (useCurve)// 使用曲线
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(curve).SetAutokill(false);
                    }
                    else
                    {
                        tweener = new XTween_Specialized_Vector2(startValue, endValue, duration * XTween_Dashboard.DurationMultiply).OnUpdate((value, linearProgress, time) => { setter(value); }).OnRewind(() => { setter(startValue); }).SetEase(easeMode).SetAutokill(false);
                    }
                }
                return tweener;
            }
        }
    }
}