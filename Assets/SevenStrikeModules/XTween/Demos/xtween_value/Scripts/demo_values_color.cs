using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_values_color : demo_base
{
    public Image easeimg;
    public Image target;
    public Image img_start;
    public Image img_end;
    public Color value_current;
    public Color value_target;

    public override void Start()
    {
        base.Start();

        img_start.color = value_current;
        img_end.color = value_target;

        Tween_Create();
        Tween_Play();
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            target.color = value_current;
        }
        if (easeimg != null)
        {
            easeimg.sprite = (Sprite)Resources.Load(easeMode.ToString(), typeof(Sprite));
        }
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        if (useCurve)
        {
            currentTweener = XTween.To(() => value_current, x => value_current = x, value_target, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                ImageColorSet(value_current);
            }).OnUpdate<Color>((s, d, t) =>
            {
                ImageColorSet(s);
            });
        }
        else
        {
            currentTweener = XTween.To(() => value_current, x => value_current = x, value_target, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                ImageColorSet(value_current);
            }).OnUpdate<Color>((s, d, t) =>
            {
                ImageColorSet(s);
            });
        }
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

    #region 辅助
    public void ImageColorSet(Color val)
    {
        target.color = val;
    }
    #endregion
}
