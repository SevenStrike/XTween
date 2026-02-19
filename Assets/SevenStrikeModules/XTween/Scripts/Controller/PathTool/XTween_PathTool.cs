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
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于存储路径长度相关数据的结构体
    /// </summary>
    public struct XTween_PathLengthData
    {
        /// <summary>
        /// 每个路径段的长度数组
        /// </summary>
        public float[] segmentLengths;
        /// <summary>
        /// 每个路径段的累积长度数组
        /// </summary>
        public float[] cumulativeLengths;
        /// <summary>
        /// 路径的总长度
        /// </summary>
        public float totalLength;
    }

    /// <summary>
    /// 表示贝塞尔路径上的一个点，包含路径点的位置和贝塞尔曲线的控制点
    /// </summary>
    [System.Serializable]
    public class XTween_BezierPathPoint
    {
        /// <summary>
        /// 路径点的位置（基于脚本物体的局部空间）
        /// </summary>
        public Vector3 relative;
        /// <summary>
        /// 路径点的位置（路径点的世界空间）
        /// </summary>
        public Vector3 world;
        /// <summary>
        /// 路径点的位置（基于脚本物体的同级局部空间）
        /// </summary>
        public Vector3 anchored;
        /// <summary>
        /// 切线入点的位置（基于路径点的局部空间）
        /// </summary>
        public Vector3 bezier_in;
        /// <summary>
        /// 切线出点的位置（基于路径点的局部空间）
        /// </summary>
        public Vector3 bezier_out;
        /// <summary>
        /// 切线入点的位置（基于路径点的局部空间转换的世界空间）
        /// </summary>
        public Vector3 bezier_in_world;
        /// <summary>
        /// 切线出点的位置（基于路径点的局部空间转换的世界空间）
        /// </summary>
        public Vector3 bezier_out_world;
        /// <summary>
        /// 路径点的方向（可选，用于存储自定义方向）
        /// </summary>
        public Vector3 direction = Vector3.forward;
        /// <summary>
        /// 贝塞尔曲线点构造函数
        /// </summary>
        /// <param name="anchor">路径点的位置</param>
        /// <param name="inControl">切线入点的位置</param>
        /// <param name="outControl">切线出点的位置</param>
        public XTween_BezierPathPoint(Vector3 anchor, Vector3 inControl, Vector3 outControl)
        {
            relative = anchor;
            bezier_in = inControl;
            bezier_out = outControl;
            bezier_in_world = Vector3.zero;
            bezier_out_world = Vector3.zero;
            world = Vector3.zero;
            anchored = Vector3.zero;
        }
        /// <summary>
        /// 将路径点的局部坐标转换为世界坐标
        /// </summary>
        /// <param name="target">目标物体的 Transform</param>
        public void Convert_World_Position(Transform target)
        {
            if (target != null)
            {
                // 转换：局部坐标 -> 世界坐标
                world = target.TransformPoint(relative);
            }
        }
        /// <summary>
        /// 将路径点的世界坐标转换为脚本物体同级的局部坐标
        /// </summary>
        /// <param name="target">目标物体的 Transform</param>
        public void Convert_Anchored_Position(Transform target)
        {
            if (target != null)
            {
                // 转换：世界坐标 -> 脚本物体同级的局部坐标
                anchored = target.parent.InverseTransformPoint(world);
            }
        }
        /// <summary>
        /// 将路径点的贝塞尔控制点的局部坐标转换为世界坐标
        /// </summary>
        /// <param name="target">目标物体的 Transform</param>
        public void Convert_BezierTangent_Position(Transform target)
        {
            if (target != null)
            {
                // 转换：切线点局部坐标 -> 切线点世界坐标
                bezier_in_world = target.TransformPoint(relative + bezier_in);
                bezier_out_world = target.TransformPoint(relative + bezier_out);
            }
        }
    }

    /// <summary>
    /// 用于定义和操作贝塞尔路径的工具类
    /// 提供路径点的管理、路径的生成与可视化，以及路径上点的插值计算
    /// </summary>
    public class XTween_PathTool : MonoBehaviour
    {
        #region Path Settings
        /// <summary>
        /// 路径点列表，包含路径的关键点信息
        /// 每个路径点可以定义路径的形状和方向
        /// </summary>
        [SerializeField] public List<XTween_BezierPathPoint> PathPoints = new List<XTween_BezierPathPoint>();
        /// <summary>
        /// 路径的类型，决定路径的生成方式
        /// 默认值为 <see cref="XTween_PathType.线性"/>，表示线性路径
        /// </summary>
        [SerializeField] public XTween_PathType PathType = XTween_PathType.线性;
        /// <summary>
        /// 路径朝向模式，控制物体在路径运动时的朝向行为
        /// </summary>
        [SerializeField] public XTween_PathOrientation PathOrientation = XTween_PathOrientation.无;
        /// <summary>
        /// 路径朝向轴向，当使用FollowPath模式时指定物体的对齐路径的轴向，如果使用LookObject或LookPosition模式时则是将动画物体的相对应轴向指向目标物体或位置
        /// </summary>
        [SerializeField] public XTween_PathOrientationVector PathOrientationVector = XTween_PathOrientationVector.正向X轴;
        /// <summary>
        /// 注视目标对象，当朝向模式设为LookObject时需要指定的目标物体
        /// </summary>
        [SerializeField] public GameObject LookAtObject;
        /// <summary>
        /// 注视目标位置，当朝向模式设为LookPosition时需要指定的世界坐标
        /// </summary>
        [SerializeField] public Vector3 LookAtPosition;
        /// <summary>
        /// 注视方向线段端点坐标数组（自动生成）[0]物体当前位置，[1]注视目标位置
        /// </summary>
        [SerializeField] public Vector3[] LookAtPoints = new Vector3[2];
        /// <summary>
        /// 是否使用世界坐标模式
        /// 如果为 <c>true</c>，路径点的坐标以世界坐标系为基准；
        /// 如果为 <c>false</c>，路径点的坐标以局部坐标系为基准
        /// </summary>
        [SerializeField] public bool IsWorldMode;
        /// <summary>
        /// 路径的起始位置
        /// </summary>
        [SerializeField] public Vector3 StartPosition;
        /// <summary>
        /// 路径的进度，范围为 [0, 1]
        /// 用于表示当前路径的完成度或位置
        /// </summary>
        [Range(0, 1)][SerializeField] public float PathProgress;
        /// <summary>
        /// 路径的总长度
        /// 用于计算路径的进度或其他相关操作
        /// </summary>
        [SerializeField] public float PathLength = 0;
        /// <summary>
        /// 有效路径的总长度的百分比，即路径动画只运动到此百分比的位置算完成
        /// </summary>
        [SerializeField][Range(0, 1)] public float PathLimitePercent = 1;
        /// <summary>
        /// 每条贝塞尔曲线的分段数量
        /// 用于控制路径的平滑度
        /// 默认值为 30
        /// </summary>
        [SerializeField] public int SegmentsPerCurve = 30;
        /// <summary>
        /// 路径是否闭合
        /// 如果为 <c>true</c>，路径的起点和终点相连，形成一个闭环
        /// 默认值为 <c>false</c>
        /// </summary>
        [SerializeField] public bool IsClosed = false;
        /// <summary>
        /// 是否显示路径
        /// 如果为 <c>true</c>，路径在编辑器中可见；否则不可见
        /// 默认值为 <c>true</c>
        /// </summary>
        [SerializeField] public bool DisplayPath = true;
        /// <summary>
        /// 是否显示路径点的索引
        /// 如果为 <c>true</c>，在编辑器中显示每个路径点的编号；否则不显示
        /// 默认值为 <c>true</c>
        /// </summary>
        [SerializeField] public bool DisplayIndex = true;
        #endregion

        #region Style Settings
        /// <summary>
        /// 普通路径点的颜色
        /// 默认值为半透明的白色
        /// </summary>
        [SerializeField] public Color Color_PathPoint = new Color(1, 1, 1, 0.5607843f);
        /// <summary>
        /// 选中路径点的颜色
        /// 默认值为橙色，用于突出显示选中的路径点
        /// </summary>
        [SerializeField] public Color Color_PathPoint_Selected = new Color(1f, 0.6285f, 0.2705883f, 1f);
        /// <summary>
        /// 贝塞尔控制点的颜色
        /// 默认值为半透明的灰色
        /// </summary>
        [SerializeField] public Color Color_BezierControl = new Color(0.695f, 0.695f, 0.695f, 0.254902f);
        /// <summary>
        /// 选中贝塞尔控制点的颜色
        /// 默认值为半透明的黄色，用于突出显示选中的控制点
        /// </summary>
        [SerializeField] public Color Color_BezierControl_Selected = new Color(1f, 0.6802889f, 0.1462264f, 0.5960785f);
        /// <summary>
        /// 路径的颜色
        /// 默认值为亮绿色，用于显示路径的主体部分
        /// </summary>
        [SerializeField] public Color Color_Path = new Color(0.4009434f, 1f, 0.7615631f, 1f);
        /// <summary>
        /// 路径点索引的颜色
        /// 默认值为白色，用于显示路径点的索引编号
        /// </summary>
        [SerializeField] public Color Color_Index = Color.white;
        /// <summary>
        /// 路径点索引长度的颜色
        /// 默认值为橙色，用于显示路径点索引的长度信息
        /// </summary>
        [SerializeField] public Color Color_IndexLength = new Color(1f, 0.6073911f, 0.298f, 1f);
        /// <summary>
        /// 路径点索引长度的颜色
        /// 默认值为橙色，用于显示路径点索引的长度信息
        /// </summary>
        [SerializeField] public Color Color_LookAtLine = Color.white;
        /// <summary>
        /// 控制点的线条样式
        /// </summary>
        [SerializeField] public XTween_LineStyle ControlLineStyle = XTween_LineStyle.虚线;
        /// <summary>
        /// 路径点的大小
        /// 默认值为 5，用于控制路径点的显示大小
        /// </summary>
        [SerializeField] public float PathPointSize = 5f;
        /// <summary>
        /// 贝塞尔控制点的大小
        /// 默认值为 3，用于控制贝塞尔控制点的显示大小
        /// </summary>
        [SerializeField] public float BezierControlSize = 3f;
        /// <summary>
        /// 路径的宽度
        /// 默认值为 3，用于控制路径的显示宽度
        /// </summary>
        [SerializeField] public float PathWidth = 3f;
        /// <summary>
        /// 路径点索引的字体大小
        /// 默认值为 15，用于控制路径点索引的显示大小
        /// </summary>
        [SerializeField] public int IndexSize = 15;
        /// <summary>
        /// 路径点索引长度的显示高度
        /// 默认值为 20，用于控制路径点索引长度的显示高度
        /// </summary>
        [SerializeField] public float IndexLengthHeight = 20f;
        /// <summary>
        /// 路径点索引的偏移量
        /// 默认值为 (0, 35)，用于控制路径点索引的显示位置
        /// </summary>
        [SerializeField] public Vector2 IndexOffset = new Vector2(0, 35);
        /// <summary>
        /// 添加的距离值，用于路径点之间的间距调整
        /// 默认值为 0.1，用于微调路径点的显示位置
        /// </summary>
        [SerializeField] public float AddedDistance = 0.1f;
        /// <summary>
        /// 是否显示物体到注视目标的指引线（编辑器可视化用）
        /// </summary>
        [SerializeField] public bool LookAtLine = true;
        /// <summary>
        /// 注视指引线的显示宽度（像素单位）
        /// </summary>
        [SerializeField] public float LookAtLineWidth = 3f;
        #endregion

        #region Path Marks
        /// <summary>
        /// 路径标记的样式
        /// </summary>
        [SerializeField] public Texture PathMarksTexture;
        /// <summary>
        /// 路径标记的尺寸
        /// </summary>
        [SerializeField] public float PathMarksSize = 60f;
        /// <summary>
        /// 路径标记的颜色
        /// </summary>
        [SerializeField] public Color PathMarksColor = Color.white;
        /// <summary>
        /// 路径标记的采样率
        /// 用于定义在路径标记过程中，路径上采样点的密度
        /// 采样频率越高，标记的精度越高，但计算成本也会相应增加
        /// 默认值为 30.0f，表示每单位长度采样 30 个点
        /// </summary>
        [SerializeField] public float PathMarksSample = 30f;
        /// <summary>
        /// 路径标记的模式
        /// 指定路径标记的具体行为模式
        /// 值为 <see cref="XTween_PathMarksMode.根据路径点"/>，表示标记操作基于路径的关键点进行
        /// 值为 <see cref="XTween_PathMarksMode.根据路径线条"/>，表示标记操作基于路径的采样进行
        /// </summary>
        [SerializeField] public XTween_PathMarksMode PathMarksMode = XTween_PathMarksMode.根据路径线条;
        /// <summary>
        /// 路径父级物体
        /// </summary>
        [SerializeField] public UnityEngine.RectTransform PathParent;
        /// <summary>
        /// 标记物组
        /// </summary>
        [SerializeField] public UnityEngine.RectTransform PathMarksGroup;
        /// <summary>
        /// 所有标记物
        /// </summary>
        [SerializeField] public Image[] PathMarks;

        #endregion

        #region Editor
#pragma warning disable CS0414
        [SerializeField] private bool PathPointsIsFold;
        [SerializeField] private string IndexPathType = "线性";
        [SerializeField] private string IndexPathOrientation = "无";
        [SerializeField] private string IndexPathOrientationVector = "正向X轴";
        [SerializeField] private string IndexControlLineStyle = "虚线";
        [SerializeField] private string IndexPathMarkMode = "根据路径线条";
#pragma warning restore CS0414
        #endregion

        #region 委托
        public Action<Vector3> act_on_pathStart;
        public Action<Vector3> act_on_pathComplete;
        public Action<Vector3> act_on_pathMove;
        public Action<float> act_on_pathProgress;
        public Action<Quaternion> act_on_pathOrientation;
        public Action<Vector3> act_on_pathLookatOrientation_withObject;
        public Action<Vector3> act_on_pathLookatOrientation_withPosition;
        public Action act_on_pathChanged;
        #endregion

        private void OnDrawGizmosSelected()
        {
            DrawLookAtLine();
            PathMarks_SyncStyle();
        }

        #region 辅助
        /// <summary>
        /// 绘制LookAt目标与动画物体之间的朝向线段
        /// </summary>
        private void DrawLookAtLine()
        {
#if UNITY_EDITOR
            if (LookAtLine)
            {
                if (PathOrientation == XTween_PathOrientation.无)
                    return;
                if (PathOrientation == XTween_PathOrientation.跟随路径)
                    return;
                LookAtPoints[0] = transform.position;
                if (PathOrientation == XTween_PathOrientation.注视目标位置)
                    LookAtPoints[1] = LookAtPosition;
                else if (PathOrientation == XTween_PathOrientation.注视目标物体)
                {
                    if (LookAtObject == null)
                        return;
                    LookAtPoints[1] = LookAtObject.transform.position;
                }
                Handles.color = Color_LookAtLine;
                Handles.DrawAAPolyLine(LookAtLineWidth, LookAtPoints);
            }
#endif
        }
        /// <summary>
        /// 设置路径总动画长度的限制百分比
        /// </summary>
        /// <param name="percent"></param>
        public void SetLimtePathPercent(float percent)
        {
            PathLimitePercent = percent;
        }
        /// <summary>
        /// 获取路径点的数量
        /// </summary>
        /// <returns></returns>
        public int GetPathPointCount()
        {
            return PathPoints.Count;
        }
        #endregion

        #region 路径点坐标转换
        /// <summary>
        /// 转换路径点坐标为anchored 和 world
        /// </summary>
        public void ConvertPositions()
        {
            foreach (var point in PathPoints)
            {
                point.Convert_World_Position(transform);
                point.Convert_Anchored_Position(transform);
                point.Convert_BezierTangent_Position(transform);
            }
        }
        /// <summary>
        /// 记录动画前的初始位置
        /// </summary>
        public void SaveStartPosition()
        {
            StartPosition = transform.localPosition;
        }
        /// <summary>
        /// 还原动画前的初始位置
        /// </summary>
        public void RestoreStartPosition()
        {
            transform.localPosition = StartPosition;
        }
        #endregion

        #region 路径点坐标获取
        /// <summary>
        /// 获取指定路径点的世界坐标
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// <returns>路径点的世界坐标如果索引无效，则返回 三维向量_Vector3.zero</returns>
        public Vector3 Get_WorldPosition(int index)
        {
            if (index < 0 || index >= PathPoints.Count)
                return Vector3.zero;

            return PathPoints[index].world;
        }
        /// <summary>
        /// 获取指定路径点的局部同级坐标
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// <returns>路径点的局部同级坐标如果索引无效，则返回 三维向量_Vector3.zero</returns>
        public Vector3 Get_AnchoredPosition(int index)
        {
            if (index < 0 || index >= PathPoints.Count)
                return Vector3.zero;

            return PathPoints[index].anchored;
        }
        /// <summary>
        /// 获取指定路径点的入点坐标（贝塞尔曲线的入点）
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// <returns>路径点的入点坐标如果索引无效，则返回 三维向量_Vector3.zero</returns>
        public Vector3 Get_In_Position(int index)
        {
            if (index < 0 || index >= PathPoints.Count)
                return Vector3.zero;
            return PathPoints[index].bezier_in_world;
        }
        /// <summary>
        /// 获取指定路径点的出点坐标（贝塞尔曲线的出点）
        /// </summary>
        /// <param name="index">路径点的索引</param>
        /// <returns>路径点的出点坐标如果索引无效，则返回 三维向量_Vector3.zero</returns>
        public Vector3 Get_Out_Position(int index)
        {
            if (index < 0 || index >= PathPoints.Count) return Vector3.zero;
            return PathPoints[index].bezier_out_world;
        }
        /// <summary>
        /// 获取路径上某点的切线方向
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lengthData"></param>
        /// <param name="progress"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public Vector3 GetTangentOnPath(Vector3[] path, XTween_PathLengthData lengthData, float progress, float delta = 0.01f)
        {
            float prevProgress = Mathf.Clamp01(progress - delta);
            float nextProgress = Mathf.Clamp01(progress + delta);

            Vector3 prevPos = GetPositionOnPath(path, lengthData, prevProgress);
            Vector3 nextPos = GetPositionOnPath(path, lengthData, nextProgress);

            return (nextPos - prevPos).normalized;
        }
        #endregion

        #region 路径处理与长度计算
        /// <summary>
        /// 获取处理后的路径点数组
        /// 根据路径类型（线性、贝塞尔等）生成路径点数组
        /// </summary>
        /// <returns>处理后的路径点数组</returns>
        public Vector3[] GetProcessedPathPoints()
        {
            if (PathPoints == null || PathPoints.Count < 2)
                return Array.Empty<Vector3>();

            XTween_BezierPathPoint[] points = new XTween_BezierPathPoint[PathPoints.Count];
            for (int i = 0; i < PathPoints.Count; i++)
            {
                points[i] = new XTween_BezierPathPoint(
                    PathPoints[i].relative,
                    PathPoints[i].bezier_in,
                    PathPoints[i].bezier_out
                );
                points[i].anchored = PathPoints[i].anchored;
                points[i].world = PathPoints[i].world;
            }

            if (IsClosed && points.Length > 2)
            {
                Array.Resize(ref points, points.Length + 1);
                points[points.Length - 1] = points[0];
            }

            // 3. 根据路径类型生成曲线点
            switch (PathType)
            {
                case XTween_PathType.线性:
                    return ProcessLinearPath(points);
                case XTween_PathType.自动贝塞尔:
                    return ProcessAutoBezierPath(points);
                case XTween_PathType.手动贝塞尔:
                case XTween_PathType.平衡贝塞尔:
                    return ProcessManualBezierPath(points);
                default:
                    Debug.LogError($"Unsupported path type: {PathType}");
                    return Array.Empty<Vector3>();
            }
        }
        /// <summary>
        /// 计算路径的长度数据
        /// </summary>
        /// <param name="path">路径点数组</param>
        /// <returns>路径长度数据，包含每段的长度、累积长度和总长度</returns>
        public XTween_PathLengthData CalculatePathLength(Vector3[] path)
        {
            XTween_PathLengthData data = new XTween_PathLengthData();
            data.segmentLengths = new float[path.Length - 1];
            data.cumulativeLengths = new float[path.Length];
            data.totalLength = 0f;

            for (int i = 0; i < path.Length - 1; i++)
            {
                data.segmentLengths[i] = Vector3.Distance(path[i], path[i + 1]);
                data.cumulativeLengths[i] = data.totalLength;
                data.totalLength += data.segmentLengths[i];
            }
            data.cumulativeLengths[path.Length - 1] = data.totalLength;

            return data;
        }
        /// <summary>
        /// 根据路径进度获取路径上的位置
        /// </summary>
        /// <param name="path">路径点数组</param>
        /// <param name="lengthData">路径长度数据</param>
        /// <param name="progress">路径进度（0到1）</param>
        /// <returns>路径上的位置</returns>
        public Vector3 GetPositionOnPath(Vector3[] path, XTween_PathLengthData lengthData, float progress)
        {
            float pathProgress = progress * lengthData.totalLength;

            int segmentIndex = 0;
            for (int i = 0; i < lengthData.cumulativeLengths.Length - 1; i++)
            {
                if (pathProgress >= lengthData.cumulativeLengths[i] && pathProgress <= lengthData.cumulativeLengths[i + 1])
                {
                    segmentIndex = i;
                    break;
                }
            }

            float segmentStart = lengthData.cumulativeLengths[segmentIndex];
            float segmentEnd = lengthData.cumulativeLengths[segmentIndex + 1];
            float segmentLocalProgress = (pathProgress - segmentStart) / (segmentEnd - segmentStart);

            return Vector3.Lerp(path[segmentIndex], path[segmentIndex + 1], segmentLocalProgress);
        }
        #endregion

        #region 路径点插值与处理
        /// <summary>
        /// 获取路径上的点
        /// 根据路径类型（线性、贝塞尔等）计算路径上的点
        /// </summary>
        /// <param name="startIndex">起始点索引</param>
        /// <param name="endIndex">结束点索引</param>
        /// <param name="t">插值参数（0到1）</param>
        /// <returns>路径上的点</returns>
        public Vector3 GetPointOnPath(int startIndex, int endIndex, float t)
        {
            t = Mathf.Clamp01(t);

            Vector3 p0 = Get_WorldPosition(startIndex);
            Vector3 p1 = Get_WorldPosition(endIndex);

            switch (PathType)
            {
                case XTween_PathType.线性:
                    return Vector3.Lerp(p0, p1, t);
                case XTween_PathType.自动贝塞尔:
                    Vector3 p0_prev = startIndex > 0 ? Get_WorldPosition(startIndex - 1) :
                        (IsClosed ? Get_WorldPosition(PathPoints.Count - 1) : p0);
                    Vector3 p1_next = endIndex < PathPoints.Count - 1 ? Get_WorldPosition(endIndex + 1) :
                        (IsClosed ? Get_WorldPosition(0) : p1);

                    Vector3 tangent1 = 0.5f * (p1 - p0_prev);
                    Vector3 tangent2 = 0.5f * (p1_next - p0);

                    return CalculateBezierPoint(p0, p0 + tangent1 * 0.3f, p1 - tangent2 * 0.3f, p1, t);
                case XTween_PathType.手动贝塞尔:
                case XTween_PathType.平衡贝塞尔:
                    return CalculateBezierPoint(
                        p0,
                        Get_Out_Position(startIndex),
                        Get_In_Position(endIndex),
                        p1,
                        t);
            }

            return Vector3.zero;
        }
        /// <summary>
        /// 处理线性路径，生成路径点数组
        /// </summary>
        /// <param name="points">路径点数组</param>
        /// <returns>处理后的路径点数组</returns>
        private Vector3[] ProcessLinearPath(XTween_BezierPathPoint[] points)
        {
            Vector3[] path = new Vector3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                path[i] = points[i].anchored;
            }
            return path;
        }
        /// <summary>
        /// 处理自动贝塞尔路径，生成路径点数组
        /// </summary>
        /// <param name="points">路径点数组</param>
        /// <returns>处理后的路径点数组</returns>
        private Vector3[] ProcessAutoBezierPath(XTween_BezierPathPoint[] points)
        {
            List<Vector3> path = new List<Vector3>();

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 p0 = points[i].anchored;
                Vector3 p3 = points[i + 1].anchored;

                // 计算自动控制点
                Vector3 p0_prev = i > 0 ? points[i - 1].anchored :
                    (IsClosed ? points[points.Length - 2].anchored : p0);

                Vector3 p3_next = i < points.Length - 2 ? points[i + 2].anchored :
                    (IsClosed ? points[1].anchored : p3);

                Vector3 tangent1 = 0.5f * (p3 - p0_prev);
                Vector3 tangent2 = 0.5f * (p3_next - p0);

                Vector3 p1 = p0 + tangent1 * 0.3f;
                Vector3 p2 = p3 - tangent2 * 0.3f;

                // 采样曲线段
                for (int s = 0; s <= SegmentsPerCurve; s++)
                {
                    float t = s / (float)SegmentsPerCurve;
                    path.Add(CalculateBezierPoint(p0, p1, p2, p3, t));
                }
            }

            return path.ToArray();
        }
        /// <summary>
        /// 处理手动贝塞尔路径，生成路径点数组
        /// </summary>
        /// <param name="points">路径点数组</param>
        /// <returns>处理后的路径点数组</returns>
        private Vector3[] ProcessManualBezierPath(XTween_BezierPathPoint[] points)
        {
            List<Vector3> path = new List<Vector3>();

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 p0 = points[i].anchored;
                Vector3 p1 = p0 + points[i].bezier_out;
                Vector3 p3 = points[i + 1].anchored;
                Vector3 p2 = p3 + points[i + 1].bezier_in;

                // 采样曲线段
                for (int s = 0; s <= SegmentsPerCurve; s++)
                {
                    float t = s / (float)SegmentsPerCurve;
                    path.Add(CalculateBezierPoint(p0, p1, p2, p3, t));
                }
            }

            return path.ToArray();
        }
        /// <summary>
        /// 计算贝塞尔曲线上的点
        /// </summary>
        /// <param name="p0">起始点</param>
        /// <param name="p1">起始点的控制点</param>
        /// <param name="p2">结束点的控制点</param>
        /// <param name="p3">结束点</param>
        /// <param name="t">插值参数（0到1）</param>
        /// <returns>贝塞尔曲线上的点</returns>
        private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            return uuu * p0 +
                   3 * uu * t * p1 +
                   3 * u * tt * p2 +
                   ttt * p3;
        }
        /// <summary>
        /// 计算路径的总长度
        /// </summary>
        /// <returns>路径的总长度</returns>
        public float CalculateTotalLength()
        {
            if (PathPoints.Count < 2) return 0f;

            float totalLength = 0f;

            for (int i = 0; i < PathPoints.Count - 1; i++)
            {
                totalLength += CalculateSegmentLength(i, i + 1, SegmentsPerCurve);
            }

            // 如果是闭合路径，计算最后一个点到第一个点的长度
            if (IsClosed && PathPoints.Count > 2)
            {
                totalLength += CalculateSegmentLength(PathPoints.Count - 1, 0, SegmentsPerCurve);
            }

            return totalLength;
        }
        /// <summary>
        /// 计算路径段的长度
        /// </summary>
        /// <param name="startIndex">起始点索引</param>
        /// <param name="endIndex">结束点索引</param>
        /// <param name="samples">采样点数量</param>
        /// <returns>路径段的长度</returns>
        public float CalculateSegmentLength(int startIndex, int endIndex, int samples)
        {
            samples = Mathf.Max(2, samples); // 至少需要2个采样点

            Vector3 previousPoint = Get_WorldPosition(startIndex);
            float segmentLength = 0f;

            for (int i = 1; i <= samples; i++)
            {
                float t = i / (float)samples;
                Vector3 currentPoint = GetPointOnPath(startIndex, endIndex, t);
                segmentLength += Vector3.Distance(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }

            return segmentLength;
        }
        #endregion

        #region 路径点标记
        /// <summary>
        /// 创建路径点的可视化标记物体
        /// 根据路径捕捉模式（根据路径点或路径线条）创建可视化标记物体
        /// </summary>
        public void PathMarks_Create()
        {
            List<Image> imglist = new List<Image>();

#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(this, "Create Path Visual Objects");
#endif

            #region 加载材质和贴图
#if UNITY_EDITOR
            string path_mat = XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + "Icon/Icons_Hud_XTween_PathTool/XTween_PathMark.mat";
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path_mat);
            if (PathMarksTexture != null)
                mat.mainTexture = PathMarksTexture;
            else
            {
                string path_tex = XTween_Dashboard.Get_path_XTween_GUIStyle_Path() + "Icon/Icons_Hud_XTween_PathTool/PathMark.png";
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path_tex);
                mat.mainTexture = tex;
            }
