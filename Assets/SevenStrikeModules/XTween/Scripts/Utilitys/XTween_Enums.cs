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
    /// <summary>
    /// 步长更新模式
    /// </summary>
    public enum StepUpdateMode
    {
        /// <summary>
        /// 每帧（默认，保持向后兼容）
        /// </summary>
        EveryFrame,
        /// <summary>
        /// 时间间隔模式
        /// </summary>
        TimeInterval,
        /// <summary>
        /// 进度步长模式
        /// </summary>
        ProgressStep
    }
    /// <summary>
    /// 四元数平滑差值过渡方式
    /// </summary>
    public enum RotateLerpType
    {
        LerpUnclamped,
        SlerpUnclamped
    }
    /// <summary>
    /// 欧拉角旋转动画的方式
    /// </summary>
    public enum RotationMode
    {
        /// <summary>
        /// 默认模式，保持按照角度顺序旋转
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 最短路径旋转
        /// </summary>
        Shortest = 1,
        /// <summary>
        /// 完整旋转模式，允许360度完整旋转
        /// </summary>
        FullRotation = 2
    }
    /// <summary>
    /// 动画坐标空间
    /// </summary>
    public enum TweenSpace
    {
        /// <summary>
        /// 本地坐标空间
        /// </summary>
        相对 = 0,
        /// <summary>
        /// 世界坐标空间
        /// </summary>
        绝对 = 1,
    }
    /// <summary>
    /// 定义XTween框架中所有可用的补间动画类型
    /// 用于指定要动画化的属性或组件类型
    /// </summary>
    public enum XTweenTypes
    {
        /// <summary>
        /// 无动画效果
        /// </summary>
        无_None = 0,
        /// <summary>
        /// 通用值变化动画
        /// </summary>
        原生动画_To = 1,
        /// <summary>
        /// 路径变化动画
        /// </summary>
        路径_Path = 2,
        /// <summary>
        /// 位置变化动画
        /// </summary>
        位置_Position = 3,
        /// <summary>
        /// 旋转变化动画
        /// </summary>
        旋转_Rotation = 4,
        /// <summary>
        /// 缩放变化动画
        /// </summary>
        缩放_Scale = 5,
        /// <summary>
        /// 尺寸变化动画
        /// </summary>
        尺寸_Size = 6,
        /// <summary>
        /// 抖动效果动画
        /// </summary>
        震动_Shake = 7,
        /// <summary>
        /// 颜色变化动画
        /// </summary>
        颜色_Color = 8,
        /// <summary>
        /// 透明度变化动画
        /// </summary>
        透明度_Alpha = 9,
        /// <summary>
        /// 填充量变化动画
        /// </summary>
        填充_Fill = 10,
        /// <summary>
        /// 精灵平铺变化动画
        /// </summary>
        平铺_Tiled = 11,
        /// <summary>
        /// HUD文本属性动画
        /// </summary>
        文字_Text = 12,
        /// <summary>
        /// HUD TMP文本属性动画
        /// </summary>
        文字_TmpText = 13
    }
    /// <summary>
    /// 定义位置动画的具体类型
    /// 用于指定位置动画的坐标空间
    /// </summary>
    public enum XTweenTypes_Positions
    {
        /// <summary>
        /// 使用2D锚点位置动画
        /// </summary>
        锚点位置_AnchoredPosition = 0,
        /// <summary>
        /// 使用3D锚点位置动画
        /// </summary>
        锚点位置3D_AnchoredPosition3D = 1
    }
    /// <summary>
    /// 定义旋转动画的具体类型
    /// 用于指定旋转动画的表示方式
    /// </summary>
    public enum XTweenTypes_Rotations
    {
        /// <summary>
        /// 使用欧拉角表示旋转
        /// </summary>
        欧拉角度_Euler = 0,
        /// <summary>
        /// 使用四元数表示旋转
        /// </summary>
        四元数_Quaternion = 1
    }
    /// <summary>
    /// 定义透明度动画的具体类型
    /// 用于指定透明度动画的目标组件
    /// </summary>
    public enum XTweenTypes_Alphas
    {
        /// <summary>
        /// 作用于Image组件的透明度
        /// </summary>
        Image组件 = 0,
        /// <summary>
        /// 作用于CanvasGroup组件的透明度
        /// </summary>
        CanvasGroup组件 = 1
    }
    /// <summary>
    /// 定义抖动动画的具体类型
    /// 用于指定抖动效果的作用属性
    /// </summary>
    public enum XTweenTypes_Shakes
    {
        /// <summary>
        /// 位置抖动效果
        /// </summary>
        位置_Position = 0,
        /// <summary>
        /// 旋转抖动效果
        /// </summary>
        旋转_Rotation = 1,
        /// <summary>
        /// 缩放抖动效果
        /// </summary>
        缩放_Scale = 2,
        /// <summary>
        /// 尺寸抖动效果
        /// </summary>
        尺寸_Size = 3
    }
    /// <summary>
    /// 定义HUD文本组件可动画化的属性
    /// 用于控制标准文本元素的动画
    /// </summary>
    public enum XTweenTypes_Text
    {
        /// <summary>
        /// 字体尺寸变化
        /// </summary>
        文字尺寸_FontSize = 0,
        /// <summary>
        /// 行高变化
        /// </summary>
        文字行高_LineHeight = 1,
        /// <summary>
        /// 文本颜色变化
        /// </summary>
        文字颜色_Color = 2,
        /// <summary>
        /// 文本内容变化
        /// </summary>
        文字内容_Content = 3
    }
    /// <summary>
    /// 定义HUD TMP文本组件可动画化的属性
    /// 用于控制TextMeshPro文本元素的动画
    /// </summary>
    public enum XTweenTypes_TmpText
    {
        /// <summary>
        /// 字体尺寸变化
        /// </summary>
        文字尺寸_FontSize = 0,
        /// <summary>
        /// 行高变化
        /// </summary>
        文字行高_LineHeight = 1,
        /// <summary>
        /// 文本颜色变化
        /// </summary>
        文字颜色_Color = 2,
        /// <summary>
        /// 文本内容变化
        /// </summary>
        文字内容_Content = 3,
        /// <summary>
        /// 字符间距变化
        /// </summary>
        文字间距_Character = 4,
        /// <summary>
        /// 文本边距变化
        /// </summary>
        文字边距_Margin = 5
    }
    /// <summary>
    /// 定义To动画的属性
    /// </summary>
    public enum XTweenTypes_To
    {
        /// <summary>
        /// 整数_Int
        /// </summary>
        整数_Int = 0,
        /// <summary>
        /// 浮点数_Float
        /// </summary>
        浮点数_Float = 1,
        /// <summary>
        /// 字符串_String
        /// </summary>
        字符串_String = 2,
        /// <summary>
        /// 二维向量_Vector2
        /// </summary>
        二维向量_Vector2 = 3,
        /// <summary>
        /// 三维向量_Vector3
        /// </summary>
        三维向量_Vector3 = 4,
        /// <summary>
        /// 四维向量_Vector4
        /// </summary>
        四维向量_Vector4 = 5,
        /// <summary>
        /// 颜色_Color
        /// </summary>
        颜色_Color = 6
    }
    /// <summary>
    /// 定义路径的类型，用于指定路径的生成方式
    /// </summary>
    public enum XTween_PathType
    {
        /// <summary>
        /// 线性路径，表示路径由直线段组成
        /// </summary>
        线性,
        /// <summary>
        /// 自动贝塞尔路径，表示路径的贝塞尔控制点由系统自动计算
        /// </summary>
        自动贝塞尔,
        /// <summary>
        /// 手动贝塞尔路径，表示路径的贝塞尔控制点由用户手动设置
        /// </summary>
        手动贝塞尔,
        /// <summary>
        /// 平衡贝塞尔路径，表示路径的贝塞尔控制点在自动计算的基础上进行平衡调整
        /// </summary>
        平衡贝塞尔
    }
    /// <summary>
    /// 定义路径的类型，用于指定物体在路径上的运动方式
    /// </summary>
    public enum XTween_PathOrientation
    {
        /// <summary>
        /// 默认路径运动
        /// </summary>
        无,
        /// <summary>
        /// 在物体运动时沿着路径运动
        /// </summary>
        跟随路径,
        /// <summary>
        /// 在物体运动时看向一个物体位置
        /// </summary>
        注视目标物体,
        /// <summary>
        /// 在物体运动时看向一个位置
        /// </summary>
        注视目标位置
    }
    /// <summary>
    /// 定义路径的类型，用于指定物体在路径上的运动轴
    /// </summary>
    public enum XTween_PathOrientationVector
    {
        /// <summary>
        /// 按照X轴
        /// </summary>
        正向X轴,
        /// <summary>
        /// 按照Y轴
        /// </summary>
        正向Y轴,
        /// <summary>
        /// 按照Z轴
        /// </summary>
        正向Z轴,
        /// <summary>
        /// 按照-X轴
        /// </summary>
        反向X轴,
        /// <summary>
        /// 按照-Y轴
        /// </summary>
        反向Y轴,
        /// <summary>
        /// 按照-Z轴
        /// </summary>
        反向Z轴
    }
    /// <summary>
    /// 定义可视化路径的样式
    /// </summary>
    public enum XTween_LineStyle
    {
        /// <summary>
        /// 可视化路径 - 实心线
        /// </summary>
        实心线,
        /// <summary>
        /// 可视化路径 - 虚线
        /// </summary>
        虚线
    }
    /// <summary>
    /// 定义路径可视化对象的生成模式
    /// </summary>
    public enum XTween_PathMarksMode
    {
        /// <summary>
        /// 根据路径点位生成可视化对象
        /// </summary>
        根据路径点,
        /// <summary>
        /// 根据路径生成可视化对象
        /// </summary>
        根据路径线条
    }
    /// <summary>
    /// 颜色样式
    /// </summary>
    public enum GUIColor
    {
        /// <summary>
        /// 颜色值：232323
        /// </summary>
        深空灰 = 0,
        /// <summary>
        /// 颜色值：909090
        /// </summary>
        阴影灰 = 1,
        /// <summary>
        /// 颜色值：cecece
        /// </summary>
        亮白 = 2,
        /// <summary>
        /// 颜色值：a8cc3e
        /// </summary>
        柠檬绿 = 3,
        /// <summary>
        ///  颜色值：56b2f4
        /// </summary>
        工业蓝 = 4,
        /// <summary>
        /// 颜色值：ffc230
        /// </summary>
        警示黄 = 5,
        /// <summary>
        /// 颜色值：d977b7
        /// </summary>
        玫瑰粉 = 6,
        /// <summary>
        /// 颜色值：a86bcd
        /// </summary>
        神秘紫 = 7,
        /// <summary>
        /// 颜色值：f45656
        /// </summary>
        魅力红 = 8,
        /// <summary>
        /// 颜色值：72804b
        /// </summary>
        灰绿 = 9,
        /// <summary>
        /// 颜色值：ff8021
        /// </summary>
        亮橘红 = 10,
        /// <summary>
        /// 颜色值：747474
        /// </summary>
        枪灰 = 11,
        /// <summary>
        /// 颜色值：ffd86b
        /// </summary>
        亮金色 = 12,
        /// <summary>
        /// 颜色值：642a2a
        /// </summary>
        沉暗红 = 13,
        /// <summary>
        /// 颜色值：5d6a82
        /// </summary>
        烟灰蓝 = 14,
        /// <summary>
        /// 颜色值：359882
        /// </summary>
        健康绿 = 15,
        /// <summary>
        /// 颜色值：414141
        /// </summary>
        浅灰 = 16,
        无 = 17
    }
    /// <summary>
    /// 填充类型
    /// </summary>
    public enum GUIFilled
    {
        实体 = 0,
        边框 = 1,
        纯色边框 = 2,
        无 = 3,
        透明 = 4
    }
    /// <summary>
    /// 通知消息类型
    /// </summary>
    public enum GUIMsgState
    {
        通知 = 0,
        确认 = 1,
        警告 = 2,
        错误 = 3,
        设置 = 4,
        未开启消息模块功能 = 5
    }

}