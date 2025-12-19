using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

public class demo_colors_sender_bullet : MonoBehaviour
{
    public Image image;
    public float speed = 1;
    public Vector2 position_current;
    public Vector2 position_end;
    private demo_colors_sender sender;

    // 销毁时触发的回调
    public Action<demo_colors_sender_bullet> OnDestroyed;

    void Start()
    {
        image = GetComponent<Image>();
        // 尝试获取发送者引用
        sender = GetComponentInParent<demo_colors_sender>();
    }

    void Update()
    {
        position_current = Vector2.Lerp(position_current, position_end, Time.deltaTime * speed);
        MoveRelative(position_current);

        if (position_current.x >= position_end.x - 0.99f)
        {
            // 销毁前从列表中移除
            if (sender != null)
            {
                sender.RemoveBulletFromList(this);
            }
            DestroyImmediate(gameObject, true);
        }
    }

    // 当组件被销毁时（包括手动删除或其他方式）
    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="pos_cur"></param>
    /// <param name="pos_end"></param>
    /// <param name="color_start"></param>
    /// <param name="color_end"></param>
    /// <param name="duration"></param>
    /// <param name="delay"></param>
    /// <param name="ease"></param>
    public void Initialized(Vector2 pos_cur, Vector2 pos_end, Color color_start, Color color_end, float duration, float delay, EaseMode ease)
    {
        image.rectTransform.anchoredPosition = pos_cur;
        image.color = color_start;
        position_current = pos_cur;
        position_end = pos_end;

        CreateColorTween(color_end, duration, delay, ease);
    }

    /// <summary>
    /// 从当前位置相对移动
    /// </summary>
    /// <param name="offset">移动的距离（像素或单位）</param>
    public void MoveRelative(Vector2 offset)
    {
        image.rectTransform.anchoredPosition = offset;
    }

    /// <summary>
    /// 创建颜色过渡动画
    /// </summary>
    /// <param name="color_start"></param>
    /// <param name="color_end"></param>
    /// <param name="duration"></param>
    /// <param name="delay"></param>
    /// <param name="ease"></param>
    public void CreateColorTween(Color color_end, float duration, float delay, EaseMode ease)
    {
        image.xt_Color_To(color_end, duration, true).SetEase(ease).SetDelay(delay).SetLoop(0).SetLoopType(XTween_LoopType.Restart).Play();
    }
}
