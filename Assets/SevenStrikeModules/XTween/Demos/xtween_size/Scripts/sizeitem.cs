using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class sizeTween
{
    public Image img;

    [SerializeField] public Vector2 target;
    [SerializeField] public Vector2 from;
    [SerializeField] public EaseMode ease = EaseMode.InOutCubic;
    [SerializeField] public float duration = 1;
    [SerializeField] public float delay = 0;
    [SerializeField] public string id;
    [SerializeField] public XTween_Interface tween;
}

public class sizeitem : demo_base
{
    public List<sizeTween> sizeTweens = new List<sizeTween>();
    public string dir = "display";
    public bool isOpend;
    public Text title;
    public Image icon;
    public bool iscomp;
    public Action<string> act_selected;

    public override void Start()
    {
        base.Start();

        foreach (var twn in sizeTweens)
        {
            twn.img.rectTransform.sizeDelta = twn.from;
        }
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        act_selected = null;
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        if (sizeTweens.Count == 0)
        {
            Debug.LogWarning("当前 \"tweens\" 中暂无任何动画目标！");
            return;
        }

        foreach (var twn in sizeTweens)
        {
            CreateTween_Size(twn, dir);
        }

        base.Tween_Create();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public override void Tween_Play()
    {
        //base.Tween_Play();

        foreach (var twn in sizeTweens)
        {
            if (twn.tween != null)
                twn.tween.Play();
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        //base.Tween_Rewind();

        foreach (var twn in sizeTweens)
        {
            if (twn.tween != null)
                twn.tween.Rewind();
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        //base.Tween_Pause_Or_Resume();

        ForEachTween(twn =>
        {
            if (twn == null) return;

            if (twn.IsPaused)
            {
                twn.Resume();
            }
            else
            {
                twn.Pause();
            }
        });
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        //base.Tween_Kill();

        Tween_Rewind();

        foreach (var twn in sizeTweens)
        {
            if (twn.tween != null)
            {
                twn.tween.Kill();
                twn.tween = null;
            }
            twn.id = null;
        }
    }
    #endregion

    #region 尺寸动画
    /// <summary>
    /// 创建动画 - 尺寸
    /// </summary>
    /// <param name="twn"></param>
    public void CreateTween_Size(sizeTween twn, string dir, float hiddenDelay = 0)
    {
        twn.tween = twn.img.rectTransform.xt_Size_To(twn.target, duration * twn.duration, isRelative, isAutoKill, easeMode, isFromMode, () => twn.from).SetEase(twn.ease).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetDelay(delay + twn.delay);

        twn.id = twn.tween.ShortId;
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 提取通用方法处理多个tween
    /// </summary>
    /// <param name="action"></param>
    public void ForEachTween(Action<XTween_Interface> action)
    {
        foreach (var twn in sizeTweens)
        {
            action?.Invoke(twn.tween);
            action?.Invoke(twn.tween);
        }
    }
    /// <summary>
    /// 检查所有tween是否存在
    /// </summary>
    /// <returns></returns>
    public bool HasActiveTweens()
    {
        // 使用 for 循环
        for (int i = 0; i < sizeTweens.Count; i++)
        {
            var twn = sizeTweens[i];
            if (twn.tween != null || twn.tween != null)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 检查所有tween是否在播放中
    /// </summary>
    /// <returns></returns>
    public bool HasPlayingTweens()
    {
        // 使用 for 循环
        for (int i = 0; i < sizeTweens.Count; i++)
        {
            var twn = sizeTweens[i];
            if (twn.tween.IsPlaying)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 设置道具图标
    /// </summary>
    /// <param name="spr"></param>
    internal void setTexture(Sprite spr)
    {
        icon.sprite = spr;
    }
    /// <summary>
    /// 设置道具标题
    /// </summary>
    /// <param name="name"></param>
    internal void setTitle(string name)
    {
        title.text = name;
    }
    #endregion

    #region 动画调用
    public void Tween_Display()
    {
        dir = "display";
        Tween_Create();
        Tween_Play();
    }
    #endregion
}
