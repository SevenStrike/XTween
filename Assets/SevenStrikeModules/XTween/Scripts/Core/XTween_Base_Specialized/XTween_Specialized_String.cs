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

    public class XTween_Specialized_String : XTween_Base<string>
    {
        public XTween_Specialized_String(string defaultFromValue, string endValue, float duration) : base(defaultFromValue, endValue, duration)
        {
        }

        public XTween_Specialized_String()
        {
            _DefaultValue = string.Empty;
            _EndValue = string.Empty;
            _Duration = 1;
            _StartValue = string.Empty;
            _CustomEaseCurve = null;
            _UseCustomEaseCurve = false;

            ResetState();
        }

        protected override string Lerp(string a, string b, float t)
        {
            // 字符串类型不支持 Lerp，直接返回目标值
            return b;
        }

        protected override string GetDefaultValue()
        {
            return string.Empty;
        }

        public override XTween_Base<string> ReturnSelf()
        {
            return this;
        }

        protected override string CalculateCurrentValue()
        {
            // 计算当前应该显示的字符数量
            float easedProgress = CalculateEasedProgress(_CurrentLinearProgress);
            int charCount = Mathf.RoundToInt(easedProgress * _EndValue.Length);
            charCount = Mathf.Clamp(charCount, 0, _EndValue.Length);

            // 构建当前显示的字符串
            return _EndValue.Substring(0, charCount);
        }
    }
}