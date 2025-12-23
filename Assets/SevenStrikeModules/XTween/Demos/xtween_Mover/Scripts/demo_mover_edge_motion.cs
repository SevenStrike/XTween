using SevenStrikeModules.XTween;
using UnityEngine;

public class demo_mover_edge_motion : demo_base
{
    public RectTransform rect;
    public RectTransform rect_parent;
    public Vector2 targetPos;
    public Vector2 startPos;
    public bool autoStart;
    public int index_count;

    public override void Start()
    {
        base.Start();
        rect = GetComponent<RectTransform>();

        if (autoStart)
        {
            Tween_Create();
        }

        // 获取父节点
        rect_parent = rect.parent.GetComponent<RectTransform>();
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
        switch (index_count)
        {
            case 0:
                targetPos = new Vector2(rect_parent.rect.width - rect.sizeDelta.x, 0);
                break;
            case 1:
                targetPos = new Vector2(rect_parent.rect.width - rect.sizeDelta.x, -rect_parent.rect.height + rect.sizeDelta.y);
                break;
            case 2:
                targetPos = new Vector2(0, -rect_parent.rect.height + rect.sizeDelta.y);
                break;
            case 3:
                targetPos = new Vector2(0, 0);
                break;
        }

        currentTweener = rect.xt_AnchoredPosition_To(targetPos, duration, false, isAutoKill).SetLoop(loop, XTween_LoopType.Restart).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
        {
            rect.anchoredPosition = startPos;
        }).OnProgress<Vector2>((b, v) =>
        {

        }).OnStart(() =>
        {

        }).OnComplete((s) =>
        {
            if (index_count >= 3)
                index_count = 0;
            else
                index_count++;

            Tween_Create();
        });

        Tween_Play();
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
        index_count = 0;
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
        index_count = 0;
        targetPos = new Vector2(0, 0);
        rect.anchoredPosition = targetPos;
        base.Tween_Kill();
    }
    #endregion
}
