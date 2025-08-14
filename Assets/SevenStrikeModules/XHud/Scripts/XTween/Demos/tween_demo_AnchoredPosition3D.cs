using SevenStrikeModules.XHud.XTween;
using UnityEngine;

public class tween_demo_AnchoredPosition3D : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] internal UnityEngine.RectTransform tweenTarget;

    [Header("Values")]
    [SerializeField] private Vector3 endValue = new Vector3(300, 300, 0);
    [SerializeField] private Vector3 fromValue = new Vector3(0, 0, 0);

    public override void Update()
    {
        base.Update();
    }

    public override void Tween_Create()
    {
        base.Tween_Create();

        CreateTween();
    }

    public override XTween_Interface CreateTween()
    {
        if (isFromMode)
        {
            if (useCurve)
            {
                CurrentTweener = tweenTarget.xt_AnchoredPosition3D_To(endValue, duration, isRelative, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay);
            }
            else
            {
                CurrentTweener = tweenTarget.xt_AnchoredPosition3D_To(endValue, duration, isRelative, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay);
            }
        }
        else
        {
            if (useCurve)
            {
                CurrentTweener = tweenTarget.xt_AnchoredPosition3D_To(endValue, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay);
            }
            else
            {
                CurrentTweener = tweenTarget.xt_AnchoredPosition3D_To(endValue, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay);
            }
        }

        return base.CreateTween();
    }

}