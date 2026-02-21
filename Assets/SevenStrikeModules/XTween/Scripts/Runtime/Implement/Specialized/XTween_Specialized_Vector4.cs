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
    /// 专门处理 四维向量_Vector4 类型动画的补间类
    /// </summary>
    /// <remarks>
    /// 该类继承自 HudAnimation_TweenerBase<四维向量_Vector4>，实现了对 四维向量_Vector4 类型的插值计算
    /// 主要用于处理单个 四维向量_Vector4 的动画，例如3D位置、缩放_Scale、旋转等
    /// </remarks>
    public class XTween_Specialized_Vector4 : XTween_Base<Vector4>
    {
        /// <summary>
        /// 默认初始化构造
        /// </summary>
        /// <param name="defaultFromValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public XTween_Specialized_Vector4(Vector4 defaultFromValue, Vector4 endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
            // 已在基类 protected XTween_Base(TArg defaultFromValue, TArg endValue, float duration) 初始化
        }

        /// <summary>
        /// 用于对象池预加载新建实例构造
        /// </summary>
        public XTween_Specialized_Vector4()
        {
            _DefaultValue = Vector4.zero;
            _EndValue = Vector4.zero;
            _Duration = 1;
            _StartValue = Vector4.zero;
            _CustomEaseCurve = null; // 显式初始化为null
            _UseCustomEaseCurve = false; // 默认不使用自定义曲线

            ResetState();
        }
        /// <summary>
        /// 执行 四维向量_Vector4 类型的插值计算。
        /// 使用 四维向量_Vector4.LerpUnclamped 方法，支持超出 [0, 1] 范围的插值系数。
        /// </summary>
        /// <param name="a">起始值。</param>
        /// <param name="b">目标值。</param>
        /// <param name="t">插值系数。</param>
        /// <returns>插值结果。</returns>
        protected override Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            /// <summary>
            /// 使用 四维向量_Vector4.LerpUnclamped 方法计算插值
            /// 四维向量_Vector4.LerpUnclamped 是 Unity 提供的插值方法，适用于 四维向量_Vector4 类型
            /// 与 四维向量_Vector4.Lerp 不同，LerpUnclamped 不限制 t 的范围，允许 t 超出 [0, 1] 范围
            /// </summary>
            return Vector4.LerpUnclamped(a, b, t);
        }

        /// <summary>
        /// 获取 四维向量_Vector4 类型的默认初始值。
        /// 默认值为 四维向量_Vector4.zero。
        /// </summary>
        /// <returns>默认初始值。</returns>
        protected override Vector4 GetDefaultValue()
        {
            /// <summary>
            /// 返回 四维向量_Vector4 类型的默认值 四维向量_Vector4.zero
            /// </summary>
            return Vector4.zero;
        }

        /// <summary>
        /// 返回当前实例，支持链式调用。
        /// </summary>
        /// <returns>当前实例。</returns>
        public override XTween_Base<Vector4> ReturnSelf()
        {
            /// <summary>
            /// 返回当前实例，支持链式调用
            /// </summary>
            return this;
        }
    }
}