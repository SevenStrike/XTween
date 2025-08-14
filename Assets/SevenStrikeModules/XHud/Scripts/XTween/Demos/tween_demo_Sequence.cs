using SevenStrikeModules.XHud.XTween;
using UnityEngine;
using UnityEngine.UI;

public class tween_demo_Sequence : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] public int tweenTarget;
    [SerializeField] public Image Image;
    public Sprite[] Sprites;

    [Header("Values")]
    [SerializeField] public int endValue = 1;
    [SerializeField] public int fromValue = 0;

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
        Tween_CreateRandomDelay();
        if (isFromMode)
        {
            if (useCurve)
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
                }).OnRewind(() =>
                {
                    Image.sprite = Sprites[tweenTarget];
                    if (showLogs)
                        Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成序列帧：{transform.name}");
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
                }).OnRewind(() =>
                {
                    Image.sprite = Sprites[tweenTarget];
                    if (showLogs)
                        Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成序列帧：{transform.name}");
                });
            }
        }
        else
        {
            if (useCurve)
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
                }).OnRewind(() =>
                {
                    Image.sprite = Sprites[tweenTarget];
                    if (showLogs)
                        Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成序列帧：{transform.name}");
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
                }).OnRewind(() =>
                {
                    Image.sprite = Sprites[tweenTarget];
                    if (showLogs)
                        Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    if (showLogs)
                        Debug.Log($"完成序列帧：{transform.name}");
                });
            }
        }

        return base.CreateTween();
    }
}
