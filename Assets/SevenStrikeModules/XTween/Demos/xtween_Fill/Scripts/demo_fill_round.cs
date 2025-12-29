using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class roundTweener
{
    public Image img;

    [SerializeField] public float target;
    [SerializeField] public float from;
    [SerializeField] public float delay;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
    [SerializeField] public Text text;
}

public class demo_fill_round : demo_base
{
    public List<roundTweener> maptweens = new List<roundTweener>();

    public override void Start()
    {
        base.Start();

        CreateAndPlayTweens();
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
        if (maptweens.Count == 0)
        {
            Debug.LogWarning("当前 \"tweens\" 中暂无任何动画目标！");
            return;
        }

        foreach (var twn in maptweens)
        {
            CreateTween_Fill(twn);
        }

        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        //base.Tween_Play();

        foreach (var tweener in maptweens)
        {
            if (tweener.tween != null)
                tweener.tween.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        //base.Tween_Rewind();

        foreach (var tweener in maptweens)
        {
            if (tweener.tween != null)
                tweener.tween.Rewind();
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

        foreach (var tweener in maptweens)
        {
            if (tweener.tween != null)
            {
                tweener.tween.Kill();
                tweener.tween = null;
            }
            tweener.id = null;
        }
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 创建动画 - 填充
    /// </summary>
    /// <param name="tweener"></param>
    public void CreateTween_Fill(roundTweener tweener)
    {
        tweener.tween = tweener.img.xt_Fill_To(tweener.target, duration, isAutoKill, easeMode, isFromMode, () => tweener.from, useCurve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(tweener.delay).OnRewind(() =>
        {
            tweener.img.fillAmount = tweener.from;
            tweener.text.text = "0";
        }).OnUpdate<float>((s, d, t) =>
        {
            tweener.text.text = (s * 100).ToString("F0");
        });
        tweener.id = tweener.tween.ShortId;
    }
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween(Action<XTween_Interface> action)
    {
        foreach (var twn in maptweens)
        {
            action?.Invoke(twn.tween);
            action?.Invoke(twn.tween);
        }
    }
    /// <summary>
    /// 检查tween是否存在的辅助方法
    /// </summary>
    /// <returns></returns>
    public bool HasActiveTweens()
    {
        // 使用 for 循环
        for (int i = 0; i < maptweens.Count; i++)
        {
            var tween = maptweens[i];
            if (tween.tween != null || tween.tween != null)
            {
                return true;
            }
        }
        return false;
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
