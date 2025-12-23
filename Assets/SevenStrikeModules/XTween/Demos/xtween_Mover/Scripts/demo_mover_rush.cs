using SevenStrikeModules.XTween;
using UnityEngine;

public class demo_mover_rush : demo_base
{
    public RectTransform rect;
    public Vector2 targetPos;
    public Vector2 fromPos;
    public XTween_Controller controller;
    public bool autoStart;

    public override void Start()
    {
        base.Start();
        rect = GetComponent<RectTransform>();

        if (autoStart)
        {
            Tween_Create();
            Tween_Play();
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
        if (isFromMode)
        {
            if (useCurve)
            {
                currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill, easeMode, true, () => fromPos).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
                {
                    rect.anchoredPosition = fromPos;
                }).OnProgress<Vector2>((b, v) =>
                {
                    if (v > 0.9f)
                        controller.Tween_Play();
                }).OnStart(() =>
                {
                    controller.Tween_ReCreate();
                });
            }
            else
            {
                currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill, easeMode, true, () => fromPos).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
                {
                    rect.anchoredPosition = fromPos;
                }).OnProgress<Vector2>((b, v) =>
                {
                    if (v > 0.9f)
                        controller.Tween_Play();
                }).OnStart(() =>
                {
                    controller.Tween_ReCreate();
                });
            }
        }
        else
        {
            if (useCurve)
            {
                currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
                {
                    rect.anchoredPosition = fromPos;
                }).OnProgress<Vector2>((b, v) =>
                {
                    if (v > 0.9f)
                        controller.Tween_Play();
                }).OnStart(() =>
                {
                    controller.Tween_ReCreate();
                });
            }
            else
            {
                currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
                {
                    rect.anchoredPosition = fromPos;
                }).OnProgress<Vector2>((b, v) =>
                {
                    if (v > 0.9f)
                        controller.Tween_Play();
                }).OnStart(() =>
                {
                    controller.Tween_ReCreate();
                });
            }
        }
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
}
