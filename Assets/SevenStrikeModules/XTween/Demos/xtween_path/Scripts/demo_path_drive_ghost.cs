using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_path_drive_ghost : MonoBehaviour
{
    public XTween_Interface colorTween;

    public void CreateGhost(Image root, float duration, float delay)
    {
        colorTween = root.xt_Color_To(Color.clear, duration, true).SetDelay(delay).SetEase(EaseMode.OutCubic).Play().SetLoop(0);
    }

    public void KillTween()
    {
        if (colorTween != null)
            colorTween.Kill();
    }
}
