using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demo_scale_machine : demo_base
{
    [Header("Button")]
    public List<scaleTween> machinetweens = new List<scaleTween>();
    public List<scaleRotTween> scaleRottweens = new List<scaleRotTween>();

    [Header("RulerTween")]
    public scaleRotTween rulertween;
    public EaseMode Ruler_easeMode = EaseMode.InOutCubic;
    public float Ruler_Delay;

    [Header("ProbTween")]
    public scaleColorTween probtween;
    public AnimationCurve Prob_Curve_display = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve Prob_Curve_hidden = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float Prob_Duration_display;
    public float Prob_Duration_hidden;
    public float Prob_Delay_display;
    public float Prob_Delay_hidden;

    [Header("HiddenEffect")]
    public EaseMode Hidden_easeMode = EaseMode.InOutCubic;
    public float Hidden_Duration = 0.6f;
    public float Hidden_DelayMultiply = 0.6f;

    [Header("Button")]
    public Button btn_display;
    public Button btn_hidden;

    public string dir = "display";
    public bool isOpend;
    public Text text_state;

    public override void Start()
    {
        base.Start();

        foreach (var twn in machinetweens)
        {
            twn.img.rectTransform.localScale = twn.from;
        }

        foreach (var twn in scaleRottweens)
        {
            twn.img.rectTransform.eulerAngles = Vector3.zero;
        }

        rulertween.img.rectTransform.eulerAngles = Vector3.forward * 210;

        probtween.img.color = probtween.from;

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
        if (machinetweens.Count == 0)
        {
            Debug.LogWarning("当前 \"tweens\" 中暂无任何动画目标！");
            return;
        }

        if (dir == "display")
        {
            foreach (var twn in machinetweens)
            {
                CreateTween_Scale(twn, dir);
            }
            foreach (var twn in scaleRottweens)
            {
                CreateTween_Rot(twn);
            }

            rulertween.tween = rulertween.img.rectTransform.xt_Rotate_To(rulertween.target, rulertween.duration, false, true, TweenSpace.相对, RotationMode.Shortest, Ruler_easeMode, true, () => Vector3.forward * 210, false, null).SetDelay(Ruler_Delay).SetLoop(0);

            probtween.tween = probtween.img.xt_Color_To(probtween.target, Prob_Duration_display, true, EaseMode.InSine, true, () => probtween.from, true, Prob_Curve_display).SetDelay(Prob_Delay_display).OnKill(() =>
            {
                if (probtween != null && probtween.img != null)
                    probtween.img.color = probtween.target;
            }).SetLoop(0);
        }
        else
        {

            float[] hiddenDelays = GetTweensDelay(machinetweens);
            int s = 0;
            for (int i = machinetweens.Count - 1; i >= 0; i--)
            {
                CreateTween_Scale(machinetweens[i], dir, hiddenDelays[s]);
                s++;
            }

            foreach (var twn in scaleRottweens)
            {
                if (twn.tween != null)
                    twn.tween.Kill();
                twn.img.rectTransform.eulerAngles = Vector3.forward * 210;
            }

            probtween.tween = probtween.img.xt_Color_To(probtween.from, Prob_Duration_hidden, true, EaseMode.InSine, false, null, true, Prob_Curve_hidden).SetDelay(Prob_Delay_hidden).OnKill(() =>
            {
                if (probtween != null && probtween.img != null)
                    probtween.img.color = probtween.from;
            }).SetLoop(0);

            particleplayed = false;
        }

        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        //base.Tween_Play();

        foreach (var twn in machinetweens)
        {
            if (twn.tween != null)
                twn.tween.Play();
        }
        foreach (var twn in scaleRottweens)
        {
            if (twn.tween != null)
                twn.tween.Play();
        }

        if (rulertween.tween != null)
            rulertween.tween.Play();

        if (probtween.tween != null)
            probtween.tween.Play();
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        //base.Tween_Rewind();

        foreach (var twn in machinetweens)
        {
            if (twn.tween != null)
                twn.tween.Rewind();
        }

        foreach (var twn in scaleRottweens)
        {
            if (twn.tween != null)
                twn.tween.Rewind();
        }

        if (rulertween.tween != null)
            rulertween.tween.Rewind();

        if (probtween.tween != null)
            probtween.tween.Rewind();
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        //base.Tween_Pause_Or_Resume();

        ForEachTween_Scale(twn =>
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

        ForEachTween_Rot(twn =>
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

        if (rulertween.tween != null)
        {
            if (rulertween.tween.IsPaused)
            {
                rulertween.tween.Resume();
            }
            else
            {
                rulertween.tween.Pause();
            }
        }

        if (probtween.tween != null)
        {
            if (probtween.tween.IsPaused)
            {
                probtween.tween.Resume();
            }
            else
            {
                probtween.tween.Pause();
            }
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        //base.Tween_Kill();

        Tween_Rewind();

        foreach (var twn in machinetweens)
        {
            if (twn.tween != null)
            {
                twn.tween.Kill();
                twn.tween = null;
            }
            twn.id = null;
        }

        foreach (var twn in scaleRottweens)
        {
            if (twn.tween != null)
            {
                twn.tween.Kill();
                twn.tween = null;
            }
            twn.id = null;
        }

        if (rulertween.tween != null)
            rulertween.tween.Kill();

        if (probtween.tween != null)
            probtween.tween.Kill();
    }
    #endregion

    public bool particleplayed;

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
                if (twn.img != null)
                    twn.img.rectTransform.localScale = dir == "display" ? twn.target : twn.from;
            }).SetStepTimeInterval(0.2f).OnStepUpdate<Vector3>((s, f, k) =>
            {
                if (f > 0.2f)
                {
                    if (twn.particle != null && dir == "display")
                    {
                        if (particleplayed)
                            return;
                        particleplayed = true;
                        twn.particle.Play();
                    }
                }
            });
        }
        else
        {
            twn.tween = twn.img.rectTransform.xt_Scale_To(dir == "display" ? twn.target : twn.from, dir == "display" ? duration : Hidden_Duration, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "display" ? twn.from : twn.target, false, null).SetEase(dir == "display" ? easeMode : Hidden_easeMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "display" ? twn.delay : hiddenDelay * Hidden_DelayMultiply).OnRewind(() =>
            {
                if (twn.img != null)
                    twn.img.rectTransform.localScale = dir == "display" ? twn.target : twn.from;
            }).SetStepTimeInterval(0.2f).OnStepUpdate<Vector3>((s, f, k) =>
            {
                if (f > 0.2f)
                {
                    if (twn.particle != null && dir == "display")
                    {
                        if (particleplayed)
                            return;
                        particleplayed = true;
                        twn.particle.Play();
                    }
                }
            });
        }

        twn.id = twn.tween.ShortId;
    }
    #endregion

    #region 旋转动画
    /// <summary>
    /// 旋转动画
    /// </summary>
    /// <param name="twn"></param>
    public void CreateTween_Rot(scaleRotTween twn)
    {
        twn.tween = twn.img.rectTransform.xt_Rotate_To(twn.target, twn.duration, false, false, TweenSpace.相对, RotationMode.FullRotation).OnKill(() =>
        {
            if (twn.img != null)
                twn.img.rectTransform.eulerAngles = Vector3.zero;
        }).SetLoop(-1).SetEase(EaseMode.Linear);
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween_Scale(Action<XTween_Interface> action)
    {
        foreach (var twn in machinetweens)
        {
            action?.Invoke(twn.tween);
            action?.Invoke(twn.tween);
        }
    }
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween_Rot(Action<XTween_Interface> action)
    {
        foreach (var twn in scaleRottweens)
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
        for (int i = 0; i < machinetweens.Count; i++)
        {
            var twn = machinetweens[i];
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
        for (int i = 0; i < machinetweens.Count; i++)
        {
            var twn = machinetweens[i];
            if (twn.tween != null && twn.tween.IsPlaying)
            {
                return true;
            }
        }
        return false;
    }
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
