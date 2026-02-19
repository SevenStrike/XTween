using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class paraArgs
{
    public RectTransform rect;
    public float pos_from;
    public float pos_target;
    public float pos_recalc;
    public float pos_added;
    public float delay = 0;
    public int loop = 0;
    public bool isForward;
    public XTween_Interface tweener;
}

[Serializable]
public class para_loop_Args
{
    public RectTransform rect;
    public float duration;
    public float pos_target;
    public float delay = 0;
    public float loopDelay_min = 0;
    public float loopDelay_max = 0;
    public XTween_Interface tweener;
}

public class demo_mover_Parallax : demo_base
{
    public Button btn_parallaxControl;
    public paraArgs[] paras;
    public para_loop_Args[] paras_loop;

    public override void Start()
    {
        base.Start();

        btn_parallaxControl.onClick.AddListener(() =>
        {
            ParallaxMove();
        });

        for (int i = 0; i < paras_loop.Length; i++)
        {
            float random_v = UnityEngine.Random.Range(paras_loop[i].loopDelay_min, paras_loop[i].loopDelay_max);
            float random_s = UnityEngine.Random.Range(paras_loop[i].loopDelay_min, paras_loop[i].loopDelay_max);
            paras_loop[i].tweener = paras_loop[i].rect.xt_AnchoredPosition_To(new Vector2(paras_loop[i].pos_target, paras_loop[i].rect.anchoredPosition.y), paras_loop[i].duration, false, false).SetDelay(paras_loop[i].delay).SetDelay(random_v * 5.5f).SetEase(EaseMode.Linear).SetLoop(-1).SetLoopType(XTween_LoopType.Restart).SetLoopingDelay(random_s).Play();
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
        //if (isFromMode)
        //{
        //    if (useCurve)
        //    {
        //        currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill, easeMode, true, () => fromPos).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
        //        {
        //            rect.anchoredPosition = fromPos;
        //        }).OnProgress<Vector2>((b, v) =>
        //        {
        //            if (v > 0.9f)
        //                controller.Tween_Play();
        //        }).OnStart(() =>
        //        {
        //            controller.Tween_ReCreate();
        //        });
        //    }
        //    else
        //    {
        //        currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill, easeMode, true, () => fromPos).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        //        {
        //            rect.anchoredPosition = fromPos;
        //        }).OnProgress<Vector2>((b, v) =>
        //        {
        //            if (v > 0.9f)
        //                controller.Tween_Play();
        //        }).OnStart(() =>
        //        {
        //            controller.Tween_ReCreate();
        //        });
        //    }
        //}
        //else
        //{
        //    if (useCurve)
        //    {
        //        currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
        //        {
        //            rect.anchoredPosition = fromPos;
        //        }).OnProgress<Vector2>((b, v) =>
        //        {
        //            if (v > 0.9f)
        //                controller.Tween_Play();
        //        }).OnStart(() =>
        //        {
        //            controller.Tween_ReCreate();
        //        });
        //    }
        //    else
        //    {
        //        currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        //        {
        //            rect.anchoredPosition = fromPos;
        //        }).OnProgress<Vector2>((b, v) =>
        //        {
        //            if (v > 0.9f)
        //                controller.Tween_Play();
        //        }).OnStart(() =>
        //        {
        //            controller.Tween_ReCreate();
        //        });
        //    }
        //}
        base.Tween_Create();
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

    private void ParallaxMove()
    {
        for (int i = 0; i < paras.Length; i++)
        {
            if (!isFromMode)
            {
                if (paras[i].tweener != null && paras[i].tweener.IsPlaying)
                    continue;

                if (!paras[i].isForward)
                {
                    paras[i].isForward = true;
                    paras[i].pos_recalc = paras[i].rect.anchoredPosition.x + paras[i].pos_added;
                }
                else
                {
                    paras[i].isForward = false;
                    paras[i].pos_recalc = paras[i].rect.anchoredPosition.x - paras[i].pos_added;
                }
                if (useCurve)
                    paras[i].tweener = paras[i].rect.xt_AnchoredPosition_To(new Vector2(paras[i].pos_recalc, paras[i].rect.anchoredPosition.y), duration, false, true).SetLoop(paras[i].loop).SetEase(curve).Play();
                else
                    paras[i].tweener = paras[i].rect.xt_AnchoredPosition_To(new Vector2(paras[i].pos_recalc, paras[i].rect.anchoredPosition.y), duration, false, true).SetLoop(paras[i].loop).SetEase(easeMode).Play();
            }
            else
            {
                if (!paras[i].isForward)
                {
                    paras[i].isForward = true;
                    paras[i].pos_recalc = paras[i].pos_target;
                }
                else
                {
                    paras[i].isForward = false;
                    paras[i].pos_recalc = paras[i].pos_from;
                }

                if (useCurve)
                    paras[i].tweener = paras[i].rect.xt_AnchoredPosition_To(new Vector2(paras[i].pos_recalc, paras[i].rect.anchoredPosition.y), duration, false, true).SetLoop(paras[i].loop).SetEase(curve).Play();
                else
                    paras[i].tweener = paras[i].rect.xt_AnchoredPosition_To(new Vector2(paras[i].pos_recalc, paras[i].rect.anchoredPosition.y), duration, false, true).SetLoop(paras[i].loop).SetEase(easeMode).Play();
            }

        }
    }
}
