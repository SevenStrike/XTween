using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class tween_demo_Custom_Int : tween_demo_Base
{
    [Header("Target")]
    [SerializeField] internal int tweenTarget;
    [SerializeField] internal Image Image;
    public Sprite[] Sprites;

    [Header("Values")]
    [SerializeField] private int endValue = 1;
    [SerializeField] private int fromValue = 0;
    public override void Tween_Create()
    {
        base.Tween_Create();

        CreateTween();
    }

    override public void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.End))
        {
            XTween_Interface tween = XTween_Manager.Instance.FindTween_By_ShortID(CurrentTweener.ShortId);
            if (tween != null)
                Debug.Log($"通过ShortID找到！");
        }
    }

    public override XTween_Interface CreateTween()
    {
        if (isFromMode)
        {
            if (useCurve)
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetFrom(fromValue).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
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
                });
            }
            else
            {
                CurrentTweener = XTween.To(() => tweenTarget, x => tweenTarget = x, endValue, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
                {
                    Image.sprite = Sprites[value];
                });
            }
        }

        return base.CreateTween();
    }
}