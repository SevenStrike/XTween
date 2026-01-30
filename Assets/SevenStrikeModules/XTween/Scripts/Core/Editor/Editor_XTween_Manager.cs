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
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public struct xTweenDatas
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count;
        /// <summary>
        /// 百分比
        /// </summary>
        public float Percentage;
        /// <summary>
        /// 动画百分比
        /// </summary>
        public float Percentage_Smooth;

        /// <summary>
        /// 计算数量与百分比
        /// </summary>
        /// <param name="type"></param>
        public void CalculateData(int count, int total)
        {
            if (total <= 0) // 避免除零错误
            {
                Count = 0;
                Percentage = 0f;
                Percentage_Smooth = 0f;
                return;
            }

            Count = count;
            Percentage = (float)count / total;

            Percentage_Smooth = Mathf.Lerp(Percentage_Smooth, Percentage, Time.unscaledDeltaTime * 3);
        }
    }

    [CustomEditor(typeof(XTween_Manager))]
    public class Editor_XTween_Manager : Editor
    {
        private XTween_Manager BaseScript;

        #region 折叠开关
        private static bool _showStatistics = true;
        private static bool _showPendingOperations = true;
        #endregion

        #region 滚动列表
        private Vector2 _scrollPosition;
        private Dictionary<XTween_Interface, bool> _tweenFoldoutStates = new Dictionary<XTween_Interface, bool>();
        #endregion

        /// <summary>
        /// 液晶背景
        /// </summary>
        private Texture2D icon_pathpercent,
            icon_notrun,
            icon_main,
            icon_preview_r,
            icon_preview_p,
            icon_rewind_r,
            icon_rewind_p,
            icon_recycle_r,
            icon_recycle_p,
            TweenLiquidScreen,
            TweenLiquidScreen_Status,
            LiquidBg_Pure,
            LiquidBg_Scan,
            LiquidBg_Status_Pure,
            LiquidBg_Status_Scan,
            LiquidPlug_Green,
            LiquidPlug_Yellow,
            MetalGrid,
            LiquidDirty_Bg,
            LiquidDirty_Bg_Small,
            LiquidDirty_Status,
            LiquidDirty_Status_Small,
            ListIndex,
            Highlight,
            ItemIcon;
        private Texture2D EasePicBg;

        private string TweenLiquidContent;
        private float liquid_left_margin = 35;
        private float liquid_right_margin = 70;
        private int Count_Tweens;

        private xTweenDatas TwnData_Playing;
        private xTweenDatas TwnData_Pausing;
        private xTweenDatas TwnData_Comleted;
        private xTweenDatas TwnData_HasLoop;

        private float currentWidth;
        private float progressheight;

        #region 字体
        /// <summary>
        /// 字体 - 粗体
        /// </summary>
        Font Font_Bold;
        /// <summary>
        /// 字体 - 细体
        /// </summary>
        Font Font_Light;
        #endregion

        Rect rect_liquid_prim;
        Rect rect_liquid_set;
        RectOffset liquid_rectoffet;
        GUIStyle ListNameStyle;

        private void OnEnable()
        {
            BaseScript = (XTween_Manager)target;

            #region 图标获取
            icon_main = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_main");
            icon_pathpercent = XTween_GUI.GetIcon("Icons_Hud_XTween_Controller/icon_pathpercent");
            LiquidBg_Pure = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidBg_Pure");
            LiquidBg_Scan = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidBg_Scan");
            LiquidBg_Status_Pure = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidBg_Status_Pure");
            LiquidBg_Status_Scan = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidBg_Status_Scan");
            icon_notrun = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/icon_notrun");
            Font_Bold = XTween_GUI.GetFont("SS_Editor_Bold");
            Font_Light = XTween_GUI.GetFont("SS_Editor_Light");
            icon_preview_r = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_preview_r");
            icon_preview_p = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_preview_p");
            icon_rewind_r = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_rewind_r");
            icon_rewind_p = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_rewind_p");
            icon_recycle_r = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_recycle_r");
            icon_recycle_p = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/icon_recycle_p");
            LiquidPlug_Green = XTween_GUI.GetIcon("Icons_Liquid/LiquidPlug_Green");
            LiquidPlug_Yellow = XTween_GUI.GetIcon("Icons_Liquid/LiquidPlug_Yellow");
            MetalGrid = XTween_GUI.GetIcon("Icons_Liquid/MetalGrid");
            LiquidDirty_Bg = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidDirty_Bg");
            LiquidDirty_Bg_Small = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidDirty_Bg_Small");
            LiquidDirty_Status = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidDirty_Status");
            LiquidDirty_Status_Small = XTween_GUI.GetIcon("Icons_Liquid/XTween_Manager/LiquidDirty_Status_Small");
            ListIndex = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/ListIndex");
            ItemIcon = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/ItemIcon");
            Highlight = XTween_GUI.GetIcon("Icons_Hud_XTween_Manager/Highlight");
            #endregion

            liquid_rectoffet = new RectOffset(45, 45, 20, 20);

            #region 列表标题名称样式
            ListNameStyle = new GUIStyle(XTween_GUI.Style_LabelfieldBoldText);
            ListNameStyle.alignment = TextAnchor.MiddleLeft;
            ListNameStyle.contentOffset = new Vector2(15, 0);
            ListNameStyle.fontSize = 12;
            ListNameStyle.richText = true;
            ListNameStyle.wordWrap = true;

#if UNITY_6000_0_OR_NEWER
            // Unity 6+ 使用 Ellipsis
            TextClipping clipping = TextClipping.Ellipsis;
#else
    // Unity 2021.1 之前使用 Clip
    TextClipping clipping = TextClipping.Clip;
#endif

            ListNameStyle.clipping = clipping;
            #endregion

            EasePicBg = GetEasePicBg();

            //if (BaseScript.EasePics.Length <= 0)
            //{
            string[] names = Enum.GetNames(typeof(EaseMode));
            BaseScript.EasePics = new Texture2D[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                BaseScript.EasePics[i] = GetEasePic(names[i]);
            }
            //}
        }

        private void OnDisable()
        {
            _tweenFoldoutStates.Clear();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            XTween_GUI.Gui_Layout_Banner(icon_main, GUIFilled.实体, GUIColor.深空灰, "XTween - 动画管理器", Color.white, 20, 20);
            XTween_GUI.Gui_Layout_Space(5);

            #region 动画批量操作
            XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "动画批量操作", XTween_Dashboard.Theme_Primary);
            XTween_GUI.Gui_Layout_Space(10);

            XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            XTween_GUI.Gui_Layout_Space(10);
            #region 全部播放
            GUI.enabled = true;

            if (XTween_GUI.Gui_Layout_Button(15, "全部播放", icon_preview_r, icon_preview_p))
            {
                if (Application.isPlaying)
                    BaseScript.Play_All();
                else
                {
                    XTween_Utilitys.DebugInfo("XTween动画管理器消息", "应用未运行，只有在应用运行时期才可以使用此功能！", GUIMsgState.警告);
                }

                return;
            }
            #endregion
            GUILayout.FlexibleSpace();
            #region 全部倒退
            GUI.enabled = true;
            if (XTween_GUI.Gui_Layout_Button(15, "全部倒退", icon_rewind_r, icon_rewind_p))
            {
                if (Application.isPlaying)
                    BaseScript.Rewind_All();
                else
                {
                    XTween_Utilitys.DebugInfo("XTween动画管理器消息", "应用未运行，只有在引用运行时期才可以使用此功能！", GUIMsgState.警告);
                }

                return;
            }
            #endregion
            GUILayout.FlexibleSpace();
            #region 全部回收
            GUI.enabled = true;
            if (XTween_GUI.Gui_Layout_Button(15, "全部回收", icon_recycle_r, icon_recycle_p))
            {
                if (Application.isPlaying)
                    XTween_Pool.ForceRecycleAll();
                else
                {
                    XTween_Utilitys.DebugInfo("XTween动画管理器消息", "应用未运行，只有在引用运行时期才可以使用此功能！", GUIMsgState.警告);
                }
                return;
            }
            #endregion
            XTween_GUI.Gui_Layout_Space(10);
            XTween_GUI.Gui_Layout_Horizontal_End();

            XTween_GUI.Gui_Layout_Space(10);
            XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            currentWidth = EditorGUIUtility.currentViewWidth;

            bool IsExtraExpandPanel = currentWidth < 215 ? true : false;
            bool IsExpandPanelWidth = currentWidth > 352 ? true : false;

            rect_liquid_prim = new Rect(18, 148, EditorGUIUtility.currentViewWidth - 35, 225);
            rect_liquid_set = rect_liquid_prim;

            if (Application.isPlaying)
            {
                Count_Tweens = BaseScript.Get_TweenCount_ActiveTween();
                TwnData_Playing.CalculateData(BaseScript.Get_TweenCount_Playing(), Count_Tweens);
                TwnData_Pausing.CalculateData(BaseScript.Get_TweenCount_Paused(), Count_Tweens);
                TwnData_Comleted.CalculateData(BaseScript.Get_TweenCount_Completed(), Count_Tweens);
                TwnData_HasLoop.CalculateData(BaseScript.Get_TweenCount_HasLoop(), Count_Tweens);
                Repaint();
            }

            #region 统计数据
            XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "统计数据", XTween_Dashboard.Theme_Primary);
            XTween_GUI.Gui_Layout_Space(5);

            if (Application.isPlaying)
            {
                TweenLiquidContent = "应用运行中";
                if (XTween_Dashboard.TweenVisualStyleData.LiquidScanStyle)
                    TweenLiquidScreen_Status = LiquidBg_Status_Scan;
                else
                    TweenLiquidScreen_Status = LiquidBg_Status_Pure;
                GUI.backgroundColor = XTween_Dashboard.TweenVisualStyleData.Liquid_On_Color;
            }
            else
            {
                GUI.backgroundColor = XTween_Dashboard.TweenVisualStyleData.Liquid_Off_Color;
                TweenLiquidContent = "应用未运行";
                if (XTween_Dashboard.TweenVisualStyleData.LiquidScanStyle)
                    TweenLiquidScreen_Status = LiquidBg_Status_Scan;
                else
                    TweenLiquidScreen_Status = LiquidBg_Status_Pure;
            }

            // 液晶屏
            rect_liquid_prim.Set(rect_liquid_set.x + 15, rect_liquid_set.y, rect_liquid_set.width - 30, TweenLiquidScreen_Status.height);
            XTween_GUI.Gui_LiquidField(rect_liquid_prim, TweenLiquidContent, liquid_rectoffet, TweenLiquidScreen_Status);

            // 液晶屏肮脏
            if (!IsExtraExpandPanel)
            {
                if (XTween_Dashboard.TweenVisualStyleData.LiquidDirty)
                {
                    rect_liquid_prim.Set(rect_liquid_set.x + (IsExpandPanelWidth ? (rect_liquid_set.width - LiquidDirty_Status.width - 13) : (rect_liquid_set.width - LiquidDirty_Status_Small.width - 13)), rect_liquid_set.y - 1, IsExpandPanelWidth ? LiquidDirty_Status.width : LiquidDirty_Status_Small.width, IsExpandPanelWidth ? LiquidDirty_Status.height : LiquidDirty_Status_Small.height);
                    XTween_GUI.Gui_TextureBox(rect_liquid_prim, IsExpandPanelWidth ? LiquidDirty_Status : LiquidDirty_Status_Small);
                }
            }

            // 液晶屏接口
            if (!IsExtraExpandPanel)
            {
                rect_liquid_prim.Set(rect_liquid_set.x + ((rect_liquid_set.width / 2) - (LiquidPlug_Green.width / 2)), rect_liquid_set.y + 225, LiquidPlug_Green.width, LiquidPlug_Green.height);
                XTween_GUI.Gui_TextureBox(rect_liquid_prim, LiquidPlug_Green);
            }

            // 液晶屏金属网格角
            if (!IsExtraExpandPanel)
            {
                rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - MetalGrid.width - 5), rect_liquid_set.y + 185, MetalGrid.width, MetalGrid.height);
                XTween_GUI.Gui_TextureBox(rect_liquid_prim, MetalGrid);
            }

            rect_liquid_prim.Set(rect_liquid_set.x + rect_liquid_set.width - 85, rect_liquid_set.y + 20, 50, 6);
            XTween_GUI.Gui_Labelfield(rect_liquid_prim, Application.isPlaying ? $"动画统计：{Count_Tweens} 个" : "", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleRight, new Vector2(0, 0), 11, Font_Light);

            progressheight = 70;
            LiquidProgress(rect_liquid_set, progressheight, liquid_left_margin, liquid_right_margin, 25, "播放中", Application.isPlaying ? $"{TwnData_Playing.Count} / {Count_Tweens} ({(Count_Tweens == 0 ? 0 : TwnData_Playing.Percentage_Smooth.ToString("F2"))})" : "-", (Count_Tweens == 0 ? 0 : TwnData_Playing.Percentage_Smooth));
            progressheight += 38;
            LiquidProgress(rect_liquid_set, progressheight, liquid_left_margin, liquid_right_margin, 25, "暂停中", Application.isPlaying ? $"{TwnData_Pausing.Count} / {Count_Tweens} ({(Count_Tweens == 0 ? 0 : TwnData_Pausing.Percentage_Smooth.ToString("F2"))})" : "-", (Count_Tweens == 0 ? 0 : TwnData_Pausing.Percentage_Smooth));
            progressheight += 38;
            LiquidProgress(rect_liquid_set, progressheight, liquid_left_margin, liquid_right_margin, 25, "已完成", Application.isPlaying ? $"{TwnData_Comleted.Count} / {Count_Tweens} ({(Count_Tweens == 0 ? 0 : TwnData_Comleted.Percentage_Smooth.ToString("F2"))})" : "-", (Count_Tweens == 0 ? 0 : TwnData_Comleted.Percentage_Smooth));
            progressheight += 38;
            LiquidProgress(rect_liquid_set, progressheight, liquid_left_margin, liquid_right_margin, 25, "循环模式", Application.isPlaying ? $"{TwnData_HasLoop.Count} / {Count_Tweens} ({(Count_Tweens == 0 ? 0 : TwnData_HasLoop.Percentage_Smooth.ToString("F2"))})" : "-", (Count_Tweens == 0 ? 0 : TwnData_HasLoop.Percentage_Smooth));
            progressheight += 38;
            GUI.backgroundColor = Color.white;

            XTween_GUI.Gui_Layout_Space(254);
            XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 活跃动画列表
            XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "活跃动画列表", XTween_Dashboard.Theme_Primary);
            XTween_GUI.Gui_Layout_Space(5);

            rect_liquid_prim = new Rect(18, 440, EditorGUIUtility.currentViewWidth - 35, 100);
            rect_liquid_set = rect_liquid_prim;

            if (Application.isPlaying)
            {
                if (XTween_Dashboard.TweenVisualStyleData.LiquidScanStyle)
                    TweenLiquidScreen = LiquidBg_Scan;
                else
                    TweenLiquidScreen = LiquidBg_Pure;
                GUI.backgroundColor = XTween_Dashboard.TweenVisualStyleData.Liquid_On_Color;
            }
            else
            {
                GUI.backgroundColor = XTween_Dashboard.TweenVisualStyleData.Liquid_Off_Color;

                if (XTween_Dashboard.TweenVisualStyleData.LiquidScanStyle)
                    TweenLiquidScreen = LiquidBg_Scan;
                else
                    TweenLiquidScreen = LiquidBg_Pure;
            }

            // 液晶屏
            rect_liquid_prim.Set(rect_liquid_set.x + 15, rect_liquid_set.y, rect_liquid_set.width - 30, TweenLiquidScreen.height);
            XTween_GUI.Gui_LiquidField(rect_liquid_prim, "", liquid_rectoffet, TweenLiquidScreen);


            // 液晶屏接口
            if (!IsExtraExpandPanel)
            {
                rect_liquid_prim.Set(rect_liquid_set.x + ((rect_liquid_set.width / 2) - (LiquidPlug_Yellow.width / 2)), rect_liquid_set.y + 540, LiquidPlug_Yellow.width, LiquidPlug_Yellow.height);
                XTween_GUI.Gui_TextureBox(rect_liquid_prim, LiquidPlug_Yellow);
            }

            // 液晶屏金属网格角
            if (!IsExtraExpandPanel)
            {
                rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - MetalGrid.width - 5), rect_liquid_set.y + 500, MetalGrid.width, MetalGrid.height);
                XTween_GUI.Gui_TextureBox(rect_liquid_prim, MetalGrid);
            }

            GUI.backgroundColor = Color.white;

            ActiveTweensList(BaseScript);

            // 液晶屏肮脏
            if (!IsExtraExpandPanel)
            {
                if (XTween_Dashboard.TweenVisualStyleData.LiquidDirty)
                {
                    rect_liquid_prim.Set(rect_liquid_set.x + (IsExpandPanelWidth ? (rect_liquid_set.width - LiquidDirty_Bg.width - 13) : (rect_liquid_set.width - LiquidDirty_Bg_Small.width - 13)), rect_liquid_set.y - 2, IsExpandPanelWidth ? LiquidDirty_Bg.width : LiquidDirty_Bg_Small.width, IsExpandPanelWidth ? LiquidDirty_Bg.height : LiquidDirty_Bg_Small.height);
                    XTween_GUI.Gui_TextureBox(rect_liquid_prim, IsExpandPanelWidth ? LiquidDirty_Bg : LiquidDirty_Bg_Small);
                }
            }

            XTween_GUI.Gui_Layout_Space(0);
            XTween_GUI.Gui_Layout_Vertical_End();
            #endregion
        }

        #region 动画项参数绘制
        /// <summary>
        /// 绘制动画参数
        /// </summary>
        /// <param name="manager"></param>
        private void ActiveTweensList(XTween_Manager manager)
        {
            var activeTweens = manager.Get_ActiveTweens();
            if (activeTweens.Count == 0)
            {
                XTween_GUI.Gui_Layout_Space(50);
                XTween_GUI.Gui_Layout_Space(180);
                XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无);
                XTween_GUI.Gui_Layout_FlexSpace();
                XTween_GUI.Gui_Layout_Icon(45, icon_notrun, new Vector2(0, -20));
                XTween_GUI.Gui_Layout_FlexSpace();
                XTween_GUI.Gui_Layout_Horizontal_End();

#if UNITY_6000_0_OR_NEWER
                // Unity 6+ 使用 Ellipsis
                TextClipping clipping = TextClipping.Ellipsis;
#else
    // Unity 2021.1 之前使用 Clip
    TextClipping clipping = TextClipping.Clip;
#endif

                XTween_GUI.Gui_Layout_Labelfield("当前没有活跃的动画", GUIFilled.无, GUIColor.无, Color.black * 0.8f, true, clipping, TextAnchor.MiddleCenter, 12, Font_Light);
                XTween_GUI.Gui_Layout_Space(260);
            }
            else
            {
                XTween_GUI.Gui_Layout_Space(20);
                // 修复点1：使用 BeginVertical 包裹 ScrollView，强制明确布局范围
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(5);
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 95), GUILayout.Height(500));

                // 修复点2：禁用滚动视图内的自动布局计算
                EditorGUI.BeginChangeCheck();
                {
                    foreach (var tween in activeTweens)
                    {
                        if (tween == null) continue;

                        if (!_tweenFoldoutStates.ContainsKey(tween))
                        {
                            _tweenFoldoutStates[tween] = false;
                        }

                        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                        GUI.color = Color.black;
                        _tweenFoldoutStates[tween] = EditorGUILayout.Foldout(_tweenFoldoutStates[tween], $": :  {tween.ShortId}", true, ListNameStyle);
                        GUI.color = Color.white;
                        if (_tweenFoldoutStates[tween])
                        {
                            DrawTweenInfo(tween);
                        }
                        EditorGUILayout.EndVertical();
                    }
                }
                // 修复点3：仅在内容实际变化时更新布局
                if (EditorGUI.EndChangeCheck())
                {
                    // 手动触发重新布局
                    GUI.changed = true;
                }

                EditorGUILayout.EndScrollView();
                EditorGUILayout.Space(5);
                EditorGUILayout.EndHorizontal();
                XTween_GUI.Gui_Layout_Space(50);
            }
        }
        private void DrawTweenInfo(XTween_Interface tween)
        {
            if (tween == null) return;
            Rect rect = XTween_GUI.Gui_GetLastRect();
            Rect rect_item = rect;
            XTween_GUI.Gui_Box(rect, Color.black * 0.25f);
            GUI.color = Color.black;
            rect_item.Set(rect.x + 8, rect.y + 25, 15, 15);
            XTween_GUI.Gui_TextureBox(rect_item, ListIndex);
            rect_item.Set(rect.x + 32, rect.y + 25, rect.width - 35, 15);

#if UNITY_6000_0_OR_NEWER
            // Unity 6+ 使用 Ellipsis
            TextClipping clipping = TextClipping.Ellipsis;
#else
    // Unity 2021.1 之前使用 Clip
    TextClipping clipping = TextClipping.Clip;
#endif

            XTween_GUI.Gui_Labelfield(rect_item, $"ID：{tween.UniqueId.ToString()}", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 11, true, clipping, true, Font_Light);

            bool IsExtraExpandPanel = currentWidth < 310 ? true : false;
            float height = 75;
            rect_item.Set(rect.x, rect.y, rect.width, 15);
            LiquidProgress(rect_item, height, 12, IsExtraExpandPanel ? 20 : 120, 40, "缓动进度", $"{tween.CurrentEasedProgress:F2}", tween.CurrentEasedProgress);
            height += 35;
            LiquidProgress(rect_item, height, 12, IsExtraExpandPanel ? 20 : 120, 40, "实际进度", $"{tween.CurrentLoopProgress:F2}", tween.CurrentLoopProgress);

            #region EaseGraph图形
            if (!IsExtraExpandPanel)
            {
                GUI.color = Color.black;
                if (tween.UseCustomEaseCurve)
                    GUI.color = Color.black * 0.2f;
                rect_item.Set(rect.width - 96, rect.y + 57, 100, 65);
                XTween_GUI.Gui_TextureBox(rect_item, EasePicBg);
                rect_item.Set(rect.width - 96, rect.y + 57, 100, 65);
                XTween_GUI.Gui_TextureBox(rect_item, GetEasePic(tween.EaseMode));

                if (tween.UseCustomEaseCurve)
                {
                    GUI.color = Color.black;
                    rect_item.Set(rect.width - 76, rect.y + 57, 100, 65);
                    XTween_GUI.Gui_Labelfield(rect_item, "CustomCurve", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleLeft, 11, Font_Bold);
                }
            }
            #endregion

            height = 140;

            #region 基础
            rect_item.Set(rect.x + 10, rect.y + height + 1, 12, 12);
            XTween_GUI.Gui_Icon(rect_item, ItemIcon);
            rect_item.Set(rect.x + 30, rect.y + height - 2, Highlight.width, Highlight.height);
            XTween_GUI.Gui_Icon(rect_item, Highlight);
            rect_item.Set(rect.x + 38, rect.y + height, rect.width, 15);
            XTween_GUI.Gui_Labelfield(rect_item, "基础", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 10, rect.y + height + 15, rect.width - 20, 1);
            XTween_GUI.Gui_Box(rect_item);
            rect_item.Set(rect.x + 10, rect.y + height + 28, (rect.width / 2) - 5, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"耗时：<b>{tween.ElapsedTime * 1000:F0}</b>ms / <b>{tween.Duration * 1000:F0}</b>ms", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + (rect.width / 2) + 10, rect.y + height + 28, (rect.width / 2) - 10, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"延迟：<b>{tween.Delay.ToString()}</b> s", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 10, rect.y + height + 50, (rect.width / 2) - 5, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"缓动：<b>{tween.EaseMode.ToString()}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + (rect.width / 2) + 10, rect.y + height + 50, (rect.width / 2) - 10, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"曲线：<b>{(tween.UseCustomEaseCurve ? "使用" : "不使用")}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            #endregion

            height += 78;

            #region 循环
            rect_item.Set(rect.x + 10, rect.y + height + 1, 12, 12);
            XTween_GUI.Gui_Icon(rect_item, ItemIcon);
            rect_item.Set(rect.x + 30, rect.y + height - 2, Highlight.width, Highlight.height);
            XTween_GUI.Gui_Icon(rect_item, Highlight);
            rect_item.Set(rect.x + 38, rect.y + height, rect.width, 15);
            XTween_GUI.Gui_Labelfield(rect_item, "循环", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 10, rect.y + height + 15, rect.width - 20, 1);
            XTween_GUI.Gui_Box(rect_item);
            rect_item.Set(rect.x + 10, rect.y + height + 28, (rect.width / 2) - 5, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"次数：<b>{tween.LoopCount.ToString()}</b> s", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + (rect.width / 2) + 10, rect.y + height + 28, (rect.width / 2) - 10, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"递归：<b>{tween.CurrentLoop.ToString()}</b> 次", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 10, rect.y + height + 50, (rect.width / 2) - 5, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"模式：<b>{tween.LoopType.ToString()}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + (rect.width / 2) + 10, rect.y + height + 50, (rect.width / 2) - 10, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"间隔：<b>{tween.LoopingDelay.ToString()}</b> s", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            #endregion

            height += 78;

            #region 状态
            rect_item.Set(rect.x + 10, rect.y + height + 1, 12, 12);
            XTween_GUI.Gui_Icon(rect_item, ItemIcon);
            rect_item.Set(rect.x + 30, rect.y + height - 2, Highlight.width, Highlight.height);
            XTween_GUI.Gui_Icon(rect_item, Highlight);
            rect_item.Set(rect.x + 38, rect.y + height, rect.width, 15);
            XTween_GUI.Gui_Labelfield(rect_item, "状态", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 72, rect.y + height, rect.width, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"({GetStateString(tween)})", GUIFilled.无, GUIColor.无, Color.black, TextAnchor.MiddleLeft, Vector2.zero, 11, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 10, rect.y + height + 15, rect.width - 20, 1);
            XTween_GUI.Gui_Box(rect_item);
            rect_item.Set(rect.x + 10, rect.y + height + 28, (rect.width / 2) - 5, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"自动杀死：<b>{tween.AutoKill.ToString()}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + (rect.width / 2) + 10, rect.y + height + 28, (rect.width / 2) - 10, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"相对模式：<b>{tween.IsRelative.ToString()}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + 10, rect.y + height + 50, (rect.width / 2) - 5, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"暂停状态：<b>{tween.IsPaused.ToString()}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            rect_item.Set(rect.x + (rect.width / 2) + 10, rect.y + height + 50, (rect.width / 2) - 10, 15);
            XTween_GUI.Gui_Labelfield(rect_item, $"完成状态：<b>{tween.IsCompleted.ToString()}</b>", GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, true, Font_Light);
            #endregion

            XTween_GUI.Gui_Layout_Space(370);
            GUI.color = Color.white;
        }
        private string GetStateString(XTween_Interface tween)
        {
            if (tween == null) return "null";
            if (tween.IsKilled) return "已杀死";
            if (tween.IsCompleted) return "已完成";
            if (tween.IsPaused) return "已暂停";
            return tween.IsPlaying ? "播放中" : "待命中";
        }
        private string GetProgressString(XTween_Interface tween)
        {
            if (tween == null) return "null";
            return $"{tween.CurrentLinearProgress * 100:F1}% (缓动参数: {tween.CurrentEasedProgress * 100:F1}%)";
        }
        private void LiquidProgress(Rect rect_progress, float height, float mar_left, float mar_right, float mar_value, string title, string value, float progress)
        {
            Rect r_pro = rect_progress;
            r_pro.Set(rect_progress.x + mar_left, rect_progress.y + height, (rect_progress.width - mar_right), 1);
            // 背景线
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 进度条
            r_pro.Set(rect_progress.x + mar_left, rect_progress.y + height - 4, (rect_progress.width - mar_right) * progress, 4);
            EditorGUI.DrawRect(r_pro, Color.black);
            // 标题
            r_pro.Set(rect_progress.x + mar_left, rect_progress.y + (height - 17), 50, 6);
            XTween_GUI.Gui_Labelfield(r_pro, title, GUIFilled.无, GUIColor.无, Color.black * 0.9f, TextAnchor.MiddleLeft, new Vector2(0, 0), 12, Font_Light);
            // 数值
            r_pro.Set(rect_progress.x + (rect_progress.width - mar_right - mar_value), rect_progress.y + (height - 17), 50, 6);
            XTween_GUI.Gui_Labelfield(r_pro, value, GUIFilled.无, GUIColor.无, Color.black * 0.95f, TextAnchor.MiddleRight, new Vector2(0, 0), 11, Font_Light);
            // 起点线
            r_pro.Set((rect_progress.x + mar_left), rect_progress.y + (height - 2), 1, 6);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 终点线
            r_pro.Set((rect_progress.x + (rect_progress.width - mar_right + mar_left)), rect_progress.y + (height - 2), 1, 6);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
            // 指示器
            r_pro.Set(((rect_progress.x + mar_left - 4) + (rect_progress.width - mar_right) * progress), rect_progress.y + (height + 3), 8, 8);
            XTween_GUI.Gui_Icon(r_pro, icon_pathpercent);
            // 中点线
            r_pro.Set(((rect_progress.x + mar_left) + (rect_progress.width - mar_right) * 0.5f), rect_progress.y + (height - 10), 1, 10);
            EditorGUI.DrawRect(r_pro, Color.black * 0.3f);
        }
        /// <summary>
        /// 获取缓动参数曲线图
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        private Texture2D GetEasePic(EaseMode ease)
        {
            Texture2D ease_tex = null;
            for (int i = 0; i < BaseScript.EasePics.Length; i++)
            {
                if (BaseScript.EasePics[i].name == ease.ToString())
                {
                    ease_tex = BaseScript.EasePics[i];
                    break;
                }
            }
            return ease_tex;
        }
        private Texture2D GetEasePic(string ease)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>($"{XTween_Dashboard.Get_path_XTween_GUIStyle_Path()}Icon/EaseCurveGraph/{ease}.png");
        }
        /// <summary>
        /// 获取缓动参数曲线图背景
        /// </summary>        
        /// <returns></returns>
        private Texture2D GetEasePicBg()
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>($"{XTween_Dashboard.Get_path_XTween_GUIStyle_Path()}Icon/EaseCurveGraph/bg.png");
        }
        #endregion

        #region xxxx
        private void DrawStatisticsSection(XTween_Manager manager)
        {
            _showStatistics = EditorGUILayout.Foldout(_showStatistics, "统计", true);
            if (!_showStatistics) return;

            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField($"激活的动画: {manager.Get_TweenCount_ActiveTween()}");
            EditorGUILayout.LabelField($"等待添加: {manager.Get_PendingAddCount()}");
            EditorGUILayout.LabelField($"等待移除: {manager.Get_PendingRemoveCount()}");
            EditorGUI.indentLevel--;
        }
        private void DrawPendingOperationsSection(XTween_Manager manager)
        {
            _showPendingOperations = EditorGUILayout.Foldout(_showPendingOperations, "等待操作的动画", true);
            if (!_showPendingOperations) return;

            EditorGUI.indentLevel++;

            var pendingAdd = manager.Get_PendingAddTweens();
            var pendingRemove = manager.Get_PendingRemoveTweens();

            EditorGUILayout.LabelField($"增加: {pendingAdd.Count}");
            foreach (var tween in pendingAdd)
            {
                DrawTweenInfoSimple(tween);
            }

            EditorGUILayout.LabelField($"移除: {pendingRemove.Count}");
            foreach (var tween in pendingRemove)
            {
                DrawTweenInfoSimple(tween);
            }

            EditorGUI.indentLevel--;
        }
        private void DrawTweenInfoSimple(XTween_Interface tween)
        {
            if (tween == null) return;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField($"{tween.ShortId} - {GetStateString(tween)} - {GetProgressString(tween)}");
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}