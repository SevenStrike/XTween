using SevenStrikeModules.XHud.XTween;
using UnityEngine;

public class tween_demo_path : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] internal UnityEngine.RectTransform tweenTarget;

    [Header("Bezier Path")]
    [SerializeField] public XTween_PathTool tweenPath;


    public override void Tween_Create()
    {
        base.Tween_Create();

        CreateTween();
    }

    public override XTween_Interface CreateTween()
    {
        if (useCurve)
        {
            CurrentTweener = tweenTarget.xt_PathMove(tweenPath, duration, tweenPath.PathOrientation, tweenPath.PathOrientationVector, isAutoKill).SetEase(curve).SetDelay(delay).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).OnUpdate<Vector3>((value, linearProgress, time) =>
            {

            }).OnRewind(() =>
            {
                Debug.Log($"复位路径：{transform.name}");
            }).OnComplete((d) =>
            {
                Debug.Log($"完成路径：{transform.name}");
            });
        }
        else
        {
            CurrentTweener = tweenTarget.xt_PathMove(tweenPath, duration, tweenPath.PathOrientation, tweenPath.PathOrientationVector, isAutoKill).SetEase(easeMode).SetDelay(delay).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).OnUpdate<Vector3>((value, linearProgress, time) =>
                {

                }).OnRewind(() =>
                {
                    Debug.Log($"复位路径：{transform.name}");
                }).OnComplete((d) =>
                {
                    Debug.Log($"完成路径：{transform.name}");
                });
        }

        return base.CreateTween();
    }

}