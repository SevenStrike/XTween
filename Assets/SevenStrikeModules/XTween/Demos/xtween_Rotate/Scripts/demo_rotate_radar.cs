using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class demo_rotate_radar : demo_base
{
    public RectTransform rect_scan;

    public Text text_value_count;
    public Text text_value_status;

    public ParticleSystem particle_dots;

    public RotationMode RotationMode;

    public override void Start()
    {
        base.Start();

        rect_scan.rotation = Quaternion.Euler(Vector3.zero);

        Tween_Create();
        Tween_Play();
    }

    public override void Update()
    {
        base.Update();

        text_value_count.text = particle_dots.particleCount.ToString();
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        if (useCurve)
        {
            currentTweener = rect_scan.xt_Rotate_To(Vector3.forward * 360, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                rect_scan.rotation = Quaternion.Euler(Vector3.zero);
                if (Application.isPlaying)
                    text_value_status.text = currentTweener.CurrentLoop.ToString();
                particle_dots.Play();
            });
        }
        else
        {
            currentTweener = rect_scan.xt_Rotate_To(Vector3.forward * 360, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                rect_scan.rotation = Quaternion.Euler(Vector3.zero);
                if (Application.isPlaying)
                    text_value_status.text = currentTweener.CurrentLoop.ToString();
                particle_dots.Play();
            });
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
        rect_scan.rotation = Quaternion.Euler(Vector3.zero);
    }
    #endregion
}
