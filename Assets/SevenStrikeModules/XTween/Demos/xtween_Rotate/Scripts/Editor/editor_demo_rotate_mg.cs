using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_rotate_mg))]
public class editor_demo_rotate_mg : editor_demo_base
{
    private demo_rotate_mg demo_mg;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_mg = target as demo_rotate_mg;
    }
    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        if (Application.isPlaying)
        {
            // 按钮 - 切换目标颜色
            Button btn_plus = demoGUI_Button("增加浓度", Color.green);
            btn_plus.clicked += (() =>
            {
                editor_btn_clicked_plusmg();
            });

            // 按钮 - 切换目标颜色
            Button btn_minus = demoGUI_Button("减少浓度", Color.green);
            btn_minus.clicked += (() =>
            {
                editor_btn_clicked_minusmg();
            });

            // 按钮 - 切换目标颜色
            Button btn_reset = demoGUI_Button("重置浓度", Color.green);
            btn_reset.clicked += (() =>
            {
                editor_btn_clicked_resetmg();
            });

            root.Add(btn_plus);
            root.Add(btn_minus);
            root.Add(btn_reset);
        }
        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 浓度 - 增加
    /// </summary>
    private void editor_btn_clicked_plusmg()
    {
        if (Application.isPlaying)
        {
            demo_mg.value_forward();
            demo_mg.Tweener_Restart();
        }
    }

    /// <summary>
    /// 浓度 - 减少
    /// </summary>
    private void editor_btn_clicked_minusmg()
    {
        if (Application.isPlaying)
        {
            demo_mg.value_backward();
            demo_mg.Tweener_Restart();
        }
    }

    /// <summary>
    /// 浓度 - 重置
    /// </summary>
    private void editor_btn_clicked_resetmg()
    {
        if (Application.isPlaying)
        {
            demo_mg.value_reset();
            demo_mg.Tween_Create();
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
            if (demo_mg.currentTweener == null)
            {   // 创建动画
                demo_mg.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_mg.Tween_Rewind();
            }

            // 播放动画
            demo_mg.Tween_Play();
        }
        //如果没运行
        else
        {
            demo_mg.target_MG = -demo_mg.target_Step;
            // 创建动画
            demo_mg.Tween_Create();
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