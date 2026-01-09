using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class infinityArgs
{
    public RectTransform rect;
    public Text text;
    public Vector2 size;
    public float duration;
    public Color color;
    public float delay;
    public float loopdelay;
    public string id;
    public XTween_Interface tween;
    public XTween_Interface tween_alpha;
}

public class demo_size_infinity : demo_base
{
    public infinityArgs[] shapes;

    public override void Start()
    {
        base.Start();

        Tween_Create();
        Tween_Play();
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
        for (int i = 0; i < shapes.Length; i++)
        {
            CreateTween_Size(shapes[i]);
        }
        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        base.Tween_Play();

        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].tween.Play();
            if (shapes[i].text != null)
                shapes[i].tween_alpha.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        base.Tween_Rewind();

        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].tween.Rewind();
            if (shapes[i].text != null)
                shapes[i].tween_alpha.Rewind();
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        base.Tween_Pause_Or_Resume();

        for (int i = 0; i < shapes.Length; i++)
        {
            if (shapes[i].tween != null)
            {
                if (shapes[i].tween.IsPlaying)
                {
                    shapes[i].tween.Pause();
                    if (shapes[i].text != null)
                        shapes[i].tween_alpha.Pause();
                }
                else
                {
                    shapes[i].tween.Resume();
                    if (shapes[i].text != null)
                        shapes[i].tween_alpha.Resume();
                }
            }
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        base.Tween_Kill();

        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].tween.Kill();
            if (shapes[i].text != null)
                shapes[i].tween_alpha.Kill();
        }
    }
    #endregion

    #region 尺寸动画
    /// <summary>
    /// 创建动画 - 尺寸
    /// </summary>
    /// <param name="twn"></param>
    public void CreateTween_Size(infinityArgs twn)
    {
        twn.tween = twn.rect.xt_Size_To(twn.size, duration * twn.duration, isRelative, isAutoKill).SetEase(easeMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay + twn.loopdelay).SetDelay(delay + twn.delay);

        if (twn.text != null)
            twn.tween_alpha = twn.text.xt_FontColor_To(twn.color, duration * twn.duration, isAutoKill).SetEase(easeMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay + twn.loopdelay).SetDelay(delay + twn.delay);

        twn.id = twn.tween.ShortId;
    }
    #endregion
}
