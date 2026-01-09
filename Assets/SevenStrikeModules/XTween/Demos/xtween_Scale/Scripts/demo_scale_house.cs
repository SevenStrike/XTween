using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class scaleTween
{
    public Image img;

    [SerializeField] public Vector3 target;
    [SerializeField] public Vector3 from;
    [SerializeField] public float delay;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
    [SerializeField] public ParticleSystem particle;
}

[Serializable]
public class scaleRotTween
{
    public Image img;

    [SerializeField] public Vector3 target;
    [SerializeField] public float duration;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
}

[Serializable]
public class scaleColorTween
{
    public Image img;

    [SerializeField] public Color target;
    [SerializeField] public Color from;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
}

public class demo_scale_house : demo_base
{
    public List<scaleTween> houseweens = new List<scaleTween>();
    public EaseMode Hidden_easeMode = EaseMode.InOutCubic;
    public float Hidden_Duration = 0.6f;
    public float Hidden_DelayMultiply = 0.6f;
    public Button btn_display;
    public Button btn_hidden;
    public string dir = "display";
    public bool isOpend;
    public Text text_state;

    public override void Start()
    {
        base.Start();

        foreach (var twn in houseweens)
        {
            twn.img.rectTransform.localScale = twn.from;
        }

        btn_display.onClick.AddListener(() =>
        {
            if (HasPlayingTweens())
                return;

            if (isOpend)
                return;

            isOpend = true;

            dir = "display";
            Tween_Create();
            Tween_Play();
            text_state.gameObject.SetActive(false);
        });

        btn_hidden.onClick.AddListener(() =>
        {
            if (HasPlayingTweens())
                return;

            if (!isOpend)
                return;
            isOpend = false;

            dir = "hidden";
            Tween_Create();
            Tween_Play();

            text_state.gameObject.SetActive(true);
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
        if (houseweens.Count == 0)
        {
            Debug.LogWarning("当前 \"tweens\" 中暂无任何动画目标！");
            return;
        }

        if (dir == "display")
        {
            foreach (var twn in houseweens)
            {
                CreateTween_Scale(twn, dir);
            }
        }
        else
        {

            float[] hiddenDelays = GetTweensDelay(houseweens);
            int s = 0;
            for (int i = houseweens.Count - 1; i >= 0; i--)
            {
                CreateTween_Scale(houseweens[i], dir, hiddenDelays[s]);
                s++;
            }
        }



        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        //base.Tween_Play();

        foreach (var twn in houseweens)
        {
            if (twn.tween != null)
                twn.tween.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        //base.Tween_Rewind();

        foreach (var twn in houseweens)
        {
            if (twn.tween != null)
                twn.tween.Rewind();
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        //base.Tween_Pause_Or_Resume();

        ForEachTween(twn =>
        {
            if (twn == null) return;

            if (twn.IsPaused)
            {
                twn.Resume();
            }
            else
            {
                twn.Pause();
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

        foreach (var twn in houseweens)
        {
            if (twn.tween != null)
            {
                twn.tween.Kill();
                twn.tween = null;
            }
            twn.id = null;
        }
    }
    #endregion

    #region 缩放动画
    /// <summary>
    /// 创建动画 - 缩放
    /// </summary>
    /// <param name="twn"></param>
    public void CreateTween_Scale(scaleTween twn, string dir, float hiddenDelay = 0)
    {
        if (useCurve)
        {
            twn.tween = twn.img.rectTransform.xt_Scale_To(dir == "display" ? twn.target : twn.from, dir == "display" ? duration : Hidden_Duration, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "display" ? twn.from : twn.target, useCurve, curve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "display" ? twn.delay : hiddenDelay * Hidden_DelayMultiply).OnRewind(() =>
            {
                twn.img.rectTransform.localScale = dir == "display" ? twn.target : twn.from;
            });
        }
        else
        {
            twn.tween = twn.img.rectTransform.xt_Scale_To(dir == "display" ? twn.target : twn.from, dir == "display" ? duration : Hidden_Duration, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "display" ? twn.from : twn.target).SetEase(dir == "display" ? easeMode : Hidden_easeMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "display" ? twn.delay : hiddenDelay * Hidden_DelayMultiply).OnRewind(() =>
            {
                twn.img.rectTransform.localScale = dir == "display" ? twn.target : twn.from;
            });
        }
        twn.id = twn.tween.ShortId;
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween(Action<XTween_Interface> action)
    {
        foreach (var twn in houseweens)
        {
            action?.Invoke(twn.tween);
            action?.Invoke(twn.tween);
        }
    }
    /// <summary>
    /// 检查所有tween是否存在
    /// </summary>
    /// <returns></returns>
    public bool HasActiveTweens()
    {
        // 使用 for 循环
        for (int i = 0; i < houseweens.Count; i++)
        {
            var twn = houseweens[i];
            if (twn.tween != null || twn.tween != null)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 检查所有tween是否在播放中
    /// </summary>
    /// <returns></returns>
    public bool HasPlayingTweens()
    {
        // 使用 for 循环
        for (int i = 0; i < houseweens.Count; i++)
        {
            var twn = houseweens[i];
            if (twn.tween != null && twn.tween.IsPlaying)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 获取每个Tween的Delay
    /// </summary>
    /// <param name="maptweens"></param>
    /// <returns></returns>
    private float[] GetTweensDelay(List<scaleTween> maptweens)
    {
        float[] f = new float[maptweens.Count];
        for (int i = 0; i < f.Length; i++)
        {
            f[i] = maptweens[i].delay;
        }
        return f;
    }
    #endregion
}
