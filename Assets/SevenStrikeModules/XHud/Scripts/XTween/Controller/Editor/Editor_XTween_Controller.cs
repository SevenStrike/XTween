namespace SevenStrikeModules.XTween
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(XTween_Controller))]
    public class Editor_XTween_Controller : Editor
    {
        private XTween_Controller BaseScript;

        /// <summary>
        /// 序列化属性
        /// </summary>
        private SerializedProperty sp_Duration, sp_Delay, sp_UseRandomDelay, sp_RandomDelay, sp_EaseMode, sp_UseCurve, sp_Curve, sp_LoopCount, sp_LoopDelay, sp_LoopType, sp_IsFromMode, sp_IsRelative, sp_IsAutoKill, sp_EndValue_String, sp_EndValue_Int, sp_EndValue_Float, sp_EndValue_Vector2, sp_EndValue_Vector3, sp_EndValue_Vector4, sp_EndValue_Color, sp_EndValue_Quaternion, sp_FromValue_Int, sp_FromValue_Float, sp_FromValue_String, sp_FromValue_Vector2, sp_FromValue_Vector3, sp_FromValue_Vector4, sp_FromValue_Color, sp_FromValue_Quaternion, sp_Target_PathTool, sp_TweenTypes, sp_TweenTypes_Positions, sp_TweenTypes_Rotations, sp_TweenTypes_Alphas, sp_TweenTypes_Shakes, sp_TweenTypes_XHudManager, sp_TweenTypes_XHudAnimator, sp_TweenTypes_XHudText, sp_TweenTypes_XHudTmpText, sp_TweenTypes_XHudElement, sp_TweenTypes_To, sp_index_TweenTypes, sp_index_TweenTypes_Positions, sp_index_TweenTypes_Rotations, sp_index_TweenTypes_Alphas, sp_index_TweenTypes_Shakes, sp_index_TweenTypes_XHudManager, sp_index_TweenTypes_XHudAnimator, sp_index_TweenTypes_XHudText, sp_index_TweenTypes_XHudTmpText, sp_index_TweenTypes_XHudElement, sp_index_TweenTypes_To, sp_Target_RectTransform, sp_Target_Image, sp_Target_CanvasGroup, sp_Target_HudAnimator, sp_Target_HudElement, sp_Target_HudText, sp_Target_HudTmpText, sp_Target_HudManager, sp_Target_Int, sp_Target_Float, sp_Target_String, sp_Target_Vector2, sp_Target_Vector3, sp_Target_Vector4, sp_Target_Color, sp_index_AutoKillPreviewTweens, sp_index_RewindPreviewTweensWithKill, sp_index_ClearPreviewTweensWithKill, sp_LiquidLEDBlink, sp_keyControl_Tween_Play, sp_keyControl_Tween_Rewind, sp_keyControl_Tween_Kill, sp_keyControl_Tween_Replay, sp_keyControl_Enabled, sp_keyControl_Tween_Create, sp_DebugMode, sp_IsExtendedString, sp_HudRotateMode, sp_Vibrato, sp_Randomness, sp_FadeShake;


        /// <summary>
        /// 图标
        /// </summary>
        private Texture2D icon_main, icon_status, icon_preview_r, icon_preview_p, icon_rewind_r, icon_rewind_p, icon_kill_r, icon_kill_p, icon_pathpercent, icon_statu_autokill, icon_statu_cycle, icon_statu_relative, icon_statu_tomode, icon_statu_remode_restart, icon_statu_remode_yoyo, LiquidBg_Expand_Ready, LiquidBg_Expand_Ready_Scanline, LiquidBg_Expand_Playing, LiquidBg_Expand_Playing_Scanline, LiquidBg_NoExpand_Ready, LiquidBg_NoExpand_Ready_Scanline, LiquidBg_NoExpand_Playing,
            LiquidBg_NoExpand_Playing_Scanline, LiquidPlug_Red, MetalGrid, LiquidDirty, LiquidDirty_Small;

        private bool BasicVars = false;
        private bool IsPreviewed = false;
        private float TweenEasedProgress, TweenLoopProgress;
        private Color TweenLedOnColor;
        private string TweenLiquidContent;
        private Texture2D TweenLiquidScreen;
        private Texture2D EasePic, EasePicBg;

        Rect rect_liquid_prim;
        Rect rect_liquid_set;
        RectOffset liquid_rectoffet;

        #region 动画类型
        private XTweenTypes TweenTypes;
        private XTweenTypes_Positions TweenTypes_Positions;
        private XTweenTypes_Rotations TweenTypes_Rotations;
        private XTweenTypes_Alphas TweenTypes_Alphas;
        private XTweenTypes_Shakes TweenTypes_Shakes;
        private XTweenTypes_XHudManager TweenTypes_XHudManager;
        private XTweenTypes_XHudAnimator TweenTypes_XHudAnimator;
        private XTweenTypes_XHudText TweenTypes_XHudText;
        private XTweenTypes_XHudTmpText TweenTypes_XHudTmpText;
        private XTweenTypes_XHudElement TweenTypes_XHudElement;
        private XTweenTypes_To TweenTypes_To;
        #endregion

        #region 呼吸灯效果
        private static double lastUpdateTime;
        private static float LedBreathSpeed = 6;
        #endregion

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

        #region 批量化操作

        private XTween_Controller[] SelectedObjects;

        private void GetAllTargets()
        {
            if (targets.Length > 1)
            {
                SelectedObjects = new XTween_Controller[targets.Length];
                for (int i = 0; i < SelectedObjects.Length; i++)
                {
                    var t = targets[i];
                    SelectedObjects[i] = (XTween_Controller)t;
                }
            }
            else
            {
                SelectedObjects = new XTween_Controller[targets.Length];
                SelectedObjects[0] = (XTween_Controller)target;
            }
        }

        private bool IsMultiSelected()
        {
            if (SelectedObjects == null)
                return false;
            if (SelectedObjects.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        void OnEnable()
        {
            BaseScript = (XTween_Controller)target;

            #region 获取序列化属性
            sp_Duration = serializedObject.FindProperty("Duration");
            sp_Delay = serializedObject.FindProperty("Delay");
            sp_UseRandomDelay = serializedObject.FindProperty("UseRandomDelay");
            sp_RandomDelay = serializedObject.FindProperty("RandomDelay");
            sp_EaseMode = serializedObject.FindProperty("EaseMode");
            sp_UseCurve = serializedObject.FindProperty("UseCurve");
            sp_Curve = serializedObject.FindProperty("Curve");
            sp_LoopCount = serializedObject.FindProperty("LoopCount");
            sp_LoopDelay = serializedObject.FindProperty("LoopDelay");
            sp_LoopType = serializedObject.FindProperty("LoopType");
            sp_IsFromMode = serializedObject.FindProperty("IsFromMode");
            sp_IsRelative = serializedObject.FindProperty("IsRelative");
            sp_IsAutoKill = serializedObject.FindProperty("IsAutoKill");
            sp_IsExtendedString = serializedObject.FindProperty("IsExtendedString");
            sp_EndValue_String = serializedObject.FindProperty("EndValue_String");
            sp_EndValue_Int = serializedObject.FindProperty("EndValue_Int");
            sp_EndValue_Float = serializedObject.FindProperty("EndValue_Float");
            sp_EndValue_Vector2 = serializedObject.FindProperty("EndValue_Vector2");
            sp_EndValue_Vector3 = serializedObject.FindProperty("EndValue_Vector3");
            sp_EndValue_Vector4 = serializedObject.FindProperty("EndValue_Vector4");
            sp_EndValue_Color = serializedObject.FindProperty("EndValue_Color");
            sp_EndValue_Quaternion = serializedObject.FindProperty("EndValue_Quaternion");
            sp_HudRotateMode = serializedObject.FindProperty("HudRotateMode");
            sp_Vibrato = serializedObject.FindProperty("Vibrato");
            sp_Randomness = serializedObject.FindProperty("Randomness");
            sp_FadeShake = serializedObject.FindProperty("FadeShake");

            sp_FromValue_Int = serializedObject.FindProperty("FromValue_Int");
            sp_FromValue_Float = serializedObject.FindProperty("FromValue_Float");
            sp_FromValue_String = serializedObject.FindProperty("FromValue_String");
            sp_FromValue_Vector2 = serializedObject.FindProperty("FromValue_Vector2");
            sp_FromValue_Vector3 = serializedObject.FindProperty("FromValue_Vector3");
            sp_FromValue_Vector4 = serializedObject.FindProperty("FromValue_Vector4");
            sp_FromValue_Color = serializedObject.FindProperty("FromValue_Color");
            sp_FromValue_Quaternion = serializedObject.FindProperty("FromValue_Quaternion");

            sp_Target_PathTool = serializedObject.FindProperty("Target_PathTool");

            sp_TweenTypes = serializedObject.FindProperty("TweenTypes");
            sp_TweenTypes_Positions = serializedObject.FindProperty("TweenTypes_Positions");
            sp_TweenTypes_Rotations = serializedObject.FindProperty("TweenTypes_Rotations");
            sp_TweenTypes_Alphas = serializedObject.FindProperty("TweenTypes_Alphas");
            sp_TweenTypes_Shakes = serializedObject.FindProperty("TweenTypes_Shakes");
            sp_TweenTypes_XHudManager = serializedObject.FindProperty("TweenTypes_XHudManager");
            sp_TweenTypes_XHudAnimator = serializedObject.FindProperty("TweenTypes_XHudAnimator");
            sp_TweenTypes_XHudText = serializedObject.FindProperty("TweenTypes_XHudText");
            sp_TweenTypes_XHudTmpText = serializedObject.FindProperty("TweenTypes_XHudTmpText");
            sp_TweenTypes_XHudElement = serializedObject.FindProperty("TweenTypes_XHudElement");

            sp_TweenTypes_To = serializedObject.FindProperty("TweenTypes_To");

            sp_index_TweenTypes = serializedObject.FindProperty("index_TweenTypes");
            sp_index_TweenTypes_Positions = serializedObject.FindProperty("index_TweenTypes_Positions");
            sp_index_TweenTypes_Rotations = serializedObject.FindProperty("index_TweenTypes_Rotations");
            sp_index_TweenTypes_Alphas = serializedObject.FindProperty("index_TweenTypes_Alphas");
            sp_index_TweenTypes_Shakes = serializedObject.FindProperty("index_TweenTypes_Shakes");
            sp_index_TweenTypes_XHudManager = serializedObject.FindProperty("index_TweenTypes_XHudManager");
            sp_index_TweenTypes_XHudAnimator = serializedObject.FindProperty("index_TweenTypes_XHudAnimator");
            sp_index_TweenTypes_XHudText = serializedObject.FindProperty("index_TweenTypes_XHudText");
            sp_index_TweenTypes_XHudTmpText = serializedObject.FindProperty("index_TweenTypes_XHudTmpText");
            sp_index_TweenTypes_XHudElement = serializedObject.FindProperty("index_TweenTypes_XHudElement");

            sp_index_TweenTypes_To = serializedObject.FindProperty("index_TweenTypes_To");

            sp_Target_RectTransform = serializedObject.FindProperty("Target_RectTransform");
            sp_Target_Image = serializedObject.FindProperty("Target_Image");
            sp_Target_CanvasGroup = serializedObject.FindProperty("Target_CanvasGroup");
            sp_Target_HudAnimator = serializedObject.FindProperty("Target_HudAnimator");
            sp_Target_HudElement = serializedObject.FindProperty("Target_HudElement");
            sp_Target_HudText = serializedObject.FindProperty("Target_HudText");
            sp_Target_HudTmpText = serializedObject.FindProperty("Target_HudTmpText");
            sp_Target_HudManager = serializedObject.FindProperty("Target_HudManager");

            sp_Target_Int = serializedObject.FindProperty("Target_Int");
            sp_Target_Float = serializedObject.FindProperty("Target_Float");
            sp_Target_String = serializedObject.FindProperty("Target_String");
            sp_Target_Vector2 = serializedObject.FindProperty("Target_Vector2");
            sp_Target_Vector3 = serializedObject.FindProperty("Target_Vector3");
            sp_Target_Vector4 = serializedObject.FindProperty("Target_Vector4");
            sp_Target_Color = serializedObject.FindProperty("Target_Color");

            sp_index_AutoKillPreviewTweens = serializedObject.FindProperty("index_AutoKillPreviewTweens");
            sp_index_RewindPreviewTweensWithKill = serializedObject.FindProperty("index_RewindPreviewTweensWithKill");
            sp_index_ClearPreviewTweensWithKill = serializedObject.FindProperty("index_ClearPreviewTweensWithKill");

            sp_keyControl_Tween_Play = serializedObject.FindProperty("keyControl_Tween_Play");
            sp_keyControl_Tween_Rewind = serializedObject.FindProperty("keyControl_Tween_Rewind");
            sp_keyControl_Tween_Kill = serializedObject.FindProperty("keyControl_Tween_Kill");
            sp_keyControl_Tween_Replay = serializedObject.FindProperty("keyControl_Tween_Replay");
            sp_keyControl_Enabled = serializedObject.FindProperty("keyControl_Enabled");
            sp_keyControl_Tween_Create = serializedObject.FindProperty("keyControl_Tween_Create");
            sp_LiquidLEDBlink = serializedObject.FindProperty("LiquidLEDBlink");

            sp_DebugMode = serializedObject.FindProperty("DebugMode");
            #endregion

            #region 图标获取
            icon_main = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_main");
            icon_status = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_status");
            icon_preview_r = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_preview_r");
            icon_preview_p = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_preview_p");
            icon_rewind_r = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_rewind_r");
            icon_rewind_p = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_rewind_p");
            icon_kill_r = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_kill_r");
            icon_kill_p = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_kill_p");
            icon_pathpercent = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_pathpercent");

            icon_statu_autokill = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_statu_autokill");
            icon_statu_cycle = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_statu_cycle");
            icon_statu_relative = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_statu_relative");
            icon_statu_tomode = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_statu_tomode");
            icon_statu_remode_restart = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_statu_remode_restart");
            icon_statu_remode_yoyo = util_XHUDGUI.GetIcon("Icons_Hud_XTween_Controller/icon_statu_remode_yoyo");

            LiquidBg_Expand_Ready = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_Expand_Ready");
            LiquidBg_NoExpand_Ready = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_NoExpand_Ready");
            LiquidBg_Expand_Ready_Scanline = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_Expand_Ready_Scanline");
            LiquidBg_NoExpand_Ready_Scanline = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_NoExpand_Ready_Scanline");
            LiquidBg_Expand_Playing = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_Expand_Playing");
            LiquidBg_NoExpand_Playing = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_NoExpand_Playing");
            LiquidBg_Expand_Playing_Scanline = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_Expand_Playing_Scanline");
            LiquidBg_NoExpand_Playing_Scanline = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidBg_NoExpand_Playing_Scanline");

            LiquidPlug_Red = util_XHUDGUI.GetIcon("Icons_Liquid/LiquidPlug_Red");
            MetalGrid = util_XHUDGUI.GetIcon("Icons_Liquid/MetalGrid");
            LiquidDirty = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidDirty");
            LiquidDirty_Small = util_XHUDGUI.GetIcon("Icons_Liquid/XTween_Controller/LiquidDirty_Small");
            #endregion

            Font_Bold = util_XHUDGUI.GetFont("SS_Editor_Bold");
            Font_Light = util_XHUDGUI.GetFont("SS_Editor_Light");

            EasePic = GetEasePic((EaseMode)sp_EaseMode.enumValueIndex);
            EasePicBg = GetEasePicBg();

            GetAllTargets();
            GetComponents();

            TweenLedOnColor = XTween_Dashboard.Theme_Primary;

            #region 动画预览器参数状态获取
            sp_index_AutoKillPreviewTweens.boolValue = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("PreviewTween_AutoKillWithDuration");
            sp_index_AutoKillPreviewTweens.serializedObject.ApplyModifiedProperties();

            sp_index_RewindPreviewTweensWithKill.boolValue = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("PreviewTween_RewindPreviewTweensWithKill");
            sp_index_RewindPreviewTweensWithKill.serializedObject.ApplyModifiedProperties();

            sp_index_ClearPreviewTweensWithKill.boolValue = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("PreviewTween_ClearPreviewTweensWithKill");
            sp_index_ClearPreviewTweensWithKill.serializedObject.ApplyModifiedProperties();
            #endregion           

            #region 液晶LED闪烁
            XTween_Dashboard.Tween_LiquidLEDBlink = sp_LiquidLEDBlink.boolValue;
            sp_LiquidLEDBlink.boolValue = util_Tools.PlayerPrefs_ReadValue_Bool_ForEditor("TweenController_LiquidLEDBlink");
            sp_LiquidLEDBlink.serializedObject.ApplyModifiedProperties();

            if (sp_LiquidLEDBlink.boolValue)
            {
                // 注册更新回调
                EditorApplication.update += OnEditorUpdate;
                lastUpdateTime = EditorApplication.timeSinceStartup;
            }
            #endregion

            liquid_rectoffet = new RectOffset(45, 45, 20, 20);
        }

        private void OnDisable()
        {
            // 注销更新回调
            EditorApplication.update -= OnEditorUpdate;

            Preview_Kill();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            util_XHUDGUI.Gui_Layout_Banner(icon_main, HudFilled.实体, HudColor.深空灰, "XTween - 动画控制器", Color.white, 20, 20);
            #region 预览
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "动画预览", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(10);

            util_XHUDGUI.Gui_Layout_Horizontal_Start(HudFilled.无, HudColor.无, 0);
            util_XHUDGUI.Gui_Layout_Space(10);
            #region 预览
            GUI.enabled = true;
            if (IsPreviewed)
            {
                if (util_XHUDGUI.Gui_Layout_Button(15, "杀死", icon_kill_r, icon_kill_p))
                {
                    Preview_Kill();
                    return;
                }
            }
            else
            {
                if (util_XHUDGUI.Gui_Layout_Button(15, "预览", icon_preview_r, icon_preview_p))
                {
                    if (!ValidPreviewed())
                    {
                        if (sp_DebugMode.boolValue)
                            util_Tools.Func_PrintInfo("XTween动画管理器消息", "因缺失组件或异常问题，导致无法预览动画！请检查组件项中是否缺失组件或是其他异常问题弹窗内容！", HudMsgState.警告);
                        return;
                    }
                    Preview_Start(); return;
                }
            }
            #endregion
            GUILayout.FlexibleSpace();
            #region 倒退
            GUI.enabled = true;
            if (util_XHUDGUI.Gui_Layout_Button(15, "倒退", icon_rewind_r, icon_rewind_p))
            {
                Preview_Rewind();
                return;
            }
            #endregion
            util_XHUDGUI.Gui_Layout_Space(10);
            util_XHUDGUI.Gui_Layout_Horizontal_End();

            util_XHUDGUI.Gui_Layout_Space(10);
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion

            rect_liquid_prim = GUILayoutUtility.GetLastRect();
            rect_liquid_set = rect_liquid_prim;

            #region 动画状态
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "动画状态", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(10);

            #region 面板元素坐标计算
            float liquid_left_margin = 35;
            float liquid_right_margin = 100;
            float CurrentPanelWidth = EditorGUIUtility.currentViewWidth;

            bool IsExpandPanel = CurrentPanelWidth > 309 ? true : false;
            bool IsExtraExpandPanel = CurrentPanelWidth < 215 ? true : false;

            if (IsExpandPanel)
                liquid_right_margin = 212;

            float height = IsExpandPanel ? 210 : 242;
            #endregion

            if (!IsMultiSelected())
            {
                #region 动画的Lcd信息刷新
                if (IsPreviewed || BaseScript.CurrentTweener != null ? BaseScript.CurrentTweener.IsPlaying : false)
                {
                    TweenLiquidContent = "正在动画...";

                    if (XTween_Dashboard.LiquidScanStyle)
                        TweenLiquidScreen = IsExpandPanel ? LiquidBg_Expand_Playing_Scanline : LiquidBg_NoExpand_Playing_Scanline;
                    else
                        TweenLiquidScreen = IsExpandPanel ? LiquidBg_Expand_Playing : LiquidBg_NoExpand_Playing;

                    TweenEasedProgress = BaseScript.CurrentTweener != null ? BaseScript.CurrentTweener.CurrentEasedProgress : 0;
                    TweenLoopProgress = BaseScript.CurrentTweener != null ? BaseScript.CurrentTweener.CurrentLoopProgress : 0;

                    if (XTween_Dashboard.LiquidLagacyColor)
                        GUI.backgroundColor = XTween_Dashboard.Liquid_On_Color;
                    else
                        GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
                    Repaint();
                }
                else
                {
                    if (XTween_Dashboard.LiquidLagacyColor)
                        GUI.backgroundColor = XTween_Dashboard.Liquid_Off_Color;
                    else
                        GUI.backgroundColor = Color.white;

                    TweenLiquidContent = "已就绪";

                    if (XTween_Dashboard.LiquidScanStyle)
                        TweenLiquidScreen = IsExpandPanel ? LiquidBg_Expand_Ready_Scanline : LiquidBg_NoExpand_Ready_Scanline;
                    else
                        TweenLiquidScreen = IsExpandPanel ? LiquidBg_Expand_Ready : LiquidBg_NoExpand_Ready;

                    TweenLedOnColor = Color.white * 0.5f;
                    TweenEasedProgress = 0;
                    TweenLoopProgress = 0;
                }

                rect_liquid_prim.Set(rect_liquid_set.x + 15, rect_liquid_set.y + 98, rect_liquid_set.width - 30, IsExpandPanel ? LiquidBg_Expand_Ready.height : LiquidBg_NoExpand_Ready.height);
                util_XHUDGUI.Gui_LiquidField(
                    rect_liquid_prim,
                    TweenLiquidContent,
                    liquid_rectoffet,
                    TweenLiquidScreen);

                // 液晶屏肮脏
                if (!IsExtraExpandPanel)
                {
                    if (XTween_Dashboard.LiquidDirty)
                    {
                        rect_liquid_prim.Set(rect_liquid_set.x + (IsExpandPanel ? (rect_liquid_set.width - LiquidDirty.width - 13) : (rect_liquid_set.width - LiquidDirty_Small.width - 13)), rect_liquid_set.y + 97, IsExpandPanel ? LiquidDirty.width : LiquidDirty_Small.width, IsExpandPanel ? LiquidDirty.height : LiquidDirty_Small.height);
                        util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, IsExpandPanel ? LiquidDirty : LiquidDirty_Small);
                    }
                }

                // 液晶屏接口
                if (!IsExtraExpandPanel)
                {
                    rect_liquid_prim.Set(rect_liquid_set.x + ((rect_liquid_set.width / 2) - (LiquidPlug_Red.width / 2)), IsExpandPanel ? rect_liquid_set.y + 288 : rect_liquid_set.y + 388, LiquidPlug_Red.width, LiquidPlug_Red.height);
                    util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, LiquidPlug_Red);
                }
                // 液晶屏金属网格角
                if (!IsExtraExpandPanel)
                {
                    rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width - MetalGrid.width - 5), IsExpandPanel ? rect_liquid_set.y + 248 : rect_liquid_set.y + 348, MetalGrid.width, MetalGrid.height);
                    util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, MetalGrid);
                }

                GUI.backgroundColor = Color.white;
                #endregion

                #region 呼吸Led效果
                if (IsPreviewed || BaseScript.CurrentTweener != null ? BaseScript.CurrentTweener.IsPlaying : false)
                {
                    // 呼吸效果计算
                    if (sp_LiquidLEDBlink.boolValue)
                    {
                        float alpha = (Mathf.Sin((float)(EditorApplication.timeSinceStartup * LedBreathSpeed) * Mathf.PI) + 1) * 0.5f;
                        TweenLedOnColor = new Color(XTween_Dashboard.Theme_Primary.r, XTween_Dashboard.Theme_Primary.g, XTween_Dashboard.Theme_Primary.b, alpha);
                    }
                    else
                        TweenLedOnColor = XTween_Dashboard.Theme_Primary;
                }

                // 绘制呼吸Led
                rect_liquid_prim.Set(rect_liquid_set.x + (rect_liquid_set.width / 2) - 2, rect_liquid_set.y + (IsExpandPanel ? LiquidBg_Expand_Ready.height : LiquidBg_NoExpand_Ready.height) + 90, 4, 2);
                EditorGUI.DrawRect(rect_liquid_prim, TweenLedOnColor);
                #endregion

                #region ID 显示
                rect_liquid_prim.Set(rect_liquid_set.x + 35, rect_liquid_set.y + 112, rect_liquid_set.width - 60, 65);
                util_XHUDGUI.Gui_Labelfield_WrapText(rect_liquid_prim, $"ID :  {(BaseScript.CurrentTweener == null ? "-" : BaseScript.CurrentTweener.UniqueId.ToString())}", HudFilled.无, HudColor.无, Color.black, TextAnchor.MiddleLeft, Vector2.zero, 11, false, true, TextClipping.Ellipsis, true, Font_Light);

                rect_liquid_prim.Set(rect_liquid_set.x + 35, rect_liquid_set.y + 135, rect_liquid_set.width - 60, 65);
                util_XHUDGUI.Gui_Labelfield_WrapText(rect_liquid_prim, $"短 ID :  {(BaseScript.CurrentTweener == null ? "-" : BaseScript.CurrentTweener.ShortId)}", HudFilled.无, HudColor.无, Color.black, TextAnchor.MiddleLeft, Vector2.zero, 11, false, true, TextClipping.Ellipsis, true, Font_Light);
                #endregion

                #region 进度条 - EasedProgress
                // 背景线
                rect_liquid_prim.Set(rect_liquid_set.x + liquid_left_margin, rect_liquid_set.y + height, (CurrentPanelWidth - liquid_right_margin), 1);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                // 进度条
                rect_liquid_prim.Set(rect_liquid_set.x + liquid_left_margin, rect_liquid_set.y + height - 4, (CurrentPanelWidth - liquid_right_margin) * TweenEasedProgress, 4);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black);
                // 标题
                rect_liquid_prim.Set(rect_liquid_set.x + liquid_left_margin, rect_liquid_set.y + (height - 20), 50, 6);
                util_XHUDGUI.Gui_Labelfield(rect_liquid_prim, "缓动进度", HudFilled.无, HudColor.无, Color.black * 0.7f, TextAnchor.MiddleLeft, new Vector2(0, 0), 11);
                // 数值
                rect_liquid_prim.Set(rect_liquid_set.x + (CurrentPanelWidth - liquid_right_margin - 25), rect_liquid_set.y + (height - 20), 50, 6);
                util_XHUDGUI.Gui_Labelfield(rect_liquid_prim, sp_UseCurve.boolValue ? "CustomCurve" : ((EaseMode)sp_EaseMode.enumValueIndex).ToString(), HudFilled.无, HudColor.无, Color.black * 0.85f, TextAnchor.MiddleRight, new Vector2(0, 0), 11, Font_Light);
                // 起点线
                rect_liquid_prim.Set((rect_liquid_set.x + liquid_left_margin), rect_liquid_set.y + (height - 2), 1, 6);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                // 终点线
                rect_liquid_prim.Set((rect_liquid_set.x + (CurrentPanelWidth - liquid_right_margin + liquid_left_margin)), rect_liquid_set.y + (height - 2), 1, 6);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                // 指示器
                rect_liquid_prim.Set(((rect_liquid_set.x + liquid_left_margin - 4) + (CurrentPanelWidth - liquid_right_margin) * TweenEasedProgress), rect_liquid_set.y + (height + 3), 8, 8);
                util_XHUDGUI.Gui_Icon(rect_liquid_prim, icon_pathpercent);
                // 中点线
                rect_liquid_prim.Set(((rect_liquid_set.x + liquid_left_margin) + (CurrentPanelWidth - liquid_right_margin) * 0.5f), rect_liquid_set.y + (height - 10), 1, 10);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                #endregion

                height += 42;

                #region 进度条 - LoopProgress
                // 背景线
                rect_liquid_prim.Set(rect_liquid_set.x + liquid_left_margin, rect_liquid_set.y + height, (CurrentPanelWidth - liquid_right_margin), 1);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                // 进度条
                rect_liquid_prim.Set(rect_liquid_set.x + liquid_left_margin, rect_liquid_set.y + height - 4, (CurrentPanelWidth - liquid_right_margin) * TweenLoopProgress, 4);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black);
                // 标题
                rect_liquid_prim.Set(rect_liquid_set.x + liquid_left_margin, rect_liquid_set.y + (height - 20), 50, 6);
                util_XHUDGUI.Gui_Labelfield(rect_liquid_prim, "实际进度", HudFilled.无, HudColor.无, Color.black * 0.7f, TextAnchor.MiddleLeft, new Vector2(0, 0), 11);
                // 数值
                rect_liquid_prim.Set(rect_liquid_set.x + (CurrentPanelWidth - liquid_right_margin - 25), rect_liquid_set.y + (height - 20), 50, 6);
                util_XHUDGUI.Gui_Labelfield(rect_liquid_prim, $"耗时 {sp_Duration.floatValue} s / 延迟 {sp_Delay.floatValue} s", HudFilled.无, HudColor.无, Color.black * 0.85f, TextAnchor.MiddleRight, new Vector2(0, 0), 11, Font_Light);
                // 起点线
                rect_liquid_prim.Set((rect_liquid_set.x + liquid_left_margin), rect_liquid_set.y + (height - 2), 1, 6);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                // 终点线
                rect_liquid_prim.Set((rect_liquid_set.x + (CurrentPanelWidth - liquid_right_margin + liquid_left_margin)), rect_liquid_set.y + (height - 2), 1, 6);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                // 指示器
                rect_liquid_prim.Set(((rect_liquid_set.x + liquid_left_margin - 4) + (CurrentPanelWidth - liquid_right_margin) * TweenLoopProgress), rect_liquid_set.y + (height + 3), 8, 8);
                util_XHUDGUI.Gui_Icon(rect_liquid_prim, icon_pathpercent);
                // 中点线
                rect_liquid_prim.Set(((rect_liquid_set.x + liquid_left_margin) + (CurrentPanelWidth - liquid_right_margin) * 0.5f), rect_liquid_set.y + (height - 10), 1, 10);
                EditorGUI.DrawRect(rect_liquid_prim, Color.black * 0.3f);
                #endregion

                #region 动画状态图标

                float distanceStatu = 30;

                if (sp_IsAutoKill.boolValue)
                    GUI.color = Color.white;
                else
                    GUI.color = Color.white * 0.2f;
                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - distanceStatu : rect_liquid_set.x + 136, IsExpandPanel ? rect_liquid_set.y + 110 : rect_liquid_set.y + 185, 15, 15);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, icon_statu_autokill);

                distanceStatu += 30;

                if (sp_IsRelative.boolValue)
                    GUI.color = Color.white;
                else
                    GUI.color = Color.white * 0.2f;
                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - distanceStatu : rect_liquid_set.x + 109, IsExpandPanel ? rect_liquid_set.y + 110 : rect_liquid_set.y + 185, 15, 15);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, icon_statu_relative);

                distanceStatu += 30;

                if (sp_LoopCount.intValue < 0 || sp_LoopCount.intValue > 0)
                    GUI.color = Color.white;
                else
                    GUI.color = Color.white * 0.2f;
                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - distanceStatu : rect_liquid_set.x + 82, IsExpandPanel ? rect_liquid_set.y + 110 : rect_liquid_set.y + 185, 15, 15);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, icon_statu_cycle);

                distanceStatu += 30;

                if (sp_TweenTypes.enumValueIndex == 1)
                    GUI.color = Color.white;
                else
                    GUI.color = Color.white * 0.2f;
                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - distanceStatu : rect_liquid_set.x + 55, IsExpandPanel ? rect_liquid_set.y + 110 : rect_liquid_set.y + 185, 15, 15);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, icon_statu_tomode);
                GUI.color = Color.white;

                distanceStatu += 30;

                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - distanceStatu : rect_liquid_set.x + 28, IsExpandPanel ? rect_liquid_set.y + 110 : rect_liquid_set.y + 185, 15, 15);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, sp_LoopType.enumValueIndex == 0 ? icon_statu_remode_restart : icon_statu_remode_yoyo);
                #endregion

                #region EaseGraph图形
                GUI.color = Color.black;
                if (sp_UseCurve.boolValue)
                    GUI.color = Color.black * 0.2f;
                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - 110 : rect_liquid_set.x + 22, IsExpandPanel ? rect_liquid_set.y + 195 : rect_liquid_set.y + 304, 100, 65);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, EasePicBg);
                rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - 110 : rect_liquid_set.x + 22, IsExpandPanel ? rect_liquid_set.y + 195 : rect_liquid_set.y + 304, 100, 65);
                util_XHUDGUI.Gui_TextureBox(rect_liquid_prim, EasePic);

                if (sp_UseCurve.boolValue)
                {
                    GUI.color = Color.black;
                    rect_liquid_prim.Set(IsExpandPanel ? rect_liquid_set.width - 90 : rect_liquid_set.x + 36, IsExpandPanel ? rect_liquid_set.y + 195 : rect_liquid_set.y + 285, 100, 65);
                    util_XHUDGUI.Gui_Labelfield(rect_liquid_prim, "CustomCurve", HudFilled.无, HudColor.无, Color.black, TextAnchor.MiddleLeft, 10, Font_Bold);
                }
                GUI.color = Color.white;
                #endregion
                util_XHUDGUI.Gui_Layout_Space(IsExpandPanel ? 218 : 318);
            }
            else
            {
                EditorGUILayout.HelpBox("暂不支持多选控制信息查看！", MessageType.Info);
                util_XHUDGUI.Gui_Layout_Space(10);
            }
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion

            #region 动画类型
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "动画类型", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(5);

            #region 动画目标
            string[] actionlist = System.Enum.GetNames(typeof(XTweenTypes));
            Undo.RecordObject(sp_TweenTypes.serializedObject.targetObject, "TweenTypesSet");
            util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("动画目标", actionlist, ref sp_index_TweenTypes, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
            {
                for (int i = 0; i < SelectedObjects.Length; i++)
                {
                    SelectedObjects[i].TweenTypes = (XTweenTypes)System.Enum.Parse(typeof(XTweenTypes), sp_index_TweenTypes.stringValue);
                }
                GetComponents();
            }, (res) =>
            {
                sp_TweenTypes.enumValueIndex = (int)(XTweenTypes)System.Enum.Parse(typeof(XTweenTypes), res);
                GetComponents();
            });
            #endregion

            TweenTypes = (XTweenTypes)sp_TweenTypes.enumValueIndex;

            #region 位置类型
            if (TweenTypes == XTweenTypes.位置_Position)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_Positions));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("位置类型", actionlist, ref sp_index_TweenTypes_Positions, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_Positions = (XTweenTypes_Positions)System.Enum.Parse(typeof(XTweenTypes_Positions), sp_index_TweenTypes_Positions.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_Positions.enumValueIndex = (int)(XTweenTypes_Positions)System.Enum.Parse(typeof(XTweenTypes_Positions), res);
                    GetComponents();
                });
            }
            #endregion

            #region 旋转类型
            if (TweenTypes == XTweenTypes.旋转_Rotation)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_Rotations));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("旋转类型", actionlist, ref sp_index_TweenTypes_Rotations, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_Rotations = (XTweenTypes_Rotations)System.Enum.Parse(typeof(XTweenTypes_Rotations), sp_index_TweenTypes_Rotations.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_Rotations.enumValueIndex = (int)(XTweenTypes_Rotations)System.Enum.Parse(typeof(XTweenTypes_Rotations), res);
                    GetComponents();
                });
            }
            #endregion

            #region 透明度类型
            if (TweenTypes == XTweenTypes.透明度_Alpha)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_Alphas));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("透明度类型", actionlist, ref sp_index_TweenTypes_Alphas, HudFilled.实体, 120, 22, SelectedObjects, (comps) => { }, (res) =>
                {
                    sp_TweenTypes_Alphas.enumValueIndex = (int)(XTweenTypes_Alphas)System.Enum.Parse(typeof(XTweenTypes_Alphas), res);
                    GetComponents();
                });
            }
            #endregion

            #region 震动类型
            if (TweenTypes == XTweenTypes.震动_Shake)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_Shakes));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("震动类型", actionlist, ref sp_index_TweenTypes_Shakes, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_Shakes = (XTweenTypes_Shakes)System.Enum.Parse(typeof(XTweenTypes_Shakes), sp_index_TweenTypes_Shakes.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_Shakes.enumValueIndex = (int)(XTweenTypes_Shakes)System.Enum.Parse(typeof(XTweenTypes_Shakes), res);
                    GetComponents();
                });
            }
            #endregion

            #region XHudManager属性
            if (TweenTypes == XTweenTypes.管理器_XHudManager)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_XHudManager));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("XHudManager属性", actionlist, ref sp_index_TweenTypes_XHudManager, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_XHudManager = (XTweenTypes_XHudManager)System.Enum.Parse(typeof(XTweenTypes_XHudManager), sp_index_TweenTypes_XHudManager.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_XHudManager.enumValueIndex = (int)(XTweenTypes_XHudManager)System.Enum.Parse(typeof(XTweenTypes_XHudManager), res);
                    GetComponents();
                });
            }
            #endregion

            #region XHudAnimator属性
            if (TweenTypes == XTweenTypes.动画器_XHudAnimator)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_XHudAnimator));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("XHudAnimator属性", actionlist, ref sp_index_TweenTypes_XHudAnimator, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_XHudAnimator = (XTweenTypes_XHudAnimator)System.Enum.Parse(typeof(XTweenTypes_XHudAnimator), sp_index_TweenTypes_XHudAnimator.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_XHudAnimator.enumValueIndex = (int)(XTweenTypes_XHudAnimator)System.Enum.Parse(typeof(XTweenTypes_XHudAnimator), res);
                    GetComponents();
                });
            }
            #endregion

            #region XHudElement属性
            if (TweenTypes == XTweenTypes.元素_XHudElement)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_XHudElement));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("XHudElement属性", actionlist, ref sp_index_TweenTypes_XHudElement, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_XHudElement = (XTweenTypes_XHudElement)System.Enum.Parse(typeof(XTweenTypes_XHudElement), sp_index_TweenTypes_XHudElement.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_XHudElement.enumValueIndex = (int)(XTweenTypes_XHudElement)System.Enum.Parse(typeof(XTweenTypes_XHudElement), res);
                    GetComponents();
                });
            }
            #endregion

            #region XHudText属性
            if (TweenTypes == XTweenTypes.文字_XHudText)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_XHudText));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("XHudText属性", actionlist, ref sp_index_TweenTypes_XHudText, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_XHudText = (XTweenTypes_XHudText)System.Enum.Parse(typeof(XTweenTypes_XHudText), sp_index_TweenTypes_XHudText.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_XHudText.enumValueIndex = (int)(XTweenTypes_XHudText)System.Enum.Parse(typeof(XTweenTypes_XHudText), res);
                    GetComponents();
                });
            }
            #endregion

            #region XHudTmpText属性
            if (TweenTypes == XTweenTypes.文字_XHudTmpText)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_XHudTmpText));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("XHudTmpText属性", actionlist, ref sp_index_TweenTypes_XHudTmpText, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_XHudTmpText = (XTweenTypes_XHudTmpText)System.Enum.Parse(typeof(XTweenTypes_XHudTmpText), sp_index_TweenTypes_XHudTmpText.stringValue);
                    }
                    GetComponents();
                }, (res) =>
                {
                    sp_TweenTypes_XHudTmpText.enumValueIndex = (int)(XTweenTypes_XHudTmpText)System.Enum.Parse(typeof(XTweenTypes_XHudTmpText), res);
                    GetComponents();
                });
            }
            #endregion

            #region To类型
            if (TweenTypes == XTweenTypes.原生动画_To)
            {
                actionlist = System.Enum.GetNames(typeof(XTweenTypes_To));
                util_XHUDGUI.Gui_Layout_Popup<string, XTween_Controller>("原生方式", actionlist, ref sp_index_TweenTypes_To, HudFilled.实体, 120, 22, SelectedObjects, (comps) =>
                {
                    for (int i = 0; i < SelectedObjects.Length; i++)
                    {
                        SelectedObjects[i].TweenTypes_To = (XTweenTypes_To)System.Enum.Parse(typeof(XTweenTypes_To), sp_index_TweenTypes_To.stringValue);
                    }
                }, (res) =>
                {
                    sp_TweenTypes_To.enumValueIndex = (int)(XTweenTypes_To)System.Enum.Parse(typeof(XTweenTypes_To), res);
                });
            }
            #endregion

            TweenTypes_Positions = (XTweenTypes_Positions)sp_TweenTypes_Positions.enumValueIndex;
            TweenTypes_Rotations = (XTweenTypes_Rotations)sp_TweenTypes_Rotations.enumValueIndex;
            TweenTypes_Alphas = (XTweenTypes_Alphas)sp_TweenTypes_Alphas.enumValueIndex;
            TweenTypes_Shakes = (XTweenTypes_Shakes)sp_TweenTypes_Shakes.enumValueIndex;
            TweenTypes_XHudManager = (XTweenTypes_XHudManager)sp_TweenTypes_XHudManager.enumValueIndex;
            TweenTypes_XHudAnimator = (XTweenTypes_XHudAnimator)sp_TweenTypes_XHudAnimator.enumValueIndex;
            TweenTypes_XHudText = (XTweenTypes_XHudText)sp_TweenTypes_XHudText.enumValueIndex;
            TweenTypes_XHudTmpText = (XTweenTypes_XHudTmpText)sp_TweenTypes_XHudTmpText.enumValueIndex;
            TweenTypes_XHudElement = (XTweenTypes_XHudElement)sp_TweenTypes_XHudElement.enumValueIndex;
            TweenTypes_To = (XTweenTypes_To)sp_TweenTypes_To.enumValueIndex;

            util_XHUDGUI.Gui_Layout_Space(10);
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion

            #region 目标值 & 起始值
            if (TweenTypes != XTweenTypes.无_None)
            {
                util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "动画值", XTween_Dashboard.Theme_Primary);
                util_XHUDGUI.Gui_Layout_Space(5);
                #region String
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.字符串_String)
                {
                    DrawTweenValueFields<int>("原生字符串 ( String )", sp_EndValue_String, sp_FromValue_String, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.文字_XHudText && TweenTypes_XHudText == XTweenTypes_XHudText.文字内容_Content)
                {
                    DrawTweenValueFields<int>("HudText文字内容 ( String )", sp_EndValue_String, sp_FromValue_String, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.文字_XHudTmpText && TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字内容_Content)
                {
                    DrawTweenValueFields<int>("HudTmpText文字内容 ( String )", sp_EndValue_String, sp_FromValue_String, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Int
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.整数_Int)
                {
                    DrawTweenValueFields<int>("原生整数 ( Int )", sp_EndValue_Int, sp_FromValue_Int, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.文字_XHudText && TweenTypes_XHudText == XTweenTypes_XHudText.文字尺寸_FontSize)
                {
                    DrawTweenValueFields<int>("HudText文字尺寸 ( Int )", sp_EndValue_Int, sp_FromValue_Int, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Float         
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.浮点数_Float)
                {
                    DrawTweenValueFields<float>("原生浮点数 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.透明度_Alpha && TweenTypes_Alphas == XTweenTypes_Alphas.Image组件)
                {
                    DrawTweenValueFields<float>("Image组件的透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.透明度_Alpha && TweenTypes_Alphas == XTweenTypes_Alphas.CanvasGroup组件)
                {
                    DrawTweenValueFields<float>("CanvasGroup组件的透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.填充_Fill)
                {
                    DrawTweenValueFields<float>("Image组件的填充度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.平铺_Tiled)
                {
                    DrawTweenValueFields<float>("Image组件的平铺度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager)
                {
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.屏幕空间元素透明度_ScreenElementsOpacity)
                    {
                        DrawTweenValueFields<float>("屏幕空间元素透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.世界空间元素透明度_WorldElementsOpacity)
                    {
                        DrawTweenValueFields<float>("世界空间元素透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.遮罩层透明度_MaskOpacity)
                    {
                        DrawTweenValueFields<float>("遮罩层透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.焦散遮罩透明度_BlurMaskOpacity)
                    {
                        DrawTweenValueFields<float>("焦散遮罩透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.焦散遮罩强度_BlurMaskStrength)
                    {
                        DrawTweenValueFields<float>("焦散遮罩强度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.全局字体大小缩放_GlobalFontSize)
                    {
                        DrawTweenValueFields<float>("全局文字大小缩放 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.全局音量控制_SoundVolume)
                    {
                        DrawTweenValueFields<float>("全局音量控制 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudManager == XTweenTypes_XHudManager.动画持续时间比例缩放_TweenDurationMultiply)
                    {
                        DrawTweenValueFields<float>("动画持续时间比例缩放 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                }
                if (TweenTypes == XTweenTypes.文字_XHudText)
                {
                    if (TweenTypes_XHudText == XTweenTypes_XHudText.文字行高_LineHeight)
                    {
                        DrawTweenValueFields<float>("HudText文字行高 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                }
                if (TweenTypes == XTweenTypes.文字_XHudTmpText)
                {
                    if (TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字尺寸_FontSize)
                    {
                        DrawTweenValueFields<float>("HudTmpText文字尺寸 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字行高_LineHeight)
                    {
                        DrawTweenValueFields<float>("HudTmpText文字行高 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                    if (TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字间距_Character)
                    {
                        DrawTweenValueFields<float>("HudTmpText文字间距 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                    }
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager && TweenTypes_XHudManager == XTweenTypes_XHudManager.布局水平缩放_MarginHorizontal)
                {
                    DrawTweenValueFields<float>("布局水平缩放 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager && TweenTypes_XHudManager == XTweenTypes_XHudManager.布局垂直缩放_MarginVertical)
                {
                    DrawTweenValueFields<float>("布局垂直缩放 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager && TweenTypes_XHudManager == XTweenTypes_XHudManager.布局整体缩放_MarginMultiply)
                {
                    DrawTweenValueFields<float>("布局整体缩放 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.元素_XHudElement && TweenTypes_XHudElement == XTweenTypes_XHudElement.透明度_Opacity)
                {
                    DrawTweenValueFields<float>("元素透明度 ( Float )", sp_EndValue_Float, sp_FromValue_Float, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Vector2
                if (TweenTypes == XTweenTypes.尺寸_Size)
                {
                    DrawTweenValueFields<Vector2>("RectTransform组件的尺寸 ( Vector2 )", sp_EndValue_Vector2, sp_FromValue_Vector2, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.震动_Shake && TweenTypes_Shakes == XTweenTypes_Shakes.尺寸_Size)
                {
                    DrawTweenValueFields<Vector2>("RectTransform组件的尺寸震动 ( Vector2 )", sp_EndValue_Vector2, sp_FromValue_Vector2, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.二维向量_Vector2)
                {
                    DrawTweenValueFields<Vector2>("原生二维向量 ( Vector2 )", sp_EndValue_Vector2, sp_FromValue_Vector2, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.位置_Position && TweenTypes_Positions == XTweenTypes_Positions.锚点位置_AnchoredPosition)
                {
                    DrawTweenValueFields<Vector2>("RectTransform组件的锚点位置 ( Vector2 )", sp_EndValue_Vector2, sp_FromValue_Vector2, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Vector3
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.三维向量_Vector3)
                {
                    DrawTweenValueFields<Vector3>("原生三维向量 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.旋转_Rotation && TweenTypes_Rotations == XTweenTypes_Rotations.欧拉角度_Euler)
                {
                    DrawTweenValueFields<Vector3>("RectTransform组件的欧拉角旋转 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.缩放_Scale)
                {
                    DrawTweenValueFields<Vector3>("RectTransform组件的缩放 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.震动_Shake && TweenTypes_Shakes == XTweenTypes_Shakes.位置_Position)
                {
                    DrawTweenValueFields<Vector3>("RectTransform组件的位置震动 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.震动_Shake && TweenTypes_Shakes == XTweenTypes_Shakes.旋转_Rotation)
                {
                    DrawTweenValueFields<Vector3>("RectTransform组件的旋转震动 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.震动_Shake && TweenTypes_Shakes == XTweenTypes_Shakes.缩放_Scale)
                {
                    DrawTweenValueFields<Vector3>("RectTransform组件的缩放震动 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.位置_Position && TweenTypes_Positions == XTweenTypes_Positions.锚点位置3D_AnchoredPosition3D)
                {
                    DrawTweenValueFields<Vector3>("RectTransform组件的3D锚点位置 ( Vector3 )", sp_EndValue_Vector3, sp_FromValue_Vector3, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Vector4
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.四维向量_Vector4)
                {
                    DrawTweenValueFields<Vector4>("原生四维向量 ( Vector4 )", sp_EndValue_Vector4, sp_FromValue_Vector4, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.文字_XHudTmpText && TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字边距_Margin)
                {
                    DrawTweenValueFields<Vector4>("HudTmpText文字边距 ( Vector4 )", sp_EndValue_Vector4, sp_FromValue_Vector4, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager && TweenTypes_XHudManager == XTweenTypes_XHudManager.布局边距_Margins)
                {
                    DrawTweenValueFields<Vector4>("布局边距 ( Vector4 )", sp_EndValue_Vector4, sp_FromValue_Vector4, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Color          
                if (TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.颜色_Color)
                {
                    DrawTweenValueFields<Color>("原生颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.颜色_Color)
                {
                    DrawTweenValueFields<Color>("Image组件的颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager && TweenTypes_XHudManager == XTweenTypes_XHudManager.遮罩层颜色_MaskColor)
                {
                    DrawTweenValueFields<Color>("XHud管理器遮罩层颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager && TweenTypes_XHudManager == XTweenTypes_XHudManager.焦散遮罩颜色_BlurMaskColor)
                {
                    DrawTweenValueFields<Color>("XHud管理器焦散遮罩颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.动画器_XHudAnimator && TweenTypes_XHudAnimator == XTweenTypes_XHudAnimator.原始色_OriginalColor)
                {
                    DrawTweenValueFields<Color>("XHud动画器原始色颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.文字_XHudText && TweenTypes_XHudText == XTweenTypes_XHudText.文字颜色_Color)
                {
                    DrawTweenValueFields<Color>("HudText文字颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                if (TweenTypes == XTweenTypes.文字_XHudTmpText && TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字颜色_Color)
                {
                    DrawTweenValueFields<Color>("HudTmpText文字颜色 ( Color )", sp_EndValue_Color, sp_FromValue_Color, sp_IsFromMode.boolValue);
                }
                #endregion

                #region Quaternion
                if (TweenTypes == XTweenTypes.旋转_Rotation && TweenTypes_Rotations == XTweenTypes_Rotations.四元数_Quaternion)
                {
                    DrawTweenValueFields<Quaternion>("RectTransform组件的四元数旋转 ( Quaternion )", sp_EndValue_Quaternion, sp_FromValue_Quaternion, sp_IsFromMode.boolValue);
                }
                #endregion
                util_XHUDGUI.Gui_Layout_Space(10);
                util_XHUDGUI.Gui_Layout_Vertical_End();
            }
            #endregion

            #region 起源值 & 组件
            if (TweenTypes != XTweenTypes.无_None)
            {
                util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, TweenTypes == XTweenTypes.原生动画_To ? "起源值" : "组件", XTween_Dashboard.Theme_Primary);
                util_XHUDGUI.Gui_Layout_Space(5);
                #region 起源值
                if (TweenTypes == XTweenTypes.原生动画_To)
                {
                    if (TweenTypes_To == XTweenTypes_To.字符串_String)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("原始值 String", sp_Target_String, 100);
                    }
                    if (TweenTypes_To == XTweenTypes_To.整数_Int)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("原始值 Int", sp_Target_Int, 100);
                    }
                    if (TweenTypes_To == XTweenTypes_To.浮点数_Float)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("原始值 Float", sp_Target_Float, 100);
                    }
                    if (TweenTypes_To == XTweenTypes_To.二维向量_Vector2)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("目标值 Vector2", sp_Target_Vector2, 100);
                    }
                    if (TweenTypes_To == XTweenTypes_To.三维向量_Vector3)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("原始值 Vector3", sp_Target_Vector3, 100);
                    }
                    if (TweenTypes_To == XTweenTypes_To.四维向量_Vector4)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("原始值 Vector4", sp_Target_Vector4, 100);
                    }
                    if (TweenTypes_To == XTweenTypes_To.颜色_Color)
                    {
                        util_XHUDGUI.Gui_Layout_Space(5);
                        util_XHUDGUI.Gui_Layout_Property_Field("原始值 Color", sp_Target_Color, 100);
                    }
                    util_XHUDGUI.Gui_Layout_Space(5);
                    EditorGUILayout.HelpBox("该值为你需要动画化的数值！", MessageType.Info);
                }
                #endregion

                #region 组件
                if (TweenTypes == XTweenTypes.路径_Path)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 PathTool", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_PathTool.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_PathTool, false);

                    if (sp_Target_PathTool.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该PathTool为路径绘制组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.位置_Position || TweenTypes == XTweenTypes.旋转_Rotation || TweenTypes == XTweenTypes.缩放_Scale || TweenTypes == XTweenTypes.尺寸_Size || TweenTypes == XTweenTypes.震动_Shake)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 RectTransform", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_RectTransform.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_RectTransform, false);
                    if (sp_Target_RectTransform.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该RectTransform为你需要动画化的变换组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.颜色_Color || (TweenTypes == XTweenTypes.透明度_Alpha && TweenTypes_Alphas == XTweenTypes_Alphas.Image组件) || TweenTypes == XTweenTypes.填充_Fill || TweenTypes == XTweenTypes.平铺_Tiled)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 Image", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_Image.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_Image, false);

                    if (sp_Target_Image.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该RectTransform为你需要动画化的变换组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.透明度_Alpha && TweenTypes_Alphas == XTweenTypes_Alphas.CanvasGroup组件)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 CanvasGroup", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_CanvasGroup.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_CanvasGroup, false);

                    if (sp_Target_CanvasGroup.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该CanvasGroup为你需要动画化的画布组组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.管理器_XHudManager)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 HudManager", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_HudManager.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_HudManager, false);

                    if (sp_Target_HudManager.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该HudManager为你需要动画化的Hud管理器组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.动画器_XHudAnimator)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 HudAnimator", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_HudAnimator.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_HudAnimator, false);

                    if (sp_Target_HudAnimator.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该HudAnimator为你需要动画化的Hud动画器组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.元素_XHudElement)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 HudElement", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_HudElement.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_HudElement, false);

                    if (sp_Target_HudElement.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该HudElement为你需要动画化的Hud元素组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.文字_XHudText)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 HudText", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_HudText.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_HudText, false);

                    if (sp_Target_HudText.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该HudText为你需要动画化的Hud文字组件！不可为空！", MessageType.Warning);
                }
                if (TweenTypes == XTweenTypes.文字_XHudTmpText)
                {
                    util_XHUDGUI.Gui_Layout_Space(5);
                    util_XHUDGUI.StatuDisplayer_Object(null, 12, new Vector2(0, 1), "目标 HudTmpText", 12, new Vector2(0, -7), icon_status, new Vector2(0, 3), sp_Target_HudTmpText.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_Target_HudTmpText, false);

                    if (sp_Target_HudTmpText.objectReferenceValue == null)
                        EditorGUILayout.HelpBox("该HudTmpText为你需要动画化的HudTmp文字组件！不可为空！", MessageType.Warning);
                }
                #endregion
                util_XHUDGUI.Gui_Layout_Space(5);
                util_XHUDGUI.Gui_Layout_Vertical_End();
            }
            #endregion

            #region 动画参数
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "动画参数", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("动画耗时", sp_Duration, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("延迟", sp_Delay, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("随机延迟", sp_UseRandomDelay, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("随机延迟范围", sp_RandomDelay, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            EditorGUI.BeginChangeCheck();
            util_XHUDGUI.Gui_Layout_Property_Field("缓动模式", sp_EaseMode, 100);
            if (EditorGUI.EndChangeCheck())
            {
                EasePic = GetEasePic((EaseMode)sp_EaseMode.enumValueIndex);
            }
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("使用曲线", sp_UseCurve, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("运动曲线", sp_Curve, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("循环次数", sp_LoopCount, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("循环延迟", sp_LoopDelay, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("循环方式", sp_LoopType, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("指定起始值", sp_IsFromMode, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("相对动画", sp_IsRelative, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("自动杀死", sp_IsAutoKill, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            if ((TweenTypes == XTweenTypes.原生动画_To && TweenTypes_To == XTweenTypes_To.字符串_String) ||
                (TweenTypes == XTweenTypes.文字_XHudText && TweenTypes_XHudText == XTweenTypes_XHudText.文字内容_Content) ||
                (TweenTypes == XTweenTypes.文字_XHudTmpText && TweenTypes_XHudTmpText == XTweenTypes_XHudTmpText.文字内容_Content))
                util_XHUDGUI.Gui_Layout_Property_Field("扩展字符串", sp_IsExtendedString, 100);
            if (TweenTypes == XTweenTypes.旋转_Rotation && TweenTypes_Rotations == XTweenTypes_Rotations.四元数_Quaternion)
                util_XHUDGUI.Gui_Layout_Property_Field("四元数过渡方式", sp_HudRotateMode, 100);
            if (TweenTypes == XTweenTypes.震动_Shake)
            {
                util_XHUDGUI.Gui_Layout_Property_Field("震动频率", sp_Vibrato, 100);
                util_XHUDGUI.Gui_Layout_Property_Field("震动随机度", sp_Randomness, 100);
                util_XHUDGUI.Gui_Layout_Property_Field("震动渐变", sp_FadeShake, 100);
            }
            util_XHUDGUI.Gui_Layout_Space(10);
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion

            #region 选项
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "选项", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(5);
            #region 调试信息
            util_XHUDGUI.Gui_Layout_Toggle<bool, XTween_Controller>("调试信息", new string[2] { "禁用", "启用" }, ref sp_DebugMode, HudFilled.无, HudFilled.实体, Color.white, 120, 22, SelectedObjects);
            #endregion

            util_XHUDGUI.Gui_Layout_Seperator(1, XTween_Dashboard.Theme_SeperateLine);

            #region 动画状态闪烁
            EditorGUI.BeginChangeCheck();
            util_XHUDGUI.Gui_Layout_Toggle<bool, XTween_Controller>("动画状态闪烁（全局）", new string[2] { "禁用", "启用" }, ref sp_LiquidLEDBlink, HudFilled.无, HudFilled.实体, Color.white, 120, 22, SelectedObjects);
            if (EditorGUI.EndChangeCheck())
            {
                XTween_Dashboard.Tween_LiquidLEDBlink = sp_LiquidLEDBlink.boolValue;
                util_Tools.PlayerPrefs_SaveValue_ForEditor("TweenController_LiquidLEDBlink", sp_LiquidLEDBlink.boolValue);
            }
            #endregion

            util_XHUDGUI.Gui_Layout_Seperator(1, XTween_Dashboard.Theme_SeperateLine);

            #region 启用按键控制
            util_XHUDGUI.Gui_Layout_Toggle<bool, XTween_Controller>("启用按键控制", new string[2] { "禁用", "启用" }, ref sp_keyControl_Enabled, HudFilled.无, HudFilled.实体, Color.white, 120, 22, SelectedObjects);
            #endregion

            util_XHUDGUI.Gui_Layout_Seperator(1, XTween_Dashboard.Theme_SeperateLine);

            #region 自动停止预览
            EditorGUI.BeginChangeCheck();
            util_XHUDGUI.Gui_Layout_Toggle<bool, XTween_Controller>("自动停止预览", new string[2] { "禁用", "启用" }, ref sp_index_AutoKillPreviewTweens, HudFilled.无, HudFilled.实体, Color.white, 120, 22, SelectedObjects);
            if (EditorGUI.EndChangeCheck())
            {
                XTween_Previewer.AutoKillWithDuration = sp_index_AutoKillPreviewTweens.boolValue;
                util_Tools.PlayerPrefs_SaveValue_ForEditor("PreviewTween_AutoKillWithDuration", sp_index_AutoKillPreviewTweens.boolValue);

                // 如果为自动杀死动画则会强制开启：
                // 杀死前重置动画
                // 杀死后清空预览列表
                if (sp_index_AutoKillPreviewTweens.boolValue)
                {
                    sp_index_RewindPreviewTweensWithKill.boolValue = true;
                    sp_index_RewindPreviewTweensWithKill.serializedObject.ApplyModifiedProperties();
                    sp_index_ClearPreviewTweensWithKill.boolValue = true;
                    sp_index_ClearPreviewTweensWithKill.serializedObject.ApplyModifiedProperties();

                    XTween_Previewer.BeforeKillRewind = sp_index_RewindPreviewTweensWithKill.boolValue;
                    util_Tools.PlayerPrefs_SaveValue_ForEditor("PreviewTween_RewindPreviewTweensWithKill", sp_index_RewindPreviewTweensWithKill.boolValue);
                    XTween_Previewer.AfterKillClear = sp_index_ClearPreviewTweensWithKill.boolValue;
                    util_Tools.PlayerPrefs_SaveValue_ForEditor("PreviewTween_ClearPreviewTweensWithKill", sp_index_ClearPreviewTweensWithKill.boolValue);
                }
            }
            #endregion

            #region 杀死前先重置
            if (!sp_index_AutoKillPreviewTweens.boolValue)
            {
                EditorGUI.BeginChangeCheck();
                util_XHUDGUI.Gui_Layout_Toggle<bool, XTween_Controller>("杀死前先重置", new string[2] { "禁用", "启用" }, ref sp_index_RewindPreviewTweensWithKill, HudFilled.无, HudFilled.实体, Color.white, 120, 22, SelectedObjects);
                if (EditorGUI.EndChangeCheck())
                {
                    XTween_Previewer.BeforeKillRewind = sp_index_RewindPreviewTweensWithKill.boolValue;
                    util_Tools.PlayerPrefs_SaveValue_ForEditor("PreviewTween_RewindPreviewTweensWithKill", sp_index_RewindPreviewTweensWithKill.boolValue);
                }
            }
            #endregion

            #region 杀死后清空预览器列表
            if (!sp_index_AutoKillPreviewTweens.boolValue)
            {
                EditorGUI.BeginChangeCheck();
                util_XHUDGUI.Gui_Layout_Toggle<bool, XTween_Controller>("杀死后清空预览器列表", new string[2] { "禁用", "启用" }, ref sp_index_ClearPreviewTweensWithKill, HudFilled.无, HudFilled.实体, Color.white, 120, 22, SelectedObjects);
                if (EditorGUI.EndChangeCheck())
                {
                    XTween_Previewer.AfterKillClear = sp_index_ClearPreviewTweensWithKill.boolValue;
                    util_Tools.PlayerPrefs_SaveValue_ForEditor("PreviewTween_ClearPreviewTweensWithKill", sp_index_ClearPreviewTweensWithKill.boolValue);
                }
            }
            #endregion
            util_XHUDGUI.Gui_Layout_Space(10);
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion

            #region 控制按键
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 5, "控制按键", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("动画播放", sp_keyControl_Tween_Create, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("动画播放", sp_keyControl_Tween_Play, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("动画倒退", sp_keyControl_Tween_Rewind, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("动画杀死", sp_keyControl_Tween_Kill, 100);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Property_Field("动画重播", sp_keyControl_Tween_Replay, 100);
            util_XHUDGUI.Gui_Layout_Space(10);
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion

            #region 快捷菜单
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 1)
            {
                // 创建右键菜单
                GenericMenu menu = new GenericMenu();
                menu.AddDisabledItem(new GUIContent("动画预览"));
                menu.AddItem(new GUIContent("A (预览)"), false, () =>
                {
                    if (!ValidPreviewed())
                    {
                        if (sp_DebugMode.boolValue)
                            util_Tools.Func_PrintInfo("XTween动画管理器消息", "因缺失组件，导致无法预览动画！请检查组件项中是否未指定组件！", HudMsgState.警告);
                        return;
                    }
                    Preview_Start();
                    return;
                });
                menu.AddItem(new GUIContent("R (倒退)"), false, () =>
                {
                    Preview_Rewind();
                    return;
                });
                menu.AddItem(new GUIContent("S (杀死)"), false, () =>
                {
                    Preview_Kill();
                    return;
                });

                menu.ShowAsContext(); // 在鼠标位置显示右键菜单
                e.Use();
            }
            #endregion

            #region 源脚本
            util_XHUDGUI.Gui_Layout_Vertical_Start(HudFilled.纯色边框, HudColor.亮白, 3, "源脚本", XTween_Dashboard.Theme_Primary);
            util_XHUDGUI.Gui_Layout_Space(5);

            #region 原始变量
            util_XHUDGUI.Gui_Layout_Horizontal_Start(HudFilled.无, HudColor.无, 0);
            util_XHUDGUI.Gui_Layout_Space(10);
            BasicVars = EditorGUILayout.Foldout(BasicVars, "变量/属性", true);
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Horizontal_End();
            if (BasicVars)
                DrawDefaultInspector();
            #endregion

            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_Vertical_End();
            #endregion
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 编辑器更新（用于动画状态LED闪烁更新）
        /// </summary>
        private void OnEditorUpdate()
        {
            // 限制刷新频率，每0.05秒检查一次是否需要重绘
            if (EditorApplication.timeSinceStartup - lastUpdateTime > 0.05)
            {
                lastUpdateTime = EditorApplication.timeSinceStartup;
            }
        }
        /// <summary>
        /// 杀死预览动画后的操作逻辑
        /// </summary>
        private void OnAutoKillPreview()
        {
            IsPreviewed = false;
            //BaseScript.CurrentTweener.Rewind();
            BaseScript.CurrentTweener = null;
            XTween_Previewer.act_on_editor_autokill -= OnAutoKillPreview;
        }

        #region 动画方法
        /// <summary>
        ///  动画预览 - 倒退
        /// </summary>
        private void Preview_Rewind()
        {
            IsPreviewed = false;
            XTween_Previewer.Rewind();
        }
        /// <summary>
        ///  动画预览 - 杀死
        /// </summary>
        private void Preview_Kill()
        {
            if (Application.isPlaying)
                return;
            if (sp_TweenTypes.enumValueIndex == 0)
                return;
            IsPreviewed = false;
            XTween_Previewer.Kill(XTween_Previewer.AfterKillClear, XTween_Previewer.BeforeKillRewind, () => { BaseScript.CurrentTweener = null; });

            // 当动画预览器为根据动画耗时自动杀死的情况下
            if (sp_index_AutoKillPreviewTweens.boolValue)
                XTween_Previewer.act_on_editor_autokill -= OnAutoKillPreview;
        }
        /// <summary>
        ///  动画预览 - 开始
        /// </summary>
        private void Preview_Start()
        {
            if (Application.isPlaying)
                return;
            if (sp_TweenTypes.enumValueIndex == 0)
                return;

            IsPreviewed = true;
            if (BaseScript.CurrentTweener == null)
                BaseScript.Tween_Create();

            // 当动画预览器为根据动画耗时自动杀死的情况下
            if (sp_index_AutoKillPreviewTweens.boolValue)
            {
                XTween_Previewer.AfterKillClear = true;
                XTween_Previewer.BeforeKillRewind = true;
                XTween_Previewer.act_on_editor_autokill += OnAutoKillPreview;
            }

            #region 添加至预览器并播放
            XTween_Previewer.Append(BaseScript.CurrentTweener);
            XTween_Previewer.Play();

            #endregion
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 通用方法：绘制动画目标值和起始值字段
        /// </summary>
        private void DrawTweenValueFields<T>(string label, SerializedProperty endValueProp, SerializedProperty fromValueProp, bool isFromMode)
        {
            util_XHUDGUI.Gui_Layout_Space(5);
            util_XHUDGUI.Gui_Layout_LabelfieldThin(label, HudFilled.无, HudColor.无, Color.white, TextAnchor.MiddleLeft, new Vector2(5, 0), 14, Font_Bold);
            util_XHUDGUI.Gui_Layout_Space(10);
            if (isFromMode)
            {
                util_XHUDGUI.Gui_Layout_Property_Field("起始", fromValueProp, 100);
                util_XHUDGUI.Gui_Layout_Space(5);
            }

            util_XHUDGUI.Gui_Layout_Property_Field("目标", endValueProp, 100);
            util_XHUDGUI.Gui_Layout_Space(10);
        }
        /// <summary>
        /// 获取组件
        /// </summary>
        public void GetComponents()
        {
            // 获取所有组件
            XTween_PathTool m_PathTool = BaseScript.GetComponent<XTween_PathTool>();
            if (m_PathTool)
            {
                if (sp_Target_PathTool.objectReferenceValue == null)
                    sp_Target_PathTool.objectReferenceValue = m_PathTool;
            }
            RectTransform m_RectTransform = BaseScript.GetComponent<RectTransform>();
            if (m_RectTransform)
            {
                if (sp_Target_RectTransform.objectReferenceValue == null)
                    sp_Target_RectTransform.objectReferenceValue = m_RectTransform;
            }
            Image m_Image = BaseScript.GetComponent<Image>();
            if (m_Image)
            {
                if (sp_Target_Image.objectReferenceValue == null)
                    sp_Target_Image.objectReferenceValue = m_Image;
            }
            CanvasGroup m_CanvasGroup = BaseScript.GetComponent<CanvasGroup>();
            if (m_CanvasGroup)
            {
                if (sp_Target_CanvasGroup.objectReferenceValue == null)
                    sp_Target_CanvasGroup.objectReferenceValue = m_CanvasGroup;
            }
            Hud_Animator m_Hud_Animator = BaseScript.GetComponent<Hud_Animator>();
            if (m_Hud_Animator)
            {
                if (sp_Target_HudAnimator.objectReferenceValue == null)
                    sp_Target_HudAnimator.objectReferenceValue = m_Hud_Animator;
            }
            Hud_Element m_Hud_Element = BaseScript.GetComponent<Hud_Element>();
            if (m_Hud_Element)
            {
                if (sp_Target_HudElement.objectReferenceValue == null)
                    sp_Target_HudElement.objectReferenceValue = m_Hud_Element;
            }
            Hud_Text m_Hud_Text = BaseScript.GetComponent<Hud_Text>();
            if (m_Hud_Text)
            {
                if (sp_Target_HudText.objectReferenceValue == null)
                    sp_Target_HudText.objectReferenceValue = m_Hud_Text;
            }
            Hud_TmpText m_Hud_TmpText = BaseScript.GetComponent<Hud_TmpText>();
            if (m_Hud_TmpText)
            {
                if (sp_Target_HudTmpText.objectReferenceValue == null)
                    sp_Target_HudTmpText.objectReferenceValue = m_Hud_TmpText;
            }
            Hud_Manager m_Hud_Manager = XTween_Dashboard.HudManagerGet();
            if (m_Hud_Manager)
            {
                if (sp_Target_HudManager.objectReferenceValue == null)
                    sp_Target_HudManager.objectReferenceValue = m_Hud_Manager;
            }

            // 应用保存所有组件
            sp_Target_PathTool.serializedObject.ApplyModifiedProperties();
            sp_Target_RectTransform.serializedObject.ApplyModifiedProperties();
            sp_Target_Image.serializedObject.ApplyModifiedProperties();
            sp_Target_CanvasGroup.serializedObject.ApplyModifiedProperties();
            sp_Target_HudAnimator.serializedObject.ApplyModifiedProperties();
            sp_Target_HudElement.serializedObject.ApplyModifiedProperties();
            sp_Target_HudText.serializedObject.ApplyModifiedProperties();
            sp_Target_HudTmpText.serializedObject.ApplyModifiedProperties();
            sp_Target_HudManager.serializedObject.ApplyModifiedProperties();
        }
        /// <summary>
        /// 获取缓动参数曲线图
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        private Texture2D GetEasePic(EaseMode ease)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>($"{XTween_Dashboard.Get_GUIStyle_Path()}Icon/EaseCurveGraph/{ease.ToString()}.png");
        }
        /// <summary>
        /// 获取缓动参数曲线图背景
        /// </summary>        
        /// <returns></returns>
        private Texture2D GetEasePicBg()
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>($"{XTween_Dashboard.Get_GUIStyle_Path()}Icon/EaseCurveGraph/bg.png");
        }
        /// <summary>
        /// 检查被动画的组件是否正确指定并有效
        /// </summary>
        /// <returns></returns>
        private bool ValidPreviewed()
        {
            bool valid = true;

            string hexcol = util_Tools.Color_To_HexColor(XTween_Dashboard.Theme_Primary, true);

            if (TweenTypes == XTweenTypes.文字_XHudTmpText)
            {
                if (sp_Target_HudTmpText.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "文字组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  HudTmpText </color>\"组件! ，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.文字_XHudText)
            {
                if (sp_Target_HudText.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "文字组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  HudText </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.元素_XHudElement)
            {
                if (sp_Target_HudElement.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "元素组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  HudElement </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.管理器_XHudManager)
            {
                if (sp_Target_HudManager.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "管理器组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  HudManager </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.动画器_XHudAnimator)
            {
                if (sp_Target_HudAnimator.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "动画器组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  HudAnimator </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.位置_Position)
            {
                if (sp_Target_RectTransform.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "变换组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  RectTransform </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.旋转_Rotation)
            {
                {
                    if (sp_Target_RectTransform.objectReferenceValue == null)
                        valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "变换组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  RectTransform </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.缩放_Scale)
            {
                {
                    if (sp_Target_RectTransform.objectReferenceValue == null)
                        valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "变换组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  RectTransform </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.尺寸_Size)
            {
                {
                    if (sp_Target_RectTransform.objectReferenceValue == null)
                        valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "变换组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  RectTransform </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.颜色_Color)
            {
                {
                    if (sp_Target_Image.objectReferenceValue == null)
                        valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "图像组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  Image </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.透明度_Alpha)
            {
                if (sp_Target_Image.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "图像组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  Image </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
                if (sp_Target_CanvasGroup.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "画布编组组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  CanvasGroup </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.填充_Fill)
            {
                {
                    if (sp_Target_Image.objectReferenceValue == null)
                        valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "图像组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  Image </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.平铺_Tiled)
            {
                if (sp_Target_Image.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "图像组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  Image </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.震动_Shake)
            {
                if (sp_Target_RectTransform.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "变换组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  RectTransform </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
            }
            if (TweenTypes == XTweenTypes.路径_Path)
            {
                if (sp_Target_PathTool.objectReferenceValue == null)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "路径工具组件异常", $"检测到您未正确指定对应动画所需要的\"<color={hexcol}>  PathTool </color>\"组件!，请正确指定后再预览！", "明白", 0, false);
                }
                if (BaseScript.Target_PathTool.GetPathPointCount() <= 0)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "路径工具组件异常", $"检测到您正使用路径动画，但是路径却没有任何\"<color={hexcol}> 路径点 </color>\"，请正确创建路径点后再预览！", "明白", 0, false);
                }
                if (BaseScript.Target_PathTool.GetPathPointCount() < 2)
                {
                    valid = false;
                    util_XHUDGUI.Open(XHudDialogType.警告, "HudAnimator动画器消息", "路径工具组件异常", $"检测到您正使用路径动画，但是路径却只有\"<color={hexcol}> 1个路径点 </color>\"，请确保最少创建\"<color={hexcol}> 2个路径点 </color>\"后再预览！", "明白", 0, false);
                }
            }

            return valid;
        }
        #endregion
    }
}