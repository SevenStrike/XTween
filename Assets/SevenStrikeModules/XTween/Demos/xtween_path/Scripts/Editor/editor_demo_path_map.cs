using SevenStrikeModules.XTween.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_path_map))]
public class editor_demo_path_map : editor_demo_base
{
    private demo_path_map demo_path;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_path = target as demo_path_map;
    }
    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 按钮事件 - 创建并播放
    /// </summary>
    public override void editor_btn_clicked_CreateAndPlay()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            if (demo_path.currentTweener == null)
            {   // 创建动画
                demo_path.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_path.Tween_Rewind();
            }

            // 播放动画
            demo_path.Tween_Play();
        }
        //如果没运行
        else
        {
            if (Editor_XTween_Previewer.TweenIsPreviewing())
                return;

            // 创建动画
            demo_path.Tween_Create();
            // 预览动画
            Preview_Start();
        }
    }
    /// <summary>
    /// 按钮事件 - 倒退
    /// </summary>
    public override void editor_btn_clicked_Rewind()
    {
        base.editor_btn_clicked_Rewind();
    }
    /// <summary>
    /// 按钮事件 - 暂停或继续
    /// </summary>
    public override void editor_btn_clicked_PauseOrContinue()
    {
        base.editor_btn_clicked_PauseOrContinue();
    }
    /// <summary>
    /// 按钮事件 - 杀死
    /// </summary>
    public override void editor_btn_clicked_Kill()
    {
        base.editor_btn_clicked_Kill();
    }
    #endregion
}