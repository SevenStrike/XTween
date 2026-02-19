using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class demo_rotator_absolute_angle : MonoBehaviour
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
    public float fixedAngle_Primary = 135;
    public float fixedAngle_Secondary = 270;

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
    public Button btn_fixedAngle_Primary;
    public Button btn_fixedAngle_Secondary;
    public Button btn_reset;

    void Start()
    {
        btn_fixedAngle_Primary.GetComponentInChildren<Text>().text += fixedAngle_Primary.ToString("F1") + " °";
        btn_fixedAngle_Secondary.GetComponentInChildren<Text>().text += fixedAngle_Secondary.ToString("F1") + " °";

        // 随机角度
        btn_random.onClick.AddListener(() =>
        {
            DifferentRegionCalc(lastAngle);
            text_angle.text = value_random().ToString("F1");
            DifferentRegionCorrectionDir(lastAngle);
            Tween_Create();
        });

        // 指定角度 - 主要
        btn_fixedAngle_Primary.onClick.AddListener(() =>
        {
            DifferentRegionCalc(lastAngle);
            lastAngle = fixedAngle_Primary;
            DifferentRegionCorrectionDir(lastAngle);
            text_angle.text = lastAngle.ToString("F1");
            Tween_Create();
        });

        // 指定角度 - 次要
        btn_fixedAngle_Secondary.onClick.AddListener(() =>
        {
            DifferentRegionCalc(lastAngle);
            lastAngle = fixedAngle_Secondary;
            DifferentRegionCorrectionDir(lastAngle);
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
            lastAngle_sm = v.z;
            UpdateColorDisplay(v.z);
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
        return lastAngle = Random.Range(val_min, val_max);
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

        DifferentRegion.rectTransform.localEulerAngles = Vector3.zero;
        DifferentRegion.fillAmount = 0;
    }
    /// <summary>
    /// 更新光环颜色
    /// </summary>
    /// <param name="hue"></param>
    void UpdateColorDisplay(float hue)
    {
        // 将0-360转换为0-1范围（Unity的HSVToRGB需要0-1）
        float normalizedHue = hue / 360f;

        // 使用HSV转RGB
        Color color = Color.HSVToRGB(normalizedHue, 1, 1);
        color.a = 1;

        col_EnergyFlare = color;
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
        if (lastDifferentAngle > angle)
        {
            DifferentRegion.fillClockwise = false;
            lastDifferentAngle_Next = ((1 - ((360 - lastDifferentAngle) / 360))) - (1 - ((360 - angle) / 360));
        }
        else
        {
            DifferentRegion.fillClockwise = true;
            lastDifferentAngle_Next = (1 - ((360 - angle) / 360)) - ((1 - ((360 - lastDifferentAngle) / 360)));
        }
    }
}
