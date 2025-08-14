namespace SevenStrikeModules.XTween
{
    using System;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public struct PoolData
    {
        /// <summary>
        /// 动画类型总数
        /// </summary>
        public int Count;
        /// <summary>
        /// 动画类型预加载数
        /// </summary>
        public int Preloaded;
        /// <summary>
        /// 动画已使用总数
        /// </summary>
        public int Reused;
        /// <summary>
        /// 动画已使用百分比
        /// </summary>
        public float Percentage;
        /// <summary>
        /// 动画已使用百分比（平滑）
        /// </summary>
        public float Percentage_Smooth;

        /// <summary>
        /// 计算数量与百分比
        /// </summary>
        /// <param name="type"></param>
        public void CalculateData(Type type)
        {
            Count = XTween_Pool.Get_PoolCount(type);
            Preloaded = XTween_Pool.GetPreloadCount(type);
            Reused = XTween_Pool.Get_CreatedCount(type);
            Percentage = XTween_Pool.Get_UsagePercentage(type);
            Percentage_Smooth = Mathf.Lerp(Percentage_Smooth, Percentage, Time.unscaledDeltaTime * 3);
        }
    }

    public class Editor_XTween_PoolAnalyzer : EditorWindow
    {
        private static Editor_XTween_PoolAnalyzer window;
        private StringBuilder stringBuilder;

        /// <summary>
        /// 字体 - 粗体
        /// </summary>
        Font Font_Bold;
        /// <summary>
        /// 字体 - 细体
        /// </summary>
        Font Font_Light;

        private string TweenLiquidContent;
        private Texture2D TweenLiquidScreen;

        /// <summary>
        /// 图标
        /// </summary>
        private Texture2D logo, icon_pathpercent,
            LiquidBg_Playing,
            LiquidBg_Playing_Scanline,
            LiquidBg_Ready,
            LiquidBg_Ready_Scanline,
            LiquidPlug_Blue,
            MetalGrid,
            LiquidDirty;

        #region 抬头参数
        Rect Title_rect;
        Rect Icon_rect;
        Rect Sepline_rect;
        Color SepLineColor = new Color(1, 1, 1, 0.15f);
        Color MessageColor = new Color(1, 1, 1, 0.62f);
        #endregion

        float liquid_left_margin = 35;
        float liquid_right_margin = 70;

        PoolData PoolData_Int;
        PoolData PoolData_Float;
        PoolData PoolData_String;
        PoolData PoolData_Vector2;
        PoolData PoolData_Vector3;
        PoolData PoolData_Vector4;
        PoolData PoolData_Quaternion;
        PoolData PoolData_Color;

        Rect rect_liquid_prim;
        Rect rect_liquid_set;
        RectOffset liquid_rectoffet;

        [MenuItem("Tools/SevenStrikeModules/XTween/PoolManager #F9")]
        public static void ShowWindow()
        {
            window = (Editor_XTween_PoolAnalyzer)EditorWindow.GetWindow(typeof(Editor_XTween_PoolAnalyzer), true, "XTween 动画池管理器", true);
            XTween_GUI.CenterEditorWindow(new Vector2Int(360, 650), window);
            window.maxSize = window.minSize;
            window.Show();
        }

        private void OnEnable()
        {
            #region 图标获取
            logo = XTween_GUI.GetIcon("Icons_Hud_XTween_PoolAnalyzer/logo");
            icon_pathpercent = XTween_GUI.GetIcon("Icons_Hud_XTween_Controller/icon_pathpercent");
            LiquidBg_Ready = XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidBg_Ready");
            LiquidBg_Ready_Scanline = XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidBg_Ready_Scanline");
            LiquidBg_Playing = XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidBg_Playing");
            LiquidBg_Playing_Scanline = XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidBg_Playing_Scanline");
            LiquidPlug_Blue = XTween_GUI.GetIcon("Icons_Liquid/LiquidPlug_Blue");
            MetalGrid = XTween_GUI.GetIcon("Icons_Liquid/MetalGrid");
            LiquidDirty = XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidDirty");
            #endregion

            Font_Bold = XTween_GUI.GetFont("SS_Editor_Bold");
            Font_Light = XTween_GUI.GetFont("SS_Editor_Dialog");

            stringBuilder = new StringBuilder();

            liquid_rectoffet = new RectOffset(45, 45, 20, 20);
        }

        private void OnGUI()
        {
            #region 抬头
            Rect rect = new Rect(0, 0, position.width, position.height);

            Icon_rect = new Rect(15, 15, 48, 48);

            XTween_GUI.Gui_Icon(Icon_rect, logo);

            Title_rect = new Rect(rect.x + 85, rect.y + 15, rect.width - 80, 30);
            XTween_GUI.Gui_Labelfield(Title_rect, "XTween 动画池管理器", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 20, Font_Bold);

            Sepline_rect = new Rect(rect.x + 85, rect.y + 60, 200, 1);
            XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);

            XTween_GUI.Gui_Labelfield_Thin_WrapClip(new Rect(rect.x + 18, rect.y + 80, rect.width - 38, rect.height), "此面板可监控并管理XTween动画池的使用状态以及参数！", GUIFilled.无, GUIColor.无, MessageColor, TextAnchor.UpperLeft, new Vector2(0, 0), 12, true, Font_Light);
            #endregion

            #region 面板预览器
            rect_liquid_prim = new Rect(0, 20, position.width, position.height);
            rect_liquid_set = rect_liquid_prim;
            if (Application.isPlaying)
            {
                TweenLiquidContent = "应用运行中";
                if (XTween_Dashboard.LiquidScanStyle)
                    TweenLiquidScreen = LiquidBg_Playing_Scanline;
                else
                    TweenLiquidScreen = LiquidBg_Playing;
                if (XTween_Dashboard.LiquidLagacyColor)
                    GUI.backgroundColor = XTween_Dashboard.Liquid_On_Color;
                else
                    GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                if (XTween_Dashboard.LiquidLagacyColor)
                    GUI.backgroundColor = XTween_Dashboard.Liquid_Off_Color;
                else
                    GUI.backgroundColor = Color.white;
                TweenLiquidContent = "应用未运行";
                if (XTween_Dashboard.LiquidScanStyle)
                    TweenLiquidScreen = LiquidBg_Ready_Scanline;
                else
                    TweenLiquidScreen = LiquidBg_Ready;
            }

            // 液晶屏
            rect_liquid_prim.Set(rect_liquid_set.x + 15, rect_liquid_set.y + 110, rect_liquid_set.width - 30, TweenLiquidScreen.height);
            XTween_GUI.Gui_LiquidField(rect_liquid_prim, TweenLiquidContent, liquid_rectoffet, TweenLiquidScreen);

            // 液晶屏肮脏
            if (XTween_Dashboard.LiquidDirty)
            {
                rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - LiquidDirty.width - 13), rect_liquid_set.y + 108, LiquidDirty.width, LiquidDirty.height);
                XTween_GUI.Gui_TextureBox(rect_liquid_prim, LiquidDirty);
            }

            // 液晶屏接口
            rect_liquid_prim.Set(rect_liquid_set.x + ((rect_liquid_set.width / 2) - (LiquidPlug_Blue.width / 2)), rect_liquid_set.y + 545, LiquidPlug_Blue.width, LiquidPlug_Blue.height);
            XTween_GUI.Gui_TextureBox(rect_liquid_prim, LiquidPlug_Blue);

            // 液晶屏金属网格角
            rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - MetalGrid.width - 5), rect_liquid_set.y + 505, MetalGrid.width, MetalGrid.height);
            XTween_GUI.Gui_TextureBox(rect_liquid_prim, MetalGrid);

            #region 状态显示
            rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - 130), rect_liquid_set.y + 101, 100, 65);
            XTween_GUI.Gui_Labelfield_WrapText(rect_liquid_prim, $"状态 :  {(XTween_Pool.IsAnyTweenInUse() ? "正在使用" : "未使用")}", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleRight, Vector2.zero, 11, false, false, TextClipping.Overflow, true, Font_Light);
            #endregion

            #region 进度条 - EasedProgress
            rect_liquid_prim.Set(rect_liquid_set.x, rect_liquid_set.y + 185, rect_liquid_set.width, rect_liquid_set.height - 185);

            float height = 0;
            LiquidProgress(rect_liquid_prim, height, "Int 动画", Application.isPlaying ? PoolDataVisual(PoolData_Int) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Int.Percentage_Smooth : PoolData_Int.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Float 动画", Application.isPlaying ? PoolDataVisual(PoolData_Float) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Float.Percentage_Smooth : PoolData_Float.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "String 动画", Application.isPlaying ? PoolDataVisual(PoolData_String) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_String.Percentage_Smooth : PoolData_String.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Vector2 动画", Application.isPlaying ? PoolDataVisual(PoolData_Vector2) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Vector2.Percentage_Smooth : PoolData_Vector2.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Vector3 动画", Application.isPlaying ? PoolDataVisual(PoolData_Vector3) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Vector3.Percentage_Smooth : PoolData_Vector3.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Vecto4  动画", Application.isPlaying ? PoolDataVisual(PoolData_Vector4) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Vector4.Percentage_Smooth : PoolData_Vector4.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Quaternion 动画", Application.isPlaying ? PoolDataVisual(PoolData_Quaternion) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Quaternion.Percentage_Smooth : PoolData_Quaternion.Percentage) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Color 动画", Application.isPlaying ? PoolDataVisual(PoolData_Color) : "未就绪", Application.isPlaying ? (XTween_Dashboard.LiquidProgressAnimation ? PoolData_Color.Percentage_Smooth : PoolData_Color.Percentage) : 0);
            #endregion

            GUI.backgroundColor = Color.white;
            #endregion

            XTween_GUI.Gui_Layout_Space(590);
            XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无);
            XTween_GUI.Gui_Layout_Space(15);
            if (XTween_GUI.Gui_Layout_Button("回收所有动画 (快速强制)", "", GUIFilled.实体, GUIColor.深空灰, Color.white, 35, new RectOffset(), new Vector2(0, 0), TextAnchor.MiddleCenter, 12, Font_Light))
            {
                if (Application.isPlaying)
                    XTween_Pool.ForceRecycleAll();
                else
                {
                    XTween_Utilitys.DebugInfo("XTween动画管理器消息", "应用未运行，只有在引用运行时期才可以使用此功能！", GUIMsgState.警告);
                }
            }
            XTween_GUI.Gui_Layout_Space(15);
            XTween_GUI.Gui_Layout_Horizontal_End();
        }

        private void LiquidProgress(Rect rect_progress, float height, string title, string value, float progress)
        {
            Rect r_pro = rect_progress;
            r_pro.Set(rect_progress.x + liquid_left_margin, rect_progress.y + height, (rect_progress.width - liquid_right_margin), 1);
            // 背景线
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 进度条
            r_pro.Set(rect_progress.x + liquid_left_margin, rect_progress.y + height - 4, (rect_progress.width - liquid_right_margin) * progress * 0.01f, 4);
            EditorGUI.DrawRect(r_pro, Color.black);
            // 标题
            r_pro.Set(rect_progress.x + liquid_left_margin, rect_progress.y + (height - 17), 50, 6);
            XTween_GUI.Gui_Labelfield(r_pro, title, GUIFilled.无, GUIColor.无, Color.black * 0.9f, TextAnchor.MiddleLeft, new Vector2(0, 0), 12, Font_Light);
            // 数值
            r_pro.Set(rect_progress.x + (rect_progress.width - liquid_right_margin - 25), rect_progress.y + (height - 17), 50, 6);
            XTween_GUI.Gui_Labelfield(r_pro, value, GUIFilled.无, GUIColor.无, Color.black * 0.95f, TextAnchor.MiddleRight, new Vector2(0, 0), 11, Font_Light);
            // 起点线
            r_pro.Set((rect_progress.x + liquid_left_margin), rect_progress.y + (height - 2), 1, 6);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 终点线
            r_pro.Set((rect_progress.x + (rect_progress.width - liquid_right_margin + liquid_left_margin)), rect_progress.y + (height - 2), 1, 6);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 指示器
            r_pro.Set(((rect_progress.x + liquid_left_margin - 4) + (rect_progress.width - liquid_right_margin) * progress * 0.01f), rect_progress.y + (height + 3), 8, 8);
            XTween_GUI.Gui_Icon(r_pro, icon_pathpercent);
            // 中点线
            r_pro.Set(((rect_progress.x + liquid_left_margin) + (rect_progress.width - liquid_right_margin) * 0.5f), rect_progress.y + (height - 10), 1, 10);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
        }

        private string PoolDataVisual(PoolData data)
        {
            stringBuilder.Clear();
            stringBuilder.Append(data.Reused)
                .Append(" | ")
                .Append(data.Count)
                .Append(" / ")
                .Append(data.Preloaded)
                .Append(" | ")
                .Append(XTween_Dashboard.LiquidProgressAnimation ? data.Percentage_Smooth.ToString("F2") : data.Percentage)
                .Append(" %");
            return stringBuilder.ToString();
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                PoolData_Int.CalculateData(typeof(XTween_Specialized_Int));
                PoolData_Float.CalculateData(typeof(XTween_Specialized_Float));
                PoolData_String.CalculateData(typeof(XTween_Specialized_String));
                PoolData_Vector2.CalculateData(typeof(XTween_Specialized_Vector2));
                PoolData_Vector3.CalculateData(typeof(XTween_Specialized_Vector3));
                PoolData_Vector4.CalculateData(typeof(XTween_Specialized_Vector4));
                PoolData_Quaternion.CalculateData(typeof(XTween_Specialized_Quaternion));
                PoolData_Color.CalculateData(typeof(XTween_Specialized_Color));
                Repaint();
            }
        }
    }
}