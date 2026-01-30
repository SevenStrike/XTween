/*
 * ============================================================================
 * ⚠️ 版权声明（禁止删除、禁止修改、衍生作品必须保留此注释）⚠️
 * ============================================================================
 * 版权声明 Copyright (C) 2025-Present Nanjing SevenStrike Media Co., Ltd.
 * 中文名称：南京塞维斯传媒有限公司
 * 英文名称：SevenStrikeMedia
 * 项目作者：徐寅智
 * 项目名称：XTween - Unity 高性能动画架构插件
 * 项目启动：2025年8月
 * 官方网站：http://sevenstrike.com/
 * 授权协议：GNU Affero General Public License Version 3 (AGPL 3.0)
 * 协议说明：
 * 1. 你可以自由使用、修改、分发本插件的源代码，但必须保留此版权注释
 * 2. 基于本插件修改后的衍生作品，必须同样遵循 AGPL 3.0 授权协议
 * 3. 若将本插件用于网络服务（如云端Unity编辑器、在线动效生成工具），必须公开修改后的完整源代码
 * 4. 完整协议文本可查阅：https://www.gnu.org/licenses/agpl-3.0.html
 * ============================================================================
 * 违反本注释保留要求，将违反 AGPL 3.0 授权协议，需承担相应法律责任
 */
