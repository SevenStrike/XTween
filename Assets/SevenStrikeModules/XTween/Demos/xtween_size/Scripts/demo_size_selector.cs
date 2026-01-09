using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct sizeselectorArgs
{
    public Vector2 size;
    public Color color;
}

public class demo_size_selector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public sizeitem sizeitem;
    public Image edge;
    public XTween_Interface tween;
    public sizeselectorArgs size_pointer_down;
    public sizeselectorArgs size_pointer_up;
    public sizeselectorArgs size_pointer_enter;
    public sizeselectorArgs size_pointer_exit;

    public void OnPointerDown(PointerEventData eventData)
    {
        edge.rectTransform.sizeDelta = size_pointer_down.size;
        edge.color = size_pointer_down.color;

        if (sizeitem.act_selected != null)
            sizeitem.act_selected(sizeitem.title.text);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        edge.rectTransform.sizeDelta = size_pointer_enter.size;
        edge.color = size_pointer_enter.color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        edge.rectTransform.sizeDelta = size_pointer_exit.size;
        edge.color = size_pointer_exit.color;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        edge.rectTransform.sizeDelta = size_pointer_up.size;
        edge.color = size_pointer_up.color;
    }

    void Start()
    {
        if (sizeitem == null)
            sizeitem = GetComponent<sizeitem>();

        edge.rectTransform.sizeDelta = size_pointer_exit.size;
        edge.color = size_pointer_up.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

};