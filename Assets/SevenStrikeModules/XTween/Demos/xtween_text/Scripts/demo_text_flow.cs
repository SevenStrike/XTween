using SevenStrikeModules.XTween;
using UnityEngine.UI;

public class demo_text_flow : demo_base
{
    public Text text;
    public string value;
    public string cursor = " _";
    public float blinktime = 0.2f;

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
        text.text = null;

        base.Tween_Create();

        currentTweener = text.xt_FontText_To(false, cursor, value, duration, false, blinktime).SetEase(easeMode).SetDelay(delay).SetLoopingDelay(loopDelay).SetLoop(loop).SetLoopType(loopType);
        ShortID = currentTweener.ShortId;
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

        text.text = null;
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
        text.text = null;
    }
    #endregion  
}
