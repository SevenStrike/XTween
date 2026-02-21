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
            LiquidBg_Pure,
            LiquidBg_Scan,
            LiquidPlug,
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

        [MenuItem("Assets/XTween/D 动画池管理器（PoolManager)")]
        public static void ShowWindow()
        {
            window = (Editor_XTween_PoolAnalyzer)EditorWindow.GetWindow(typeof(Editor_XTween_PoolAnalyzer), true, "XTween 动画池管理器", true);
            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(360, 640), window);
            window.maxSize = window.minSize;
            window.Show();
        }

        private void OnEnable()
        {
            #region 图标获取
            logo = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PoolAnalyzer/logo");
            icon_pathpercent = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_Controller/icon_pathpercent");
            LiquidBg_Pure = Editor_XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidBg_Pure");
            LiquidBg_Scan = Editor_XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidBg_Scan");
            LiquidPlug = Editor_XTween_GUI.GetIcon("Icons_Liquid/LiquidPlug_Blue");
            MetalGrid = Editor_XTween_GUI.GetIcon("Icons_Liquid/MetalGrid");
            LiquidDirty = Editor_XTween_GUI.GetIcon("Icons_Liquid/XTween_PoolAnalyzer/LiquidDirty");
            #endregion

            Font_Bold = Editor_XTween_GUI.GetFont("SS_Editor_Bold");
            Font_Light = Editor_XTween_GUI.GetFont("SS_Editor_Dialog");

            stringBuilder = new StringBuilder();

            liquid_rectoffet = new RectOffset(45, 45, 20, 20);
        }

        private void OnGUI()
        {
            #region 抬头
            Rect rect = new Rect(0, 0, position.width, position.height);

            Icon_rect = new Rect(15, 15, 48, 48);

            Editor_XTween_GUI.Gui_Icon(Icon_rect, logo);

            Title_rect = new Rect(rect.x + 85, rect.y + 15, rect.width - 80, 30);
            Editor_XTween_GUI.Gui_Labelfield(Title_rect, "XTween 动画池管理器", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 20, Font_Bold);

            Sepline_rect = new Rect(rect.x + 85, rect.y + 60, 200, 1);
            Editor_XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);

            Editor_XTween_GUI.Gui_Labelfield_Thin_WrapClip(new Rect(rect.x + 18, rect.y + 80, rect.width - 38, rect.height), "此面板可监控并管理XTween动画池的使用状态以及参数！", GUIFilled.无, GUIColor.无, MessageColor, TextAnchor.UpperLeft, new Vector2(0, 0), 12, true, Font_Light);
            #endregion

            #region 面板预览器
            rect_liquid_prim = new Rect(0, 20, position.width, position.height);
            rect_liquid_set = rect_liquid_prim;
            if (Application.isPlaying)
            {
                TweenLiquidContent = "应用运行中";
                if (XTween_Dashboard.ConfigData.LiquidScanStyle)
                    TweenLiquidScreen = LiquidBg_Scan;
                else
                    TweenLiquidScreen = LiquidBg_Pure;
                GUI.backgroundColor = XTween_Utilitys.ConvertHexStringToColor(XTween_Dashboard.ConfigData.LiquidColor_Playing);
            }
            else
            {
                GUI.backgroundColor = XTween_Utilitys.ConvertHexStringToColor(XTween_Dashboard.ConfigData.LiquidColor_Idle);

                TweenLiquidContent = "应用未运行";
                if (XTween_Dashboard.ConfigData.LiquidScanStyle)
                    TweenLiquidScreen = LiquidBg_Scan;
                else
                    TweenLiquidScreen = LiquidBg_Pure;
            }

            // 液晶屏
            rect_liquid_prim.Set(rect_liquid_set.x + 15, rect_liquid_set.y + 110, rect_liquid_set.width - 30, TweenLiquidScreen.height);
            Editor_XTween_GUI.Gui_LiquidField(rect_liquid_prim, TweenLiquidContent, liquid_rectoffet, TweenLiquidScreen);

            // 液晶屏肮脏
            if (XTween_Dashboard.ConfigData.LiquidDirty)
            {
                rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - LiquidDirty.width - 13), rect_liquid_set.y + 108, LiquidDirty.width, LiquidDirty.height);
                Editor_XTween_GUI.Gui_TextureBox(rect_liquid_prim, LiquidDirty);
            }

            // 液晶屏接口
            rect_liquid_prim.Set(rect_liquid_set.x + ((rect_liquid_set.width / 2) - (LiquidPlug.width / 2)), rect_liquid_set.y + 545, LiquidPlug.width, LiquidPlug.height);
            Editor_XTween_GUI.Gui_TextureBox(rect_liquid_prim, LiquidPlug);

            // 液晶屏金属网格角
            rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - MetalGrid.width - 5), rect_liquid_set.y + 505, MetalGrid.width, MetalGrid.height);
            Editor_XTween_GUI.Gui_TextureBox(rect_liquid_prim, MetalGrid);

            #region 状态显示
            rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - 130), rect_liquid_set.y + 101, 100, 65);
            Editor_XTween_GUI.Gui_Labelfield_WrapText(rect_liquid_prim, $"状态 :  {(XTween_Pool.IsAnyTweenInUse() ? "正在使用" : "未使用")}", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleRight, Vector2.zero, 11, false, false, TextClipping.Overflow, true, Font_Light);
            #endregion

            #region 进度条 - EasedProgress
            rect_liquid_prim.Set(rect_liquid_set.x, rect_liquid_set.y + 185, rect_liquid_set.width, rect_liquid_set.height - 185);

            float height = 0;
            LiquidProgress(rect_liquid_prim, height, "Int 动画", Application.isPlaying ? PoolDataVisual(PoolData_Int) : "未就绪", Application.isPlaying ? (PoolData_Int.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Float 动画", Application.isPlaying ? PoolDataVisual(PoolData_Float) : "未就绪", Application.isPlaying ? (PoolData_Float.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "String 动画", Application.isPlaying ? PoolDataVisual(PoolData_String) : "未就绪", Application.isPlaying ? (PoolData_String.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Vector2 动画", Application.isPlaying ? PoolDataVisual(PoolData_Vector2) : "未就绪", Application.isPlaying ? (PoolData_Vector2.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Vector3 动画", Application.isPlaying ? PoolDataVisual(PoolData_Vector3) : "未就绪", Application.isPlaying ? (PoolData_Vector3.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Vecto4  动画", Application.isPlaying ? PoolDataVisual(PoolData_Vector4) : "未就绪", Application.isPlaying ? (PoolData_Vector4.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Quaternion 动画", Application.isPlaying ? PoolDataVisual(PoolData_Quaternion) : "未就绪", Application.isPlaying ? (PoolData_Quaternion.Percentage_Smooth) : 0);
            height += 45;
            LiquidProgress(rect_liquid_prim, height, "Color 动画", Application.isPlaying ? PoolDataVisual(PoolData_Color) : "未就绪", Application.isPlaying ? PoolData_Color.Percentage_Smooth : 0);
            #endregion

            GUI.backgroundColor = Color.white;
            #endregion

            Editor_XTween_GUI.Gui_Layout_Space(590);
            Editor_XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无);
            Editor_XTween_GUI.Gui_Layout_Space(15);
            if (Editor_XTween_GUI.Gui_Layout_Button("回收所有动画 (快速强制)", "", GUIFilled.实体, GUIColor.深空灰, Color.white, 35, new RectOffset(), new Vector2(0, 0), TextAnchor.MiddleCenter, 12, Font_Light))
            {
                if (Application.isPlaying)
                    XTween_Pool.ForceRecycleAll();
                else
                {
                    XTween_Utilitys.DebugInfo("XTween动画管理器消息", "应用未运行，只有在引用运行时期才可以使用此功能！", GUIMsgState.警告);
                }
            }
            Editor_XTween_GUI.Gui_Layout_Space(15);
            Editor_XTween_GUI.Gui_Layout_Horizontal_End();
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
            Editor_XTween_GUI.Gui_Labelfield(r_pro, title, GUIFilled.无, GUIColor.无, Color.black * 0.9f, TextAnchor.MiddleLeft, new Vector2(0, 0), 12, Font_Light);
            // 数值
            r_pro.Set(rect_progress.x + (rect_progress.width - liquid_right_margin - 25), rect_progress.y + (height - 17), 50, 6);
            Editor_XTween_GUI.Gui_Labelfield(r_pro, value, GUIFilled.无, GUIColor.无, Color.black * 0.95f, TextAnchor.MiddleRight, new Vector2(0, 0), 11, Font_Light);
            // 起点线
            r_pro.Set((rect_progress.x + liquid_left_margin), rect_progress.y + (height - 2), 1, 6);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 终点线
            r_pro.Set((rect_progress.x + (rect_progress.width - liquid_right_margin + liquid_left_margin)), rect_progress.y + (height - 2), 1, 6);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 指示器
            r_pro.Set(((rect_progress.x + liquid_left_margin - 4) + (rect_progress.width - liquid_right_margin) * progress * 0.01f), rect_progress.y + (height + 3), 8, 8);
            Editor_XTween_GUI.Gui_Icon(r_pro, icon_pathpercent);
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
                .Append(data.Percentage_Smooth.ToString("F2"))
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