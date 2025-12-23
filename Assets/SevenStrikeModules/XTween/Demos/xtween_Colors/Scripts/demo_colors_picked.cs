using SevenStrikeModules.XTween;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class demo_colors_picked : demo_base
{
    public Image image;
    public Color[] col_list = new Color[5] {
        XTween_Utilitys.ConvertHexStringToColor("FF4A9C"),
        XTween_Utilitys.ConvertHexStringToColor("FFCE00"),
        XTween_Utilitys.ConvertHexStringToColor("0085FF"),
        XTween_Utilitys.ConvertHexStringToColor("00FF7E"),
        XTween_Utilitys.ConvertHexStringToColor("000000")
    };
    public int col_index;
    public Color col_from;
    public Color col_end;
    public bool autoloop;
    public float looptime = 1;

    public override void Start()
    {
        base.Start();
        StartCoroutine(ColorsLoop());
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

    #region 赋值
    /// <summary>
    /// 设置目标颜色
    /// </summary>
    /// <param name="color"></param>
    public void SetTargetColor(Color color)
    {
        col_end = color;
        Tweener_Restart();
    }
    #endregion

    #region 循环颜色
    IEnumerator ColorsLoop()
    {
        while (true)
        {
            if (autoloop)
            {
                // 颜色循环
                ColorLoop();
                // 重新创建动画
                Tweener_Restart();

                yield return new WaitForSeconds(looptime + duration);
            }
            else
            {
                yield return null;
            }
        }
    }
    /// <summary>
    /// 颜色循环计算逻辑
    /// </summary>
    public void ColorLoop()
    {
        if (col_index >= col_list.Length - 1)
            col_index = 0;
        else
            col_index++;

        col_end = col_list[col_index];
    }
    #endregion
}