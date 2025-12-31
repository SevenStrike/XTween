using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_scale_machine))]
public class editor_demo_scale_machine : editor_demo_base
{
    private demo_scale_machine demo_scale;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_scale = target as demo_scale_machine;
    }

    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        if (Application.isPlaying)
        {
            // 按钮 - 切换目标颜色
            Button btn_open = demoGUI_Button("显示", Color.green);
            btn_open.clicked += (() =>
            {
                editor_btn_clicked_open();
            });

            // 按钮 - 切换目标颜色
            Button btn_close = demoGUI_Button("隐藏", Color.green);
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
            demo_scale.dir = "display";
            demo_scale.Tween_Create();
            demo_scale.Tween_Play();
        }
    }
    /// <summary>
    /// 合上
    /// </summary>
    private void editor_btn_clicked_close()
    {
        if (Application.isPlaying)
        {
            demo_scale.dir = "hidden";
            demo_scale.Tween_Create();
            demo_scale.Tween_Play();
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
            if (!demo_scale.HasActiveTweens())
            {   // 创建动画
                demo_scale.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_scale.Tween_Rewind();
            }

            // 播放动画
            demo_scale.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_scale.Tween_Create();

            for (int i = 0; i < demo_scale.machinetweens.Count; i++)
            {
                AppendToPreviewer(demo_scale.machinetweens[i].tween);
            }
            for (int i = 0; i < demo_scale.scaleRottweens.Count; i++)
            {
                AppendToPreviewer(demo_scale.scaleRottweens[i].tween);
            }
            AppendToPreviewer(demo_scale.rulertween.tween);
            AppendToPreviewer(demo_scale.probtween.tween);
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
            foreach (var tweener in demo_scale.machinetweens)
            {
                tweener.id = null;
            }

            foreach (var tweener in demo_scale.scaleRottweens)
            {
                tweener.id = null;
            }

            Preview_Kill(false);

            foreach (var tweener in demo_scale.machinetweens)
            {
                tweener.img.rectTransform.localScale = tweener.target;
            }

            foreach (var tweener in demo_scale.scaleRottweens)
            {
                tweener.img.rectTransform.eulerAngles = Vector3.zero;
            }

            demo_scale.rulertween.img.rectTransform.eulerAngles = Vector3.forward * 210;

            demo_scale.probtween.img.color = demo_scale.dir == "display" ? demo_scale.probtween.target : demo_scale.probtween.from;
            demo_scale.sws = false;
        }

    }
    #endregion
}