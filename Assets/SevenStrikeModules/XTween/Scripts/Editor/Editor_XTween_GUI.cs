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
namespace SevenStrikeModules.XTween.Editor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    using Color = UnityEngine.Color;
    using Font = UnityEngine.Font;

    /// <summary>
    /// XTween弹窗对话框参数
    /// </summary>
    public struct XTweenDialogInfo
    {
        /// <summary>
        /// 弹窗类型
        /// </summary>
        public XTweenDialogType type;
        /// <summary>
        /// 窗口标题
        /// </summary>
        public string windowtitle;
        /// <summary>
        /// 主标题
        /// </summary>
        public string title;
        /// <summary>
        /// 内容
        /// </summary>
        public string msg;
        /// <summary>
        /// 选项按钮
        /// </summary>
        public string[] options;
        /// <summary>
        /// 主要按钮索引
        /// </summary>
        public int PrimaryIndex;
    }

    /// <summary>
    /// 为XTween的Editor界面提供样式控件
    /// </summary>
    public static class Editor_XTween_GUI
    {
        /// <summary>
        /// GUI样式
        /// </summary>
        public static GUISkin GUICreator;
        /// <summary>
        /// 初始化样式文件
        /// </summary>
        public static void Gui_Layout_Initia()
        {
            GUISkin guiskin = GUICreator = AssetDatabase.LoadAssetAtPath<GUISkin>($"{XTween_Dashboard.Get_path_XTween_GUIStyle_Path()}XTweenEditorStyle.guiskin");
        }

        #region GUI样式
        /// <summary>
        /// 获取背景填充
        /// </summary>
        /// <param name="Color"></param>
        public static Texture2D GetFillTexture(GUIFilled Mode, GUIColor Color, bool IsHalf = false)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            Texture2D tex;

            if (IsHalf)
            {
                tex = AssetDatabase.LoadAssetAtPath<Texture2D>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"EditorUI/Group/Group_缺口{Mode}_{Color.ToString()}.png");
            }
            else
            {
                tex = AssetDatabase.LoadAssetAtPath<Texture2D>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"EditorUI/Group/Group_{Mode}_{Color.ToString()}.png");
            }
            return tex;
        }
        /// <summary>
        /// 获取按钮背景填充
        /// </summary>
        /// <param name="Mode">类型</param>
        /// <param name="Color">颜色（具体请查看工程目录中文件的实际名称）</param>
        public static Texture2D GetBtnFillTexture(GUIFilled Mode, GUIColor Color)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            if (Mode == GUIFilled.透明)
                return AssetDatabase.LoadAssetAtPath<Texture2D>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"EditorUI/Button/Btn_{Mode}.png");
            else
                return AssetDatabase.LoadAssetAtPath<Texture2D>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"EditorUI/Button/Btn_{Mode}_{Color}.png");
        }
        /// <summary>
        /// 获取内建图标
        /// </summary>
        /// <param name="IconName">图标名称</param>
        /// <returns></returns>
        public static Texture2D GetBuiltInIcon(string IconName)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            return EditorGUIUtility.IconContent(IconName).image as Texture2D;
        }
        /// <summary>
        /// 获取最近一个Rect
        /// </summary>
        /// <returns></returns>
        public static Rect Gui_GetLastRect()
        {
            return GUILayoutUtility.GetLastRect();
        }
        /// <summary>
        /// 获取规范化颜色
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetColor(GUIColor color)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            Color col = new Color();
            switch (color)
            {
                case GUIColor.深空灰:
                    ColorUtility.TryParseHtmlString("#232323", out col);
                    break;
                case GUIColor.阴影灰:
                    ColorUtility.TryParseHtmlString("#909090", out col);
                    break;
                case GUIColor.亮白:
                    ColorUtility.TryParseHtmlString("#e8e8e8", out col);
                    break;
                case GUIColor.柠檬绿:
                    ColorUtility.TryParseHtmlString("#c9e63f", out col);
                    break;
                case GUIColor.工业蓝:
                    ColorUtility.TryParseHtmlString("#38aafb", out col);
                    break;
                case GUIColor.警示黄:
                    ColorUtility.TryParseHtmlString("#ffc230", out col);
                    break;
                case GUIColor.玫瑰粉:
                    ColorUtility.TryParseHtmlString("#fa5d98", out col);
                    break;
                case GUIColor.神秘紫:
                    ColorUtility.TryParseHtmlString("#9e5afb", out col);
                    break;
                case GUIColor.魅力红:
                    ColorUtility.TryParseHtmlString("#ff3737", out col);
                    break;
                case GUIColor.灰绿:
                    ColorUtility.TryParseHtmlString("#94a565", out col);
                    break;
                case GUIColor.亮橘红:
                    ColorUtility.TryParseHtmlString("#ff872e", out col);
                    break;
                case GUIColor.枪灰:
                    ColorUtility.TryParseHtmlString("#747474", out col);
                    break;
                case GUIColor.亮金色:
                    ColorUtility.TryParseHtmlString("#ffd86b", out col);
                    break;
                case GUIColor.沉暗红:
                    ColorUtility.TryParseHtmlString("#a32a2a", out col);
                    break;
                case GUIColor.烟灰蓝:
                    ColorUtility.TryParseHtmlString("#76829a", out col);
                    break;
                case GUIColor.健康绿:
                    ColorUtility.TryParseHtmlString("#4aeec9", out col);
                    break;
                case GUIColor.浅灰:
                    ColorUtility.TryParseHtmlString("#414141", out col);
                    break;
                case GUIColor.无:
                    break;
            }
            return col;
        }
        /// <summary>
        /// 获取基础图标
        /// </summary>
        /// <param name="str">图标名称</param>
        public static Texture2D GetIcon(string str)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            return AssetDatabase.LoadAssetAtPath<Texture2D>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"Icon/{str}.png");
        }
        /// <summary>
        /// 获取自定义图标
        /// </summary>
        /// <param name="str">图标路径</param>
        public static Texture2D GetCustomIcon(string str)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            return AssetDatabase.LoadAssetAtPath<Texture2D>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"{str}.png");
        }
        /// <summary>
        /// Logo样式
        /// </summary>
        public static GUIStyle Style_Logo
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Logo"));
                return gs;
            }
        }
        /// <summary>
        /// LogoState样式
        /// </summary>
        public static GUIStyle Style_LogoState
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("LogoState"));
                return gs;
            }
        }
        /// <summary>
        /// Box样式
        /// </summary>
        public static GUIStyle Style_Box
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Box"));
                return gs;
            }
        }
        /// <summary>
        /// Line样式
        /// </summary>
        public static GUIStyle Style_Seperator
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Separator"));
                return gs;
            }
        }
        /// <summary>
        /// 内容区域样式
        /// </summary>
        public static GUIStyle Style_Group
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Group"));
                return gs;
            }
        }
        /// <summary>
        /// 内容区域样式（缺口）
        /// </summary>
        public static GUIStyle Style_Group_Half
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("GroupHalf"));
                return gs;
            }
        }
        /// <summary>
        /// 开关样式
        /// </summary>
        public static GUIStyle Style_EnumsOption
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("EnumsOption"));
                return gs;
            }
        }
        /// <summary>
        /// 输入框样式
        /// </summary>
        public static GUIStyle Style_Inputfield
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Inputfield"));
                return gs;
            }
        }
        /// <summary>
        /// 标签框样式
        /// </summary>
        public static GUIStyle Style_Labelfield
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Labelfield"));
                return gs;
            }
        }
        /// <summary>
        /// 标签框样式
        /// </summary>
        public static GUIStyle Style_LabelfieldBoldText
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("LabelfieldBoldText"));
                return gs;
            }
        }
        /// <summary>
        /// 标签框样式
        /// </summary>
        public static GUIStyle Style_LabelfieldOffset
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("OffsetLabelfield"));
                return gs;
            }
        }
        /// <summary>
        /// 选项标签样式
        /// </summary>
        public static GUIStyle Style_OptionLabel
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                return GUICreator.GetStyle("OptionLabel");
            }
        }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public static GUIStyle Style_Button
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Button"));
                return gs;
            }
        }
        /// <summary>
        /// 输入框样式
        /// </summary>
        public static GUIStyle Style_TextsField
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("TextsField"));
                return gs;
            }
        }
        /// <summary>
        /// 信息框样式
        /// </summary>
        public static GUIStyle Style_LiquidField
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("LiquidField"));
                return gs;
            }
        }
        /// <summary>
        /// 选项条样式
        /// </summary>
        public static GUIStyle Style_ToolBar
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("ToolBar"));
                return gs;
            }
        }
        /// <summary>
        /// 图标样式 - Setup
        /// </summary>
        public static GUIStyle Style_IconButton
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("IconButton"));
                return gs;
            }
        }
        /// <summary>
        /// 图标样式 - Setup
        /// </summary>
        public static GUIStyle Style_Icon
        {
            get
            {
                if (GUICreator == null)
                {
                    Gui_Layout_Initia();
                }

                GUIStyle gs = new GUIStyle(GUICreator.GetStyle("Icon"));
                return gs;
            }
        }
        public static GUIStyle GetStyle(string StyleName)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            return new GUIStyle(GUICreator.GetStyle(StyleName));
        }
        public static Font GetFont(string FontName)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            Font f_ttf = AssetDatabase.LoadAssetAtPath<Font>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"EditorFonts/{FontName}.ttf");

            Font f_otf = AssetDatabase.LoadAssetAtPath<Font>(XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + $"EditorFonts/{FontName}.otf");

            if (f_ttf != null)
                return f_ttf;
            else if (f_ttf != null)
                return f_otf;
            else
                return null;
        }

        #endregion

        #region GUI 控件
        /// <summary>
        /// 排版布局
        /// </summary>
        /// <param name="rect_group"></param>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        /// <param name="Title">标题</param>
        /// <param name="TitleOffset">标题偏移</param>
        /// <param name="TitleColor">标题颜色</param>
        /// <param name="Font">字体</param>
        public static void Gui_Group(Rect rect_group, GUIFilled FillStyle, GUIColor Color, string Title, Vector2 TitleOffset, Color TitleColor, Font Font)
        {
            GUIStyle Style = new GUIStyle(Style_Group_Half);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color, true);
            else
                Style.normal.background = null;

            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Group;
            GUI.BeginGroup(rect_group, Style);
            GUI.EndGroup();
            GUI.backgroundColor = bgcol;

            Gui_Labelfield(new Rect(rect_group.x + TitleOffset.x, rect_group.y + TitleOffset.y, 80, 20), Title, GUIFilled.无, GUIColor.亮白, TitleColor, TextAnchor.UpperLeft, new Vector2(0, 0), 12, Font);
        }

        #region Box
        public static void Gui_Box_Style(Rect rect, GUIFilled FillStyle, GUIColor Color)
        {
            GUIStyle Style = new GUIStyle(Style_Group);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            GUI.Box(rect, "", Style);
        }
        public static void Gui_Box(Rect rect)
        {
            GUIStyle Style = new GUIStyle(Style_Box);
            GUI.Box(rect, "", Style);
        }
        public static void Gui_Box(Rect rect, Color color)
        {
            EditorGUI.DrawRect(rect, color);
        }
        public static void Gui_TextureBox(Rect rect, Texture2D tex)
        {
            EditorGUI.LabelField(rect, new GUIContent(tex));
        }
        #endregion

        #region Icon
        /// <summary>
        /// 创造一个图标
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        public static void Gui_Icon(Rect Rect, Texture2D tex_release)
        {
            GUIStyle Style = new GUIStyle(Style_IconButton);
            Style.normal.background = tex_release;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;
            GUI.Box(Rect, "", Style);
        }
        /// <summary>
        /// 创造一个图标
        /// </summary>
        /// <param name="Rect">尺寸</param>
        /// <param name="tex_release">贴图</param>
        /// <param name="color">颜色</param>
        public static void Gui_Icon(Rect Rect, Texture2D tex_release, Color color)
        {
            GUIStyle Style = new GUIStyle(Style_IconButton);
            Style.normal.background = tex_release;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;
            GUI.backgroundColor = color;
            GUI.Box(Rect, "", Style);
            GUI.backgroundColor = Color.white;
        }
        /// <summary>
        /// 创造一个图标
        /// </summary>
        /// <param name="Rect">尺寸</param>
        public static void Gui_Icon(Rect Rect, GUIContent Content)
        {
            GUIStyle Style = new GUIStyle(Style_IconButton);
            Style.normal.background = (Texture2D)Content.image;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;
            GUI.Box(Rect, Content, Style);
        }
        /// <summary>
        /// 创造一个图标
        /// </summary>
        /// <param name="Rect">尺寸</param>
        /// <param name="tex_release">贴图</param>
        /// <param name="Rect">偏移</param>
        /// <param name="Rect">颜色</param>
        public static void Gui_Icon(Rect Rect, Texture2D tex_release, RectOffset border, Color color)
        {
            GUIStyle Style = new GUIStyle(Style_IconButton);
            Style.normal.background = tex_release;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;
            Style.border = border;
            GUI.backgroundColor = color;
            GUI.Box(Rect, "", Style);
            GUI.backgroundColor = Color.white;
        }
        #endregion

        #region Popup
        /// <summary>
        /// 创造一个选项按钮
        /// </summary>
        /// <param name="Rect">尺寸</param>
        /// <param name="Rect"></param>
        /// <param name="index"></param>
        /// <param name="options"></param>
        /// <param name="FillStyle"></param>
        /// <param name="Color"></param>
        /// <param name="ButtonTextColor"></param>        
        /// <returns>返回的Int值用于判断选择了那个选项</returns>
        public static int Gui_Popup(Rect Rect, int index, string[] options, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor)
        {
            GUIStyle Style = new GUIStyle(Style_EnumsOption);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;
            int sw = EditorGUI.Popup(Rect, index, options, Style);
            return sw;
        }
        /// <summary>
        /// 创造一个选项按钮
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        /// <returns>返回的Int值用于判断选择了那个选项</returns>
        public static void Gui_PopupWithString(Rect Rect, ref SerializedProperty serializedProperty, string[] options, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor)
        {
            GUIStyle Style = new GUIStyle(Style_EnumsOption);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;

            string indexname = serializedProperty.stringValue;
            int index = 0;

            for (int i = 0; i < options.Length; i++)
            {
                if (options[i] == indexname)
                {
                    index = i;
                }
            }

            int sss = EditorGUI.Popup(Rect, index, options, Style);

            serializedProperty.stringValue = options[sss];
        }
        #endregion

        #region Button
        /// <summary>
        /// 创造一个图标按钮
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Button(Rect Rect, Texture2D tex_release, Texture2D tex_press, bool use_tex = true, string button_text = "", string button_tooltip = "", Color button_color = default(Color), Color button_text_color = default(Color), GUIFilled FillStyle = GUIFilled.实体)
        {
            GUIStyle Style = new GUIStyle(Style_Button);

            if (use_tex)
            {
                Style.border = new RectOffset(0, 0, 0, 0);
                Style.normal.background = tex_release;
                Style.active.background = tex_press;
            }
            else
            {
                if (FillStyle != GUIFilled.无)
                    Style.normal.background = GetBtnFillTexture(FillStyle, GUIColor.亮白);
                else
                    Style.normal.background = null;
            }

            Style.normal.textColor = button_text_color;

            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;

            GUIContent gui = new GUIContent();
            if (use_tex)
            {
                gui.text = button_text;
                gui.tooltip = button_tooltip;
            }

            GUI.backgroundColor = button_color;
            bool sw = false;
            if (use_tex)
                sw = GUI.Button(Rect, "", Style);
            else
                sw = GUI.Button(Rect, button_text, Style);
            GUI.backgroundColor = Color.white;
            return sw;
        }
        /// <summary>
        /// 创造一个图标按钮
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Button(Rect Rect, Texture2D tex_release, Texture2D tex_press, TextAnchor anchor, bool use_tex = true, string button_text = "", string button_tooltip = "", Color button_color = default(Color), Color button_text_color = default(Color), GUIFilled FillStyle = GUIFilled.实体, int FontSize = 12)
        {
            GUIStyle Style = new GUIStyle(Style_Button);

            if (use_tex)
            {
                Style.border = new RectOffset(0, 0, 0, 0);
                Style.normal.background = tex_release;
                Style.active.background = tex_press;
            }
            else
            {
                if (FillStyle != GUIFilled.无)
                    Style.normal.background = GetBtnFillTexture(FillStyle, GUIColor.亮白);
                else
                    Style.normal.background = null;
            }
            Style.alignment = anchor;
            Style.normal.textColor = button_text_color;
            Style.fontSize = FontSize;
            Style.fixedWidth = Rect.width;
            Style.fixedHeight = Rect.height;

            GUIContent gui = new GUIContent();
            if (use_tex)
            {
                gui.text = button_text;
                gui.tooltip = button_tooltip;
            }

            GUI.backgroundColor = button_color;
            bool sw = false;
            if (use_tex)
                sw = GUI.Button(Rect, "", Style);
            else
                sw = GUI.Button(Rect, button_text, Style);
            GUI.backgroundColor = Color.white;
            return sw;
        }
        #endregion

        #region Toggle
        /// <summary>
        /// 创造一个开关
        /// </summary>
        /// <param name="Text">开关标题</param>
        /// <param name="Val">指定布尔值</param>
        /// <param name="FillStyle_Normal">正常状态下的选项按钮背景样式 Solid=实心  Edge=空心</param>
        /// <param name="Color_Normal">正常状态下的选项按钮颜色样式</param>
        /// <param name="FillStyle_Selected">选中状态下的选项按钮背景样式 Solid=实心  Edge=空心</param>
        /// <param name="Color_Selected">选中状态下的选项按钮颜色样式</param>
        /// <param name="TextColor_Normal">正常状态选项按钮文字颜色</param>
        /// <param name="TextColor_Selected">选中状态选项按钮文字颜色</param>
        /// <returns></returns>
        public static bool Gui_Toggle(Rect rect, bool simple, string[] Options = null, bool Val = false, GUIFilled FillStyle_Normal = GUIFilled.边框, GUIColor Color_Normal = GUIColor.亮白, GUIFilled FillStyle_Selected = GUIFilled.实体, Color Color_Selected = default, Color TextColor_Normal = default, Color TextColor_Selected = default)
        {
            if (simple)
            {
                return EditorGUI.Toggle(rect, GUIContent.none, Val);
            }
            else
            {
                int x = 0;
                GUIStyle Style = new GUIStyle(Style_ToolBar);
                Style.normal.background = GetBtnFillTexture(FillStyle_Normal, Color_Normal);
                Style.onNormal.background = GetBtnFillTexture(FillStyle_Selected, GUIColor.亮白);
                Style.normal.textColor = TextColor_Normal;
                Style.onNormal.textColor = TextColor_Selected;

                if (Val)
                    x = 1;
                else
                    x = 0;
                Color bgcol = GUI.backgroundColor;
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
                x = GUI.Toolbar(rect, x, Options, Style);
                GUI.backgroundColor = bgcol;
                if (x == 1)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region ToolBar
        /// <summary>
        /// 创造一个工具选项
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="Options"></param>
        /// <param name="Val"></param>
        /// <param name="FillStyle_Normal"></param>
        /// <param name="FillStyle_Selected"></param>
        /// <param name="Color_Selected"></param>
        /// <param name="TextColor_Normal"></param>
        /// <param name="TextColor_Selected"></param>
        /// <returns></returns>
        public static int Gui_ToolBar(Rect rect, string[] Options, int Val, GUIFilled FillStyle_Normal, GUIFilled FillStyle_Selected, Color Color_Selected, Color TextColor_Normal, Color TextColor_Selected)
        {
            GUIStyle Style = new GUIStyle(Style_ToolBar);
            Style.normal.background = GetBtnFillTexture(FillStyle_Normal, GUIColor.亮白);
            Style.onNormal.background = GetBtnFillTexture(FillStyle_Selected, GUIColor.亮白);
            Style.normal.textColor = TextColor_Normal;
            Style.onNormal.textColor = TextColor_Selected;

            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            Val = GUI.Toolbar(rect, Val, Options, Style);
            GUI.backgroundColor = bgcol;
            return Val;
        }
        #endregion

        #region Colorfield
        /// <summary>
        /// 创造一个Color框
        /// </summary>
        /// <param name="Rect">尺寸_Size</param>
        /// <param name="val">颜色</param>
        /// <returns>返回Color数值</returns>
        public static Color Gui_ColorField(Rect Rect, Color val)
        {
            return EditorGUI.ColorField(Rect, val);
        }
        /// <summary>
        /// 创造一个Color框
        /// </summary>
        /// <param name="Rect">尺寸</param>
        /// <param name="val">序列化颜色</param>
        /// <returns>返回Color数值</returns>
        public static void Gui_ColorField(Rect Rect, SerializedProperty val)
        {
            EditorGUI.PropertyField(Rect, val, GUIContent.none);
        }
        #endregion

        /// <summary>
        /// 创造一个曲线框
        /// </summary>
        /// <param name="Rect">尺寸_Size</param>
        /// <param name="val">曲线</param>
        /// <returns>返回AnimationCurve数值</returns>
        public static AnimationCurve Gui_CurveField(Rect Rect, AnimationCurve val)
        {
            return EditorGUI.CurveField(Rect, val);
        }

        #region Textfield
        /// <summary>
        /// 创造一个文本输入框
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string Gui_TextField(Rect Rect, string Content)
        {
            GUIStyle Style = new GUIStyle(Style_Inputfield);
            return GUI.TextField(Rect, Content, Style);
        }
        /// <summary>
        /// 创造一个文本输入框
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string Gui_TextField(Rect Rect, string Content, Color TextColor, int FontSize)
        {
            GUIStyle Style = new GUIStyle(Style_TextsField);
            Style.fontSize = FontSize;
            Style.normal.textColor = TextColor;
            return EditorGUI.TextField(Rect, Content, Style);
        }
        /// <summary>
        /// 创造一个信息输入框
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static void Gui_LiquidField(Rect Rect, string Content, RectOffset border, Texture2D Bg)
        {
            GUIStyle Style = new GUIStyle(Style_LiquidField);
            Style.border = border;
            Style.normal.background = Bg;
            EditorGUI.LabelField(Rect, Content, Style);
        }
        #endregion

        #region Inputfield
        /// <summary>
        /// 创造一个Vector2输入框
        /// </summary>
        /// <param name="Rect">尺寸_Size</param>
        /// <returns>返回Vector2数值</returns>
        public static Vector2 Gui_InputField_Vector2(Rect Rect, Vector2 val)
        {
            return EditorGUI.Vector2Field(Rect, GUIContent.none, val);
        }
        /// <summary>
        /// 创造一个Vector3输入框
        /// </summary>
        /// <param name="Rect">尺寸_Size</param>
        /// <returns>返回Vector3数值</returns>
        public static Vector3 Gui_InputField_Vector3(Rect Rect, Vector3 val)
        {
            return EditorGUI.Vector3Field(Rect, GUIContent.none, val);
        }
        /// <summary>
        /// 创造一个Vector4输入框
        /// </summary>
        /// <param name="Rect">尺寸_Size</param>
        /// <returns>返回Vector4数值</returns>
        public static Vector4 Gui_InputField_Vector4(Rect Rect, Vector4 val)
        {
            return EditorGUI.Vector4Field(Rect, GUIContent.none, val);
        }
        /// <summary>
        /// 创造一个int输入框
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        /// <returns>返回int数值</returns>
        public static int Gui_InputField_Int(Rect Rect, int val)
        {
            //GUIStyle gfs = Style_Inputfield;
            //gfs.alignment = TextAnchor.MiddleLeft;
            return EditorGUI.IntField(Rect, val);
        }
        /// <summary>
        /// 创造一个Float输入框
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        /// <returns>返回Float数值</returns>
        public static float Gui_InputField_Float(Rect Rect, float val)
        {
            return EditorGUI.FloatField(Rect, val);
        }
        /// <summary>
        /// 创造一个Float输入框
        /// </summary>
        /// <param name="Rect">按钮尺寸</param>
        /// <returns>返回Float数值</returns>
        public static float Gui_InputField_Float(Rect Rect, string Text, float val)
        {
            return EditorGUI.FloatField(Rect, Text, val);
        }
        /// <summary>
        /// 创造一个String输入框
        /// </summary>
        /// <param name="Rect">尺寸_Size</param>
        /// <returns>返回String数值</returns>
        public static string Gui_InputField_String(Rect Rect, string val)
        {
            return EditorGUI.TextField(Rect, val);
        }
        #endregion

        /// <summary>
        /// 创造一个序列化属性
        /// </summary>
        /// <param name="Rect"></param>
        /// <param name="property"></param>
        public static void Gui_SerializePropertyWithObject(Rect Rect, SerializedProperty property)
        {
            EditorGUI.ObjectField(Rect, property, GUIContent.none);
        }

        #region Property_Field
        /// <summary>
        /// 创造一个属性框
        /// </summary>
        public static void Gui_Property_Field(Rect Rect, string Text, SerializedProperty value, float Margin, float LabelWidth = 40)
        {
            float labwidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = LabelWidth;
            EditorGUI.PropertyField(new Rect(Rect.x + Margin, Rect.y, Rect.width, Rect.height), value, new GUIContent(Text));
            value.serializedObject.ApplyModifiedProperties();
            EditorGUIUtility.labelWidth = labwidth;
        }
        /// <summary>
        /// 创造一个属性框
        /// </summary>
        public static void Gui_Property_Field(Rect Rect, string Text, string Abbr, SerializedProperty value)
        {
            float labwidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = Rect.width < 100 ? Abbr.Length * 10 : Text.Length * 15;

            EditorGUI.PropertyField(new Rect(Rect.x, Rect.y, Rect.width, Rect.height), value, new GUIContent(Rect.width < 100 ? Abbr : Text));
            value.serializedObject.ApplyModifiedProperties();
            EditorGUIUtility.labelWidth = labwidth;
        }
        /// <summary>
        /// 创造一个属性框
        /// </summary>
        public static void Gui_Property_Field(Rect Rect, string Text, SerializedProperty value, float Margin, float PropertyWidth, float PropertyHeight, float LabelWidth = 40)
        {
            float labwidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = LabelWidth;
            EditorGUI.PropertyField(new Rect(Rect.x + Margin, Rect.y, PropertyWidth, PropertyHeight), value, new GUIContent(Text));
            value.serializedObject.ApplyModifiedProperties();
            EditorGUIUtility.labelWidth = labwidth;
        }
        #endregion

        #region Labelfield
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.font = font;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, int FontSize = 12, bool richText = true, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.font = font;
            Style.richText = richText;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, int FontSize = 12, Font font = null, bool WordWrap = true)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.font = font;
            Style.wordWrap = WordWrap;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.font = font;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, bool DisplayBg, Color BgColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.font = font;
            Style.richText = true;
            if (DisplayBg)
            {
                EditorGUI.DrawRect(Rect, BgColor);
            }
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, TextClipping cliping = TextClipping.Overflow, bool RichText = false)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.clipping = cliping;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, bool ClipText = false, TextClipping ClipMode = TextClipping.Clip, bool RichText = false, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.richText = RichText;
            if (font != null)
                Style.font = font;
            if (ClipText)
            {
                // 使用 TextClipping.Overflow 来显示省略号
                Style.clipping = ClipMode;
            }

            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield_WrapText(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, bool WrapText = false, bool ClipText = false, TextClipping ClipMode = TextClipping.Clip, bool RichText = false, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldBoldText);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.richText = RichText;
            Style.wordWrap = WrapText;
            if (font != null)
                Style.font = font;
            if (ClipText)
            {
                // 使用 TextClipping.Overflow 来显示省略号
                Style.clipping = ClipMode;
            }

            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框（细字体） - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield_Thin_WrapClip(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, bool RichText = false, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.font = font;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.wordWrap = true;
            Style.richText = RichText;
            Style.clipping = TextClipping.Clip;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框（细字体） - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield_Thin(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, bool WrapWord = false, bool ContentClip = false, bool RichText = false)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.wordWrap = WrapWord;
            Style.richText = RichText;
            if (ContentClip)
                Style.clipping = TextClipping.Clip;
            GUI.Label(Rect, Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框（细字体） - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Labelfield_Thin_WithClipping(Rect Rect, string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, bool WrapWord = false, bool ContentClip = false, bool RichText = false, TextClipping textClipping = TextClipping.Clip)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.contentOffset = Offset;
            Style.wordWrap = WrapWord;
            Style.richText = RichText;
            if (ContentClip)
                Style.clipping = textClipping;
            GUI.Label(Rect, Text, Style);
        }
        #endregion

        #region Slider
        /// <summary>
        /// 创造一个Gui滑动条
        /// </summary>
        public static float Gui_Slider(Rect rect, string Text, float Value, float LeftVal, float RightVal)
        {
            float x = EditorGUI.Slider(rect, GUIContent.none, Value, LeftVal, RightVal);
            return x;
        }
        /// <summary>
        /// 创造一个Gui滑动条
        /// </summary>
        public static int Gui_Slider(Rect rect, string Text, int Value, int LeftVal, int RightVal)
        {
            int x = EditorGUI.IntSlider(rect, GUIContent.none, Value, LeftVal, RightVal);
            return x;
        }
        #endregion
        #endregion

        #region GUILayout控件
        /// <summary>
        /// 控制GUI可用性开关
        /// </summary>
        /// <param name="state"></param>
        public static void SetEnabled(bool state)
        {
            GUI.enabled = state;
        }

        #region Space
        /// <summary>
        /// 间距
        /// </summary>
        /// <param name="Val">间距值</param>
        public static void Gui_Layout_Space(float Val)
        {
            GUILayout.Space(Val);
        }
        /// <summary>
        /// 自适应间距
        /// </summary>
        /// <param name="Val">间距值</param>
        public static void Gui_Layout_FlexSpace()
        {
            GUILayout.FlexibleSpace();
        }
        #endregion

        #region Banner
        /// <summary>
        /// 创造一个版头
        /// </summary>
        /// <param name="FillStyle">版头背景填充样式  实心 / 镂空</param>
        /// <param name="Color">背景颜色</param>
        /// <param name="BannerText">版头文字</param>
        public static void Gui_Layout_Banner(GUIFilled FillStyle, GUIColor Color, string BannerText, Color TextColor)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            GUIStyle BannerStyle = new GUIStyle(GUICreator.GetStyle("Banner_Text"));
            BannerStyle.normal.background = GetFillTexture(FillStyle, Color);

            GUIContent guicontent = new GUIContent();
            guicontent.text = BannerText;
            BannerStyle.normal.textColor = TextColor;
            Color bgcol = GUI.color;
            GUI.color = XTween_Dashboard.Theme_Primary;
            GUILayout.BeginVertical(guicontent, BannerStyle);
            GUI.color = bgcol;
            GUILayout.EndVertical();
        }
        /// <summary>
        /// 创造一个版头
        /// </summary>
        /// <param name="FillStyle">版头背景填充样式  实心 / 镂空</param>
        /// <param name="Color">背景颜色</param>
        /// <param name="BannerText">版头文字</param>
        public static void Gui_Layout_Banner(Texture2D Logo, GUIFilled FillStyle, GUIColor Color, string BannerText, Color TextColor, float logo_width = 0, float logo_height = 0)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            GUIStyle BannerStyle = new GUIStyle(GUICreator.GetStyle("Banner"));
            BannerStyle.normal.background = GetFillTexture(FillStyle, Color);

            GUIStyle TitleStyle = new GUIStyle(Style_LabelfieldBoldText);
            TitleStyle.contentOffset = Vector2.up * 3f;
            TitleStyle.fontSize = 12;
            TitleStyle.alignment = TextAnchor.MiddleLeft;

            GUIStyle BannerLogo = new GUIStyle(Style_Logo);
            BannerLogo.normal.textColor = TextColor;

            if (logo_width != 0)
                BannerLogo.fixedWidth = logo_width;

            if (logo_height != 0)
                BannerLogo.fixedHeight = logo_height;

            GUILayout.BeginHorizontal(BannerStyle);
            Color bgcol = GUI.color;
            GUI.color = XTween_Dashboard.Theme_Primary;
            GUILayout.Box(Logo, BannerLogo);
            GUI.color = bgcol;
            GUILayout.Space(10);
            GUILayout.Label(BannerText, TitleStyle);
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// 创造一个版头
        /// </summary>
        /// <param name="FillStyle">版头背景填充样式  实心 / 镂空</param>
        /// <param name="Color">背景颜色</param>
        /// <param name="BannerText">版头文字</param>
        public static void Gui_Layout_Banner(Texture2D Logo, GUIFilled FillStyle, GUIColor Color, string BannerText, Color TextColor, Texture2D LogoState, string StateText, float logo_width = 0, float logo_height = 0)
        {
            if (GUICreator == null)
            {
                Gui_Layout_Initia();
            }

            GUIStyle BannerStyle = new GUIStyle(GUICreator.GetStyle("Banner"));
            BannerStyle.normal.background = GetFillTexture(FillStyle, Color);

            GUIStyle TitleStyle = new GUIStyle(Style_LabelfieldBoldText);
            TitleStyle.contentOffset = Vector2.up * 3f;
            TitleStyle.fontSize = 12;

            GUIStyle BannerLogo = new GUIStyle(Style_Logo);
            BannerLogo.normal.textColor = TextColor;
            if (logo_width != 0)
                BannerLogo.fixedWidth = logo_width;

            if (logo_height != 0)
                BannerLogo.fixedHeight = logo_height;

            GUIStyle StateLogo = new GUIStyle(Style_LogoState);

            GUILayout.BeginHorizontal(BannerStyle);
            Color bgcol = GUI.color;
            GUI.color = XTween_Dashboard.Theme_Primary;
            GUILayout.Box(Logo, BannerLogo);
            GUI.color = bgcol;
            GUILayout.Space(10);
            GUILayout.Label(BannerText, TitleStyle);
            GUILayout.FlexibleSpace();

            GUILayout.Label(LogoState, StateLogo);

            GUILayout.Label(StateText, StateLogo);
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Labelfied
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Layout_Labelfield(string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            if (font != null)
                Style.font = font;
            GUILayout.Label(Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Layout_Labelfield(string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, bool Wrap = false, TextClipping Clipping = TextClipping.Overflow, TextAnchor Align = TextAnchor.MiddleCenter, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.wordWrap = Wrap;
            Style.clipping = Clipping;
            if (font != null)
                Style.font = font;
            GUILayout.Label(Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Layout_Labelfield(string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_LabelfieldOffset);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.contentOffset = Offset;
            Style.alignment = Align;
            if (font != null)
                Style.font = font;
            GUILayout.Label(Text, Style);
        }
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Layout_LabelfieldThin(string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, Vector2 Offset, int FontSize = 12, Font font = null)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.contentOffset = Offset;
            Style.alignment = Align;
            if (font != null)
                Style.font = font;
            GUILayout.Label(Text, Style);
        }
        #endregion

        #region Button
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="Text">按钮文字</param>
        /// <param name="FillStyle">按钮背景填充样式</param>
        /// <param name="Color">按钮背景填充颜色</param>
        /// <param name="ButtonTextColor">按钮文字</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(string Text, string Tooltip, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, int BtnHeight)
        {
            GUIStyle Style = new GUIStyle(Style_Button);
            if (FillStyle != GUIFilled.无)
            {
                Style.normal.background = GetBtnFillTexture(FillStyle, Color);
                Style.active.background = GetBtnFillTexture(FillStyle, Color);
            }
            else
            {
                Style.normal.background = null;
                Style.active.background = null;
            }
            Style.normal.textColor = ButtonTextColor;
            Style.fixedHeight = BtnHeight;

            GUIContent gui = new GUIContent();
            gui.text = Text;
            gui.tooltip = Tooltip;

            GUILayout.BeginHorizontal();
            //GUILayout.Space(5);
            bool sw = GUILayout.Button(gui, Style);
            //GUILayout.Space(5);
            GUILayout.EndHorizontal();
            return sw;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="Text">按钮文字</param>
        /// <param name="FillStyle">按钮背景填充样式</param>
        /// <param name="Color">按钮背景填充颜色</param>
        /// <param name="ButtonTextColor">按钮文字</param>
        /// <param name="font">字体</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(string Text, string Tooltip, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, int BtnHeight, Font font)
        {
            GUIStyle Style = new GUIStyle(Style_Button);
            if (FillStyle != GUIFilled.无)
            {
                Style.normal.background = GetBtnFillTexture(FillStyle, Color);
                Style.active.background = GetBtnFillTexture(FillStyle, Color);
            }
            else
            {
                Style.normal.background = null;
                Style.active.background = null;
            }
            Style.normal.textColor = ButtonTextColor;
            Style.fixedHeight = BtnHeight;

            Style.font = font;

            GUIContent gui = new GUIContent();
            gui.text = Text;
            gui.tooltip = Tooltip;

            GUILayout.BeginHorizontal();
            //GUILayout.Space(5);
            bool sw = GUILayout.Button(gui, Style);
            //GUILayout.Space(5);
            GUILayout.EndHorizontal();
            return sw;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="tex_release">松开图片</param>
        /// <param name="tex_press">按下图片</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(float ButtonSize, string Tooltip, Texture2D tex_release, Texture2D tex_press, float Offset = 0)
        {
            GUIStyle Style = new GUIStyle(Style_IconButton);
            Style.normal.background = tex_release;
            Style.active.background = tex_press;
            Style.fixedWidth = ButtonSize;
            Style.fixedHeight = ButtonSize;
            GUIContent gui = new GUIContent();
            gui.tooltip = Tooltip;
            GUILayout.BeginVertical();
            GUILayout.Space(Offset);
            bool sw = GUILayout.Button(gui, Style);
            GUILayout.EndVertical();
            return sw;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="Text">按钮文字</param>
        /// <param name="FillStyle">按钮背景填充样式</param>
        /// <param name="Color">按钮背景填充颜色</param>
        /// <param name="ButtonTextColor">按钮文字</param>
        /// <param name="ButtonHeight">按钮高度</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(string Text, string Tooltip, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, float ButtonHeight, RectOffset Margin, Vector2 Offset)
        {
            GUIStyle Style = new GUIStyle(Style_Button);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetBtnFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            Style.margin = Margin;
            Style.contentOffset = Offset; ;
            Style.normal.textColor = ButtonTextColor;
            Style.fixedHeight = ButtonHeight;
            GUILayout.BeginHorizontal();
            bool sw = GUILayout.Button(Text, Style);
            GUILayout.EndHorizontal();
            return sw;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="Text">按钮文字</param>
        /// <param name="FillStyle">按钮背景填充样式</param>
        /// <param name="Color">按钮背景填充颜色</param>
        /// <param name="ButtonTextColor">按钮文字</param>
        /// <param name="ButtonHeight">按钮高度</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(string Text, string Tooltip, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, float ButtonHeight, RectOffset Margin, Vector2 Offset, TextAnchor textanchor, int FontSize, Font Font)
        {
            GUIStyle Style = new GUIStyle(Style_Button);
            if (FillStyle != GUIFilled.无)
            {
                Style.normal.background = GetBtnFillTexture(FillStyle, Color);
                Style.active.background = GetBtnFillTexture(FillStyle, Color);
            }
            else
            {
                Style.normal.background = null;
            }
            Style.font = Font;
            Style.fontSize = FontSize;
            Style.margin = Margin;
            Style.contentOffset = Offset;
            Style.alignment = textanchor;
            Style.normal.textColor = ButtonTextColor;
            Style.fixedHeight = ButtonHeight;
            GUILayout.BeginHorizontal();
            bool sw = GUILayout.Button(Text, Style);
            GUILayout.EndHorizontal();
            return sw;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="Text">按钮文字</param>
        /// <param name="FillStyle">按钮背景填充样式</param>
        /// <param name="Color">按钮背景填充颜色</param>
        /// <param name="ButtonTextColor">按钮文字</param>
        /// <param name="Width">按钮宽度</param>
        /// <param name="Height">按钮高度</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(string Text, string Tooltip, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, int FontSize, float Width, float Height)
        {
            GUIStyle Style = new GUIStyle(Style_Button);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetBtnFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.fixedWidth = Width;
            Style.fixedHeight = Height;
            GUILayout.BeginHorizontal();

            GUIContent gui = new GUIContent();
            gui.text = Text;
            gui.tooltip = Tooltip;

            bool sw = GUILayout.Button(gui, Style);
            GUILayout.EndHorizontal();
            return sw;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="Text">按钮文字</param>
        /// <param name="FillStyle">按钮背景填充样式</param>
        /// <param name="Color">按钮背景填充颜色</param>
        /// <param name="ButtonTextColor">按钮文字</param>
        /// <param name="Width">按钮宽度</param>
        /// <param name="Height">按钮高度</param>
        /// <param name="ButtonTextFont">字体</param>
        /// <param name="FocusName">焦点名称</param>
        /// <returns>返回的布尔值用于判断该按钮是否被按下</returns>
        public static bool Gui_Layout_Button(string Text, string Tooltip, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, int FontSize, float Width, float Height, Font ButtonTextFont, string FocusName)
        {
            GUIStyle Style = new GUIStyle(Style_Button);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetBtnFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            Style.normal.textColor = ButtonTextColor;
            Style.font = ButtonTextFont;
            Style.fontSize = FontSize;
            Style.fixedWidth = Width;
            Style.fixedHeight = Height;
            GUILayout.BeginHorizontal();

            GUIContent gui = new GUIContent();
            gui.text = Text;
            gui.tooltip = Tooltip;
            // 为第一个 TextField 分配名称
            GUI.SetNextControlName(FocusName);
            bool sw = GUILayout.Button(gui, Style);
            GUILayout.EndHorizontal();
            return sw;
        }
        #endregion

        #region PropertyField
        /// <summary>
        /// 创造一个属性框
        /// </summary>
        public static void Gui_Layout_Property_Field(string TitleName, SerializedProperty value, float LabelWidth = 70, bool ClearGUIContent = false)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(5);
            float labwidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = LabelWidth;
            if (ClearGUIContent)
                EditorGUILayout.LabelField(TitleName);
            EditorGUILayout.PropertyField(value, ClearGUIContent ? GUIContent.none : new GUIContent(TitleName));
            EditorGUIUtility.labelWidth = labwidth;
            value.serializedObject.ApplyModifiedProperties();
            GUILayout.Space(5);
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// 创造一个属性框
        /// </summary>
        public static void Gui_Layout_Property_Field_WithIcon(string Text, Texture2D Icon, float IconSize, Vector2 IconOffset, ref SerializedProperty Val, float LabelWidth = 40)
        {
            GUILayout.BeginHorizontal();
            Gui_Layout_Icon(IconSize, Icon, IconOffset);
            GUILayout.Space(5);
            GUILayout.BeginVertical();
            GUILayout.Space(-2f);
            float labwidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = LabelWidth;
            EditorGUILayout.PropertyField(Val, new GUIContent(Text));
            Val.serializedObject.ApplyModifiedProperties();
            EditorGUIUtility.labelWidth = labwidth;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Popup
        /// <summary>
        /// 创建一个下拉菜单
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="Text"></param>
        /// <param name="Options"></param>
        /// <param name="Prop"></param>
        /// <param name="FillStyle"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="objects"></param>
        /// <param name="Callback_Multiple"></param>
        /// <param name="Callback_Single"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T Gui_Layout_Popup<T, TObject>(string Text, string[] Options, ref SerializedProperty Prop, GUIFilled FillStyle, float Width, float Height, TObject[] objects, Action<TObject[]> Callback_Multiple = null, Action<T> Callback_Single = null)
        {
            string fieldName = Prop.propertyPath;

            int selectedIndex = 0;
            bool allSame = true;

            GUIStyle Style = new GUIStyle(Style_ToolBar);
            Style.normal.background = GetBtnFillTexture(FillStyle, GUIColor.亮白);
            Style.onNormal.background = GetBtnFillTexture(FillStyle, GUIColor.亮白);
            Style.margin = new RectOffset(0, 0, -1, 0);
            if (XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary))
            {
                Style.normal.textColor = Color.black;
                Style.onNormal.textColor = Color.black;
            }
            else
            {
                Style.normal.textColor = Color.white;
                Style.onNormal.textColor = Color.white;
            }
            Style.fixedHeight = Height;

            GUILayout.BeginHorizontal();
            //GUILayout.Space(5);
            GUILayout.Label(Text, Style_OptionLabel);
            GUILayout.FlexibleSpace();

            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Primary;

            // 获取嵌套字段的值
            T firstValue = GetNestedFieldValue<T>(objects[0], fieldName);

            for (int i = 1; i < objects.Length; i++)
            {
                if (!GetNestedFieldValue<T>(objects[i], fieldName).Equals(firstValue))
                {
                    allSame = false;
                    break;
                }
            }

            if (allSame)
            {
                if (typeof(T) == typeof(int))
                {
                    selectedIndex = (int)(object)firstValue;
                }
                else if (typeof(T) == typeof(string))
                {
                    selectedIndex = Array.IndexOf(Options, (string)(object)firstValue);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type for return value. Only int and string are supported.");
                }
            }
            else
            {
                selectedIndex = -1; // "混合" 状态
            }

            EditorGUI.showMixedValue = !allSame; // 设置混合状态

            // 使用 EditorGUILayout.Popup 替代 GUILayout.Toolbar
            EditorGUI.BeginChangeCheck();
            int newIndex = EditorGUILayout.Popup(selectedIndex, Options, Style, GUILayout.MaxWidth(Width));
            EditorGUI.showMixedValue = false; // 恢复默认值

            if (EditorGUI.EndChangeCheck())
            {
                if (typeof(T) == typeof(int))
                {
                    T newValue = (T)(object)newIndex;
                    Prop.intValue = newIndex; // 设置 SerializedProperty 的值
                    foreach (var obj in objects)
                    {
                        SetNestedFieldValue(obj, fieldName, newValue);
                    }

                    Prop.serializedObject.ApplyModifiedProperties();

                    if (objects.Length > 1)
                    {
                        if (Callback_Multiple != null)
                            Callback_Multiple.Invoke(objects);
                    }
                    else
                    {
                        if (Callback_Single != null)
                            Callback_Single.Invoke(newValue);
                    }
                }
                else if (typeof(T) == typeof(string))
                {
                    T newValue = (T)(object)Options[newIndex];
                    Prop.stringValue = newValue as string; // 设置 SerializedProperty 的值
                    foreach (var obj in objects)
                    {
                        SetNestedFieldValue(obj, fieldName, newValue);
                    }

                    Prop.serializedObject.ApplyModifiedProperties();

                    if (objects.Length > 1)
                    {
                        if (Callback_Multiple != null)
                            Callback_Multiple.Invoke(objects);
                    }
                    else
                    {
                        if (Callback_Single != null)
                            Callback_Single.Invoke(newValue);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type for return value. Only int and string are supported.");
                }

            }

            GUI.backgroundColor = bgcol;
            GUILayout.EndHorizontal();
            GUILayout.Space(2);

            // 根据泛型类型 TArg 返回相应的值
            if (typeof(T) == typeof(int))
            {
                return (T)(object)newIndex;
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)(object)(newIndex >= 0 && newIndex < Options.Length ? Options[newIndex] : "混合");
            }
            else
            {
                throw new InvalidOperationException("Unsupported type for return value. Only int and string are supported.");
            }
        }
        /// <summary>
        /// 创建一个下拉菜单
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="Text"></param>
        /// <param name="Options"></param>
        /// <param name="Prop"></param>
        /// <param name="FillStyle"></param>
        /// <param name="BgColor"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="objects"></param>
        /// <param name="Callback_Multiple"></param>
        /// <param name="Callback_Single"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T Gui_Layout_Popup<T, TObject>(string Text, string[] Options, ref SerializedProperty Prop, GUIFilled FillStyle, Color BgColor, float Width, float Height, TObject[] objects, Action<TObject[]> Callback_Multiple = null, Action<T> Callback_Single = null)
        {
            string fieldName = Prop.propertyPath;

            int selectedIndex = 0;
            bool allSame = true;

            GUIStyle Style = new GUIStyle(Style_ToolBar);
            Style.normal.background = GetBtnFillTexture(FillStyle, GUIColor.亮白);
            Style.onNormal.background = GetBtnFillTexture(FillStyle, GUIColor.亮白);
            Style.margin = new RectOffset(0, 0, -1, 0);
            if (XTween_Utilitys.GetColorBrightnessLimite(BgColor))
            {
                Style.normal.textColor = Color.black;
                Style.onNormal.textColor = Color.black;
            }
            else
            {
                Style.normal.textColor = Color.white;
                Style.onNormal.textColor = Color.white;
            }
            Style.fixedHeight = Height;

            GUILayout.BeginHorizontal();
            //GUILayout.Space(5);
            //GUILayout.Label(Text, Style_OptionLabel);
            GUILayout.FlexibleSpace();

            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = BgColor;

            // 获取嵌套字段的值
            T firstValue = GetNestedFieldValue<T>(objects[0], fieldName);

            for (int i = 1; i < objects.Length; i++)
            {
                if (!GetNestedFieldValue<T>(objects[i], fieldName).Equals(firstValue))
                {
                    allSame = false;
                    break;
                }
            }

            if (allSame)
            {
                if (typeof(T) == typeof(int))
                {
                    selectedIndex = (int)(object)firstValue;
                }
                else if (typeof(T) == typeof(string))
                {
                    selectedIndex = Array.IndexOf(Options, (string)(object)firstValue);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type for return value. Only int and string are supported.");
                }
            }
            else
            {
                selectedIndex = -1; // "混合" 状态
            }

            EditorGUI.showMixedValue = !allSame; // 设置混合状态

            // 使用 EditorGUILayout.Popup 替代 GUILayout.Toolbar
            EditorGUI.BeginChangeCheck();
            float labwidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 40;
            int newIndex = EditorGUILayout.Popup(Text, selectedIndex, Options, Style, GUILayout.MaxWidth(Width));
            EditorGUIUtility.labelWidth = labwidth;
            EditorGUI.showMixedValue = false; // 恢复默认值

            if (EditorGUI.EndChangeCheck())
            {
                if (typeof(T) == typeof(int))
                {
                    T newValue = (T)(object)newIndex;
                    Prop.intValue = newIndex; // 设置 SerializedProperty 的值
                    foreach (var obj in objects)
                    {
                        SetNestedFieldValue(obj, fieldName, newValue);
                    }

                    Prop.serializedObject.ApplyModifiedProperties();

                    if (objects.Length > 1)
                    {
                        if (Callback_Multiple != null)
                            Callback_Multiple.Invoke(objects);
                    }
                    else
                    {
                        if (Callback_Single != null)
                            Callback_Single.Invoke(newValue);
                    }
                }
                else if (typeof(T) == typeof(string))
                {
                    T newValue = (T)(object)Options[newIndex];
                    Prop.stringValue = newValue as string; // 设置 SerializedProperty 的值
                    foreach (var obj in objects)
                    {
                        SetNestedFieldValue(obj, fieldName, newValue);
                    }

                    Prop.serializedObject.ApplyModifiedProperties();

                    if (objects.Length > 1)
                    {
                        if (Callback_Multiple != null)
                            Callback_Multiple.Invoke(objects);
                    }
                    else
                    {
                        if (Callback_Single != null)
                            Callback_Single.Invoke(newValue);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type for return value. Only int and string are supported.");
                }

            }

            GUI.backgroundColor = bgcol;
            GUILayout.EndHorizontal();
            GUILayout.Space(2);

            // 根据泛型类型 TArg 返回相应的值
            if (typeof(T) == typeof(int))
            {
                return (T)(object)newIndex;
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)(object)(newIndex >= 0 && newIndex < Options.Length ? Options[newIndex] : "混合");
            }
            else
            {
                throw new InvalidOperationException("Unsupported type for return value. Only int and string are supported.");
            }
        }
        #endregion

        #region Toggle
        /// <summary>
        /// 创建一个拨动开关
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="Text"></param>
        /// <param name="Options"></param>
        /// <param name="Prop"></param>
        /// <param name="FillStyle_Normal"></param>
        /// <param name="FillStyle_Selected"></param>
        /// <param name="TextColor_Normal"></param>
        /// <param name="TextColor_Selected"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static T Gui_Layout_Toggle<T, TObject>(string Text, string[] Options, ref SerializedProperty Prop, GUIFilled FillStyle_Normal, GUIFilled FillStyle_Selected, Color TextColor_Normal, float Width, float Height, TObject[] objects, Action<TObject[]> Callback_Multiple = null, Action<T> Callback_Single = null)
        {
            string fieldName = Prop.propertyPath;

            T result = default(T);
            int x = 0;

            GUIStyle Style = new GUIStyle(Style_ToolBar);
            Style.normal.background = GetBtnFillTexture(FillStyle_Normal, GUIColor.无);
            Style.onNormal.background = GetBtnFillTexture(FillStyle_Selected, GUIColor.亮白);
            Style.margin = new RectOffset(0, 0, -1, 0);
            Style.normal.textColor = TextColor_Normal;
            if (XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary))
            {
                Style.onNormal.textColor = Color.black;
            }
            else
            {
                Style.onNormal.textColor = Color.white;
            }
            Style.fixedHeight = Height;

            GUILayout.BeginHorizontal();
            //GUILayout.Space(5);
            GUILayout.Label(Text, Style_OptionLabel);
            GUILayout.FlexibleSpace();

            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Primary;

            if (objects.Length > 1)
            {
                int Index = 0;

                // 检查多选时是否所有对象的字段值一致
                bool allSame = true;
                T firstValue = GetNestedFieldValue<T>(objects[0], fieldName);

                for (int i = 1; i < objects.Length; i++)
                {
                    if (!GetNestedFieldValue<T>(objects[i], fieldName).Equals(firstValue))
                    {
                        allSame = false;
                        break;
                    }
                }

                if (allSame)
                {
                    if (firstValue.Equals(default(T)))
                        Index = 0;
                    else
                        Index = 1;
                }

                EditorGUI.BeginChangeCheck();
                string[] m_Options = new string[Options.Length + 1];
                if (allSame)
                {
                    m_Options = Options;
                }
                else
                {
                    m_Options[0] = "—";
                    Array.Copy(Options, 0, m_Options, 1, Options.Length);
                }
                int xxx = GUILayout.Toolbar(Index, m_Options, Style, GUILayout.MaxWidth(Width));
                if (EditorGUI.EndChangeCheck())
                {
                    if (allSame)
                    {
                        if (xxx == 0)
                        {
                            T value = default(T);
                            Prop.SetValue(value);
                            foreach (var obj in objects)
                            {
                                SetNestedFieldValue(obj, fieldName, value);
                            }
                            result = value;
                        }
                        else if (xxx == 1)
                        {
                            T value = (T)Convert.ChangeType(true, typeof(T)); // 假设选中状态对应 true
                            Prop.SetValue(value);
                            foreach (var obj in objects)
                            {
                                SetNestedFieldValue(obj, fieldName, value);
                            }
                            result = value;
                        }
                    }
                    else
                    {
                        if (xxx == 1)
                        {
                            T value = default(T);
                            Prop.SetValue(value);
                            foreach (var obj in objects)
                            {
                                SetNestedFieldValue(obj, fieldName, value);
                            }
                            result = value;
                        }
                        else if (xxx == 2)
                        {
                            T value = (T)Convert.ChangeType(true, typeof(T)); // 假设选中状态对应 true
                            Prop.SetValue(value);
                            foreach (var obj in objects)
                            {
                                SetNestedFieldValue(obj, fieldName, value);
                            }
                            result = value;
                        }
                    }
                    Prop.serializedObject.ApplyModifiedProperties();

                    if (Callback_Multiple != null)
                        Callback_Multiple.Invoke(objects);
                }
            }
            else
            {
                T value = GetNestedFieldValue<T>(objects[0], fieldName);
                if (value.Equals(default(T)))
                    x = 0;
                else
                    x = 1;
                x = GUILayout.Toolbar(x, Options, Style, GUILayout.MaxWidth(Width));
                if (x == 0)
                {
                    T setValue = default(T);
                    Prop.SetValue(setValue);
                    SetNestedFieldValue(objects[0], fieldName, setValue);
                    result = setValue;
                }
                else if (x == 1)
                {
                    T setValue = (T)Convert.ChangeType(true, typeof(T)); // 假设选中状态对应 true
                    Prop.SetValue(setValue);
                    SetNestedFieldValue(objects[0], fieldName, setValue);
                    result = setValue;
                }
                Prop.serializedObject.ApplyModifiedProperties();


                if (Callback_Single != null)
                    Callback_Single.Invoke(result);
            }
            GUI.backgroundColor = bgcol;
            GUILayout.EndHorizontal();
            GUILayout.Space(2);

            return result;
        }
        #endregion

        #region Slider
        /// <summary>
        /// 创造一个Gui滑动条
        /// </summary>
        public static float Gui_Layout_Slider(string Text, float Value, float LeftVal, float RightVal)
        {
            return EditorGUILayout.Slider(new GUIContent(Text), Value, LeftVal, RightVal);
        }
        /// <summary>
        /// 创造一个Gui滑动条 - 大小范围
        /// </summary>
        public static void Gui_Layout_SliderMinMax(string Text, ref float Min, ref float Max, float MinLimite, float MaxLimite)
        {
            EditorGUILayout.MinMaxSlider(new GUIContent(Text), ref Min, ref Max, MinLimite, MaxLimite);
        }
        #endregion

        #region TextArea
        /// <summary>
        /// 创造一个Gui标签框 - 字符串内容
        /// </summary>
        /// <param name="Text">输入框标题</param>
        public static void Gui_Layout_TextArea_Wrap(string Text, GUIFilled FillStyle, GUIColor Color, Color ButtonTextColor, TextAnchor Align, float Width, int FontSize = 12)
        {
            GUIStyle Style = new GUIStyle(Style_Labelfield);
            Style.normal.background = GetFillTexture(FillStyle, Color);
            Style.normal.textColor = ButtonTextColor;
            Style.fontSize = FontSize;
            Style.alignment = Align;
            Style.wordWrap = true;
            Style.richText = true;
            GUILayout.Label(Text, Style, GUILayout.MaxWidth(Width));
        }
        #endregion

        /// <summary>
        /// 分割线
        /// </summary>
        /// <param name="width">宽度</param>
        public static void Gui_Layout_Seperator(float width, Color color)
        {
            GUIStyle Style = new GUIStyle(Style_Seperator);
            Style.fixedHeight = width;
            //Style.margin = new RectOffset(35, 35, 10, 5);
            GUI.color = color;
            GUILayout.Box("", Style);
            GUI.color = Color.white;
        }
        /// <summary>
        /// 创造一个按钮
        /// </summary>
        /// <param name="icon">图片</param>
        /// <returns></returns>
        public static void Gui_Layout_Icon(float IconSize, Texture2D icon, Vector2 Offset)
        {
            GUIStyle Style = new GUIStyle(Style_Icon);
            Style.fixedWidth = IconSize;
            Style.fixedHeight = IconSize;
            Style.contentOffset = Offset;
            GUILayout.Label(icon, Style, GUILayout.MaxWidth(IconSize), GUILayout.MinWidth(0));
        }
        #endregion

        #region GUILayout布局
        /// <summary>
        /// 排版布局 - 横向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        public static void Gui_Layout_Horizontal_Start(GUIFilled FillStyle, GUIColor Color)
        {
            GUIStyle Style = new GUIStyle(Style_Group);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            GUILayout.BeginHorizontal(Style);
        }
        /// <summary>
        /// 排版布局 - 横向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        public static void Gui_Layout_Horizontal_Start(GUIFilled FillStyle, GUIColor Color, Color AddedColor, float Space)
        {
            GUIStyle Style = new GUIStyle(Style_Group);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = AddedColor;
            GUILayout.BeginHorizontal(Style);
            GUI.backgroundColor = bgcol;
            Gui_Layout_Space(Space);
        }
        /// <summary>
        /// 排版布局 - 横向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        public static void Gui_Layout_Horizontal_Start(GUIFilled FillStyle, GUIColor Color, float Space)
        {
            GUIStyle Style = new GUIStyle(Style_Group);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            GUILayout.BeginHorizontal(Style);
            Gui_Layout_Space(Space);
        }
        /// <summary>
        /// 排版布局 - 横向 - 终点
        /// </summary>
        public static void Gui_Layout_Horizontal_End(float Space = 0)
        {
            Gui_Layout_Space(Space);
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// 排版布局 - 竖向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        /// <summary>
        public static void Gui_Layout_Vertical_Start(GUIFilled FillStyle, GUIColor Color, float Margin = 0)
        {
            GUIStyle Style = new GUIStyle(Style_Group);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Group;
            GUILayout.BeginVertical(Style);
            GUI.backgroundColor = bgcol;
            Gui_Layout_Space(Margin);
        }
        /// <summary>
        /// 排版布局 - 竖向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        /// <param name="Margin">间距</param>
        /// <param name="Title">标题</param>
        public static void Gui_Layout_Vertical_Start(GUIFilled FillStyle, GUIColor Color, float Margin, string Title, Color TitleColor)
        {
            GUIStyle Style = new GUIStyle(Style_Group_Half);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color, true);
            else
                Style.normal.background = null;
            GUILayout.Space(5 + Margin);
            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Group;
            Gui_Layout_Labelfield(Title, GUIFilled.无, GUIColor.无, TitleColor, TextAnchor.MiddleLeft, new Vector2(20, 2), 12);
            GUILayout.BeginVertical(Style);
            GUI.backgroundColor = bgcol;
            Gui_Layout_Space(Margin);
        }
        /// <summary>
        /// 排版布局 - 竖向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        /// <param name="Margin">间距</param>
        /// <param name="Title">标题</param>
        public static bool Gui_Layout_Vertical_Start_WithFolder(GUIFilled FillStyle, GUIColor Color, float Margin, string Title, Color TitleColor, Color TitleHoverColor, Color TitleActiveColor, RectOffset Btn_Margin, Vector2 Btn_Offset, Texture2D Icon, bool fold, float hideIconThreshold)
        {
            GUIStyle Style = new GUIStyle(Style_Group_Half);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color, true);
            else
                Style.normal.background = null;
            GUILayout.Space(5 + Margin);
            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Group;
            if (Gui_Layout_Button(Title, "", GUIFilled.透明, GUIColor.无, TitleColor, 20, Btn_Margin, Btn_Offset, TextAnchor.MiddleLeft, 12, GetFont("SS_Editor_Light")))
            {
                fold = !fold;
            }
            Rect rect = GUILayoutUtility.GetLastRect();

            GUILayout.BeginVertical(Style);
            Gui_Layout_Space(Margin);
            GUI.backgroundColor = bgcol;
            if (!fold)
                if (hideIconThreshold > 205)
                    Gui_Icon(new Rect(rect.width - 35, rect.y - 5, 20, 20), Icon);
            return fold;
        }
        /// <summary>
        /// 排版布局 - 竖向 - 起点
        /// </summary>
        /// <param name="FillStyle">填充样式  Solid=实心   Edge=空心</param>
        /// <param name="Color">填充颜色</param>
        /// <param name="Space">内部距离</param>
        /// <param name="Margin">外部间距</param>
        public static void Gui_Layout_Vertical_Start(GUIFilled FillStyle, GUIColor Color, float Space, RectOffset Margin)
        {
            GUIStyle Style = new GUIStyle(Style_Group);
            if (FillStyle != GUIFilled.无)
                Style.normal.background = GetFillTexture(FillStyle, Color);
            else
                Style.normal.background = null;
            Style.margin = Margin;
            Color bgcol = GUI.backgroundColor;
            GUI.backgroundColor = XTween_Dashboard.Theme_Group;
            GUILayout.BeginVertical(Style);
            GUI.backgroundColor = bgcol;
            Gui_Layout_Space(Space);
        }
        /// <summary>
        /// 排版布局 - 竖向 - 终点
        /// </summary>
        public static void Gui_Layout_Vertical_End(float Margin = 0)
        {
            Gui_Layout_Space(Margin);
            GUILayout.EndVertical();
        }
        #endregion

        #region 通用反射方法
        /// <summary>
        /// 获取嵌套字段的值
        /// </summary>
        /// <typeparam name="T">字段值的类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <param name="fieldPath">字段路径（支持嵌套，如 "TextStyleInfo.tmp_rich"）</param>
        /// <returns>字段值</returns>
        private static T GetNestedFieldValue<T>(object obj, string fieldPath)
        {
            return (T)GetNestedField(obj, fieldPath);
        }
        /// <summary>
        /// 设置嵌套字段的值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="fieldPath">字段路径（支持嵌套，如 "TextStyleInfo.tmp_rich"）</param>
        /// <param name="value">要设置的值</param>
        private static void SetNestedFieldValue(object obj, string fieldPath, object value)
        {
            SetNestedField(obj, fieldPath, value);
        }
        /// <summary>
        /// 递归获取嵌套字段的值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="fieldPath">字段路径（支持嵌套，如 "TextStyleInfo.tmp_rich"）</param>
        /// <returns>字段值</returns>
        private static object GetNestedField(object obj, string fieldPath)
        {
            string[] fields = fieldPath.Split('.');
            object current = obj;
            foreach (string field in fields)
            {
                FieldInfo fieldInfo = current.GetType().GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo == null)
                {
                    throw new ArgumentException($"Field '{field}' not found in type '{current.GetType().Name}'");
                }
                current = fieldInfo.GetValue(current);
            }
            return current;
        }
        /// <summary>
        /// 递归设置嵌套字段的值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="fieldPath">字段路径（支持嵌套，如 "TextStyleInfo.tmp_rich"）</param>
        /// <param name="value">要设置的值</param>
        private static void SetNestedField(object obj, string fieldPath, object value)
        {
            string[] fields = fieldPath.Split('.');
            object current = obj;
            for (int i = 0; i < fields.Length - 1; i++)
            {
                FieldInfo fieldInfo = current.GetType().GetField(fields[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo == null)
                {
                    throw new ArgumentException($"Field '{fields[i]}' not found in type '{current.GetType().Name}'");
                }
                current = fieldInfo.GetValue(current);
            }
            FieldInfo lastFieldInfo = current.GetType().GetField(fields.Last(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (lastFieldInfo == null)
            {
                throw new ArgumentException($"Field '{fields.Last()}' not found in type '{current.GetType().Name}'");
            }
            lastFieldInfo.SetValue(current, value);
        }
        #endregion

        #region CustomUtilitys
        /// <summary>
        /// 状态控件 - 文字值
        /// </summary>
        /// <param name="Icon"></param>
        /// <param name="IconSize"></param>
        /// <param name="IconOffset"></param>
        /// <param name="Title"></param>
        /// <param name="TitleFontSize"></param>
        /// <param name="Value"></param>
        /// <param name="ValueColor"></param>
        /// <param name="ValueFontSize"></param>
        public static void StatuDisplayer_text(Texture2D Icon, float IconSize, Vector2 IconOffset, string Title, int TitleFontSize, string Value, Color ValueColor, int ValueFontSize, bool UseTitleLogo = true)
        {
            Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            if (UseTitleLogo)
            {
                Gui_Layout_Space(5);
                Gui_Layout_Icon(IconSize, Icon, IconOffset);
                Gui_Layout_Space(10);
            }
            else
            {
                Gui_Layout_Space(5);
            }
            Gui_Layout_LabelfieldThin(Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, new Vector2(0, 0), TitleFontSize);
            Gui_Layout_FlexSpace();
            Gui_Layout_LabelfieldThin(Value, GUIFilled.无, GUIColor.无, ValueColor, TextAnchor.MiddleRight, new Vector2(0, 0), ValueFontSize);
            Gui_Layout_Space(5);
            Gui_Layout_Horizontal_End();
        }
        /// <summary>
        /// 状态控件 - 图片值
        /// </summary>
        /// <param name="Icon"></param>
        /// <param name="IconSize"></param>
        /// <param name="IconOffset"></param>
        /// <param name="Title"></param>
        /// <param name="TitleFontSize"></param>
        /// <param name="Value"></param>
        /// <param name="ValueOffset"></param>
        public static void StatuDisplayer_tex(Texture2D Icon, float IconSize, Vector2 IconOffset, string Title, int TitleFontSize, Texture2D Value, float ValueSize, Vector2 ValueOffset)
        {
            Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            Gui_Layout_Space(5);
            Gui_Layout_Icon(IconSize, Icon, IconOffset);
            Gui_Layout_Space(10);
            Gui_Layout_LabelfieldThin(Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, new Vector2(0, 0), TitleFontSize);
            Gui_Layout_FlexSpace();
            Gui_Layout_Icon(ValueSize, Value, ValueOffset);
            Gui_Layout_Space(5);
            Gui_Layout_Horizontal_End();
        }
        /// <summary>
        /// 状态控件 - 颜色值
        /// </summary>
        /// <param name="Icon"></param>
        /// <param name="IconSize"></param>
        /// <param name="IconOffset"></param>
        /// <param name="Title"></param>
        /// <param name="TitleFontSize"></param>
        /// <param name="Color"></param>
        public static void StatuDisplayer_color(Texture2D Icon, float IconSize, Vector2 IconOffset, string Title, int TitleFontSize, Color Color)
        {
            Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            Gui_Layout_Space(5);
            Gui_Layout_Icon(IconSize, Icon, IconOffset);
            Gui_Layout_Space(10);
            Gui_Layout_LabelfieldThin(Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, new Vector2(0, 0), TitleFontSize);
            Gui_Layout_FlexSpace();
            GUI.color = Color;
            Gui_Layout_Icon(IconSize, GetIcon("ColorRect"), IconOffset);
            GUI.color = Color.white;
            Gui_Layout_Space(5);
            Gui_Layout_Horizontal_End();
        }
        /// <summary>
        /// 状态控件 - 图标
        /// </summary>
        /// <param name="Icon"></param>
        /// <param name="IconSize"></param>
        /// <param name="IconOffset"></param>
        /// <param name="Title"></param>
        /// <param name="TitleFontSize"></param>
        /// <param name="Color"></param>
        /// <param name="StateIcon"></param>
        /// <param name="StateIconSize"></param>
        /// <param name="StateIconOffset"></param>
        public static void StatuDisplayer_icon(Texture2D Icon, float IconSize, Vector2 IconOffset, string Title, int TitleFontSize, Color Color, Texture2D StateIcon, float StateIconSize, Vector2 StateIconOffset, bool UseTitleLogo = true)
        {
            Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            if (UseTitleLogo)
            {
                Gui_Layout_Space(5);
                Gui_Layout_Icon(IconSize, Icon, IconOffset);
                Gui_Layout_Space(10);
            }
            else
            {
                Gui_Layout_Space(5);
            }
            Gui_Layout_LabelfieldThin(Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, new Vector2(0, 0), TitleFontSize);
            Gui_Layout_FlexSpace();
            GUI.color = Color;
            Gui_Layout_Icon(StateIconSize, StateIcon, StateIconOffset);
            GUI.color = Color.white;
            Gui_Layout_Space(5);
            Gui_Layout_Horizontal_End();
        }
        /// <summary>
        /// 状态控件 - 按钮
        /// </summary>
        /// <param name="Icon"></param>
        /// <param name="IconSize"></param>
        /// <param name="IconOffset"></param>
        /// <param name="Title"></param>
        /// <param name="TitleFontSize"></param>
        /// <param name="Value"></param>
        /// <param name="ValueColor"></param>
        /// <param name="ValueFontSize"></param>
        public static bool StatuDisplayer_btn(Texture2D Icon, float IconSize, Vector2 IconOffset, string Title, int TitleFontSize, string Value, Color ValueColor, int ValueFontSize, Color ButtonColor, float ButtonSize, string ButtonToolTip, Texture2D Button_Release, Texture2D Button_Press, float Button_Offset)
        {
            Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            Gui_Layout_Space(5);
            Gui_Layout_Icon(IconSize, Icon, IconOffset);
            Gui_Layout_Space(10);
            Gui_Layout_LabelfieldThin(Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, new Vector2(0, 0), TitleFontSize);
            Gui_Layout_FlexSpace();
            Gui_Layout_LabelfieldThin(Value, GUIFilled.无, GUIColor.无, ValueColor, TextAnchor.MiddleRight, new Vector2(0, 0), ValueFontSize);
            Gui_Layout_Space(10);
            bool sw = Gui_Layout_Button(ButtonSize, ButtonToolTip, Button_Release, Button_Press, Button_Offset);
            Gui_Layout_Space(5);
            Gui_Layout_Horizontal_End();

            return sw;
        }
        /// <summary>
        /// 状态控件 - 物体
        /// </summary>
        /// <param name="Icon"></param>
        /// <param name="IconSize"></param>
        /// <param name="IconOffset"></param>
        /// <param name="Title"></param>
        /// <param name="TitleFontSize"></param>
        /// <param name="Icon_status"></param>
        /// <param name="Icon_statusOffset"></param>
        /// <param name="status"></param>
        /// <param name="status_true"></param>
        /// <param name="status_false"></param>
        public static void StatuDisplayer_Object(Texture2D Icon, float IconSize, Vector2 IconOffset, string Title, int TitleFontSize, Vector2 TitleOffset, Texture2D Icon_status, Vector2 Icon_statusOffset, bool status, Color status_true, Color status_false, SerializedProperty Property, bool UseTitleLogo = true)
        {
            Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            if (UseTitleLogo)
            {
                Gui_Layout_Space(5);
                Gui_Layout_Icon(IconSize, Icon, IconOffset);
                Gui_Layout_Space(10);
            }
            else
            {
                Gui_Layout_Space(5);
            }
            Gui_Layout_LabelfieldThin(Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, TitleOffset, TitleFontSize);
            Gui_Layout_FlexSpace();
            Gui_Layout_Property_Field("", Property);
            Gui_Layout_Space(10);
            if (status)
                GUI.color = status_true;
            else
                GUI.color = status_false;
            Gui_Layout_Icon(IconSize, Icon_status, Icon_statusOffset);
            GUI.color = Color.white;
            Gui_Layout_Space(5);
            Gui_Layout_Horizontal_End();
        }
        #endregion

        #region Dialog对话框
        #region 文字类消息
        /// <summary>
        /// 打开一个对话框 - 自定义节点
        /// </summary>   
        /// <param name="info"></param>
        public static string Open(XTweenDialogInfo info)
        {
            if (info.options.Length > 5)
                return "超出最大选项按钮数量";
            if (info.options.Length <= 0)
                return "最小选项按钮数量应为1";

            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(info.windowtitle);

            if (info.options.Length == 5)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.五个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };
                window.Callback_Other = (d) => { res = d; };
                window.Callback_Special = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], info.options[2], info.options[3], info.options[4], info.PrimaryIndex);
            }
            else if (info.options.Length == 4)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.四个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };
                window.Callback_Other = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], info.options[2], info.options[3], "", info.PrimaryIndex);
            }
            else if (info.options.Length == 3)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.三个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], info.options[2], "", "", info.PrimaryIndex);
            }
            else if (info.options.Length == 2)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.两个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], "", "", "", info.PrimaryIndex);
            }
            else if (info.options.Length == 1)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.单个按钮;
                window.Callback_Ok = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], "", "", "", "", info.PrimaryIndex);
            }

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            // 显示模态窗口弹窗
            window.ShowModal();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 数组按钮
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="options"></param>
        /// <param name="PrimaryIndex"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogType type, string windowtitle, string title, string msg, string[] options, int PrimaryIndex = 0, bool usemodal = true)
        {
            if (options.Length > 5)
                return "超出最大选项按钮数量";
            if (options.Length <= 0)
                return "最小选项按钮数量应为1";

            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);

            if (options.Length == 5)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.五个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };
                window.Callback_Other = (d) => { res = d; };
                window.Callback_Special = (d) => { res = d; };

                window.SetInfo(type, title, msg, options[0], options[1], options[2], options[3], options[4], PrimaryIndex);
            }
            else if (options.Length == 4)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.四个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };
                window.Callback_Other = (d) => { res = d; };

                window.SetInfo(type, title, msg, options[0], options[1], options[2], options[3], "", PrimaryIndex);
            }
            else if (options.Length == 3)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.三个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };

                window.SetInfo(type, title, msg, options[0], options[1], options[2], "", "", PrimaryIndex);
            }
            else if (options.Length == 2)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.两个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };

                window.SetInfo(type, title, msg, options[0], options[1], "", "", "", PrimaryIndex);
            }
            else if (options.Length == 1)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.单个按钮;
                window.Callback_Ok = (d) => { res = d; };

                window.SetInfo(type, title, msg, options[0], "", "", "", "", PrimaryIndex);
            }

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 确认、取消、辅助、其他、特别
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="alt"></param>
        /// <param name="other"></param>
        /// <param name="special"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, string alt, string other, string special, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);

            window.XTweenDialogButtonMode = XTweenDialogButtonMode.五个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.Callback_Alt = (r) => { res = r; };
            window.Callback_Other = (r) => { res = r; };
            window.Callback_Special = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, alt, other, special, PrimaryIndex);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 确认、取消、辅助、其他
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="alt"></param>
        /// <param name="other"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, string alt, string other, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);

            window.XTweenDialogButtonMode = XTweenDialogButtonMode.四个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.Callback_Alt = (r) => { res = r; };
            window.Callback_Other = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, alt, other, "", PrimaryIndex);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 确认、取消、辅助
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="alt"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, string alt, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);

            window.XTweenDialogButtonMode = XTweenDialogButtonMode.三个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.Callback_Alt = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, alt, "", "", PrimaryIndex);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 确认、取消
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);

            window.XTweenDialogButtonMode = XTweenDialogButtonMode.两个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, "", "", "", PrimaryIndex);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 确认
        /// </summary>   
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogType type, string windowtitle, string title, string msg, string ok, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);

            window.XTweenDialogButtonMode = XTweenDialogButtonMode.单个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, "", "", "", "", PrimaryIndex);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        #endregion

        #region 数据列表类消息
        /// <summary>
        /// 打开一个对话框 - 列表数据 - 确认
        /// </summary>   
        /// <param name="datas"></param>
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogListDatas[] datas, XTweenDialogType type, string windowtitle, string title, string msg, string ok, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);
            window.XTweenDialogButtonMode = XTweenDialogButtonMode.单个按钮;
            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 420), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, "", "", "", "", PrimaryIndex);

            window.SetList(datas);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 列表数据 - 确认、取消
        /// </summary>   
        /// <param name="datas"></param>
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogListDatas[] datas, XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);
            window.XTweenDialogButtonMode = XTweenDialogButtonMode.两个按钮;
            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 420), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, "", "", "", PrimaryIndex);
            window.SetList(datas);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 列表数据 - 确认、取消、辅助
        /// </summary>   
        /// <param name="datas"></param>
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="alt"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogListDatas[] datas, XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, string alt, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);
            window.XTweenDialogButtonMode = XTweenDialogButtonMode.三个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 420), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.Callback_Alt = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, alt, "", "", PrimaryIndex);
            window.SetList(datas);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 列表数据 - 确认、取消、辅助、其他
        /// </summary>   
        /// <param name="datas"></param>
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="alt"></param>
        /// <param name="other"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogListDatas[] datas, XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, string alt, string other, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);
            window.XTweenDialogButtonMode = XTweenDialogButtonMode.四个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 420), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.Callback_Alt = (r) => { res = r; };
            window.Callback_Other = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, alt, other, "", PrimaryIndex);
            window.SetList(datas);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 列表数据 - 确认、取消、辅助、其他、特别
        /// </summary>   
        /// <param name="datas"></param>
        /// <param name="type"></param>
        /// <param name="windowtitle"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="ok"></param>
        /// <param name="cancel"></param>
        /// <param name="alt"></param>
        /// <param name="other"></param>
        /// <param name="special"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogListDatas[] datas, XTweenDialogType type, string windowtitle, string title, string msg, string ok, string cancel, string alt, string other, string special, int PrimaryIndex = 0, bool usemodal = true)
        {
            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(windowtitle);
            window.XTweenDialogButtonMode = XTweenDialogButtonMode.五个按钮;

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 420), window);

            // 回调消息接收
            window.Callback_Ok = (r) => { res = r; };
            window.Callback_Cancel = (r) => { res = r; };
            window.Callback_Alt = (r) => { res = r; };
            window.Callback_Other = (r) => { res = r; };
            window.Callback_Special = (r) => { res = r; };
            window.SetInfo(type, title, msg, ok, cancel, alt, other, special, PrimaryIndex);
            window.SetList(datas);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        /// <summary>
        /// 打开一个对话框 - 列表数据 - 自定义节点
        /// </summary>   
        /// <param name="info"></param>
        /// <param name="datas"></param>
        /// <param name="usemodal"></param>
        public static string Open(XTweenDialogInfo info, XTweenDialogListDatas[] datas, bool usemodal = true)
        {
            if (info.options.Length > 5)
                return "超出最大选项按钮数量";
            if (info.options.Length <= 0)
                return "最小选项按钮数量应为1";

            string res = "";
            Editor_XTween_DialogGUI window = EditorWindow.GetWindow<Editor_XTween_DialogGUI>(true);
            window.titleContent = new GUIContent(info.windowtitle);

            if (info.options.Length == 5)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.五个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };
                window.Callback_Other = (d) => { res = d; };
                window.Callback_Special = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], info.options[2], info.options[3], info.options[4], info.PrimaryIndex);
            }
            else if (info.options.Length == 4)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.四个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };
                window.Callback_Other = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], info.options[2], info.options[3], "", info.PrimaryIndex);
            }
            else if (info.options.Length == 3)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.三个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };
                window.Callback_Alt = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], info.options[2], "", "", info.PrimaryIndex);
            }
            else if (info.options.Length == 2)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.两个按钮;
                window.Callback_Ok = (d) => { res = d; };
                window.Callback_Cancel = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], info.options[1], "", "", "", info.PrimaryIndex);
            }
            else if (info.options.Length == 1)
            {
                window.XTweenDialogButtonMode = XTweenDialogButtonMode.单个按钮;
                window.Callback_Ok = (d) => { res = d; };

                window.SetInfo(info.type, info.title, info.msg, info.options[0], "", "", "", "", info.PrimaryIndex);
            }
            window.SetList(datas);

            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(545, 235), window);

            if (usemodal)
                // 显示模态窗口弹窗
                window.ShowModal();
            else
                window.Show();

            // 反馈选择消息
            return res;
        }
        #endregion    
        #endregion

        #region EditorData 数据传输中转操作
        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool EditorData_Has_String(string key)
        {
            if (EditorPrefs.HasKey(key))
                return true;
            else return false;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EditorData_Get_With_String(string key)
        {
            if (EditorPrefs.HasKey(key))
                return EditorPrefs.GetString(key);
            else return null;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int EditorData_Get_With_Int(string key)
        {
            if (EditorPrefs.HasKey(key))
                return EditorPrefs.GetInt(key);
            else return 0;
        }
        /// <summary>
        /// 存入数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void EditorData_Set_With_String(string key, string data)
        {
            EditorPrefs.SetString(key, data);
        }
        /// <summary>
        /// 存入数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void EditorData_Set_With_Int(string key, int data)
        {
            EditorPrefs.SetInt(key, data);
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="key"></param>
        public static void EditorData_Clear(string key)
        {
            if (EditorPrefs.HasKey(key))
                EditorPrefs.DeleteKey(key);
        }
        #endregion

        #region EditorWindow
        /// <summary>
        /// 对话框居中
        /// </summary>
        /// <param name="size"></param>
        /// <param name="window"></param>
        public static void CenterEditorWindow(Vector2Int size, EditorWindow window)
        {
            window.minSize = size;
            window.maxSize = window.minSize;

            // 获取当前屏幕的分辨率
            int screenWidth = Screen.currentResolution.width;
            int screenHeight = Screen.currentResolution.height;

            // 计算窗口位置（屏幕中心）
            Rect windowRect = new Rect((screenWidth - size.x) / 2.0f, (screenHeight - size.y) / 2.0f, size.x, size.y);

            // 更新窗口位置和大小
            window.position = windowRect;
        }
        #endregion

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="p"></param>
        /// <param name="value"></param>
        public static void SetValue(this SerializedProperty p, object value)
        {
            switch (p.propertyType)
            {
                case SerializedPropertyType.AnimationCurve:
                    p.animationCurveValue = value as AnimationCurve;
                    break;
                case SerializedPropertyType.ArraySize:
                    p.intValue = (int)value;
                    break;
                case SerializedPropertyType.Boolean:
                    p.boolValue = (bool)value;
                    break;
                case SerializedPropertyType.Bounds:
                    p.boundsValue = (Bounds)value;
                    break;
                case SerializedPropertyType.Character:
                    p.stringValue = (string)value;
                    break;
                case SerializedPropertyType.Color:
                    p.colorValue = (Color)value;
                    break;
                case SerializedPropertyType.Enum:
                    p.enumValueIndex = (int)value;
                    break;
                case SerializedPropertyType.Float:
                    p.floatValue = (float)value;
                    break;
                case SerializedPropertyType.Generic:
                    Debug.LogWarning("Get/Set of Generic SerializedProperty not Effective");
                    break;
                case SerializedPropertyType.Gradient:
                    Debug.LogWarning("Get/Set of Gradient SerializedProperty not Effective");
                    break;
                case SerializedPropertyType.Integer:
                    p.intValue = (int)value;
                    break;
                case SerializedPropertyType.LayerMask:
                    p.intValue = (int)value;
                    break;
                case SerializedPropertyType.ObjectReference:
                    p.objectReferenceValue = value as UnityEngine.Object;
                    break;
                case SerializedPropertyType.Quaternion:
                    p.quaternionValue = (Quaternion)value;
                    break;
                case SerializedPropertyType.Rect:
                    p.rectValue = (Rect)value;
                    break;
                case SerializedPropertyType.String:
                    p.stringValue = (string)value;
                    break;
                case SerializedPropertyType.Vector2:
                    p.vector2Value = (Vector2)value;
                    break;
                case SerializedPropertyType.Vector3:
                    p.vector3Value = (Vector3)value;
                    break;
                case SerializedPropertyType.Vector4:
                    p.vector4Value = (Vector4)value;
                    break;
            }
        }
    }
}