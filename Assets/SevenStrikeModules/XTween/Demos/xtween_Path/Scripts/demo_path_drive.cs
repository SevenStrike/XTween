using SevenStrikeModules.XTween;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class demo_path_drive : demo_base
{
    [SerializeField] public XTween_PathTool tweenPathTool;

    [SerializeField] public bool autoStart;
    [SerializeField] internal RectTransform carRoot;
    [SerializeField] internal RectTransform carTarget;
    [SerializeField] internal Image carTargetImage;

    [Header("透明动画")]
    [SerializeField] public XTween_Interface alphaTween;
    [SerializeField] public float alphaTarget;
    [SerializeField] public float alphaFrom;
    [SerializeField] public float alphaDelay;
    [SerializeField] public AnimationCurve alphaCurve;

    [Header("漂移旋转动画")]
    [SerializeField] public XTween_Interface rotateTween;
    [SerializeField] public Vector3 rotateTarget;
    [SerializeField] public Vector3 rotateFrom;
    [SerializeField] public float rotateDelay;
    [SerializeField] public AnimationCurve rotateCurve;
    [SerializeField] public RotationMode RotationMode;
    [SerializeField] public bool AlwaysDraft;

    [Header("残影")]
    [SerializeField] public float GhostIntervalTime = 0.2f;
    [SerializeField] public float GhostDuration = 1f;
    [SerializeField] public float GhostDelay = 0.5f;
    [SerializeField] public Vector2 GhostRange = new Vector2(0, 1);
    [SerializeField] public Texture2D GhostTexture;
    [SerializeField] public List<demo_path_drive_ghost> Ghosts = new List<demo_path_drive_ghost>();

    [Header("内容")]
    public demo_path_ContentDisplayer PercentDisplayer;
    public demo_path_ContentDisplayer DraftAngleDisplayer;

    [Header("汽车着色")]
    private Material car_Material;
    public Color[] carPaint_Colors;
    public int carPaint_Index;

    public override void Start()
    {
        base.Start();

        if (carPaint_Colors.Length == 0)
        {
            carPaint_Colors = new Color[5];
            carPaint_Colors[0] = XTween_Utilitys.ConvertHexStringToColor("BEBEBE");
            carPaint_Colors[1] = XTween_Utilitys.ConvertHexStringToColor("C09627");
            carPaint_Colors[2] = XTween_Utilitys.ConvertHexStringToColor("2767C0");
            carPaint_Colors[3] = XTween_Utilitys.ConvertHexStringToColor("A13131");
            carPaint_Colors[4] = XTween_Utilitys.ConvertHexStringToColor("785DB3");
        }

        if (autoStart)
        {
            CreateAndPlayTweens();
        }

        InitializedCarMaterial();
    }

    /// <summary>
    /// 初始化车材质
    /// </summary>
    private void InitializedCarMaterial()
    {
        car_Material = carTargetImage.material;
        carTargetImage.material = new Material(car_Material);
    }

    public override void Update()
    {
        base.Update();

        if (PercentDisplayer != null)
        {
            PercentDisplayer.SetText($"{Math.Round(tweenPathTool.PathProgress, 2) * 100}%");
        }
        if (DraftAngleDisplayer != null)
        {
            if (carTargetImage.rectTransform.localEulerAngles.z > 0)
                DraftAngleDisplayer.SetText($"{360 - carTargetImage.rectTransform.localEulerAngles.z}°");
            else
                DraftAngleDisplayer.SetText($"{carTargetImage.rectTransform.localEulerAngles.z}°");
        }
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        if (useCurve)
        {
            currentTweener = carTarget.xt_PathMove(tweenPathTool, duration, tweenPathTool.PathOrientation, tweenPathTool.PathOrientationVector, isAutoKill).SetEase(curve).SetDelay(delay).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).OnStart(() =>
            {
                CreateAlphaTween().Play();
                if (ValidDraft() == 1)
                    CreateRotationTween().Play();
            }).OnRewind(() =>
            {
                if (!Application.isPlaying)
                {
                    carPaint_Index = 0;
                    carTargetImage.material.SetColor("_Color", carPaint_Colors[carPaint_Index]);
                    return;
                }
                ClearGhost();
                NextCarPaint();
                CreateAlphaTween().Play();
                if (ValidDraft() == 1)
                    CreateRotationTween().Play();
            }).OnKill(() =>
            {
                carPaint_Index = 0;
                carTargetImage.material.SetColor("_Color", carPaint_Colors[carPaint_Index]);
            });
        }
        else
        {
            currentTweener = carTarget.xt_PathMove(tweenPathTool, duration, tweenPathTool.PathOrientation, tweenPathTool.PathOrientationVector, isAutoKill).SetEase(easeMode).SetDelay(delay).SetLoop(loop, loopType).SetLoopingDelay(loopDelay).OnStart(() =>
            {
                CreateAlphaTween().Play();
                if (ValidDraft() == 1)
                    CreateRotationTween().Play();
            }).OnRewind(() =>
            {
                if (!Application.isPlaying)
                {
                    carPaint_Index = 0;
                    carTargetImage.material.SetColor("_Color", carPaint_Colors[carPaint_Index]);
                    return;
                }
                ClearGhost();
                NextCarPaint();
                CreateAlphaTween().Play();
                if (ValidDraft() == 1)
                    CreateRotationTween().Play();
            }).OnKill(() =>
            {
                carPaint_Index = 0;
                carTargetImage.material.SetColor("_Color", carPaint_Colors[carPaint_Index]);
            });
        }

        if (!Application.isPlaying)
        {
            CreateRotationTween();
            CreateAlphaTween();
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

        if (alphaTween != null)
        {
            if (alphaTween.IsPaused)
                alphaTween.Resume();
            else
                alphaTween.Pause();
        }
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

    #region Alpha动画
    /// <summary>
    /// 创建一个车辆透明度动画
    /// </summary>
    /// <returns></returns>
    public XTween_Interface CreateAlphaTween()
    {
        return alphaTween = carTargetImage.xt_Alpha_To(alphaTarget, duration, true, EaseMode.Linear, true, () => alphaFrom, true, alphaCurve).SetDelay(alphaDelay).SetLoop(0).OnComplete((s) =>
        {
            if (carTargetImage != null)
                carTargetImage.color = new Color(carTargetImage.color.r, carTargetImage.color.g, carTargetImage.color.b, alphaFrom);
        }).OnKill(() =>
        {
            if (carTargetImage != null)
                carTargetImage.color = new Color(carTargetImage.color.r, carTargetImage.color.g, carTargetImage.color.b, alphaFrom);
        });
    }
    #endregion

    #region 漂移旋转动画
    /// <summary>
    /// 创建一个车辆漂移旋转动画
    /// </summary>
    /// <returns></returns>
    public XTween_Interface CreateRotationTween()
    {
        return rotateTween = carTargetImage.rectTransform.xt_Rotate_To(rotateTarget, duration, isRelative, true, TweenSpace.相对, RotationMode, EaseMode.Linear, true, () => rotateFrom, true, rotateCurve).SetDelay(rotateDelay).SetLoop(0).OnComplete((s) =>
        {
            carTargetImage.rectTransform.localRotation = Quaternion.Euler(rotateFrom);
        }).OnKill(() =>
        {
            if (carTargetImage != null)
                carTargetImage.rectTransform.localRotation = Quaternion.Euler(rotateFrom);
        }).SetStepTimeInterval(GhostIntervalTime).OnStepUpdate<Vector3>((s, v, w) =>
        {
            if (v > GhostRange.x && v < GhostRange.y)
            {
                CreateGhost(carTargetImage.rectTransform.eulerAngles);
            }
            //Debug.Log($"StepUpdate:{v}");
        });
    }
    #endregion

    #region 辅助
    /// <summary>
    /// 创建并播放动画
    /// </summary>
    private void CreateAndPlayTweens()
    {
        Tween_Create();
        Tween_Play();
    }
    /// <summary>
    /// 切换到下一个车漆颜色
    /// </summary>
    private void NextCarPaint()
    {
        if (carPaint_Index >= carPaint_Colors.Length - 1)
            carPaint_Index = 0;
        else
            carPaint_Index++;

        carTargetImage.material.SetColor("_Color", carPaint_Colors[carPaint_Index]);
    }
    /// <summary>
    /// 判断是否漂移
    /// </summary>
    /// <returns></returns>
    private int ValidDraft()
    {
        if (AlwaysDraft)
            return 1;
        else
            return Random.Range(0, 2);
    }
    /// <summary>
    /// 创建残影
    /// </summary>
    /// <param name="rot"></param>
    private void CreateGhost(Vector3 rot)
    {
        GameObject ghost = new GameObject();

        RectTransform rect_ghost = ghost.AddComponent<RectTransform>();
        rect_ghost.SetParent(carRoot);
        rect_ghost.sizeDelta = carTargetImage.rectTransform.sizeDelta;
        rect_ghost.anchoredPosition3D = new Vector3(carTarget.anchoredPosition.x, carTarget.anchoredPosition.y, 0);
        rect_ghost.eulerAngles = rot;
        rect_ghost.localScale = Vector3.one;

        Image gho_img = ghost.AddComponent<Image>();
        gho_img.material = new Material(carTargetImage.material);
        gho_img.material.SetTexture("_Map", GhostTexture);
        gho_img.material.SetColor("_Color", carPaint_Colors[carPaint_Index]);
        gho_img.material.SetTexture("_Shadow", null);
        gho_img.material.SetTexture("_Mask", null);

        demo_path_drive_ghost gho = ghost.AddComponent<demo_path_drive_ghost>();
        if (Application.isPlaying)
            gho.CreateGhost(gho_img, GhostDuration, GhostDelay);

        Ghosts.Add(gho);
    }
    /// <summary>
    /// 清理残影
    /// </summary>
    public void ClearGhost()
    {
        // 减法删除
        for (int i = Ghosts.Count - 1; i >= 0; i--)
        {
            Ghosts[i].KillTween();
            DestroyImmediate(Ghosts[i].gameObject, true);
        }
        Ghosts.Clear();
    }
    #endregion
}