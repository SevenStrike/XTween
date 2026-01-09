using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_sequencer : demo_base
{
    public Image image;
    public int index;
    public Sprite[] sprites;
    public Button btn_play;
    public Button btn_prev;
    public Button btn_stop;
    public Button btn_rewind;
    public string dir = "play";
    public bool isTweening;
    public Text text_state;
    public Text text_index;
    public Image progress;

    public override void Start()
    {
        base.Start();

        btn_play.onClick.AddListener(() =>
        {
            Tween_Rewind();
            Tween_Kill();

            isTweening = true;

            dir = "play";
            Tween_Create();
            Tween_Play();
            text_state.text = "Playing  >>";
        });

        btn_prev.onClick.AddListener(() =>
        {
            Tween_Rewind();
            Tween_Kill();

            isTweening = true;

            dir = "prev";
            Tween_Create();
            Tween_Play();

            text_state.text = "Reversing  >>";
        });

        btn_stop.onClick.AddListener(() =>
        {
            if (!isTweening)
                return;

            isTweening = false;

            Tween_Kill();
            text_state.text = "Stoped  >>";
        });

        btn_rewind.onClick.AddListener(() =>
        {
            if (!isTweening)
                return;

            isTweening = false;

            Tween_Rewind();
            text_state.text = "Rewind  >>";
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
        CreateTween_Sequence(currentTweener, dir);
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
        base.Tween_Rewind();

        if (dir == "play")
            index = 0;
        else
            index = sprites.Length - 1;

        image.sprite = sprites[index];
        text_index.text = index.ToString();
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
        base.Tween_Kill();

        Tween_Rewind();

        if (dir == "play")
            index = 0;
        else
            index = sprites.Length - 1;

        image.sprite = sprites[index];
        text_index.text = index.ToString();

        if (dir == "play")
            progress.fillAmount = 0;
        else
            progress.fillAmount = 1;
    }
    #endregion

    #region 缩放动画
    /// <summary>
    /// 创建动画 - 填充
    /// </summary>
    /// <param name="twn"></param>
    public void CreateTween_Sequence(XTween_Interface twn, string dir, float hiddenDelay = 0)
    {
        if (dir == "play")
        {
            index = 0;
            image.sprite = sprites[index];
            text_index.text = index.ToString();
            progress.fillAmount = 0;

            currentTweener = XTween.To(() => index, x => index = x, sprites.Length - 1, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
            {
                image.sprite = sprites[value];
                text_index.text = value.ToString();
                progress.fillAmount = linearProgress;
            }).OnRewind(() =>
            {
                index = 0;
                image.sprite = sprites[index];
                if (debug)
                    Debug.Log($"复位序列帧：{transform.name}");
            }).OnComplete((d) =>
            {
                Debug.Log($"完成序列帧：{transform.name}");
            }).OnKill(() =>
            {
                index = 0;
                image.sprite = sprites[index];
            });
        }
        else
        {
            index = sprites.Length - 1;
            image.sprite = sprites[index];
            text_index.text = index.ToString();
            progress.fillAmount = 1;

            currentTweener = XTween.To(() => index, x => index = x, 0, duration, isAutoKill).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnUpdate<int>((value, linearProgress, time) =>
            {
                image.sprite = sprites[value];
                text_index.text = value.ToString();
                progress.fillAmount = linearProgress;
            }).OnRewind(() =>
            {
                index = sprites.Length - 1;
                image.sprite = sprites[index];
                if (debug)
                    Debug.Log($"复位序列帧：{transform.name}");
            }).OnComplete((d) =>
            {
                Debug.Log($"完成序列帧：{transform.name}");
            }).OnKill(() =>
            {
                index = sprites.Length - 1;
                image.sprite = sprites[index];
            });
        }
    }
    #endregion
}
