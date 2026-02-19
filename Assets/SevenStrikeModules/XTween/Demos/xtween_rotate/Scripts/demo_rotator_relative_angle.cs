using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_rotator_relative_angle : MonoBehaviour
{
    public Image Energy_Flare;
    public Image Ring;
    public Image Arrow_Outside;
    public RectTransform Arrows;
    public Image DifferentRegion;
    public Text text_smoothangle;
    public Text text_angle;

    [Header("Colors")]
    public Color col_EnergyFlare;
    public Color col_Arrow;
    public Color col_DifferentRegion;

    [Header("AngleRange")]
    public int val_max = -360;
    public float val_min = 0;

    [Header("AngleValues")]
    public float lastAngle = 0;
    public float lastAngle_sm = 0;

    [Header("FixedAngles")]
    public float fixedAngle_plus = 135;
    public float fixedAngle_minus = 270;

    [Header("DifferentMark")]
    public float lastDifferentAngle = 0;
    public float lastDifferentAngle_Next = 0;
    public float lastDifferentAngle_Dynamic = 0;

    [Header("TweenArgs")]
    public float duration = 1;
    public bool relative = false;
    public int loopCount;
    public XTween_LoopType loopType;
    public EaseMode ease;
    public RotationMode rotationMode;
    public TweenSpace space;
    public XTween_Interface tweener_arrow;
    public XTween_Interface tweener_different;

    public Button btn_random;
    public Button btn_angle_plus;
    public Button btn_angle_minus;
    public Button btn_reset;

    void Start()
    {
        btn_random.GetComponentInChildren<Text>().text += $" {val_min.ToString("F1")} - {val_max.ToString("F1")} °";
        btn_angle_plus.GetComponentInChildren<Text>().text += fixedAngle_plus.ToString("F1") + " °";
        btn_angle_minus.GetComponentInChildren<Text>().text += fixedAngle_minus.ToString("F1") + " °";

        // 随机角度
        btn_random.onClick.AddListener(() =>
        {
            DifferentRegionCalc(Arrows.localEulerAngles.z);
            float v = value_random();
            text_angle.text = v > 0 ? "+" + v.ToString("F1") : v.ToString("F1");
            DifferentRegionCorrectionDir(lastAngle > 0 ? lastAngle : -lastAngle);
            Tween_Create();
        });

        // 增加角度 - 主要
        btn_angle_plus.onClick.AddListener(() =>
        {
            DifferentRegionCalc(Arrows.localEulerAngles.z);
            lastAngle = fixedAngle_plus;
            DifferentRegionCorrectionDir(lastAngle);
            text_angle.text = "+" + lastAngle.ToString("F1");
            Tween_Create();
        });

        // 减少角度 - 次要
        btn_angle_minus.onClick.AddListener(() =>
        {
            DifferentRegionCalc(Arrows.localEulerAngles.z);
            lastAngle = -fixedAngle_minus;
            DifferentRegionCorrectionDir(-lastAngle);
            text_angle.text = lastAngle.ToString("F1");
            Tween_Create();
        });

        // 重置角度
        btn_reset.onClick.AddListener(() =>
        {
            value_reset();
            text_angle.text = "0";
            Tween_Create();
        });
    }


    void Update()
    {
        text_smoothangle.text = Mathf.Abs(lastAngle_sm).ToString("F1");

        Ring.fillAmount = Mathf.Abs(lastAngle_sm / val_max);

        if (Energy_Flare != null)
            Energy_Flare.color = col_EnergyFlare;

        if (Arrow_Outside != null)
            Arrow_Outside.color = col_Arrow;

        if (DifferentRegion != null)
            DifferentRegion.color = col_DifferentRegion;
    }

    private void OnDrawGizmos()
    {
        if (Energy_Flare != null)
            Energy_Flare.color = col_EnergyFlare;

        if (Arrow_Outside != null)
            Arrow_Outside.color = col_Arrow;

        if (DifferentRegion != null)
            DifferentRegion.color = col_DifferentRegion;
    }

    /// <summary>
    /// 创建动画
    /// </summary>
    private void Tween_Create()
    {
        if (tweener_arrow != null)
        {
            tweener_arrow.Kill();
            tweener_arrow = null;
        }
        tweener_arrow = Arrows.xt_Rotate_To(Vector3.forward * lastAngle, duration, relative, true, space, rotationMode).SetEase(ease).SetLoop(loopCount).SetLoopType(loopType).OnUpdate<Vector3>((v, s, j) =>
        {
            float angle = v.z;
            if (angle < 0)
                angle = 360 - angle;
            else if (angle > 360)
                angle = angle - 360;
            lastAngle_sm = angle;
        }).Play();
        if (tweener_different != null)
        {
            tweener_different.Kill();
            tweener_different = null;
        }
        tweener_different = XTween.To(() => lastDifferentAngle_Dynamic, x => lastDifferentAngle_Dynamic = x, lastDifferentAngle_Next, duration, true).SetEase(ease).OnUpdate<float>((d, c, f) =>
        {
            DifferentRegion.fillAmount = lastDifferentAngle_Dynamic;
        }).OnKill(() =>
        {
            lastDifferentAngle_Dynamic = 0;
        }).Play();
    }
    /// <summary>
    /// 速度数值随机化
    /// </summary>
    public float value_random()
    {
        lastAngle = Random.Range(val_min, val_max);

        // 随机决定正负号
        if (Random.value > 0.5f)
        {
            lastAngle = -lastAngle;
        }

        return lastAngle;
    }
    /// <summary>
    /// 速度数值重置
    /// </summary>
    public void value_reset()
    {
        if (tweener_arrow != null)
        {
            tweener_arrow.Kill();
            tweener_arrow = null;
        }
        if (tweener_different != null)
        {
            tweener_different.Kill();
            tweener_different = null;
        }


        lastAngle = 0;
        lastAngle_sm = 0;
        lastDifferentAngle_Next = 0;
        Arrows.localEulerAngles = Vector3.zero;
        DifferentRegion.rectTransform.localEulerAngles = Vector3.zero;
        DifferentRegion.fillAmount = 0;
    }
    /// <summary>
    /// 差异可视化环的目标角度设定
    /// </summary>
    /// <param name="angle"></param>
    void DifferentRegionCalc(float angle)
    {
        lastDifferentAngle = angle;
        DifferentRegion.rectTransform.localEulerAngles = Vector3.forward * -angle;
    }
    /// <summary>
    /// 差异可视化环的进度计算
    /// </summary>
    /// <param name="angle"></param>
    void DifferentRegionCorrectionDir(float angle)
    {
        if (lastAngle < angle)
        {
            DifferentRegion.fillClockwise = false;
            lastDifferentAngle_Next = angle / 360;
        }
        else
        {
            DifferentRegion.fillClockwise = true;
            lastDifferentAngle_Next = angle / 360;
        }
    }
}
