using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(demo_text_NavigateOptions))]
public class editor_demo_text_NavigateOptions : editor_demo_base
{
    private demo_text_NavigateOptions demo_text_nav;

    public override void OnEnable()
    {
        base.OnEnable();

        demo_text_nav = target as demo_text_NavigateOptions;
    }

    public override VisualElement ExtensionsDraw()
    {
        VisualElement root = base.ExtensionsDraw();

        demoGUI_Line(root);

        // 按钮 - 创建动画
        Button btn_init = demoGUI_Button("初始化", Color.green);
        btn_init.clicked += (() =>
        {
            demo_text_nav.Initialized();
        });
        root.Add(btn_init);

        // 按钮集合容器
        VisualElement root_btns = new VisualElement
        {
            style = {
                flexDirection = new StyleEnum<FlexDirection>( FlexDirection.Row),
                flexWrap = new StyleEnum<Wrap>(Wrap.Wrap)
            }
        };

        // 按钮 - 创建动画
        Button btn_prev = demoGUI_Button("上一项", Color.green);
        btn_prev.clicked += (() =>
        {
            demo_text_nav.Opt_Prev();
        });

        // 按钮 - 创建动画
        Button btn_next = demoGUI_Button("下一项", Color.red);
        btn_next.clicked += (() =>
        {
            demo_text_nav.Opt_Next();
        });

        root_btns.Add(btn_prev);
        root_btns.Add(btn_next);

        root.Add(root_btns);
        return root;
    }

    #region 按钮动作 - 重写
    /// <summary>
    /// 按钮事件 - 创建并播放
    /// </summary>
    public override void editor_btn_clicked_CreateAndPlay()
    {
        base.editor_btn_clicked_CreateAndPlay();
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