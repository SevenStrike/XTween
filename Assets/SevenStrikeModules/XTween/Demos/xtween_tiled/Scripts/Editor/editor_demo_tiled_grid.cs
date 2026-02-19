using SevenStrikeModules.XTween;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_tiled_grid))]
public class editor_demo_tiled_grid : editor_demo_base
{
    private demo_tiled_grid demo_tiled;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_tiled = target as demo_tiled_grid;
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
            if (!demo_tiled.HasActiveTweens())
            {   // 创建动画
                demo_tiled.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_tiled.Tween_Rewind();
            }

            // 播放动画
            demo_tiled.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_tiled.Tween_Create();

            for (int i = 0; i < demo_tiled.gridtweens.Length; i++)
            {
                AppendToPreviewer(demo_tiled.gridtweens[i].tween);
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

        for (int i = 0; i < demo_tiled.gridtweens.Length; i++)
        {
            demo_tiled.gridtweens[i].tween.Rewind();
        }
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
            demo_tiled.Tween_Kill();
        }
        //如果没运行
        else
        {
            demo_tiled.ShortID = null;

            foreach (var tweener in demo_tiled.gridtweens)
            {
                tweener.tween.Kill();
                tweener.target.pixelsPerUnitMultiplier = tweener.original;
                tweener.id = null;
            }

            Preview_Kill(false);
        }

    }
    #endregion  
}