using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class demo_mover_Wheel_ThinColor_Picker : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image image;
    public demo_mover_Wheel controller;
    private Color originalColor;
    public Color TargetColor;

    void Start()
    {
        image = GetComponent<Image>();

        // 确保 Image 可以接收射线检测
        image.raycastTarget = true;
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeColor();
    }

    /// <summary>
    /// 改变并切换拾取的颜色
    /// </summary>
    void ChangeColor()
    {
        controller.SetThinColor(TargetColor);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalColor = image.color;
        image.color = image.color * 0.4f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = originalColor;
    }
}