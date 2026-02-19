using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_colors_Screen : demo_base
{
    public Image easeimg;
    public Image image_lastColor;
    public Image image_currentColor;
    public RawImage image;
    public Text EaseName;
    public Text Duration;
    public Color col_last;
    public Color col_target;

    public override void Start()
    {
        col_last = col_target;
        image.color = col_target;

        image_lastColor.color = col_last;
        image_currentColor.color = col_target;
        Duration.text = duration.ToString();

        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }
    private void OnDrawGizmos()
    {
        if (easeimg != null)
        {
            easeimg.sprite = (Sprite)Resources.Load(easeMode.ToString(), typeof(Sprite));
        }
        if (EaseName != null)
        {
            EaseName.text = easeMode.ToString();
        }
        if (Duration != null)
        {
            Duration.text = duration.ToString();
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
            currentTweener = image.xt_Color_To(col_target, duration, isAutoKill).SetAutoKill(isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay);
        }
        else
        {
            currentTweener = image.xt_Color_To(col_target, duration, isAutoKill).SetAutoKill(isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay);
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
        col_last = col_target;
        col_target = color;
        Tweener_Restart();

        image_lastColor.color = col_last;
        image_currentColor.color = col_target;

        if (easeimg != null)
        {
            easeimg.sprite = (Sprite)Resources.Load(easeMode.ToString(), typeof(Sprite));
        }
        if (EaseName != null)
        {
            EaseName.text = easeMode.ToString();
        }
        if (Duration != null)
        {
            Duration.text = duration.ToString();
        }
    }
    #endregion  
}