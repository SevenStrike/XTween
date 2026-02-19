using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MoverTweener
{
    public RectTransform rect;
    public Text text;
    public Image img;
    [Header("---pos")]
    public bool use_pos;
    public bool initial_pos;
    public bool durationAdd_pos;
    public float duration_pos;
    public float delay_pos;
    public EaseMode ease_pos;
    public bool usecurve_pos;
    public AnimationCurve curve_pos;
    public Vector3 from_pos;
    public Vector3 target_pos;

    [Header("---rot")]
    public bool use_rot;
    public bool initial_rot;
    public bool durationAdd_rot;
    public float duration_rot;
    public float delay_rot;
    public EaseMode ease_rot;
    public bool usecurve_rot;
    public AnimationCurve curve_rot;
    public Vector3 from_rot;
    public Vector3 target_rot;

    [Header("---col")]
    public bool use_col;
    public bool initial_col;
    public bool durationAdd_col;
    public float duration_col;
    public float delay_col;
    public EaseMode ease_col;
    public bool usecurve_col;
    public AnimationCurve curve_col;
    public Color from_col;
    public Color target_col;

    public float delay_multi;

    public XTween_Interface tween_pos;
    public XTween_Interface tween_rot;
    public XTween_Interface tween_color;
}

public class demo_mover_Text : demo_base
{
    public MoverTweener[] textTweeners_Faded;

    public override void Start()
    {
        base.Start();

        Tween_Create();
    }

    public override void Update()
    {
        base.Update();
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        base.Tween_Create();

        CreateTween_Move();
        CreateTween_Color();
        CreateTween_Rot();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        base.Tween_Play();
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        base.Tween_Rewind();
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        base.Tween_Pause_Or_Resume();
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        base.Tween_Kill();
    }
    #endregion

    #region 辅助
    public void CollectTweens(MoverTweener[] tweens)
    {
        List<MoverTweener> list = new List<MoverTweener>();
        // 先收集原有的
        for (int i = 0; i < textTweeners_Faded.Length; i++)
        {
            list.Add(textTweeners_Faded[i]);
        }
        // 收集传入的动画参数
        for (int i = 0; i < tweens.Length; i++)
        {
            list.Add(tweens[i]);
        }
        // 替换原有动画列表
        textTweeners_Faded = list.ToArray();
    }
    /// <summary>
    /// 创建动画 - 颜色
    /// </summary>
    /// <param name="tweener"></param>
    private void CreateTween_Color()
    {
        for (int i = 0; i < textTweeners_Faded.Length; i++)
        {
            MoverTweener t = textTweeners_Faded[i];
            if (t.use_col)
            {
                if (t.img != null)
                {
                    if (t.initial_col)
                        t.img.color = t.from_col;
                    if (t.usecurve_col)
                        t.tween_color = t.img.xt_Color_To(t.target_col, t.durationAdd_col ? t.duration_col + duration : t.duration_col, true, false, false).SetEase(t.curve_col).SetDelay(t.delay_col + t.delay_multi + delay).Play();
                    else
                        t.tween_color = t.img.xt_Color_To(t.target_col, t.durationAdd_col ? t.duration_col + duration : t.duration_col, true, false, false).SetEase(t.ease_col).SetDelay(t.delay_col + t.delay_multi + delay).Play();
                }
                if (t.text != null)
                {
                    if (t.initial_col)
                        t.text.color = t.from_col;
                    if (t.usecurve_col)
                        t.tween_color = t.text.xt_FontColor_To(t.target_col, t.durationAdd_col ? t.duration_col + duration : t.duration_col, true, false, false).SetEase(t.curve_col).SetDelay(t.delay_col + t.delay_multi + delay).Play();
                    else
                        t.tween_color = t.text.xt_FontColor_To(t.target_col, t.durationAdd_col ? t.duration_col + duration : t.duration_col, true, false, false).SetEase(t.ease_col).SetDelay(t.delay_col + t.delay_multi + delay).Play();
                }
            }
        }
    }
    /// <summary>
    /// 创建动画 - 运动
    /// </summary>
    /// <param name="tweener"></param>
    private void CreateTween_Move()
    {
        for (int i = 0; i < textTweeners_Faded.Length; i++)
        {
            MoverTweener t = textTweeners_Faded[i];
            if (t.use_pos)
            {
                if (t.initial_pos)
                    t.rect.anchoredPosition3D = t.from_pos;
                if (t.usecurve_pos)
                    t.tween_pos = t.rect.xt_AnchoredPosition3D_To(t.target_pos, t.durationAdd_pos ? t.duration_pos + duration : t.duration_pos, false, true).SetEase(t.curve_pos).SetDelay(t.delay_pos + t.delay_multi + delay).Play();
                else
                    t.tween_pos = t.rect.xt_AnchoredPosition3D_To(t.target_pos, t.durationAdd_pos ? t.duration_pos + duration : t.duration_pos, false, true).SetEase(t.ease_pos).SetDelay(t.delay_pos + t.delay_multi + delay).Play();
            }
        }
    }
    /// <summary>
    /// 创建动画 - 旋转
    /// </summary>
    /// <param name="tweener"></param>
    private void CreateTween_Rot()
    {

    }
    #endregion
}
