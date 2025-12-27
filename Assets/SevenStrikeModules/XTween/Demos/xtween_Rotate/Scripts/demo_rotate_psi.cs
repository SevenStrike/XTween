using SevenStrikeModules.XTween;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class demo_rotate_psi : demo_base
{
    public RectTransform rect_PSI;
    public float targetPos_PSI;

    public XTween_Interface tween_TOG;
    public RectTransform rect_TOG;
    public float targetPos_TOG;

    public Text text_psi;
    public Text text_tog;

    public Button btn_switch;

    public RotationMode RotationMode;

    public Vector2 valueRange = new Vector2(0, -260);

    public override void Start()
    {
        base.Start();

        rect_PSI.rotation = Quaternion.Euler(Vector3.forward * targetPos_PSI);
        rect_TOG.rotation = Quaternion.Euler(Vector3.forward * targetPos_TOG);

        if (btn_switch == null)
        {
            btn_switch = GetComponentInChildren<Button>();
        }
        btn_switch.onClick.AddListener(() =>
        {
            Tween_Create();
            Tween_Play();
        });
    }

    public override void Update()
    {
        base.Update();

        text_psi.text = $"{Math.Round(Mathf.Abs(targetPos_PSI), 2)} psi";
        text_tog.text = $"{Math.Round(Mathf.Abs(targetPos_TOG), 2)} tog";
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        RandomValues();

        if (useCurve)
        {
            currentTweener = rect_PSI.xt_Rotate_To(Vector3.forward * targetPos_PSI, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                rect_PSI.rotation = Quaternion.Euler(Vector3.zero);
            }).OnStart(() =>
            {
            });
        }
        else
        {
            currentTweener = rect_PSI.xt_Rotate_To(Vector3.forward * targetPos_PSI, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                rect_PSI.rotation = Quaternion.Euler(Vector3.zero);
            }).OnStart(() =>
            {
            });
        }

        if (useCurve)
        {
            tween_TOG = rect_TOG.xt_Rotate_To(Vector3.forward * targetPos_TOG, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(curve).SetDelay(delay).OnRewind(() =>
            {
                rect_TOG.rotation = Quaternion.Euler(Vector3.zero);
            }).OnStart(() =>
            {
            });
        }
        else
        {
            tween_TOG = rect_TOG.xt_Rotate_To(Vector3.forward * targetPos_TOG, duration, isRelative, isAutoKill, RotationMode).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).SetEase(easeMode).SetDelay(delay).OnRewind(() =>
            {
                rect_TOG.rotation = Quaternion.Euler(Vector3.zero);
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
        if (tween_TOG != null)
            tween_TOG.Play();
        base.Tween_Play();
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public override void Tween_Rewind()
    {
        if (tween_TOG != null)
            tween_TOG.Rewind();
        base.Tween_Rewind();
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public override void Tween_Pause_Or_Resume()
    {
        if (tween_TOG != null)
        {
            if (tween_TOG.IsPlaying)
                tween_TOG.Pause();
            else
                tween_TOG.Resume();
        }
        base.Tween_Pause_Or_Resume();
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public override void Tween_Kill()
    {
        if (tween_TOG != null)
            tween_TOG.Kill();
        base.Tween_Kill();
        rect_PSI.rotation = Quaternion.Euler(Vector3.zero);
        rect_TOG.rotation = Quaternion.Euler(Vector3.zero);
    }
    #endregion

    #region 随机数值
    public void RandomValues()
    {
        targetPos_PSI = UnityEngine.Random.Range(valueRange.x, valueRange.y);
        targetPos_TOG = UnityEngine.Random.Range(valueRange.x, valueRange.y);
        //Debug.LogWarning($"{targetPos_PSI}  /  {targetPos_TOG}");
    }
    #endregion
}
