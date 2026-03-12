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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    #region 预设类
    /// <summary>
    /// 动画预设数据基类
    /// 包含所有动画类型共有的基础参数
    /// </summary>
    [Serializable]
    public class XTweenPresetBase
    {
        /// <summary>
        /// 预设名称，用于在预设列表中显示
        /// </summary>
        [SerializeField] public string Name;
        /// <summary>
        /// 预设描述，详细说明这个预设的用途和效果
        /// </summary>
        [SerializeField] public string Description;
        /// <summary>
        /// 动画持续时间，单位：秒
        /// </summary>
        [SerializeField] public float Duration = 1f;
        /// <summary>
        /// 动画开始前的延迟时间，单位：秒
        /// </summary>
        [SerializeField] public float Delay = 0f;
        /// <summary>
        /// 是否使用随机延迟时间
        /// 如果为true，则Delay将被RandomDelay范围内的随机值覆盖
        /// </summary>
        [SerializeField] public bool UseRandomDelay = false;
        /// <summary>
        /// 随机延迟的配置，包含最小值和最大值
        /// 仅当UseRandomDelay为true时生效
        /// </summary>
        [SerializeField] public RandomDelay RandomDelay;
        /// <summary>
        /// 动画缓动模式，控制动画速度变化曲线
        /// 例如：Linear线性、InOutCubic先慢后快再慢等
        /// </summary>
        [SerializeField] public EaseMode EaseMode = EaseMode.InOutCubic;
        /// <summary>
        /// 是否使用自定义动画曲线
        /// 如果为true，则使用Curve代替EaseMode
        /// </summary>
        [SerializeField] public bool UseCurve = false;
        /// <summary>
        /// 自定义动画曲线
        /// 可以通过编辑曲线精确控制动画进度
        /// </summary>
        [SerializeField] public AnimationCurve Curve;
        /// <summary>
        /// 动画循环次数
        /// -1: 无限循环
        /// 0: 不循环，播放一次
        /// >0: 循环指定次数
        /// </summary>
        [SerializeField] public int LoopCount = 0;
        /// <summary>
        /// 每次循环之间的延迟时间，单位：秒
        /// </summary>
        [SerializeField] public float LoopDelay = 0f;
        /// <summary>
        /// 循环类型
        /// Restart: 每次循环重新开始
        /// Yoyo: 往返循环，如A→B→A→B
        /// </summary>
        [SerializeField] public XTween_LoopType LoopType = XTween_LoopType.Restart;
        /// <summary>
        /// 是否使用相对值动画
        /// true: 在当前位置基础上增加目标值
        /// false: 直接设置到目标值
        /// </summary>
        [SerializeField] public bool IsRelative = false;
        /// <summary>
        /// 动画完成后是否自动销毁动画对象
        /// true: 动画完成后自动释放资源
        /// false: 动画完成后保留，可以重新播放
        /// </summary>
        [SerializeField] public bool IsAutoKill = false;
        /// <summary>
        /// 是否使用指定的起始值
        /// true: 从FromValue开始动画
        /// false: 从当前值开始动画
        /// </summary>
        [SerializeField] public bool UseFromMode = false;
        /// <summary>
        /// 标记为喜爱的预设
        /// </summary>
        [SerializeField] public bool IsFavourite = false;
        /// <summary>
        /// 设置预设名称
        /// </summary>
        /// <param name="name">预设名称</param>
        public void Set_PresetName(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// 设置预设描述
        /// </summary>
        /// <param name="des">预设描述文本</param>
        public void Set_Preset_Description(string des)
        {
            this.Description = des;
        }

        /// <summary>
        /// 脱离引用克隆 - 虚方法支持派生类重写
        /// </summary>
        public virtual XTweenPresetBase Clone()
        {
            XTweenPresetBase clone = new XTweenPresetBase();
            CopyBaseFields(clone);
            return clone;
        }

        /// <summary>
        /// 复制基类字段到目标对象
        /// </summary>
        protected void CopyBaseFields(XTweenPresetBase target)
        {
            target.Name = this.Name;
            target.Description = this.Description;
            target.Duration = this.Duration;
            target.Delay = this.Delay;
            target.UseRandomDelay = this.UseRandomDelay;
            target.RandomDelay = this.RandomDelay; // 结构体直接复制
            target.EaseMode = this.EaseMode;
            target.UseCurve = this.UseCurve;
            target.LoopCount = this.LoopCount;
            target.LoopDelay = this.LoopDelay;
            target.LoopType = this.LoopType;
            target.IsRelative = this.IsRelative;
            target.IsAutoKill = this.IsAutoKill;
            target.IsFavourite = this.IsFavourite;
            target.UseFromMode = this.UseFromMode;

            // 深拷贝AnimationCurve（引用类型）
            if (this.Curve != null)
            {
                target.Curve = new AnimationCurve();

                // 复制关键帧
                Keyframe[] keyframes = new Keyframe[this.Curve.keys.Length];
                for (int i = 0; i < this.Curve.keys.Length; i++)
                {
                    Keyframe originalKey = this.Curve.keys[i];
                    Keyframe newKey = new Keyframe(
                        originalKey.time,
                        originalKey.value,
                        originalKey.inTangent,
                        originalKey.outTangent,
                        originalKey.inWeight,
                        originalKey.outWeight
                    );
                    newKey.weightedMode = originalKey.weightedMode;
                    keyframes[i] = newKey;
                }

                target.Curve.keys = keyframes;
                target.Curve.preWrapMode = this.Curve.preWrapMode;
                target.Curve.postWrapMode = this.Curve.postWrapMode;
            }
        }
    }

    /// <summary>
    /// Alpha透明度动画预设
    /// 用于控制Image或CanvasGroup的透明度变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Alpha : XTweenPresetBase
    {
        /// <summary>
        /// 透明度动画子类型
        /// Image组件: 控制Image的透明度
        /// CanvasGroup组件: 控制CanvasGroup的透明度
        /// </summary>
        [SerializeField] public XTweenTypes_Alphas AlphaType = XTweenTypes_Alphas.Image组件;
        /// <summary>
        /// 目标透明度值，范围0-1
        /// 0=完全透明，1=完全不透明
        /// </summary>
        [SerializeField] public float EndValue = 0f;
        /// <summary>
        /// 起始透明度值，范围0-1
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public float FromValue = 0f;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Alpha clone = new XTweenPreset_Alpha();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.AlphaType = this.AlphaType;
            clone.EndValue = this.EndValue;
            clone.FromValue = this.FromValue;

            return clone;
        }
    }

    /// <summary>
    /// Color颜色动画预设
    /// 用于控制Image或Text的颜色变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Color : XTweenPresetBase
    {
        /// <summary>
        /// 目标颜色值
        /// </summary>
        [SerializeField] public Color EndValue = Color.white;
        /// <summary>
        /// 起始颜色值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Color FromValue = Color.white;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Color clone = new XTweenPreset_Color();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.EndValue = this.EndValue;
            clone.FromValue = this.FromValue;

            return clone;
        }
    }

    /// <summary>
    /// Position位置动画预设
    /// 用于控制RectTransform的位置变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Position : XTweenPresetBase
    {
        /// <summary>
        /// 位置动画类型
        /// AnchoredPosition: 2D锚点位置
        /// AnchoredPosition3D: 3D锚点位置
        /// </summary>
        [SerializeField] public XTweenTypes_Positions PositionType = XTweenTypes_Positions.锚点位置_AnchoredPosition;
        /// <summary>
        /// 目标位置（2D向量）
        /// 当PositionType为AnchoredPosition时使用
        /// </summary>
        [SerializeField] public Vector2 EndValue_Vector2 = Vector2.zero;
        /// <summary>
        /// 目标位置（3D向量）
        /// 当PositionType为AnchoredPosition3D时使用
        /// </summary>
        [SerializeField] public Vector3 EndValue_Vector3 = Vector3.zero;
        /// <summary>
        /// 起始位置（2D向量）
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector2 FromValue_Vector2 = Vector2.zero;
        /// <summary>
        /// 起始位置（3D向量）
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector3 FromValue_Vector3 = Vector3.zero;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Position clone = new XTweenPreset_Position();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.PositionType = this.PositionType;
            clone.EndValue_Vector2 = this.EndValue_Vector2;
            clone.EndValue_Vector3 = this.EndValue_Vector3;
            clone.FromValue_Vector2 = this.FromValue_Vector2;
            clone.FromValue_Vector3 = this.FromValue_Vector3;

            return clone;
        }
    }

    /// <summary>
    /// Rotation旋转动画预设
    /// 用于控制RectTransform的旋转变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Rotation : XTweenPresetBase
    {
        /// <summary>
        /// 旋转动画类型
        /// Euler: 使用欧拉角度
        /// Quaternion: 使用四元数
        /// </summary>
        [SerializeField] public XTweenTypes_Rotations RotationType = XTweenTypes_Rotations.欧拉角度_Euler;
        /// <summary>
        /// 目标欧拉角度
        /// 当RotationType为Euler时使用
        /// </summary>
        [SerializeField] public Vector3 EndValue_Euler = Vector3.zero;
        /// <summary>
        /// 目标四元数
        /// 当RotationType为Quaternion时使用
        /// </summary>
        [SerializeField] public Quaternion EndValue_Quaternion = Quaternion.identity;
        /// <summary>
        /// 起始欧拉角度
        /// 仅当UseFromMode为true且RotationType为Euler时生效
        /// </summary>
        [SerializeField] public Vector3 FromValue_Euler = Vector3.zero;
        /// <summary>
        /// 起始四元数
        /// 仅当UseFromMode为true且RotationType为Quaternion时生效
        /// </summary>
        [SerializeField] public Quaternion FromValue_Quaternion = Quaternion.identity;
        /// <summary>
        /// 动画空间
        /// 相对: 在本地空间旋转
        /// 绝对: 在世界空间旋转
        /// </summary>
        [SerializeField] public XTweenRotationSpace RotationSpace = XTweenRotationSpace.相对;
        /// <summary>
        /// 欧拉角度旋转方式
        /// Normal: 正常旋转
        /// ShortestPath: 最短路径
        /// </summary>
        [SerializeField] public XTweenRotationMode RotationMode = XTweenRotationMode.Normal;
        /// <summary>
        /// 四元数插值方式
        /// Slerp: 球面插值
        /// Lerp: 线性插值
        /// </summary>
        [SerializeField] public XTweenRotateLerpType RotateLerpMode = XTweenRotateLerpType.SlerpUnclamped;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Rotation clone = new XTweenPreset_Rotation();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.RotationType = this.RotationType;
            clone.EndValue_Euler = this.EndValue_Euler;
            clone.EndValue_Quaternion = this.EndValue_Quaternion;
            clone.FromValue_Euler = this.FromValue_Euler;
            clone.FromValue_Quaternion = this.FromValue_Quaternion;
            clone.RotationSpace = this.RotationSpace;
            clone.RotationMode = this.RotationMode;
            clone.RotateLerpMode = this.RotateLerpMode;
            return clone;
        }
    }

    /// <summary>
    /// Scale缩放动画预设
    /// 用于控制RectTransform的缩放变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Scale : XTweenPresetBase
    {
        /// <summary>
        /// 目标缩放值
        /// 例如：Vector3.one 表示原大
        /// </summary>
        [SerializeField] public Vector3 EndValue = Vector3.one;
        /// <summary>
        /// 起始缩放值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector3 FromValue = Vector3.one;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Scale clone = new XTweenPreset_Scale();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.EndValue = this.EndValue;
            clone.FromValue = this.FromValue;

            return clone;
        }
    }

    /// <summary>
    /// Size尺寸动画预设
    /// 用于控制RectTransform的尺寸变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Size : XTweenPresetBase
    {
        /// <summary>
        /// 目标尺寸
        /// </summary>
        [SerializeField] public Vector2 EndValue = Vector2.zero;
        /// <summary>
        /// 起始尺寸
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector2 FromValue = Vector2.zero;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Size clone = new XTweenPreset_Size();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.EndValue = this.EndValue;
            clone.FromValue = this.FromValue;

            return clone;
        }
    }

    /// <summary>
    /// Shake震动动画预设
    /// 用于产生抖动效果，如震动、摇晃等
    /// </summary>
    [Serializable]
    public class XTweenPreset_Shake : XTweenPresetBase
    {
        /// <summary>
        /// 震动类型
        /// Position: 位置震动
        /// Rotation: 旋转震动
        /// Scale: 缩放震动
        /// Size: 尺寸震动
        /// </summary>
        [SerializeField] public XTweenTypes_Shakes ShakeType = XTweenTypes_Shakes.位置_Position;
        /// <summary>
        /// 震动强度（3D）
        /// 用于Position、Rotation、Scale类型的震动
        /// </summary>
        [SerializeField] public Vector3 Strength_Vector3 = Vector3.one;
        /// <summary>
        /// 震动强度（2D）
        /// 用于Size类型的震动
        /// </summary>
        [SerializeField] public Vector2 Strength_Vector2 = Vector2.one;
        /// <summary>
        /// 震动频率
        /// 值越大震动越密集
        /// </summary>
        [SerializeField] public float Vibrato = 10f;
        /// <summary>
        /// 震动随机度
        /// 0-180之间的值，控制震动方向的随机性
        /// </summary>
        [SerializeField] public float Randomness = 90f;
        /// <summary>
        /// 是否使用震动渐变
        /// true: 震动强度随时间减弱
        /// false: 震动强度保持不变
        /// </summary>
        [SerializeField] public bool FadeShake = true;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Shake clone = new XTweenPreset_Shake();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.ShakeType = this.ShakeType;
            clone.Strength_Vector3 = this.Strength_Vector3;
            clone.Strength_Vector2 = this.Strength_Vector2;
            clone.Vibrato = this.Vibrato;
            clone.Randomness = this.Randomness;
            clone.FadeShake = this.FadeShake;

            return clone;
        }
    }

    /// <summary>
    /// Text文本动画预设
    /// 用于控制Unity UI Text组件的各种属性
    /// </summary>
    [Serializable]
    public class XTweenPreset_Text : XTweenPresetBase
    {
        /// <summary>
        /// 文本动画类型
        /// FontSize: 文字尺寸
        /// Color: 文字颜色
        /// Content: 文字内容
        /// LineHeight: 行高
        /// </summary>
        [SerializeField] public XTweenTypes_Text TextType = XTweenTypes_Text.文字尺寸_FontSize;
        /// <summary>
        /// 目标整数值
        /// 用于FontSize类型的动画
        /// </summary>
        [SerializeField] public int EndValue_Int = 0;
        /// <summary>
        /// 目标浮点值
        /// 用于LineHeight类型的动画
        /// </summary>
        [SerializeField] public float EndValue_Float = 0;
        /// <summary>
        /// 目标颜色
        /// 用于Color类型的动画
        /// </summary>
        [SerializeField] public Color EndValue_Color = Color.white;
        /// <summary>
        /// 目标字符串
        /// 用于Content类型的动画
        /// </summary>
        [SerializeField] public string EndValue_String = "";
        /// <summary>
        /// 起始整数值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public int FromValue_Int = 0;
        /// <summary>
        /// 起始浮点值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public float FromValue_Float = 0;
        /// <summary>
        /// 起始颜色
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Color FromValue_Color = Color.white;
        /// <summary>
        /// 起始字符串
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public string FromValue_String = "";
        /// <summary>
        /// 是否使用扩展字符串模式
        /// true: 文字会以打字机效果逐字显示
        /// false: 文字直接变化
        /// </summary>
        [SerializeField] public bool IsExtendedString = false;
        /// <summary>
        /// 打字机效果的光标符号
        /// 例如："_", "|", "●"等
        /// </summary>
        [SerializeField] public string TextCursor = "_";
        /// <summary>
        /// 光标闪烁间隔时间，单位：秒
        /// 值越小闪烁越快
        /// </summary>
        [SerializeField] public float CursorBlinkTime = 0.5f;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Text clone = new XTweenPreset_Text();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.TextType = this.TextType;
            clone.EndValue_Int = this.EndValue_Int;
            clone.EndValue_Float = this.EndValue_Float;
            clone.EndValue_Color = this.EndValue_Color;
            clone.EndValue_String = this.EndValue_String;
            clone.FromValue_Int = this.FromValue_Int;
            clone.FromValue_Float = this.FromValue_Float;
            clone.FromValue_Color = this.FromValue_Color;
            clone.FromValue_String = this.FromValue_String;
            clone.IsExtendedString = this.IsExtendedString;
            clone.TextCursor = this.TextCursor;
            clone.CursorBlinkTime = this.CursorBlinkTime;

            return clone;
        }
    }

    /// <summary>
    /// TMP Text文本动画预设
    /// 用于控制TextMeshPro组件的各种属性
    /// </summary>
    [Serializable]
    public class XTweenPreset_TmpText : XTweenPresetBase
    {
        /// <summary>
        /// TMP文本动画类型
        /// FontSize: 文字尺寸
        /// Color: 文字颜色
        /// Content: 文字内容
        /// LineHeight: 行高
        /// Character: 字符间距
        /// Margin: 边距
        /// </summary>
        [SerializeField] public XTweenTypes_TmpText TmpTextType = XTweenTypes_TmpText.文字尺寸_FontSize;
        /// <summary>
        /// 目标浮点值
        /// 用于FontSize、LineHeight、Character类型的动画
        /// </summary>
        [SerializeField] public float EndValue_Float = 0;
        /// <summary>
        /// 目标颜色
        /// 用于Color类型的动画
        /// </summary>
        [SerializeField] public Color EndValue_Color = Color.white;
        /// <summary>
        /// 目标字符串
        /// 用于Content类型的动画
        /// </summary>
        [SerializeField] public string EndValue_String = "";
        /// <summary>
        /// 目标四维向量
        /// 用于Margin类型的动画
        /// </summary>
        [SerializeField] public Vector4 EndValue_Vector4 = Vector4.zero;
        /// <summary>
        /// 起始浮点值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public float FromValue_Float = 0;
        /// <summary>
        /// 起始颜色
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Color FromValue_Color = Color.white;
        /// <summary>
        /// 起始字符串
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public string FromValue_String = "";
        /// <summary>
        /// 起始四维向量
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector4 FromValue_Vector4 = Vector4.zero;
        /// <summary>
        /// 是否使用扩展字符串模式
        /// true: 文字会以打字机效果逐字显示
        /// false: 文字直接变化
        /// </summary>
        [SerializeField] public bool IsExtendedString = false;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_TmpText clone = new XTweenPreset_TmpText();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.TmpTextType = this.TmpTextType;
            clone.EndValue_Float = this.EndValue_Float;
            clone.EndValue_Color = this.EndValue_Color;
            clone.EndValue_String = this.EndValue_String;
            clone.EndValue_Vector4 = this.EndValue_Vector4;
            clone.FromValue_Float = this.FromValue_Float;
            clone.FromValue_Color = this.FromValue_Color;
            clone.FromValue_String = this.FromValue_String;
            clone.FromValue_Vector4 = this.FromValue_Vector4;
            clone.IsExtendedString = this.IsExtendedString;

            return clone;
        }
    }

    /// <summary>
    /// Fill填充动画预设
    /// 用于控制Image组件的填充度变化（如进度条）
    /// </summary>
    [Serializable]
    public class XTweenPreset_Fill : XTweenPresetBase
    {
        /// <summary>
        /// 目标填充度，范围0-1
        /// 0=空，1=满
        /// </summary>
        [SerializeField] public float EndValue = 1f;
        /// <summary>
        /// 起始填充度，范围0-1
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public float FromValue = 0f;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Fill clone = new XTweenPreset_Fill();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.EndValue = this.EndValue;
            clone.FromValue = this.FromValue;

            return clone;
        }
    }

    /// <summary>
    /// Tiled平铺动画预设
    /// 用于控制Image组件的平铺比例变化
    /// </summary>
    [Serializable]
    public class XTweenPreset_Tiled : XTweenPresetBase
    {
        /// <summary>
        /// 目标平铺比例
        /// </summary>
        [SerializeField] public float EndValue = 1f;
        /// <summary>
        /// 起始平铺比例
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public float FromValue = 0f;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Tiled clone = new XTweenPreset_Tiled();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.EndValue = this.EndValue;
            clone.FromValue = this.FromValue;

            return clone;
        }
    }

    /// <summary>
    /// Path路径动画预设
    /// 用于沿预设路径移动
    /// 注意：路径点由XTween_PathTool管理，预设只保存路径名称
    /// </summary>
    [Serializable]
    public class XTweenPreset_Path : XTweenPresetBase
    {
        /// <summary>
        /// 路径名称
        /// 用于查找对应的XTween_PathTool组件中的路径点
        /// </summary>
        [SerializeField] public string PathName = "";

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_Path clone = new XTweenPreset_Path();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.PathName = this.PathName;

            return clone;
        }
    }

    /// <summary>
    /// To原生动画预设
    /// 用于对任意数值进行动画（不限于UI组件）
    /// </summary>
    [Serializable]
    public class XTweenPreset_To : XTweenPresetBase
    {
        /// <summary>
        /// 原生动画数值类型
        /// Int: 整数
        /// Float: 浮点数
        /// String: 字符串
        /// Vector2: 二维向量
        /// Vector3: 三维向量
        /// Vector4: 四维向量
        /// Color: 颜色
        /// </summary>
        [SerializeField] public XTweenTypes_To ToType = XTweenTypes_To.浮点数_Float;
        /// <summary>
        /// 目标整数值
        /// 用于Int类型的动画
        /// </summary>
        [SerializeField] public int EndValue_Int = 0;
        /// <summary>
        /// 目标浮点值
        /// 用于Float类型的动画
        /// </summary>
        [SerializeField] public float EndValue_Float = 0;
        /// <summary>
        /// 目标字符串
        /// 用于String类型的动画
        /// </summary>
        [SerializeField] public string EndValue_String = "";
        /// <summary>
        /// 目标二维向量
        /// 用于Vector2类型的动画
        /// </summary>
        [SerializeField] public Vector2 EndValue_Vector2 = Vector2.zero;
        /// <summary>
        /// 目标三维向量
        /// 用于Vector3类型的动画
        /// </summary>
        [SerializeField] public Vector3 EndValue_Vector3 = Vector3.zero;
        /// <summary>
        /// 目标四维向量
        /// 用于Vector4类型的动画
        /// </summary>
        [SerializeField] public Vector4 EndValue_Vector4 = Vector4.zero;
        /// <summary>
        /// 目标颜色
        /// 用于Color类型的动画
        /// </summary>
        [SerializeField] public Color EndValue_Color = Color.white;
        /// <summary>
        /// 起始整数值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public int FromValue_Int = 0;
        /// <summary>
        /// 起始浮点值
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public float FromValue_Float = 0;
        /// <summary>
        /// 起始字符串
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public string FromValue_String = "";
        /// <summary>
        /// 起始二维向量
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector2 FromValue_Vector2 = Vector2.zero;
        /// <summary>
        /// 起始三维向量
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector3 FromValue_Vector3 = Vector3.zero;
        /// <summary>
        /// 起始四维向量
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Vector4 FromValue_Vector4 = Vector4.zero;
        /// <summary>
        /// 起始颜色
        /// 仅当UseFromMode为true时生效
        /// </summary>
        [SerializeField] public Color FromValue_Color = Color.white;
        /// <summary>
        /// 是否使用扩展字符串模式
        /// true: 文字会以打字机效果逐字显示
        /// false: 文字直接变化
        /// </summary>
        [SerializeField] public bool IsExtendedString = false;
        /// <summary>
        /// 打字机效果的光标符号
        /// </summary>
        [SerializeField] public string TextCursor = "_";
        /// <summary>
        /// 光标闪烁间隔时间，单位：秒
        /// </summary>
        [SerializeField] public float CursorBlinkTime = 0.5f;

        public override XTweenPresetBase Clone()
        {
            XTweenPreset_To clone = new XTweenPreset_To();

            // 复制基类字段
            CopyBaseFields(clone);

            // 复制派生类特有字段
            clone.ToType = this.ToType;
            clone.EndValue_Int = this.EndValue_Int;
            clone.EndValue_Float = this.EndValue_Float;
            clone.EndValue_String = this.EndValue_String;
            clone.EndValue_Vector2 = this.EndValue_Vector2;
            clone.EndValue_Vector3 = this.EndValue_Vector3;
            clone.EndValue_Vector4 = this.EndValue_Vector4;
            clone.EndValue_Color = this.EndValue_Color;
            clone.FromValue_Int = this.FromValue_Int;
            clone.FromValue_Float = this.FromValue_Float;
            clone.FromValue_String = this.FromValue_String;
            clone.FromValue_Vector2 = this.FromValue_Vector2;
            clone.FromValue_Vector3 = this.FromValue_Vector3;
            clone.FromValue_Vector4 = this.FromValue_Vector4;
            clone.FromValue_Color = this.FromValue_Color;
            clone.IsExtendedString = this.IsExtendedString;
            clone.TextCursor = this.TextCursor;
            clone.CursorBlinkTime = this.CursorBlinkTime;

            return clone;
        }
    }

    /// <summary>
    /// 预设容器类
    /// 用于JSON序列化/反序列化
    /// 包含类型标识和具体的预设JSON数据
    /// </summary>
    [Serializable]
    public class XTweenPresetContainer
    {
        /// <summary>
        /// 动画类型标识
        /// 例如：Alpha、Color、Position等
        /// </summary>
        [SerializeField] public string Type;
        /// <summary>
        /// 多个预设数据列表
        /// 每个元素可以是XTweenPreset_Alpha、XTweenPreset_Color等派生类
        /// </summary>
        [SerializeReference] public List<XTweenPresetBase> Presets = new List<XTweenPresetBase>();

        /// <summary>
        /// 深拷贝容器
        /// </summary>
        public XTweenPresetContainer Clone()
        {
            XTweenPresetContainer clone = new XTweenPresetContainer();

            // 复制类型标识
            clone.Type = this.Type;

            // 深拷贝预设列表
            if (this.Presets != null)
            {
                clone.Presets = new List<XTweenPresetBase>();
                foreach (var preset in this.Presets)
                {
                    if (preset != null)
                    {
                        // 调用每个预设的Clone方法进行深拷贝
                        clone.Presets.Add(preset.Clone());
                    }
                    else
                    {
                        clone.Presets.Add(null);
                    }
                }
            }

            return clone;
        }
    }
    #endregion

    /// <summary>
    /// XTween预设管理器
    /// 负责预设的保存、加载、创建、删除等所有操作
    /// 所有预设文件存储在 Resources/Presets/ 目录下
    /// </summary>
    public static class XTween_PresetManager
    {
        /// <summary>
        /// 预设管理器自己的调试开关
        /// 设置为true时会在控制台输出详细的调试信息
        /// </summary>
        public static bool EnableDebugLogs = false;

        #region JsonFile
        /// <summary>
        /// 检查并创建所有类型的默认预设文件
        /// </summary>
        /// <remarks>
        /// 该方法是预设系统的初始化入口，在插件启动或资源检查时调用。
        /// 它会遍历所有支持的动画类型，确保每个类型都有对应的预设文件。
        /// 
        /// 检查流程：
        /// 1. 依次调用 preset_JsonFile_Exist 检查以下所有动画类型：
        ///    - 透明度_Alpha
        ///    - 原生动画_To
        ///    - 路径_Path
        ///    - 位置_Position
        ///    - 旋转_Rotation
        ///    - 缩放_Scale
        ///    - 尺寸_Size
        ///    - 震动_Shake
        ///    - 颜色_Color
        ///    - 填充_Fill
        ///    - 平铺_Tiled
        ///    - 文字_Text
        ///    - 文字_TmpText
        /// 2. 对于每个类型，如果对应的预设文件不存在，则自动创建默认预设文件
        /// 
        /// 使用场景：
        /// - XTween插件初始化时（如第一次导入插件）
        /// - 编辑器菜单中的"重建所有默认预设"功能
        /// - 检测到预设文件丢失或损坏后的恢复操作
        /// - 版本升级时确保新版本的预设文件存在
        /// 
        /// 设计理念：
        /// - 自动化：用户无需手动创建预设文件
        /// - 完整性：确保所有动画类型都有可用的预设示例
        /// - 非侵入性：只创建缺失的文件，不会覆盖用户已有的自定义预设
        /// 
        /// 默认预设内容：
        /// 每个类型的默认预设都包含精心设计的示例，展示该类型的主要功能：
        /// - Alpha：淡入效果、淡入效果2（不同起始值）
        /// - Color：颜色渐变（红→蓝，带Yoyo循环）
        /// - Position：水平移动（左→右）
        /// - Rotation：360度旋转（无限循环）
        /// - Scale：弹性缩放（OutBack缓动）
        /// - Size：尺寸扩大（100→200）
        /// - Shake：震动效果（位置震动）
        /// - Text：打字机效果
        /// - TmpText：TMP文字渐变
        /// - Fill：填充进度（0→1）
        /// - Tiled：平铺比例变化（0.5→2）
        /// - Path：沿路径移动
        /// - To：整数渐变（0→100）
        /// 
        /// 注意事项：
        /// - 此方法不会删除或修改已存在的预设文件
        /// - 如果某个类型的预设文件已存在，保持原样不变
        /// - 建议在插件安装后、版本更新后调用一次
        /// - 可以安全地多次调用，不会产生副作用
        /// </remarks>
        public static void preset_JsonFile_Checker()
        {
            preset_JsonFile_Exist(XTweenTypes.透明度_Alpha);
            preset_JsonFile_Exist(XTweenTypes.原生动画_To);
            preset_JsonFile_Exist(XTweenTypes.路径_Path);
            preset_JsonFile_Exist(XTweenTypes.位置_Position);
            preset_JsonFile_Exist(XTweenTypes.旋转_Rotation);
            preset_JsonFile_Exist(XTweenTypes.缩放_Scale);
            preset_JsonFile_Exist(XTweenTypes.尺寸_Size);
            preset_JsonFile_Exist(XTweenTypes.震动_Shake);
            preset_JsonFile_Exist(XTweenTypes.颜色_Color);
            preset_JsonFile_Exist(XTweenTypes.填充_Fill);
            preset_JsonFile_Exist(XTweenTypes.平铺_Tiled);
            preset_JsonFile_Exist(XTweenTypes.文字_Text);
            preset_JsonFile_Exist(XTweenTypes.文字_TmpText);
        }
        /// <summary>
        /// 检查指定类型的预设文件是否存在
        /// </summary>
        /// <remarks>
        /// 该方法检查指定动画类型的预设文件是否存在于 Resources/Presets/ 目录下。
        /// 如果文件不存在，则自动创建包含默认预设的JSON文件。
        /// 
        /// 检查流程：
        /// 1. 通过 GetFileNameFromType(type) 获取文件名（如 "alpha"）
        /// 2. 构建Resources加载路径：$"Presets/xtween_presets_{fileName}"
        /// 3. 使用 Resources.Load<TextAsset> 尝试加载文本资源
        /// 4. 判断加载结果：
        ///    - 如果 presetAsset == null（文件不存在）：
        ///      a. 调用 preset_JsonFile_Create(fileName) 生成默认预设JSON字符串
        ///      b. 调用 preset_JsonFile_Save(type, jsonContent) 保存到文件
        ///      c. 输出创建日志（如果EnableDebugLogs为true）
        ///    - 如果文件已存在：
        ///      d. 输出存在日志（如果EnableDebugLogs为true）
        /// 
        /// 使用场景：
        /// - preset_JsonFile_Checker：批量检查所有类型时调用
        /// - 首次访问某个类型的预设时（如打开预设选择器）
        /// - 手动触发"恢复默认预设"功能时
        /// 
        /// 文件命名规范：
        /// - 文件名格式：xtween_presets_{fileName}.json
        /// - fileName 取自动画类型的第二部分（如 "透明度_Alpha" → "alpha"）
        /// - 所有文件名统一使用小写，确保跨平台兼容性
        /// 
        /// 默认预设的生成：
        /// - 调用 preset_JsonFile_Create 生成JSON内容
        /// - 该方法会根据类型创建包含示例预设的完整容器JSON
        /// - 生成的JSON格式美观，便于阅读和手动编辑
        /// 
        /// 注意事项：
        /// - 此方法只会创建文件，不会覆盖已存在的文件
        /// - 使用 Resources.Load 而不是 File.Exists，因为Resources系统会自动处理路径
        /// - 文件创建后会调用 AssetDatabase.Refresh() 刷新编辑器
        /// - 如果文件已存在，不会进行任何修改
        /// </remarks>
        /// <param name="type">要检查的动画类型（如 XTweenTypes.透明度_Alpha）</param>
        public static void preset_JsonFile_Exist(XTweenTypes type)
        {
            // 从枚举名称中提取类型名称（移除前缀）
            string fileName = GetFileNameFromType(type);

            // 转换为小写文件名格式（Resources.Load 不需要文件扩展名）
            string resourcePath = $"Presets/xtween_presets_{fileName}";

            // 使用 Resources.Load 检查文本资源是否存在
            TextAsset presetAsset = Resources.Load<TextAsset>(resourcePath);

            if (presetAsset == null)
            {
                string jsonContent = preset_JsonFile_Create(type);
                preset_JsonFile_Save(type, jsonContent); // 保存到文件

                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"预设资源不存在，已创建默认资源: {resourcePath}.json", XTweenGUIMsgState.设置);
            }
            else
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"预设资源已存在: {resourcePath}.json", XTweenGUIMsgState.警告);
            }
        }
        /// <summary>
        /// 生成指定类型的默认预设JSON字符串（返回的是容器的JSON）
        /// </summary>
        /// <remarks>
        /// 该方法创建包含默认预设数据的容器，并将整个容器序列化为格式化的JSON字符串。
        /// 这是生成默认预设的核心方法，为每个动画类型提供示例预设。
        /// 
        /// 创建流程：
        /// 1. 创建新的 XTweenPresetContainer 对象
        /// 2. 设置容器类型为传入的 typename（如 "alpha"）
        /// 3. 根据 typename 在 switch 中选择对应的预设类型分支
        /// 4. 在每个分支中，创建一个或多个默认预设对象并添加到容器的 Presets 列表
        /// 5. 使用 JsonUtility.ToJson(container, true) 序列化为格式化的JSON字符串
        /// 6. 返回JSON字符串
        /// 
        /// 各类型默认预设详情：
        /// 
        /// ██████ alpha（透明度）██████
        /// - 预设1：淡入效果
        ///   - 名称："淡入效果"
        ///   - 描述："从完全透明到完全不透明"
        ///   - 参数：Duration=1.5, EaseMode=InOutCubic, EndValue=1, FromValue=0
        /// 
        /// - 预设2：淡入效果2
        ///   - 名称："淡入效果2"
        ///   - 描述："从半透明到完全不透明"
        ///   - 参数：Duration=1.5, EndValue=1, FromValue=0.5
        /// 
        /// ██████ to（原生动画）██████
        /// - 预设1：整数渐变
        ///   - 名称："整数渐变"
        ///   - 描述："整数从0到100的渐变"
        ///   - 参数：Duration=2, Delay=0.2, ToType=Int, EndValue=100, FromValue=0
        /// 
        /// ██████ path（路径）██████
        /// - 预设1：沿路径移动
        ///   - 名称："沿路径移动"
        ///   - 描述："沿预设路径移动"
        ///   - 参数：Duration=3, LoopCount=1, PathName="Bezier曲线路径"
        /// 
        /// ██████ position（位置）██████
        /// - 预设1：水平移动
        ///   - 名称："水平移动"
        ///   - 描述："从左侧移动到右侧"
        ///   - 参数：Duration=1, EaseMode=OutQuad, EndValue_Vector2=(300,0), FromValue_Vector2=(-300,0)
        /// 
        /// ██████ rotation（旋转）██████
        /// - 预设1：360度旋转
        ///   - 名称："360度旋转"
        ///   - 描述："完整旋转一圈"
        ///   - 参数：Duration=2, LoopCount=-1, IsRelative=true, EndValue_Euler=(0,0,360)
        /// 
        /// ██████ scale（缩放）██████
        /// - 预设1：弹性缩放
        ///   - 名称："弹性缩放"
        ///   - 描述："从0到1的弹性效果"
        ///   - 参数：Duration=0.8, EaseMode=OutBack, EndValue=(1.2,1.2,1.2), FromValue=(0,0,0)
        /// 
        /// ██████ size（尺寸）██████
        /// - 预设1：尺寸扩大
        ///   - 名称："尺寸扩大"
        ///   - 描述："RectTransform尺寸从100x100到200x200"
        ///   - 参数：Duration=1.2, EaseMode=OutCubic, EndValue=(200,200), FromValue=(100,100)
        /// 
        /// ██████ shake（震动）██████
        /// - 预设1：震动效果
        ///   - 名称："震动效果"
        ///   - 描述："轻微的位置震动"
        ///   - 参数：Duration=0.5, ShakeType=Position, Strength=(10,10,0), Vibrato=10, Randomness=90
        /// 
        /// ██████ color（颜色）██████
        /// - 预设1：颜色渐变
        ///   - 名称："颜色渐变"
        ///   - 描述："从红色到蓝色的渐变"
        ///   - 参数：Duration=1.5, LoopCount=2, LoopType=Yoyo, EndValue=蓝色, FromValue=红色
        /// 
        /// ██████ fill（填充）██████
        /// - 预设1：填充进度
        ///   - 名称："填充进度"
        ///   - 描述："Image填充从0到1"
        ///   - 参数：Duration=1, EndValue=1, FromValue=0
        /// 
        /// ██████ tiled（平铺）██████
        /// - 预设1：平铺效果
        ///   - 名称："平铺效果"
        ///   - 描述："Image平铺比例变化"
        ///   - 参数：Duration=1.2, EaseMode=OutQuad, EndValue=2, FromValue=0.5
        /// 
        /// ██████ text（Text文本）██████
        /// - 预设1：打字机效果
        ///   - 名称："打字机效果"
        ///   - 描述："逐字显示文本"
        ///   - 参数：Duration=2.5, TextType=Content, EndValue="Hello, XTween!", IsExtendedString=true
        /// 
        /// ██████ tmptext（TMP文本）██████
        /// - 预设1：TMP文字渐变
        ///   - 名称："TMP文字渐变"
        ///   - 描述："TextMeshPro文字颜色渐变和尺寸变化"
        ///   - 参数：Duration=2, TmpTextType=Color, EndValue=绿色, FromValue=白色, EndValue_Float=36, FromValue_Float=24
        /// 
        /// 使用场景：
        /// - preset_JsonFile_Exist：文件不存在时调用此方法生成JSON内容
        /// - preset_JsonFile_RegenerateDefault：重新生成默认预设时调用
        /// - 编辑器工具中导出默认预设模板
        /// 
        /// JSON格式特点：
        /// - 使用 JsonUtility.ToJson(container, true) 生成带缩进的格式化JSON
        /// - 便于用户直接阅读和手动编辑
        /// - 包含完整的类型信息，便于反序列化时正确恢复对象类型
        /// 
        /// 注意事项：
        /// - 此方法只生成JSON内容，不会将文件保存到磁盘
        /// - 返回的JSON字符串可以直接写入文件或用于其他用途
        /// - 每个类型的默认预设都是精心设计的示例，展示该类型的主要功能
        /// - 可以根据需要扩展默认预设的数量和内容
        /// </remarks>
        /// <param name="typename">类型名称，如"alpha"（透明度）、"color"（颜色）、"position"（位置）等</param>
        /// <returns>包含默认预设的完整JSON字符串</returns>
        public static string preset_JsonFile_Create(XTweenTypes type)
        {
            // 创建预设容器
            XTweenPresetContainer container = new XTweenPresetContainer();
            container.Type = type.ToString();

            #region 默认预设
            // 根据类型创建对应的预设数据
            //CreateDefaultPresets(type, container);
            #endregion

            // 将容器序列化为格式化的JSON字符串
            return JsonUtility.ToJson(container, true);
        }

        /// <summary>
        /// 创建默认预设
        /// </summary>
        /// <param name="type"></param>
        /// <param name="container"></param>
        private static void CreateDefaultPresets(XTweenTypes type, XTweenPresetContainer container)
        {
            switch (type)
            {
                case XTweenTypes.透明度_Alpha:
                    container.Presets.Add(new XTweenPreset_Alpha
                    {
                        Name = "Image组件淡入效果",
                        Description = "从完全透明到完全不透明",
                        Duration = 1.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        AlphaType = XTweenTypes_Alphas.Image组件,
                        EndValue = 1f,
                        FromValue = 0f,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Alpha
                    {
                        Name = "CanvasGroup组件淡入效果",
                        Description = "从完全透明到完全不透明",
                        Duration = 1.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        AlphaType = XTweenTypes_Alphas.CanvasGroup组件,
                        EndValue = 1f,
                        FromValue = 0f,
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.原生动画_To:
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 INT",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.整数_Int,
                        EndValue_Int = 100,
                        FromValue_Int = 0,
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 FLOAT",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.浮点数_Float,
                        EndValue_Float = 56.25f,
                        FromValue_Float = 0,
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 STRING",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.字符串_String,
                        EndValue_String = "TargetString",
                        FromValue_String = "FromString",
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 COLOR",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.颜色_Color,
                        EndValue_Color = Color.red,
                        FromValue_Color = Color.green,
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 VECTOR2",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.二维向量_Vector2,
                        EndValue_Vector2 = new Vector2(15, 25),
                        FromValue_Vector2 = new Vector2(0, 10),
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 VECTOR3",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.三维向量_Vector3,
                        EndValue_Vector3 = new Vector3(100, 55, 16),
                        FromValue_Vector3 = new Vector3(0, 25, 35),
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    container.Presets.Add(new XTweenPreset_To
                    {
                        Name = "原生动画 VECTOR4",
                        Description = "预设又称为前提、先设和前设，指的是说话者在说出某个话语或句子时所做的假设，即说话者为保证句子或语段的合适性而必须满足的前提，它是由德国哲学家、现代逻辑奠基人弗雷格于1892年提出",
                        Duration = 2f,
                        Delay = 0.2f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        ToType = XTweenTypes_To.四维向量_Vector4,
                        EndValue_Vector4 = new Vector4(10, 55, 23, 75),
                        FromValue_Vector4 = new Vector4(0, 50, 88, 12),
                        IsExtendedString = false,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = false,
                        IsFavourite = true
                    });
                    break;

                case XTweenTypes.路径_Path:
                    container.Presets.Add(new XTweenPreset_Path
                    {
                        Name = "沿路径移动",
                        Description = "沿预设路径移动",
                        Duration = 3f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 1,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        PathName = "Bezier曲线路径"
                    });
                    break;

                case XTweenTypes.位置_Position:
                    container.Presets.Add(new XTweenPreset_Position
                    {
                        Name = "水平移动 锚点位置_AnchoredPosition",
                        Description = "从左侧移动到右侧",
                        Duration = 1f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.OutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        PositionType = XTweenTypes_Positions.锚点位置_AnchoredPosition,
                        EndValue_Vector2 = new Vector2(300f, 0f),
                        FromValue_Vector2 = new Vector2(-300f, 0f),
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Position
                    {
                        Name = "水平移动 锚点位置3D_AnchoredPosition3D",
                        Description = "从左侧移动到右侧",
                        Duration = 1f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.OutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        PositionType = XTweenTypes_Positions.锚点位置3D_AnchoredPosition3D,
                        EndValue_Vector3 = new Vector3(300f, 0f, 75f),
                        FromValue_Vector3 = new Vector3(-300f, 0f, 75f),
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.旋转_Rotation:
                    container.Presets.Add(new XTweenPreset_Rotation
                    {
                        Name = "360度旋转 欧拉角度_Euler 相对",
                        Description = "完整旋转一圈",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = -1,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = true,
                        IsAutoKill = false,
                        RotationType = XTweenTypes_Rotations.欧拉角度_Euler,
                        EndValue_Euler = new Vector3(0f, 0f, 360f),
                        FromValue_Euler = new Vector3(0f, 0f, 0f),
                        RotationSpace = XTweenRotationSpace.相对,
                        RotationMode = XTweenRotationMode.Normal,
                        RotateLerpMode = XTweenRotateLerpType.SlerpUnclamped,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Rotation
                    {
                        Name = "360度旋转 欧拉角度_Euler 绝对",
                        Description = "完整旋转一圈",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = -1,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = true,
                        IsAutoKill = false,
                        RotationType = XTweenTypes_Rotations.欧拉角度_Euler,
                        EndValue_Euler = new Vector3(0f, 0f, 360f),
                        FromValue_Euler = new Vector3(0f, 0f, 0f),
                        RotationSpace = XTweenRotationSpace.绝对,
                        RotationMode = XTweenRotationMode.Normal,
                        RotateLerpMode = XTweenRotateLerpType.SlerpUnclamped,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Rotation
                    {
                        Name = "360度旋转 欧拉角度_Euler 计算最近距离",
                        Description = "完整旋转一圈",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = -1,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = true,
                        IsAutoKill = false,
                        RotationType = XTweenTypes_Rotations.欧拉角度_Euler,
                        EndValue_Euler = new Vector3(0f, 0f, 360f),
                        FromValue_Euler = new Vector3(0f, 0f, 0f),
                        RotationSpace = XTweenRotationSpace.相对,
                        RotationMode = XTweenRotationMode.Shortest,
                        RotateLerpMode = XTweenRotateLerpType.SlerpUnclamped,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Rotation
                    {
                        Name = "360度旋转 四元数_Quaternion",
                        Description = "完整旋转一圈",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = -1,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = true,
                        IsAutoKill = false,
                        RotationType = XTweenTypes_Rotations.四元数_Quaternion,
                        EndValue_Quaternion = Quaternion.Euler(new Vector3(0f, 0f, 360f)),
                        FromValue_Quaternion = Quaternion.Euler(new Vector3(0f, 0f, 0f)),
                        RotationSpace = XTweenRotationSpace.相对,
                        RotationMode = XTweenRotationMode.Normal,
                        RotateLerpMode = XTweenRotateLerpType.SlerpUnclamped,
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.缩放_Scale:
                    container.Presets.Add(new XTweenPreset_Scale
                    {
                        Name = "弹性缩放",
                        Description = "从0到1的弹性效果",
                        Duration = 0.8f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.OutBack,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        EndValue = new Vector3(1.2f, 1.2f, 1.2f),
                        FromValue = new Vector3(0f, 0f, 0f),
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.尺寸_Size:
                    container.Presets.Add(new XTweenPreset_Size
                    {
                        Name = "尺寸扩大",
                        Description = "RectTransform尺寸从100x100到200x200",
                        Duration = 1.2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.OutCubic,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        EndValue = new Vector2(200f, 200f),
                        FromValue = new Vector2(100f, 100f),
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.震动_Shake:
                    container.Presets.Add(new XTweenPreset_Shake
                    {
                        Name = "震动效果 位置_Position",
                        Description = "轻微的位置震动",
                        Duration = 0.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = true,
                        ShakeType = XTweenTypes_Shakes.位置_Position,
                        Strength_Vector3 = new Vector3(10f, 10f, 0f),
                        Strength_Vector2 = new Vector2(10f, 10f),
                        Vibrato = 10f,
                        Randomness = 90f,
                        FadeShake = true
                    });
                    container.Presets.Add(new XTweenPreset_Shake
                    {
                        Name = "震动效果 缩放_Scale",
                        Description = "轻微的位置震动",
                        Duration = 0.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = true,
                        ShakeType = XTweenTypes_Shakes.缩放_Scale,
                        Strength_Vector3 = new Vector3(10f, 10f, 0f),
                        Strength_Vector2 = new Vector2(10f, 10f),
                        Vibrato = 10f,
                        Randomness = 90f,
                        FadeShake = true
                    });
                    container.Presets.Add(new XTweenPreset_Shake
                    {
                        Name = "震动效果 尺寸_Size",
                        Description = "轻微的位置震动",
                        Duration = 0.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = true,
                        ShakeType = XTweenTypes_Shakes.尺寸_Size,
                        Strength_Vector3 = new Vector3(10f, 10f, 0f),
                        Strength_Vector2 = new Vector2(10f, 10f),
                        Vibrato = 10f,
                        Randomness = 90f,
                        FadeShake = true
                    });
                    container.Presets.Add(new XTweenPreset_Shake
                    {
                        Name = "震动效果 旋转_Rotation",
                        Description = "轻微的位置震动",
                        Duration = 0.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = true,
                        ShakeType = XTweenTypes_Shakes.旋转_Rotation,
                        Strength_Vector3 = new Vector3(10f, 10f, 0f),
                        Strength_Vector2 = new Vector2(10f, 10f),
                        Vibrato = 10f,
                        Randomness = 90f,
                        FadeShake = true
                    });
                    break;

                case XTweenTypes.颜色_Color:
                    container.Presets.Add(new XTweenPreset_Color
                    {
                        Name = "颜色渐变",
                        Description = "从红色到蓝色的渐变",
                        Duration = 1.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutSine,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 2,
                        LoopDelay = 0.2f,
                        LoopType = XTween_LoopType.Yoyo,
                        IsRelative = false,
                        IsAutoKill = false,
                        EndValue = new Color(0f, 0f, 1f, 1f),
                        FromValue = new Color(1f, 0f, 0f, 1f),
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.填充_Fill:
                    container.Presets.Add(new XTweenPreset_Fill
                    {
                        Name = "填充进度",
                        Description = "Image填充从0到1",
                        Duration = 1f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        EndValue = 1f,
                        FromValue = 0f,
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.平铺_Tiled:
                    container.Presets.Add(new XTweenPreset_Tiled
                    {
                        Name = "平铺效果",
                        Description = "Image平铺比例变化",
                        Duration = 1.2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.OutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        EndValue = 2f,
                        FromValue = 0.5f,
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.文字_Text:
                    container.Presets.Add(new XTweenPreset_Text
                    {
                        Name = "打字机效果 文字内容_Content",
                        Description = "逐字显示文本",
                        Duration = 2.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TextType = XTweenTypes_Text.文字内容_Content,
                        EndValue_String = "Hello, XTween!",
                        FromValue_String = "",
                        IsExtendedString = true,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Text
                    {
                        Name = "打字机效果 文字行高_LineHeight",
                        Description = "逐字显示文本",
                        Duration = 2.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TextType = XTweenTypes_Text.文字行高_LineHeight,
                        EndValue_Float = 5,
                        FromValue_Float = 0,
                        IsExtendedString = true,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Text
                    {
                        Name = "打字机效果 文字尺寸_FontSize",
                        Description = "逐字显示文本",
                        Duration = 2.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TextType = XTweenTypes_Text.文字尺寸_FontSize,
                        EndValue_Int = 50,
                        FromValue_Int = 10,
                        IsExtendedString = true,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_Text
                    {
                        Name = "打字机效果 文字颜色_Color",
                        Description = "逐字显示文本",
                        Duration = 2.5f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.Linear,
                        UseCurve = false,
                        Curve = AnimationCurve.Linear(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TextType = XTweenTypes_Text.文字颜色_Color,
                        EndValue_Color = Color.red,
                        FromValue_Color = Color.white,
                        IsExtendedString = true,
                        TextCursor = "_",
                        CursorBlinkTime = 0.5f,
                        UseFromMode = true
                    });
                    break;

                case XTweenTypes.文字_TmpText:
                    container.Presets.Add(new XTweenPreset_TmpText
                    {
                        Name = "TMP文字 文字颜色_Color",
                        Description = "TextMeshPro文字颜色渐变和尺寸变化",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TmpTextType = XTweenTypes_TmpText.文字颜色_Color,
                        EndValue_Color = new Color(0f, 1f, 0f, 1f),
                        FromValue_Color = new Color(1f, 1f, 1f, 1f),
                        IsExtendedString = false,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_TmpText
                    {
                        Name = "TMP文字 文字行高_LineHeight",
                        Description = "TextMeshPro文字颜色渐变和尺寸变化",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TmpTextType = XTweenTypes_TmpText.文字行高_LineHeight,
                        EndValue_Float = 36f,
                        FromValue_Float = 0f,
                        IsExtendedString = false,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_TmpText
                    {
                        Name = "TMP文字 文字尺寸_FontSize",
                        Description = "TextMeshPro文字颜色渐变和尺寸变化",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TmpTextType = XTweenTypes_TmpText.文字尺寸_FontSize,
                        EndValue_Float = 36f,
                        FromValue_Float = 10f,
                        IsExtendedString = false,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_TmpText
                    {
                        Name = "TMP文字 文字内容_Content",
                        Description = "TextMeshPro文字颜色渐变和尺寸变化",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TmpTextType = XTweenTypes_TmpText.文字内容_Content,
                        EndValue_String = "TmpText Content",
                        FromValue_String = "",
                        IsExtendedString = false,
                        UseFromMode = true
                    });
                    container.Presets.Add(new XTweenPreset_TmpText
                    {
                        Name = "TMP文字 文字边距_Margin",
                        Description = "TextMeshPro文字颜色渐变和尺寸变化",
                        Duration = 2f,
                        Delay = 0f,
                        UseRandomDelay = false,
                        RandomDelay = new RandomDelay { Min = 0f, Max = 0f },
                        EaseMode = EaseMode.InOutQuad,
                        UseCurve = false,
                        Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f),
                        LoopCount = 0,
                        LoopDelay = 0f,
                        LoopType = XTween_LoopType.Restart,
                        IsRelative = false,
                        IsAutoKill = false,
                        TmpTextType = XTweenTypes_TmpText.文字边距_Margin,
                        EndValue_Vector4 = new Vector4(10, 10, 10, 10),
                        FromValue_Vector4 = new Vector4(0, 0, 0, 0),
                        IsExtendedString = false,
                        UseFromMode = true
                    });
                    break;
            }
        }

        /// <summary>
        /// 保存JSON字符串到预设文件
        /// </summary>
        /// <remarks>
        /// 该方法将JSON字符串写入到对应类型的预设文件中。
        /// 这是所有预设保存操作的最终落地方法，负责实际的磁盘写入。
        /// 
        /// 保存流程：
        /// 1. 通过 GetFileNameFromType(type) 获取文件名（如 "alpha"）
        /// 2. 调用 GetPresetFilePath(fileName) 获取完整的物理文件路径
        /// 3. 获取文件所在目录路径，如果目录不存在则创建
        /// 4. 使用 File.WriteAllText 将JSON字符串写入文件
        /// 5. 调用 AssetDatabase.Refresh() 刷新编辑器，使更改立即生效
        /// 6. 如果 EnableDebugLogs 为 true，输出保存成功的日志
        /// 
        /// 路径示例：
        /// - fileName = "alpha"
        /// - fullPath = "D:/MyProject/Assets/Resources/Presets/xtween_presets_alpha.json"
        /// - directory = "D:/MyProject/Assets/Resources/Presets"
        /// 
        /// 使用场景：
        /// - preset_Container_Save_Added：追加预设后保存
        /// - preset_Container_Save_Replace：替换预设后保存
        /// - preset_JsonFile_Exist：创建默认预设时保存
        /// - preset_JsonFile_RegenerateDefault：重新生成默认预设时保存
        /// - 编辑器工具中手动编辑预设后保存
        /// 
        /// 文件操作特点：
        /// - 如果文件已存在，会直接覆盖（使用 File.WriteAllText）
        /// - 如果目录不存在，会自动创建（Directory.CreateDirectory）
        /// - 写入后立即刷新 AssetDatabase，确保Unity编辑器识别到文件变化
        /// 
        /// 注意事项：
        /// - 此方法仅在UNITY_EDITOR模式下可用
        /// - 写入的文件是纯文本JSON格式，可以用任何文本编辑器打开
        /// - 文件编码为UTF-8（File.WriteAllText 默认编码）
        /// - 写入操作是同步的，大文件可能会有短暂阻塞
        /// - 频繁写入会触发多次 AssetDatabase.Refresh()，可能影响编辑器性能
        /// </remarks>
        /// <param name="type">动画类型，用于生成文件名</param>
        /// <param name="jsonContent">要保存的JSON字符串</param>
        private static void preset_JsonFile_Save(XTweenTypes type, string jsonContent)
        {
#if UNITY_EDITOR            
            string fileName = GetFileNameFromType(type);

            string fullPath = GetPresetFilePath(fileName);
            string directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(fullPath, jsonContent);
            UnityEditor.AssetDatabase.Refresh();

            if (EnableDebugLogs)
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"预设已保存: {fullPath}", XTweenGUIMsgState.确认);
#endif
        }
        /// <summary>
        /// 删除指定类型的预设文件
        /// </summary>
        /// <remarks>
        /// 该方法删除指定动画类型的整个预设文件。
        /// 这是一个危险操作，会永久删除该类型的所有预设。
        /// 
        /// 删除流程：
        /// 1. 通过 GetFileNameFromType(type) 获取文件名（如 "alpha"）
        /// 2. 调用 GetPresetFilePath(fileName) 获取完整的物理文件路径
        /// 3. 检查文件是否存在（File.Exists）
        /// 4. 如果文件存在：
        ///    a. 使用 File.Delete 删除文件
        ///    b. 调用 AssetDatabase.Refresh() 刷新编辑器
        ///    c. 如果 EnableDebugLogs 为 true，输出删除成功的日志
        /// 
        /// 使用场景：
        /// - 预设管理器中的"删除所有预设"功能
        /// - 重置插件时清理所有预设文件
        /// - 卸载插件前的清理工作
        /// - 编辑器工具中手动删除整个类型的预设
        /// 
        /// 危险程度分级：
        /// ⚠️⚠️⚠️ 高度危险 - 会永久删除整个类型的预设文件
        /// 
        /// 注意事项：
        /// - 此方法仅在UNITY_EDITOR模式下可用
        /// - 删除操作不可逆，文件不会进入回收站
        /// - 如果文件不存在，静默返回，不会报错
        /// - 删除后需要重新调用 preset_JsonFile_Checker 才能恢复默认预设
        /// - 建议在删除前询问用户确认
        /// </remarks>
        /// <param name="type">要删除的动画类型</param>
        public static void preset_JsonFile_Delete(XTweenTypes type)
        {
#if UNITY_EDITOR
            string fileName = GetFileNameFromType(type);

            string fullPath = GetPresetFilePath(fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                UnityEditor.AssetDatabase.Refresh();

                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"预设已删除: {fullPath}", XTweenGUIMsgState.确认);
            }
#endif
        }
        /// <summary>
        /// 获取所有预设文件的原始JSON内容
        /// </summary>
        /// <remarks>
        /// 该方法遍历 Resources/Presets/ 目录下的所有预设文件，
        /// 返回每个文件的完整JSON字符串内容，不进行解析。
        /// 
        /// 获取流程：
        /// 1. 使用 Resources.LoadAll<TextAsset>("Presets") 获取Presets目录下的所有文本资源
        /// 2. 遍历每个 TextAsset 资源：
        ///    a. 尝试获取 preset.text（原始JSON字符串）
        ///    b. 如果成功，添加到结果列表
        ///    c. 如果失败（极少发生），静默跳过
        /// 3. 返回所有JSON字符串的列表
        /// 
        /// 使用场景：
        /// - 预设备份功能：获取所有预设的原始数据用于导出
        /// - 预设迁移：在不同项目间转移预设
        /// - 批量处理：对预设进行全局搜索或替换
        /// - 调试分析：查看预设文件的原始格式
        /// - 版本控制：比较不同版本的预设文件差异
        /// 
        /// 与 preset_Container_GetAll 的区别：
        /// - 本方法：返回原始JSON字符串，适合导出、备份、搜索
        /// - Container方法：返回解析后的对象，适合编辑、应用、修改
        /// 
        /// 数据格式：
        /// 返回的每个字符串都是完整的JSON文件内容，例如：
        /// {
        ///     "Type": "alpha",
        ///     "Presets": [
        ///         { "Name": "淡入效果", ... }
        ///     ]
        /// }
        /// 
        /// 性能考虑：
        /// - 只读取文件内容，不进行JSON解析，性能较好
        /// - 适合处理大量预设文件
        /// - 返回的字符串可以直接用于文件写入或网络传输
        /// 
        /// 注意事项：
        /// - 返回的是原始JSON字符串，不是解析后的对象
        /// - 如果Presets目录下没有预设文件，返回空列表
        /// - 不会验证JSON的有效性，可能包含无效的JSON（极少发生）
        /// - 字符串内容与文件完全一致，包含所有缩进和格式
        /// </remarks>
        /// <returns>所有预设文件的原始JSON字符串列表</returns>
        public static List<string> preset_JsonData_GetAll()
        {
            var jsons = new List<string>();
            TextAsset[] presets = Resources.LoadAll<TextAsset>("Presets");

            foreach (var preset in presets)
            {
                try
                {
                    // 直接返回整个文件的JSON内容
                    jsons.Add(preset.text);
                }
                catch { }
            }
            return jsons;
        }
        /// <summary>
        /// 重新生成指定类型的默认预设
        /// </summary>
        /// <remarks>
        /// 该方法强制重新生成指定类型的默认预设文件，会覆盖已存在的任何自定义预设。
        /// 这是一个恢复操作，用于将预设文件重置为初始状态。
        /// 
        /// 重新生成流程：
        /// 1. 通过 GetFileNameFromType(type) 获取文件名（如 "alpha"）
        /// 2. 调用 preset_JsonFile_Create(fileName) 生成默认预设的JSON字符串
        /// 3. 调用 preset_JsonFile_Save(type, jsonContent) 保存到文件（覆盖写入）
        /// 4. 如果 EnableDebugLogs 为 true，输出重新生成成功的日志
        /// 
        /// 使用场景：
        /// - 预设管理器中的"恢复默认预设"按钮
        /// - 插件设置中的"重置所有预设"选项
        /// - 预设文件损坏后的修复操作
        /// - 用户想重新体验默认预设示例时
        /// - 版本升级后需要更新默认预设内容
        /// 
        /// 覆盖说明：
        /// ⚠️ 此方法会完全覆盖现有文件，所有自定义修改都将丢失！
        /// ⚠️ 调用前请确保用户确实想要恢复默认设置
        /// ⚠️ 建议在操作前询问用户确认
        /// 
        /// 与 preset_JsonFile_Create 的区别：
        /// - Create：只生成JSON字符串，不保存文件
        /// - RegenerateDefault：生成JSON并强制保存，覆盖现有文件
        /// 
        /// 与 preset_JsonFile_Checker 的区别：
        /// - Checker：只在文件不存在时创建，不会覆盖
        /// - RegenerateDefault：无论文件是否存在，都强制重新生成
        /// 
        /// 默认预设的内容：
        /// 重新生成后，文件将包含 preset_JsonFile_Create 中定义的默认预设，
        /// 所有自定义修改都将被清除。
        /// 
        /// 注意事项：
        /// - 此方法仅在UNITY_EDITOR模式下可用
        /// - 操作不可逆，自定义预设将永久丢失
        /// - 保存后会自动刷新 AssetDatabase
        /// - 建议在调用前备份当前预设（如果需要）
        /// </remarks>
        /// <param name="type">要重新生成的动画类型</param>
        public static void preset_JsonFile_RegenerateDefault(XTweenTypes type)
        {
#if UNITY_EDITOR
            string fileName = GetFileNameFromType(type);
            string jsonContent = preset_JsonFile_Create(type);
            preset_JsonFile_Save(type, jsonContent);

            if (EnableDebugLogs)
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"默认预设已重新生成: {fileName}", XTweenGUIMsgState.设置);
#endif
        }
        #endregion

        #region Container
        /// <summary>
        /// 保存预设数据到文件（追加模式）
        /// </summary>
        /// <remarks>
        /// 该方法将单个预设数据添加到指定类型的预设列表中。
        /// 如果文件不存在，则创建新文件；如果文件已存在，则追加到现有列表末尾。
        /// 
        /// 操作流程：
        /// 1. 尝试加载指定类型的现有容器（preset_Container_Load）
        /// 2. 判断容器是否存在：
        ///    - 如果不存在（首次保存）：创建新容器，设置 Type 为当前类型，Presets 列表包含新预设
        ///    - 如果存在：将新预设追加到现有容器的 Presets 列表末尾
        /// 3. 将更新后的容器序列化为格式化的JSON字符串（缩进格式，便于阅读）
        /// 4. 调用 preset_JsonFile_Save 将JSON字符串写入文件
        /// 
        /// 文件操作示例：
        /// 初始文件内容（xtween_presets_alpha.json）：
        /// {
        ///     "Type": "alpha",
        ///     "Presets": [
        ///         { "Name": "淡入效果1", ... }
        ///     ]
        /// }
        /// 
        /// 调用 preset_Container_Save_Added(Alpha, 新预设"淡入效果2") 后：
        /// {
        ///     "Type": "alpha",
        ///     "Presets": [
        ///         { "Name": "淡入效果1", ... },
        ///         { "Name": "淡入效果2", ... }  // 追加到末尾
        ///     ]
        /// }
        /// 
        /// 使用场景：
        /// - preset_Save_From_Controller：保存从控制器创建的预设
        /// - 编辑器UI中的"新建预设"按钮：创建新预设并追加到文件
        /// - 预设导入功能：将外部预设添加到现有文件
        /// - 批量创建预设时，多次调用此方法逐个添加
        /// 
        /// 优势：
        /// - 保留现有预设，不会覆盖或丢失已有数据
        /// - 适合渐进式添加预设的场景
        /// - 操作简单，无需关心现有预设的细节
        /// 
        /// 注意事项：
        /// - 此方法不会检查预设名称是否重复，调用方需要自行处理重名问题
        /// - 如果希望覆盖同名预设，需要先调用 preset_Delete_ByName 再调用此方法
        /// - 每次调用都会重新序列化整个容器并写入文件，开销与预设总数相关
        /// - 仅在UNITY_EDITOR模式下可用
        /// - 保存后会自动刷新 AssetDatabase，使编辑器立即识别文件变化
        /// </remarks>
        /// <param name="type">动画类型，用于确定要保存到哪个类型的预设文件（如 XTweenTypes.透明度_Alpha）</param>
        /// <param name="presetData">要保存的预设数据对象，必须是XTweenPresetBase的派生类实例（如 XTweenPreset_Alpha）</param>
        public static void preset_Container_Save_Added(XTweenTypes type, XTweenPresetBase presetData)
        {
#if UNITY_EDITOR
            if (presetData == null)
                return;

            // 先尝试加载现有容器
            var container = preset_Container_Load(type);

            if (container == null)
            {
                // 文件不存在，创建新容器
                container = new XTweenPresetContainer
                {
                    Type = type.ToString(),
                    Presets = new List<XTweenPresetBase> { presetData }
                };
            }
            else
            {
                // 文件存在，追加新预设
                container.Presets.Add(presetData);
            }

            string jsonContent = JsonUtility.ToJson(container, true);
            preset_JsonFile_Save(type, jsonContent);
#endif
        }
        /// <summary>
        /// 保存预设数据到文件（替换模式）
        /// </summary>
        /// <remarks>
        /// 该方法用全新的预设列表替换指定类型的整个预设文件内容。
        /// 这是一个"全量替换"操作，会完全覆盖文件中现有的所有预设。
        /// 
        /// 操作流程：
        /// 1. 创建新的容器对象，设置 Type 为当前类型
        /// 2. 将传入的 presetsData 列表直接赋值给容器的 Presets 属性
        /// 3. 将新容器序列化为格式化的JSON字符串（缩进格式，便于阅读）
        /// 4. 调用 preset_JsonFile_Save 将JSON字符串写入文件
        /// 
        /// 文件操作示例：
        /// 初始文件内容（xtween_presets_alpha.json）：
        /// {
        ///     "Type": "alpha",
        ///     "Presets": [
        ///         { "Name": "旧预设1", ... },
        ///         { "Name": "旧预设2", ... }
        ///     ]
        /// }
        /// 
        /// 新预设列表：
        /// [
        ///     { "Name": "新预设A", ... },
        ///     { "Name": "新预设B", ... },
        ///     { "Name": "新预设C", ... }
        /// ]
        /// 
        /// 调用 preset_Container_Save_Replace(Alpha, 新列表) 后：
        /// {
        ///     "Type": "alpha",
        ///     "Presets": [
        ///         { "Name": "新预设A", ... },  // 旧预设全部被替换
        ///         { "Name": "新预设B", ... },
        ///         { "Name": "新预设C", ... }
        ///     ]
        /// }
        /// 
        /// 使用场景：
        /// - preset_Delete_ByName：删除指定预设后，用剩余的列表替换整个文件
        /// - 预设管理器中的"全选删除"功能：删除多个预设后保存剩余列表
        /// - 预设导入时的"替换全部"选项：用导入的预设完全替换现有文件
        /// - 批量编辑后的一次性保存：在内存中修改整个列表，然后一次性写入
        /// - 预设排序功能：调整预设顺序后重新保存
        /// - 恢复默认预设：用默认预设列表替换当前文件
        /// 
        /// 与追加模式的区别：
        /// - 追加模式：保留现有预设，只添加新预设
        /// - 替换模式：完全覆盖，现有预设全部丢失
        /// 
        /// 性能优势：
        /// - 当需要同时删除多个预设时，比多次调用 preset_Delete_ByName 更高效
        /// - 只需要一次文件写入操作
        /// 
        /// 警告：
        /// ⚠️ 此方法会覆盖文件中所有现有预设，操作不可逆！
        /// ⚠️ 调用前请确保 presetsData 包含所有需要保留的预设
        /// ⚠️ 如果不小心丢失数据，只能通过备份或重新生成默认预设恢复
        /// 
        /// 注意事项：
        /// - 传入的 presetsData 列表会被完整保存，即使是空列表也会清空文件
        /// - 如果 presetsData 为 null，会导致序列化异常（调用方需确保不为null）
        /// - 此方法不检查列表内容（如名称重复），调用方需要自行验证数据有效性
        /// - 仅在UNITY_EDITOR模式下可用
        /// - 保存后会自动刷新 AssetDatabase
        /// </remarks>
        /// <param name="type">动画类型，用于确定要替换哪个类型的预设文件（如 XTweenTypes.透明度_Alpha）</param>
        /// <param name="presetsData">要保存的预设数据列表，将完全替换文件中现有的所有预设</param>
        public static void preset_Container_Save_Replace(XTweenTypes type, List<XTweenPresetBase> presetsData)
        {
#if UNITY_EDITOR
            if (presetsData == null || presetsData.Count <= 0)
                return;

            var container = new XTweenPresetContainer
            {
                Type = type.ToString(),
                Presets = presetsData
            };

            string jsonContent = JsonUtility.ToJson(container, true);
            preset_JsonFile_Save(type, jsonContent);
#endif
        }
        /// <summary>
        /// 将一个预设参数类替换到目标类型的容器中的预设
        /// </summary>
        /// <param name="type"></param>
        /// <param name="preset"></param>
        /// <returns>返回的是替换好预设的新的容器</returns>
        public static XTweenPresetContainer preset_Container_ReplacePreset(XTweenTypes type, XTweenPresetBase preset)
        {
            XTweenPresetContainer con = preset_Container_Load(type);
            for (int i = 0; i < con.Presets.Count; i++)
            {
                if (con.Presets[i].Name == preset.Name)
                {
                    con.Presets[i] = preset.Clone();
                    break;
                }
            }

            return con;
        }
        /// <summary>
        /// 通过动画类型加载预设容器
        /// </summary>
        /// <remarks>
        /// 该方法从Resources/Presets/目录下加载指定动画类型的预设文件，并将其解析为XTweenPresetContainer对象。
        /// 容器对象包含了该类型的所有预设数据，是访问预设的入口。
        /// 
        /// 加载流程：
        /// 1. 通过 GetFileNameFromType(type) 获取文件名（如 "alpha"）
        /// 2. 构建Resources加载路径：$"Presets/xtween_presets_{fileName}"
        /// 3. 使用 Resources.Load<TextAsset> 加载JSON文件
        /// 4. 如果文件存在，使用 JsonUtility.FromJson 解析为 XTweenPresetContainer 对象
        /// 5. 返回解析后的容器对象
        /// 
        /// 返回的容器结构：
        /// {
        ///     "Type": "alpha",           // 容器类型标识
        ///     "Presets": [                // 预设列表
        ///         { ... },                 // XTweenPreset_Alpha 对象1
        ///         { ... }                  // XTweenPreset_Alpha 对象2
        ///     ]
        /// }
        /// 
        /// 使用场景：
        /// - preset_Container_Save_Added：加载现有容器，然后追加新预设
        /// - preset_Container_GetAll：加载所有类型的容器（间接调用）
        /// - preset_Delete_ByName：加载容器，删除指定预设后重新保存
        /// - 需要直接操作容器（如批量导入/导出预设）时使用
        /// 
        /// 文件位置说明：
        /// - 物理路径：{项目根路径}/Resources/Presets/xtween_presets_{fileName}.json
        /// - Resources路径：Presets/xtween_presets_{fileName}（无需扩展名）
        /// 
        /// 错误处理：
        /// - 如果文件不存在，返回null并输出警告日志（如果EnableDebugLogs为true）
        /// - 如果JSON解析失败，返回null并输出错误日志（包含异常信息）
        /// 
        /// 性能考虑：
        /// - 每次调用都会从磁盘加载文件并解析JSON，适合偶尔操作
        /// - 如需频繁访问，建议缓存容器对象，并在文件变更时重新加载
        /// - 文件较小时（几十个预设）性能影响不大，文件较大时需注意
        /// </remarks>
        /// <param name="type">动画类型，用于确定要加载哪个类型的预设文件（如 XTweenTypes.透明度_Alpha）</param>
        /// <returns>
        /// 解析后的 XTweenPresetContainer 对象，包含该类型的所有预设数据。
        /// 如果文件不存在或解析失败，返回 null。
        /// </returns>
        public static XTweenPresetContainer preset_Container_Load(XTweenTypes type)
        {
            // 从枚举名称中提取类型名称（移除前缀）            
            string fileName = GetFileNameFromType(type);

            // 确保使用小写路径
            string resourcePath = $"Presets/xtween_presets_{fileName}";
            TextAsset presetAsset = Resources.Load<TextAsset>(resourcePath);

            if (presetAsset == null)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"无法加载预设: {resourcePath}.json", XTweenGUIMsgState.错误);
                return null;
            }

            try
            {
                return JsonUtility.FromJson<XTweenPresetContainer>(presetAsset.text);
            }
            catch (Exception e)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"解析预设JSON失败: {e.Message}", XTweenGUIMsgState.错误);
                return null;
            }
        }
        /// <summary>
        /// 通过文件名加载预设容器
        /// </summary>
        /// <remarks>
        /// 该方法是第一个重载的字符串版本，直接通过文件名加载预设容器。
        /// 适用于已经知道文件名，但不知道对应动画类型的场景。
        /// 
        /// 加载流程：
        /// 1. 直接使用传入的 fileName 构建Resources路径：$"Presets/xtween_presets_{fileName}"
        /// 2. 使用 Resources.Load<TextAsset> 加载JSON文件
        /// 3. 如果文件存在，使用 JsonUtility.FromJson 解析为 XTweenPresetContainer 对象
        /// 4. 返回解析后的容器对象
        /// 
        /// 使用场景：
        /// - preset_Container_GetAll：遍历所有预设文件时，通过文件名加载每个容器
        /// - 处理从旧版本导入的预设数据（文件名已知，但类型需要从容器中读取）
        /// - 通过配置文件指定的预设文件（如 "level1_presets"）
        /// - 编辑器工具中通过文件选择器加载特定预设文件
        /// 
        /// 与枚举版本的区别：
        /// - 枚举版本：从动画类型推导文件名，类型安全，编译时检查
        /// - 字符串版本：直接使用文件名，更灵活，但需要确保文件存在
        /// 
        /// 文件名规范：
        /// - 文件名应为小写（如 "alpha"、"color"）
        /// - 实际文件名为 "xtween_presets_{fileName}.json"
        /// - fileName 参数不应包含路径、前缀或扩展名
        /// 
        /// 错误处理：
        /// - 如果文件不存在，返回null并输出警告日志（如果EnableDebugLogs为true）
        /// - 如果JSON解析失败，返回null并输出错误日志（包含异常信息）
        /// 
        /// 注意事项：
        /// - 此方法不会验证文件名是否对应有效的动画类型
        /// - 返回的容器对象中的 Type 字段可能为任意字符串
        /// - 调用方需要根据实际情况处理容器类型
        /// </remarks>
        /// <param name="fileName">文件名（不含路径、前缀和扩展名，如 "alpha"）</param>
        /// <returns>
        /// 解析后的 XTweenPresetContainer 对象。
        /// 如果文件不存在或解析失败，返回 null。
        /// </returns>
        public static XTweenPresetContainer preset_Container_Load(string fileName)
        {
            string resourcePath = $"Presets/xtween_presets_{fileName}";
            TextAsset presetAsset = Resources.Load<TextAsset>(resourcePath);

            if (presetAsset == null)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"无法加载预设: {resourcePath}.json", XTweenGUIMsgState.错误);
                return null;
            }

            try
            {
                return JsonUtility.FromJson<XTweenPresetContainer>(presetAsset.text);
            }
            catch (Exception e)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"解析预设JSON失败: {e.Message}", XTweenGUIMsgState.错误);
                return null;
            }
        }
        /// <summary>
        /// 获取所有预设容器对象
        /// </summary>
        /// <remarks>
        /// 该方法遍历 Resources/Presets/ 目录下的所有预设文件，将每个文件解析为
        /// XTweenPresetContainer 对象，并返回所有有效容器的列表。
        /// 
        /// 获取流程：
        /// 1. 使用 Resources.LoadAll<TextAsset>("Presets") 获取 Presets 目录下的所有文本资源
        /// 2. 遍历每个 TextAsset 资源：
        ///    a. 尝试使用 JsonUtility.FromJson 解析为 XTweenPresetContainer
        ///    b. 如果解析成功且容器对象有效（container != null 且 Type 不为空）
        ///    c. 将容器添加到结果列表
        ///    d. 如果解析失败，静默跳过（不抛出异常）
        /// 3. 返回所有有效容器的列表
        /// 
        /// 使用场景：
        /// - preset_Get_All_Presets_Of_Type：获取所有预设后按类型筛选
        /// - preset_Get_All_Preset_Names：获取所有预设名称
        /// - preset_Check_NameExists：检查名称是否存在
        /// - preset_Apply_To_Controller_ByName：按名称查找预设
        /// - 预设管理器的主界面，显示所有类型的预设概览
        /// - 批量导出所有预设
        /// - 统计预设数量和类型分布
        /// 
        /// 返回的容器列表特点：
        /// - 每个容器对应一个JSON文件（如 xtween_presets_alpha.json）
        /// - 容器中包含该类型的所有预设
        /// - 列表顺序由 Resources.LoadAll 的返回顺序决定（通常按文件名排序）
        /// 
        /// 错误处理：
        /// - 对于解析失败的文件，静默跳过，不会中断整个操作
        /// - 不会输出错误日志（避免刷屏），因为可能是非预设文件或其他文本资源
        /// - 调用方可以通过返回的列表大小判断成功加载的容器数量
        /// 
        /// 性能考虑：
        /// - 每次调用都会加载并解析所有预设文件，开销较大
        /// - 如果预设文件数量多（如几十个）或文件大，建议缓存结果
        /// - 可以在编辑器初始化时调用一次并缓存，在文件变更时刷新
        /// 
        /// 注意事项：
        /// - 只返回有效的容器（Type 字段不为空），无效文件会被自动过滤
        /// - 如果 Presets 目录下没有预设文件，返回空列表
        /// - 修改返回的容器对象不会影响实际文件，需要调用保存方法才会持久化
        /// - 返回的列表是新建的 List<XTweenPresetContainer>，修改它不影响内部数据
        /// </remarks>
        /// <returns>
        /// 包含所有有效预设容器的列表。
        /// 每个容器对应一个预设文件，包含该类型的所有预设数据。
        /// 如果没有找到任何预设文件，返回空列表（Count = 0）。
        /// </returns>
        public static List<XTweenPresetContainer> preset_Container_GetAll()
        {
            var containers = new List<XTweenPresetContainer>();

            // 在 Resources/Presets/ 文件夹中查找所有预设文件
            TextAsset[] presets = Resources.LoadAll<TextAsset>("Presets");

            foreach (var preset in presets)
            {
                try
                {
                    var container = JsonUtility.FromJson<XTweenPresetContainer>(preset.text);
                    if (container != null && !string.IsNullOrEmpty(container.Type))
                    {
                        containers.Add(container);
                    }
                }
                catch { }
            }

            return containers;
        }
        /// <summary>
        /// 根据类型和名称删除预设项
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void preset_Container_DeletePreset(XTweenTypes type, string name)
        {
            // 删除匹配项预设
            XTweenPresetContainer container = preset_Container_Load(type);
            var preset = container.Presets.FirstOrDefault(p => p.Name == name);
            if (preset != null)
            {
                container.Presets.Remove(preset);
            }
            // 重新覆盖保存
            preset_Container_Save_Replace(type, container.Presets);
        }
        /// <summary>
        /// 导出所有预设到指定文件夹
        /// </summary>
        /// <remarks>
        /// 该方法将所有的预设文件导出到用户选择的文件夹中。
        /// 每个预设类型会保存为独立的JSON文件，文件名格式为：xtween_presets_{type}.json
        /// 
        /// 导出流程：
        /// 1. 获取所有预设的原始JSON数据
        /// 2. 遍历每个JSON字符串
        /// 3. 解析JSON以获取类型信息，用于生成文件名
        /// 4. 将每个JSON保存为独立的文件
        /// 5. 刷新AssetDatabase使文件在编辑器中可见
        /// 
        /// 使用场景：
        /// - 用户通过右键菜单"导出预设"功能
        /// - 预设备份
        /// - 在不同项目间迁移预设
        /// 
        /// 文件命名规则：
        /// - 文件名：xtween_presets_{type}.json
        /// - type 取自容器中的Type字段（如 "alpha"、"color"）
        /// - 例如：xtween_presets_alpha.json
        /// 
        /// 注意事项：
        /// - 如果目标文件夹已存在同名文件，会被覆盖
        /// - 此方法仅在UNITY_EDITOR模式下可用
        /// - 导出完成后会自动刷新AssetDatabase
        /// </remarks>
        /// <param name="path">目标文件夹路径，由EditorUtility.SaveFolderPanel返回</param>
        public static bool preset_ExportAllPresets(string path)
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(path))
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", "导出路径为空，操作取消", XTweenGUIMsgState.警告);
                return false;
            }

            // 确保目标文件夹存在
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // 获取所有预设的原始JSON数据
            List<string> jsonList = preset_JsonData_GetAll();

            if (jsonList == null || jsonList.Count == 0)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", "没有找到任何预设文件可导出", XTweenGUIMsgState.警告);
                return false;
            }

            int successCount = 0;
            int failCount = 0;

            foreach (string json in jsonList)
            {
                try
                {
                    // 解析JSON以获取类型信息
                    var container = JsonUtility.FromJson<XTweenPresetContainer>(json);

                    if (container == null || string.IsNullOrEmpty(container.Type))
                    {
                        failCount++;
                        continue;
                    }

                    // 从类型字符串中提取文件名（如 "透明度_Alpha" -> "alpha"）
                    string fileName = GetFileNameFromType(container.Type);

                    // 构建完整的文件路径
                    string filePath = Path.Combine(path, $"xtween_presets_{fileName}.json");

                    // 写入文件
                    File.WriteAllText(filePath, json);

                    successCount++;

                    if (EnableDebugLogs)
                        XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已导出预设文件: xtween_presets_{fileName}.json", XTweenGUIMsgState.确认);
                }
                catch (Exception e)
                {
                    failCount++;
                    if (EnableDebugLogs)
                        XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"导出预设失败: {e.Message}", XTweenGUIMsgState.错误);
                }
            }

            // 刷新AssetDatabase，使新文件在编辑器中可见
            AssetDatabase.Refresh();

            // 输出导出结果统计
            if (EnableDebugLogs)
            {
                string resultMsg = $"预设导出完成！成功：{successCount} 个，失败：{failCount} 个，保存路径：{path}";
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", resultMsg, failCount > 0 ? XTweenGUIMsgState.警告 : XTweenGUIMsgState.确认);
            }

            // 显示编辑器对话框通知用户
            if (successCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
#else
    // 非编辑器模式下，这个方法不应该被调用，但为了编译通过，返回false
    return false;
#endif
        }
        #endregion

        #region Preset
        /// <summary>
        /// 获取指定类型的所有预设（返回预设对象列表）
        /// </summary>
        /// <remarks>
        /// 该方法从所有预设文件中筛选出指定动画类型的预设，并以泛型列表的形式返回。
        /// 返回的是完整的预设对象，包含所有参数，可用于预览、编辑或应用。
        /// 
        /// 查找流程：
        /// 1. 调用 preset_Container_GetAll() 获取所有预设容器
        /// 2. 遍历每个容器，通过 container.Type 筛选出与目标类型匹配的容器
        /// 3. 遍历匹配容器的 Presets 列表，将每个预设对象转换为指定类型 T
        /// 4. 收集所有成功转换的预设对象到结果列表并返回
        /// 
        /// 使用场景：
        /// - preset_Apply_To_Controller_ByIndex：获取列表后通过索引访问
        /// - 编辑器UI中的预设浏览器，显示所有预设及其参数
        /// - 批量导出或备份所有预设
        /// - 预设对比工具，比较不同预设的参数差异
        /// - 运行时动态加载所有预设供用户选择
        /// 
        /// 返回对象包含的完整数据：
        /// 基础参数（所有类型共有）：
        /// - Name、Description：预设名称和描述
        /// - Duration、Delay：时间和延迟
        /// - EaseMode、Curve：缓动参数
        /// - LoopCount、LoopDelay、LoopType：循环参数
        /// - IsRelative、IsAutoKill：其他设置
        /// 
        /// 特定类型参数（以 Alpha 为例）：
        /// - EndValue：目标透明度
        /// - FromValue：起始透明度
        /// - UseFromMode：是否使用起始值
        /// 
        /// 性能考虑：
        /// - 每次调用都会重新解析所有预设文件，适合偶尔操作
        /// - 如需频繁访问，建议缓存结果并在预设文件变更时刷新
        /// - 返回的是预设对象的深拷贝（JSON反序列化产生的新对象），修改不影响源文件
        /// 
        /// 注意事项：
        /// - 类型参数 T 必须与预设的实际类型匹配，否则该预设会被跳过
        /// - 如果指定类型的预设文件不存在，或文件中没有预设，返回空列表
        /// - 返回的列表是新建的 List<T>，修改它不会影响实际的预设数据
        /// - 预设对象的字段都是可序列化的，可以直接用于Inspector显示
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase（如 XTweenPreset_Alpha）</typeparam>
        /// <param name="type">动画类型，指定要获取哪个类型的预设（如 XTweenTypes.透明度_Alpha）</param>
        /// <returns>
        /// 包含所有匹配预设对象的列表。
        /// 如果没有找到任何预设，返回空列表（Count = 0）。
        /// </returns>
        public static List<T> preset_Get_All_Presets_Of_Type<T>(XTweenTypes type) where T : XTweenPresetBase
        {
            var result = new List<T>();
            string targetType = type.ToString();

            var containers = preset_Container_GetAll();
            foreach (var container in containers)
            {
                if (container.Type.Equals(targetType, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var preset in container.Presets)
                    {
                        if (preset is T tPreset)
                        {
                            result.Add(tPreset);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 获取指定类型的所有预设名称
        /// </summary>
        /// <remarks>
        /// 该方法从所有预设文件中筛选出指定动画类型的预设名称，以字符串列表的形式返回。
        /// 相比 preset_Get_All_Presets_Of_Type，这个方法只返回名称，不加载完整预设数据，
        /// 因此性能更好，适合只需要名称列表的场景。
        /// 
        /// 查找流程：
        /// 1. 调用 preset_Container_GetAll() 获取所有预设容器
        /// 2. 遍历每个容器，通过 container.Type 筛选出与目标类型匹配的容器
        /// 3. 遍历匹配容器的 Presets 列表，收集每个预设的 Name 字段
        /// 4. 过滤掉名称为 null 或空字符串的预设，返回名称列表
        /// 
        /// 使用场景：
        /// - preset_Check_NameExists：检查名称是否存在（虽然已有专用方法）
        /// - 编辑器UI中的下拉选择框，让用户从所有预设名称中选择
        /// - 预设导入时的名称冲突检测
        /// - 自动生成不重复的预设名称（基础名称+数字后缀）
        /// - 预设搜索功能的索引构建
        /// - 显示预设列表的概览信息（只显示名称，不加载详情）
        /// 
        /// 与 preset_Get_All_Presets_Of_Type 的对比：
        /// - 本方法：只返回名称，轻量快速，适合列表显示
        /// - 另一个方法：返回完整预设对象，包含所有参数，适合编辑和应用
        /// 
        /// 性能优势：
        /// - 只需要读取 Name 字段，不需要完整反序列化所有数据
        /// - 内存占用小，适合构建大型列表
        /// - 如果预设文件很大（包含复杂曲线数据），性能差异更明显
        /// 
        /// 注意事项：
        /// - 返回的列表顺序由容器遍历顺序和文件中预设的顺序决定
        /// - 预设名称允许重复（虽然不建议），如果存在同名预设，列表中会有多个相同名称
        /// - 空名称的预设会被自动过滤，不会出现在返回列表中
        /// - 返回的 List<string> 是新建的，修改它不会影响实际预设数据
        /// </remarks>
        /// <param name="type">动画类型，指定要获取哪个类型的预设名称（如 XTweenTypes.透明度_Alpha）</param>
        /// <returns>
        /// 包含所有预设名称的字符串列表。
        /// 如果没有找到任何预设，返回空列表（Count = 0）。
        /// </returns>
        public static List<string> preset_Get_All_Preset_Names(XTweenTypes type)
        {
            var names = new List<string>();
            string targetType = type.ToString();

            var containers = preset_Container_GetAll();
            foreach (var container in containers)
            {
                if (container.Type.Equals(targetType, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var preset in container.Presets)
                    {
                        if (preset != null && !string.IsNullOrEmpty(preset.Name))
                        {
                            names.Add(preset.Name);
                        }
                    }
                }
            }
            return names;
        }
        #endregion

        #region Controller
        /// <summary>
        /// 复制特定类型的预设数据（私有方法）
        /// </summary>
        /// <remarks>
        /// 该方法是预设保存过程中的核心数据复制逻辑，负责将XTween_Controller中各个动画类型特有的参数
        /// 复制到对应的预设对象中。每个动画类型都有自己独特的参数集合，需要单独处理。
        /// 
        /// 数据映射关系：
        /// 
        /// ██████ 透明度 Alpha ██████
        /// - controller.EndValue_Float    → alphaPreset.EndValue
        /// - controller.FromValue_Float    → alphaPreset.FromValue
        /// - controller.IsFromMode         → alphaPreset.UseFromMode
        /// 
        /// ██████ 颜色 Color ██████
        /// - controller.EndValue_Color     → colorPreset.EndValue
        /// - controller.FromValue_Color     → colorPreset.FromValue
        /// - controller.IsFromMode         → colorPreset.UseFromMode
        /// 
        /// ██████ 位置 Position ██████
        /// - controller.TweenTypes_Positions       → posPreset.PositionType
        /// - controller.EndValue_Vector2            → posPreset.EndValue_Vector2
        /// - controller.EndValue_Vector3            → posPreset.EndValue_Vector3
        /// - controller.FromValue_Vector2           → posPreset.FromValue_Vector2
        /// - controller.FromValue_Vector3           → posPreset.FromValue_Vector3
        /// - controller.IsFromMode                  → posPreset.UseFromMode
        /// 
        /// ██████ 旋转 Rotation ██████
        /// - controller.TweenTypes_Rotations        → rotPreset.RotationType
        /// - controller.EndValue_Vector3             → rotPreset.EndValue_Euler
        /// - controller.EndValue_Quaternion          → rotPreset.EndValue_Quaternion
        /// - controller.FromValue_Vector3            → rotPreset.FromValue_Euler
        /// - controller.FromValue_Quaternion         → rotPreset.FromValue_Quaternion
        /// - controller.AnimateSpace                 → rotPreset.AnimateSpace
        /// - controller.RotationMode                  → rotPreset.RotationMode
        /// - controller.RotateMode                    → rotPreset.RotateMode
        /// - controller.IsFromMode                    → rotPreset.UseFromMode
        /// 
        /// ██████ 缩放 Scale ██████
        /// - controller.EndValue_Vector3     → scalePreset.EndValue
        /// - controller.FromValue_Vector3     → scalePreset.FromValue
        /// - controller.IsFromMode            → scalePreset.UseFromMode
        /// 
        /// ██████ 尺寸 Size ██████
        /// - controller.EndValue_Vector2      → sizePreset.EndValue
        /// - controller.FromValue_Vector2      → sizePreset.FromValue
        /// - controller.IsFromMode             → sizePreset.UseFromMode
        /// 
        /// ██████ 震动 Shake ██████
        /// - controller.TweenTypes_Shakes     → shakePreset.ShakeType
        /// - controller.EndValue_Vector3       → shakePreset.Strength_Vector3
        /// - controller.EndValue_Vector2       → shakePreset.Strength_Vector2
        /// - controller.Vibrato                 → shakePreset.Vibrato
        /// - controller.Randomness              → shakePreset.Randomness
        /// - controller.FadeShake               → shakePreset.FadeShake
        /// 
        /// ██████ Text文本 ██████
        /// - controller.TweenTypes_Text        → textPreset.TextType
        /// - controller.EndValue_Int            → textPreset.EndValue_Int
        /// - controller.EndValue_Float          → textPreset.EndValue_Float
        /// - controller.EndValue_Color          → textPreset.EndValue_Color
        /// - controller.EndValue_String         → textPreset.EndValue_String
        /// - controller.FromValue_Int           → textPreset.FromValue_Int
        /// - controller.FromValue_Float         → textPreset.FromValue_Float
        /// - controller.FromValue_Color         → textPreset.FromValue_Color
        /// - controller.FromValue_String        → textPreset.FromValue_String
        /// - controller.IsExtendedString        → textPreset.IsExtendedString
        /// - controller.TextCursor              → textPreset.TextCursor
        /// - controller.CursorBlinkTime         → textPreset.CursorBlinkTime
        /// - controller.IsFromMode              → textPreset.UseFromMode
        /// 
        /// ██████ TMP文本 ██████
        /// - controller.TweenTypes_TmpText      → tmpPreset.TmpTextType
        /// - controller.EndValue_Float           → tmpPreset.EndValue_Float
        /// - controller.EndValue_Color           → tmpPreset.EndValue_Color
        /// - controller.EndValue_String          → tmpPreset.EndValue_String
        /// - controller.EndValue_Vector4         → tmpPreset.EndValue_Vector4
        /// - controller.FromValue_Float          → tmpPreset.FromValue_Float
        /// - controller.FromValue_Color          → tmpPreset.FromValue_Color
        /// - controller.FromValue_String         → tmpPreset.FromValue_String
        /// - controller.FromValue_Vector4        → tmpPreset.FromValue_Vector4
        /// - controller.IsExtendedString         → tmpPreset.IsExtendedString
        /// - controller.IsFromMode               → tmpPreset.UseFromMode
        /// 
        /// ██████ 填充 Fill ██████
        /// - controller.EndValue_Float     → fillPreset.EndValue
        /// - controller.FromValue_Float     → fillPreset.FromValue
        /// - controller.IsFromMode          → fillPreset.UseFromMode
        /// 
        /// ██████ 平铺 Tiled ██████
        /// - controller.EndValue_Float      → tiledPreset.EndValue
        /// - controller.FromValue_Float      → tiledPreset.FromValue
        /// - controller.IsFromMode           → tiledPreset.UseFromMode
        /// 
        /// ██████ 路径 Path ██████
        /// - controller.Target_PathTool?.name → pathPreset.PathName
        /// 
        /// ██████ 原生动画 To ██████
        /// - controller.TweenTypes_To        → toPreset.ToType
        /// - controller.EndValue_Int          → toPreset.EndValue_Int
        /// - controller.EndValue_Float        → toPreset.EndValue_Float
        /// - controller.EndValue_String       → toPreset.EndValue_String
        /// - controller.EndValue_Vector2      → toPreset.EndValue_Vector2
        /// - controller.EndValue_Vector3      → toPreset.EndValue_Vector3
        /// - controller.EndValue_Vector4      → toPreset.EndValue_Vector4
        /// - controller.EndValue_Color        → toPreset.EndValue_Color
        /// - controller.FromValue_Int         → toPreset.FromValue_Int
        /// - controller.FromValue_Float       → toPreset.FromValue_Float
        /// - controller.FromValue_String      → toPreset.FromValue_String
        /// - controller.FromValue_Vector2     → toPreset.FromValue_Vector2
        /// - controller.FromValue_Vector3     → toPreset.FromValue_Vector3
        /// - controller.FromValue_Vector4     → toPreset.FromValue_Vector4
        /// - controller.FromValue_Color       → toPreset.FromValue_Color
        /// - controller.IsExtendedString      → toPreset.IsExtendedString
        /// - controller.TextCursor            → toPreset.TextCursor
        /// - controller.CursorBlinkTime       → toPreset.CursorBlinkTime
        /// - controller.IsFromMode            → toPreset.UseFromMode
        /// 
        /// 设计原则：
        /// - 类型安全：使用 is 关键字进行类型检查，避免强制转换异常
        /// - 单一职责：每个case只处理对应类型的数据复制
        /// - 可扩展性：添加新动画类型时只需在此方法中增加一个case
        /// - 调试友好：开启EnableDebugLogs时可查看详细的数据复制过程
        /// 
        /// 使用场景：
        /// - 在 preset_Save_From_Controller 泛型方法中调用，完成特定数据的填充
        /// 
        /// 注意事项：
        /// - preset参数使用 ref 关键字，确保对原始对象的修改生效
        /// - 如果控制器类型与预设类型不匹配（理论上不会发生），会安全地跳过
        /// - 默认分支处理不支持的动画类型，给出警告日志
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase</typeparam>
        /// <param name="controller">XTween控制器实例，数据源</param>
        /// <param name="preset">要填充数据的预设对象（引用传递，会被修改）</param>
        private static void CopySpecificPresetData<T>(XTween_Controller controller, ref T preset) where T : XTweenPresetBase
        {
            switch (controller.TweenTypes)
            {
                #region 透明度 Alpha
                case XTweenTypes.透明度_Alpha:
                    if (preset is XTweenPreset_Alpha alphaPreset)
                    {
                        alphaPreset.AlphaType = controller.TweenTypes_Alphas;
                        alphaPreset.EndValue = controller.EndValue_Float;
                        alphaPreset.FromValue = controller.FromValue_Float;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Alpha预设数据: EndValue={alphaPreset.EndValue}, FromValue={alphaPreset.FromValue}！", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 颜色 Color
                case XTweenTypes.颜色_Color:
                    if (preset is XTweenPreset_Color colorPreset)
                    {
                        colorPreset.EndValue = controller.EndValue_Color;
                        colorPreset.FromValue = controller.FromValue_Color;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Color预设数据: EndValue={colorPreset.EndValue}, FromValue={colorPreset.FromValue}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 位置 Position
                case XTweenTypes.位置_Position:
                    if (preset is XTweenPreset_Position posPreset)
                    {
                        posPreset.PositionType = controller.TweenTypes_Positions;
                        posPreset.EndValue_Vector2 = controller.EndValue_Vector2;
                        posPreset.EndValue_Vector3 = controller.EndValue_Vector3;
                        posPreset.FromValue_Vector2 = controller.FromValue_Vector2;
                        posPreset.FromValue_Vector3 = controller.FromValue_Vector3;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Position预设数据: Type={posPreset.PositionType}, End2D={posPreset.EndValue_Vector2}, End3D={posPreset.EndValue_Vector3}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 旋转 Rotation
                case XTweenTypes.旋转_Rotation:
                    if (preset is XTweenPreset_Rotation rotPreset)
                    {
                        rotPreset.RotationType = controller.TweenTypes_Rotations;
                        rotPreset.EndValue_Euler = controller.EndValue_Vector3;
                        rotPreset.EndValue_Quaternion = controller.EndValue_Quaternion;
                        rotPreset.FromValue_Euler = controller.FromValue_Vector3;
                        rotPreset.FromValue_Quaternion = controller.FromValue_Quaternion;
                        rotPreset.RotationSpace = controller.RotationSpace;
                        rotPreset.RotationMode = controller.RotationMode;
                        rotPreset.RotateLerpMode = controller.RotateLerpMode;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Rotation预设数据: Type={rotPreset.RotationType}, Euler={rotPreset.EndValue_Euler}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 缩放 Scale
                case XTweenTypes.缩放_Scale:
                    if (preset is XTweenPreset_Scale scalePreset)
                    {
                        scalePreset.EndValue = controller.EndValue_Vector3;
                        scalePreset.FromValue = controller.FromValue_Vector3;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Scale预设数据: EndValue={scalePreset.EndValue}, FromValue={scalePreset.FromValue}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 尺寸 Size
                case XTweenTypes.尺寸_Size:
                    if (preset is XTweenPreset_Size sizePreset)
                    {
                        sizePreset.EndValue = controller.EndValue_Vector2;
                        sizePreset.FromValue = controller.FromValue_Vector2;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Size预设数据: EndValue={sizePreset.EndValue}, FromValue={sizePreset.FromValue}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 震动 Shake
                case XTweenTypes.震动_Shake:
                    if (preset is XTweenPreset_Shake shakePreset)
                    {
                        shakePreset.ShakeType = controller.TweenTypes_Shakes;
                        shakePreset.Strength_Vector3 = controller.EndValue_Vector3;
                        shakePreset.Strength_Vector2 = controller.EndValue_Vector2;
                        shakePreset.Vibrato = controller.Vibrato;
                        shakePreset.Randomness = controller.Randomness;
                        shakePreset.FadeShake = controller.FadeShake;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Shake预设数据: Type={shakePreset.ShakeType}, Vibrato={shakePreset.Vibrato}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region Text 文本
                case XTweenTypes.文字_Text:
                    if (preset is XTweenPreset_Text textPreset)
                    {
                        textPreset.TextType = controller.TweenTypes_Text;
                        textPreset.EndValue_Int = controller.EndValue_Int;
                        textPreset.EndValue_Float = controller.EndValue_Float;
                        textPreset.EndValue_Color = controller.EndValue_Color;
                        textPreset.EndValue_String = controller.EndValue_String;
                        textPreset.FromValue_Int = controller.FromValue_Int;
                        textPreset.FromValue_Float = controller.FromValue_Float;
                        textPreset.FromValue_Color = controller.FromValue_Color;
                        textPreset.FromValue_String = controller.FromValue_String;
                        textPreset.IsExtendedString = controller.IsExtendedString;
                        textPreset.TextCursor = controller.TextCursor;
                        textPreset.CursorBlinkTime = controller.CursorBlinkTime;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Text预设数据: Type={textPreset.TextType}, String='{textPreset.EndValue_String}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region TMP Text 文本
                case XTweenTypes.文字_TmpText:
                    if (preset is XTweenPreset_TmpText tmpPreset)
                    {
                        tmpPreset.TmpTextType = controller.TweenTypes_TmpText;
                        tmpPreset.EndValue_Float = controller.EndValue_Float;
                        tmpPreset.EndValue_Color = controller.EndValue_Color;
                        tmpPreset.EndValue_String = controller.EndValue_String;
                        tmpPreset.EndValue_Vector4 = controller.EndValue_Vector4;
                        tmpPreset.FromValue_Float = controller.FromValue_Float;
                        tmpPreset.FromValue_Color = controller.FromValue_Color;
                        tmpPreset.FromValue_String = controller.FromValue_String;
                        tmpPreset.FromValue_Vector4 = controller.FromValue_Vector4;
                        tmpPreset.IsExtendedString = controller.IsExtendedString;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制TmpText预设数据: Type={tmpPreset.TmpTextType}, Color={tmpPreset.EndValue_Color}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 填充 Fill
                case XTweenTypes.填充_Fill:
                    if (preset is XTweenPreset_Fill fillPreset)
                    {
                        fillPreset.EndValue = controller.EndValue_Float;
                        fillPreset.FromValue = controller.FromValue_Float;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Fill预设数据: EndValue={fillPreset.EndValue}, FromValue={fillPreset.FromValue}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 平铺 Tiled
                case XTweenTypes.平铺_Tiled:
                    if (preset is XTweenPreset_Tiled tiledPreset)
                    {
                        tiledPreset.EndValue = controller.EndValue_Float;
                        tiledPreset.FromValue = controller.FromValue_Float;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Tiled预设数据: EndValue={tiledPreset.EndValue}, FromValue={tiledPreset.FromValue}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 路径 Path
                case XTweenTypes.路径_Path:
                    if (preset is XTweenPreset_Path pathPreset)
                    {
                        pathPreset.PathName = controller.Target_PathTool != null ? controller.Target_PathTool.name : "";

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制Path预设数据: PathName={pathPreset.PathName}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 原生动画 To
                case XTweenTypes.原生动画_To:
                    if (preset is XTweenPreset_To toPreset)
                    {
                        toPreset.ToType = controller.TweenTypes_To;
                        toPreset.EndValue_Int = controller.EndValue_Int;
                        toPreset.EndValue_Float = controller.EndValue_Float;
                        toPreset.EndValue_String = controller.EndValue_String;
                        toPreset.EndValue_Vector2 = controller.EndValue_Vector2;
                        toPreset.EndValue_Vector3 = controller.EndValue_Vector3;
                        toPreset.EndValue_Vector4 = controller.EndValue_Vector4;
                        toPreset.EndValue_Color = controller.EndValue_Color;
                        toPreset.FromValue_Int = controller.FromValue_Int;
                        toPreset.FromValue_Float = controller.FromValue_Float;
                        toPreset.FromValue_String = controller.FromValue_String;
                        toPreset.FromValue_Vector2 = controller.FromValue_Vector2;
                        toPreset.FromValue_Vector3 = controller.FromValue_Vector3;
                        toPreset.FromValue_Vector4 = controller.FromValue_Vector4;
                        toPreset.FromValue_Color = controller.FromValue_Color;
                        toPreset.IsExtendedString = controller.IsExtendedString;
                        toPreset.TextCursor = controller.TextCursor;
                        toPreset.CursorBlinkTime = controller.CursorBlinkTime;

                        if (EnableDebugLogs)
                            XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制To预设数据: Type={toPreset.ToType}", XTweenGUIMsgState.确认);
                    }
                    break;
                #endregion

                #region 无类型/默认
                default:
                    if (EnableDebugLogs)
                        XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"不支持的动画类型: {controller.TweenTypes}，无法复制特定数据", XTweenGUIMsgState.错误);
                    break;
                    #endregion
            }
        }
        /// <summary>
        /// 复制基类数据（所有预设共有的基础参数）
        /// </summary>
        /// <remarks>
        /// 该方法负责将XTween_Controller中的基础动画参数复制到预设对象中。
        /// 这些参数是所有动画类型都共有的，定义在XTweenPresetBase基类中。
        /// 
        /// 复制的参数列表：
        /// 
        /// ⏱️ 时间相关：
        /// - Duration    : 动画持续时间（秒）
        /// - Delay       : 动画开始前的延迟时间（秒）
        /// - UseRandomDelay : 是否使用随机延迟
        /// - RandomDelay : 随机延迟的配置（Min/Max）
        /// 
        /// 📈 缓动相关：
        /// - EaseMode    : 缓动模式（如 Linear、InOutCubic）
        /// - UseCurve    : 是否使用自定义曲线
        /// - Curve       : 自定义动画曲线
        /// 
        /// 🔄 循环相关：
        /// - LoopCount   : 循环次数（-1无限循环，0不循环，>0指定次数）
        /// - LoopDelay   : 每次循环之间的延迟
        /// - LoopType    : 循环方式（Restart/Rewind/Yoyo）
        /// 
        /// ⚙️ 其他设置：
        /// - IsRelative  : 是否使用相对值动画
        /// - IsAutoKill  : 动画完成后是否自动销毁
        /// 
        /// 使用场景：
        /// - 在 preset_Save_From_Controller 泛型方法中，先调用此方法复制基础数据
        /// - 然后再调用 CopySpecificPresetData 复制特定类型数据
        /// 
        /// 设计理念：
        /// - 分离基础数据和特定数据，符合单一职责原则
        /// - 避免在每个类型特定的case中重复复制基础参数
        /// - 便于后续扩展新的基础参数时只需修改一处
        /// 
        /// 注意事项：
        /// - 此方法不检查参数有效性（假设调用方已确保controller不为null）
        /// - 复制的都是值类型或不可变类型，直接赋值即可
        /// - 开启EnableDebugLogs时会输出复制的关键参数值
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase</typeparam>
        /// <param name="controller">XTween控制器实例，数据源</param>
        /// <param name="preset">要填充数据的预设对象</param>
        private static void CopyBaseData<T>(XTween_Controller controller, T preset) where T : XTweenPresetBase
        {
            preset.Duration = controller.Duration;
            preset.Delay = controller.Delay;
            preset.UseRandomDelay = controller.UseRandomDelay;
            preset.RandomDelay = controller.RandomDelay;
            preset.EaseMode = controller.EaseMode;
            preset.UseCurve = controller.UseCurve;
            preset.Curve = controller.Curve;
            preset.LoopCount = controller.LoopCount;
            preset.LoopDelay = controller.LoopDelay;
            preset.LoopType = controller.LoopType;
            preset.IsRelative = controller.IsRelative;
            preset.IsAutoKill = controller.IsAutoKill;
            preset.UseFromMode = controller.IsFromMode;

            if (EnableDebugLogs)
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已复制基类数据: Duration={preset.Duration}, LoopCount={preset.LoopCount}", XTweenGUIMsgState.确认);
        }
        /// <summary>
        /// 删除指定类型的指定名称的预设
        /// </summary>
        /// <remarks>
        /// 该方法用于从预设文件中删除指定名称的单个预设。
        /// 这是覆盖保存功能的核心支撑方法，先删除旧预设再保存新预设。
        /// 
        /// 操作流程：
        /// 1. 加载指定类型的预设容器（从JSON文件）
        /// 2. 在容器的Presets列表中查找并删除所有名称为 presetName 的预设
        /// 3. 如果至少删除一个预设，则重新保存修改后的容器到文件
        /// 4. 刷新AssetDatabase确保Unity编辑器识别到文件变化
        /// 
        /// 使用场景：
        /// - preset_Save_From_Controller 中 overrideExist = true 时，先调用此方法删除旧预设
        /// - 编辑器工具中手动删除单个预设时调用
        /// - 预设管理器中的"删除"功能
        /// 
        /// 注意事项：
        /// - 如果存在多个同名预设（理论上不应该发生），会全部删除
        /// - 如果指定类型的预设文件不存在，或文件中没有该名称的预设，返回false
        /// - 删除操作会触发文件写入和AssetDatabase刷新，开销较大
        /// - 此方法仅在UNITY_EDITOR模式下可用
        /// 
        /// 性能考虑：
        /// - 每次删除都会重新保存整个容器文件，不仅仅是删除一条记录
        /// - 如果频繁删除，建议考虑批量操作
        /// </remarks>
        /// <param name="type">动画类型，指定要从哪个类型的预设文件中删除</param>
        /// <param name="presetName">要删除的预设名称</param>
        /// <returns>
        /// true - 删除成功（至少删除了一个预设）
        /// false - 删除失败（文件不存在、没有匹配的预设、或其他错误）
        /// </returns>
        public static bool preset_Delete_ByName(XTweenTypes type, string presetName)
        {
#if UNITY_EDITOR
            var container = preset_Container_Load(type);
            if (container == null || container.Presets == null)
                return false;

            int removedCount = container.Presets.RemoveAll(p => p != null && p.Name == presetName);

            if (removedCount > 0)
            {
                // 重新保存容器
                preset_Container_Save_Replace(type, container.Presets);
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已删除预设: {presetName}", XTweenGUIMsgState.确认);

                return true;
            }
#endif
            return false;
        }
        #endregion

        #region Controller扩展
        /// <summary>
        /// 从XTween_Controller保存动画预设（自动推断类型）- 推荐使用的公共接口
        /// </summary>
        /// <remarks>
        /// 这是保存预设的标准入口方法，会自动处理以下逻辑：
        /// <list type="bullet">
        /// <item><description>如果未提供预设名称，自动生成基于时间戳的唯一名称</description></item>
        /// <item><description>检查是否存在同名预设，避免意外覆盖</description></item>
        /// <item><description>根据 overrideExist 参数决定是覆盖还是取消保存</description></item>
        /// <item><description>根据控制器的动画类型自动选择对应的预设数据类型</description></item>
        /// </list>
        /// 
        /// 使用示例：
        /// <code>
        /// // 保存预设，如果重名则取消
        /// bool success = XTween_PresetManager.preset_Save_From_Controller(
        ///     controller,                     // XTween_Controller实例
        ///     "我的淡入效果",                   // 预设名称
        ///     "从透明到不透明的淡入动画",         // 预设描述
        ///     false                           // 重名时不覆盖
        /// );
        /// 
        /// if (!success)
        /// {
        ///     Debug.Log("保存失败：预设名称已存在");
        ///     // 可以提示用户是否要覆盖
        /// }
        /// 
        /// // 保存预设，如果重名则覆盖
        /// success = XTween_PresetManager.preset_Save_From_Controller(
        ///     controller,
        ///     "我的淡入效果",
        ///     "从透明到不透明的淡入动画",
        ///     true  // 重名时覆盖
        /// );
        /// 
        /// // 自动生成预设名称（使用默认参数）
        /// success = XTween_PresetManager.preset_Save_From_Controller(
        ///     controller,
        ///     description: "自动生成的预设"
        /// );
        /// // 生成的名称格式如：Alpha_preset_20250225143022
        /// </code>
        /// </remarks>
        /// <param name="controller">
        /// XTween_Controller 实例，不能为null。
        /// 从这个控制器中提取动画参数来创建预设。
        /// </param>
        /// <param name="presetName">
        /// 预设名称，用于在预设列表中标识。
        /// 如果为空字符串，将自动生成"{动画类型}_preset_时间戳"格式的名称。
        /// 例如："Alpha_preset_20250225143022"
        /// </param>
        /// <param name="description">
        /// 预设描述，详细说明这个预设的用途和效果。
        /// 可用于工具提示、文档或在预设选择器中显示。
        /// </param>
        /// <param name="overrideExist">
        /// 重名时的处理方式：
        /// <list type="table">
        /// <item>
        /// <term>true</term>
        /// <description>覆盖模式：如果存在同名预设，先删除原预设再保存新预设</description>
        /// </item>
        /// <item>
        /// <term>false</term>
        /// <description>安全模式：如果存在同名预设，直接返回false，不执行保存操作</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>
        /// <para><c>true</c>: 预设保存成功</para>
        /// <para><c>false</c>: 保存失败，可能原因：</para>
        /// <list type="bullet">
        /// <item><description>controller 为 null</description></item>
        /// <item><description>存在同名预设且 overrideExist = false</description></item>
        /// <item><description>不支持的动画类型（如 XTweenTypes.无_None）</description></item>
        /// <item><description>文件写入失败（由底层方法抛出异常）</description></item>
        /// </list>
        /// </returns>
        public static bool preset_Save_From_Controller(this XTween_Controller controller, string presetName = "", string description = "")
        {
#if UNITY_EDITOR
            if (controller == null)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", "控制器不能为空！", XTweenGUIMsgState.警告);
                return false;
            }

            // 生成预设名称（如果为空）
            if (string.IsNullOrEmpty(presetName))
            {
                presetName = $"{controller.TweenTypes}_preset_{DateTime.Now:yyyyMMddHHmmss}";
            }

            // 能执行到此处说明同意覆盖或者并没有同名预设
            // 根据动画类型自动选择对应的预设类型
            switch (controller.TweenTypes)
            {
                case XTweenTypes.透明度_Alpha:
                    return preset_Save_From_Controller<XTweenPreset_Alpha>(controller, presetName, description);
                case XTweenTypes.颜色_Color:
                    return preset_Save_From_Controller<XTweenPreset_Color>(controller, presetName, description);
                case XTweenTypes.位置_Position:
                    return preset_Save_From_Controller<XTweenPreset_Position>(controller, presetName, description);
                case XTweenTypes.旋转_Rotation:
                    return preset_Save_From_Controller<XTweenPreset_Rotation>(controller, presetName, description);
                case XTweenTypes.缩放_Scale:
                    return preset_Save_From_Controller<XTweenPreset_Scale>(controller, presetName, description);
                case XTweenTypes.尺寸_Size:
                    return preset_Save_From_Controller<XTweenPreset_Size>(controller, presetName, description);
                case XTweenTypes.震动_Shake:
                    return preset_Save_From_Controller<XTweenPreset_Shake>(controller, presetName, description);
                case XTweenTypes.文字_Text:
                    return preset_Save_From_Controller<XTweenPreset_Text>(controller, presetName, description);
                case XTweenTypes.文字_TmpText:
                    return preset_Save_From_Controller<XTweenPreset_TmpText>(controller, presetName, description);
                case XTweenTypes.填充_Fill:
                    return preset_Save_From_Controller<XTweenPreset_Fill>(controller, presetName, description);
                case XTweenTypes.平铺_Tiled:
                    return preset_Save_From_Controller<XTweenPreset_Tiled>(controller, presetName, description);
                case XTweenTypes.路径_Path:
                    return preset_Save_From_Controller<XTweenPreset_Path>(controller, presetName, description);
                case XTweenTypes.原生动画_To:
                    return preset_Save_From_Controller<XTweenPreset_To>(controller, presetName, description);
                default:
                    if (EnableDebugLogs)
                        XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"不支持的动画类型: {controller.TweenTypes}！", XTweenGUIMsgState.警告);
                    return false;
            }
#else
    return false;
#endif
        }
        /// <summary>
        /// 从XTween_Controller保存预设（泛型实现）- 内部方法
        /// </summary>
        /// <remarks>
        /// 这是预设保存的底层实现，完成以下工作：
        /// <list type="bullet">
        /// <item><description>创建指定类型的预设对象（如 XTweenPreset_Alpha）</description></item>
        /// <item><description>从控制器复制基础参数（Duration、Delay、EaseMode等）</description></item>
        /// <item><description>从控制器复制特定类型参数（如 Alpha 的 EndValue）</description></item>
        /// <item><description>将预设对象保存到对应的JSON文件</description></item>
        /// </list>
        /// 
        /// 注意：此方法不处理重名检查，由调用方保证预设名称唯一。
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase</typeparam>
        /// <param name="controller">XTween_Controller实例，从中提取动画参数</param>
        /// <param name="presetName">预设名称（已确保唯一）</param>
        /// <param name="description">预设描述</param>
        /// <returns>保存成功返回true，失败返回false</returns>
        private static bool preset_Save_From_Controller<T>(this XTween_Controller controller, string presetName = "", string description = "") where T : XTweenPresetBase, new()
        {
#if UNITY_EDITOR
            // 创建预设对象
            T preset = new T();

            // 设置基本属性
            preset.Name = presetName;
            preset.Description = description;

            // 从控制器复制基础参数
            CopyBaseData<T>(controller, preset);

            // 根据不同类型复制特定参数（保持原有逻辑）
            CopySpecificPresetData(controller, ref preset);

            // 保存预设
            preset_Container_Save_Added(controller.TweenTypes, preset);

            if (EnableDebugLogs)
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已从控制器保存到预设: {preset.Name}！", XTweenGUIMsgState.确认);

            return true;
#else
    return false;
#endif
        }
        /// <summary>
        /// 将指定的预设数据应用到XTween_Controller
        /// </summary>
        /// <remarks>
        /// 该方法是预设加载的核心实现，负责将预设对象中的所有参数
        /// 反向应用到XTween_Controller实例中，实现动画配置的快速加载。
        /// 
        /// 应用流程：
        /// 1️⃣ 应用基础参数（所有预设共有）：
        ///    - 时间参数：Duration、Delay、RandomDelay等
        ///    - 缓动参数：EaseMode、UseCurve、Curve
        ///    - 循环参数：LoopCount、LoopDelay、LoopType
        ///    - 其他设置：IsRelative、IsAutoKill
        /// 
        /// 2️⃣ 根据预设类型应用特定参数：
        /// 
        /// ██████ 透明度 Alpha ██████
        /// - 设置控制器类型为 XTweenTypes.透明度_Alpha
        /// - 应用 EndValue_Float、FromValue_Float、IsFromMode
        /// 
        /// ██████ 颜色 Color ██████
        /// - 设置控制器类型为 XTweenTypes.颜色_Color
        /// - 应用 EndValue_Color、FromValue_Color、IsFromMode
        /// 
        /// ██████ 位置 Position ██████
        /// - 设置控制器类型为 XTweenTypes.位置_Position
        /// - 应用 PositionType、EndValue_Vector2/3、FromValue_Vector2/3、IsFromMode
        /// 
        /// ██████ 旋转 Rotation ██████
        /// - 设置控制器类型为 XTweenTypes.旋转_Rotation
        /// - 应用 RotationType、EndValue_Euler/Quaternion、FromValue_Euler/Quaternion
        /// - 应用 AnimateSpace、RotationMode、RotateMode、IsFromMode
        /// 
        /// ██████ 缩放 Scale ██████
        /// - 设置控制器类型为 XTweenTypes.缩放_Scale
        /// - 应用 EndValue_Vector3、FromValue_Vector3、IsFromMode
        /// 
        /// ██████ 尺寸 Size ██████
        /// - 设置控制器类型为 XTweenTypes.尺寸_Size
        /// - 应用 EndValue_Vector2、FromValue_Vector2、IsFromMode
        /// 
        /// ██████ 震动 Shake ██████
        /// - 设置控制器类型为 XTweenTypes.震动_Shake
        /// - 应用 ShakeType、Strength_Vector3/2、Vibrato、Randomness、FadeShake
        /// 
        /// ██████ Text文本 ██████
        /// - 设置控制器类型为 XTweenTypes.文字_Text
        /// - 应用 TextType、EndValue_Int/Float/Color/String
        /// - 应用 FromValue_Int/Float/Color/String
        /// - 应用 IsExtendedString、TextCursor、CursorBlinkTime、IsFromMode
        /// 
        /// ██████ TMP文本 ██████
        /// - 设置控制器类型为 XTweenTypes.文字_TmpText
        /// - 应用 TmpTextType、EndValue_Float/Color/String/Vector4
        /// - 应用 FromValue_Float/Color/String/Vector4
        /// - 应用 IsExtendedString、IsFromMode
        /// 
        /// ██████ 填充 Fill ██████
        /// - 设置控制器类型为 XTweenTypes.填充_Fill
        /// - 应用 EndValue_Float、FromValue_Float、IsFromMode
        /// 
        /// ██████ 平铺 Tiled ██████
        /// - 设置控制器类型为 XTweenTypes.平铺_Tiled
        /// - 应用 EndValue_Float、FromValue_Float、IsFromMode
        /// 
        /// ██████ 路径 Path ██████
        /// - 设置控制器类型为 XTweenTypes.路径_Path
        /// - 注意：路径名称需要额外通过PathTool查找，此处只设置类型
        /// 
        /// ██████ 原生动画 To ██████
        /// - 设置控制器类型为 XTweenTypes.原生动画_To
        /// - 应用 ToType 及所有 EndValue/FromValue 的各类型字段
        /// - 应用 IsExtendedString、TextCursor、CursorBlinkTime、IsFromMode
        /// 
        /// 使用场景：
        /// - preset_Apply_To_Controller_ByName：按名称查找预设后应用
        /// - preset_Apply_To_Controller_ByIndex：按索引查找预设后应用
        /// - 编辑器UI中点击"应用预设"按钮时调用
        /// - 运行时动态加载预设配置
        /// 
        /// 设计原则：
        /// - 与 CopySpecificPresetData 形成逆向操作（保存 vs 加载）
        /// - 完整的双向映射确保预设可以完整保存和恢复
        /// - 类型安全，使用 switch 语句处理每种预设类型
        /// 
        /// 注意事项：
        /// - controller 和 preset 都不能为null
        /// - 此方法会修改 controller 的所有相关字段
        /// - Path预设的路径名称需要额外处理，此处只设置类型
        /// - 开启EnableDebugLogs时可查看应用过程
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase</typeparam>
        /// <param name="controller">要应用预设的XTween_Controller实例（会被修改）</param>
        /// <param name="preset">包含动画参数的预设数据对象</param>
        public static void preset_Apply_To_Controller<T>(this XTween_Controller controller, T preset) where T : XTweenPresetBase
        {
            if (controller == null)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"控制器不能为空！", XTweenGUIMsgState.错误);
                return;
            }

            if (preset == null)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"预设数据不能为空！", XTweenGUIMsgState.错误);
                return;
            }

            // 应用基础参数
            controller.Duration = preset.Duration;
            controller.Delay = preset.Delay;
            controller.UseRandomDelay = preset.UseRandomDelay;
            controller.IsFromMode = preset.UseFromMode;
            controller.RandomDelay = preset.RandomDelay;
            controller.EaseMode = preset.EaseMode;
#if UNITY_EDITOR
            controller.EaseGraph = AssetDatabase.LoadAssetAtPath<Texture2D>($"{XTween_Dashboard.Get_path_XTween_GUIStyle_Path()}Icon/EaseCurveGraph/{preset.EaseMode.ToString()}.png");
#endif
            controller.UseCurve = preset.UseCurve;
            controller.Curve = preset.Curve;
            controller.LoopCount = preset.LoopCount;
            controller.LoopDelay = preset.LoopDelay;
            controller.LoopType = preset.LoopType;
            controller.IsRelative = preset.IsRelative;
            controller.IsAutoKill = preset.IsAutoKill;

            // 根据预设类型应用特定参数
            switch (preset)
            {
                case XTweenPreset_Alpha alphaPreset:
                    controller.TweenTypes = XTweenTypes.透明度_Alpha;
                    controller.TweenTypes_Alphas = alphaPreset.AlphaType;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_Alphas = controller.TweenTypes_Alphas.ToString();
                    controller.EndValue_Float = alphaPreset.EndValue;
                    controller.FromValue_Float = alphaPreset.FromValue;
                    break;

                case XTweenPreset_Color colorPreset:
                    controller.TweenTypes = XTweenTypes.颜色_Color;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.EndValue_Color = colorPreset.EndValue;
                    controller.FromValue_Color = colorPreset.FromValue;
                    break;

                case XTweenPreset_Position posPreset:
                    controller.TweenTypes = XTweenTypes.位置_Position;
                    controller.TweenTypes_Positions = posPreset.PositionType;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_Positions = controller.TweenTypes_Positions.ToString();
                    controller.EndValue_Vector2 = posPreset.EndValue_Vector2;
                    controller.EndValue_Vector3 = posPreset.EndValue_Vector3;
                    controller.FromValue_Vector2 = posPreset.FromValue_Vector2;
                    controller.FromValue_Vector3 = posPreset.FromValue_Vector3;
                    break;

                case XTweenPreset_Rotation rotPreset:
                    controller.TweenTypes = XTweenTypes.旋转_Rotation;
                    controller.TweenTypes_Rotations = rotPreset.RotationType;
                    controller.RotationSpace = rotPreset.RotationSpace;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_Rotations = controller.TweenTypes_Rotations.ToString();
                    controller.index_TweenTypes_Rotation_Space = controller.RotationSpace.ToString();
                    controller.EndValue_Vector3 = rotPreset.EndValue_Euler;
                    controller.EndValue_Quaternion = rotPreset.EndValue_Quaternion;
                    controller.FromValue_Vector3 = rotPreset.FromValue_Euler;
                    controller.FromValue_Quaternion = rotPreset.FromValue_Quaternion;
                    controller.RotationMode = rotPreset.RotationMode;
                    controller.RotateLerpMode = rotPreset.RotateLerpMode;
                    break;

                case XTweenPreset_Scale scalePreset:
                    controller.TweenTypes = XTweenTypes.缩放_Scale;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.EndValue_Vector3 = scalePreset.EndValue;
                    controller.FromValue_Vector3 = scalePreset.FromValue;
                    break;

                case XTweenPreset_Size sizePreset:
                    controller.TweenTypes = XTweenTypes.尺寸_Size;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.EndValue_Vector2 = sizePreset.EndValue;
                    controller.FromValue_Vector2 = sizePreset.FromValue;
                    break;

                case XTweenPreset_Shake shakePreset:
                    controller.TweenTypes = XTweenTypes.震动_Shake;
                    controller.TweenTypes_Shakes = shakePreset.ShakeType;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_Shakes = controller.TweenTypes_Shakes.ToString();
                    controller.EndValue_Vector3 = shakePreset.Strength_Vector3;
                    controller.EndValue_Vector2 = shakePreset.Strength_Vector2;
                    controller.Vibrato = shakePreset.Vibrato;
                    controller.Randomness = shakePreset.Randomness;
                    controller.FadeShake = shakePreset.FadeShake;
                    break;

                case XTweenPreset_Text textPreset:
                    controller.TweenTypes = XTweenTypes.文字_Text;
                    controller.TweenTypes_Text = textPreset.TextType;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_Text = controller.TweenTypes_Text.ToString();
                    controller.EndValue_Int = textPreset.EndValue_Int;
                    controller.EndValue_Float = textPreset.EndValue_Float;
                    controller.EndValue_Color = textPreset.EndValue_Color;
                    controller.EndValue_String = textPreset.EndValue_String;
                    controller.FromValue_Int = textPreset.FromValue_Int;
                    controller.FromValue_Float = textPreset.FromValue_Float;
                    controller.FromValue_Color = textPreset.FromValue_Color;
                    controller.FromValue_String = textPreset.FromValue_String;
                    controller.IsExtendedString = textPreset.IsExtendedString;
                    controller.TextCursor = textPreset.TextCursor;
                    controller.CursorBlinkTime = textPreset.CursorBlinkTime;
                    break;

                case XTweenPreset_TmpText tmpPreset:
                    controller.TweenTypes = XTweenTypes.文字_TmpText;
                    controller.TweenTypes_TmpText = tmpPreset.TmpTextType;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_TmpText = controller.TweenTypes_TmpText.ToString();
                    controller.EndValue_Float = tmpPreset.EndValue_Float;
                    controller.EndValue_Color = tmpPreset.EndValue_Color;
                    controller.EndValue_String = tmpPreset.EndValue_String;
                    controller.EndValue_Vector4 = tmpPreset.EndValue_Vector4;
                    controller.FromValue_Float = tmpPreset.FromValue_Float;
                    controller.FromValue_Color = tmpPreset.FromValue_Color;
                    controller.FromValue_String = tmpPreset.FromValue_String;
                    controller.FromValue_Vector4 = tmpPreset.FromValue_Vector4;
                    controller.IsExtendedString = tmpPreset.IsExtendedString;
                    break;

                case XTweenPreset_Fill fillPreset:
                    controller.TweenTypes = XTweenTypes.填充_Fill;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.EndValue_Float = fillPreset.EndValue;
                    controller.FromValue_Float = fillPreset.FromValue;
                    break;

                case XTweenPreset_Tiled tiledPreset:
                    controller.TweenTypes = XTweenTypes.平铺_Tiled;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.EndValue_Float = tiledPreset.EndValue;
                    controller.FromValue_Float = tiledPreset.FromValue;
                    break;

                case XTweenPreset_Path pathPreset:
                    controller.TweenTypes = XTweenTypes.路径_Path;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    // 路径名称需要通过PathTool查找，这里只设置类型
                    if (controller.Target_PathTool != null && !string.IsNullOrEmpty(pathPreset.PathName))
                    {
                        // 可以在这里添加通过名称查找PathTool的逻辑
                    }
                    break;

                case XTweenPreset_To toPreset:
                    controller.TweenTypes = XTweenTypes.原生动画_To;
                    controller.TweenTypes_To = toPreset.ToType;
                    controller.index_TweenTypes = controller.TweenTypes.ToString();
                    controller.index_TweenTypes_To = controller.TweenTypes_To.ToString();
                    controller.EndValue_Int = toPreset.EndValue_Int;
                    controller.EndValue_Float = toPreset.EndValue_Float;
                    controller.EndValue_String = toPreset.EndValue_String;
                    controller.EndValue_Vector2 = toPreset.EndValue_Vector2;
                    controller.EndValue_Vector3 = toPreset.EndValue_Vector3;
                    controller.EndValue_Vector4 = toPreset.EndValue_Vector4;
                    controller.EndValue_Color = toPreset.EndValue_Color;
                    controller.FromValue_Int = toPreset.FromValue_Int;
                    controller.FromValue_Float = toPreset.FromValue_Float;
                    controller.FromValue_String = toPreset.FromValue_String;
                    controller.FromValue_Vector2 = toPreset.FromValue_Vector2;
                    controller.FromValue_Vector3 = toPreset.FromValue_Vector3;
                    controller.FromValue_Vector4 = toPreset.FromValue_Vector4;
                    controller.FromValue_Color = toPreset.FromValue_Color;
                    controller.IsExtendedString = toPreset.IsExtendedString;
                    controller.TextCursor = toPreset.TextCursor;
                    controller.CursorBlinkTime = toPreset.CursorBlinkTime;
                    break;
            }

            if (EnableDebugLogs)
                XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"已应用预设 '{preset.Name}' 到控制器！", XTweenGUIMsgState.确认);
        }
        /// <summary>
        /// 通过预设名称从指定类型的预设文件中加载并应用到控制器
        /// </summary>
        /// <remarks>
        /// 该方法提供了按名称查找预设并应用到控制器的便捷方式。
        /// 它会遍历所有预设容器，查找匹配类型和名称的预设，然后应用。
        /// 
        /// 查找流程：
        /// 1. 获取所有预设容器（遍历 Resources/Presets/ 下的所有JSON文件）
        /// 2. 筛选出类型与指定 type 匹配的容器（通过 container.Type 比较）
        /// 3. 在该容器的 Presets 列表中查找 Name 等于 presetName 的预设
        /// 4. 如果找到，调用 preset_Apply_To_Controller 应用该预设
        /// 
        /// 使用场景：
        /// - 编辑器UI中的预设下拉选择器，用户选择预设名称后点击"应用"
        /// - 通过字符串配置动态加载预设（如从Excel、JSON配置读取预设名称）
        /// - 运行时根据玩家选择加载不同的动画效果
        /// - 预设浏览器中双击预设名称时应用
        /// 
        /// 优势：
        /// - 无需关心预设的内部索引，通过有意义的名称引用预设
        /// - 预设名称可以直观地表达动画效果（如"弹窗出现"、"按钮悬停"）
        /// - 即使预设文件内容变化（增加/删除预设），只要名称不变就能正确加载
        /// 
        /// 注意事项：
        /// - 类型参数 T 必须与预设的实际类型匹配（如查找Alpha预设用 XTweenPreset_Alpha）
        /// - 如果存在多个同名预设（理论上不应该发生），只会应用第一个找到的
        /// - 区分大小写（"淡入" 和 "淡入" 视为相同）
        /// - 性能：每次调用都会遍历所有预设文件，适合偶尔操作
        /// - 如需频繁查找，建议先获取容器再缓存预设列表
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase</typeparam>
        /// <param name="controller">要应用预设的XTween_Controller实例</param>
        /// <param name="type">动画类型，指定要从哪个类型的预设文件中查找</param>
        /// <param name="presetName">要应用的预设名称</param>
        /// <returns>
        /// true - 成功找到并应用预设
        /// false - 未找到匹配的预设
        /// </returns>
        public static T preset_Apply_To_Controller_ByName<T>(this XTween_Controller controller, XTweenTypes type, string presetName) where T : XTweenPresetBase
        {
            var containers = preset_Container_GetAll();
            foreach (var container in containers)
            {
                if (container.Type.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var preset in container.Presets)
                    {
                        if (preset is T && preset.Name == presetName)
                        {
                            preset_Apply_To_Controller(controller, preset);
                            return preset as T;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 通过索引从指定类型的预设文件中加载并应用到控制器
        /// </summary>
        /// <remarks>
        /// 该方法提供了按索引查找预设并应用到控制器的便捷方式。
        /// 它会先获取指定类型的所有预设列表，然后通过索引直接访问并应用。
        /// 
        /// 查找流程：
        /// 1. 调用 preset_Get_All_Presets_Of_Type<T>(type) 获取指定类型的所有预设列表
        /// 2. 检查索引是否在有效范围内（0 <= presetIndex < list.Count）
        /// 3. 如果在范围内，直接通过索引访问预设并调用 preset_Apply_To_Controller 应用
        /// 
        /// 使用场景：
        /// - 编辑器UI中的预设列表，用户选择列表项后点击"应用"
        /// - 顺序播放多个预设（通过递增索引）
        /// - 随机选择预设（生成随机索引）
        /// - 预设轮播展示功能
        /// 
        /// 优势：
        /// - 访问速度快（O(1)时间复杂度）
        /// - 适合UI列表绑定，索引与列表项一一对应
        /// - 可以轻松实现"上一个/下一个"预设的导航
        /// 
        /// 与按名称查找的对比：
        /// - 按名称：更稳定，不受预设顺序影响，但稍慢
        /// - 按索引：更快，适合UI交互，但受预设顺序影响
        /// 
        /// 注意事项：
        /// - 必须先通过 preset_Get_All_Presets_Of_Type 获取列表
        /// - 必须确保索引不越界，否则返回false
        /// - 预设列表的顺序由JSON文件中的存储顺序决定
        /// - 如果预设文件被修改（增删预设），索引可能会变化
        /// - 适合在UI列表等索引稳定的场景使用
        /// </remarks>
        /// <typeparam name="T">预设数据类型，必须继承自XTweenPresetBase</typeparam>
        /// <param name="controller">要应用预设的XTween_Controller实例</param>
        /// <param name="type">动画类型，指定要从哪个类型的预设文件中获取列表</param>
        /// <param name="presetIndex">要应用的预设索引（从0开始）</param>
        /// <returns>
        /// true - 成功找到并应用预设
        /// false - 索引超出范围
        /// </returns>
        public static bool preset_Apply_To_Controller_ByIndex<T>(this XTween_Controller controller, XTweenTypes type, int presetIndex) where T : XTweenPresetBase
        {
            var presets = preset_Get_All_Presets_Of_Type<T>(type);

            if (presetIndex < 0 || presetIndex >= presets.Count)
            {
                if (EnableDebugLogs)
                    XTween_Utilitys.DebugInfo("XTween预设管理器消息", $"预设索引超出范围: {presetIndex}, 可用数量: {presets.Count}！", XTweenGUIMsgState.警告);
                return false;
            }

            preset_Apply_To_Controller(controller, presets[presetIndex]);
            return true;
        }
        #endregion

        #region Get
        /// <summary>
        /// 获取预设文件的完整物理路径
        /// </summary>
        /// <remarks>
        /// 该方法根据文件名构建预设文件在项目中的完整存储路径。
        /// 路径格式：{项目根路径}/Resources/Presets/xtween_presets_{fileName}.json
        /// 
        /// 路径组成说明：
        /// - 项目根路径：通过 XTween_Dashboard.Get_XTween_Root_Path() 获取
        /// - Resources目录：Unity的资源文件夹，用于运行时加载
        /// - Presets子目录：专门存放预设文件的文件夹
        /// - 文件名前缀："xtween_presets_" 固定前缀
        /// - 文件名：由动画类型决定（如 "alpha"、"color"）
        /// - 扩展名：".json"
        /// 
        /// 使用场景：
        /// - preset_JsonFile_Save 方法：获取要写入的文件路径
        /// - preset_JsonFile_Delete 方法：获取要删除的文件路径
        /// - 编辑器工具中查看预设文件位置时使用
        /// 
        /// 注意事项：
        /// - 此方法返回的是物理文件路径（如 "D:/MyProject/Assets/Resources/Presets/xtween_presets_alpha.json"）
        /// - 不是Unity的资源加载路径（如 "Presets/xtween_presets_alpha"）
        /// - 仅在UNITY_EDITOR模式下使用，因为运行时不需要直接操作物理文件
        /// - 如果Presets目录不存在，调用方需要负责创建
        /// </remarks>
        /// <param name="fileName">文件名（不含路径和扩展名，如 "alpha"）</param>
        /// <returns>预设文件的完整物理路径字符串</returns>
        public static string GetPresetFilePath(string fileName)
        {
            string rootPath = XTween_Dashboard.Get_XTween_Root_Path();
            return Path.Combine(rootPath, "Resources", "Presets", $"xtween_presets_{fileName}.json");
        }
        /// <summary>
        /// 从动画类型枚举中提取文件名（用于JSON文件命名）
        /// </summary>
        /// <remarks>
        /// 该方法将XTweenTypes枚举值转换为对应的预设文件名。
        /// 文件名提取规则：取下划线后的第二部分并转换为小写。
        /// 
        /// 转换示例：
        /// - XTweenTypes.透明度_Alpha     -> "alpha"
        /// - XTweenTypes.颜色_Color       -> "color"
        /// - XTweenTypes.位置_Position    -> "position"
        /// - XTweenTypes.旋转_Rotation    -> "rotation"
        /// - XTweenTypes.缩放_Scale       -> "scale"
        /// - XTweenTypes.尺寸_Size        -> "size"
        /// - XTweenTypes.震动_Shake       -> "shake"
        /// - XTweenTypes.文字_Text        -> "text"
        /// - XTweenTypes.文字_TmpText     -> "tmptext"
        /// - XTweenTypes.填充_Fill        -> "fill"
        /// - XTweenTypes.平铺_Tiled       -> "tiled"
        /// - XTweenTypes.路径_Path        -> "path"
        /// - XTweenTypes.原生动画_To      -> "to"
        /// 
        /// 特殊处理：
        /// - 如果枚举名称中没有下划线，直接返回整个名称的小写形式
        /// - 如果枚举名称中有多个下划线，只取第一部分之后的部分（如 "文字_TmpText" -> "tmptext"）
        /// 
        /// 使用场景：
        /// - preset_JsonFile_Exist：构建Resources加载路径
        /// - preset_JsonFile_Save：构建文件名
        /// - preset_JsonFile_Delete：构建要删除的文件名
        /// - preset_Container_Load：构建加载路径
        /// - preset_Container_Save_Added/Replace：保存时获取类型标识
        /// - preset_Check_NameExists：比较容器类型
        /// 
        /// 注意事项：
        /// - 返回的文件名统一为小写，确保跨平台兼容性（Windows不区分大小写，但Linux/Mac区分）
        /// - 文件名用于构建 "xtween_presets_{fileName}.json" 格式的预设文件
        /// - 此方法返回的是文件名主体，不含前缀和扩展名
        /// </remarks>
        /// <param name="type">动画类型枚举，如 XTweenTypes.透明度_Alpha</param>
        /// <returns>提取后的文件名（小写，如 "alpha"）</returns>
        public static string GetFileNameFromType(XTweenTypes type)
        {
            string typeName = type.ToString();
            string[] parts = typeName.Split('_');
            string fileName = parts.Length > 1 ? parts[1] : typeName;
            return fileName.ToLower();  // 统一返回小写
        }
        /// <summary>
        /// 从类型名称字符串中提取文件名（用于JSON文件命名）
        /// </summary>
        /// <remarks>
        /// 该方法是第一个重载的字符串版本，用于处理从JSON反序列化得到的类型字符串。
        /// 提取规则与枚举版本相同：取下划线后的第二部分并转换为小写。
        /// 
        /// 转换示例：
        /// - "透明度_Alpha"     -> "alpha"
        /// - "颜色_Color"       -> "color"
        /// - "位置_Position"    -> "position"
        /// - "旋转_Rotation"    -> "rotation"
        /// 
        /// 使用场景：
        /// - preset_Container_GetAll：解析已加载的预设容器时，比较容器类型
        /// - preset_Check_NameExists：遍历容器时比较类型
        /// - preset_Apply_To_Controller_ByName：查找匹配的预设容器
        /// - 处理从旧版本JSON导入的预设数据
        /// 
        /// 与枚举版本的区别：
        /// - 枚举版本：从强类型枚举转换，更安全，编译时检查
        /// - 字符串版本：处理运行时数据，更灵活，但需要确保字符串格式正确
        /// 
        /// 注意事项：
        /// - 输入字符串必须符合 "前缀_类型" 的格式（如 "透明度_Alpha"）
        /// - 如果字符串格式不正确（没有下划线），直接返回整个字符串的小写形式
        /// - 返回的文件名统一为小写，确保一致性
        /// - 此方法主要用于内部数据比较，外部调用较少
        /// </remarks>
        /// <param name="typeName">类型名称字符串，如 "透明度_Alpha"</param>
        /// <returns>提取后的文件名（小写，如 "alpha"）</returns>
        public static string GetFileNameFromType(string typeName)
        {
            string[] parts = typeName.Split('_');
            string fileName = parts.Length > 1 ? parts[1] : typeName;
            return fileName.ToLower();  // 统一返回小写
        }
        #endregion

        #region Repeat
        /// <summary>
        /// 检查指定类型的预设中是否存在同名预设
        /// </summary>
        /// <remarks>
        /// 该方法用于在保存预设前进行重名检查，避免意外覆盖已有预设。
        /// 它会遍历所有预设容器，查找与指定类型匹配且名称相同的预设。
        /// 
        /// 使用场景：
        /// - 在 preset_Save_From_Controller 方法中，根据此方法的返回值决定是直接保存、覆盖还是取消
        /// - 在编辑器UI中，用于实时验证用户输入的预设名称是否可用
        /// - 在批量导入预设时，检查是否存在命名冲突
        /// 
        /// 注意事项：
        /// - 名称比较区分大小写（"淡入" 和 "淡入" 被视为相同）
        /// - 只检查与指定类型匹配的预设文件，不会跨类型检查（Alpha类型的预设不会和Color类型的预设比较）
        /// - 如果指定类型的预设文件不存在，或者文件中没有任何预设，则返回false
        /// </remarks>
        /// <param name="type">动画类型，用于确定要检查哪个类型的预设文件（如 XTweenTypes.透明度_Alpha）</param>
        /// <param name="presetName">要检查的预设名称，不能为null或空字符串</param>
        /// <returns>
        /// <c>true</c> - 存在同名预设
        /// <c>false</c> - 不存在同名预设，或者指定类型的预设文件不存在
        /// </returns>
        public static bool preset_Check_NameExists(XTweenTypes type, string presetName)
        {
            var containers = preset_Container_GetAll();
            string targetType = type.ToString();

            foreach (var container in containers)
            {
                if (container.Type.Equals(targetType, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var preset in container.Presets)
                    {
                        if (preset != null && preset.Name == presetName)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 获取指定类型的所有预设名称（返回HashSet用于快速查找）
        /// </summary>
        /// <remarks>
        /// 该方法将指定类型的所有预设名称以HashSet<string>的形式返回，
        /// 适用于需要频繁进行存在性检查的场景，比返回List的性能更好。
        /// 
        /// 使用场景：
        /// - 批量验证多个预设名称是否唯一
        /// - 在预设导入/导出时快速检查命名冲突
        /// - 生成不重复的预设名称（自动重命名功能）
        /// - 在编辑器UI中构建预设名称的自动完成下拉列表
        /// 
        /// 性能优势：
        /// HashSet的Contains方法的时间复杂度为O(1)，而List的Contains为O(n)。
        /// 当预设数量较多（如几十个）且需要多次检查时，使用HashSet能显著提升性能。
        /// 
        /// 注意事项：
        /// - 返回的HashSet是只读的，修改它不会影响实际的预设数据
        /// - 如果指定类型的预设文件不存在或没有预设，返回空HashSet（不是null）
        /// - 预设名称去重（同一个文件中不可能有同名预设，但不同文件也不会跨类型）
        /// </remarks>
        /// <param name="type">动画类型，用于确定要获取哪个类型的预设名称（如 XTweenTypes.透明度_Alpha）</param>
        /// <returns>
        /// 包含所有预设名称的HashSet集合。
        /// 如果没有找到任何预设，返回空的HashSet（Count = 0）。
        /// </returns>
        public static HashSet<string> preset_Get_All_Preset_Names_HashSet(XTweenTypes type)
        {
            var names = new HashSet<string>();
            var containers = preset_Container_GetAll();
            string targetType = type.ToString();

            foreach (var container in containers)
            {
                if (container.Type.Equals(targetType, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var preset in container.Presets)
                    {
                        if (preset != null && !string.IsNullOrEmpty(preset.Name))
                        {
                            names.Add(preset.Name);
                        }
                    }
                }
            }
            return names;
        }
        #endregion
    }
}