#endif
            #endregion

            #region 创建父物体
            GameObject markRoot = new GameObject();
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(markRoot, "Create Path Point Root");
#endif
            markRoot.name = "PathMarksGroup";
            markRoot.transform.SetParent(PathParent);
            UnityEngine.RectTransform rect = markRoot.AddComponent<UnityEngine.RectTransform>();
            rect.localEulerAngles = Vector3.zero;
            rect.anchoredPosition3D = transform.localPosition;
            rect.localScale = Vector3.one;
            PathMarksGroup = rect;
            #endregion

            #region 根据不同的方式生成标记物
            if (PathMarksMode == XTween_PathMarksMode.根据路径点)
            {
                for (int i = 0; i < PathPoints.Count; i++)
                {
                    GameObject mark = new GameObject();
                    Image img_mark = mark.AddComponent<Image>();
                    img_mark.raycastTarget = false;
                    img_mark.maskable = false;
#if UNITY_EDITOR
                    Undo.RegisterCreatedObjectUndo(mark, "Create Path Point");
#endif
                    mark.name = $"PathPoint_{i}";
#if UNITY_EDITOR
                    Undo.RecordObject(mark.transform, "Set Path Point Transform");
                    img_mark.rectTransform.SetParent(transform);
                    img_mark.rectTransform.anchoredPosition3D = PathPoints[i].relative;
                    img_mark.rectTransform.localEulerAngles = Vector3.zero;
                    img_mark.rectTransform.sizeDelta = Vector2.one * PathMarksSize;
                    img_mark.rectTransform.localScale = Vector3.one;
                    img_mark.rectTransform.SetParent(rect);
                    img_mark.material = mat;

                    imglist.Add(img_mark);
#endif
                }
            }
            else if (PathMarksMode == XTween_PathMarksMode.根据路径线条 && PathPoints.Count >= 2)
            {
                Vector3[] pathPoints = GetProcessedPathPoints();
                XTween_PathLengthData lengthData = CalculatePathLength(pathPoints);

                for (int i = 0; i < PathMarksSample; i++)
                {
                    float progress = i / (float)(PathMarksSample - 1);
                    Vector3 position = GetPositionOnPath(pathPoints, lengthData, progress);

                    GameObject mark = new GameObject();
                    Image img_mark = mark.AddComponent<Image>();
                    img_mark.raycastTarget = false;
                    img_mark.maskable = false;
#if UNITY_EDITOR
                    Undo.RegisterCreatedObjectUndo(mark, "Create Path Point");
#endif
                    mark.name = $"PathPoint_{i}";
#if UNITY_EDITOR
                    Undo.RecordObject(mark.transform, "Set Path Point Transform");
                    img_mark.rectTransform.SetParent(transform.parent);
                    img_mark.rectTransform.anchoredPosition3D = position;
                    img_mark.rectTransform.localEulerAngles = Vector3.zero;
                    img_mark.rectTransform.sizeDelta = Vector2.one * PathMarksSize;
                    img_mark.rectTransform.localScale = Vector3.one;
                    img_mark.rectTransform.SetParent(rect);
                    img_mark.material = mat;

                    imglist.Add(img_mark);
#endif
                }
            }
            #endregion

            PathMarks = imglist.ToArray();
        }
        /// <summary>
        /// 清除路径点的可视化标记物体
        /// </summary>
        public void PathMarks_Clear()
        {
#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(this, "Create Path Visual Objects");
#endif

            #region 清除所有现有标记物
#if UNITY_EDITOR
            for (int i = 0; i < PathMarks.Length; i++)
            {
                if (PathMarks[i] == null)
                    continue;
                Undo.DestroyObjectImmediate(PathMarks[i].gameObject);
                PathMarks[i] = null;
            }
            PathMarks = null;
#endif

#if UNITY_EDITOR
            if (PathMarksGroup != null)
            {
                Undo.DestroyObjectImmediate(PathMarksGroup.gameObject);
                PathMarksGroup = null;
            }
#endif
            #endregion       
        }
        /// <summary>
        /// 同步标记点尺寸
        /// </summary>
        public void PathMarks_SyncStyle()
        {
            if (PathMarks == null)
                return;
            for (int i = 0; i < PathMarks.Length; i++)
            {
                if (PathMarks[i] == null)
                    continue;
                PathMarks[i].rectTransform.sizeDelta = Vector2.one * PathMarksSize;
                PathMarks[i].color = PathMarksColor;
            }

            if (!IsWorldMode && PathMarksGroup != null)
                PathMarksGroup.position = transform.position;
        }
        #endregion
    }
}