using SevenStrikeModules.XTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct itemstruct
{
    public string name;
    public Sprite spr;
}

[Serializable]
public struct sizeitemTweenArgs
{
    [Header("open_param")]
    [SerializeField] public Vector2 target_open;
    [SerializeField] public float duration_open;
    [SerializeField] public float delay_open;
    [SerializeField] public EaseMode ease_open;

    [Header("close_param")]
    [SerializeField] public Vector2 target_close;
    [SerializeField] public float duration_close;
    [SerializeField] public float delay_close;
    [SerializeField] public EaseMode ease_close;
}

public class demo_size_items : demo_base
{
    public sizeitemTweenArgs args_frame;
    public sizeitemTweenArgs args_titleRegion;

    public XTween_Interface titleRegionTween;

    [Header("common_param")]
    public float delayCreateTime = 0.05f;
    public RectTransform frame;
    public RectTransform titleRegion;
    public RectTransform itemsRect;
    public GridLayoutGroup itemsGrid;
    public Button btn_bag_open;
    public Button btn_bag_close;
    public string dir = "open";
    public bool isOpend;
    public Transform item;
    public List<itemstruct> itemstructs = new List<itemstruct>();
    public List<sizeitem> items = new List<sizeitem>();

    public Text SelectorText;

    public override void Start()
    {
        base.Start();

        frame.sizeDelta = args_frame.target_close;
        titleRegion.sizeDelta = args_titleRegion.target_close;

        // 打开背包
        btn_bag_open.onClick.AddListener(() =>
        {
            if (currentTweener != null && currentTweener.IsPlaying)
                return;

            if (isOpend)
                return;

            if (itemsTweening())
                return;

            isOpend = true;

            dir = "open";
            Tween_Create();
            Tween_Play();
            StartCoroutine(DelayCreate());
        });

        // 关闭背包
        btn_bag_close.onClick.AddListener(() =>
        {
            if (currentTweener != null && currentTweener.IsPlaying)
                return;

            if (!isOpend)
                return;

            if (itemsTweening())
                return;

            isOpend = false;

            dir = "close";
            Tween_Create();
            Tween_Play();

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Tween_Kill();
                Destroy(items[i].gameObject);
            }
            items.Clear();

            SetSelectorName(null);
        });
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
        #region Frame Tween
        if (useCurve)
        {
            currentTweener = frame.xt_Size_To(dir == "open" ? args_frame.target_open : args_frame.target_close, dir == "open" ? args_frame.duration_open : args_frame.duration_close, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "open" ? args_frame.target_close : args_frame.target_open, useCurve, curve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "open" ? args_frame.delay_open : args_frame.delay_close).OnRewind(() =>
            {
                frame.sizeDelta = dir == "open" ? args_frame.target_open : args_frame.target_close;
            });
        }
        else
        {
            currentTweener = frame.xt_Size_To(dir == "open" ? args_frame.target_open : args_frame.target_close, dir == "open" ? args_frame.duration_open : args_frame.duration_close, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "open" ? args_frame.target_close : args_frame.target_open, false, null).SetEase(dir == "open" ? args_frame.ease_open : args_frame.ease_close).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "open" ? args_frame.delay_open : args_frame.delay_close).OnRewind(() =>
            {
                frame.sizeDelta = dir == "open" ? args_frame.target_open : args_frame.target_close;
            });
        }
        #endregion

        #region TitleRegion
        if (useCurve)
        {
            titleRegionTween = titleRegion.xt_Size_To(dir == "open" ? args_titleRegion.target_open : args_titleRegion.target_close, dir == "open" ? args_titleRegion.duration_open : args_titleRegion.duration_close, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "open" ? args_titleRegion.target_close : args_titleRegion.target_open, useCurve, curve).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "open" ? args_titleRegion.delay_open : args_titleRegion.delay_close).OnRewind(() =>
            {
                titleRegion.sizeDelta = dir == "open" ? args_titleRegion.target_open : args_titleRegion.target_close;
            });
        }
        else
        {
            titleRegionTween = titleRegion.xt_Size_To(dir == "open" ? args_titleRegion.target_open : args_titleRegion.target_close, dir == "open" ? args_titleRegion.duration_open : args_titleRegion.duration_close, isRelative, isAutoKill, easeMode, isFromMode, () => dir == "open" ? args_titleRegion.target_close : args_titleRegion.target_open, false, null).SetEase(dir == "open" ? args_titleRegion.ease_open : args_titleRegion.ease_close).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(dir == "open" ? args_titleRegion.delay_open : args_titleRegion.delay_close).OnRewind(() =>
            {
                titleRegion.sizeDelta = dir == "open" ? args_titleRegion.target_open : args_titleRegion.target_close;
            });
        }
        #endregion
        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        base.Tween_Play();
        titleRegionTween.Play();
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        base.Tween_Rewind();
        titleRegionTween.Rewind();
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        base.Tween_Pause_Or_Resume();

        if (titleRegionTween != null)
        {
            if (titleRegionTween.IsPlaying)
            {
                titleRegionTween.Pause();
            }
            else
            {
                titleRegionTween.Resume();
            }
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        base.Tween_Kill();

        titleRegionTween.Kill();
    }
    #endregion

    #region 辅助
    public bool itemsTweening()
    {
        bool playing = false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].HasPlayingTweens())
                playing = true;
        }

        return playing;
    }

    /// <summary>
    /// 延迟生成背包道具
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayCreate()
    {
        for (int i = 0; i < itemstructs.Count; i++)
        {
            Transform im = Instantiate(item, itemsRect.transform);
            im.gameObject.name = "item";
            sizeitem _itm = im.GetComponent<sizeitem>();
            // 设置背包道具的图标
            _itm.setTexture(itemstructs[i].spr);
            // 设置背包道具的标题
            _itm.setTitle(itemstructs[i].name);
            // 播放道具项动画
            _itm.Tween_Display();
            _itm.act_selected += (text) =>
            {
                SetSelectorName(text);
            };
            items.Add(_itm);
            yield return new WaitForSeconds(delayCreateTime);
        }
    }
    /// <summary>
    /// 设置选择的项名称
    /// </summary>
    /// <param name="val"></param>
    public void SetSelectorName(string val)
    {
        SelectorText.text = val;
    }
    #endregion
}
