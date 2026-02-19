using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_sequencer))]
public class editor_demo_sequencer : editor_demo_base
{
    private demo_sequencer demo_seq;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_seq = target as demo_sequencer;
    }

    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        if (Application.isPlaying)
        {
            // 按钮 - 切换目标颜色
            Button btn_play = demoGUI_Button("正放", Color.green);
            btn_play.clicked += (() =>
            {
                editor_btn_clicked_play();
            });

            // 按钮 - 切换目标颜色
            Button btn_prev = demoGUI_Button("倒放", Color.green);
            btn_prev.clicked += (() =>
            {
                editor_btn_clicked_prev();
            });

            // 按钮 - 切换目标颜色
            Button btn_stop = demoGUI_Button("停止", Color.green);
            btn_stop.clicked += (() =>
            {
                editor_btn_clicked_stop();
            });

            // 按钮 - 切换目标颜色
            Button btn_rewind = demoGUI_Button("重置", Color.green);
            btn_rewind.clicked += (() =>
            {
                editor_btn_clicked_rewind();
            });

            root.Add(btn_play);
            root.Add(btn_prev);
            root.Add(btn_stop);
            root.Add(btn_rewind);
        }
        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 正放
    /// </summary>
    private void editor_btn_clicked_play()
    {
        if (Application.isPlaying)
        {
            demo_seq.dir = "play";
            demo_seq.Tween_Create();
            demo_seq.Tween_Play();
        }
    }
    /// <summary>
    /// 倒放
    /// </summary>
    private void editor_btn_clicked_prev()
    {
        if (Application.isPlaying)
        {
            demo_seq.dir = "prev";
            demo_seq.Tween_Create();
            demo_seq.Tween_Play();
        }
    }
    /// <summary>
    /// 停止
    /// </summary>
    private void editor_btn_clicked_stop()
    {
        if (Application.isPlaying)
        {
            demo_seq.Tween_Kill();
        }
    }
    /// <summary>
    /// 倒退
    /// </summary>
    private void editor_btn_clicked_rewind()
    {
        if (Application.isPlaying)
        {
            demo_seq.Tween_Rewind();
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
            if (demo_seq.currentTweener == null)
            {   // 创建动画
                demo_seq.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_seq.Tween_Rewind();
            }
            // 播放动画
            demo_seq.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_seq.Tween_Create();
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

        // 如果在运行
        if (Application.isPlaying)
        {
            demo_seq.Tween_Kill();
        }
        //如果没运行
        else
        {
            Preview_Kill();
            if (demo_seq.dir == "play")
                demo_seq.index = 0;
            else
                demo_seq.index = demo_seq.sprites.Length - 1;

            demo_seq.image.sprite = demo_seq.sprites[demo_seq.index];
            demo_seq.text_index.text = demo_seq.index.ToString();

            if (demo_seq.dir == "play")
                demo_seq.progress.fillAmount = 0;
            else
                demo_seq.progress.fillAmount = 1;
        }
    }
    #endregion
}