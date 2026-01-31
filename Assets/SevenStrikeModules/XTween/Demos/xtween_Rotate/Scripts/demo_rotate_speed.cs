using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class demo_rotate_speed : demo_base
{
    public RectTransform rect_speed;
    public Image img_ring;
    public float ring_speed;
    public float target_speed;
    public float target_speed_sm;

    public Text text_speed;

    public Button btn_random;
    public Button btn_reset;

    public RotationMode RotationMode;

    public int val_max = -270;
    public float val_min = -0;

    public override void Start()
    {
        base.Start();

        rect_speed.rotation = Quaternion.Euler(Vector3.forward * target_speed);

        // 浓度按钮 - 提升
        btn_random.onClick.AddListener(() =>
        {
            value_random();
            Tween_Create();
            Tween_Play();
        });

        // 浓度按钮 - 重置
        btn_reset.onClick.AddListener(() =>
        {
            value_reset();
            Tween_Create();
            Tween_Play();
        });
    }

    public override void Update()
    {
        base.Update();

        text_speed.text = $"{(int)Mathf.Abs(target_speed)}";

        target_speed_sm = Mathf.Lerp(target_speed_sm, target_speed, Time.deltaTime * ring_speed);
        img_ring.fillAmount = Mathf.Abs(target_speed_sm / val_max);
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        if (useCurve)
        {
            currentTweener = rect_speed.xt_Rotate_To(Vector3.forward * target_speed, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                if (rect_speed != null)
                    rect_speed.rotation = Quaternion.Euler(Vector3.zero);
            });
        }
        else
        {
            currentTweener = rect_speed.xt_Rotate_To(Vector3.forward * target_speed, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                if (rect_speed != null)
                    rect_speed.rotation = Quaternion.Euler(Vector3.zero);
            });
        }

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
        rect_speed.rotation = Quaternion.Euler(Vector3.zero);
    }
    #endregion

    #region 数值
    /// <summary>
    /// 速度数值随机化
    /// </summary>
    public void value_random()
    {
        target_speed = Random.Range(val_min, val_max);
    }
    /// <summary>
    /// 速度数值重置
    /// </summary>
    public void value_reset()
    {
        target_speed = 0;
    }
    #endregion
}
