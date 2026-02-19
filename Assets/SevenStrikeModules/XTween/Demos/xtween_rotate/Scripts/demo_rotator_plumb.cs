using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class demo_rotator_plumb : demo_base
{
    public RectTransform rect_target;

    public Button btn_start;
    public Button btn_reset;

    public RotationMode RotationMode;

    // 摆荡参数 - 暴露在Inspector中，方便调整
    [Header("摆荡参数")]
    public float initialAngle = 30f;      // 初始角度
    public float decayFactor = 0.5f;      // 衰减因子
    public int swingCount = 8;            // 摆荡次数

    public override void Start()
    {
        base.Start();

        // 开始运动
        btn_start.onClick.AddListener(() =>
        {
            Tween_Create();
            Tween_Play();
        });

        // 重置运动
        btn_reset.onClick.AddListener(() =>
        {
            rect_target.localEulerAngles = Vector3.zero;
            Tween_Kill();
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
        Tween_Kill();
        CreateRotationSequence(0);
        base.Tween_Create();
    }
    /// <summary>
    /// 递归摆荡动画
    /// </summary>
    /// <param name="step"></param>
    private void CreateRotationSequence(int step)
    {
        if (step >= swingCount)
        {
            currentTweener.Kill();
            // 最后一下归零，此动画需要播完直接杀死
            rect_target.xt_Rotate_To(Vector3.zero, duration, false, true, TweenSpace.相对, RotationMode.Shortest).SetEase(easeMode).Play();
            Debug.Log("摆荡结束");
            return;
        }

        // 第一步：从0摆到+30度
        // 第二步：从-15度摆到-15度？不对，应该是从+30度摆到-15度

        float amplitude = initialAngle * Mathf.Pow(decayFactor, step);

        // 目标角度
        float toAngle = (step % 2 == 0) ? amplitude : -amplitude;

        // 起始角度：如果是第一步，从0开始；否则从上一次目标的反方向衰减位置开始
        float fromAngle;
        if (step == 0)
            fromAngle = 0;
        else
        {
            float prevAmplitude = initialAngle * Mathf.Pow(decayFactor, step - 1);
            fromAngle = (step % 2 == 0) ? -prevAmplitude : prevAmplitude;
        }

        currentTweener = rect_target.xt_Rotate_To(
            Vector3.forward * toAngle,
            duration,
            isRelative,
            isAutoKill,
            TweenSpace.相对,
            RotationMode,
            easeMode,
            true,
            () => Vector3.forward * fromAngle,
            useCurve,
            curve
        )
        .OnComplete((d) =>
        {
            CreateRotationSequence(step + 1);
        })
        .Play();
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
    }
    #endregion
}
