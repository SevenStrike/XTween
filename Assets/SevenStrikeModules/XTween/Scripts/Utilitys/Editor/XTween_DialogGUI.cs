namespace SevenStrikeModules.XTween
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public enum XTweenDialogButtonMode
    {
        /// <summary>
        /// 一个按钮
        /// </summary>
        单个按钮 = 0,
        /// <summary>
        /// 两个按钮
        /// </summary>
        两个按钮 = 1,
        /// <summary>
        /// 三个按钮
        /// </summary>
        三个按钮 = 2,
        /// <summary>
        /// 四个按钮
        /// </summary>
        四个按钮 = 3,
        /// <summary>
        /// 五个按钮
        /// </summary>
        五个按钮 = 4
    }

    public enum XTweenDialogType
    {
        /// <summary>
        /// 通知
        /// </summary>
        通知 = 0,
        /// <summary>
        /// 确认
        /// </summary>
        确认 = 1,
        /// <summary>
        /// 修改
        /// </summary>
        修改 = 2,
        /// <summary>
        /// 警告
        /// </summary>
        警告 = 3,
        /// <summary>
        /// 错误
        /// </summary>
        错误 = 4,
        /// <summary>
        /// 帮助
        /// </summary>
        帮助 = 5
    }

    [System.Serializable]
    public class XTweenDialogListDatas
    {
        [SerializeField]
        /// <summary>
        /// 列表项标题 位于列表项开头
        /// </summary>
        public string Title;
        [SerializeField]
        /// <summary>
        /// 列表项子标题 位于列表项中间
        /// </summary>
        public string SubTitle;
        [SerializeField]
        /// <summary>
        /// 列表项消息内容 位于列表项最后
        /// </summary>
        public string Message;
        [SerializeField]
        /// <summary>
        /// 目标源物体
        /// </summary>
        public UnityEngine.Object SourceObject;

        public XTweenDialogListDatas()
        {

        }

        /// <summary>
        /// 设置对话框数据列表项内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="message"></param>
        public XTweenDialogListDatas(string title, string subTitle, string message)
        {
            Title = title;
            SubTitle = subTitle;
            Message = message;
        }
    }

    public class XTween_DialogGUI : EditorWindow
    {
        private SerializedObject BaseObject;
        private SerializedProperty
            sp_DataList;

        public XTweenDialogButtonMode XTweenDialogButtonMode;
        public XTweenDialogType XTweenDialogMode;

        /// <summary>
        /// 数据列表
        /// </summary>
        public ReorderableList ReorderableList;
        /// <summary>
        /// Editor列表项高度
        /// </summary>
        public float itemHeight = 28;
        /// <summary>
        /// 可视区域显示的元素数量
        /// </summary>
        public int visibleItemCount = 8;
        /// <summary>
        /// 列表滚动位置
        /// </summary>
        public Vector2 DataList_Scroller;
        /// <summary>
        /// 选中项索引号
        /// </summary>
        public int SelectedIndex;
        [SerializeField]
        public List<XTweenDialogListDatas> DataList = new List<XTweenDialogListDatas>();

        /// <summary>
        /// 窗口内容
        /// </summary>
        public string Title = "";
        /// <summary>
        /// 窗口内容
        /// </summary>
        public string Message = "";
        /// <summary>
        /// 主要按钮索引
        /// </summary>
        private int PrimaryIndex = 0;
        /// <summary>
        /// 按钮文本 - 确认
        /// </summary>
        public string Text_Ok = "";
        /// <summary>
        /// 按钮文本 - 取消
        /// </summary>
        public string Text_Cancel = "";
        /// <summary>
        /// 按钮文本 - 确认
        /// </summary>
        public string Text_Alt = "";
        /// <summary>
        /// 按钮文本 - 其他
        /// </summary>
        public string Text_Other = "";
        /// <summary>
        /// 按钮文本 - 特别
        /// </summary>
        public string Text_Special = "";

        /// <summary>
        /// 图片
        /// </summary>
        Texture2D Texture;

        /// <summary>
        /// 图标 - 帮助
        /// </summary>
        Texture2D icon_Help;
        /// <summary>
        /// 图标 - 警告
        /// </summary>
        Texture2D icon_Warning;
        /// <summary>
        /// 图标 - 错误
        /// </summary>
        Texture2D icon_Error;
        /// <summary>
        /// 图标 - 修改
        /// </summary>
        Texture2D icon_Modified;
        /// <summary>
        /// 图标 - 通知
        /// </summary>
        Texture2D icon_Notice;
        /// <summary>
        /// 图标 - 确认
        /// </summary>
        Texture2D icon_Confirm;

        /// <summary>
        /// 字体 - 粗体
        /// </summary>
        Font Font_Bold;
        /// <summary>
        /// 字体 - 细体
        /// </summary>
        Font Font_Light;

        /// <summary>
        /// 回调函数 - Ok
        /// </summary>
        public Action<string> Callback_Ok;
        /// <summary>
        /// 回调函数 - Cancel
        /// </summary>
        public Action<string> Callback_Cancel;
        /// <summary>
        /// 回调函数 - Alt
        /// </summary>
        public Action<string> Callback_Alt;
        /// <summary>
        /// 回调函数 - Other
        /// </summary>
        public Action<string> Callback_Other;
        /// <summary>
        /// 回调函数 - Special
        /// </summary>
        public Action<string> Callback_Special;

        /// <summary>
        /// 日期时间
        /// </summary>
        string DateTimes;

        Color SepLineColor = new Color(1, 1, 1, 0.15f);
        Color MessageColor = new Color(1, 1, 1, 1f);
        Color DateTimeColor = new Color(1, 1, 1, 0.42f);

        Rect Sepline_rect;
        Rect Title_rect;
        Rect Date_rect;
        Rect Icon_rect;
        Rect TotalCount_rect;

        /// <summary>
        /// 按钮宽度
        /// </summary>
        private float ButtonWidth = 90;
        /// <summary>
        /// 按钮高度
        /// </summary>
        private float ButtonHeight = 25;
        /// <summary>
        /// 按钮间距
        /// </summary>
        private float ButtonDistance = 5;

        private void OnEnable()
        {
            BaseObject = new SerializedObject(this);

            sp_DataList = BaseObject.FindProperty("DataList");

            icon_Help = XTween_GUI.GetIcon("Dialogs/icon_help");
            icon_Warning = XTween_GUI.GetIcon("Dialogs/icon_warning");
            icon_Error = XTween_GUI.GetIcon("Dialogs/icon_error");
            icon_Modified = XTween_GUI.GetIcon("Dialogs/icon_modified");
            icon_Notice = XTween_GUI.GetIcon("Dialogs/icon_notice");
            icon_Confirm = XTween_GUI.GetIcon("Dialogs/icon_confirm");

            Font_Bold = XTween_GUI.GetFont("SS_Editor_Bold");
            Font_Light = XTween_GUI.GetFont("SS_Editor_Dialog");

            #region ReorderableList
            ReorderableList = new ReorderableList(BaseObject, sp_DataList, true, true, true, true);
            ReorderableList.drawElementCallback = DrawElementCallback;
            #endregion
        }

        private void OnGUI()
        {
            Rect rect = new Rect(0, 0, position.width, position.height);

            Icon_rect = new Rect(26, 15, 48, 48);

            switch (XTweenDialogMode)
            {
                case XTweenDialogType.通知:
                    XTween_GUI.Gui_Icon(Icon_rect, icon_Notice);
                    break;
                case XTweenDialogType.确认:
                    XTween_GUI.Gui_Icon(Icon_rect, icon_Confirm);
                    break;
                case XTweenDialogType.修改:
                    XTween_GUI.Gui_Icon(Icon_rect, icon_Modified);
                    break;
                case XTweenDialogType.警告:
                    XTween_GUI.Gui_Icon(Icon_rect, icon_Warning);
                    break;
                case XTweenDialogType.错误:
                    XTween_GUI.Gui_Icon(Icon_rect, icon_Error);
                    break;
                case XTweenDialogType.帮助:
                    XTween_GUI.Gui_Icon(Icon_rect, icon_Help);
                    break;
            }

            Title_rect = new Rect(rect.x + 100, rect.y + 15, rect.width - 80, 30);
            XTween_GUI.Gui_Labelfield(Title_rect, Title, GUIFilled.无, GUIColor.无, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 20, Font_Bold);

            Sepline_rect = new Rect(rect.x + 102, rect.y + 60, 200, 1);
            XTween_GUI.Gui_Box(Sepline_rect, SepLineColor);

            XTween_GUI.Gui_Labelfield_Thin_WrapClip(new Rect(rect.x + 26, rect.y + 80, rect.width - 45, rect.height), Message, GUIFilled.无, GUIColor.无, MessageColor, TextAnchor.UpperLeft, new Vector2(0, 0), 12, true, Font_Light);

            DateTimes = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss:ff");
            Date_rect = new Rect(rect.x + 150, rect.y + 15, rect.width - 180, rect.height);
            XTween_GUI.Gui_Labelfield_Thin_WrapClip(Date_rect, DateTimes, GUIFilled.无, GUIColor.无, DateTimeColor, TextAnchor.UpperRight, new Vector2(0, 0), 13, true, Font_Light);

            BaseObject.Update();

            #region 列表
            if (DataList.Count > 0)
            {
                TotalCount_rect = new Rect(rect.x + 26, rect.height - 52, 80, 30);
                XTween_GUI.Gui_Labelfield(TotalCount_rect, DataList.Count.ToString(), GUIFilled.无, GUIColor.无, XTween_Dashboard.Theme_Primary, TextAnchor.MiddleLeft, Vector2.zero, 17, Font_Bold);

                XTween_GUI.Gui_Labelfield(new Rect(TotalCount_rect.x + 30, TotalCount_rect.y + 1, 20, TotalCount_rect.height), " 项", GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 14, Font_Bold);

                XTween_GUI.Gui_Layout_Space(130);
                DrawDataList();
                XTween_GUI.Gui_Layout_Space(26);
            }
            else
            {
                XTween_GUI.Gui_Layout_Space(185);
            }
            #endregion

            BaseObject.ApplyModifiedProperties();

            string FocusName = Text_Ok + PrimaryIndex;

            switch (PrimaryIndex)
            {
                case 0:
                    FocusName = Text_Ok + PrimaryIndex;
                    break;
                case 1:
                    FocusName = Text_Cancel + PrimaryIndex;
                    break;
                case 2:
                    FocusName = Text_Alt + PrimaryIndex;
                    break;
                case 3:
                    FocusName = Text_Other + PrimaryIndex;
                    break;
                case 4:
                    FocusName = Text_Special + PrimaryIndex;
                    break;
            }

            EditorGUI.FocusTextInControl(FocusName);

            XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            XTween_GUI.Gui_Layout_FlexSpace();
            switch (XTweenDialogButtonMode)
            {
                case XTweenDialogButtonMode.单个按钮:
                    DialogType_BtnMode_1();
                    break;
                case XTweenDialogButtonMode.两个按钮:
                    DialogType_BtnMode_2();
                    break;
                case XTweenDialogButtonMode.三个按钮:
                    DialogType_BtnMode_3();
                    break;
                case XTweenDialogButtonMode.四个按钮:
                    DialogType_BtnMode_4();
                    break;
                case XTweenDialogButtonMode.五个按钮:
                    DialogType_BtnMode_5();
                    break;
            }
            XTween_GUI.Gui_Layout_Space(25);
            XTween_GUI.Gui_Layout_Horizontal_End();
            Repaint();
        }

        private void OnDisable()
        {

        }

        private void OnDestroy()
        {
            GetWindow<SceneView>().Focus();
        }

        #region ColorInfoList_Original      

        Rect index_rect;
        Rect msg_rect;
        Rect title_rect;
        Rect sub_rect;
        Rect scrollview_rect;
        Rect item_rect;
        Rect itemmark_rect;
        Rect itemmarkBg_rect;

        /// <summary>
        /// 绘制元素
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="index"></param>
        /// <param name="isActive"></param>
        /// <param name="isFocused"></param>
        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            index_rect = new Rect(rect.x + 15, rect.y + 5, 30, 20);
            title_rect = new Rect(rect.x + 39, rect.y + 5, 160, 20);
            msg_rect = new Rect(rect.x + 77, rect.y + 5, rect.width - 115, 20);
            sub_rect = new Rect((rect.width / 2) - 50, rect.y + 5, 100, 20);

            SerializedProperty prop = sp_DataList.GetArrayElementAtIndex(index);
            SerializedProperty sp_title = prop.FindPropertyRelative("Title");
            SerializedProperty sp_sub = prop.FindPropertyRelative("SubTitle");
            SerializedProperty sp_msg = prop.FindPropertyRelative("Message");

            XTween_GUI.Gui_Labelfield(index_rect, index.ToString("D2"), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11);

            #region 标识名称
            XTween_GUI.Gui_Labelfield(title_rect, sp_title.stringValue, GUIFilled.无, GUIColor.无, XTween_GUI.GetColor(GUIColor.亮白), TextAnchor.MiddleLeft, Vector2.zero, 12, TextClipping.Ellipsis);
            XTween_GUI.Gui_Labelfield_Thin(sub_rect, sp_sub.stringValue, GUIFilled.无, GUIColor.无, XTween_Dashboard.Theme_Primary, TextAnchor.MiddleLeft, Vector2.zero, 12);
            XTween_GUI.Gui_Labelfield_Thin(msg_rect, sp_msg.stringValue, GUIFilled.无, GUIColor.无, XTween_Dashboard.Theme_Primary, TextAnchor.MiddleRight, Vector2.zero, 12);
            #endregion
        }

        /// <summary>
        /// 绘制列表
        /// </summary>
        private void DrawDataList()
        {
            // 绘制滚动视图
            scrollview_rect = GUILayoutUtility.GetRect(0, visibleItemCount * itemHeight);
            DataList_Scroller = GUI.BeginScrollView(scrollview_rect, DataList_Scroller, new Rect(-15, 0, scrollview_rect.width - 50, DataList.Count * itemHeight), false, true);

            // 计算可视区域的起始和结束索引
            int startIndex = Mathf.FloorToInt(DataList_Scroller.y / itemHeight);
            int endIndex = Mathf.CeilToInt((DataList_Scroller.y + scrollview_rect.height) / itemHeight);

            // 只绘制可视区域内的元素
            for (int i = startIndex; i < endIndex && i < ReorderableList.count; i++)
            {
                SerializedProperty prop = sp_DataList.GetArrayElementAtIndex(i);

                item_rect = new Rect(0, i * itemHeight, scrollview_rect.width, itemHeight);

                // 如果当前元素被选中，绘制选中效果
                if (SelectedIndex == i)
                {
                    itemmark_rect = new Rect(item_rect.x + 1, item_rect.y + 11, 5, 5);
                    // 高亮标记表示选中
                    EditorGUI.DrawRect(itemmark_rect, XTween_Dashboard.Theme_Primary);
                    itemmarkBg_rect = new Rect(item_rect.x, item_rect.y, item_rect.width + 20, item_rect.height);
                    // 高亮背景表示选中
                    EditorGUI.DrawRect(itemmarkBg_rect, new Color(0, 0, 0, 0.2f));
                }

                ReorderableList.drawElementCallback.Invoke(item_rect, i, i == ReorderableList.index, true);

                // 检测鼠标是否在当前元素区域内
                if (item_rect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.MouseDown)
                    {
                        // 更新选中项
                        SelectedIndex = i;

                        SerializedProperty prop_sourceobject = prop.FindPropertyRelative("SourceObject");
                        if (prop_sourceobject.objectReferenceValue != null)
                        {
                            EditorGUIUtility.PingObject(prop_sourceobject.objectReferenceValue);
                        }

                        // 标记界面需要更新
                        GUI.changed = true;
                    }
                }
            }

            GUI.EndScrollView();
        }

        #endregion

        /// <summary>
        ///1个按钮模式
        /// </summary>
        private void DialogType_BtnMode_1()
        {
            GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            if (XTween_GUI.Gui_Layout_Button(Text_Ok, "", GUIFilled.实体, GUIColor.亮白, XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary) ? Color.black : Color.white, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Ok + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Ok?.Invoke(Text_Ok);
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary>
        /// 2个按钮模式
        /// </summary>
        private void DialogType_BtnMode_2()
        {
            Color textcolor = XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary) ? Color.black : Color.white;

            if (PrimaryIndex == 1)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Cancel, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Cancel + PrimaryIndex))
            {
                Close();
                // 调用回调函数
                Callback_Cancel?.Invoke(Text_Cancel);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 0)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Ok, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Ok + PrimaryIndex))
            {
                Close();
                // 调用回调函数
                Callback_Ok?.Invoke(Text_Ok);
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary>
        /// 3个按钮模式
        /// </summary>
        private void DialogType_BtnMode_3()
        {
            Color textcolor = XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary) ? Color.black : Color.white;

            if (PrimaryIndex == 2)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Alt, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Alt + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Alt?.Invoke(Text_Alt);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 1)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Cancel, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Cancel + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Cancel?.Invoke(Text_Cancel);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 0)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Ok, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Ok + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Ok?.Invoke(Text_Ok);
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary>
        /// 4个按钮模式
        /// </summary>
        private void DialogType_BtnMode_4()
        {
            Color textcolor = XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary) ? Color.black : Color.white;

            if (PrimaryIndex == 3)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Other, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Other + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Other?.Invoke(Text_Other);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 2)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Alt, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Alt + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Alt?.Invoke(Text_Alt);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 1)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Cancel, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Cancel + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Cancel?.Invoke(Text_Cancel);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 0)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Ok, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Ok + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Ok?.Invoke(Text_Ok);
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary>
        /// 5个按钮模式
        /// </summary>
        private void DialogType_BtnMode_5()
        {
            Color textcolor = XTween_Utilitys.GetColorBrightnessLimite(XTween_Dashboard.Theme_Primary) ? Color.black : Color.white;

            if (PrimaryIndex == 4)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Special, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Special + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Special?.Invoke(Text_Special);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 3)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Other, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Other + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Other?.Invoke(Text_Other);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 2)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Alt, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Alt + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Alt?.Invoke(Text_Alt);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 1)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Cancel, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Cancel + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Cancel?.Invoke(Text_Cancel);
            }
            GUI.backgroundColor = Color.white;
            XTween_GUI.Gui_Layout_Space(ButtonDistance);
            if (PrimaryIndex == 0)
            {
                GUI.backgroundColor = XTween_Dashboard.Theme_Primary;
            }
            else
            {
                textcolor = Color.black;
            }
            if (XTween_GUI.Gui_Layout_Button(Text_Ok, "", GUIFilled.实体, GUIColor.亮白, textcolor, 12, ButtonWidth, ButtonHeight, Font_Light, Text_Ok + PrimaryIndex))
            {
                Close();

                // 调用回调函数
                Callback_Ok?.Invoke(Text_Ok);
            }
            GUI.backgroundColor = Color.white;
        }

        /// <summary>
        /// 设置对话框信息
        /// </summary>
        /// <param name="Type">模式</param>
        /// <param name="x_Title">面板标题</param>
        /// <param name="x_Msg">面板内容</param>
        /// <param name="x_Ok">确认文字</param>
        /// <param name="x_Cancel">取消文字</param>
        /// <param name="x_Alt">辅助文字</param>
        /// <param name="x_Other">其他文字</param>
        /// <param name="x_Special">特别文字</param>
        public void SetInfo(XTweenDialogType Type, string x_Title = "", string x_Msg = "", string x_Ok = "", string x_Cancel = "", string x_Alt = "", string x_Other = "", string x_Special = "", int x_PrimaryIndex = 0)
        {
            PrimaryIndex = x_PrimaryIndex;
            XTweenDialogMode = Type;
            Title = x_Title;
            Message = x_Msg;
            Text_Ok = x_Ok;
            Text_Cancel = x_Cancel;
            Text_Alt = x_Alt;
            Text_Other = x_Other;
            Text_Special = x_Special;
        }

        /// <summary>
        /// 设置数据列表
        /// </summary>
        /// <param name="datas"></param>
        public void SetList(XTweenDialogListDatas[] datas)
        {
            if (DataList == null)
                DataList = new List<XTweenDialogListDatas>();
            DataList.Clear();
            for (int i = 0; i < datas.Length; i++)
            {
                DataList.Add(datas[i]);
            }
        }
    }
}
