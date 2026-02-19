using UnityEngine;
using UnityEngine.EventSystems;

public class demo_mover_NavigateOptions_Selector : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rect;
    public demo_mover_NavigateOptions controller;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeOption();
    }
    /// <summary>
    /// 改变并切换选项标记的位置
    /// </summary>
    void ChangeOption()
    {
        controller.SetIndex(rect);
        controller.targetPos = controller.CalculateDotPosition(rect);
        controller.Tween_Create();
        controller.Tween_Play();

        controller.switchImage();
    }
}