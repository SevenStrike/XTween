using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_fill_map))]
public class editor_demo_fill_map : editor_demo_base
{
    private demo_fill_map demo_fill;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_fill = target as demo_fill_map;
    }

    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        if (Application.isPlaying)
        {
            // 按钮 - 切换目标颜色
            Button btn_open = demoGUI_Button("展开", Color.green);
            btn_open.clicked += (() =>
            {
                editor_btn_clicked_open();
            });

            // 按钮 - 切换目标颜色
            Button btn_close = demoGUI_Button("合上", Color.green);
            btn_close.clicked += (() =>
            {
                editor_btn_clicked_close();
            });

            root.Add(btn_open);
            root.Add(btn_close);
        }
        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 展开
    /// </summary>
    private void editor_btn_clicked_open()
    {
        if (Application.isPlaying)
        {
            demo_fill.dir = "open";
            demo_fill.Tween_Create();
            demo_fill.Tween_Play();
        }
    }
    /// <summary>
    /// 合上
    /// </summary>
    private void editor_btn_clicked_close()
    {
        if (Application.isPlaying)
        {
            demo_fill.dir = "close";
            demo_fill.Tween_Create();
            demo_fill.Tween_Play();
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
            if (demo_fill.mapscroller.tween == null && !demo_fill.HasActiveTweens())
            {   // 创建动画
                demo_fill.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_fill.Tween_Rewind();
            }

            // 播放动画
            demo_fill.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_fill.Tween_Create();

            AppendToPreviewer(demo_fill.mapscroller.tween);

            for (int i = 0; i < demo_fill.maptweens.Count; i++)
            {
                AppendToPreviewer(demo_fill.maptweens[i].tween);
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
        //base.editor_btn_clicked_Kill();

        // 如果在运行
        if (Application.isPlaying)
        {
            demo_base.Tween_Kill();
        }
        //如果没运行
        else
        {
            demo_fill.mapscroller.id = null;

            foreach (var tweener in demo_fill.maptweens)
            {
                tweener.id = null;
            }

            Preview_Kill(false);
        }

    }
    #endregion
}