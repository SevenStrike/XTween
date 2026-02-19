using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_rotator_radar_targetsite : MonoBehaviour
{
    public XTween_Interface colorTween;
    public XTween_Interface alphaTween;
    internal CanvasGroup canvasGroup;

    public void CreateGhost(Image root, float duration, float delay)
    {
        if (root == null)
            return;
        colorTween = root.xt_Color_To(Color.white, duration * 0.5f, true).SetDelay(delay * 0.5f).SetEase(EaseMode.OutCubic).SetLoop(0).OnComplete((d) =>
        {
            if (canvasGroup == null)
                return;
            alphaTween = canvasGroup.xt_Alpha_To(0, duration, true).SetDelay(delay).SetEase(EaseMode.OutCubic).SetLoop(0).OnComplete((d) =>
            {
                DestroyImmediate(gameObject, true);
            }).Play();
        }).Play();
    }

    public void KillTween()
    {
        if (colorTween != null)
            colorTween.Kill();
        if (alphaTween != null)
            alphaTween.Kill();
    }
}
