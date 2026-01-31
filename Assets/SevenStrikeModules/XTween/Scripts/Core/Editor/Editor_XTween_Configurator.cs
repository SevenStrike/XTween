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
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class Editor_XTween_Configurator : EditorWindow
    {
        private static Editor_XTween_Configurator window;

        /// <summary>
        /// 字体 - 粗体
        /// </summary>
        Font Font_Bold;
        /// <summary>
        /// 字体 - 细体
        /// </summary>
        Font Font_Light;

        /// <summary>
        /// 图标
        /// </summary>
        private Texture2D
            logo,
            LiquidIcon_Pure_Press,
            LiquidIcon_Scanline_Press,
            LiquidIcon_DirtyScanline_Press,
            LiquidIcon_Pure_Released,
            LiquidIcon_Scanline_Released,
            LiquidIcon_DirtyScanline_Released,
            Liquid_PreviewBg_Pure,
            Liquid_PreviewBg_Scan,
            Liquid_PreviewBg_ScanDirty,
            Liquid_PreviewBg_status_playing,
            Liquid_PreviewBg_status_idle,
            Liquid_Plug,
            Liquid_MetalGrid;

        #region 抬头参数
        Rect Title_rect;
        Rect Icon_rect;
        Rect Sepline_rect;
        Color SepLineColor = new Color(1, 1, 1, 0.15f);
        #endregion

        Rect rect_primary;
        Rect rect_secondary;
        public bool IsPressed;

        public TweenConfigData TweenConfigData;

        [MenuItem("Assets/XTween/C 配置面板（Configurator)")]
        public static void ShowWindow()
        {
            window = (Editor_XTween_Configurator)EditorWindow.GetWindow(typeof(Editor_XTween_Configurator), true, "XTween 配置面板", true);
            XTween_GUI.CenterEditorWindow(new Vector2Int(375, 820), window);
            window.maxSize = window.minSize;
            window.Show();
        }

        private void OnEnable()
        {
            #region 图标获取
            logo = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/logo");

            LiquidIcon_Pure_Press = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_pure_press");
            LiquidIcon_Pure_Released = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_pure_released");

            LiquidIcon_Scanline_Press = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_scan_press");
            LiquidIcon_Scanline_Released = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_scan_released");

            LiquidIcon_DirtyScanline_Press = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_scandirty_press");
            LiquidIcon_DirtyScanline_Released = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_scandirty_released");

            Liquid_PreviewBg_Pure = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_previewbg_pure");
            Liquid_PreviewBg_Scan = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_previewbg_scan");
            Liquid_PreviewBg_ScanDirty = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_previewbg_scandirty");

            Liquid_PreviewBg_status_playing = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_previewbg_status_playing");
            Liquid_PreviewBg_status_idle = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/liquidstyle_previewbg_status_idle");

            Liquid_Plug = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/LiquidPlug");
            Liquid_MetalGrid = XTween_GUI.GetIcon("Icons_Hud_XTween_Configurator/MetalGrid");
            #endregion

            //获取配置文件
            string json = AssetDatabase.LoadAssetAtPath<TextAsset>(XTween_Dashboard.Get_path_XTween_Config_Path() + $"XTweenConfigData.json").text;
            TweenConfigData = JsonUtility.FromJson<TweenConfigData>(json);
            Font_Bold = XTween_GUI.GetFont("SS_Editor_Bold");
            Font_Light = XTween_GUI.GetFont("SS_Editor_Dialog");

            XTween_Dashboard.LiquidColor_Idle = XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Idle);
            XTween_Dashboard.LiquidColor_Playing = XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Playing);
        }

        private void OnGUI()
        {
            #region 抬头
            Rect rect = new Rect(0, 0, position.width, position.height);

            Icon_rect = new Rect(15, 15, 48, 48);

            XTween_GUI.Gui_Icon(Icon_rect, logo);

            Title_rect = new Rect(rect.x + 85, rect.y + 15, rect.width - 80, 30);
            XTween_GUI.Gui_Labelfield(Title_rect, "XTween 配置面板", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 20, Font_Bold);

            Sepline_rect = new Rect(rect.x + 85, rect.y + 60, 200, 1);
            XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);
            #endregion

            rect_primary.Set(20, 85, 300, 15);
            rect_secondary = rect_primary;

            #region 面板预览器
            rect_primary = new Rect(10, 20, 300, position.height);


            // 液晶屏
            rect_primary.Set(rect_secondary.x, rect_secondary.y, 285, 150);
            //Rect rect_pressor = rect_primary;

            // 检测鼠标事件
            Event currentEvent = Event.current;

            if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0 && rect_primary.Contains(currentEvent.mousePosition))
            {
                IsPressed = true;
                currentEvent.Use();
            }
            if (currentEvent.type == EventType.MouseUp && currentEvent.button == 0 && rect_primary.Contains(currentEvent.mousePosition))
            {
                IsPressed = false;
                currentEvent.Use();
            }

            GUI.backgroundColor = IsPressed ? XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Playing) : XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Idle);

            XTween_GUI.Gui_LiquidField(rect_primary, "", new RectOffset(0, 0, 0, 0), (TweenConfigData.LiquidScanStyle ? (TweenConfigData.LiquidDirty ? Liquid_PreviewBg_ScanDirty : Liquid_PreviewBg_Scan) : Liquid_PreviewBg_Pure));

            GUI.backgroundColor = Color.white;

            // 液晶屏内容
            rect_primary.Set(rect_secondary.x + 14, rect_secondary.y + 14, Liquid_PreviewBg_status_playing.width, Liquid_PreviewBg_status_playing.height);
            XTween_GUI.Gui_TextureBox(rect_primary, IsPressed ? Liquid_PreviewBg_status_playing : Liquid_PreviewBg_status_idle);

            // 液晶屏接口
            rect_primary.Set(rect_secondary.x + 68, rect_secondary.y + 150, Liquid_Plug.width, Liquid_Plug.height);
            XTween_GUI.Gui_TextureBox(rect_primary, Liquid_Plug);

            // 液晶屏金属网格角
            rect_primary.Set(rect_secondary.width - 38, rect_secondary.y + 110, Liquid_MetalGrid.width, Liquid_MetalGrid.height);
            XTween_GUI.Gui_TextureBox(rect_primary, Liquid_MetalGrid);

            #endregion

            #region 数据面板样式选择器
            float size = 35;
            float offset = 20;
            float dis = 56;
            rect_primary.Set(rect_secondary.width + offset, rect_secondary.y, size, size);
            if (XTween_GUI.Gui_Button(rect_primary, LiquidIcon_Pure_Released, LiquidIcon_Pure_Press, true, null, null, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Idle)))
            {
                TweenConfigData.LiquidScanStyle = false;
                TweenConfigData.LiquidDirty = false;
            }
            rect_primary.Set(rect_secondary.width + offset, rect_secondary.y + dis, size, size);
            if (XTween_GUI.Gui_Button(rect_primary, LiquidIcon_Scanline_Released, LiquidIcon_Scanline_Press, true, null, null, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Idle)))
            {
                TweenConfigData.LiquidScanStyle = true;
                TweenConfigData.LiquidDirty = false;
            }
            rect_primary.Set(rect_secondary.width + offset, rect_secondary.y + dis * 2, size, size);
            if (XTween_GUI.Gui_Button(rect_primary, LiquidIcon_DirtyScanline_Released, LiquidIcon_DirtyScanline_Press, true, null, null, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Idle)))
            {
                TweenConfigData.LiquidScanStyle = true;
                TweenConfigData.LiquidDirty = true;
            }
            #endregion

            float cloum_left = rect_secondary.x + 5;
            float cloum_right = rect_secondary.width;
            float row = rect_secondary.y + 185;
            float interval = 28;

            // 播放时背景色
            rect_primary.Set(cloum_left, row, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "播放时背景色", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5, 80, 20);
            TweenConfigData.LiquidColor_Playing = XTween_Utilitys.ConvertColorToHexString(XTween_GUI.Gui_ColorField(rect_primary, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Playing)), true);

            // 待命时背景色
            rect_primary.Set(cloum_left, row + interval, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "待命时背景色", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval, 80, 20);
            TweenConfigData.LiquidColor_Idle = XTween_Utilitys.ConvertColorToHexString(XTween_GUI.Gui_ColorField(rect_primary, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.LiquidColor_Idle)), true);

            // 动画指示器闪烁
            rect_primary.Set(cloum_left, row + interval * 2f, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画指示器闪烁", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 2f, 80, 20);
            TweenConfigData.LiquidBlinker = XTween_GUI.Gui_Popup(rect_primary, TweenConfigData.LiquidBlinker, new string[2] { "关闭", "开启" }, GUIFilled.实体, GUIColor.深空灰, Color.white);

            // 面板控件主题色
            rect_primary.Set(cloum_left, row + interval * 3f, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "面板控件主题色", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 3f, 80, 20);
            TweenConfigData.Theme_Primary = XTween_Utilitys.ConvertColorToHexString(XTween_GUI.Gui_ColorField(rect_primary, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.Theme_Primary)), true);

            // 面板分组边框色
            rect_primary.Set(cloum_left, row + interval * 4f, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "面板分组边框色", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 4f, 80, 20);
            TweenConfigData.Theme_Group = XTween_Utilitys.ConvertColorToHexString(XTween_GUI.Gui_ColorField(rect_primary, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.Theme_Group)), true);

            // 面板分割线色
            rect_primary.Set(cloum_left, row + interval * 5f, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "面板分割线色", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 5f, 80, 20);
            TweenConfigData.Theme_SeperateLine = XTween_Utilitys.ConvertColorToHexString(XTween_GUI.Gui_ColorField(rect_primary, XTween_Utilitys.ConvertHexStringToColor(TweenConfigData.Theme_SeperateLine)), true);

            Sepline_rect = new Rect(cloum_left, row + interval * 6.25f, cloum_right, 1);
            XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);

            #region 动画池预加载
            row += 85;
            // 动画池预加载 Integer
            rect_primary.Set(cloum_left, row + interval * 4, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Integer", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 4, 80, 20);
            TweenConfigData.PoolCount_Int = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Int);

            // 动画池预加载 Float
            rect_primary.Set(cloum_left, row + interval * 5, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Float", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 5, 80, 20);
            TweenConfigData.PoolCount_Float = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Float);

            // 动画池预加载 String
            rect_primary.Set(cloum_left, row + interval * 6, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - String", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 6, 80, 20);
            TweenConfigData.PoolCount_String = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_String);

            // 动画池预加载 Color
            rect_primary.Set(cloum_left, row + interval * 7, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Color", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 7, 80, 20);
            TweenConfigData.PoolCount_Color = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Color);

            // 动画池预加载 Quaternion
            rect_primary.Set(cloum_left, row + interval * 8, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Quaternion", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 8, 80, 20);
            TweenConfigData.PoolCount_Quaternion = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Quaternion);

            // 动画池预加载 Vector2
            rect_primary.Set(cloum_left, row + interval * 9, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Vector2", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 9, 80, 20);
            TweenConfigData.PoolCount_Vector2 = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Vector2);

            // 动画池预加载 Vector3
            rect_primary.Set(cloum_left, row + interval * 10, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Vector3", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 10, 80, 20);
            TweenConfigData.PoolCount_Vector3 = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Vector3);

            // 动画池预加载 Vector4
            rect_primary.Set(cloum_left, row + interval * 11, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池预加载 - Vector4", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 11, 80, 20);
            TweenConfigData.PoolCount_Vector4 = XTween_GUI.Gui_InputField_Int(rect_primary, TweenConfigData.PoolCount_Vector4);

            // 场景加载时动画池自动回收开关
            rect_primary.Set(cloum_left, row + interval * 12, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池自动回收 - 场景加载时", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 12, 80, 20);
            TweenConfigData.PoolRecyleAllOnSceneLoaded = XTween_GUI.Gui_Toggle(rect_primary, false, new string[2] { "关闭", "开启" }, TweenConfigData.PoolRecyleAllOnSceneLoaded, GUIFilled.无, GUIColor.亮白, GUIFilled.实体, XTween_Dashboard.Theme_Primary, Color.white, Color.black);

            // 场景卸载时动画池自动回收开关
            rect_primary.Set(cloum_left, row + interval * 13, 90, 20);
            XTween_GUI.Gui_Labelfield(rect_primary, "动画池自动回收 - 场景卸载时", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.UpperLeft, 12, Font_Light);
            rect_primary.Set(cloum_right - 26, row - 5 + interval * 13, 80, 20);
            TweenConfigData.PoolRecyleAllOnSceneUnloaded = XTween_GUI.Gui_Toggle(rect_primary, false, new string[2] { "关闭", "开启" }, TweenConfigData.PoolRecyleAllOnSceneUnloaded, GUIFilled.无, GUIColor.亮白, GUIFilled.实体, XTween_Dashboard.Theme_Primary, Color.white, Color.black);
            #endregion

            #region 保存配置按钮
            XTween_GUI.Gui_Layout_Space(768);
            XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无);
            XTween_GUI.Gui_Layout_Space(15);
            if (XTween_GUI.Gui_Layout_Button("复位参数", "", GUIFilled.实体, GUIColor.警示黄, Color.black, 35, new RectOffset(), new Vector2(0, 0), TextAnchor.MiddleCenter, 12, Font_Light))
            {
                TweenConfigData.Theme_Primary = "#3BFE9B";
                TweenConfigData.Theme_Group = "#1E1E1E";
                TweenConfigData.Theme_SeperateLine = "#535353";
                TweenConfigData.LiquidScanStyle = true;
                TweenConfigData.LiquidDirty = true;
                TweenConfigData.LiquidBlinker = 1;
                TweenConfigData.LiquidColor_Playing = "#94AC59";
                TweenConfigData.LiquidColor_Idle = "#778456";
                TweenConfigData.PoolCount_Int = 250;
                TweenConfigData.PoolCount_Float = 250;
                TweenConfigData.PoolCount_String = 250;
                TweenConfigData.PoolCount_Vector2 = 250;
                TweenConfigData.PoolCount_Vector3 = 250;
                TweenConfigData.PoolCount_Vector4 = 250;
                TweenConfigData.PoolCount_Quaternion = 250;
                TweenConfigData.PoolCount_Color = 250;
                TweenConfigData.PoolRecyleAllOnSceneUnloaded = true;
                TweenConfigData.PoolRecyleAllOnSceneLoaded = false;

                //SaveConfig();
            }
            XTween_GUI.Gui_Layout_Space(5);
            if (XTween_GUI.Gui_Layout_Button("更新配置", "", GUIFilled.实体, GUIColor.深空灰, Color.white, 35, new RectOffset(), new Vector2(0, 0), TextAnchor.MiddleCenter, 12, Font_Light))
            {
                if (!Application.isPlaying)
                {
                    SaveConfig();
                }
                else
                {
                    XTween_Utilitys.DebugInfo("XTween 配置面板", "应用正在运行，为避免出问题只有在非运行时期才可以使用！", GUIMsgState.警告);
                }
            }
            XTween_GUI.Gui_Layout_Space(15);
            XTween_GUI.Gui_Layout_Horizontal_End();
            #endregion
        }

        private void SaveConfig()
        {
            string json = JsonUtility.ToJson(TweenConfigData);
            // 使用StreamWriter写入文件
            using (StreamWriter writer = new StreamWriter(XTween_Dashboard.Get_path_XTween_Config_Path() + $"XTweenConfigData.json"))
            {
                writer.Write(json);
            }
            AssetDatabase.Refresh();

            XTween_Dashboard.LoadLiquidStyle();
            XTween_Dashboard.LoadThemes();

            XTween_Utilitys.DebugInfo("XTween 配置面板", "已更新 XTween 的配置参数！", GUIMsgState.设置);
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                Repaint();
            }
        }
    }
}