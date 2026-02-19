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

    public static class XTween_Utilitys
    {
        public static bool PrintMsgEnable = true;

        /// <summary>
        /// 计算考虑旋转的相对位移坐标（2D空间）
        /// </summary>
        /// <param name="rectTransform">目标UI变换组件</param>
        /// <param name="currentPos">当前锚点位置</param>
        /// <param name="offset">局部空间偏移量</param>
        /// <returns>世界空间位移后的新坐标</returns>
        /// <remarks>
        /// 数学原理：
        /// 1. 获取RectTransform的Z轴旋转角度
        /// 2. 应用二维旋转矩阵计算实际偏移：
        ///    newX = offset.x * cosθ - offset.y * sinθ
        ///    newY = offset.x * sinθ + offset.y * cosθ
        /// 3. 叠加到当前位置
        ///
        /// 典型应用场景：
        /// - 实现"相对当前旋转角度的位移"
        /// - 处理旋转后的UI元素坐标变换
        ///
        /// 示例：
        /// 按钮旋转45度后，向右移动100像素：
        /// CalculateRelativePosition(transform, pos, new 二维向量_Vector2(100,0))
        /// </remarks>
        public static Vector2 CalculateRelativePosition(RectTransform rectTransform, Vector2 currentPos, Vector2 offset)
        {
            float angle = rectTransform.localEulerAngles.z * Mathf.Deg2Rad;
            return new Vector2(
                currentPos.x + offset.x * Mathf.Cos(angle) - offset.y * Mathf.Sin(angle),
                currentPos.y + offset.x * Mathf.Sin(angle) + offset.y * Mathf.Cos(angle)
            );
        }
        /// <summary>
        /// 计算考虑旋转的相对位移坐标（3D空间扩展版）
        /// </summary>
        /// <param name="offset">
        /// 偏移量：
        /// - XY轴：受旋转影响
        /// - Z轴：直接叠加
        /// </param>
        /// <returns>
        /// 新坐标：
        /// - XY分量：经过旋转计算
        /// - Z分量：currentPos.z + offset.z
        /// </returns>
        /// <remarks>
        /// 与2D版本的区别：
        /// 1. 保持Z轴独立性（不受旋转影响）
        /// 2. 使用Vector3类型参数
        ///
        /// 注意：
        /// - 仅处理Z轴旋转，忽略X/Y轴旋转
        /// - 适用于大部分UI动画场景
        /// </remarks>
        public static Vector3 CalculateRelativePosition(RectTransform rectTransform, Vector3 currentPos, Vector3 offset)
        {
            Vector2 xyOffset = CalculateRelativePosition(rectTransform, (Vector2)currentPos, (Vector2)offset);
            return new Vector3(xyOffset.x, xyOffset.y, currentPos.z + offset.z);
        }
        /// <summary>
        /// 计算颜色的亮度极限
        /// </summary>
        /// <param name="color">要计算亮度的颜色</param>
        /// <returns>返回False小于亮度中间值，返回True大于亮度中间值</returns>
        public static bool GetColorBrightnessLimite(Color color, float Threshold = 0.35f)
        {
            return (0.299f * color.r + 0.587f * color.g + 0.114f * color.b) > Threshold;
        }
        /// <summary>
        /// 计算颜色的亮度（灰度值）
        /// </summary>
        /// <param name="color">要计算亮度的颜色</param>
        /// <returns>颜色的亮度，范围在 0 到 1 之间</returns>
        public static float GetColorBrightness(Color color)
        {
            return 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
        }
        /// <summary>
        /// 打印消息到控制台
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <param name="Mode">消息类型</param>
        public static GUIMsgState DebugInfo(string Title, string Content, GUIMsgState Mode, GameObject xobject = null)
        {
            if (!PrintMsgEnable)
                return GUIMsgState.未开启消息模块功能;
            switch (Mode)
            {
                case GUIMsgState.确认:
                    Debug.Log("<color=#c4c4c4>" + Title + "： </color>" + "┠─<color=#c3e55c>" + Content + "</color>", xobject);
                    break;
                case GUIMsgState.错误:
                    Debug.Log("<color=#c4c4c4>" + Title + "： </color>" + "┠─<color=#ff3f3f>" + Content + "</color>", xobject);
                    break;
                case GUIMsgState.通知:
                    Debug.Log("<color=#c4c4c4>" + Title + "： </color>" + "┠─<color=#bebebe>" + Content + "</color>", xobject);
                    break;
                case GUIMsgState.警告:
                    Debug.Log("<color=#c4c4c4>" + Title + "： </color>" + "┠─<color=#ffb80e>" + Content + "</color>", xobject);
                    break;
                case GUIMsgState.设置:
                    Debug.Log("<color=#c4c4c4>" + Title + "： </color>" + "┠─<color=#5cc1e5>" + Content + "</color>", xobject);
                    break;
            }
            return Mode;
        }
        /// <summary>
        /// 将分隔符为逗号的"r,g,b,a"的字符串转换为Color类型
        /// </summary>
        /// <param name="ColorString">目标颜色格式字符串.</param>
        /// <param name="ValueMode">指定色值模式 \n为True时：输入色值范围=0 - 255 \n为False时：输入色值范围=0.0 - 1.0.</param>
        /// <returns>此方法返回类型为 -> 颜色_Color.</returns>
        public static Color ConvertStringToColor(string ColorString, bool ValueMode = true)
        {
            if (string.IsNullOrEmpty(ColorString))
                return Color.white;
            int Count = 0;
            string[] xx = ColorString.Split(new char[1] { ',' });

            float[] x = new float[xx.Length];

            foreach (string a in xx)
            {
                if (ValueMode)
                    x[Count] = float.Parse(a) / 255f;
                else
                    x[Count] = float.Parse(a);
                Count++;
                if (Count >= 4)
                {
                    Count = 0;
                }
            }
            Color color = new Color(x[0], x[1], x[2], x[3]);
            return color;
        }
        /// <summary>
        /// 将十六位进制颜色码字符串转换到Color类型
        /// </summary>
        /// <param name="hex">填写需要转换成Color类型的十六位进制的颜色码字符串（请忽略颜色代码开头的 #）</param>
        /// <returns>此方法返回类型为 -> 颜色_Color</returns>
        public static Color ConvertHexStringToColor(string hex)
        {
            Color nowColor = Color.black;

            bool HasPrefix = false;
            for (int i = 0; i < hex.Length; i++)
            {
                if (hex[i] == '#')
                    HasPrefix = true;
            }
            if (HasPrefix)
                ColorUtility.TryParseHtmlString(hex, out nowColor);
            else
                ColorUtility.TryParseHtmlString("#" + hex, out nowColor);
            return nowColor;
        }
        /// <summary>
        /// 将Color类型转换到十六进制颜色码字符串
        /// </summary>
        /// <param name="color">填写需要转换成字符串格式的Color类型</param>
        /// <param name="HasPrefixSymbol">True：前缀带有 # 号，False：无_None # 号前缀</param>
        /// <returns>此方法返回类型为 -> 字符串_String</returns>
        public static string ConvertColorToHexString(Color color, bool HasPrefixSymbol = false)
        {
            if (HasPrefixSymbol)
                return "#" + ColorUtility.ToHtmlStringRGB(color);
            else
                return ColorUtility.ToHtmlStringRGB(color);
        }
        /// <summary>
        ///  将Vector3向量转换为 x,y,z 字符串格式（小括号可选）
        /// </summary>
        /// <param name="SourceVector">需要转换的向量.</param>
        /// <param name="IncludeBracket">转换后是否包含前后小括号，例：(x,y,z) </param>
        /// <returns>此方法返回类型为 ->  x,y,z 格式的字符串.</returns>
        public static string ConvertVector3ToString(Vector3 SourceVector, bool IncludeBracket = false)
        {
            string Combine = null;

            if (!IncludeBracket)
            {
                string ConData = SourceVector.ToString().Remove(0, 1).Trim();
                Combine = ConData.Remove(ConData.Length - 1, 1).Trim();
            }
            else
            {
                Combine = SourceVector.ToString();
            }
            return Combine;
        }
    }
}