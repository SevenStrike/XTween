using SevenStrikeModules.XTween;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class demo_rotator_radar : demo_base
{
    public RectTransform rect_RadarRotator;
    public RectTransform rect_MapMover;
    public RectTransform rect_SiteCreatorCenter;
    public Image img_ScanTrailEnd;
    public Image img_ScanTrailStart;
    public Color color_ScanTrailStart = Color.white;
    public Color color_scanTrailEnd = Color.green;
    public Button btn_Start;
    public Button btn_Reset;
    public Button btn_NextMapSite;
    public Button btn_PrevMapSite;

    public RotationMode TrailRotationMode;

    [Header("目标")]
    public Color site_Color = Color.white;
    public float site_Offset = 40f;
    public float site_Size = 16f;
    public float site_SpawnInterval = 0.15f;
    public float site_FadeDuration = 1f;
    public float site_KeepAlive = 0.5f;
    public Vector2 site_SpawnRange = new Vector2(0, 1);
    public Sprite[] site_Sprites;
    [Range(0, 1)] public float site_SpawnWeight_Circle;
    [Range(0, 1)] public float site_SpawnWeight_Triangle;

    [Header("地图地点坐标")]
    public Vector2[] map_StoreSites;
    public int map_SiteIndex = 0;
    public bool map_IsMoved;
    [Range(0.1f, 15f)] public float map_MoveDuration = 1f;
    public EaseMode map_EaseMode = EaseMode.InOutCubic;
    public XTween_Interface map_tween;

    [Header("标签文字")]
    public int label_FontSize = 10;
    public Vector2 label_Offset;
    public Font label_Font;
    public string label_Text = "sitename";

    public override void Start()
    {
        base.Start();

        // 开始运动
        btn_Start.onClick.AddListener(() =>
        {
            Tween_Create();
            Tween_Play();
        });

        // 重置运动
        btn_Reset.onClick.AddListener(() =>
        {
            Tween_Kill();
            rect_RadarRotator.localEulerAngles = Vector3.zero;
            if (map_tween != null)
                map_tween.Kill();
            map_tween = null;
            rect_MapMover.anchoredPosition = Vector2.zero;
            map_SiteIndex = 0;
        });

        // 下一个地点
        btn_NextMapSite.onClick.AddListener(() =>
        {
            if (map_SiteIndex >= map_StoreSites.Length - 1)
                map_SiteIndex = 0;
            else
                map_SiteIndex++;
            mapMove();
        });

        // 上一个地点
        btn_PrevMapSite.onClick.AddListener(() =>
        {
            if (map_SiteIndex <= 0)
                map_SiteIndex = map_StoreSites.Length - 1;
            else
                map_SiteIndex--;

            mapMove();
        });
    }

    public override void Update()
    {
        if (img_ScanTrailEnd != null)
            img_ScanTrailEnd.color = color_scanTrailEnd;
        if (img_ScanTrailStart != null)
            img_ScanTrailStart.color = color_ScanTrailStart;
        base.Update();
    }

    private void OnDrawGizmos()
    {
        if (img_ScanTrailEnd != null)
            img_ScanTrailEnd.color = color_scanTrailEnd;
        if (img_ScanTrailStart != null)
            img_ScanTrailStart.color = color_ScanTrailStart;
    }

    #region 动画控制 - 重写
    /// <summary>
    /// 创建动画
    /// </summary>
    public override void Tween_Create()
    {
        Tween_Kill();
        currentTweener = rect_RadarRotator.xt_Rotate_To(Vector3.forward * -360, duration, isRelative, isAutoKill, TweenSpace.相对, TrailRotationMode).SetLoop(loop).SetLoopingDelay(loopDelay).SetLoopType(loopType).SetEase(easeMode).OnComplete((d) =>
        {

        }).SetStepTimeInterval(site_SpawnInterval).OnStepUpdate<Vector3>((s, v, w) =>
        {
            if (v > site_SpawnRange.x && v < site_SpawnRange.y)
            {
                if (Application.isPlaying)
                    CreateTarget(s);
            }
        }).Play();
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

    #region 辅助
    /// <summary>
    /// 创建残影
    /// </summary>
    /// <param name="euler"></param>
    private void CreateTarget(Vector3 euler)
    {
        if (map_IsMoved)
            return;

        //创建扫描标记物容器
        GameObject siteContainer = new GameObject();
        siteContainer.name = "SiteAnchor";
        RectTransform rect_siteContainer = siteContainer.AddComponent<RectTransform>();
        demo_rotator_radar_targetsite site = siteContainer.AddComponent<demo_rotator_radar_targetsite>();
        CanvasGroup canvasGroup = siteContainer.AddComponent<CanvasGroup>();

        rect_siteContainer.SetParent(rect_SiteCreatorCenter);
        float DistancePer = (Random.Range(0f, 1f) * (220 - site_Offset)) + site_Offset;

        // 根据扫描角度计算残影位置（雷达圆周坐标）
        // 获取扫描角度（Z轴是UI平面的旋转角度，0°=向上，90°=向右）
        float scanAngle = -euler.z;
        // 角度转弧度（三角函数需要弧度）
        float radian = scanAngle * Mathf.Deg2Rad;
        // 计算圆周坐标（雷达中心为原点的偏移量）
        // 核心公式：x = 半径 * sin(角度)  |  y = 半径 * cos(角度)（适配UI坐标系）
        float offsetX = DistancePer * Mathf.Sin(radian);
        float offsetY = DistancePer * Mathf.Cos(radian);
        // 最终位置 = 雷达中心位置 + 圆周偏移（Z轴保持0）
        rect_siteContainer.anchoredPosition3D = new Vector3(
            rect_SiteCreatorCenter.anchoredPosition3D.x + offsetX,
            rect_SiteCreatorCenter.anchoredPosition3D.y + offsetY,
            0
        );
        rect_siteContainer.localScale = Vector3.one;
        //并入地图层级
        rect_siteContainer.SetParent(rect_MapMover);

        //创建扫描标记物
        GameObject sitePoint = new GameObject();
        sitePoint.name = "SitePoint";
        RectTransform rect_site = sitePoint.AddComponent<RectTransform>();
        rect_site.SetParent(rect_siteContainer);
        rect_site.sizeDelta = new Vector2(site_Size, site_Size);
        rect_site.anchoredPosition3D = Vector3.zero;
        rect_site.localEulerAngles = Vector3.forward * Random.Range(0, 360);
        rect_site.localScale = Vector3.one;
        Image img = sitePoint.AddComponent<Image>();
        img.raycastTarget = false;
        img.color = site_Color;
        int index = GetWeightedRandom(0, 1, site_SpawnWeight_Circle, site_SpawnWeight_Triangle);
        img.sprite = site_Sprites[index];

        site.canvasGroup = canvasGroup;
        if (Application.isPlaying)
            site.CreateGhost(img, site_FadeDuration, site_KeepAlive);

        //创建扫描标记物标签
        GameObject siteLabel = new GameObject();
        siteLabel.name = "SiteLabel";
        RectTransform rect_label = siteLabel.AddComponent<RectTransform>();
        rect_label.SetParent(rect_siteContainer);
        rect_label.anchoredPosition3D = Vector3.zero;
        rect_label.anchoredPosition = new Vector2(label_Offset.x, label_Offset.y);
        rect_label.localEulerAngles = Vector3.zero;
        rect_label.localScale = Vector3.one;

        Text label = rect_label.gameObject.AddComponent<Text>();
        label.fontSize = label_FontSize;
        label.alignment = TextAnchor.MiddleCenter;
        label.font = label_Font;
        label.text = label_Text;
    }
    /// <summary>
    /// 根据权重随机选择两个值中的一个
    /// </summary>
    /// <param name="valueA">第一个可选值</param>
    /// <param name="valueB">第二个可选值</param>
    /// <param name="weightA">第一个值的权重（数值越大几率越高）</param>
    /// <param name="weightB">第二个值的权重（数值越大几率越高）</param>
    /// <returns>随机选中的值</returns>
    public static T GetWeightedRandom<T>(T valueA, T valueB, float weightA, float weightB)
    {
        // 安全校验：权重不能为负数
        weightA = Mathf.Max(0, weightA);
        weightB = Mathf.Max(0, weightB);

        // 处理权重都为0的特殊情况（均等概率）
        if (weightA <= 0 && weightB <= 0)
        {
            return Random.value > 0.5f ? valueA : valueB;
        }

        // 计算权重总和
        float totalWeight = weightA + weightB;
        // 生成0到总权重之间的随机数
        float randomPoint = Random.Range(0f, totalWeight);

        // 判断随机数落在哪个区间
        if (randomPoint < weightA)
        {
            return valueA;
        }
        else
        {
            return valueB;
        }
    }
    /// <summary>
    /// 地图位移动画
    /// </summary>
    public void mapMove()
    {
        if (map_tween != null)
        {
            map_tween.Kill();
            map_tween = null;
        }
        Vector2 pos = map_StoreSites[map_SiteIndex];
        map_tween = rect_MapMover.xt_AnchoredPosition_To(pos, map_MoveDuration, false, true).SetEase(map_EaseMode).OnComplete((d) => { map_IsMoved = false; }).Play();
        map_IsMoved = true;
    }
    #endregion
}
