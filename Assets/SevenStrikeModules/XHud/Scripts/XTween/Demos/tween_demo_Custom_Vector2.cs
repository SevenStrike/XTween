using SevenStrikeModules.XHud.XTween;
using UnityEngine;

public class tween_demo_Custom_Vector2 : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] internal Vector2 tweenTarget;

    [Header("Values")]
    [SerializeField] private Vector2 endValue = Vector2.one;
    [SerializeField] private Vector2 fromValue = Vector2.zero;

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
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {
                });
            }
        }
        else
        {
            if (useCurve)
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {
                });
            }
        }

        return base.CreateTween();
    }
}