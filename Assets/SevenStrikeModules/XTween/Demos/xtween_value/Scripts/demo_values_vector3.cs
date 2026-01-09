using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_values_vector3 : demo_base
{
    public Image easeimg;
    public Text text_start;
    public Text text_end;
    public Text text_x;
    public Text text_y;
    public Text text_z;
    public Vector3 value_current;
    public Vector3 value_target;

    public override void Start()
    {
        base.Start();

        text_start.text = value_current.ToString();
        text_end.text = value_target.ToString();

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
        if (useCurve)
        {
            currentTweener = XTween.To(() => value_current, x => value_current = x, value_target, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                QuaContentSet(value_current);
            }).OnUpdate<Vector3>((s, d, t) =>
            {
                QuaContentSet(value_current);
            });
        }
        else
        {
            currentTweener = XTween.To(() => value_current, x => value_current = x, value_target, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                QuaContentSet(value_current);
            }).OnUpdate<Vector3>((s, d, t) =>
            {
                QuaContentSet(s);
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
    public void QuaContentSet(Vector3 val)
    {
        text_x.text = val.x.ToString("F2");
        text_y.text = val.y.ToString("F2");
        text_z.text = val.z.ToString("F2");
    }
    #endregion
}
