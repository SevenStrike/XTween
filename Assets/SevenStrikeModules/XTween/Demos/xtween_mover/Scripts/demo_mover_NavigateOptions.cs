using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GalleryArgs
{
    public Vector2 from;
    public XTween_Interface tweener_pos;
    public XTween_Interface tweener_alp;
    public float duration;
    public EaseMode ease;
}

public class demo_mover_NavigateOptions : demo_base
{
    public Vector2 targetPos;
    public RectTransform mark;
    public RectTransform structs;
    public RectTransform slots;
    public RectTransform[] dots;
    public GalleryArgs galleryArgs;
    public Image Image;
    public Sprite[] Sprites;
    public int opt_index;

    public override void Start()
    {
        base.Start();

        Initialized();

        Tween_Create();
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
        if (!Application.isPlaying) return;

        currentTweener = mark.xt_AnchoredPosition_To(targetPos, duration, false, true).SetEase(easeMode).SetDelay(delay).SetLoop(loop, loopType);
        base.Tween_Create();
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
        opt_index = 0;
        mark.anchoredPosition = targetPos = CalculateDotPosition(dots[opt_index]);
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
        Tween_Rewind();
        base.Tween_Kill();
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialized()
    {
        structs = transform.Find("struct").GetComponent<RectTransform>();
        mark = structs.Find("mark").GetComponent<RectTransform>();
        slots = structs.Find("slots").GetComponent<RectTransform>();

        dots = new RectTransform[slots.childCount];
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i] = slots.GetChild(i).GetComponent<RectTransform>();
        }
    }
    /// <summary>
    /// 上一个选项
    /// </summary>
    public void Opt_Prev()
    {
        if (!Application.isPlaying)
        {
            if (opt_index <= 0)
                opt_index = dots.Length - 1;
            else
                opt_index--;
            targetPos = CalculateDotPosition(dots[opt_index]);
            return;
        }

        if (currentTweener == null) return;
        if (currentTweener.IsPlaying) return;

        if (opt_index <= 0)
        {
            opt_index = 0;
            return;
        }
        else
            opt_index--;

        targetPos = CalculateDotPosition(dots[opt_index]);
        Tween_Create();
        Tween_Play();
    }
    /// <summary>
    /// 下一个选项
    /// </summary>
    public void Opt_Next()
    {
        if (!Application.isPlaying)
        {
            if (opt_index >= dots.Length - 1)
                opt_index = 0;
            else
                opt_index++;
            targetPos = CalculateDotPosition(dots[opt_index]);
            return;
        }

        if (currentTweener == null) return;
        if (currentTweener.IsPlaying) return;

        if (opt_index >= dots.Length - 1)
        {
            opt_index = dots.Length - 1;
            return;
        }
        else
            opt_index++;

        targetPos = CalculateDotPosition(dots[opt_index]);
        Tween_Create();
        Tween_Play();
    }
    /// <summary>
    /// 计算标记物要移动到的相对Dots的点的坐标位置
    /// </summary>
    /// <returns></returns>
    public Vector2 CalculateDotPosition(RectTransform rect)
    {
        Vector2 targetScreenPos = RectTransformUtility.WorldToScreenPoint(null, rect.position);
        Vector2 localPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(structs, targetScreenPos, null, out localPos))
        {
            if (!Application.isPlaying)
                mark.anchoredPosition = localPos;
        }

        //Debug.Log($"ORI：{pos_dot}     /     CALC：{localPos}");
        return localPos;
    }

    internal void SetIndex(RectTransform rect)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (dots[i] == rect)
            {
                opt_index = i;
                break;
            }
        }
    }

    internal void switchImage()
    {
        if (galleryArgs.tweener_pos != null)
        {
            galleryArgs.tweener_pos.Kill();
        }
        if (galleryArgs.tweener_alp != null)
        {
            galleryArgs.tweener_alp.Kill();
        }
        Image.sprite = Sprites[opt_index];
        Image.rectTransform.anchoredPosition = galleryArgs.from;
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0);
        galleryArgs.tweener_pos = Image.rectTransform.xt_AnchoredPosition_To(Vector2.zero, galleryArgs.duration, false, true).SetEase(galleryArgs.ease).Play();
        galleryArgs.tweener_alp = Image.xt_Alpha_To(1, galleryArgs.duration, true).SetEase(galleryArgs.ease).Play();
    }
    #endregion
}
