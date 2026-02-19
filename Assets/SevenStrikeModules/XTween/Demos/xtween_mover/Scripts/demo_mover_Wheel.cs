using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class wheel_MoveArgs
{
    public RectTransform rect;
    public float origin;
    public float start;
    public float end;
    public XTween_Interface tweener;
}

[Serializable]
public class wheel_RotateArgs
{
    public RectTransform rect;
    public float origin;
    public float Angle;
    public XTween_Interface tweener;
}

[Serializable]
public class wheel_GroundmarkArgs
{
    public RectTransform rect;
    public float limits;
    public float threshold_in_start;
    public float threshold_in_end;
    public float threshold_out_start;
    public float threshold_out_end;
    public XTween_Interface tweener;
}

public class demo_mover_Wheel : demo_base
{
    public Image Thin;
    public Button btn_in;
    public Button btn_out;
    public Text text;
    public wheel_MoveArgs arg_wheel_Move;
    public wheel_RotateArgs arg_wheel_Rotate;
    public wheel_GroundmarkArgs[] arg_wheel_GroundMarks;
    public float duration_in = 1;
    public EaseMode ease_in = EaseMode.InOutCubic;
    public float duration_out = 1;
    public EaseMode ease_out = EaseMode.InOutCubic;
    public float duration_thincolor = 1;
    public EaseMode thin_ease = EaseMode.InOutCubic;
    public XTween_Interface tweener_ThinColor;
    public bool IsPlaying;
    public string Status;
    bool inFinishedStatus = false;
    public Action act_on_wheel_in_finished;
    public Action act_on_wheel_out_started;

    public override void Start()
    {
        base.Start();

        btn_in.onClick.AddListener(Wheel_In);
        btn_out.onClick.AddListener(Wheel_Out);

        arg_wheel_Move.rect.anchoredPosition = new Vector2(arg_wheel_Move.origin, arg_wheel_Move.rect.anchoredPosition.y);
        arg_wheel_Rotate.rect.localEulerAngles = new Vector3(arg_wheel_Rotate.rect.localEulerAngles.x, arg_wheel_Rotate.rect.localEulerAngles.y, arg_wheel_Rotate.origin);
        for (int i = 0; i < arg_wheel_GroundMarks.Length; i++)
        {
            // 用因子计算目标宽度
            arg_wheel_GroundMarks[i].rect.sizeDelta = new Vector2(0, arg_wheel_GroundMarks[i].rect.sizeDelta.y);
        }
    }

    /// <summary>
    /// 轮胎进入
    /// </summary>
    private void Wheel_In()
    {
        WheelMotion("in");
    }

    /// <summary>
    /// 轮胎离开
    /// </summary>
    private void Wheel_Out()
    {
        WheelMotion("out");
    }

    public override void Update()
    {
        base.Update();
    }

    private void WheelMotion(string type)
    {
        if (IsPlaying)
            return;

        if (Status == type)
            return;

        Status = type;
        IsPlaying = true;

        #region 就绪姿态
        if (type == "in")
        {
            arg_wheel_Move.rect.anchoredPosition = new Vector2(arg_wheel_Move.origin, arg_wheel_Move.rect.anchoredPosition.y);
            arg_wheel_Rotate.rect.localEulerAngles = new Vector3(arg_wheel_Rotate.rect.localEulerAngles.x, arg_wheel_Rotate.rect.localEulerAngles.y, arg_wheel_Rotate.origin);
        }
        #endregion

        #region 文字提示
        if (type == "in")
        {
            text.text = "进入中...";
        }
        else
        {
            text.text = "离开中...";
        }
        #endregion

        #region 移动
        float pos = type == "in" ? arg_wheel_Move.start : arg_wheel_Move.end;
        arg_wheel_Move.tweener = arg_wheel_Move.rect.xt_AnchoredPosition_To(new Vector2(pos, arg_wheel_Move.rect.anchoredPosition.y), type == "in" ? duration_in : duration_out, isRelative, isAutoKill)
            .SetEase(type == "in" ? ease_in : ease_out)
            .OnStart(() =>
            {
                if (type == "out")
                {
                    if (act_on_wheel_out_started != null)
                        act_on_wheel_out_started();
                }
            })
            .OnComplete((d) =>
            {
                IsPlaying = false;
                text.text = null;
            })
            .OnEaseProgress<Vector2>((v, f) =>
            {
                if (type == "in")
                {
                    for (int i = 0; i < arg_wheel_GroundMarks.Length; i++)
                    {
                        // 核心计算：将 [arg_wheel_Marks[i].threshold, 1.0] 映射到 [0, 1] 的因子
                        float t = Mathf.InverseLerp(arg_wheel_GroundMarks[i].threshold_in_start, arg_wheel_GroundMarks[i].threshold_in_end, f);
                        // 用因子计算目标宽度
                        arg_wheel_GroundMarks[i].rect.sizeDelta = new Vector2(Mathf.Lerp(0, arg_wheel_GroundMarks[i].limits, t), arg_wheel_GroundMarks[i].rect.sizeDelta.y);
                    }

                    if (f > 0.98f)
                    {
                        if (inFinishedStatus)
                            return;
                        inFinishedStatus = true;

                        if (type == "in")
                        {
                            if (act_on_wheel_in_finished != null)
                                act_on_wheel_in_finished();
                        }
                    }
                }
                else
                {
                    inFinishedStatus = false;
                    for (int i = 0; i < arg_wheel_GroundMarks.Length; i++)
                    {
                        // 核心计算：将 [arg_wheel_Marks[i].threshold, 1.0] 映射到 [0, 1] 的因子
                        float t = Mathf.InverseLerp(arg_wheel_GroundMarks[i].threshold_out_start, arg_wheel_GroundMarks[i].threshold_out_end, f);
                        // 用因子计算目标宽度
                        arg_wheel_GroundMarks[i].rect.sizeDelta = new Vector2(Mathf.Lerp(arg_wheel_GroundMarks[i].limits, 0, t), arg_wheel_GroundMarks[i].rect.sizeDelta.y);
                    }
                }
            }).Play();
        #endregion

        #region 旋转
        // 计算周长
        float length = Mathf.PI * arg_wheel_Rotate.rect.sizeDelta.x;
        // 计算距离 - in
        float distance_in = arg_wheel_Move.start - arg_wheel_Move.rect.anchoredPosition.x;
        // 计算距离 - out
        float distance_out = arg_wheel_Move.end - arg_wheel_Move.rect.anchoredPosition.x;

        if (type == "in")
        {
            // 计算圈数 - in
            arg_wheel_Rotate.Angle = Mathf.Abs(distance_in / length);
        }
        else
        {
            // 计算圈数 - out
            arg_wheel_Rotate.Angle = Mathf.Abs(distance_out / length);
        }
        arg_wheel_Rotate.tweener = arg_wheel_Rotate.rect.xt_Rotate_To(new Vector3(arg_wheel_Rotate.rect.localEulerAngles.x, arg_wheel_Rotate.rect.localEulerAngles.y, -360f * arg_wheel_Rotate.Angle), type == "in" ? duration_in : duration_out, isRelative, isAutoKill, TweenSpace.相对, RotationMode.FullRotation).SetEase(type == "in" ? ease_in : ease_out).Play();
        #endregion
    }

    public void SetThinColor(Color color)
    {
        //Thin.color = color;
        tweener_ThinColor = Thin.xt_Color_To(color, duration_thincolor, true).SetEase(thin_ease).Play();
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
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
    }
    #endregion   
}
