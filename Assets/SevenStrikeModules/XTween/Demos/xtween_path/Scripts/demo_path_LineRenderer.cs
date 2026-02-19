using SevenStrikeModules.XTween;
using UnityEngine;

public class demo_path_LineRenderer : MonoBehaviour
{
    [SerializeField] public XTween_PathTool pathTool;
    [SerializeField] public LineRenderer lineRenderer;
    [SerializeField] public bool updateInRealTime = true;
    [SerializeField] public float roadwidth = 0.1f;
    [SerializeField] public Color roadcolor_start = Color.cyan;
    [SerializeField] public Color roadcolor_end = Color.red;

    private void Start()
    {
        if (pathTool != null)
        {
            pathTool.act_on_pathChanged += OnPathChanged;
        }

        RefreshPath();
    }

    private void OnPathChanged()
    {
        RefreshPath();
    }

    public void RefreshPath()
    {
        if (pathTool == null || lineRenderer == null) return;

        Vector3[] points = pathTool.GetProcessedPathPoints();
        if (points == null || points.Length < 2) return;

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

        LineRenderUpdate();
    }

    public void LineRenderUpdate()
    {
        // 应用样式
        lineRenderer.startColor = roadcolor_start;
        lineRenderer.endColor = roadcolor_end;
        lineRenderer.startWidth = roadwidth * 0.01f;
        lineRenderer.endWidth = roadwidth * 0.01f;
        lineRenderer.loop = pathTool.IsClosed;
    }

    private void OnDestroy()
    {
        if (pathTool != null)
        {
            pathTool.act_on_pathChanged -= OnPathChanged;
        }
    }
}