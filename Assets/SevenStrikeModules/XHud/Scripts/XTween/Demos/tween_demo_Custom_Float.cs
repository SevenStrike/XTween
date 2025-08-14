using SevenStrikeModules.XHud.XTween;
using UnityEngine;
using UnityEngine.UI;

public class tween_demo_Custom_Float : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] internal float tweenTarget;
    [SerializeField] internal Image Image;

    [Header("Values")]
    [SerializeField] private float endValue = 1;
    [SerializeField] private float fromValue = 0;

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
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<float>((value, linearProgress, time) =>
                {
                    Image.fillAmount = value;
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<float>((value, linearProgress, time) =>
                {
                    Image.fillAmount = value;
                });
            }
        }
        else
        {
            if (useCurve)
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<float>((value, linearProgress, time) =>
                {
                    Image.fillAmount = value;
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<float>((value, linearProgress, time) =>
                {
                    Image.fillAmount = value;
                });
            }
        }

        return base.CreateTween();
    }
}