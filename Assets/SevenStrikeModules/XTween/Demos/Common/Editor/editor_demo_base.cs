using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

[CustomEditor(typeof(demo_base), true)]
public class editor_demo_base : Editor
{
    internal demo_base demo_base;
    /// <summary>
    /// 附加的预览动画列表
    /// </summary>
    [SerializeField] public List<XTween_Interface> PreviewListAddon = new List<XTween_Interface>();

    public virtual void OnEnable()
    {
        demo_base = target as demo_base;
    }

    private void OnDisable()
    {
        Preview_Kill();
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        // 首先添加默认的属性字段
        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        demoGUI_Line(root);

        // 按钮集合容器
        VisualElement root_btns = new VisualElement
        {
            style = {
                flexDirection = new StyleEnum<FlexDirection>( FlexDirection.Row),
                flexWrap = new StyleEnum<Wrap>(Wrap.Wrap)
            }
        };

        // 按钮 - 创建动画
        Button btn_create = demoGUI_Button(Application.isPlaying ? "创建" : "预览", Color.green);
        btn_create.clicked += (() =>
        {
            editor_btn_clicked_CreateAndPlay();
        });

        // 按钮 - 倒退动画
        Button btn_rewind = demoGUI_Button("倒退", Color.white);
        btn_rewind.clicked += (() =>
        {
            editor_btn_clicked_Rewind();
        });

        // 按钮 - 杀死动画
        Button btn_kill = demoGUI_Button("杀死", Color.red);
        btn_kill.clicked += (() =>
        {
            editor_btn_clicked_Kill();
        });

        // 按钮 - 暂停&继续动画
        Button btn_pause_continue = demoGUI_Button("暂停&继续", Color.yellow);
        btn_pause_continue.clicked += (() =>
        {
            editor_btn_clicked_PauseOrContinue();
        });

        // 加入按钮控件到面板
        root_btns.Add(btn_create);
        root_btns.Add(btn_rewind);
        root_btns.Add(btn_kill);
        root_btns.Add(btn_pause_continue);
        root.Add(root_btns);
        root.Add(ExtensionsDraw());

        return root;
    }

    #region 按钮动作
    /// <summary>
    /// 按钮事件 - 创建并播放
    /// </summary>
    public virtual void editor_btn_clicked_CreateAndPlay()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            if (demo_base.currentTweener == null)
            {   // 创建动画
                demo_base.Tween_Create();
            }
            else
            {
                // 倒退动画
                demo_base.Tween_Rewind();
            }
            // 播放动画
            demo_base.Tween_Play();
        }
        //如果没运行
        else
        {
            // 创建动画
            demo_base.Tween_Create();
            // 预览动画
            Preview_Start();
        }
    }
    /// <summary>
    /// 按钮事件 - 倒退
    /// </summary>
    public virtual void editor_btn_clicked_Rewind()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            demo_base.Tween_Rewind();
        }
        //如果没运行
        else
        {
            Preview_Rewind();
        }
    }
    /// <summary>
    /// 按钮事件 - 暂停或继续
    /// </summary>
    public virtual void editor_btn_clicked_PauseOrContinue()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            demo_base.Tween_Pause_Or_Resume();
        }
    }
    /// <summary>
    /// 按钮事件 - 杀死
    /// </summary>
    public virtual void editor_btn_clicked_Kill()
    {
        // 如果在运行
        if (Application.isPlaying)
        {
            demo_base.Tween_Kill();
        }
        //如果没运行
        else
        {
            Preview_Kill();
        }
    }
    #endregion

    #region 自定义扩展控件
    /// <summary>
    /// 由派生类完成绘制
    /// </summary>
    /// <returns></returns>
    public virtual VisualElement ExtensionsDraw()
    {
        VisualElement root_extensions = new VisualElement();

        return root_extensions;
    }
    #endregion

    #region GUI
    /// <summary>
    /// 创建GUI按钮
    /// </summary>
    /// <param name="name"></param>
    /// <param name="enable"></param>
    /// <returns></returns>
    public Button demoGUI_Button(string name, Color color)
    {
        Button btn = new Button();
        btn.text = name;
        btn.style.height = 25;
        btn.style.fontSize = 12;
        btn.style.paddingLeft = 10;
        btn.style.flexGrow = 1;
        btn.style.paddingRight = 10;
        btn.style.unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold);
        VisualElement mark = new VisualElement
        {
            style ={
                width = 3,
                height = 3,
                position = new StyleEnum<Position>( Position.Absolute),
                right = 5,
                top=5,
                backgroundColor = color,
            }
        };
        btn.Add(mark);

        return btn;
    }
    /// <summary>
    /// 创建GUI分割线
    /// </summary>
    /// <param name="root"></param>
    public void demoGUI_Line(VisualElement root)
    {
        // 添加一个分隔线
        root.Add(new VisualElement
        {
            style =
                {
                    height = 1,
                    backgroundColor = new Color(0.3f, 0.3f, 0.3f),
                    marginTop = 10,
                    marginBottom = 10
                }
        });
    }
    #endregion

    #region 预览方法
    /// <summary>
    ///  动画预览 - 倒退
    /// </summary>
    public void Preview_Rewind()
    {
        if (Application.isPlaying)
            return;
        XTween_Previewer.Rewind();

        if (demo_base.debug)
        {
            Debug.Log($"Tween Rewind");
            XTween_Pool.LogStatistics(demo_base.debug);
        }
    }
    /// <summary>
    ///  动画预览 - 杀死
    /// </summary>
    public void Preview_Kill()
    {
        if (Application.isPlaying)
            return;
        if (demo_base.currentTweener == null)
            return;

        XTween_Previewer.act_on_editor_autokill -= OnAutoKillPreview;

        XTween_Previewer.Kill(XTween_Previewer.AfterKillClear, XTween_Previewer.BeforeKillRewind, () =>
        {
            demo_base.currentTweener = null;
            PreviewListAddon.Clear();
        });

        if (demo_base.debug)
        {
            Debug.Log($"Tween Kill");
            XTween_Pool.LogStatistics(demo_base.debug);
        }
    }
    /// <summary>
    ///  动画预览 - 开始
    /// </summary>
    public void Preview_Start()
    {
        if (Application.isPlaying)
            return;

        if (demo_base.currentTweener == null)
            return;

        // 当动画预览器为根据动画耗时自动杀死的情况下
        if (demo_base.currentTweener.AutoKill)
        {
            XTween_Previewer.AfterKillClear = true;
            XTween_Previewer.BeforeKillRewind = true;
            XTween_Previewer.act_on_editor_autokill += OnAutoKillPreview;
        }

        #region 添加至预览器并播放
        XTween_Previewer.Append(demo_base.currentTweener);
        for (int i = 0; i < PreviewListAddon.Count; i++)
        {
            XTween_Previewer.Append(PreviewListAddon[i]);
        }
        XTween_Previewer.Play(null, demo_base.debug);
        #endregion

        if (demo_base.debug)
        {
            Debug.Log($"Tween Play");
            XTween_Pool.LogStatistics(demo_base.debug);
        }
    }

    public void AppendToPreviewer(XTween_Interface tween)
    {
        PreviewListAddon.Add(tween);
    }

    /// <summary>
    /// 杀死预览动画后的操作逻辑
    /// </summary>
    private void OnAutoKillPreview()
    {
        demo_base.currentTweener = null;
        PreviewListAddon.Clear();
        XTween_Previewer.act_on_editor_autokill -= OnAutoKillPreview;
    }
    #endregion
}