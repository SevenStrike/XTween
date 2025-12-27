using SevenStrikeModules.XTween;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class demo_rotate_mg : demo_base
{
    public RectTransform rect_MG;
    public float target_MG;
    public float target_Step;

    public Text text_mg;

    public Button btn_plus;
    public Button btn_minus;
    public Button btn_reset;

    public RotationMode RotationMode;
    public int val_Maximum = -270;
    public float val_Minimum = -0;

    public float val_Blend = 0;
    public float val_BlendSpeed = 1;
    public float val_smoothBlend = 0;

    public Material mat_cup;

    public override void Start()
    {
        base.Start();

        Image cup = transform.Find("dial_mg/based/cup").GetComponent<Image>();
        mat_cup = new Material(cup.material);
        cup.material = mat_cup;

        rect_MG.rotation = Quaternion.Euler(Vector3.forward * target_MG);

        // 浓度按钮 - 提升
        btn_plus.onClick.AddListener(() =>
        {
            if (!isMaximum())
                return;
            value_forward();
            Tween_Create();
            Tween_Play();
        });

        // 浓度按钮 - 下降
        btn_minus.onClick.AddListener(() =>
        {
            if (!isMinimum())
                return;
            value_backward();
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

        val_Blend = target_MG / val_Maximum;
        val_smoothBlend = Mathf.Lerp(val_smoothBlend, Mathf.Abs(val_Blend), Time.deltaTime * val_BlendSpeed);
        mat_cup.SetFloat("_blend", val_smoothBlend);

        text_mg.text = $"{Math.Round(Mathf.Abs(target_MG), 2)} mg";
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        if (useCurve)
        {
            currentTweener = rect_MG.xt_Rotate_To(Vector3.forward * target_MG, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                rect_MG.rotation = Quaternion.Euler(Vector3.zero);
            }).OnStart(() =>
            {
            });
        }
        else
        {
            currentTweener = rect_MG.xt_Rotate_To(Vector3.forward * target_MG, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                rect_MG.rotation = Quaternion.Euler(Vector3.zero);
            }).OnStart(() =>
            {
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
        rect_MG.rotation = Quaternion.Euler(Vector3.zero);
    }
    #endregion

    #region 浓度数值
    /// <summary>
    /// 浓度提升
    /// </summary>
    public void value_forward()
    {
        target_MG -= target_Step;
    }
    /// <summary>
    /// 浓度下降
    /// </summary>
    public void value_backward()
    {
        target_MG += target_Step;
    }
    /// <summary>
    /// 浓度重置
    /// </summary>
    public void value_reset()
    {
        target_MG = 0;
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 浓度达到最大
    /// </summary>
    /// <returns></returns>
    public bool isMaximum()
    {
        return target_MG > -val_Maximum;
    }
    /// <summary>
    /// 浓度达到最小
    /// </summary>
    /// <returns></returns>
    public bool isMinimum()
    {
        return target_MG < val_Minimum;
    }
    #endregion
}
