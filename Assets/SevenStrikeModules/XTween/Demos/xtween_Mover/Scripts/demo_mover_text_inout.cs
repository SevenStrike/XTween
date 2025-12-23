using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextTweener
{
    public Text text;

    [SerializeField] public Vector2 moveTarget;
    [SerializeField] public Vector2 moveFrom;
    [SerializeField] public string moveID;
    [SerializeField] public XTween_Interface moveTween;

    [SerializeField] public Color colorTarget;
    [SerializeField] public Color colorFrom;
    [SerializeField] public string colorID;
    [SerializeField] public XTween_Interface colorTween;
}

public class demo_mover_text_inout : demo_base
{
    public List<TextTweener> tweens = new List<TextTweener>();

    public bool autoStart;

    public override void Start()
    {
        base.Start();

        // 初始化所有文本的初始颜色
        InitializeTextColors();

        if (autoStart)
        {
            CreateAndPlayTweens();
        }
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
        if (tweens.Count == 0)
        {
            Debug.LogWarning("当前 \"tweens\" 中暂无任何动画目标！");
            return;
        }

        foreach (var twn in tweens)
        {
            if (twn.text == null)
            {
                Debug.LogError("动画目标 \"text\" 丢失或未配置！");
                continue;
            }

            CreateTween_Move(twn);
            CreateTween_Color(twn);
        }

        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        //base.Tween_Play();

        foreach (var tweener in tweens)
        {
            if (tweener.moveTween != null)
                tweener.moveTween.Play();
            if (tweener.colorTween != null)
                tweener.colorTween.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        //base.Tween_Rewind();

        foreach (var tweener in tweens)
        {
            if (tweener.moveTween != null)
                tweener.moveTween.Rewind();
            if (tweener.colorTween != null)
                tweener.colorTween.Rewind();
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        //base.Tween_Pause_Or_Resume();

        ForEachTween(tween =>
        {
            if (tween == null) return;

            if (tween.IsPaused)
            {
                tween.Resume();
            }
            else
            {
                tween.Pause();
            }
        });
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        //base.Tween_Kill();

        Tween_Rewind();

        foreach (var tweener in tweens)
        {
            if (tweener.moveTween != null)
            {
                tweener.moveTween.Kill();
                tweener.moveTween = null;
                tweener.moveID = null;
            }
            if (tweener.colorTween != null)
            {
                tweener.colorTween.Kill();
                tweener.colorTween = null;
                tweener.colorID = null;
            }
        }
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 创建动画 - 颜色
    /// </summary>
    /// <param name="tweener"></param>
    private void CreateTween_Color(TextTweener tweener)
    {
        tweener.colorTween = tweener.text.xt_FontColor_To(tweener.colorTarget, duration, false, easeMode, true, () => tweener.colorFrom, useCurve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        {
            tweener.text.color = tweener.colorFrom;
        }).OnStart(() =>
        {

        });
        tweener.colorID = tweener.colorTween.ShortId;
    }
    /// <summary>
    /// 创建动画 - 运动
    /// </summary>
    /// <param name="tweener"></param>
    private void CreateTween_Move(TextTweener tweener)
    {
        tweener.moveTween = tweener.text.rectTransform.xt_AnchoredPosition_To(tweener.moveTarget, duration, false, true, easeMode, true, () => tweener.moveFrom, useCurve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        {
            tweener.text.rectTransform.anchoredPosition = tweener.moveFrom;
        }).OnStart(() =>
        {

        });
        tweener.moveID = tweener.moveTween.ShortId;
    }
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween(Action<XTween_Interface> action)
    {
        foreach (var twn in tweens)
        {
            action?.Invoke(twn.moveTween);
            action?.Invoke(twn.colorTween);
        }
    }
    /// <summary>
    /// 检查tween是否存在的辅助方法
    /// </summary>
    /// <returns></returns>
    public bool HasActiveTweens()
    {
        // 使用 for 循环
        for (int i = 0; i < tweens.Count; i++)
        {
            var tween = tweens[i];
            if (tween.moveTween != null || tween.colorTween != null)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 初始化文字颜色
    /// </summary>
    private void InitializeTextColors()
    {
        foreach (var twn in tweens)
        {
            twn.colorTarget = twn.text.color;
        }
    }
    /// <summary>
    /// 创建并播放动画
    /// </summary>
    private void CreateAndPlayTweens()
    {
        Tween_Create();
        Tween_Play();
    }
    #endregion
}
