using SevenStrikeModules.XTween;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_path_drive))]
public class editor_demo_path_drive : editor_demo_base
{
    private demo_path_drive demo_path;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_path = target as demo_path_drive;
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
            // 创建动画
            demo_path.Tween_Create();

            demo_path.rotateTween.SetLoopType(XTween_LoopType.Restart).SetLoop(-1).SetLoopingDelay(demo_path.loopDelay);
            demo_path.alphaTween.SetLoopType(XTween_LoopType.Restart).SetLoop(-1).SetLoopingDelay(demo_path.loopDelay);

            AppendToPreviewer(demo_path.rotateTween);
            AppendToPreviewer(demo_path.alphaTween);
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
        if (!Application.isPlaying)
            demo_path.ClearGhost();
    }
    #endregion
}