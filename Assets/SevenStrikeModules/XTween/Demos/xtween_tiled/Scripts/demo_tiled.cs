using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demo_tiled : demo_base
{
    public Image target;
    public float value;

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
        currentTweener = target.xt_Tiled_To(value, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        {
            target.pixelsPerUnitMultiplier = 1;
        }).OnUpdate<float>((s, d, t) =>
        {
            target.pixelsPerUnitMultiplier = s;
        });
        ShortID = currentTweener.ShortId;

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

        Tween_Rewind();
    }
    #endregion
}
