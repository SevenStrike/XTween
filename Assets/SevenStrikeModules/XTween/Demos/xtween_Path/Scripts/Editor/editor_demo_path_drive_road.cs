using UnityEngine;
using UnityEditor;
using SevenStrikeModules.XTween;

[CustomEditor(typeof(demo_path_drive_road))]
public class editor_demo_path_drive_road : Editor
{
    private demo_path_drive_road road_generator;

    private void OnEnable()
    {
        road_generator = (demo_path_drive_road)target;

        UpdateLineRenderer();

        if (road_generator.pathTool != null)
        {
            road_generator.pathTool.act_on_pathChanged -= UpdateLineRenderer;
            road_generator.pathTool.act_on_pathChanged += UpdateLineRenderer;
        }
    }

    private void UpdateLineRenderer()
    {
        if (road_generator.pathTool != null && !Application.isPlaying)
        {
            road_generator.RefreshPath();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UpdateLineRenderer();
    }
}
