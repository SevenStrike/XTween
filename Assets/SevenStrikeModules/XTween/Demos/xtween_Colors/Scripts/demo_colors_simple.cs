using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_colors_simple : demo_base
{
    public Image image;
    public Color col_from;
    public Color col_end;
    public bool autoStart;

    public override void Start()
    {
        base.Start();

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
                currentTweener = image.xt_Color_To(col_end, duration, isAutoKill).SetAutoKill(isAutoKill).SetFrom(col_from).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
                {

                });
            }
            else
            {
                currentTweener = image.xt_Color_To(col_end, duration, isAutoKill).SetAutoKill(isAutoKill).SetFrom(col_from).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
                {

                });
            }
        }
        else
        {
            if (useCurve)
            {
                currentTweener = image.xt_Color_To(col_end, duration, isAutoKill).SetAutoKill(isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
                {

                });
            }
            else
            {
                currentTweener = image.xt_Color_To(col_end, duration, isAutoKill).SetAutoKill(isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
                {

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