using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class gridtween
{
    public Image target;
    public float value;
    public float original;
    public string id;
    public XTween_Interface tween;
}

public class demo_tiled_grid : demo_base
{
    public gridtween[] gridtweens;
    public float parallaxdelay = 0;

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
        if (gridtweens.Length == 0)
        {
            Debug.LogWarning("当前 \"tweens\" 中暂无任何动画目标！");
            return;
        }

        for (int i = 0; i < gridtweens.Length; i++)
        {
            gridtween twn = gridtweens[i];
            twn.tween = twn.target.xt_Tiled_To(twn.value, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay + (i * parallaxdelay)).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                twn.target.pixelsPerUnitMultiplier = twn.original;
            }).OnUpdate<float>((s, d, t) =>
            {
                twn.target.pixelsPerUnitMultiplier = s;
            });
            twn.id = twn.tween.ShortId;
        }

        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        base.Tween_Play();
        for (int i = 0; i < gridtweens.Length; i++)
        {
            gridtweens[i].tween.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        base.Tween_Rewind();
        for (int i = 0; i < gridtweens.Length; i++)
        {
            gridtweens[i].tween.Rewind();
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        base.Tween_Pause_Or_Resume();
        for (int i = 0; i < gridtweens.Length; i++)
        {
            if (gridtweens[i].tween.IsPlaying)
                gridtweens[i].tween.Pause();
            else
                gridtweens[i].tween.Resume();
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        base.Tween_Kill();

        for (int i = 0; i < gridtweens.Length; i++)
        {
            gridtweens[i].tween.Kill();
            gridtweens[i].target.pixelsPerUnitMultiplier = gridtweens[i].original;
        }
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween(Action<XTween_Interface> action)
    {
        foreach (var twn in gridtweens)
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
        for (int i = 0; i < gridtweens.Length; i++)
        {
            var twn = gridtweens[i];
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
        for (int i = 0; i < gridtweens.Length; i++)
        {
            var twn = gridtweens[i];
            if (twn.tween != null && twn.tween.IsPlaying)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
