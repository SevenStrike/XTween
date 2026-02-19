using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class mapScroller
{
    public Image img;

    [SerializeField] public Vector2 target;
    [SerializeField] public Vector2 from;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
}

[Serializable]
public class mapTweener
{
    public Image img;

    [SerializeField] public float target;
    [SerializeField] public float from;
    [SerializeField] public float delay;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
}

public class demo_fill_map : demo_base
{
    public mapScroller mapscroller;
    public List<mapTweener> maptweens = new List<mapTweener>();
    public ParticleSystem particle_points;
    public Button btn_open;
    public Button btn_close;
    public string dir = "open";
    public bool isOpend;

    //public bool autoStart;

    public override void Start()
    {
        base.Start();

        btn_open.onClick.AddListener(() =>
        {
            if (isOpend)
                return;
            isOpend = true;

            dir = "open";
            Tween_Create();
            Tween_Play();
        });

        btn_close.onClick.AddListener(() =>
        {
            if (!isOpend)
                return;
            isOpend = false;

            dir = "close";
            Tween_Create();
            Tween_Play();

            particle_points.Stop();
            particle_points.Clear();
        });
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

        CreateTween_Motion(mapscroller);

        foreach (var twn in maptweens)
        {
            CreateTween_Fill(twn, dir);
        }

        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        //base.Tween_Play();

        if (mapscroller.tween != null)
            mapscroller.tween.Play();

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

        if (mapscroller.tween != null)
            mapscroller.tween.Rewind();

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

        if (mapscroller.tween.IsPaused)
        {
            mapscroller.tween.Resume();
        }
        else
        {
            mapscroller.tween.Pause();
        }

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

        if (mapscroller.tween != null)
        {
            mapscroller.tween.Kill();
            mapscroller.tween = null;
        }
        mapscroller.id = null;

        foreach (var tweener in maptweens)
        {
            if (tweener.tween != null)
            {
                tweener.tween.Kill();
                tweener.tween = null;
            }
            tweener.id = null;
        }

        if (dir == "close")
        {
            particle_points.Stop();
            particle_points.Clear();
        }
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 创建动画 - 填充
    /// </summary>
    /// <param name="tweener"></param>
    public void CreateTween_Fill(mapTweener tweener, string dir)
    {
        tweener.tween = tweener.img.xt_Fill_To(dir == "open" ? tweener.target : tweener.from, duration, isAutoKill, easeMode, isFromMode, () => tweener.from, useCurve, curve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(dir == "open" ? tweener.delay : 0).OnRewind(() =>
        {
            tweener.img.fillAmount = tweener.from;
        });
        tweener.id = tweener.tween.ShortId;
    }
    /// <summary>
    /// 创建动画 - 运动
    /// </summary>
    /// <param name="tweener"></param>
    public void CreateTween_Motion(mapScroller tweener)
    {
        tweener.tween = tweener.img.rectTransform.xt_AnchoredPosition_To(dir == "open" ? tweener.target : tweener.from, duration, isRelative, isAutoKill, easeMode, isFromMode, () => tweener.from, useCurve, curve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        {
            tweener.img.rectTransform.anchoredPosition = tweener.from;
        }).OnComplete((s) =>
        {
            if (dir == "open")
            {
                particle_points.Play();
            }
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
