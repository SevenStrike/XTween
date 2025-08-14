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