using SevenStrikeModules.XTween;
using UnityEngine;

public class tween_demo_AnchoredPosition : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] internal UnityEngine.RectTransform tweenTarget;

    [Header("Values")]
    [SerializeField] private Vector2 endValue = new Vector2(300, 300);
    [SerializeField] private Vector2 fromValue = new Vector2(0, 0);

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
                CurrentTweener = tweenTarget.xt_AnchoredPosition_To(endValue, duration, isRelative, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {

                }).OnRewind(() =>
                {
                    Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成序列帧：{transform.name}");
                });
            }
            else
            {
                CurrentTweener = tweenTarget.xt_AnchoredPosition_To(endValue, duration, isRelative, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {

                }).OnRewind(() =>
                {
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
                CurrentTweener = tweenTarget.xt_AnchoredPosition_To(endValue, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {

                }).OnRewind(() =>
                {
                    Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成序列帧：{transform.name}");
                });
            }
            else
            {
                CurrentTweener = tweenTarget.xt_AnchoredPosition_To(endValue, duration, isRelative, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<Vector2>((value, linearProgress, time) =>
                {

                }).OnRewind(() =>
                {
                    Debug.Log($"复位序列帧：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成序列帧：{transform.name}");
                });
            }
        }
        return base.CreateTween();
    }
}