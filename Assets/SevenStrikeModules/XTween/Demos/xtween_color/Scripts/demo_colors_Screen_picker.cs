using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class demo_colors_Screen_picker : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image image;
    public demo_colors_Screen controller;
    private Color originalColor;

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
        controller.SetTargetColor(image.color);
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