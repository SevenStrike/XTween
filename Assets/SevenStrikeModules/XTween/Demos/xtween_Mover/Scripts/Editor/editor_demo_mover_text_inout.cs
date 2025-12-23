using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_mover_text_inout))]
public class editor_demo_mover_text_inout : editor_demo_base
{
    private demo_mover_text_inout demo_mover;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_mover = target as demo_mover_text_inout;
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
        //base.editor_btn_clicked_CreateAndPlay();

        // 如果未在运行直接返回不执行
        if (!Application.isPlaying) return;

        // 检查动画是否已经生成
        if (!demo_mover.HasActiveTweens())
        {
            // 创建动画
            demo_mover.Tween_Create();
        }
        else
        {
            // 倒退动画
            demo_mover.Tween_Rewind();
        }

        // 播放动画
        demo_mover.Tween_Play();
    }
    /// <summary>
    /// 按钮事件 - 倒退
    /// </summary>
    public override void editor_btn_clicked_Rewind()
    {
        //base.editor_btn_clicked_Rewind();
        if (Application.isPlaying)
            demo_mover.Tween_Rewind();
    }
    /// <summary>
    /// 按钮事件 - 暂停或继续
    /// </summary>
    public override void editor_btn_clicked_PauseOrContinue()
    {
        //base.editor_btn_clicked_PauseOrContinue();
        if (Application.isPlaying)
            demo_mover.Tween_Pause_Or_Resume();
    }
    /// <summary>
    /// 按钮事件 - 杀死
    /// </summary>
    public override void editor_btn_clicked_Kill()
    {
        //base.editor_btn_clicked_Kill();
        if (Application.isPlaying)
            demo_mover.Tween_Kill();
    }
    #endregion
}