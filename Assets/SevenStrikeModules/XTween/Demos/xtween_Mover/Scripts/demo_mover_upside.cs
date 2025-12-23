using SevenStrikeModules.XTween;
using UnityEngine;

public class demo_mover_upside : demo_base
{
    public bool autoStart;
    public float sizeRadius_min;
    public float sizeRadius_max;
    public float delayStep = 0.5f;
    public XTween_Controller[] cons;

    public override void Start()
    {
        base.Start();
        cons = GetComponentsInChildren<XTween_Controller>();
        for (int i = 0; i < cons.Length; i++)
        {
            cons[i].AutoStart = false;
        }
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
        base.Tween_Create();

        Tween_SetParams();

        for (int i = 0; i < cons.Length; i++)
        {
            cons[i].Tween_Create();
        }
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        base.Tween_Play();

        for (int i = 0; i < cons.Length; i++)
        {
            if (cons[i].CurrentTweener == null)
                continue;
            cons[i].CurrentTweener.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        base.Tween_Rewind();

        for (int i = 0; i < cons.Length; i++)
        {
            if (cons[i].CurrentTweener == null)
                continue;
            cons[i].CurrentTweener.Rewind();
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        base.Tween_Pause_Or_Resume();
        for (int i = 0; i < cons.Length; i++)
        {
            if (cons[i].CurrentTweener == null)
                continue;

            if (!cons[i].CurrentTweener.IsPaused)
                cons[i].CurrentTweener.Pause();
            else
                cons[i].CurrentTweener.Resume();
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        base.Tween_Kill();

        for (int i = 0; i < cons.Length; i++)
        {
            if (cons[i].CurrentTweener == null)
                continue;
            cons[i].CurrentTweener.Rewind();
            cons[i].CurrentTweener.Kill();
        }
    }
    #endregion

    #region 动画控制器集群控制参数
    public void Tween_SetParams()
    {
        // 动画延迟启动
        for (int i = 0; i < cons.Length; i++)
        {
            cons[i].Delay = (delayStep * i);
            cons[i].LoopDelay = loopDelay;
            cons[i].Duration = duration;
            cons[i].UseCurve = useCurve;
            cons[i].Curve = curve;
            cons[i].EaseMode = easeMode;
        }

        // 动画物体随机尺寸
        for (int i = 0; i < cons.Length; i++)
        {
            float ran_radius = Random.Range(sizeRadius_min, sizeRadius_max);
            cons[i].Target_Image.rectTransform.sizeDelta = new Vector2(ran_radius, ran_radius);
        }
    }
    #endregion
}
