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
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    /// <summary>
    /// 自定义编辑器类，用于在 Unity 编辑器中可视化和编辑 XTween_PathTool 路径工具
    /// 提供路径点的绘制、编辑、删除以及路径的可视化功能
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(XTween_PathTool))]
    public class Editor_XTween_PathTool : Editor
    {
        /// <summary>
        /// 当前正在编辑的路径工具实例
        /// </summary>
        private XTween_PathTool BaseScript;
        /// <summary>
        /// 路径点列表
        /// </summary>
        private ReorderableList PathPoints;
        /// <summary>
        /// 序列化属性
        /// </summary>
        private SerializedProperty sp_PathPoints, sp_PathType, sp_PathOrientation, sp_PathOrientationVector, sp_LookAtObject, sp_LookAtPosition, sp_LookAtPoints, sp_IsWorldMode, sp_StartPosition, sp_PathProgress, sp_PathLength, sp_SegmentsPerCurve, sp_IsClosed, sp_DisplayPath, sp_DisplayIndex, sp_Color_Path, sp_Color_PathPoint, sp_Color_PathPoint_Selected, sp_Color_BezierControl, sp_Color_BezierControl_Selected, sp_Color_Index, sp_Color_IndexLength, sp_Color_LookAtLine, sp_ControlLineStyle, sp_PathPointSize, sp_BezierControlSize, sp_PathWidth, sp_IndexSize, sp_IndexLengthHeight, sp_IndexOffset, sp_AddedDistance, sp_LookAtLine, sp_LookAtLineWidth, sp_PathMarksTexture, sp_PathMarksSize, sp_PathMarksSample, sp_PathMarksMode, sp_PathPointsIsFold, sp_IndexPathType, sp_IndexPathOrientation, sp_IndexPathOrientationVector, sp_IndexControlLineStyle, sp_IndexPathMarkMode, sp_PathLimitePercent, sp_PathParent, sp_PathMarksColor, sp_PathMarksGroup;
        /// <summary>
        /// 图标
        /// </summary>
        private Texture2D icon_main, icon_worldmode, icon_startpos, icon_pathlength, icon_Grandparent, icon_pathpercent, icon_add_r, icon_add_p, clear_r, clear_p, repos_zero_r, repos_zero_p, createpathmarks_r, createpathmarks_p, locate, status;
        /// <summary>
        /// 当前选中的锚点索引，用于路径点的编辑
        /// </summary>
        private int selectedAnchorIndex = -1;
        /// <summary>
        /// 当前选中的控制点索引，用于贝塞尔曲线控制点的编辑
        /// </summary>
        private int selectedControlIndex = -1;
        /// <summary>
        /// GUI单行高度
        /// </summary>
        private float LineHeight;
        /// <summary>
        /// 是否正在编辑控制点
        /// </summary>
        private bool isInControl = false;
        private bool BasicVars = false;
        /// <summary>
        /// 用于绘制路径点索引的样式
        /// </summary>
        private GUIStyle IndexStyle;

        #region 批量化操作
        private XTween_PathTool[] SelectedObjects;
        private void GetAllTargets()
        {
            if (targets.Length > 1)
            {
                SelectedObjects = new XTween_PathTool[targets.Length];
                for (int i = 0; i < SelectedObjects.Length; i++)
                {
                    var t = targets[i];
                    SelectedObjects[i] = (XTween_PathTool)t;
                }
            }
            else
            {
                SelectedObjects = new XTween_PathTool[targets.Length];
                SelectedObjects[0] = (XTween_PathTool)target;
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

        /// <summary>
        /// 在编辑器启用时初始化路径工具和相关事件
        /// </summary>
        private void OnEnable()
        {
            BaseScript = (XTween_PathTool)target;
            SceneView.duringSceneGui -= DuringSceneGUI;
            SceneView.duringSceneGui += DuringSceneGUI;

            #region 获取序列化属性
            sp_PathPoints = serializedObject.FindProperty("PathPoints");
            sp_PathType = serializedObject.FindProperty("PathType");
            sp_PathOrientation = serializedObject.FindProperty("PathOrientation");
            sp_PathOrientationVector = serializedObject.FindProperty("PathOrientationVector");
            sp_LookAtObject = serializedObject.FindProperty("LookAtObject");
            sp_LookAtPosition = serializedObject.FindProperty("LookAtPosition");
            sp_LookAtPoints = serializedObject.FindProperty("LookAtPoints");
            sp_IsWorldMode = serializedObject.FindProperty("IsWorldMode");
            sp_StartPosition = serializedObject.FindProperty("StartPosition");
            sp_PathProgress = serializedObject.FindProperty("PathProgress");
            sp_PathLength = serializedObject.FindProperty("PathLength");
            sp_SegmentsPerCurve = serializedObject.FindProperty("SegmentsPerCurve");
            sp_IsClosed = serializedObject.FindProperty("IsClosed");
            sp_DisplayPath = serializedObject.FindProperty("DisplayPath");
            sp_DisplayIndex = serializedObject.FindProperty("DisplayIndex");
            sp_Color_Path = serializedObject.FindProperty("Color_Path");
            sp_Color_PathPoint = serializedObject.FindProperty("Color_PathPoint");
            sp_Color_PathPoint_Selected = serializedObject.FindProperty("Color_PathPoint_Selected");
            sp_Color_BezierControl = serializedObject.FindProperty("Color_BezierControl");
            sp_Color_BezierControl_Selected = serializedObject.FindProperty("Color_BezierControl_Selected");
            sp_Color_Index = serializedObject.FindProperty("Color_Index");
            sp_Color_IndexLength = serializedObject.FindProperty("Color_IndexLength");
            sp_Color_LookAtLine = serializedObject.FindProperty("Color_LookAtLine");
            sp_ControlLineStyle = serializedObject.FindProperty("ControlLineStyle");
            sp_PathPointSize = serializedObject.FindProperty("PathPointSize");
            sp_BezierControlSize = serializedObject.FindProperty("BezierControlSize");
            sp_PathWidth = serializedObject.FindProperty("PathWidth");
            sp_IndexSize = serializedObject.FindProperty("IndexSize");
            sp_IndexLengthHeight = serializedObject.FindProperty("IndexLengthHeight");
            sp_IndexOffset = serializedObject.FindProperty("IndexOffset");
            sp_AddedDistance = serializedObject.FindProperty("AddedDistance");
            sp_LookAtLine = serializedObject.FindProperty("LookAtLine");
            sp_LookAtLineWidth = serializedObject.FindProperty("LookAtLineWidth");
            sp_PathMarksTexture = serializedObject.FindProperty("PathMarksTexture");
            sp_PathMarksSize = serializedObject.FindProperty("PathMarksSize");
            sp_PathMarksSample = serializedObject.FindProperty("PathMarksSample");
            sp_PathMarksMode = serializedObject.FindProperty("PathMarksMode");
            sp_PathPointsIsFold = serializedObject.FindProperty("PathPointsIsFold");
            sp_IndexPathType = serializedObject.FindProperty("IndexPathType");
            sp_IndexPathOrientation = serializedObject.FindProperty("IndexPathOrientation");
            sp_IndexPathOrientationVector = serializedObject.FindProperty("IndexPathOrientationVector");
            sp_IndexControlLineStyle = serializedObject.FindProperty("IndexControlLineStyle");
            sp_IndexPathMarkMode = serializedObject.FindProperty("IndexPathMarkMode");
            sp_PathLimitePercent = serializedObject.FindProperty("PathLimitePercent");
            sp_PathParent = serializedObject.FindProperty("PathParent");
            sp_PathMarksColor = serializedObject.FindProperty("PathMarksColor");
            sp_PathMarksGroup = serializedObject.FindProperty("PathMarksGroup");
            #endregion

            #region 图标获取
            icon_main = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_main");
            icon_worldmode = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_worldmode");
            icon_pathpercent = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_pathpercent");
            icon_add_r = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_add_r");
            icon_add_p = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_add_p");
            clear_r = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/clear_r");
            clear_p = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/clear_p");
            repos_zero_r = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/repos_zero_r");
            repos_zero_p = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/repos_zero_p");
            createpathmarks_r = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/createpathmarks_r");
            createpathmarks_p = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/createpathmarks_p");
            locate = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/locate");
            status = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/status");
            icon_startpos = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_startpos");
            icon_pathlength = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_pathlength");
            icon_Grandparent = Editor_XTween_GUI.GetIcon("Icons_Hud_XTween_PathTool/icon_Grandparent");
            #endregion

            LineHeight = EditorGUIUtility.singleLineHeight;

            // 初始化索引样式
            IndexStyle = new GUIStyle();
            IndexStyle.font = AssetDatabase.LoadAssetAtPath<Font>(XTween_Dashboard.Get_XTween_Root_Path() + "Fonts/Text/SevenStrikeFont_Bold.ttf");
            GetAllTargets();

            sp_PathParent.objectReferenceValue = BaseScript.transform.parent;
            sp_PathParent.serializedObject.ApplyModifiedProperties();

            SetTransformCenter();

            #region ReorderableList - PathPoints
            PathPoints = new ReorderableList(serializedObject, sp_PathPoints)
            {
                displayAdd = false,
                displayRemove = true,
                draggable = true,

                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, "路径点信息列表");
                },
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    float titleheight = rect.y + 6;

                    SerializedProperty sp_root = sp_PathPoints.GetArrayElementAtIndex(index);
                    if (sp_root != null)
                    {
#if UNITY_6000_0_OR_NEWER
                        // Unity 6+ 使用 Ellipsis
                        TextClipping clipping = TextClipping.Ellipsis;
#else
    // Unity 2021.1 之前使用 Clip
    TextClipping clipping = TextClipping.Clip;
#endif

                        Editor_XTween_GUI.Gui_Labelfield(new Rect(rect.x + 25, titleheight - 3.5f, (rect.width * 0.45f) - 20, LineHeight), $"#  路径点 {index.ToString()}", GUIFilled.无, GUIColor.亮白, Color.white, TextAnchor.MiddleLeft, Vector2.zero, 12, clipping);

                        GUI.color = XTween_Dashboard.Theme_Primary;
                        Editor_XTween_GUI.Gui_Icon(new Rect(rect.x + 5, titleheight, 10, 10), locate);

                        GUI.color = Color.white;

                        SerializedProperty sp_relative = sp_root.FindPropertyRelative("relative");
                        SerializedProperty sp_world = sp_root.FindPropertyRelative("world");
                        SerializedProperty sp_anchored = sp_root.FindPropertyRelative("anchored");
                        SerializedProperty sp_bezier_in = sp_root.FindPropertyRelative("bezier_in");
                        SerializedProperty sp_bezier_out = sp_root.FindPropertyRelative("bezier_out");
                        SerializedProperty sp_bezier_in_world = sp_root.FindPropertyRelative("bezier_in_world");
                        SerializedProperty sp_bezier_out_world = sp_root.FindPropertyRelative("bezier_out_world");

                        string hexcol = XTween_Utilitys.ConvertColorToHexString(XTween_Dashboard.Theme_Primary, true);

                        #region 最小音高
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 20, rect.width - 15, 19), $"<color={hexcol}> -   Relative   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_relative.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 40, rect.width - 15, 19), $"<color=#c2c2c2> -   World   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_world.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 60, rect.width - 15, 19), $"<color={hexcol}> -   Anchored   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_anchored.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 80, rect.width - 15, 19), $"<color=#c2c2c2> -   Bezier_In   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_bezier_in.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 100, rect.width - 15, 19), $"<color={hexcol}> -   Bezier_Out   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_bezier_out.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 120, rect.width - 15, 19), $"<color=#c2c2c2> -   BezierWorld_In   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_bezier_in_world.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect.x + 5, titleheight + 140, rect.width - 15, 19), $"<color={hexcol}> -   BezierWorld_Out   :   </color>" + XTween_Utilitys.ConvertVector3ToString(sp_bezier_out_world.vector3Value), GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleLeft, Vector2.zero, 11, false, false, true);
                        #endregion
                    }
                },
                onSelectCallback = (ReorderableList list) =>
                {
                    selectedAnchorIndex = list.index;
                },
                elementHeightCallback = index =>
                {
                    return 9.5f * LineHeight;
                }
            };
            #endregion
        }
        /// <summary>
        /// 在编辑器禁用时移除相关事件
        /// </summary>
        private void OnDisable()
        {
            SetTransformCenter();
            SceneView.duringSceneGui -= DuringSceneGUI;
        }
        /// <summary>
        /// 在编辑器销毁时移除相关事件
        /// </summary>
        private void OnDestroy()
        {
            SceneView.duringSceneGui -= DuringSceneGUI;
        }
        /// <summary>
        /// 在 Inspector 中绘制路径工具的属性和按钮
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Editor_XTween_GUI.Gui_Layout_Banner(icon_main, GUIFilled.实体, GUIColor.深空灰, "XTween - 路径工具", Color.white, 20, 20);

            #region 快捷功能
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "快捷功能", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(10);

            Editor_XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            Editor_XTween_GUI.Gui_Layout_Space(10);
            #region 添加路径点
            GUI.enabled = true;
            if (Editor_XTween_GUI.Gui_Layout_Button(15, "添加路径点", icon_add_r, icon_add_p))
            {
                PathPoints_Add();
                return;
            }
            #endregion
            GUILayout.FlexibleSpace();
            #region 清空路径点
            GUI.enabled = true;
            if (Editor_XTween_GUI.Gui_Layout_Button(15, "清空路径点", clear_r, clear_p))
            {
                PathPoints_Clear();
                return;
            }
            #endregion
            GUILayout.FlexibleSpace();
            #region 路径点深度归零
            GUI.enabled = true;
            if (Editor_XTween_GUI.Gui_Layout_Button(15, "路径点深度归零", repos_zero_r, repos_zero_p))
            {
                PathPoints_ZAxis_ToZero();
                return;
            }
            #endregion
            GUILayout.FlexibleSpace();
            #region 生成路径标记
            GUI.enabled = true;
            if (Editor_XTween_GUI.Gui_Layout_Button(15, "生成路径标记", createpathmarks_r, createpathmarks_p))
            {
                PathMarksCreator();
                return;
            }
            #endregion
            Editor_XTween_GUI.Gui_Layout_Space(10);
            Editor_XTween_GUI.Gui_Layout_Horizontal_End();

            Editor_XTween_GUI.Gui_Layout_Space(10);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 路径列表
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "路径列表", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            #region 路径列表
            if (sp_PathPoints.arraySize > 0)
            {
                if (IsMultiSelected())
                {
                    Editor_XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
                    Editor_XTween_GUI.Gui_Layout_Space(10);
                    EditorGUILayout.HelpBox("路径列表不支持多项操作", MessageType.Warning);
                    Editor_XTween_GUI.Gui_Layout_Space(5);
                    Editor_XTween_GUI.Gui_Layout_Horizontal_End();
                }
                else
                {
                    Editor_XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
                    Editor_XTween_GUI.Gui_Layout_Space(10);
                    sp_PathPointsIsFold.boolValue = EditorGUILayout.Foldout(sp_PathPointsIsFold.boolValue, "路径", true);
                    Editor_XTween_GUI.Gui_Layout_Space(5);
                    Editor_XTween_GUI.Gui_Layout_Horizontal_End();

                    Editor_XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
                    Editor_XTween_GUI.Gui_Layout_Space(5);
                    if (sp_PathPointsIsFold.boolValue)
                        PathPoints.DoLayoutList();
                    Editor_XTween_GUI.Gui_Layout_Space(5);
                    Editor_XTween_GUI.Gui_Layout_Horizontal_End();
                }
            }
            #endregion
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 有效路径百分比
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "有效路径百分比", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(10);

            Rect rect_longpress = GUILayoutUtility.GetLastRect();

            EditorGUI.DrawRect(new Rect(rect_longpress.x + 5, rect_longpress.y + 22, (EditorGUIUtility.currentViewWidth - 65), 1), Color.black * 0.3f);

            EditorGUI.DrawRect(new Rect(rect_longpress.x + 5, rect_longpress.y + 22, (EditorGUIUtility.currentViewWidth - 65) * sp_PathLimitePercent.floatValue, 1), XTween_Dashboard.Theme_Primary);

            Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect_longpress.x + 5, rect_longpress.y + 8, 50, 6), "起点", GUIFilled.无, GUIColor.无, XTween_Dashboard.Theme_Primary, TextAnchor.MiddleLeft, new Vector2(0, 0), 9);

            Editor_XTween_GUI.Gui_Labelfield_Thin(new Rect(rect_longpress.x + (EditorGUIUtility.currentViewWidth - 110), rect_longpress.y + 8, 50, 6), "终点", GUIFilled.无, GUIColor.无, Color.gray, TextAnchor.MiddleRight, new Vector2(0, 0), 9);

            EditorGUI.DrawRect(new Rect((rect_longpress.x + 5), rect_longpress.y + 20, 1, 6), Color.gray);

            EditorGUI.DrawRect(new Rect((rect_longpress.x + (EditorGUIUtility.currentViewWidth - 60)), rect_longpress.y + 20, 1, 6), Color.gray);

            Editor_XTween_GUI.Gui_Icon(new Rect(((rect_longpress.x + 1) + (EditorGUIUtility.currentViewWidth - 65) * sp_PathLimitePercent.floatValue), rect_longpress.y + 6, 8, 8), icon_pathpercent);

            EditorGUI.DrawRect(new Rect(((rect_longpress.x + 5) + (EditorGUIUtility.currentViewWidth - 65) * sp_PathLimitePercent.floatValue), rect_longpress.y + 18, 1, 10), Color.red);


            Editor_XTween_GUI.Gui_Layout_Space(28);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 选项
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "选项", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(10);
            #region 路径样式
            string[] actionlist = System.Enum.GetNames(typeof(XTween_PathType));
            Editor_XTween_GUI.Gui_Layout_Popup<string, XTween_PathTool>("路径样式", actionlist, ref sp_IndexPathType, GUIFilled.实体, 120, 22, SelectedObjects, (comps) => { }, (res) =>
            {
                sp_PathType.enumValueIndex = (int)(XTween_PathType)System.Enum.Parse(typeof(XTween_PathType), res);
            });
            #endregion

            #region 朝向模式
            actionlist = System.Enum.GetNames(typeof(XTween_PathOrientation));
            Editor_XTween_GUI.Gui_Layout_Popup<string, XTween_PathTool>("朝向模式", actionlist, ref sp_IndexPathOrientation, GUIFilled.实体, 120, 22, SelectedObjects, (comps) => { }, (res) =>
            {
                sp_PathOrientation.enumValueIndex = (int)(XTween_PathOrientation)System.Enum.Parse(typeof(XTween_PathOrientation), res);
            });
            #endregion

            #region 朝向轴向
            if (BaseScript.PathOrientation != XTween_PathOrientation.无)
            {
                actionlist = System.Enum.GetNames(typeof(XTween_PathOrientationVector));
                Editor_XTween_GUI.Gui_Layout_Popup<string, XTween_PathTool>("朝向轴向", actionlist, ref sp_IndexPathOrientationVector, GUIFilled.实体, 120, 22, SelectedObjects, (comps) => { }, (res) =>
                {
                    sp_PathOrientationVector.enumValueIndex = (int)(XTween_PathOrientationVector)System.Enum.Parse(typeof(XTween_PathOrientationVector), res);
                });
            }
            #endregion

            #region 控制点样式
            actionlist = System.Enum.GetNames(typeof(XTween_LineStyle));
            Editor_XTween_GUI.Gui_Layout_Popup<string, XTween_PathTool>("控制点样式", actionlist, ref sp_IndexControlLineStyle, GUIFilled.实体, 120, 22, SelectedObjects, (comps) => { }, (res) =>
            {
                sp_ControlLineStyle.enumValueIndex = (int)(XTween_LineStyle)System.Enum.Parse(typeof(XTween_LineStyle), res);
            });
            #endregion

            #region 标记方式
            actionlist = System.Enum.GetNames(typeof(XTween_PathMarksMode));
            Editor_XTween_GUI.Gui_Layout_Popup<string, XTween_PathTool>("生成标记方式", actionlist, ref sp_IndexPathMarkMode, GUIFilled.实体, 120, 22, SelectedObjects, (comps) => { }, (res) =>
            {
                sp_PathMarksMode.enumValueIndex = (int)(XTween_PathMarksMode)System.Enum.Parse(typeof(XTween_PathMarksMode), res);
            });
            #endregion

            #region 路径闭合
            Editor_XTween_GUI.Gui_Layout_Toggle<bool, XTween_PathTool>("路径闭合", new string[] { "禁用", "启用" }, ref sp_IsClosed, GUIFilled.无, GUIFilled.实体, Color.white, 120, 22, SelectedObjects);
            #endregion

            #region 显示路径
            Editor_XTween_GUI.Gui_Layout_Toggle<bool, XTween_PathTool>("显示路径", new string[] { "禁用", "启用" }, ref sp_DisplayPath, GUIFilled.无, GUIFilled.实体, Color.white, 120, 22, SelectedObjects);
            #endregion

            #region 显示路径点信息
            Editor_XTween_GUI.Gui_Layout_Toggle<bool, XTween_PathTool>("显示路径点信息", new string[] { "禁用", "启用" }, ref sp_DisplayIndex, GUIFilled.无, GUIFilled.实体, Color.white, 120, 22, SelectedObjects);
            #endregion

            #region 注视线
            Editor_XTween_GUI.Gui_Layout_Toggle<bool, XTween_PathTool>("注视线可视化", new string[] { "禁用", "启用" }, ref sp_LookAtLine, GUIFilled.无, GUIFilled.实体, Color.white, 120, 22, SelectedObjects);
            #endregion
            Editor_XTween_GUI.Gui_Layout_Space(10);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 注视参数
            if (BaseScript.PathOrientation == XTween_PathOrientation.注视目标物体 || BaseScript.PathOrientation == XTween_PathOrientation.注视目标位置)
            {
                Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "注视参数", XTween_Dashboard.Theme_Primary);
                Editor_XTween_GUI.Gui_Layout_Space(5);
                if (BaseScript.PathOrientation == XTween_PathOrientation.注视目标物体)
                {
                    Editor_XTween_GUI.Gui_Layout_Space(5);
                    Editor_XTween_GUI.Gui_Layout_Property_Field("目标物体", sp_LookAtObject);
                }
                if (BaseScript.PathOrientation == XTween_PathOrientation.注视目标位置)
                {
                    Editor_XTween_GUI.Gui_Layout_Space(5);
                    Editor_XTween_GUI.Gui_Layout_Property_Field("目标位置", sp_LookAtPosition);
                }
                Editor_XTween_GUI.Gui_Layout_Space(5);
                Editor_XTween_GUI.SetEnabled(false);
                Editor_XTween_GUI.Gui_Layout_Property_Field("连线坐标数组", sp_LookAtPoints);
                Editor_XTween_GUI.SetEnabled(true);

                Editor_XTween_GUI.Gui_Layout_Space(10);
                Editor_XTween_GUI.Gui_Layout_Vertical_End();
            }
            #endregion

            #region 开关状态
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "开关状态", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            #region 世界状态     
            Editor_XTween_GUI.StatuDisplayer_text(icon_worldmode, 12, new Vector2(0, 7), "世界状态", 12, sp_IsWorldMode.boolValue ? "启用" : "禁用", sp_IsWorldMode.boolValue ? XTween_Dashboard.Theme_Primary : Color.gray, 11);
            #endregion

            #region 初始坐标     
            Editor_XTween_GUI.StatuDisplayer_text(icon_startpos, 12, new Vector2(0, 7), "初始坐标", 12, XTween_Utilitys.ConvertVector3ToString(sp_StartPosition.vector3Value), XTween_Dashboard.Theme_Primary, 11);
            #endregion

            #region 路径长度     
            Editor_XTween_GUI.StatuDisplayer_text(icon_pathlength, 12, new Vector2(0, 7), "路径长度", 12, sp_PathLength.floatValue.ToString("F2"), XTween_Dashboard.Theme_Primary, 11);
            #endregion

            #region 路径父级物体
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.StatuDisplayer_Object(icon_Grandparent, 12, new Vector2(0, 1), "路径父级物体", 12, new Vector2(0, -7), status, new Vector2(0, 3), sp_PathParent.objectReferenceValue == null ? false : true, XTween_Dashboard.Theme_Primary, Color.black * 0.7f, sp_PathParent);
            #endregion
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 路径参数
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "路径参数", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("有效路径百分比", sp_PathLimitePercent, 120);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径步数细分", sp_SegmentsPerCurve, 120);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("添加路径初始距离", sp_AddedDistance, 120);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 路径样式
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "路径样式", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径点尺寸", sp_PathPointSize, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("贝塞尔控制点尺寸", sp_BezierControlSize, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径宽度", sp_PathWidth, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径信息尺寸", sp_IndexSize, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径信息高度", sp_IndexLengthHeight, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径信息偏移", sp_IndexOffset, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("注视线宽度", sp_LookAtLineWidth, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径颜色", sp_Color_Path, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径点颜色", sp_Color_PathPoint, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径点选中颜色", sp_Color_PathPoint_Selected, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("控制点颜色", sp_Color_BezierControl, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("控制点选中颜色", sp_Color_BezierControl_Selected, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径点序号颜色", sp_Color_Index, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("路径点信息颜色", sp_Color_IndexLength, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("注视线颜色", sp_Color_LookAtLine, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("标记颜色", sp_PathMarksColor, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            #region 路径标记生成
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 5, "路径标记生成", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("标记样式", sp_PathMarksTexture, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("标记尺寸", sp_PathMarksSize, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Property_Field("标记采样", sp_PathMarksSample, 110);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 1)
            {
                // 创建右键菜单
                GenericMenu menu = new GenericMenu();
                menu.AddDisabledItem(new GUIContent("路径点"));
                menu.AddItem(new GUIContent("A (添加路径点)"), false, () =>
                {
                    PathPoints_Add();
                    return;
                });
                menu.AddItem(new GUIContent("X (清空路径点)"), false, () =>
                {
                    PathPoints_Clear();
                    return;
                });
                menu.AddSeparator("");
                menu.AddDisabledItem(new GUIContent("标记路径"));
                if (sp_PathMarksGroup.objectReferenceValue == null)
                {
                    menu.AddItem(new GUIContent("D (标记)"), false, () =>
                    {
                        string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTweenPath消息", "创建标记物", "此操作会根据当前的路径生成标记物！", "创建", "暂不", 0);
                        if (res == "创建")
                        {
                            BaseScript.PathMarks_Create();
                        }
                    });
                }
                else
                {
                    menu.AddItem(new GUIContent("D (清除)"), false, () =>
                    {
                        string res_de = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTweenPath消息", "已存在标记物", "此操作会将当前的所有路径标记物全部清除！", "清除", "暂不", 0);
                        if (res_de == "清除")
                        {
                            BaseScript.PathMarks_Clear();
                        }
                    });
                }
                menu.AddSeparator("");
                menu.AddDisabledItem(new GUIContent("坐标"));
                menu.AddItem(new GUIContent("R (路径点Z轴归零)"), false, () =>
                {
                    PathPoints_ZAxis_ToZero();
                });
                menu.ShowAsContext(); // 在鼠标位置显示右键菜单
                e.Use();
            }

            serializedObject.ApplyModifiedProperties();

            #region 源脚本
            Editor_XTween_GUI.Gui_Layout_Vertical_Start(GUIFilled.纯色边框, GUIColor.亮白, 3, "源脚本", XTween_Dashboard.Theme_Primary);
            Editor_XTween_GUI.Gui_Layout_Space(5);

            #region 原始变量
            Editor_XTween_GUI.Gui_Layout_Horizontal_Start(GUIFilled.无, GUIColor.无, 0);
            Editor_XTween_GUI.Gui_Layout_Space(10);
            BasicVars = EditorGUILayout.Foldout(BasicVars, "变量/属性", true);
            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Horizontal_End();
            if (BasicVars)
                DrawDefaultInspector();
            #endregion

            Editor_XTween_GUI.Gui_Layout_Space(5);
            Editor_XTween_GUI.Gui_Layout_Vertical_End();
            #endregion
        }
        /// <summary>
        /// 增加路径点
        /// </summary>
        private void PathPoints_Add()
        {
            Undo.RecordObject(BaseScript, "Add Path Point");

            // 创建一个点位，如果路径点列表不是空的，那么就获取最后的点的坐标向右平移 0.3 单位的距离创建新点位，否则就创建在脚本物体的位置
            Vector3 InitialPosition = BaseScript.PathPoints.Count > 0 ? BaseScript.Get_WorldPosition(BaseScript.PathPoints.Count - 1) + Vector3.right * BaseScript.AddedDistance : BaseScript.transform.position;

            // 初始化一个点位的贝塞尔曲线类
            Vector3 LocalPoint = BaseScript.transform.InverseTransformPoint(InitialPosition);
            var newPoint = new XTween_BezierPathPoint(LocalPoint, Vector3.zero, Vector3.zero);

            // 如果当前路径模式为 手动贝塞尔 或 平衡贝塞尔 那么切线点要做相应的调整
            //if (BaseScript.XTween_PathType == XTween_PathType.手动贝塞尔 || BaseScript.XTween_PathType == XTween_PathType.平衡贝塞尔)
            //{

            // 首先算出距离：0.2 除以自身的 正向X轴 轴缩放
            float distance = 0.1f / BaseScript.transform.lossyScale.x;
            // 入点向左移动 distance 距离
            newPoint.bezier_in = Vector3.left * distance;

            // 如果是 Balance 模式那么出点为入点的反向位置，否则向右移动 distance 距离
            newPoint.bezier_out = BaseScript.PathType == XTween_PathType.平衡贝塞尔 ? -newPoint.bezier_in : Vector3.right * distance;

            //}

            // 将新建的路径点加入列表中
            BaseScript.PathPoints.Add(newPoint);
            // 选中创建的点位
            selectedAnchorIndex = BaseScript.PathPoints.Count - 1;
        }
        /// <summary>
        /// 清空路径点
        /// </summary>
        private void PathPoints_Clear()
        {
            string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTweenPath消息", "清空路径点", "此操作将清空当前已设置的所有路径点！请谨慎操作！", "清空", "暂不", 1);
            if (res == "清空")
            {
                Undo.RecordObject(BaseScript, "Clear All Path Points");
                BaseScript.PathPoints.Clear();
                selectedAnchorIndex = -1;
                selectedControlIndex = -1;
            }
        }
        /// <summary>
        /// 将所有路径点的Z轴归零到画布
        /// </summary>
        private void PathPoints_ZAxis_ToZero()
        {
            string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTweenPath消息", "路径点Z轴归零", "此操作会将当前已设置的所有路径点的Z轴坐标归零到画布！请谨慎操作！", "归零", "暂不", 1);
            if (res == "归零")
            {
                Undo.RecordObject(BaseScript, "Path Points ResetToZero");
                for (int i = 0; i < BaseScript.PathPoints.Count; i++)
                {
                    BaseScript.PathPoints[i].relative.z = 0;
                }
            }
        }
        /// <summary>
        /// 标记点创建/清除
        /// </summary>
        private void PathMarksCreator()
        {
            if (sp_PathMarksGroup.objectReferenceValue != null)
            {
                string res_de = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTweenPath消息", "已存在标记物", "此操作会将当前的所有路径标记物全部清除！", "清除", "暂不", 0);
                if (res_de == "清除")
                {
                    BaseScript.PathMarks_Clear();
                }
            }
            else
            {
                string res = Editor_XTween_GUI.Open(XTweenDialogType.警告, "XTweenPath消息", "创建标记物", "此操作会根据当前的路径生成标记物！", "创建", "暂不", 0);
                if (res == "创建")
                {
                    BaseScript.PathMarks_Create();
                }
            }
        }
        /// <summary>
        /// 在场景视图中绘制路径和路径点
        /// </summary>
        private void DuringSceneGUI(SceneView sceneView)
        {
            if (BaseScript.PathPoints == null)
                return;

            // 根据脚本物体转换出对应的 world 和 anchored 位置信息
            if (!BaseScript.IsWorldMode)
                BaseScript.ConvertPositions();

            // 计算路径总长
            BaseScript.PathLength = BaseScript.CalculateTotalLength();

            // 绘制路径点和线的可视化
            DrawPath();

            Event e = Event.current;

            for (int i = 0; i < BaseScript.PathPoints.Count; i++)
            {
                DrawPoint_Path(i, e);
                DrawPoints_TangentControl(i, e);

                if (BaseScript.DisplayPath && BaseScript.DisplayIndex)
                {
                    // 在每个锚点旁边绘制序号
                    Vector3 pos = BaseScript.Get_WorldPosition(i);
                    // 字体设置
                    IndexStyle.fontSize = BaseScript.IndexSize;
                    IndexStyle.normal.textColor = BaseScript.Color_Index;
                    IndexStyle.alignment = TextAnchor.MiddleCenter;
                    IndexStyle.contentOffset = new Vector2(BaseScript.IndexOffset.x, 1 - BaseScript.IndexOffset.y - 1);
                    // 显示路径点序号
                    Handles.Label(pos, $"X {i}", IndexStyle);

                    // 如果不是最后一个点，绘制线段长度
                    if (i < BaseScript.PathPoints.Count - 1 || BaseScript.IsClosed)
                    {
                        // 计算当前线段的长度
                        int nextIndex = (i + 1) % BaseScript.PathPoints.Count;
                        float segmentLength = BaseScript.CalculateSegmentLength(i, nextIndex, 10);

                        // 计算线段中点位置
                        Vector3 midPoint = (BaseScript.Get_WorldPosition(i) + BaseScript.Get_WorldPosition(nextIndex)) * 0.5f;

                        IndexStyle.fontSize = BaseScript.IndexSize - 2;
                        IndexStyle.normal.textColor = BaseScript.Color_IndexLength;
                        IndexStyle.contentOffset = new Vector2(BaseScript.IndexOffset.x, 1 - BaseScript.IndexOffset.y - 1 - BaseScript.IndexLengthHeight);

                        float per = segmentLength / BaseScript.PathLength * 100;
                        // 显示线段长度（使用更友好的单位）
                        Handles.Label(pos, $"{segmentLength:F2} / {per:F1}%", IndexStyle);
                    }
                }
            }

            PathPointKeyboardEdit(e);

            if (BaseScript.act_on_pathChanged != null)
                BaseScript.act_on_pathChanged?.Invoke();
        }
        /// <summary>
        /// 绘制路径
        /// </summary>
        private void DrawPath()
        {
            if (!BaseScript.DisplayPath)
                return;
            if (!BaseScript.gameObject.activeSelf)
                return;
            if (BaseScript.PathPoints.Count < 2)
                return;
            Handles.color = BaseScript.Color_Path;

            switch (BaseScript.PathType)
            {
                case XTween_PathType.线性:
                    Vector3[] linearPoints = new Vector3[BaseScript.PathPoints.Count + (BaseScript.IsClosed ? 1 : 0)];
                    for (int i = 0; i < BaseScript.PathPoints.Count; i++)
                    {
                        // 获取路径点（世界坐标）
                        linearPoints[i] = BaseScript.Get_WorldPosition(i);
                    }
                    if (BaseScript.IsClosed)
                    {
                        linearPoints[BaseScript.PathPoints.Count] = BaseScript.Get_WorldPosition(0);
                    }
                    // DrawAAPolyLine 填入的位置参数是世界坐标
                    Handles.DrawAAPolyLine(BaseScript.PathWidth, linearPoints);
                    break;
                case XTween_PathType.自动贝塞尔:
                    for (int i = 0; i < BaseScript.PathPoints.Count - 1; i++)
                    {
                        Vector3 start_before_tangent = (i > 0) ? BaseScript.Get_WorldPosition(i - 1) : (BaseScript.IsClosed ? BaseScript.Get_WorldPosition(BaseScript.PathPoints.Count - 1) : BaseScript.Get_WorldPosition(i));
                        Vector3 start = BaseScript.Get_WorldPosition(i);
                        Vector3 end = BaseScript.Get_WorldPosition(i + 1);
                        Vector3 end_after_tangent = (i < BaseScript.PathPoints.Count - 2) ? BaseScript.Get_WorldPosition(i + 2) : (BaseScript.IsClosed ? BaseScript.Get_WorldPosition((i + 2) % BaseScript.PathPoints.Count) : end);

                        Vector3 tangent_start = 0.5f * (end - start_before_tangent);
                        Vector3 tangent_end = 0.5f * (end_after_tangent - start);

                        // DrawBezier 的参数均为世界坐标数值
                        Handles.DrawBezier(start, end, start + tangent_start * 0.3f, end - tangent_end * 0.3f, BaseScript.Color_Path, null, BaseScript.PathWidth);
                    }

                    // 绘制闭合段
                    if (BaseScript.IsClosed && BaseScript.PathPoints.Count > 2)
                    {
                        int i = BaseScript.PathPoints.Count - 1;
                        Vector3 start_before_tangent = BaseScript.Get_WorldPosition(i - 1);
                        Vector3 start = BaseScript.Get_WorldPosition(i);
                        Vector3 end = BaseScript.Get_WorldPosition(0);
                        Vector3 end_after_tangent = BaseScript.Get_WorldPosition(1);

                        Vector3 tangent_start = 0.5f * (end - start_before_tangent);
                        Vector3 tangent_end = 0.5f * (end_after_tangent - start);

                        // DrawBezier 的参数均为世界坐标数值
                        Handles.DrawBezier(
                            start, end,
                            start + tangent_start * 0.3f,
                            end - tangent_end * 0.3f,
                            BaseScript.Color_Path,
                            null,
                            BaseScript.PathWidth
                        );
                    }
                    break;
                case XTween_PathType.手动贝塞尔:
                case XTween_PathType.平衡贝塞尔:
                    for (int i = 0; i < BaseScript.PathPoints.Count - 1; i++)
                    {
                        // DrawBezier 的参数均为世界坐标数值
                        Handles.DrawBezier(
                            BaseScript.Get_WorldPosition(i),
                            BaseScript.Get_WorldPosition(i + 1),
                            BaseScript.Get_Out_Position(i),
                            BaseScript.Get_In_Position(i + 1),
                            BaseScript.Color_Path,
                            null,
                            BaseScript.PathWidth
                        );
                    }

                    // 绘制闭合段
                    if (BaseScript.IsClosed && BaseScript.PathPoints.Count > 2)
                    {
                        int i = BaseScript.PathPoints.Count - 1;
                        // DrawBezier 的参数均为世界坐标数值
                        Handles.DrawBezier(
                            BaseScript.Get_WorldPosition(i),
                            BaseScript.Get_WorldPosition(0),
                            BaseScript.Get_Out_Position(i),
                            BaseScript.Get_In_Position(0),
                            BaseScript.Color_Path,
                            null,
                            BaseScript.PathWidth
                        );
                    }
                    break;
            }
        }
        /// <summary>
        /// 绘制路径点
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// param name="e">当前事件</param>
        private void DrawPoint_Path(int index, Event e)
        {
            if (!BaseScript.DisplayPath)
                return;
            if (!BaseScript.gameObject.activeSelf)
                return;
            if (BaseScript.IsWorldMode)
                return;

            Vector3 w_pos = BaseScript.Get_WorldPosition(index);

            bool isSelected = (index == selectedAnchorIndex);
            Handles.color = isSelected ? BaseScript.Color_PathPoint_Selected : BaseScript.Color_PathPoint;

            float handleSize = HandleUtility.GetHandleSize(w_pos);
            float screenSize = handleSize * (BaseScript.PathPointSize * 0.1f) * 0.5f; // 关键调整：乘以0.5使视觉尺寸匹配碰撞检测

            Handles.SphereHandleCap(
                0,
                w_pos,
                Quaternion.identity,
                screenSize,
                EventType.Repaint
            );

            int anchorControlID = GUIUtility.GetControlID(FocusType.Passive);
            if (e.type == EventType.Layout)
            {
                HandleUtility.AddControl(anchorControlID, HandleUtility.DistanceToCircle(w_pos, screenSize));
            }

            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt && !e.control && !e.shift && HandleUtility.nearestControl == anchorControlID)
            {
                selectedAnchorIndex = index;
                selectedControlIndex = -1;
                GUIUtility.hotControl = anchorControlID;
                e.Use();
            }

            if (isSelected)
            {
                EditorGUI.BeginChangeCheck();
                // 通过拖动路径点产生新位置的世界坐标空间的位置值
                Vector3 handle_pos = Handles.PositionHandle(w_pos, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(BaseScript, "MovePathPoint");
                    // 拖动路径点的时候将点的世界坐标空间转换到 relative 局部坐标空间值中
                    BaseScript.PathPoints[index].relative = BaseScript.transform.InverseTransformPoint(handle_pos);
                    BaseScript.ConvertPositions();
                }
            }
        }
        /// <summary>
        /// 绘制切线控制点
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// <param name="e">当前事件</param>
        private void DrawPoints_TangentControl(int index, Event e)
        {
            if (!BaseScript.DisplayPath)
                return;
            if (!BaseScript.gameObject.activeSelf)
                return;
            if (BaseScript.IsWorldMode)
                return;
            if (BaseScript.PathType != XTween_PathType.手动贝塞尔 && BaseScript.PathType != XTween_PathType.平衡贝塞尔)
                return;

            // 绘制入控制点(对除第一个点外的所有点)
            if (index > 0 || (BaseScript.IsClosed && index == 0))
            {
                DrawPoint_Tangent(
                    index,
                    BaseScript.Get_In_Position(index),
                    isInControl: true,
                    e
                );
            }

            // 绘制出控制点(对除最后一个点外的所有点，或闭合时的最后一个点)
            if (index < BaseScript.PathPoints.Count - 1 || (BaseScript.IsClosed && index == BaseScript.PathPoints.Count - 1))
            {
                DrawPoint_Tangent(
                    index,
                    BaseScript.Get_Out_Position(index),
                    isInControl: false,
                    e
                );
            }
        }
        /// <summary>
        /// 绘制切线控制点
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// <param name="pos">切线控制点的位置</param>
        /// <param name="isInControl">是否是入控制点</param>
        /// <param name="e">当前事件</param>
        private void DrawPoint_Tangent(int index, Vector3 pos, bool isInControl, Event e)
        {
            if (!BaseScript.gameObject.activeSelf)
                return;

            bool isSelected = (index == selectedControlIndex) && ((isInControl && this.isInControl) || (!isInControl && !this.isInControl));

            Handles.color = isSelected ? BaseScript.Color_BezierControl_Selected : BaseScript.Color_BezierControl;

            Vector3 anchorPos = BaseScript.Get_WorldPosition(index);
            if (BaseScript.ControlLineStyle == XTween_LineStyle.虚线)
                Handles.DrawDottedLine(anchorPos, pos, 2f);
            else
                Handles.DrawAAPolyLine(anchorPos, pos);

            float handleSize = HandleUtility.GetHandleSize(anchorPos);
            float screenSize = handleSize * (BaseScript.BezierControlSize * 0.1f) * 0.5f; // 关键调整：乘以0.5使视觉尺寸匹配碰撞检测

            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            if (e.type == EventType.Layout)
            {
                HandleUtility.AddControl(controlID, HandleUtility.DistanceToCircle(pos, screenSize));
            }

            // 切线控制点
            Handles.SphereHandleCap(controlID, pos, Quaternion.identity, screenSize, EventType.Repaint);

            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt && !e.control && !e.shift && HandleUtility.nearestControl == controlID)
            {
                selectedControlIndex = index;
                selectedAnchorIndex = -1;
                this.isInControl = isInControl;
                GUIUtility.hotControl = controlID;
                e.Use();
            }

            if (isSelected)
            {
                Tools.current = Tool.Move;

                EditorGUI.BeginChangeCheck();
                Vector3 pos_world = Handles.PositionHandle(pos, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(BaseScript, "Move Control Point");
                    Vector3 localAnchor = BaseScript.PathPoints[index].relative;
                    Vector3 localPos = BaseScript.transform.InverseTransformPoint(pos_world);

                    if (BaseScript.PathType == XTween_PathType.平衡贝塞尔)
                    {
                        if (isInControl)
                        {
                            BaseScript.PathPoints[index].bezier_in = localPos - localAnchor;
                            BaseScript.PathPoints[index].bezier_out = -BaseScript.PathPoints[index].bezier_in;
                        }
                        else
                        {
                            BaseScript.PathPoints[index].bezier_out = localPos - localAnchor;
                            BaseScript.PathPoints[index].bezier_in = -BaseScript.PathPoints[index].bezier_out;
                        }
                    }
                    else
                    {
                        if (isInControl)
                        {
                            BaseScript.PathPoints[index].bezier_in = localPos - localAnchor;
                        }
                        else
                        {
                            BaseScript.PathPoints[index].bezier_out = localPos - localAnchor;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 对路径点进行键盘操作，按下 Delete 键时删除选中的路径点或重置控制点
        /// </summary>
        /// <param name="e">当前事件</param>
        private void PathPointKeyboardEdit(Event e)
        {
            if (!BaseScript.gameObject.activeSelf)
                return;
            if (BaseScript.IsWorldMode)
                return;

            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Delete)
                {
                    if (selectedAnchorIndex >= 0)
                    {
                        Undo.RecordObject(BaseScript, "Delete Point");
                        BaseScript.PathPoints.RemoveAt(selectedAnchorIndex);
                        selectedAnchorIndex = Mathf.Clamp(selectedAnchorIndex, 0, BaseScript.PathPoints.Count - 1);
                        e.Use();
                    }
                    else if (selectedControlIndex >= 0)
                    {
                        Undo.RecordObject(BaseScript, "Reset Control Point");
                        if (isInControl)
                        {
                            BaseScript.PathPoints[selectedControlIndex].bezier_in = Vector3.zero;
                            if (BaseScript.PathType == XTween_PathType.平衡贝塞尔)
                                BaseScript.PathPoints[selectedControlIndex].bezier_out = Vector3.zero;
                        }
                        else
                        {
                            BaseScript.PathPoints[selectedControlIndex].bezier_out = Vector3.zero;
                            if (BaseScript.PathType == XTween_PathType.平衡贝塞尔)
                                BaseScript.PathPoints[selectedControlIndex].bezier_in = Vector3.zero;
                        }
                        e.Use();
                    }
                }
            }
        }
        /// <summary>
        /// 因为路径动画的特殊性必须将其轴心点设为中心对齐
        /// </summary>
        private void SetTransformCenter()
        {
            if (target == null)
                return;
            RectTransform rect = BaseScript.GetComponent<RectTransform>();
            Vector3 ori_pos = rect.localPosition;
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = ori_pos;
        }
    }
}