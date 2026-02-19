using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_values_string : demo_base
{
    public Image easeimg;
    public Text target;
    public string value_current;
    public string value_target;
    public string cursor;
    public float cursorBlinkTime;

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

    private void OnDrawGizmos()
    {
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
        TextContentSet(null);
        if (useCurve)
        {
            currentTweener = XTween.To(() => value_current, x => value_current = x, false, cursorBlinkTime, cursor, value_target, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                TextContentSet(value_current);
            }).OnUpdate<string>((s, d, t) =>
            {
                TextContentSet(value_current);
            });
        }
        else
        {
            currentTweener = XTween.To(() => value_current, x => value_current = x, false, cursorBlinkTime, cursor, value_target, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                TextContentSet(value_current);
            }).OnUpdate<string>((s, d, t) =>
            {
                TextContentSet(value_current);
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
        Debug.Log(123);
        //TextContentSet(value_target);
    }
    #endregion

    #region 辅助
    public void TextContentSet(string val)
    {
        target.text = val;
    }
    #endregion
}
