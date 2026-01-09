using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_size_infinity))]
public class editor_demo_size_infinity : editor_demo_base
{
    private demo_size_infinity demo_size;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_size = target as demo_size_infinity;
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
            if (demo_size.currentTweener == null)
            {   // 创建动画
                demo_size.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_size.Tween_Rewind();
            }
            // 播放动画
            demo_size.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_size.Tween_Create();
            for (int i = 0; i < demo_size.shapes.Length; i++)
            {
                AppendToPreviewer(demo_size.shapes[i].tween);
                if (demo_size.shapes[i].text != null)
                    AppendToPreviewer(demo_size.shapes[i].tween_alpha);
            }
            // 预览动画
            Preview_Start(false);
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

        // 如果在运行
        if (Application.isPlaying)
        {
            demo_size.Tween_Kill();
        }
        //如果没运行
        else
        {
            Preview_Kill(false);

            for (int i = 0; i < demo_size.shapes.Length; i++)
            {
                demo_size.shapes[i].id = null;
            }
        }
    }
    #endregion
}