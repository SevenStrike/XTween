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
    using UnityEditor.PackageManager.UI;
    using UnityEditor.Presets;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.UIElements;
    using static Codice.Client.BaseCommands.Import.Commit;
    using static Codice.Client.Common.Connection.AskCredentialsToUser;

    [Serializable]
    public class PresetItemGUIStruct
    {
        public XTweenPresetBase preset;
        public XTweenTypes type;
        public bool isPressing;

        public PresetItemGUIStruct Clone()
        {
            PresetItemGUIStruct g = new PresetItemGUIStruct();
            g.type = type;
            g.isPressing = isPressing;
            g.preset = preset.Clone();

            return g;
        }
    }

    public class Editor_XTween_PresetsCentral : EditorWindow
    {
        private static Editor_XTween_PresetsCentral window;
        public static TweenConfigData TweenConfigData;
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
            btn_edit_ok,
            btn_edit_ok_press,
            btn_edit_cancel,
            btn_edit_cancel_press,
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
        /// <summary>
        /// 参数编辑模式
        /// </summary>
        private bool IsEditorMode;

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
        private Vector2 scrollPosition;
        private Rect scrollViewRect;
        private Rect viewRect;
        private Rect favRect;
        private List<PresetItemGUIStruct> loadedpresets = new List<PresetItemGUIStruct>();
        private XTweenTypes lastXtweenType;
        private PresetItemGUIStruct SelectedPresetItem;
        private PresetItemGUIStruct SelectedPresetItemForEditor;
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

                    //ClearFocus();
                    SetEditorMode(false);
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
        public static void OpenXTweenPresetsCentral()
        {
            window = (Editor_XTween_PresetsCentral)EditorWindow.GetWindow(typeof(Editor_XTween_PresetsCentral), false, "XTween 预设中心", true);

            #region 获取配置文件
            string json = AssetDatabase.LoadAssetAtPath<TextAsset>(XTween_Dashboard.Get_path_XTween_Config_Path() + $"XTweenConfigData.json").text;
            TweenConfigData = JsonUtility.FromJson<TweenConfigData>(json);
            #endregion

            if (TweenConfigData.PresetCentralWindowSize == Vector2.zero)
                TweenConfigData.PresetCentralWindowSize = new Vector2(970, 866);

            // 获取当前屏幕的分辨率
            int screenWidth = Screen.currentResolution.width;
            int screenHeight = Screen.currentResolution.height;
            // 获取记忆窗口尺寸
            Vector2 size = TweenConfigData.PresetCentralWindowSize;

            // 计算窗口位置（屏幕中心）
            Rect windowRect = new Rect((screenWidth - size.x) / 2.0f, (screenHeight - size.y) / 2.0f, size.x, size.y);

            // 更新窗口位置和大小
            window.position = windowRect;
            window.minSize = new Vector2(356, 760);
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

            btn_edit_ok = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_edit_ok");
            btn_edit_ok_press = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_edit_ok_press");
            btn_edit_cancel = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_edit_cancel");
            btn_edit_cancel_press = Editor_XTween_GUI.GetIcon("Icons_XTween_PresetsCentral/btn_edit_cancel_press");

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

            #region Favourite
            if (!isSize_HideInfo())
                favRect = new Rect(rect.x + 620, rect.y + 19, btn_favourite.width, btn_favourite.height);
            else
                favRect = new Rect(rect.x + rect.width - 65, rect.y + 19, btn_favourite.width, btn_favourite.height);

            if (Editor_XTween_GUI.Gui_IconButton(favRect, btn_favourite, btn_favourite_press))
            {
                ClearFocus();
                SetEditorMode(false);

                PresetSearchString = null;
                InSearchmode = false;
                OpenFavouritePresets();
            }
            #endregion

            #region Search
            if (isSize_HideSearchWidth())
                Draw_PresetSearch(new Rect(favRect.x, favRect.y, rect.width, rect.height));
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
            if (!isSize_HideInfo())
            {
                middle_strartpos = 70;
                middle_width_margin_right = 290;
                middle_height_margin_bottom = 20;
            }
            else
            {
                middle_strartpos = 70;
                middle_width_margin_right = 20;
                middle_height_margin_bottom = 20;
            }

            middle_rect = new Rect(left_rect.x + middle_strartpos, left_rect.y, rect.width - middle_width_margin_right - middle_strartpos, rect.height - 80 - middle_height_margin_bottom);

            //Editor_XTween_GUI.Gui_Box(middle_rect, Color.red * 0.3f);
            // 绘制列表
            Draw_PresetsScrollView_VirtualScrollOptmize(middle_rect);
            // 绘制当前分类的预设统计数量
            Draw_PresetsCountStatistic(middle_rect);
            #endregion

            #region right
            // right_rect 基础坐标
            right_rect = new Rect(rect.width - right_width, rect.y + 18, right_width - 15, rect.height - right_height_margin_bottom - 10);
            if (!isSize_HideInfo())
            {
                if (!IsEditorMode)
                    Draw_Info(right_rect);
                else
                    Draw_Editor(right_rect);
            }
            #endregion

            #region ClearFocus
            Event e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 0) // 左键点击
            {
                if (rect.Contains(e.mousePosition))
                {
                    ClearFocus();
                    e.Use(); // 标记事件已使用
                }
            }
            #endregion
        }

        #region Draw
        /// <summary>
        /// 搜索预设
        /// </summary>
        /// <param name="rect"></param>
        private void Draw_PresetSearch(Rect rect)
        {
            GUI.SetNextControlName("SearchTextField");

            Rect rect_search = new Rect(rect.x - 250, rect.y + 5, 230, 25);
            GUIStyle style = new GUIStyle(GUI.skin.textField);
            style.padding = new RectOffset(35, 0, 0, 0);
            style.alignment = TextAnchor.MiddleLeft;
            PresetSearchString = Editor_XTween_GUI.Gui_InputField_String(rect_search, PresetSearchString, style);

            Rect rect_search_icon = new Rect(rect_search.x, rect_search.y - 4, icon_search.width, icon_search.height);
            Editor_XTween_GUI.Gui_Icon(rect_search_icon, icon_search);

            // 检测当前哪个控件获得焦点
            string focusedControl = GUI.GetNameOfFocusedControl();

            // 根据焦点状态执行相应逻辑
            if (focusedControl == "SearchTextField")
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
            Event e = Event.current;
            if (Icon_rect.Contains(e.mousePosition))
            {
                if (e.type == EventType.MouseDown && e.button == (int)MouseButton.RightMouse)
                {

                    // 创建右键菜单
                    GenericMenu menu = new GenericMenu();

                    // 清空所有预设
                    menu.AddItem(new GUIContent("C 清空预设"), false, () =>
                    {
                        string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "清空预设", $"确定要清空所有预设文件吗？此操作会删除所有预设数据，将每个类型的预设文件重置为空列表！此操作不可逆！请谨慎操作！", "暂不", "清空", 0);

                        if (res == "清空")
                        {
                            bool success = ClearAllPresetFiles();

                            if (success)
                            {
                                // 清空当前显示的列表
                                loadedpresets.Clear();

                                // 隐藏分类指示器
                                SelectionTypeMark_OriginalRectSet(new Rect(-100, -100, 0, 0));

                                // 清空选中的预设
                                SelectedPresetItem = null;
                                SelectedPresetItemForEditor = null;

                                // 退出编辑模式
                                SetEditorMode(false);

                                // 刷新界面
                                Repaint();

                                // 提示用户操作成功
                                Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "清空完成", $"所有预设文件已清空！", "明白", 0);
                            }
                        }
                    });

                    // 导出所有预设
                    menu.AddItem(new GUIContent("E 导出预设"), false, () =>
                    {
                        string path = EditorUtility.SaveFolderPanel("XTween预设管理器预设导出", EditorApplication.applicationPath, "");
                        Debug.Log(path);
                        bool x = XTween_PresetManager.preset_ExportAllPresets(path);

                        if (x)
                        {
                            EditorApplication.delayCall += () =>
                            {
                                string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "导出预设", $"成功导出所有预设文件！", "明白", "查看", 0);

                                if (res == "查看")
                                {
                                    // 检查路径是否有效
                                    if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                                    {
                                        try
                                        {
                                            // 使用 System.Diagnostics.Process 直接打开文件夹（不选中任何文件）
                                            System.Diagnostics.Process.Start(path);

                                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已打开导出文件夹: {path}", XTweenGUIMsgState.确认);
                                        }
                                        catch (Exception e)
                                        {
                                            Debug.LogError($"打开文件夹失败: {e.Message}");

                                            // 备用方案：使用 Application.OpenURL
                                            try
                                            {
                                                Application.OpenURL("file://" + path);
                                            }
                                            catch
                                            {
                                                // 如果都失败，至少告诉用户路径
                                                Editor_XTween_GUI.Open(XTweenDialogType.错误, "XTween预设管理器消息", "打开文件夹失败",
                                                    $"无法自动打开文件夹，请手动访问：\n{path}", "明白", "", 0);
                                            }
                                        }
                                    }
                                }
                            };
                        }
                    });

                    // 导入所有预设
                    menu.AddItem(new GUIContent("I 导入预设"), false, () =>
                    {
                        loadedpresets.Clear();
                        SelectionTypeMark_OriginalRectSet(new Rect(-100, -100, 0, 0));

                        // 弹出文件夹选择对话框
                        string importPath = EditorUtility.OpenFolderPanel("选择要导入的预设文件夹", EditorApplication.applicationPath, "");

                        if (string.IsNullOrEmpty(importPath))
                        {
                            // 用户取消了选择
                            return;
                        }

                        EditorApplication.delayCall += () =>
                        {
                            // 确认导入操作
                            string confirmRes = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "导入预设", $"确定要从以下文件夹导入预设吗？{importPath} 。注意：导入操作会覆盖同名的预设文件！", "取消", "导入", 0);

                            if (confirmRes != "导入")
                                return;

                            // 执行导入
                            bool importSuccess = ImportPresetsFromFolder(importPath);

                            if (importSuccess)
                            {
                                // 刷新当前显示的预设列表
                                if (!isFavouriteMode && !string.IsNullOrEmpty(tweentypes_lastSelectionName))
                                {
                                    XTweenTypes xt = (XTweenTypes)XTweenTypeFromString(tweentypes_lastSelectionName);
                                    XTweenPresetContainer container = LoadPresetsContainer(xt);
                                    PresetsAppendToList(container);
                                }
                                else if (isFavouriteMode)
                                {
                                    OpenFavouritePresets();
                                }
                                Repaint();
                            }
                        };
                    });

                    // 显示菜单
                    menu.ShowAsContext();

                    e.Use();
                }
            }



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
                    ClearFocus();
                    SetEditorMode(false);

                    TweenTypeBtnClickedEvent(o, tweentypes_names[i]);
                }
                #endregion
            }
        }
        /// <summary>
        /// 绘制预设列表（优化版：虚拟滚动）
        /// </summary>
        /// <param name="rect"></param>
        private void Draw_PresetsScrollView_VirtualScrollOptmize(Rect rect)
        {
            // 定义滚动视图的区域
            scrollViewRect = new Rect(rect.x, rect.y + 25, rect.width, rect.height - 35);

            // 计算总内容高度
            float itemTotalHeight = scroll_item_height + scroll_item_distance;
            if (isSize_HideInfo())
                itemTotalHeight = scroll_item_height + scroll_item_distance;
            float totalHeight = loadedpresets.Count * itemTotalHeight;

            // 可见区域的高度
            float viewportHeight = scrollViewRect.height;

            // 开始滚动视图 - 注意这里viewRect的高度要设置为totalHeight
            viewRect = new Rect(0, 0, scrollViewRect.width - 20, totalHeight);
            scrollPosition = GUI.BeginScrollView(scrollViewRect, scrollPosition, viewRect);

            // 获取当前事件
            Event e = Event.current;

            #region 虚拟滚动：计算可见范围
            // 计算当前滚动位置对应的第一个项目的索引
            int startIndex = Mathf.FloorToInt(scrollPosition.y / itemTotalHeight);
            // 计算可见区域内最多能显示多少个项目
            int visibleCount = Mathf.CeilToInt(viewportHeight / itemTotalHeight) + 2; // +2 作为缓冲区，避免滚动时出现空白
                                                                                      // 确保索引不越界
            startIndex = Mathf.Max(0, startIndex);
            int endIndex = Mathf.Min(loadedpresets.Count, startIndex + visibleCount);
            #endregion

            // 只绘制可见范围内的项目
            for (int i = startIndex; i < endIndex; i++)
            {
                PresetItemGUIStruct pret = loadedpresets[i];

                // 计算项目的实际Y位置（基于真实索引）
                float itemY = i * itemTotalHeight;
                Rect itemRect = new Rect(5, itemY, viewRect.width - 10, scroll_item_height);
                Rect itemRect_clicked = new Rect(5, itemY, viewRect.width - 210, scroll_item_height);

                if (isSize_HideInfo())
                    itemRect_clicked = new Rect(5, itemY, viewRect.width, scroll_item_height / 2 + 15);

                // 检测鼠标点击
                if (itemRect_clicked.Contains(e.mousePosition))
                {
                    if (!isSize_HideInfo())
                    {
                        if (e.type == EventType.MouseDown && e.button == (int)MouseButton.LeftMouse)
                        {
                            ClearFocus();
                            SetEditorMode(false);

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
                    if (e.type == EventType.MouseDown && e.button == (int)MouseButton.RightMouse)
                    {
                        // 保存当前选中的预设
                        SelectedPresetItem = pret;

                        // 创建右键菜单
                        GenericMenu menu = new GenericMenu();

                        // 添加菜单项
                        if (!isSize_HideInfo())
                            menu.AddItem(new GUIContent("E 编辑预设"), false, OnEditPreset, pret);
                        menu.AddItem(new GUIContent("A 应用预设"), false, OnApplyPreset, pret);
                        menu.AddSeparator("");
                        menu.AddItem(new GUIContent("D 删除预设"), false, OnDeletePreset, pret);
                        menu.AddSeparator("");
                        menu.AddItem(new GUIContent($"{(pret.preset.IsFavourite ? "S 取消星标" : "S 设为星标")}"), pret.preset.IsFavourite, OnToggleFavourite, pret);

                        // 显示菜单
                        menu.ShowAsContext();

                        e.Use();
                    }
                }


                // 绘制背景
                if (pret.isPressing)
                    Editor_XTween_GUI.Gui_Box_Style(itemRect, XTweenGUIFilled.实体, ItemPressColor);
                else
                    Editor_XTween_GUI.Gui_Box_Style(itemRect, XTweenGUIFilled.实体, ItemColor);

                // 绘制图标（根据模式决定是否显示）
                if (isFavouriteMode || InSearchmode)
                {
                    Rect rect_icon = new Rect(itemRect.x + 8, itemRect.y + 12, tweentypes_size * 0.7f, tweentypes_size * 0.7f);
                    string nm = pret.type.ToString().Split(new char[1] { '_' })[1];
                    GUI.backgroundColor = Color.white * 0.6f;
                    Editor_XTween_GUI.Gui_Icon(rect_icon, GetTweenTypeBtnIcon(nm));
                    GUI.backgroundColor = Color.white;
                }

                #region 间距判定
                float dis = 26;
                if (InSearchmode || isFavouriteMode)
                {
                    dis = 50;
                }
                #endregion

                // 绘制标题
                Rect rect_title = new Rect(itemRect.x + dis, itemRect.y + 5, 280, 25);
                if (isSize_HideInfo())
                    if (InSearchmode || isFavouriteMode)
                    {
                        rect_title = new Rect(itemRect.x + dis, itemRect.y + 5, itemRect.width / 2.7f, 25);
                    }
                    else
                        rect_title = new Rect(itemRect.x + dis, itemRect.y + 5, itemRect.width / 2, 25);

#if UNITY_6000_0_OR_NEWER
                // Unity 6+ 使用 Ellipsis
                TextClipping clipping = TextClipping.Ellipsis;
#else
            // Unity 2021.1 之前使用 Clip
            TextClipping clipping = TextClipping.Clip;
#endif

                Editor_XTween_GUI.Gui_Labelfield(rect_title, pret.preset.Name, XTweenGUIFilled.无, XTweenGUIColor.无,
                    Color.white, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Bold);

                // 绘制解释
                Rect rect_des = new Rect(itemRect.x + dis, itemRect.y + itemRect.height - 25 - 3, 300, 25);
                if (isSize_HideInfo())
                    if (InSearchmode || isFavouriteMode)
                    {
                        rect_des = new Rect(itemRect.x + dis, itemRect.y + itemRect.height - 25 - 3, itemRect.width / 2.5f, 25);
                    }
                    else
                        rect_des = new Rect(itemRect.x + dis, itemRect.y + itemRect.height - 25 - 3, itemRect.width / 1.8f, 25);

                Editor_XTween_GUI.Gui_Labelfield(rect_des, pret.preset.Description, XTweenGUIFilled.无, XTweenGUIColor.无,
                    Color.white * 0.75f, TextAnchor.MiddleLeft, Vector2.zero, 12, true, clipping, false, Font_Light);

                float offset = 20;

                // 修改参数按钮
                if (!isSize_HideInfo())
                {
                    Rect rect_btn_edit = new Rect(itemRect.width - 160 - offset, itemRect.y + 12, btn_edit.width, btn_edit.height);
                    if (Editor_XTween_GUI.Gui_IconButton(rect_btn_edit, btn_edit, btn_edit_press))
                    {
                        ClearFocus();

                        SelectedPresetItem = pret;
                        SetEditorMode(true);
                    }
                }

                // 应用参数按钮
                Rect rect_btn_apply = new Rect(itemRect.width - 100 - offset, itemRect.y + 12, btn_apply.width, btn_apply.height);
                if (isSize_HideInfo())
                    rect_btn_apply = new Rect(itemRect.width - 60 - offset, itemRect.y + 12, btn_apply.width, btn_apply.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_apply, btn_apply, btn_apply_press))
                {
                    ClearFocus();

                    ApplyToController(pret.preset);
                }

                // 星标按钮
                Rect rect_btn_favo = new Rect(itemRect.width - 40 - offset, itemRect.y + 12, btn_favourite.width, btn_favourite.height);
                if (isSize_HideInfo())
                    rect_btn_favo = new Rect(itemRect.width - 20 - offset, itemRect.y + 12, btn_favourite.width, btn_favourite.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_favo,
                    pret.preset.IsFavourite ? btn_favourite : btn_favourite_press,
                    pret.preset.IsFavourite ? btn_favourite : btn_favourite_press))
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

                        //// 再次重新打开预设星标列表
                        //OpenFavouritePresets();
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

#if UNITY_6000_0_OR_NEWER
            // Unity 6+ 使用 Ellipsis
            TextClipping clipping = TextClipping.Ellipsis;
#else
            // Unity 2021.1 之前使用 Clip
            TextClipping clipping = TextClipping.Clip;
#endif

            rect_info.Set(rect.x + startoff, rect.y + 18, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, SelectedPresetItem.preset.Name, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 16, true, clipping, false, Font_Bold);

            rect_info.Set(rect.x + startoff, rect.y + 53, 185, 80);
            Editor_XTween_GUI.Gui_MultiLabelfield(rect_info, SelectedPresetItem.preset.Description, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.65f, TextAnchor.UpperLeft, Vector2.zero, 13);

            rect_info.Set(rect.x + startoff, rect.y + 145, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, "参数概览", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 16, true, clipping, false, Font_Bold);

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
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"耗时：{SelectedPresetItem.preset.Duration} s", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 2, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"延迟：{SelectedPresetItem.preset.Delay} s", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 3, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"随机延迟：{SelectedPresetItem.preset.UseRandomDelay}  |  {SelectedPresetItem.preset.RandomDelay.Min} s - {SelectedPresetItem.preset.RandomDelay.Max} s", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 4, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"循环数：{SelectedPresetItem.preset.LoopCount} 次", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 5, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"循环方式：{SelectedPresetItem.preset.LoopType}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 6, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"循环延迟：{SelectedPresetItem.preset.LoopDelay}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 7, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"相对模式：{(SelectedPresetItem.preset.IsRelative ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 8, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"自动杀死：{(SelectedPresetItem.preset.IsAutoKill ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 9, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"缓动方式：{SelectedPresetItem.preset.EaseMode}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 10, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"使用曲线：{(SelectedPresetItem.preset.UseCurve ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 11, 40, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, "曲线：", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
            GUI.enabled = false;
            rect_info.Set(rect.x + startoff + 40 + 5, rect.y + start + offset * 11 + 6, 140, 15);
            Editor_XTween_GUI.Gui_CurveField(rect_info, SelectedPresetItem.preset.Curve);
            GUI.enabled = true;

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 12, 185, 30);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"使用起始：{(SelectedPresetItem.preset.UseFromMode ? "是" : "否")}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

            if (SelectedPresetItem.preset is XTweenPreset_To to)
            {
                switch (to.ToType)
                {
                    case XTweenTypes_To.整数_Int:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{to.EndValue_Int}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{to.FromValue_Int}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_To.浮点数_Float:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{to.EndValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{to.FromValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_To.字符串_String:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{to.EndValue_String}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{to.FromValue_String}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_To.二维向量_Vector2:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{to.EndValue_Vector2}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{to.FromValue_Vector2}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_To.三维向量_Vector3:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{to.EndValue_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{to.FromValue_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_To.四维向量_Vector4:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{to.EndValue_Vector4}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{to.FromValue_Vector4}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_To.颜色_Color:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13.2f, 185, 18);
                        Editor_XTween_GUI.Gui_ColorField(rect_info, "目标值：", 60, to.EndValue_Color);
                        if (to.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14.2f, 185, 18);
                            Editor_XTween_GUI.Gui_ColorField(rect_info, "起始值：", 60, to.FromValue_Color);
                        }
                        break;
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Path path)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"路径名称：{path.PathName}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
            }
            if (SelectedPresetItem.preset is XTweenPreset_Position pos)
            {
                switch (pos.PositionType)
                {
                    case XTweenTypes_Positions.锚点位置_AnchoredPosition:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{pos.EndValue_Vector2}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (pos.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{pos.FromValue_Vector2}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_Positions.锚点位置3D_AnchoredPosition3D:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{pos.EndValue_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (pos.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{pos.FromValue_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Rotation rot)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"旋转类型：{rot.RotationType}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"旋转方式：{rot.RotationMode}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 15, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"坐标空间：{rot.RotationSpace}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"平滑模式：{rot.RotateLerpMode}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

                switch (rot.RotationType)
                {
                    case XTweenTypes_Rotations.欧拉角度_Euler:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 17, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{rot.EndValue_Euler}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (rot.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{rot.FromValue_Euler}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_Rotations.四元数_Quaternion:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 17, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{rot.EndValue_Quaternion}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (rot.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{rot.FromValue_Quaternion}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Scale scale)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{scale.EndValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                if (scale.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                    Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{scale.FromValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Fill fill)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{fill.EndValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                if (fill.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                    Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{fill.FromValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Size size)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{size.EndValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                if (size.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                    Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{size.FromValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Tiled tiled)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{tiled.EndValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                if (tiled.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                    Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{tiled.FromValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Text text)
            {
                switch (text.TextType)
                {
                    case XTweenTypes_Text.文字尺寸_FontSize:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{text.EndValue_Int}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (text.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{text.FromValue_Int}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_Text.文字行高_LineHeight:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{text.EndValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (text.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{text.FromValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_Text.文字颜色_Color:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{text.EndValue_Color}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (text.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{text.FromValue_Color}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_Text.文字内容_Content:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{text.EndValue_String}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (text.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{text.FromValue_String}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                }

            }
            if (SelectedPresetItem.preset is XTweenPreset_TmpText tmp)
            {
                switch (tmp.TmpTextType)
                {
                    case XTweenTypes_TmpText.文字尺寸_FontSize:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{tmp.EndValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (tmp.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{tmp.FromValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_TmpText.文字行高_LineHeight:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{tmp.EndValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (tmp.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{tmp.FromValue_Float}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_TmpText.文字颜色_Color:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{tmp.EndValue_Color}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (tmp.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{tmp.FromValue_Color}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_TmpText.文字内容_Content:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{tmp.EndValue_String}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (tmp.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{tmp.FromValue_String}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                    case XTweenTypes_TmpText.文字边距_Margin:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{tmp.EndValue_Vector4}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        if (tmp.UseFromMode)
                        {
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{tmp.FromValue_Vector4}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        }
                        break;
                }

            }
            if (SelectedPresetItem.preset is XTweenPreset_Alpha alp)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{alp.EndValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                if (alp.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                    Editor_XTween_GUI.Gui_Labelfield(rect_info, $"起始值：{alp.FromValue}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Shake shake)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"震动幅度：{shake.Vibrato}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 14, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"随机化：{shake.Randomness}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 15, 185, 30);
                Editor_XTween_GUI.Gui_Labelfield(rect_info, $"过渡震动：{shake.FadeShake}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                switch (shake.ShakeType)
                {
                    case XTweenTypes_Shakes.位置_Position:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{shake.Strength_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        break;
                    case XTweenTypes_Shakes.尺寸_Size:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{shake.Strength_Vector2}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        break;
                    case XTweenTypes_Shakes.旋转_Rotation:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{shake.Strength_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        break;
                    case XTweenTypes_Shakes.缩放_Scale:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16, 185, 30);
                        Editor_XTween_GUI.Gui_Labelfield(rect_info, $"目标值：{shake.Strength_Vector3}", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
                        break;
                }
            }
            if (SelectedPresetItem.preset is XTweenPreset_Color col)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 13.2f, 185, 18);
                Editor_XTween_GUI.Gui_ColorField(rect_info, "目标值：", 60, col.EndValue);
                if (col.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 14.2f, 185, 18);
                    Editor_XTween_GUI.Gui_ColorField(rect_info, "起始值：", 60, col.FromValue);
                }
            }


            bool rotmode = SelectedPresetItem.preset is XTweenPreset_Rotation;

            #region 液晶显示

            rect_info.Set(rect.x + startoff + 3, rect.y + start + offset * 17 + (rotmode ? 75 : 0) - 10, liquid.width, liquid.height);
            Editor_XTween_GUI.Gui_Icon(rect_info, liquid);

            rect_info.Set(rect.x + startoff + (liquid.width / 2) - (liquid_ease_bg.width / 2) + 2, rect.y + start + offset * 17 + (rotmode ? 75 : 0) + (liquid_ease_bg.height / 2) - 20, liquid_ease_bg.width, liquid_ease_bg.height);
            Editor_XTween_GUI.Gui_Icon(rect_info, liquid_ease_bg);

            Texture2D tex_ease = Editor_XTween_GUI.GetIcon($"EaseCurveGraph/{SelectedPresetItem.preset.EaseMode}");
            GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            rect_info.Set(rect.x + startoff + 40, rect.y + start + offset * 17 + (rotmode ? 75 : 0) + 16, tex_ease.width, tex_ease.height);
            Editor_XTween_GUI.Gui_Icon(rect_info, tex_ease);
            GUI.backgroundColor = Color.white;

            rect_info.Set(rect.x + startoff + 30, rect.y + start + offset * 17 + (rotmode ? 75 : 0) + 8, 120, 18);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, $"{SelectedPresetItem.preset.EaseMode}", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.75f, TextAnchor.MiddleLeft, Vector2.zero, 10, true, clipping, false, Font_Bold);
            #endregion

            float int_dis = 10;
            float int_offset = -5;

            // 修改参数按钮
            rect_info.Set(rect.x + int_offset + ((rect.width / 2) - btn_edit.width - int_dis), rect.y + rect.height - 45, btn_edit.width, btn_edit.height);
            if (Editor_XTween_GUI.Gui_IconButton(rect_info, btn_edit, btn_edit_press))
            {
                ClearFocus();
                SetEditorMode(true);
            }
            // 应用参数按钮
            rect_info.Set(rect.x + int_offset + ((rect.width / 2) + btn_apply.width - int_dis), rect.y + rect.height - 45, btn_apply.width, btn_apply.height);
            if (Editor_XTween_GUI.Gui_IconButton(rect_info, btn_apply, btn_apply_press))
            {
                ClearFocus();

                ApplyToController(SelectedPresetItem.preset);
            }
        }
        /// <summary>
        /// 绘制参数修改器
        /// </summary>
        private void Draw_Editor(Rect rect)
        {
            Rect rect_info = rect;
            float startoff = 20;
            rect_info.Set(rect.x, rect.y, rect.width, rect.height);
            Editor_XTween_GUI.Gui_Box_Style(rect, XTweenGUIFilled.实体, Color.white * 0.25f);

            XTweenPresetBase pre = SelectedPresetItemForEditor.preset;

            rect_info.Set(rect.x + startoff, rect.y + 18, 218, 21);
            pre.Name = Editor_XTween_GUI.Gui_InputField_String(rect_info, "预设名称", 60, pre.Name);

            rect_info.Set(rect.x + startoff, rect.y + 53, 218, 80);
            pre.Description = Editor_XTween_GUI.Gui_TextField(rect_info, pre.Description, Color.white, 12);

            float int_dis = 10;
            float int_offset = -5;

            float start = 120;
            float offset = 28;
            Color color = Color.white * 0.8f;

            rect_info.Set(rect.x + startoff, rect.y + start + offset, 218, 18);
            pre.Duration = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "耗时", 40, pre.Duration);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 2, 218, 18);
            pre.Delay = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "延迟", 40, pre.Delay);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 3, 218, 18);
            pre.RandomDelay.Min = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "最小延迟", 60, pre.RandomDelay.Min);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 4, 218, 18);
            pre.RandomDelay.Max = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "最大延迟", 60, pre.RandomDelay.Max);

#if UNITY_6000_0_OR_NEWER
            // Unity 6+ 使用 Ellipsis
            TextClipping clipping = TextClipping.Ellipsis;
#else
            // Unity 2021.1 之前使用 Clip
            TextClipping clipping = TextClipping.Clip;
#endif

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 5.1f, 218, 18);
            Editor_XTween_GUI.Gui_Labelfield(rect_info, "曲线：", XTweenGUIFilled.无, XTweenGUIColor.无, color, TextAnchor.MiddleLeft, Vector2.zero, 13, true, clipping, false, Font_Light);
            rect_info.Set(rect.x + startoff + 40, rect.y + start + offset * 5.1f - 2, 218 - 40, 18);
            Editor_XTween_GUI.Gui_CurveField(rect_info, SelectedPresetItem.preset.Curve);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 6.1f, 218, 18);
            pre.LoopCount = Editor_XTween_GUI.Gui_InputField_Int(rect_info, "循环次数", 60, pre.LoopCount);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 7.1f, 218, 18);
            pre.LoopDelay = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "循环延迟", 60, pre.LoopDelay);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 8.3f, 218, 18);
            string[] ease_names = Enum.GetNames(typeof(EaseMode));
            int ease_index = (int)pre.EaseMode;
            pre.EaseMode = (EaseMode)Editor_XTween_GUI.Gui_Popup(rect_info, "缓动参数", 60, new Vector2(0, -2), Font_Light, ease_index, ease_names, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 9.3f, 218, 18);
            string[] loop_types = Enum.GetNames(typeof(XTween_LoopType));
            int looptypes_index = (int)pre.LoopType;
            pre.LoopType = (XTween_LoopType)Editor_XTween_GUI.Gui_Popup(rect_info, "循环模式", 60, new Vector2(0, -3), Font_Light, looptypes_index, loop_types, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 10.3f, 218, 18);
            pre.UseRandomDelay = Editor_XTween_GUI.Gui_Toggle(rect_info, "随机延迟", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.UseRandomDelay, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 11.3f, 218, 18);
            pre.IsRelative = Editor_XTween_GUI.Gui_Toggle(rect_info, "相对模式", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.IsRelative, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 12.3f, 218, 18);
            pre.IsAutoKill = Editor_XTween_GUI.Gui_Toggle(rect_info, "自动杀死", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.IsAutoKill, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 13.3f, 218, 18);
            pre.UseCurve = Editor_XTween_GUI.Gui_Toggle(rect_info, "使用曲线", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.UseCurve, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 14.3f, 218, 18);
            pre.UseFromMode = Editor_XTween_GUI.Gui_Toggle(rect_info, "指定起始", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.UseFromMode, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

            rect_info.Set(rect.x + startoff, rect.y + start + offset * 15.3f, 218, 18);
            pre.IsFavourite = Editor_XTween_GUI.Gui_Toggle(rect_info, "喜爱星标", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.IsFavourite, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

            if (pre is XTweenPreset_To to)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                switch (to.ToType)
                {
                    case XTweenTypes_To.整数_Int:
                        to.EndValue_Int = Editor_XTween_GUI.Gui_InputField_Int(rect_info, "目标值", 60, to.EndValue_Int);
                        break;
                    case XTweenTypes_To.浮点数_Float:
                        to.EndValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, to.EndValue_Float);
                        break;
                    case XTweenTypes_To.字符串_String:
                        to.EndValue_String = Editor_XTween_GUI.Gui_InputField_String(rect_info, "目标值", 60, to.EndValue_String);
                        break;
                    case XTweenTypes_To.二维向量_Vector2:
                        to.EndValue_Vector2 = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "目标值", 60, to.EndValue_Vector2);
                        break;
                    case XTweenTypes_To.三维向量_Vector3:
                        to.EndValue_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, to.EndValue_Vector3);
                        break;
                    case XTweenTypes_To.四维向量_Vector4:
                        to.EndValue_Vector4 = Editor_XTween_GUI.Gui_InputField_Vector4(rect_info, "目标值", 60, to.EndValue_Vector4);
                        break;
                    case XTweenTypes_To.颜色_Color:
                        to.EndValue_Color = Editor_XTween_GUI.Gui_ColorField(rect_info, "目标值", 60, to.EndValue_Color);
                        break;
                }
                if (to.UseFromMode)
                {
                    switch (to.ToType)
                    {
                        case XTweenTypes_To.整数_Int:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            to.FromValue_Int = Editor_XTween_GUI.Gui_InputField_Int(rect_info, "起始值", 60, to.FromValue_Int);
                            break;
                        case XTweenTypes_To.浮点数_Float:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            to.FromValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, to.FromValue_Float);
                            break;
                        case XTweenTypes_To.字符串_String:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            to.FromValue_String = Editor_XTween_GUI.Gui_InputField_String(rect_info, "起始值", 60, to.FromValue_String);
                            break;
                        case XTweenTypes_To.二维向量_Vector2:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                            to.FromValue_Vector2 = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "起始值", 60, to.FromValue_Vector2);
                            break;
                        case XTweenTypes_To.三维向量_Vector3:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                            to.FromValue_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "起始值", 60, to.FromValue_Vector3);
                            break;
                        case XTweenTypes_To.四维向量_Vector4:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                            to.FromValue_Vector4 = Editor_XTween_GUI.Gui_InputField_Vector4(rect_info, "起始值", 60, to.FromValue_Vector4);
                            break;
                        case XTweenTypes_To.颜色_Color:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            to.FromValue_Color = Editor_XTween_GUI.Gui_ColorField(rect_info, "起始值", 60, to.FromValue_Color);
                            break;
                    }
                }
            }

            if (pre is XTweenPreset_Path path)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                path.PathName = Editor_XTween_GUI.Gui_InputField_String(rect_info, "路径名称", 60, path.PathName);
            }

            if (pre is XTweenPreset_Position pos)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                switch (pos.PositionType)
                {
                    case XTweenTypes_Positions.锚点位置_AnchoredPosition:
                        pos.EndValue_Vector2 = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "目标值", 60, pos.EndValue_Vector2);
                        break;
                    case XTweenTypes_Positions.锚点位置3D_AnchoredPosition3D:
                        pos.EndValue_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, pos.EndValue_Vector3);
                        break;
                }
                if (pos.UseFromMode)
                {
                    switch (pos.PositionType)
                    {
                        case XTweenTypes_Positions.锚点位置_AnchoredPosition:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                            pos.FromValue_Vector2 = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "起始值", 60, pos.FromValue_Vector2);
                            break;
                        case XTweenTypes_Positions.锚点位置3D_AnchoredPosition3D:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                            pos.FromValue_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "起始值", 60, pos.FromValue_Vector3);
                            break;
                    }
                }
            }

            if (pre is XTweenPreset_Rotation rot)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.3f, 218, 18);
                string[] rottype_names = Enum.GetNames(typeof(XTweenTypes_Rotations));
                int rottype_index = (int)rot.RotationType;
                rot.RotationType = (XTweenTypes_Rotations)Editor_XTween_GUI.Gui_Popup(rect_info, "旋转模式", 60, new Vector2(0, -2), Font_Light, rottype_index, rottype_names, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.3f, 218, 18);
                string[] rotmode_names = Enum.GetNames(typeof(XTweenRotationMode));
                int rotmode_index = (int)rot.RotationMode;
                rot.RotationMode = (XTweenRotationMode)Editor_XTween_GUI.Gui_Popup(rect_info, "旋转方式", 60, new Vector2(0, -2), Font_Light, rotmode_index, rotmode_names, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.3f, 218, 18);
                string[] rotslerp_names = Enum.GetNames(typeof(XTweenRotateLerpType));
                int rotslerp_index = (int)rot.RotateLerpMode;
                rot.RotateLerpMode = (XTweenRotateLerpType)Editor_XTween_GUI.Gui_Popup(rect_info, "旋转方式", 60, new Vector2(0, -2), Font_Light, rotslerp_index, rotslerp_names, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 19.3f, 218, 18);
                string[] rotspace_names = Enum.GetNames(typeof(XTweenRotationSpace));
                int rotspace_index = (int)rot.RotationSpace;
                rot.RotationSpace = (XTweenRotationSpace)Editor_XTween_GUI.Gui_Popup(rect_info, "旋转空间", 60, new Vector2(0, -2), Font_Light, rotspace_index, rotspace_names, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 20.5f, 218, 18);
                switch (rot.RotationType)
                {
                    case XTweenTypes_Rotations.欧拉角度_Euler:
                        rot.EndValue_Euler = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, rot.EndValue_Euler);
                        break;
                    case XTweenTypes_Rotations.四元数_Quaternion:
                        Quaternion qua = rot.EndValue_Quaternion;
                        Vector4 v = new Vector4(qua.x, qua.y, qua.z, qua.w);
                        Vector4 s = Editor_XTween_GUI.Gui_InputField_Vector4(rect_info, "目标值", 60, v);
                        rot.EndValue_Quaternion = new Quaternion(s.x, s.y, s.z, s.w);
                        break;
                }
                if (rot.UseFromMode)
                {
                    switch (rot.RotationType)
                    {
                        case XTweenTypes_Rotations.欧拉角度_Euler:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 22.2f, 218, 18);
                            rot.FromValue_Euler = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "起始值", 60, rot.FromValue_Euler);
                            break;
                        case XTweenTypes_Rotations.四元数_Quaternion:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 22.2f, 218, 18);
                            Quaternion qua = rot.FromValue_Quaternion;
                            Vector4 v = new Vector4(qua.x, qua.y, qua.z, qua.w);
                            Vector4 s = Editor_XTween_GUI.Gui_InputField_Vector4(rect_info, "起始值", 60, v);
                            rot.FromValue_Quaternion = new Quaternion(s.x, s.y, s.z, s.w);
                            break;
                    }
                }
            }

            if (pre is XTweenPreset_Scale scale)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                scale.EndValue = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, scale.EndValue);
                if (scale.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                    scale.FromValue = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "起始值", 60, scale.FromValue);
                }
            }

            if (pre is XTweenPreset_Fill fill)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                fill.EndValue = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, fill.EndValue);
                if (fill.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                    fill.FromValue = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, fill.FromValue);
                }
            }

            if (pre is XTweenPreset_Size size)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                size.EndValue = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "目标值", 60, size.EndValue);
                if (size.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                    size.FromValue = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "起始值", 60, size.FromValue);
                }
            }

            if (pre is XTweenPreset_Tiled tiled)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                tiled.EndValue = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, tiled.EndValue);
                if (tiled.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                    tiled.FromValue = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, tiled.FromValue);
                }
            }

            if (pre is XTweenPreset_Text text)
            {
                switch (text.TextType)
                {
                    case XTweenTypes_Text.文字尺寸_FontSize:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        text.EndValue_Int = Editor_XTween_GUI.Gui_InputField_Int(rect_info, "目标值", 60, text.EndValue_Int);
                        break;
                    case XTweenTypes_Text.文字行高_LineHeight:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        text.EndValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, text.EndValue_Float);
                        break;
                    case XTweenTypes_Text.文字颜色_Color:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        text.EndValue_Color = Editor_XTween_GUI.Gui_ColorField(rect_info, "目标值", 60, text.EndValue_Color);
                        break;
                    case XTweenTypes_Text.文字内容_Content:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        text.EndValue_String = Editor_XTween_GUI.Gui_InputField_String(rect_info, "目标值", 60, text.EndValue_String);
                        break;
                }

                if (text.UseFromMode)
                {
                    switch (text.TextType)
                    {
                        case XTweenTypes_Text.文字尺寸_FontSize:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            text.FromValue_Int = Editor_XTween_GUI.Gui_InputField_Int(rect_info, "起始值", 60, text.FromValue_Int);
                            break;
                        case XTweenTypes_Text.文字行高_LineHeight:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            text.FromValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, text.FromValue_Float);
                            break;
                        case XTweenTypes_Text.文字颜色_Color:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            text.FromValue_Color = Editor_XTween_GUI.Gui_ColorField(rect_info, "起始值", 60, text.FromValue_Color);
                            break;
                        case XTweenTypes_Text.文字内容_Content:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            text.FromValue_String = Editor_XTween_GUI.Gui_InputField_String(rect_info, "起始值", 60, text.FromValue_String);
                            break;
                    }
                }
            }

            if (pre is XTweenPreset_TmpText tmp)
            {
                switch (tmp.TmpTextType)
                {
                    case XTweenTypes_TmpText.文字尺寸_FontSize:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        tmp.EndValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, tmp.EndValue_Float);
                        break;
                    case XTweenTypes_TmpText.文字行高_LineHeight:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        tmp.EndValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, tmp.EndValue_Float);
                        break;
                    case XTweenTypes_TmpText.文字颜色_Color:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        tmp.EndValue_Color = Editor_XTween_GUI.Gui_ColorField(rect_info, "目标值", 60, tmp.EndValue_Color);
                        break;
                    case XTweenTypes_TmpText.文字内容_Content:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        tmp.EndValue_String = Editor_XTween_GUI.Gui_InputField_String(rect_info, "目标值", 60, tmp.EndValue_String);
                        break;
                    case XTweenTypes_TmpText.文字边距_Margin:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                        tmp.EndValue_Vector4 = Editor_XTween_GUI.Gui_InputField_Vector4(rect_info, "目标值", 60, tmp.EndValue_Vector4);
                        break;
                }

                if (tmp.UseFromMode)
                {
                    switch (tmp.TmpTextType)
                    {
                        case XTweenTypes_TmpText.文字尺寸_FontSize:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            tmp.FromValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, tmp.FromValue_Float);
                            break;
                        case XTweenTypes_TmpText.文字行高_LineHeight:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            tmp.FromValue_Float = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, tmp.FromValue_Float);
                            break;
                        case XTweenTypes_TmpText.文字颜色_Color:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            tmp.FromValue_Color = Editor_XTween_GUI.Gui_ColorField(rect_info, "起始值", 60, tmp.FromValue_Color);
                            break;
                        case XTweenTypes_TmpText.文字内容_Content:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                            tmp.FromValue_String = Editor_XTween_GUI.Gui_InputField_String(rect_info, "起始值", 60, tmp.FromValue_String);
                            break;
                        case XTweenTypes_TmpText.文字边距_Margin:
                            rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.2f, 218, 18);
                            tmp.FromValue_Vector4 = Editor_XTween_GUI.Gui_InputField_Vector4(rect_info, "起始值", 60, tmp.FromValue_Vector4);
                            break;
                    }
                }
            }

            if (pre is XTweenPreset_Alpha alpha)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                alpha.EndValue = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "目标值", 60, alpha.EndValue);

                if (alpha.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                    alpha.FromValue = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "起始值", 60, alpha.FromValue);
                }
            }

            if (pre is XTweenPreset_Shake shake)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.3f, 218, 18);
                string[] shaketype_names = Enum.GetNames(typeof(XTweenTypes_Shakes));
                int shaketype_index = (int)shake.ShakeType;
                shake.ShakeType = (XTweenTypes_Shakes)Editor_XTween_GUI.Gui_Popup(rect_info, "抖动类型", 60, new Vector2(0, -2), Font_Light, shaketype_index, shaketype_names, XTweenGUIFilled.实体, XTweenGUIColor.亮白, XTween_Dashboard.Theme_Primary, Color.black);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.3f, 218, 18);
                shake.FadeShake = Editor_XTween_GUI.Gui_Toggle(rect_info, "抖动过渡", Font_Light, 60, new Vector2(0, -3), false, new string[2] { "禁用", "启用" }, pre.IsFavourite, XTweenGUIFilled.边框, XTweenGUIColor.亮白, XTweenGUIFilled.实体, Color.white, Color.white * 0.8f, Color.black);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 18.5f, 218, 18);
                shake.Vibrato = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "抖动幅度", 60, shake.Vibrato);

                rect_info.Set(rect.x + startoff, rect.y + start + offset * 19.5f, 218, 18);
                shake.Randomness = Editor_XTween_GUI.Gui_InputField_Float(rect_info, "随机化", 60, shake.Randomness);

                switch (shake.ShakeType)
                {
                    case XTweenTypes_Shakes.位置_Position:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 20.5f, 218, 18);
                        shake.Strength_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, shake.Strength_Vector3);
                        break;
                    case XTweenTypes_Shakes.旋转_Rotation:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 20.5f, 218, 18);
                        shake.Strength_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, shake.Strength_Vector3);
                        break;
                    case XTweenTypes_Shakes.缩放_Scale:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 20.5f, 218, 18);
                        shake.Strength_Vector3 = Editor_XTween_GUI.Gui_InputField_Vector3(rect_info, "目标值", 60, shake.Strength_Vector3);
                        break;
                    case XTweenTypes_Shakes.尺寸_Size:
                        rect_info.Set(rect.x + startoff, rect.y + start + offset * 20.5f, 218, 18);
                        shake.Strength_Vector2 = Editor_XTween_GUI.Gui_InputField_Vector2(rect_info, "目标值", 60, shake.Strength_Vector2);
                        break;
                }
            }

            if (pre is XTweenPreset_Color col)
            {
                rect_info.Set(rect.x + startoff, rect.y + start + offset * 16.5f, 218, 18);
                col.EndValue = Editor_XTween_GUI.Gui_ColorField(rect_info, "目标值", 60, col.EndValue);

                if (col.UseFromMode)
                {
                    rect_info.Set(rect.x + startoff, rect.y + start + offset * 17.5f, 218, 18);
                    col.FromValue = Editor_XTween_GUI.Gui_ColorField(rect_info, "起始值", 60, col.FromValue);
                }
            }

            // 按钮 - 保存修改参数
            rect_info.Set(rect.x + int_offset + ((rect.width / 2) + btn_edit_ok.width - int_dis), rect.y + rect.height - 50, btn_edit_ok.width, btn_edit_ok.height);
            if (Editor_XTween_GUI.Gui_IconButton(rect_info, btn_edit_ok, btn_edit_ok_press))
            {
                ClearFocus();

                EditorApplication.delayCall += () =>
                {
                    #region 将修改的参数更新并保存到对应的预设类
                    string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "修改预设", $"您确定要修改分类为：<color=#{XTween_Utilitys.ConvertColorToHexString(XTween_Dashboard.Theme_Primary)}> {SelectedPresetItem.type} </color>且名称为：<color=#{XTween_Utilitys.ConvertColorToHexString(XTween_Dashboard.Theme_Primary)}> {SelectedPresetItem.preset.Name} </color>的预设参数吗？此修改动作不可逆！请谨慎操作！", "暂不", "修改", 0);

                    if (res == "修改")
                    {
                        // 匹配目标预设项
                        XTweenPresetContainer con = XTween_PresetManager.preset_Container_Load(SelectedPresetItem.type);
                        for (int i = 0; i < con.Presets.Count; i++)
                        {
                            if (con.Presets[i].Name == SelectedPresetItem.preset.Name)
                            {
                                // 脱离引用克隆参数给目标预设
                                con.Presets[i] = pre.Clone();
                            }
                        }

                        // 将当前预设参数列表替换到目标Json文件中
                        XTween_PresetManager.preset_Container_Save_Replace(SelectedPresetItem.type, con.Presets);

                        // 重新载入预设到列表
                        PresetsAppendToList(con);

                        //Debug.Log("已修改：" + SelectedPresetItem.type + "    ----------->>>    " + SelectedPresetItem.preset.Name);
                        // 刷新选择中的预设以返回显示参数模式时的正确参数
                        SelectedPresetItem = new PresetItemGUIStruct()
                        {
                            isPressing = false,
                            preset = pre.Clone(),
                            type = SelectedPresetItemForEditor.type
                        };

                        // 清空修改状态
                        SelectedPresetItemForEditor = null;
                        SetEditorMode(false);

                    };
                };
                #endregion
            }
            // 按钮 - 取消修改参数
            rect_info.Set(rect.x + int_offset + ((rect.width / 2) - btn_edit_cancel.width - int_dis), rect.y + rect.height - 50, btn_edit_cancel.width, btn_edit_cancel.height);
            if (Editor_XTween_GUI.Gui_IconButton(rect_info, btn_edit_cancel, btn_edit_cancel_press))
            {
                ClearFocus();
                SetEditorMode(false);
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
            SelectionTypeMark_OriginalRectSet(new Rect(-50, -50, 0, 0));
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

            // 记录最后一次窗口的尺寸
            TweenConfigData.PresetCentralWindowSize = position.size;

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

        #region Others
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
        /// <summary>
        /// 获取预设文件的目标文件夹路径
        /// </summary>
        /// <returns>目标文件夹的完整物理路径</returns>
        private string GetPresetsTargetFolder()
        {
            string rootPath = XTween_Dashboard.Get_path_XTween_Presets_Path();
            return rootPath;
        }
        /// <summary>
        /// 从指定文件夹导入预设文件
        /// </summary>
        /// <param name="importPath">要导入的文件夹路径</param>
        /// <returns>导入是否成功</returns>
        private bool ImportPresetsFromFolder(string importPath)
        {
            if (!Directory.Exists(importPath))
            {
                Editor_XTween_GUI.Open(XTweenDialogType.错误, "XTween预设管理器消息", "导入失败", $"选择的文件夹不存在：{importPath}", "明白", 0);
                return false;
            }

            try
            {
                // 获取目标文件夹路径
                string targetFolder = GetPresetsTargetFolder();

                // 确保目标文件夹存在
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // 获取所有JSON文件
                string[] jsonFiles = Directory.GetFiles(importPath, "*.json");

                if (jsonFiles.Length == 0)
                {
                    Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "导入失败", $"选择的文件夹中没有找到JSON文件：{importPath}", "明白", 0);
                    return false;
                }

                int successCount = 0;
                int failCount = 0;
                List<string> importedFiles = new List<string>();

                foreach (string jsonFile in jsonFiles)
                {
                    try
                    {
                        // 读取JSON内容
                        string jsonContent = File.ReadAllText(jsonFile);

                        // 解析JSON以验证格式并获取类型信息
                        var container = JsonUtility.FromJson<XTweenPresetContainer>(jsonContent);

                        if (container == null || string.IsNullOrEmpty(container.Type))
                        {
                            Debug.LogWarning($"跳过无效的预设文件: {Path.GetFileName(jsonFile)}");
                            failCount++;
                            continue;
                        }

                        // 从类型字符串中提取文件名（如 "透明度_Alpha" -> "alpha"）
                        XTweenTypes t = (XTweenTypes)Enum.Parse(typeof(XTweenTypes), container.Type);
                        string fileName = XTween_PresetManager.GetFileNameFromType(t);

                        // 构建目标文件路径
                        string targetPath = Path.Combine(targetFolder, $"xtween_presets_{fileName}.json");

                        // 写入文件（覆盖已存在的文件）
                        File.WriteAllText(targetPath, jsonContent);

                        importedFiles.Add(targetPath);
                        successCount++;

                        XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已导入预设文件: xtween_presets_{fileName}.json", XTweenGUIMsgState.确认);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"导入文件失败 {Path.GetFileName(jsonFile)}: {e.Message}");
                        failCount++;
                    }
                }

                // 刷新AssetDatabase，使新文件在编辑器中可见
                AssetDatabase.Refresh();

                // 重新检查所有预设文件（确保文件结构完整）
                XTween_PresetManager.preset_JsonFile_Checker();

                // 显示导入结果
                string resultMsg = $"导入完成！成功：{successCount} 个，失败：{failCount} 个";

                if (failCount > 0)
                {
                    Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "导入结果", resultMsg + " 部分文件导入失败，请查看控制台日志。", "明白", 0);
                }
                else
                {
                    Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "导入成功", resultMsg, "明白", 0);
                }

                XTween_Utilitys.DebugInfo("XTween预设管理器消息", resultMsg, failCount > 0 ? XTweenGUIMsgState.警告 : XTweenGUIMsgState.确认);

                return successCount > 0;
            }
            catch (Exception e)
            {
                Debug.LogError($"导入预设失败: {e.Message}");
                Editor_XTween_GUI.Open(XTweenDialogType.错误, "XTween预设管理器消息", "导入失败", $"导入过程中发生错误： {e.Message}", "明白", 0);
                return false;
            }
        }
        /// <summary>
        /// 清空所有预设文件的内容
        /// </summary>
        /// <returns>是否全部清空成功</returns>
        private bool ClearAllPresetFiles()
        {
#if UNITY_EDITOR
            try
            {
                // 获取所有预设类型
                XTweenTypes[] allTypes = new XTweenTypes[]
                {
            XTweenTypes.透明度_Alpha,
            XTweenTypes.原生动画_To,
            XTweenTypes.路径_Path,
            XTweenTypes.位置_Position,
            XTweenTypes.旋转_Rotation,
            XTweenTypes.缩放_Scale,
            XTweenTypes.尺寸_Size,
            XTweenTypes.震动_Shake,
            XTweenTypes.颜色_Color,
            XTweenTypes.填充_Fill,
            XTweenTypes.平铺_Tiled,
            XTweenTypes.文字_Text,
            XTweenTypes.文字_TmpText
                };

                int successCount = 0;
                int failCount = 0;

                foreach (XTweenTypes type in allTypes)
                {
                    try
                    {
                        // 创建空的预设容器
                        XTweenPresetContainer emptyContainer = new XTweenPresetContainer
                        {
                            Type = type.ToString(),
                            Presets = new List<XTweenPresetBase>()
                        };

                        // 序列化为JSON
                        string jsonContent = JsonUtility.ToJson(emptyContainer, true);

                        // 保存到文件
                        string fileName = XTween_PresetManager.GetFileNameFromType(type);
                        string fullPath = XTween_PresetManager.GetPresetFilePath(fileName);

                        // 确保目录存在
                        string directory = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        // 写入文件
                        File.WriteAllText(fullPath, jsonContent);

                        successCount++;

                        XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已清空预设文件: xtween_presets_{fileName}.json", XTweenGUIMsgState.确认);
                    }
                    catch (Exception e)
                    {
                        failCount++;
                        Debug.LogError($"清空预设文件失败 {type}: {e.Message}");
                    }
                }

                // 刷新AssetDatabase
                AssetDatabase.Refresh();

                // 输出结果
                string resultMsg = $"清空预设完成！成功：{successCount} 个，失败：{failCount} 个";
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", resultMsg, failCount > 0 ? XTweenGUIMsgState.警告 : XTweenGUIMsgState.确认);

                return failCount == 0;
            }
            catch (Exception e)
            {
                Editor_XTween_GUI.Open(XTweenDialogType.错误, "XTween预设管理器消息", "清空失败", $"清空过程中发生错误：\n{e.Message}", "明白", 0);
                return false;
            }
#else
    return false;
#endif
        }
        #endregion

        #region Apply
        /// <summary>
        /// 将预设应用到动画控制器上
        /// </summary>
        /// <param name="preset"></param>
        public static void ApplyToController(XTweenPresetBase preset)
        {
            GameObject[] objs = Selection.gameObjects;

            if (objs.Length <= 0)
            {
                string cdr = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "无效应用", $"请先至少选中一个动画控制器物体再应用预设！", "明白", 0);
                return;
            }

            for (int i = 0; i < objs.Length; i++)
            {
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已应用预设 '{preset.Name}' 到动画控制器！", XTweenGUIMsgState.确认);
                XTween_Controller con = objs[i].GetComponent<XTween_Controller>();

                Undo.RecordObject(con, $"Apply preset for {con.gameObject.GetInstanceID()}");
                con.preset_Apply_To_Controller(preset);
            }
        }
        #endregion

        #region  MenuAction
        /// <summary>
        /// 编辑预设
        /// </summary>
        /// <param name="userData"></param>
        private void OnEditPreset(object userData)
        {
            PresetItemGUIStruct pret = userData as PresetItemGUIStruct;
            if (pret != null)
            {
                Debug.Log($"编辑预设: {pret.preset.Name}");
            }
            SetEditorMode(true);
        }
        /// <summary>
        /// 应用预设
        /// </summary>
        /// <param name="userData"></param>
        private void OnApplyPreset(object userData)
        {
            PresetItemGUIStruct pret = userData as PresetItemGUIStruct;
            if (pret != null)
            {
                ApplyToController(pret.preset);
            }
        }
        /// <summary>
        /// 删除预设
        /// </summary>
        /// <param name="userData"></param>
        private void OnDeletePreset(object userData)
        {
            PresetItemGUIStruct pret = userData as PresetItemGUIStruct;
            if (pret != null)
            {
                EditorApplication.delayCall += () =>
                {
                    string cdr = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTween预设管理器消息", "删除预设", $"确定要删除预设 '{pret.preset.Name}' 吗？此操作不可逆！请谨慎操作！", "删除", "暂不", 1);

                    // 确认删除
                    if (cdr == "删除")
                    {
                        // 先获取基础参数
                        XTweenTypes pre_type = pret.type;
                        string pre_name = pret.preset.Name;

                        // 从列表中移除项
                        loadedpresets.Remove(pret);

                        //Debug.Log(pre_type + "/" + pre_name);

                        // 从json中移除
                        XTween_PresetManager.preset_Container_DeletePreset(pre_type, pre_name);

                        // ✅ 重新加载当前类型的预设列表
                        if (!isFavouriteMode)
                        {
                            XTweenPresetContainer container = XTween_PresetManager.preset_Container_Load(pre_type);
                            PresetsAppendToList(container);
                        }
                        else
                        {
                            // 如果是星标模式，重新加载所有星标预设
                            OpenFavouritePresets();
                        }


                        if (SelectedPresetItem == pret)
                            SelectedPresetItem = null;
                        Repaint();
                    }
                };
            }
        }
        /// <summary>
        /// 预设星标标记操作
        /// </summary>
        /// <param name="userData"></param>
        private void OnToggleFavourite(object userData)
        {
            PresetItemGUIStruct pret = userData as PresetItemGUIStruct;
            if (pret != null)
            {
                pret.preset.IsFavourite = !pret.preset.IsFavourite;

                if (isFavouriteMode)
                {
                    //// 刷新星标列表
                    //OpenFavouritePresets();
                }
                Repaint();
            }
        }
        /// <summary>
        /// 设置编辑器模式
        /// </summary>
        /// <param name="state"></param>
        private void SetEditorMode(bool state)
        {
            IsEditorMode = state;

            if (state)
            {
                if (SelectedPresetItem != null)
                {
                    SelectedPresetItemForEditor = SelectedPresetItem.Clone();
                }
            }
            else
            {
                SelectedPresetItemForEditor = null;
            }
        }
        #endregion

        #region WindowSizeStatus
        private bool isSize_HideInfo()
        {
            if (position.width < 927)
                return true;
            else
                return false;
        }
        private bool isSize_HideSearchWidth()
        {
            if (position.width >= 635)
                return true;
            else
                return false;
        }

        #endregion

        private void OnDestroy()
        {
            SaveConfig();
        }
    }
}