namespace SevenStrikeModules.XTween
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Random = UnityEngine.Random;

    /// <summary>
    /// 随机延迟
    /// </summary>
    [System.Serializable]
    public struct RandomDelay
    {
        /// <summary>
        /// 最小随机值
        /// </summary>
        public float Min;
        /// <summary>
        /// 最大随机值
        /// </summary>
        public float Max;

        /// <summary>
        /// 设置随机值
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void SetDelay(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }

    /// <summary>
    /// 控制动画的播放、延迟、循环等行为的控制器类
    /// 提供动画的基本配置和控制功能，支持多种动画类型（如位置、缩放_Scale、颜色等）
    /// </summary>
    public partial class XTween_Controller : MonoBehaviour
    {
        /// <summary>
        /// 当前的动画对象，用于控制动画的执行
        /// </summary>
        [SerializeReference] public XTween_Interface CurrentTweener;
        /// <summary>
        /// 调试动画信息
        /// </summary>
        [SerializeField] public bool DebugMode = false;

        #region 动作委托
        /// <summary>
        /// 动作 - 动画开始
        /// </summary>
        public Action act_on_start;
        /// <summary>
        /// 动作 - 动画停止 & 杀死
        /// </summary>
        public Action act_on_stop;
        /// <summary>
        /// 动作 - 动画完成
        /// </summary>
        public Action<float> act_on_complete;
        /// <summary>
        /// 动作 - 动画杀死
        /// </summary>
        public Action act_on_kill;
        /// <summary>
        /// 动作 - 动画暂停
        /// </summary>
        public Action act_on_pause;
        /// <summary>
        /// 动作 - 动画继续
        /// </summary>
        public Action act_on_resume;
        /// <summary>
        /// 动作 - 动画倒退
        /// </summary>
        public Action act_on_rewind;
        /// <summary>
        /// 动作 - 动画延迟更新
        /// </summary>
        public Action<float> act_on_delayUpdate;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<int, float, float> act_onUpdate_int;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<int, float, float> act_onStepUpdate_int;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<int, float> act_onProgress_int;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<float, float, float> act_onUpdate_float;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<float, float, float> act_onStepUpdate_float;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<float, float> act_onProgress_float;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<string, float, float> act_onUpdate_string;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<string, float, float> act_onStepUpdate_string;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<string, float> act_onProgress_string;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<Vector2, float, float> act_onUpdate_vector2;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<Vector2, float, float> act_onStepUpdate_vector2;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<Vector2, float> act_onProgress_vector2;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<Vector3, float, float> act_onUpdate_vector3;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<Vector3, float, float> act_onStepUpdate_vector3;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<Vector3, float> act_onProgress_vector3;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<Vector4, float, float> act_onUpdate_vector4;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<Vector4, float, float> act_onStepUpdate_vector4;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<Vector4, float> act_onProgress_vector4;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<Quaternion, float, float> act_onUpdate_quaternion;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<Quaternion, float, float> act_onStepUpdate_quaternion;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<Quaternion, float> act_onProgress_quaternion;

        /// <summary>
        /// 动作 - 动画更新
        /// </summary>
        public Action<Color, float, float> act_onUpdate_color;
        /// <summary>
        /// 动作 - 动画步进更新
        /// </summary>
        public Action<Color, float, float> act_onStepUpdate_color;
        /// <summary>
        /// 动作 - 动画进度
        /// </summary>
        public Action<Color, float> act_onProgress_color;
        #endregion

        #region 动画参数
        /// <summary>
        /// 动画的持续时间，单位为秒
        /// </summary>
        [SerializeField] public float Duration = 1;
        /// <summary>
        /// 动画的延迟时间，单位为秒
        /// </summary>
        [SerializeField] public float Delay = 0;
        /// <summary>
        /// 是否使用随机延迟时间
        /// </summary>
        [SerializeField] public bool UseRandomDelay = false;
        /// <summary>
        /// 随机延迟的配置对象，仅当UseRandomDelay为true时生效
        /// </summary>
        [SerializeField] public RandomDelay RandomDelay = new RandomDelay();
        /// <summary>
        /// 动画的缓动模式，用于控制动画的速度变化
        /// </summary>
        [SerializeField] public EaseMode EaseMode = EaseMode.InOutCubic;
        /// <summary>
        /// 是否使用自定义动画曲线
        /// </summary>
        [SerializeField] public bool UseCurve = false;
        /// <summary>
        /// 自定义的动画曲线，仅当UseCurve为true时生效
        /// </summary>
        [SerializeField] public AnimationCurve Curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        /// <summary>
        /// 动画的循环次数，-1表示无限循环，0表示单次播放，大于0按照次数循环
        /// </summary>
        [SerializeField] public int LoopCount = 0;
        /// <summary>
        /// 每次循环之间的延迟时间，单位为秒
        /// </summary>
        [SerializeField] public float LoopDelay = 0;
        /// <summary>
        /// 循环的类型，例如重新开始或反转
        /// </summary>
        [SerializeField] public XTween_LoopType LoopType = XTween_LoopType.Restart;
        /// <summary>
        /// 是否从初始值开始动画
        /// </summary>
        [SerializeField] public bool IsFromMode = false;
        /// <summary>
        /// 是否使用相对值进行动画
        /// </summary>
        [SerializeField] public bool IsRelative = false;
        /// <summary>
        /// 动画完成后是否自动销毁该动画对象
        /// </summary>
        [SerializeField] public bool IsAutoKill = false;
        /// <summary>
        /// 是否以扩展原字符串方式进行字符串动画
        /// </summary>
        [SerializeField] public bool IsExtendedString = false;
        /// <summary>
        /// 字符串动画光标
        /// </summary>
        [SerializeField] public string TextCursor = "_";
        /// <summary>
        /// 字符串动画光标闪烁频率（越小越快）
        /// </summary>
        [SerializeField] public float CursorBlinkTime = 0.5f;
        /// <summary>
        /// 四元数过渡方式
        /// </summary>
        [SerializeField] public HudRotateMode HudRotateMode = HudRotateMode.SlerpUnclamped;
        /// <summary>
        /// 欧拉角度旋转方式
        /// </summary>
        [SerializeField] public RotationMode RotationMode = RotationMode.Normal;
        /// <summary>
        /// 震动频率
        /// </summary>
        [SerializeField] public float Vibrato;
        /// <summary>
        /// 震动随机度
        /// </summary>
        [SerializeField] public float Randomness;
        /// <summary>
        /// 震动渐变过渡
        /// </summary>
        [SerializeField] public bool FadeShake;
        /// <summary>
        /// 自动开始动画
        /// </summary>
        [SerializeField] public bool AutoStart;
        #endregion

        #region 动画目标值（End）
        /// <summary>
        /// 动画的结束值，类型为整数
        /// </summary>
        [SerializeField] public int EndValue_Int = 0;
        /// <summary>
        /// 动画的结束值，类型为浮点数
        /// </summary>
        [SerializeField] public float EndValue_Float = 0;
        /// <summary>
        /// 动画的结束值，类型为字符串
        /// </summary>
        [SerializeField] public string EndValue_String = "";
        /// <summary>
        /// 动画的结束值，类型为二维向量（二维向量_Vector2）
        /// </summary>
        [SerializeField] public Vector2 EndValue_Vector2 = Vector2.zero;
        /// <summary>
        /// 动画的结束值，类型为三维向量（三维向量_Vector3）
        /// </summary>
        [SerializeField] public Vector3 EndValue_Vector3 = Vector3.zero;
        /// <summary>
        /// 动画的结束值，类型为四维向量（四维向量_Vector4）
        /// </summary>
        [SerializeField] public Vector4 EndValue_Vector4 = Vector4.zero;
        /// <summary>
        /// 动画的结束值，类型为颜色（颜色_Color）
        /// </summary>
        [SerializeField] public Color EndValue_Color = Color.white;
        /// <summary>
        /// 动画的结束值，类型为四元数（四元数_Quaternion），用于表示旋转
        /// </summary>
        [SerializeField] public Quaternion EndValue_Quaternion = Quaternion.identity;
        #endregion

        #region 动画起始值（From）
        /// <summary>
        /// 动画的起始值，类型为整数
        /// </summary>
        [SerializeField] public int FromValue_Int = 0;
        /// <summary>
        /// 动画的起始值，类型为浮点数
        /// </summary>
        [SerializeField] public float FromValue_Float = 0;
        /// <summary>
        /// 动画的起始值，类型为字符串
        /// </summary>
        [SerializeField] public string FromValue_String = "";
        /// <summary>
        /// 动画的起始值，类型为二维向量（二维向量_Vector2）
        /// </summary>
        [SerializeField] public Vector2 FromValue_Vector2 = Vector2.zero;
        /// <summary>
        /// 动画的起始值，类型为三维向量（三维向量_Vector3）
        /// </summary>
        [SerializeField] public Vector3 FromValue_Vector3 = Vector3.zero;
        /// <summary>
        /// 动画的起始值，类型为四维向量（四维向量_Vector4）
        /// </summary>
        [SerializeField] public Vector4 FromValue_Vector4 = Vector4.zero;
        /// <summary>
        /// 动画的起始值，类型为颜色（颜色_Color）
        /// </summary>
        [SerializeField] public Color FromValue_Color = Color.white;
        /// <summary>
        /// 动画的起始值，类型为四元数（四元数_Quaternion），用于表示旋转
        /// </summary>
        [SerializeField] public Quaternion FromValue_Quaternion = Quaternion.identity;
        #endregion

        #region 动画目标 & 类型
        /// <summary>
        /// 当前动画类型枚举，用于选择要执行的动画种类
        /// 如位置、旋转_Rotation、缩放_Scale、颜色等基础动画，或特定组件的高级动画
        /// </summary>
        [SerializeField] public XTweenTypes TweenTypes = XTweenTypes.无_None;
        /// <summary>
        /// 位置动画子类型，当TweenTypes为Position时生效
        /// 用于指定使用2D锚点位置还是3D锚点位置进行动画
        /// </summary>
        [SerializeField] public XTweenTypes_Positions TweenTypes_Positions = XTweenTypes_Positions.锚点位置_AnchoredPosition;
        /// <summary>
        /// 旋转动画子类型，当TweenTypes为Rotation时生效
        /// 用于选择使用欧拉角旋转还是四元数旋转
        /// </summary>
        [SerializeField] public XTweenTypes_Rotations TweenTypes_Rotations = XTweenTypes_Rotations.欧拉角度_Euler;
        /// <summary>
        /// 透明度动画子类型，当TweenTypes为Alpha时生效，用于指定是修改Image组件透明度还是CanvasGroup组件透明度
        /// </summary>
        [SerializeField] public XTweenTypes_Alphas TweenTypes_Alphas = XTweenTypes_Alphas.Image组件;
        /// <summary>
        /// 抖动动画子类型，当TweenTypes为Shake时生效
        /// 用于选择抖动效果作用的属性：位置_Position/旋转_Rotation/缩放_Scale/尺寸_Size
        /// </summary>
        [SerializeField] public XTweenTypes_Shakes TweenTypes_Shakes = XTweenTypes_Shakes.位置_Position;
        /// <summary>
        /// HudText文本动画子类型，当TweenTypes为Text时生效
        /// 用于选择文本组件要动画化的属性：字体/颜色_Color/间距等
        /// </summary>
        [SerializeField] public XTweenTypes_Text TweenTypes_Text = XTweenTypes_Text.文字尺寸_FontSize;
        /// <summary>
        /// HUD HudTmpText文本动画子类型，当TweenTypes为TmpText时生效
        /// 用于选择TextMeshPro文本要动画化的属性，包含更多文本控制选项
        /// </summary>
        [SerializeField] public XTweenTypes_TmpText TweenTypes_TmpText = XTweenTypes_TmpText.文字尺寸_FontSize;
        /// <summary>
        /// To动画子类型，当TweenTypes为To时生效
        /// </summary>
        [SerializeField] public XTweenTypes_To TweenTypes_To = XTweenTypes_To.整数_Int;

#pragma warning disable CS0414
        /// <summary>
        /// 索引 - 当前动画类型枚举
        /// </summary>
        [SerializeField] public string index_TweenTypes = "无_None";
        /// <summary>
        /// 索引 - 位置动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_Positions = "锚点位置_AnchoredPosition";
        /// <summary>
        /// 索引 - 旋转动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_Rotations = "欧拉角度_Euler";
        /// <summary>
        /// 索引 - 透明度动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_Alphas = "Image组件";
        /// <summary>
        /// 索引 - 抖动动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_Shakes = "位置_Position";
        /// <summary>
        /// 索引 - HudText文本动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_Text = "文字尺寸_FontSize";
        /// <summary>
        /// 索引 - HudTmpText文本动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_TmpText = "文字尺寸_FontSize";
        /// <summary>
        /// 索引 - To动画子类型
        /// </summary>
        [SerializeField] public string index_TweenTypes_To = "整数_Int";
#pragma warning restore CS0414
        #endregion

        #region 预览选项
#pragma warning disable CS0414
        /// <summary>
        /// 索引 - 自动停止预览
        /// </summary>
        [SerializeField] private bool index_AutoKillPreviewTweens = true;
        /// <summary>
        /// 索引 - 杀死或停止预览时是否先重置动画再停止或杀死
        /// </summary>
        [SerializeField] private bool index_RewindPreviewTweensWithKill = true;
        /// <summary>
        /// 索引 - 停止预览后是否清空预览列表
        /// </summary>
        [SerializeField] private bool index_ClearPreviewTweensWithKill = true;
#pragma warning restore CS0414
        #endregion

        #region 组件节点
        [SerializeField] public RectTransform Target_RectTransform;
        [SerializeField] public Image Target_Image;
        [SerializeField] public CanvasGroup Target_CanvasGroup;
        [SerializeField] public Text Target_Text;
        [SerializeField] public TextMeshProUGUI Target_TmpText;
        [SerializeField] public XTween_PathTool Target_PathTool;
        #endregion

        #region 起源值
        [SerializeField] private int Target_Int;
        [SerializeField] private float Target_Float;
        [SerializeField] private string Target_String;
        [SerializeField] private Vector2 Target_Vector2;
        [SerializeField] private Vector3 Target_Vector3;
        [SerializeField] private Vector4 Target_Vector4;
        [SerializeField] private Color Target_Color = Color.white;
        #endregion

        #region 控制按键
        [SerializeField] private bool keyControl_Enabled = true;
        /// <summary>
        /// 按键控制 - 动画创建
        /// </summary>
        [SerializeField] private KeyCode keyControl_Tween_Create = KeyCode.C;
        /// <summary>
        /// 按键控制 - 动画播放
        /// </summary>
        [SerializeField] private KeyCode keyControl_Tween_Play = KeyCode.W;
        /// <summary>
        /// 按键控制 - 动画倒退
        /// </summary>
        [SerializeField] private KeyCode keyControl_Tween_Rewind = KeyCode.R;
        /// <summary>
        /// 按键控制 - 动画杀死
        /// </summary>
        [SerializeField] private KeyCode keyControl_Tween_Kill = KeyCode.E;
        /// <summary>
        /// 按键控制 - 动画重播
        /// </summary>
        [SerializeField] private KeyCode keyControl_Tween_Replay = KeyCode.T;
        #endregion

        void Start()
        {
            GetComponents();

            if (AutoStart)
            {
                Tween_Create();
                Tween_Play();
            }
        }

        void Update()
        {
            Tween_Control();
        }

        #region 辅助
        /// <summary>
        /// 随机化延迟时间
        /// </summary>
        private void RandomDelaySet()
        {
            if (!UseRandomDelay)
                return;
            Delay = Random.Range(RandomDelay.Min, RandomDelay.Max);
            if (DebugMode)
                XTween_Utilitys.DebugInfo("XTween控制器消息", $"已随机化延迟时间： 最小: {RandomDelay.Min} 最大: {RandomDelay.Max} ！", GUIMsgState.设置);
        }
        /// <summary>
        /// 获取组件
        /// </summary>
        public void GetComponents()
        {
            // 获取所有组件
            XTween_PathTool m_PathTool = GetComponent<XTween_PathTool>();
            if (m_PathTool)
            {
                if (Target_PathTool == null)
                {
                    Target_PathTool = m_PathTool;
                    if (DebugMode)
                        XTween_Utilitys.DebugInfo("XTween控制器消息", $"已找到组件：{m_PathTool}  ！", GUIMsgState.确认);
                }
            }
            UnityEngine.RectTransform m_RectTransform = GetComponent<UnityEngine.RectTransform>();
            if (m_RectTransform)
            {
                if (Target_RectTransform == null)
                {
                    Target_RectTransform = m_RectTransform;
                    if (DebugMode)
                        XTween_Utilitys.DebugInfo("XTween控制器消息", $"已找到组件：{m_RectTransform}  ！", GUIMsgState.确认);
                }
            }
            Image m_Image = GetComponent<Image>();
            if (m_Image)
            {
                if (Target_Image == null)
                {
                    Target_Image = m_Image;
                    if (DebugMode)
                        XTween_Utilitys.DebugInfo("XTween控制器消息", $"已找到组件：{m_Image}  ！", GUIMsgState.确认);
                }
            }
            CanvasGroup m_CanvasGroup = GetComponent<CanvasGroup>();
            if (m_CanvasGroup)
            {
                if (Target_CanvasGroup == null)
                {
                    Target_CanvasGroup = m_CanvasGroup;
                    if (DebugMode)
                        XTween_Utilitys.DebugInfo("XTween控制器消息", $"已找到组件：{m_CanvasGroup}  ！", GUIMsgState.确认);
                }
            }
            Text m_Text = GetComponent<Text>();
            if (m_Text)
            {
                if (Target_Text == null)
                {
                    Target_Text = m_Text;
                    if (DebugMode)
                        XTween_Utilitys.DebugInfo("XTween控制器消息", $"已找到组件：{m_Text}  ！", GUIMsgState.确认);
                }
            }
            TextMeshProUGUI m_TmpText = GetComponent<TextMeshProUGUI>();
            if (m_TmpText)
            {
                if (Target_TmpText == null)
                {
                    Target_TmpText = m_TmpText;
                    if (DebugMode)
                        XTween_Utilitys.DebugInfo("XTween控制器消息", $"已找到组件：{m_TmpText}  ！", GUIMsgState.确认);
                }
            }
        }
        #endregion

        #region 动画控制
        /// <summary>
        /// 动画控制
        /// </summary>
        private void Tween_Control()
        {
            if (keyControl_Enabled)
            {
                if (Input.GetKeyDown(keyControl_Tween_Create))
                {
                    Tween_Create();
                }
                if (Input.GetKeyDown(keyControl_Tween_Play))
                {
                    Tween_Play();
                }
                if (Input.GetKeyDown(keyControl_Tween_Rewind))
                {
                    Tween_Rewind();
                }
                if (Input.GetKeyDown(keyControl_Tween_Kill))
                {
                    Tween_Kill();
                }
                if (Input.GetKeyDown(keyControl_Tween_Replay))
                {
                    Tween_Replay();
                }
            }
        }
        /// <summary>
        /// 动画播放
        /// </summary>
        public void Tween_Create()
        {
            if (TweenTypes == XTweenTypes.无_None)
            {
                if (DebugMode)
                    XTween_Utilitys.DebugInfo("XTween控制器消息", "当动画目标处于： " + TweenTypes.ToString() + " 时，则不会有任何效果！", GUIMsgState.警告);
                return;
            }

            if (UseRandomDelay)
                RandomDelaySet();
            TweenPlay_To();
            TweenPlay_Path();
            TweenPlay_Position();
            TweenPlay_Rotation();
            TweenPlay_Scale();
            TweenPlay_Size();
            TweenPlay_Shake();
            TweenPlay_Color();
            TweenPlay_Alpha();
            TweenPlay_Fill();
            TweenPlay_Tiled();
            TweenPlay_Text();
            TweenPlay_TmpText();

            if (IsAutoKill)
            {
                CurrentTweener.OnKill(Action_AutoKill);
            }
            CurrentTweener.OnComplete(Action_Complete);

            if (DebugMode)
                XTween_Utilitys.DebugInfo("XTween控制器消息", "播放动画： " + TweenTypes.ToString(), GUIMsgState.通知, gameObject);
        }
        /// <summary>
        /// 动画播放
        /// </summary>
        public void Tween_Play()
        {
            if (CurrentTweener == null)
            {
                if (DebugMode)
                    XTween_Utilitys.DebugInfo("XTween控制器消息", "未能播放动画！因为当前不存在动画！", GUIMsgState.警告);
                return;
            }
            if (TweenTypes == XTweenTypes.无_None)
            {
                if (DebugMode)
                    XTween_Utilitys.DebugInfo("XTween控制器消息", "未能播放动画！因为当前模式不是有效的动画模式！", GUIMsgState.警告);
                return;
            }

            CurrentTweener.Play();
        }
        /// <summary>
        /// 动画倒退
        /// </summary>
        public void Tween_Rewind()
        {
            if (CurrentTweener == null)
            {
                if (DebugMode)
                    XTween_Utilitys.DebugInfo("XTween控制器消息", "未能倒退动画！因为当前不存在动画！", GUIMsgState.警告);
                return;
            }
            CurrentTweener.Rewind();
            if (DebugMode)
                XTween_Utilitys.DebugInfo("XTween控制器消息", "倒退动画： " + TweenTypes.ToString(), GUIMsgState.警告);
        }
        /// <summary>
        /// 动画杀死
        /// </summary>
        public void Tween_Kill()
        {
            if (CurrentTweener == null)
            {
                if (DebugMode)
                    XTween_Utilitys.DebugInfo("XTween控制器消息", "未能杀死动画！因为当前不存在动画！", GUIMsgState.警告);
                return;
            }
            CurrentTweener.Kill();
            CurrentTweener = null;
            if (DebugMode)
                XTween_Utilitys.DebugInfo("XTween控制器消息", "杀死动画： " + TweenTypes.ToString(), GUIMsgState.确认);
        }
        /// <summary>
        /// 动画杀死后的委托
        /// </summary>
        private void Action_AutoKill()
        {
            if (CurrentTweener != null)
            {
                CurrentTweener.OnKill(Action_AutoKill, XTweenActionOpration.Unregister);
                CurrentTweener = null;
            }
        }
        /// <summary>
        /// 动画完成后的委托
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Action_Complete(float duration)
        {
            if (CurrentTweener != null)
            {
                CurrentTweener.OnComplete(Action_Complete, XTweenActionOpration.Unregister);
            }
        }
        /// <summary>
        /// 动画重播
        /// </summary>
        public void Tween_Replay()
        {
            if (CurrentTweener == null)
            {
                if (DebugMode)
                    XTween_Utilitys.DebugInfo("XTween控制器消息", "未能重播动画！因为当前不存在动画！", GUIMsgState.警告);
                return;
            }
            Tween_Rewind();
            Tween_Play();
            if (DebugMode)
                XTween_Utilitys.DebugInfo("XTween控制器消息", "重播动画： " + TweenTypes.ToString(), GUIMsgState.通知);
        }
        /// <summary>
        /// 动画重建
        /// </summary>
        public void Tween_ReCreate()
        {
            //if (CurrentTweener == null)
            //{
            //    if (DebugMode)
            //        XTween_Utilitys.DebugInfo("XTween控制器消息", "未能重播动画！因为当前不存在动画！", GUIMsgState.警告);
            //    return;
            //}
            Tween_Kill();
            Tween_Rewind();
            Tween_Create();
            Tween_Play();
            if (DebugMode)
                XTween_Utilitys.DebugInfo("XTween控制器消息", "重播动画： " + TweenTypes.ToString(), GUIMsgState.通知);
        }
        #endregion
    }
}