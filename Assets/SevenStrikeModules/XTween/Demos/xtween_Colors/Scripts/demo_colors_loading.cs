using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_colors_loading : demo_base
{
    public Image image;
    public Color col_from;
    public Color col_end;
    public Animator animator;

    public override void Start()
    {
        base.Start();
        animator.enabled = false;
    }
    public override void Update()
    {
        base.Update();
    }

    #region 动画控制 - 重写
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
    public override void Tween_Play()
    {
        base.Tween_Play();
    }
    public override void Tween_Rewind()
    {
        base.Tween_Rewind();
    }
    public override void Tween_Pause_Or_Resume()
    {
        base.Tween_Pause_Or_Resume();
    }
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
        animator.enabled = true;
        animator.Play("LoadingMotion", 0, 0);
        // 重新创建动画
        Tweener_Restart();
    }
    #endregion
}