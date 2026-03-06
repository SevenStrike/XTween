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
    using System.ComponentModel;
    using System.IO;
    using UnityEditor;
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
        Font Font_Bold;
        /// <summary>
        /// 字体 - 细体
        /// </summary>
        Font Font_Light;

        /// <summary>
        /// 图标
        /// </summary>
        private Texture2D logo, referbg, sep, selectionMark, btn_edit, btn_edit_press, btn_favourite, btn_favourite_press, btn_apply, btn_apply_press;

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
        float middle_width_margin_right = 230;
        float middle_height_margin_bottom = 20;
        Rect middle_rect;
        float scroll_item_height = 60;
        float scroll_item_distance = 3;
        float scroll_btn_size = 20;
        private Vector2 scrollPosition;
        private Rect scrollViewRect;
        private Rect viewRect;
        private List<PresetItemGUIStruct> loadedpresets = new List<PresetItemGUIStruct>();
        private XTweenTypes lastXtweenType;
        private PresetItemGUIStruct SelectedPresetItem;
        bool isFavouriteMode;
        #endregion

        #region right
        float right_width = 220;
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

            EditorApplication.delayCall += () =>
            {
                XTweenTypes xt = (XTweenTypes)XTweenTypeFromString(TweenConfigData.PresetSelectionMark_LastTypeName);
                XTweenPresetContainer container = LoadPresetsContainer(xt);
                PresetsAppendToList(container);
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

            #region 抬头
            Icon_rect = new Rect(15, 15, 48, 48);
            Editor_XTween_GUI.Gui_Icon(Icon_rect, logo);

            Title_rect = new Rect(rect.x + 85, rect.y + 15, rect.width - 80, 30);
            Editor_XTween_GUI.Gui_Labelfield(Title_rect, "XTween 预设中心", XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 20, Font_Bold);

            Sepline_rect = new Rect(rect.x + 85, rect.y + 60, 200, 1);
            Editor_XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);
            #endregion

            #region Favourite按钮
            Rect fav_rect = new Rect(rect.x + 380, rect.y + 15, btn_favourite.width, btn_favourite.height);
            if (Editor_XTween_GUI.Gui_IconButton(fav_rect, btn_favourite, btn_favourite_press))
            {
                // 在进入星标模式前先保存最后一次选中的预设类型的光标坐标信息
                SelectionTypeMark_SaveConfig();

                // 明确进入星标模式
                isFavouriteMode = true;

                // 确保预设列表实例化
                if (loadedpresets == null)
                    loadedpresets = new List<PresetItemGUIStruct>();
                // 确保先清空列表
                loadedpresets.Clear();
                // 设置光标
                SelectionTypeMark_OriginalRectSet(new Rect(fav_rect.x + 18, fav_rect.y - 5, 0, 0));
                // 将所有星标预设加入列表中
                FavouritePresetsAddToList();
            }
            #endregion

            #region  left
            // left_rect 基础坐标
            left_rect = new Rect(rect.x + 5, rect.y + 75, left_width, rect.height - 80 - left_height_margin_bottom);
            //Editor_XTween_GUI.Gui_Box(left_rect, Color.red * 0.3f);

            // left 分类按钮
            Draw_TweenTypeButtons(left_rect);

            #region left_sepline
            Rect leftpanel_rect_sep = new Rect(left_rect.x + left_rect.width + 1, left_rect.y + 18, sep.width, sep.height);
            GUI.backgroundColor = Color.white * 0.6f;
            Editor_XTween_GUI.Gui_Icon(leftpanel_rect_sep, sep);
            GUI.backgroundColor = Color.white;
            #endregion

            #region selectionmark
            Editor_XTween_GUI.Gui_Icon(selectionmark_lastSelectionRect, selectionMark);
            #endregion

            #endregion

            #region middle
            // middle_rect 基础坐标
            middle_rect = new Rect(left_rect.x + middle_strartpos, left_rect.y, rect.width - middle_width_margin_right - middle_strartpos, rect.height - 80 - middle_height_margin_bottom);
            //Editor_XTween_GUI.Gui_Box(middle_rect, Color.green * 0.3f);
            Draw_PresetsScrollView(middle_rect);
            #endregion

            #region right
            // right_rect 基础坐标
            right_rect = new Rect(rect.width - right_width, rect.y + 10, right_width - 10, rect.height - right_height_margin_bottom - 15);
            //Editor_XTween_GUI.Gui_Box(right_rect, Color.red * 0.3f);

            #endregion
        }

        #region 绘制类
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

        private void Draw_PresetsScrollView(Rect rect)
        {
            // 定义滚动视图的区域
            scrollViewRect = new Rect(rect.x, rect.y + 25, rect.width, rect.height - 35);

            // 定义内容区域的大小
            viewRect = new Rect(0, 0, scrollViewRect.width - 20, loadedpresets.Count * 25);

            // 开始滚动视图
            scrollPosition = GUI.BeginScrollView(scrollViewRect, scrollPosition, viewRect);
            // 获取当前事件
            Event e = Event.current;

            // 在内容区域内绘制项目
            for (int i = 0; i < loadedpresets.Count; i++)
            {
                Rect itemRect = new Rect(5, i * (scroll_item_height + scroll_item_distance), viewRect.width - 10, scroll_item_height);
                Rect itemRect_clicked = new Rect(5, i * (scroll_item_height + scroll_item_distance), viewRect.width - 210, scroll_item_height);

                // 检测鼠标点击
                if (itemRect_clicked.Contains(e.mousePosition))
                {
                    if (e.type == EventType.MouseDown && e.button == (int)MouseButton.LeftMouse)
                    {
                        SelectedPresetItem = loadedpresets[i];
                        loadedpresets[i].isPressing = true;
                        GUI.backgroundColor = Color.red;
                        e.Use();
                        Repaint();
                    }
                    if (e.type == EventType.MouseUp && e.button == (int)MouseButton.LeftMouse)
                    {
                        loadedpresets[i].isPressing = false;
                        GUI.backgroundColor = Color.white;
                        e.Use();
                        Repaint();
                    }
                    if (e.type == EventType.MouseDrag && e.button == (int)MouseButton.LeftMouse)
                    {
                        loadedpresets[i].isPressing = false;
                        GUI.backgroundColor = Color.white;
                        e.Use();
                        Repaint();
                    }
                }

                if (loadedpresets[i].isPressing)
                    Editor_XTween_GUI.Gui_Box_Style(itemRect, XTweenGUIFilled.实体, XTween_Dashboard.Theme_Primary);
                else
                    Editor_XTween_GUI.Gui_Box_Style(itemRect, XTweenGUIFilled.实体, XTween_Utilitys.ConvertHexStringToColor("1e1e1e"));

                // 绘制标签
                Rect rect_title = new Rect(itemRect.x + 26, itemRect.y + 5, 100, 25);
                Editor_XTween_GUI.Gui_Labelfield(rect_title, loadedpresets[i].preset.Name, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white, TextAnchor.MiddleLeft, 13, Font_Bold);

                Rect rect_des = new Rect(itemRect.x + 26, itemRect.y + itemRect.height - 25 - 3, 100, 25);
                Editor_XTween_GUI.Gui_Labelfield(rect_des, loadedpresets[i].preset.Description, XTweenGUIFilled.无, XTweenGUIColor.无, Color.white * 0.65f, TextAnchor.MiddleLeft, 11, Font_Light);

                float offset = 20;

                Rect rect_btn_edit = new Rect(itemRect.width - 160 - offset, itemRect.y + 12, btn_edit.width, btn_edit.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_edit, btn_edit, btn_edit_press))
                {

                }

                Rect rect_btn_apply = new Rect(itemRect.width - 100 - offset, itemRect.y + 12, btn_apply.width, btn_apply.height);
                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_apply, btn_apply, btn_apply_press))
                {

                }

                Rect rect_btn_favo = new Rect(itemRect.width - 40 - offset, itemRect.y + 12, btn_favourite.width, btn_favourite.height);


                if (Editor_XTween_GUI.Gui_IconButton(rect_btn_favo,
                    loadedpresets[i].preset.IsFavourite ? btn_favourite : btn_favourite_press,
                    loadedpresets[i].preset.IsFavourite ? btn_favourite : btn_favourite_press))
                {
                    loadedpresets[i].preset.IsFavourite = !loadedpresets[i].preset.IsFavourite;

                    LastTweenPresetsSaved();
                }
            }

            GUI.EndScrollView();
        }
        #endregion

        #region 按钮逻辑类
        /// <summary>
        /// 点击分类预设按钮的逻辑
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type"></param>
        private void TweenTypeBtnClickedEvent(Rect o, string type)
        {
            #region  检测是否重复点击同类型按钮
            if (tweentypes_lastSelectionName == type)
                return;

            // 保存上一次的预设参数
            LastTweenPresetsSaved();

            isFavouriteMode = false;

            // 将新的动画预设类型赋值
            tweentypes_lastSelectionName = type;
            #endregion

            // 将字符串解析成XTweenTypes枚举类
            XTweenTypes xt = (XTweenTypes)XTweenTypeFromString(type);

            // 读取指定枚举类型的预设容器
            XTweenPresetContainer container = LoadPresetsContainer(o, xt);
            PresetsAppendToList(container);
        }
        #endregion       

        #region 辅助
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
                Debug.Log(container.Type);
                PresetItemGUIStruct strc = new PresetItemGUIStruct();
                strc.isPressing = false;
                //strc.type = container.Type;
                strc.preset = container.Presets[i];
                if (strc.preset.IsFavourite)
                    loadedpresets.Add(strc);
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
        /// <summary>
        /// 设置最后一次点击的类型保存配置
        /// </summary>
        private void SelectionTypeMark_SaveConfig()
        {
            if (isFavouriteMode)
                return;
            // 记录最后一次选择的预设名称
            TweenConfigData.PresetSelectionMark_LastTypeName = tweentypes_lastSelectionName;
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

        private void OnDestroy()
        {
            SelectionTypeMark_SaveConfig();
        }
    }
}
