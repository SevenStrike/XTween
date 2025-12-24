using SevenStrikeModules.XTween;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class demo_path_map : demo_base
{
    public bool autoStart;
    public Image img;
    public XTween_PathTool path;
    public TrailRenderer trail;

    [SerializeField] public XTween_Interface alphaTween;
    [SerializeField] public float alphaTarget;
    [SerializeField] public float alphaFrom;
    [SerializeField] public float alphaDelay;
    [SerializeField] public AnimationCurve alphaCurve;

    private void Awake()
    {
        trail.emitting = false;
        trail.time = duration + loopDelay;
    }

    public override void Start()
    {
        base.Start();

        StartCoroutine(TrailOrbit());

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
        if (useCurve)
        {
            currentTweener = img.rectTransform.xt_PathMove(path, duration, path.PathOrientation, path.PathOrientationVector, isAutoKill).SetEase(curve).SetDelay(delay).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).
                 OnStart(() =>
                 {
                     CreateAlphaTween().Play();
                 }).OnRewind(() =>
                 {
                     CreateAlphaTween().Play();
                     trail.Clear();
                 }).OnKill(() =>
                 {

                 });
        }
        else
        {
            currentTweener = img.rectTransform.xt_PathMove(path, duration, path.PathOrientation, path.PathOrientationVector, isAutoKill).SetEase(easeMode).SetDelay(delay).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).
                OnStart(() =>
                {
                    CreateAlphaTween().Play();
                }).OnRewind(() =>
                {
                    CreateAlphaTween().Play();
                    trail.Clear();
                }).OnKill(() =>
                {

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

        if (alphaTween != null)
        {
            if (alphaTween.IsPaused)
                alphaTween.Resume();
            else
                alphaTween.Pause();
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        base.Tween_Kill();
    }
    #endregion

    #region Alpha动画
    /// <summary>
    /// 创建一个透明度动画
    /// </summary>
    /// <returns></returns>
    public XTween_Interface CreateAlphaTween()
    {
        return alphaTween = img.xt_Alpha_To(alphaTarget, duration, true, EaseMode.Linear, true, () => alphaFrom, true, alphaCurve).SetDelay(alphaDelay).SetLoop(0).OnComplete((s) =>
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alphaFrom);
        }).OnKill(() =>
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, alphaFrom);
        });
    }
    #endregion

    #region 运动轨迹
    IEnumerator TrailOrbit()
    {
        yield return new WaitForSeconds(0.1f);
        trail.Clear();
        trail.emitting = true;
    }
    #endregion
}
