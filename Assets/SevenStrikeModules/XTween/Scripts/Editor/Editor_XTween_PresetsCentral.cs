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
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEditor.Presets;
    using UnityEngine;
    using UnityEngine.UIElements;

    [Serializable]
    public class PresetItemGUIStruct
    {
        public XTweenPresetBase preset;
        public XTweenTypes type;
        public bool isPressing;
    }

    public class Editor_XTween_PresetsCentral : EditorWindow
    {
        private static Editor_XTween_PresetsCentral window;
        public TweenConfigData TweenConfigData;
        /// <summary>
        /// 字体 - 粗体
        /// </summary>
        private Font Font_Bold;
        /// <summary>
        /// 字体 - 细体
        /// </summary>
        private Font Font_Light;
        /// <summary>
        /// 图标
        /// </summary>
        private Texture2D
            logo,
            referbg,
            sep,
            selectionMark,
            btn_edit,
            btn_edit_press,
            btn_favourite,
            btn_favourite_press,
            btn_apply,
            btn_apply_press,
            icon_search,
            liquid,
            liquid_ease_bg;
        /// <summary>
        /// 分类图标
        /// </summary>
        private Texture2D[] icons;

        #region 抬头参数
        Rect Title_rect;
        Rect Icon_rect;
        Rect Sepline_rect;
        Color SepLineColor = new Color(1, 1, 1, 0.15f);
        #endregion

        #region left
        float left_width = 60;
        float left_height_margin_bottom = 20;
        Rect left_rect;
        #endregion

        #region typebtns
        string[] tweentypes_names = new string[13]
        {
            "To",
            "Path",
            "Position",
            "Rotation",
            "Scale",
            "Fill",
            "Size",
            "Tiled",
            "Text",
            "TmpText",
            "Alpha",
            "Shake",
            "Color"
        };
        float tweentypes_size = 48;
        float tweentypes_dis = 30;
        string tweentypes_lastSelectionName = "";
        #endregion

        #region selectionmark
        Rect selectionmark_lastSelectionRect;
        float selectionmark_offset = 2;
        #endregion

        #region middle
        float middle_strartpos = 70;
        float middle_width_margin_right = 290;
        float middle_height_margin_bottom = 20;
        Rect middle_rect;
        float scroll_item_height = 60;
        float scroll_item_distance = 3;
        float scroll_btn_size = 20;
        private Vector2 scrollPosition;
        private Rect scrollViewRect;
        private Rect viewRect;
        private Rect favRect;
        private List<PresetItemGUIStruct> loadedpresets = new List<PresetItemGUIStruct>();
        private XTweenTypes lastXtweenType;
        private PresetItemGUIStruct SelectedPresetItem;
        private Color ItemColor;
        private Color ItemPressColor;
        bool isFavouriteMode;
        public string PresetSearchString
        {
            get
            {
                return m_PresetSearchString;
            }

            set
            {
                if (value != m_PresetSearchString)
                {
                    InSearchmode = true;
                    m_PresetSearchString = value;
                    SearchPreset(m_PresetSearchString);
                }
            }
        }
        private string m_PresetSearchString;
        public bool InSearchmode;
        private bool SearchFieldFocused = false;
        #endregion

        #region right
        float right_width = 272;
        float right_height_margin_bottom = 20;
        Rect right_rect;
        #endregion

        [MenuItem("Assets/XTween/X 预设中心（Presets Central)")]
        public static void ShowWindow()
        {
            window = (Editor_XTween_PresetsCentral)EditorWindow.GetWindow(typeof(Editor_XTween_PresetsCentral), true, "XTween 预设中心", true);
            Editor_XTween_GUI.CenterEditorWindow(new Vector2Int(956, 785), window, false);
            window.maxSize = window.minSize;
            window.Show();
        }

        private void OnEnable()
        {
            #region 检查预设文件
            XTween_PresetManager.preset_JsonFile_Checker();
            #endregion

            #region 获取配置文件
            string json = AssetDatabase.LoadAssetAtPath<TextAsset>(XTween_Dashboard.Get_path_XTween_Config_Path() + $"XTweenConfigData.json").text;
            TweenConfigData = JsonUtility.FromJson<TweenConfigData>(json);
            #endregion

            #region 图标获取
            referbg = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/referbg");
            sep = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/sep");
            logo = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/logo");
            selectionMark = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/selection");

            btn_edit = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_edit");
            btn_edit_press = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_edit_press");
            btn_favourite = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_favo");
            btn_favourite_press = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_favo_press");
            btn_apply = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_apply");
            btn_apply_press = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_apply_press");
            icon_search = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/icon_search");
            liquid = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/liquid");
            liquid_ease_bg = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/liquid_ease_bg");

            #region 分类图标获取
            string[] icon_paths = AssetDatabase.FindAssets("t:Texture2D", new string[1] { $"{XTween_Dashboard.Get_path_XTween_GUIStyle_Path()}Icon/Icons_XTween_PresetsCentral/icons" });

            icons = new Texture2D[icon_paths.Length];
            for (int i = 0; i < icons.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(icon_paths[i]);
                icons[i] = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
            }
            #endregion
            #endregion

            #region 字体
            Font_Bold = Editor_XTween_GUI.GetFont("SS_Editor_Bold");
            Font_Light = Editor_XTween_GUI.GetFont("SS_Editor_Dialog");
            #endregion

            #region 预设分类指示器初始化
            tweentypes_lastSelectionName = TweenConfigData.PresetSelectionMark_LastTypeName;
            selectionmark_lastSelectionRect = TweenConfigData.PresetSelectionMark_LastRect;

            // 分类指示器光标坐标设置
            SelectionTypeMark_OriginalRectSet(selectionmark_lastSelectionRect);
            #endregion

            #region item 颜色指定
            ItemColor = XTween_Utilitys.ConvertHexStringToColor("1a1a1a");
            ItemPressColor = XTween_Utilitys.ConvertHexStringToColor("3a3a3a");
            #endregion

            EditorApplication.delayCall += () =>
            {
                if (TweenConfigData.PresetInFavouriteMode)
                {// 再次重新打开预设星标列表
                    OpenFavouritePresets();
                }
                else
                {
                    XTweenTypes xt = (XTweenTypes)XTweenTypeFromString(TweenConfigData.PresetSelectionMark_LastTypeName);
                    XTweenPresetContainer container = LoadPresetsContainer(xt);
                    PresetsAppendToList(container);
                }
            };
        }

        private void Update()
        {
            //Repaint();
        }

        private void OnGUI()
        {
            // referBG
            GUI.backgroundColor = Color.white * 0.4f;
            Editor_XTween_GUI.Gui_Icon(new Rect(0, 0, position.width, position.height), referbg);
            GUI.backgroundColor = Color.white;

            Rect rect = new Rect(0, 0, position.width, position.height);

            #region Titles
            Draw_Titles(rect);
            #endregion

            #region Search
            Draw_PresetSearch(rect);
            #endregion

            #region Favourite
            favRect = new Rect(rect.x + 620, rect.y + 19, btn_favourite.width, btn_favourite.height);
            if (Editor_XTween_GUI.Gui_IconButton(favRect, btn_favourite, btn_favourite_press))
            {
                PresetSearchString = null;
                InSearchmode = false;
                GUI.FocusControl(null);
                OpenFavouritePresets();
            }
            #endregion

            #region  left
            // left_rect 基础坐标
            left_rect = new Rect(rect.x + 5, rect.y + 75, left_width, rect.height - 80 - left_height_margin_bottom);
            // 预设分类按钮列表
            Draw_TweenTypeButtons(left_rect);

            #region 分割线
            Rect leftpanel_rect_sep = new Rect(left_rect.x + left_rect.width + 1, left_rect.y + 18, sep.width, sep.height);
            GUI.backgroundColor = Color.white * 0.6f;
            Editor_XTween_GUI.Gui_Icon(leftpanel_rect_sep, sep);
            GUI.backgroundColor = Color.white;
            #endregion

            #region 分类选择光标
            Editor_XTween_GUI.Gui_Icon(selectionmark_lastSelectionRect, selectionMark);
            #endregion

            #endregion

            #region middle
            // middle_rect 基础坐标
            middle_rect = new Rect(left_rect.x + middle_strartpos, left_rect.y, rect.width - middle_width_margin_right - middle_strartpos, rect.height - 80 - middle_height_margin_bottom);
            //Editor_XTween_GUI.Gui_Box(middle_rect, Color.red * 0.3f);
            // 绘制列表
            Draw_PresetsScrollView(middle_rect);
            // 绘制当前分类的预设统计数量
            Draw_PresetsCountStatistic(middle_rect);
            #endregion

            #region right
            // right_rect 基础坐标
            right_rect = new Rect(rect.width - right_width, rect.y + 18, right_width - 15, rect.height - right_height_margin_bottom - 20);
            Draw_Info(right_rect);
            #endregion
        }

        #region 绘制GUI类
        /// <summary>
        /// 搜索预设
        /// </summary>
        /// <param name="rect"></param>
        private void Draw_PresetSearch(Rect rect)
        {
            GUI.SetNextControlName("TextField1");

            Rect rect_search = new Rect(rect.x + 380, rect.y + 25, 230, 25);
            GUIStyle style = GUI.skin.textField;
            style.padding = new RectOffset(35, 0, 0, 0);
            style.alignment = TextAnchor.MiddleLeft;
            PresetSearchString = Editor_XTween_GUI.Gui_InputField_String(rect_search, PresetSearchString, style);

            Rect rect_search_icon = new Rect(rect_search.x, rect_search.y - 4, icon_search.width, icon_search.height);
            Editor_XTween_GUI.Gui_Icon(rect_search_icon, icon_search);

            // 检测当前哪个控件获得焦点
            string focusedControl = GUI.GetNameOfFocusedControl();

            // 根据焦点状态执行相应逻辑
            if (focusedControl == "TextField1")
            {
                if (!SearchFieldFocused)
                {
                    SearchFieldFocused = true;

                    // 在这里执行获得焦点时的逻辑
                    InSearchmode = true;
                    loadedpresets.Clear();
                    SearchPreset(m_PresetSearchString);
                }
            }
            else
            {
                SearchFieldFocused = false;
            }
        }
        /// <summary>
        /// 绘制标题区域
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private void Draw_Titles(Rect rect)
        {
            Icon_rect = new Rect(15, 15, 48, 48);
            Editor_XTween_GUI.Gui_Icon(Icon_rect, logo);

            Title_rect = new Rect(rect.x + 85, rect.y + 15, rect.width - 80, 30);
            Editor_XTween_GUI.Gui_Labelfield(Title_rect, "XTween 预设中心", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 20, Font_Bold);

            Sepline_rect = new Rect(rect.x + 85, rect.y + 60, 200, 1);
            Editor_XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);
        }
        /// <summary>
        /// 动画分类按钮
        /// </summary>
        /// <param name="rect"></param>
        private void Draw_TweenTypeButtons(Rect rect)
        {
            Rect o = rect;
            float e = 6;
            for (int i = 0; i < tweentypes_names.Length; i++)
            {
                if (i == 0)
                    o.Set(o.x + e, o.y + tweentypes_dis, tweentypes_size, tweentypes_size);
                else
                    o.Set(o.x, o.y + tweentypes_size, tweentypes_size, tweentypes_size);

                #region 点击了类型按钮后逻辑
                if (Editor_XTween_GUI.Gui_IconButton(o, GetTweenTypeBtnIcon(tweentypes_names[i]), GetTweenTypeBtnIcon_Pressed(tweentypes_names[i])))
                {
                    TweenTypeBtnClickedEvent(o, tweentypes_names[i]);
                }
                #endregion
            }
        }
        /// <summary>
        /// 绘制预设列表
        /// </summary>
        /// <param name="rect"></param>
        private void Draw_PresetsScrollView(Rect rect)
        {
            // 定义滚动视图的区域
            scrollViewRect = new Rect(rect.x, rect.y + 25, rect.width, rect.height - 35);

            // 定义内容区域的大小 - 修复：使用正确的项目高度
            float totalHeight = loadedpresets.Count * (scroll_item_height + scroll_item_distance);
            viewRect = new Rect(0, 0, scrollViewRect.width - 20, totalHeight);

            // 开始滚动视图
            scrollPosition = GUI.BeginScrollView(scrollViewRect, scrollPosition, viewRect);
            // 获取当前事件
            Event e = Event.current;

            // 在内容区域内绘制项目
            for (int i = 0; i < loadedpresets.Count; i++)
            {
                PresetItemGUIStruct pret = loadedpresets[i];

                Rect itemRect = new Rect(5, i * (scroll_item_height + scroll_item_distance), viewRect.width - 10, scroll_item_height);
                Rect itemRect_clicked = new Rect(5, i * (scroll_item_height + scroll_item_distance), viewRect.width - 210, scroll_item_height);

                // 检测鼠标点击
                if (itemRect_clicked.Contains(e.mousePosition))
                {
                    if (e.type == EventType.MouseDown && e.button == (int)MouseButton.LeftMouse)
                    {
                        ClearFocus();
                        SelectedPresetItem = pret;
                        pret.isPressing = true;
                        e.Use();
                        Repaint();
                    }
                    if (e.type == EventType.MouseUp && e.button == (int)MouseButton.LeftMouse)
                    {
                        pret.isPressing = false;
                        e.Use();
                        Repaint();
                    }
                    if (e.type == EventType.MouseDrag && e.button == (int)MouseButton.LeftMouse)
                    {
                        pret.isPressing = false;
                        e.Use();
                        Repaint();
                    }
                }

                if (pret.isPressing)
                    Editor_XTween_GUI.Gui_Box_Style(itemRect, XTweenGUIFilled.实体, ItemPressColor);
                else
                    Editor_XTween_GUI.Gui_Box_Style(itemRect, XTweenGUIFilled.实体, ItemColor);

                if (isFavouriteMode || InSearchmode)
                {
                    // 绘制图标
                    Rect rect_icon = new Rect(itemRect.x + 8, itemRect.y + 12, tweentypes_size * 0.7f, tweentypes_size * 0.7f);
                    string nm = pret.type.ToString().Split(new char[1] { '_' })[1];
                    GUI.backgroundColor = Color.white * 0.6f;
                    Editor_XTween_GUI.Gui_Icon(rect_icon, GetTweenTypeBtnIcon(nm));
                    GUI.backgroundColor = Color.white;
                }

                #region 间距判定
                float dis = 26;

                if (InSearchmode)
                {
                    dis = 50;
                }
                else
                {
                    if (isFavouriteMode)
                    {
                        dis = 50;
                    }
                }
                #endregion

                // 绘制标题
                Rect rect_title = new Rect(itemRect.x + dis, itemRect.y + 5, 280, 25);
                Editor_XTween_GUI.Gui_Labelfield(rect_title, pret.preset.Name, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Bold);

                // 绘制解释
                Rect rect_des = new Rect(itemRect.x + dis, itemRect.y + itemRect.height - 25 - 3, 300, 25);
                Editor_XTween_GUI.Gui_Labelfield(rect_des, pret.preset.Description, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.75f, TextAnchor.MiddleLeft, Vector2.zero, 12, true, TextClipping.Ellipsis, false, Font_Light);

                float offset = 20;

                // 修改参数按钮
                Rect rect_btn_edit = new Rect(itemRect.width - 160 - offset, itemRect.y + 12, btn_edit.width, btn_edit.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_edit, btn_edit, btn_edit_press))
                {
                    ClearFocus();
                }
                // 应用参数按钮
                Rect rect_btn_apply = new Rect(itemRect.width - 100 - offset, itemRect.y + 12, btn_apply.width, btn_apply.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_apply, btn_apply, btn_apply_press))
                {
                    ClearFocus();
                }
                // 星标按钮
                Rect rect_btn_favo = new Rect(itemRect.width - 40 - offset, itemRect.y + 12, btn_favourite.width, btn_favourite.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_favo, pret.preset.IsFavourite ? btn_favourite : btn_favourite_press, pret.preset.IsFavourite ? btn_favourite : btn_favourite_press))
                {
                    ClearFocus();

                    pret.preset.IsFavourite = !pret.preset.IsFavourite;
                    LastTweenPresetsSaved();

                    // 如果是星标模式下，点击星标按钮则会去除对应类中的预设星标效果，并重新刷新Favourite列表
                    if (isFavouriteMode)
                    {
                        XTweenPresetContainer pre_con = XTween_PresetManager.preset_Container_Load(pret.type);
                        for (int c = 0; c < pre_con.Presets.Count; c++)
                        {
                            XTweenPresetBase pre = pre_con.Presets[c];

                            if (pre.Name == pret.preset.Name)
                            {
                                pre.IsFavourite = pret.preset.IsFavourite;
                            }
                        }

                        // 将当前预设参数列表替换到目标Json文件中
                        XTween_PresetManager.preset_Container_Save_Replace(pret.type, pre_con.Presets);

                        // 再次重新打开预设星标列表
                        OpenFavouritePresets();
                    }
                }
            }

            GUI.EndScrollView();
        }
        /// <summary>
        /// 显示当前分类的预设总数
        /// </summary>
        private void Draw_PresetsCountStatistic(Rect rect)
        {
            Rect rect_sta = new Rect(rect.x + rect.width - 230, rect.y - 7, 200, 25);
            Editor_XTween_GUI.Gui_Labelfield(rect_sta, $"当前预设数量： {loadedpresets.Count}", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.75f, TextAnchor.MiddleRight, 13, Font_Light);
        }
        /// <summary>
        /// 刷新绘制参数
        /// </summary>
        /// <param name="pret"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Draw_Info(Rect rect)
        {
            Rect rect_info = rect;
            float startoff = 38;
            rect_info.Set(rect.x, rect.y, rect.width, rect.height);
            Editor_XTween_GUI.Gui_Box_Style(rect_info, XTweenGUIFilled.实体, Color.white * 0.25f);

            if (SelectedPresetItem == null)
            {
                rect_info.Set(rect.x + ((rect.width / 2) - 90), rect.y + ((rect.height / 2) - 15), 180, 30);
                //Editor_XTween_GUI.Gui_Box(rect_info, Color.red * 0.25f);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, "暂无预览信息", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.7f, TextAnchor.MiddleCenter, Vector2.zero, 13, true, TextClipping.Clip, false, Font_Bold);
                rect_info.Set(rect.x + ((rect.width / 2) - 90), rect.y + ((rect.height / 2) - 15) + 28, 180, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, "请先选中一个预设", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.5f, TextAnchor.MiddleCenter, Vector2.zero, 12, true, TextClipping.Clip, false, Font_Light);
                return;
            }

            rect_info.Set(rect.x + startoff, rect.y + 18, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, SelectedPresetItem.preset.Name, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 16, true, TextClipping.Ellipsis, false, Font_Bold);

            rect_info.Set(rect.x + startoff, rect.y + 53, 185, 80);
            Editor_XTween_GUI.Gui_MultiLabelfield(rect_info, SelectedPresetItem.preset.Description, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.65f, TextAnchor.UpperLeft, Vector2.zero, 13);

            rect_info.Set(rect.x + startoff, rect.y + 145, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, "参数概览", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 16, true, TextClipping.Ellipsis, false, Font_Bold);

            // 预设分类图标
            rect_info.Set(rect.x + ((rect.width - tweentypes_size) - 25), rect.y + 150, tweentypes_size * 0.65f, tweentypes_size * 0.65f);
            string nm = SelectedPresetItem.type.ToString().Split(new char[1] { '_' })[1];
            GUI.backgroundColor = Color.white * 0.5f;
            Editor_XTween_GUI.Gui_Icon(rect_info, GetTweenTypeBtnIcon($"{nm}_big"));
            GUI.backgroundColor = Color.white;

            float start = 160;
            float offset = 28;
            Color color = Color.white * 0.8f;

            rect_info.Set(rect.x + startoff, rect.y + start + offset, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"耗时：{SelectedPresetItem.preset.Duration} s", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 2, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"延迟：{SelectedPresetItem.preset.Delay} s", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 3, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"随机延迟：{SelectedPresetItem.preset.RandomDelay.Min} s - {SelectedPresetItem.preset.RandomDelay.Max} s", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 4, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"循环数：{SelectedPresetItem.preset.LoopCount} 次", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 5, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"循环方式：{SelectedPresetItem.preset.LoopType}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 6, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"相对模式：{(SelectedPresetItem.preset.IsRelative ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 7, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"自动杀死：{(SelectedPresetItem.preset.IsAutoKill ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 8, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"缓动方式：{SelectedPresetItem.preset.EaseMode}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 9, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"使用曲线：{(SelectedPresetItem.preset.UseCurve ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 10, 40, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, "曲线：", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, TextClipping.Ellipsis, false, Font_Light);
            GUI.enabled = false;
            rect_info.Set(rect.x + startoff + 40 + 5, rect.y + start + offset * 10 + 6, 140, 15);
            Editor_XTween_GUI.Gui_CurveField(rect_info, SelectedPresetItem.preset.Curve);
            GUI.enabled = true;

            rect_info.Set(rect.x + startoff + 3, rect.y + start + offset * 12 - 10, liquid.width, liquid.height);
            Editor_XTween_GUI.Gui_Icon(rect_info, liquid);

            rect_info.Set(rect.x + startoff + (liquid.width / 2) - (liquid_ease_bg.width / 2) + 2, rect.y + start + offset * 12 + (liquid_ease_bg.height / 2) - 20, liquid_ease_bg.width, liquid_ease_bg.height);
            Editor_XTween_GUI.Gui_Icon(rect_info, liquid_ease_bg);

            Texture2D tex_ease = Editor_XTween_GUI.GetIcon($"EaseCurveGraph/{SelectedPresetItem.preset.EaseMode}");
            GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            rect_info.Set(rect.x + startoff + 40, rect.y + start + offset * 12 + 16, tex_ease.width, tex_ease.height);
            Editor_XTween_GUI.Gui_Icon(rect_info, tex_ease);
            GUI.backgroundColor = Color.white;

            rect_info.Set(rect.x + startoff + (liquid.width / 2) - 60, rect.y + start + offset * 12 + liquid.height + 5, 120, 18);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"{SelectedPresetItem.preset.EaseMode}", XTweenGUIFilled.实体, XTweenGUIColor.亮白, Color.black, TextAnchor.MiddleCenter, Vector2.zero, 11, true, TextClipping.Ellipsis, false, Font_Bold);

            float int_dis = 10;
            float int_offset = -5;

            // 修改参数按钮
            rect_info.Set(rect.x + int_offset + ((rect.width / 2) - btn_edit.width - int_dis), rect.y + rect.height - 65, btn_edit.width, btn_edit.height);
            if (Editor_XTween_GUI.Gui_IconButton(rect_info, btn_edit, btn_edit_press))
            {
                ClearFocus();
            }
            // 应用参数按钮
            rect_info.Set(rect.x + int_offset + ((rect.width / 2) + btn_apply.width - int_dis), rect.y + rect.height - 65, btn_apply.width, btn_apply.height);
            if (Editor_XTween_GUI.Gui_IconButton(rect_info, btn_apply, btn_apply_press))
            {
                ClearFocus();
            }
        }
        #endregion

        #region PresetsButtonLogic
        /// <summary>
        /// 点击分类预设按钮的逻辑
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        private void TweenTypeBtnClickedEvent(Rect o, string type)
        {
            #region  检测是否重复点击同类型按钮
            // 在非星标模式下点击预设分类按钮切换时才会检测当前分类的重复性点击逻辑
            if (!isFavouriteMode)
            {
                if (tweentypes_lastSelectionName == type)
                    return;
            }

            if (!InSearchmode)
                // 保存上一次的预设参数
                LastTweenPresetsSaved();

            PresetSearchString = null;

            InSearchmode = false;

            isFavouriteMode = false;

            // 将新的动画预设类型赋值
            tweentypes_lastSelectionName = type;
            #endregion

            ClearFocus();

            // 将字符串解析成XTweenTypes枚举类
            XTweenTypes xt = (XTweenTypes)XTweenTypeFromString(type);

            // 读取指定枚举类型的预设容器
            XTweenPresetContainer container = LoadPresetsContainer(o, xt);
            PresetsAppendToList(container);
        }
        #endregion

        #region FavouritePresets
        /// <summary>
        /// 将所有星标预设加入列表中
        /// </summary>
        private void FavouritePresetsAddToList()
        {
            // 获取指定类型的所有预设类
            XTweenPresetContainer con_to = LoadPresetsContainer(XTweenTypes.原生动画_To);
            XTweenPresetContainer con_path = LoadPresetsContainer(XTweenTypes.路径_Path);
            XTweenPresetContainer con_pos = LoadPresetsContainer(XTweenTypes.位置_Position);
            XTweenPresetContainer con_rot = LoadPresetsContainer(XTweenTypes.旋转_Rotation);
            XTweenPresetContainer con_scl = LoadPresetsContainer(XTweenTypes.缩放_Scale);
            XTweenPresetContainer con_fill = LoadPresetsContainer(XTweenTypes.填充_Fill);
            XTweenPresetContainer con_size = LoadPresetsContainer(XTweenTypes.尺寸_Size);
            XTweenPresetContainer con_tiled = LoadPresetsContainer(XTweenTypes.平铺_Tiled);
            XTweenPresetContainer con_text = LoadPresetsContainer(XTweenTypes.文字_Text);
            XTweenPresetContainer con_tmp = LoadPresetsContainer(XTweenTypes.文字_TmpText);
            XTweenPresetContainer con_alp = LoadPresetsContainer(XTweenTypes.透明度_Alpha);
            XTweenPresetContainer con_shake = LoadPresetsContainer(XTweenTypes.震动_Shake);
            XTweenPresetContainer con_color = LoadPresetsContainer(XTweenTypes.颜色_Color);

            // 将类型的所有星标预设类加入列表
            FavouritePresetsAppendToList(con_to);
            FavouritePresetsAppendToList(con_path);
            FavouritePresetsAppendToList(con_pos);
            FavouritePresetsAppendToList(con_rot);
            FavouritePresetsAppendToList(con_scl);
            FavouritePresetsAppendToList(con_fill);
            FavouritePresetsAppendToList(con_size);
            FavouritePresetsAppendToList(con_tiled);
            FavouritePresetsAppendToList(con_text);
            FavouritePresetsAppendToList(con_tmp);
            FavouritePresetsAppendToList(con_alp);
            FavouritePresetsAppendToList(con_shake);
            FavouritePresetsAppendToList(con_color);
        }
        #endregion

        #region PresetLogic
        /// <summary>
        /// 获取指定类型的所有动画预设
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private XTweenPresetContainer LoadPresetsContainer(Rect rect, XTweenTypes type)
        {
            // 分类指示器光标坐标设置
            SelectionTypeMark_ReactSet(rect);

            // 获取指定类型的所有预设类
            XTweenPresetContainer container = XTween_PresetManager.preset_Container_Load(type);
            return container;
        }
        /// <summary>
        /// 获取指定类型的所有动画预设
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private XTweenPresetContainer LoadPresetsContainer(XTweenTypes type)
        {
            // 获取指定类型的所有预设类
            XTweenPresetContainer container = XTween_PresetManager.preset_Container_Load(type);
            return container;
        }
        /// <summary>
        /// 将容器中的所有预设添加到列表中
        /// </summary>
        /// <param name="container"></param>
        private void PresetsAppendToList(XTweenPresetContainer container)
        {
            if (loadedpresets == null)
                loadedpresets = new List<PresetItemGUIStruct>();
            loadedpresets.Clear();
            for (int i = 0; i < container.Presets.Count; i++)
            {
                PresetItemGUIStruct strc = new PresetItemGUIStruct();
                strc.isPressing = false;
                strc.preset = container.Presets[i];
                strc.type = (XTweenTypes)Enum.Parse(typeof(XTweenTypes), container.Type);
                loadedpresets.Add(strc);
            }
        }
        /// <summary>
        /// 将容器中的所有星标预设添加到列表中
        /// </summary>
        /// <param name="container"></param>
        private void FavouritePresetsAppendToList(XTweenPresetContainer container)
        {
            if (loadedpresets == null)
                loadedpresets = new List<PresetItemGUIStruct>();

            for (int i = 0; i < container.Presets.Count; i++)
            {
                PresetItemGUIStruct strc = new PresetItemGUIStruct();
                strc.isPressing = false;
                strc.type = (XTweenTypes)Enum.Parse(typeof(XTweenTypes), container.Type);
                strc.preset = container.Presets[i];
                if (strc.preset.IsFavourite)
                {
                    loadedpresets.Add(strc);
                }
            }
        }
        /// <summary>
        /// 保存上一次的预设类型参数文件数据
        /// </summary>
        private void LastTweenPresetsSaved()
        {
            if (isFavouriteMode)
                return;

            // 将字符串解析成XTweenTypes枚举类
            lastXtweenType = (XTweenTypes)XTweenTypeFromString(tweentypes_lastSelectionName);

            if (loadedpresets != null && loadedpresets.Count > 0)
            {
                List<XTweenPresetBase> pres = new List<XTweenPresetBase>();
                for (int i = 0; i < loadedpresets.Count; i++)
                {
                    PresetItemGUIStruct strc = loadedpresets[i];
                    pres.Add(strc.preset);
                }

                // 将当前预设参数列表替换到目标Json文件中
                XTween_PresetManager.preset_Container_Save_Replace(lastXtweenType, pres);
            }
        }
        /// <summary>
        /// 打开星标的预设列表
        /// </summary>
        /// <param name="fav_rect"></param>
        private void OpenFavouritePresets()
        {
            // 在进入星标模式前先保存最后一次选中的预设类型的光标坐标信息
            SaveConfig();

            // 明确进入星标模式
            isFavouriteMode = true;

            // 确保预设列表实例化
            if (loadedpresets == null)
                loadedpresets = new List<PresetItemGUIStruct>();
            // 确保先清空列表
            loadedpresets.Clear();

            // 设置光标
            SelectionTypeMark_OriginalRectSet(new Rect(favRect.x + 18, favRect.y - 5, 0, 0));
            // 将所有星标预设加入列表中
            FavouritePresetsAddToList();
        }
        #endregion

        #region SelectionTypemark
        /// <summary>
        /// 分类指示器光标 - 坐标设置（GUI同步联动赋值）
        /// </summary>
        /// <param name="rect"></param>
        private void SelectionTypeMark_ReactSet(Rect rect)
        {
            selectionmark_lastSelectionRect.Set(rect.x + tweentypes_size - selectionmark_offset, rect.y + (tweentypes_size / 2) - (selectionMark.height / 2), selectionMark.width, selectionMark.height);
        }
        /// <summary>
        /// 分类指示器光标 - 坐标设置（直接赋值，用于初始化）
        /// </summary>
        /// <param name="rect"></param>
        private void SelectionTypeMark_OriginalRectSet(Rect rect)
        {
            selectionmark_lastSelectionRect.Set(rect.x, rect.y, selectionMark.width, selectionMark.height);
        }
        /// <summary>
        /// 分类指示器光标 - 坐标偏移
        /// </summary>
        /// <param name="rect"></param>
        private void SelectionTypeMark_OffsetSet(float val)
        {
            selectionmark_offset = val;
        }
        #endregion

        #region ConfigSave
        /// <summary>
        /// 设置最后一次点击的类型保存配置
        /// </summary>
        private void SaveConfig()
        {
            TweenConfigData.PresetInFavouriteMode = isFavouriteMode;

            if (!isFavouriteMode)
                // 记录最后一次选择的预设名称
                TweenConfigData.PresetSelectionMark_LastTypeName = tweentypes_lastSelectionName;
            if (!isFavouriteMode)
                // 记录最后一次选择的预设Rect坐标信息
                TweenConfigData.PresetSelectionMark_LastRect = selectionmark_lastSelectionRect;

            string json = JsonUtility.ToJson(TweenConfigData);
            // 使用StreamWriter写入文件
            using (StreamWriter writer = new StreamWriter(XTween_Dashboard.Get_path_XTween_Config_Path() + $"XTweenConfigData.json"))
            {
                writer.Write(json);
            }
            AssetDatabase.Refresh();
        }
        #endregion

        #region SearchPreset
        private void SearchPreset(string m_PresetSearchString)
        {
            if (string.IsNullOrEmpty(m_PresetSearchString)) { return; }

            if (loadedpresets == null)
                loadedpresets = new List<PresetItemGUIStruct>();
            loadedpresets.Clear();

            List<XTweenPresetContainer> cons = XTween_PresetManager.preset_Container_GetAll();
            for (int i = 0; i < cons.Count; i++)
            {
                for (int s = 0; s < cons[i].Presets.Count; s++)
                {
                    XTweenPresetBase pre = cons[i].Presets[s];
                    if (pre.Name.Contains(m_PresetSearchString))
                    {
                        PresetItemGUIStruct strc = new PresetItemGUIStruct();
                        strc.preset = pre;
                        strc.type = (XTweenTypes)Enum.Parse(typeof(XTweenTypes), cons[i].Type);
                        strc.isPressing = false;
                        loadedpresets.Add(strc);
                    }
                }
            }
        }
        #endregion

        #region 辅助
        /// <summary>
        /// 清空焦点
        /// </summary>
        private static void ClearFocus()
        {
            GUI.FocusControl(null);
        }
        /// <summary>
        /// 获取动画预设类型按钮类型图标
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Texture2D GetTweenTypeBtnIcon(string name)
        {
            Texture2D tex = null;
            for (int i = 0; i < icons.Length; i++)
            {
                if (icons[i].name == name)
                {
                    tex = icons[i];
                }
            }
            return tex;
        }
        /// <summary>
        /// 获取动画预设类型按钮类型图标（按下状态）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Texture2D GetTweenTypeBtnIcon_Pressed(string name)
        {
            Texture2D tex = null;
            for (int i = 0; i < icons.Length; i++)
            {
                if (icons[i].name == name + "_press")
                {
                    tex = icons[i];
                }
            }
            return tex;
        }
        /// <summary>
        /// 将字符串名称解析为XTweenTypes枚举类
        /// </summary>
        /// <param name="typeString"></param>
        /// <returns></returns>
        private XTweenTypes XTweenTypeFromString(string typeString)
        {
            XTweenTypes types = XTweenTypes.无_None;

            // 获取指定类型的所有预设类
            string[] s = Enum.GetNames(typeof(XTweenTypes));

            for (int i = 0; i < s.Length; i++)
            {
                string a = s[i];
                string t = a.Split(new char[1] { '_' })[1];
                if (t == typeString)
                {
                    types = (XTweenTypes)Enum.Parse(typeof(XTweenTypes), a);
                }
            }
            return types;
        }
        #endregion

        private void OnDestroy()
        {
            SaveConfig();
        }
    }
}
