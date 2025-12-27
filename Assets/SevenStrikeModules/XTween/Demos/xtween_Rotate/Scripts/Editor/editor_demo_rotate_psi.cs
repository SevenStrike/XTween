using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_rotate_psi))]
public class editor_demo_rotate_psi : editor_demo_base
{
    private demo_rotate_psi demo_electronic;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_electronic = target as demo_rotate_psi;
    }
    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        // 按钮 - 切换目标颜色
        Button btn_next = demoGUI_Button("渐变到下一个颜色", Color.green);
        btn_next.clicked += (() =>
        {
            editor_btn_clicked_NextColor();
        });

        root.Add(btn_next);

        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 切换到下一个颜色
    /// </summary>
    private void editor_btn_clicked_NextColor()
    {
        if (Application.isPlaying)
        {
            //  重新创建动画
            demo_electronic.Tweener_Restart();
        }
    }
    /// <summary>
    /// 按钮事件 - 创建并播放
    /// </summary>
    public override void editor_btn_clicked_CreateAndPlay()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            if (demo_electronic.currentTweener == null)
            {   // 创建动画
                demo_electronic.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_electronic.Tween_Rewind();
            }

            // 播放动画
            demo_electronic.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_electronic.Tween_Create();
            AppendToPreviewer(demo_electronic.tween_TOG);
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