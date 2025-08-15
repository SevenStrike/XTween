# XTween 动画插件
### 概述
XTween 是一个高性能的 Unity 动画系统，提供了丰富的动画类型、缓动效果和灵活的控制方式。它采用对象池技术优化性能，支持编辑器预览，适用于 UI 和游戏对象的动画处理。 并且该插件是一个功能全面、性能优化的 Unity 动画解决方案，适用于各种动画需求，从简单的 UI 动效到复杂的游戏对象动画

|开源不易，您的支持是持续更新的动力，这个小工具倾注了我无数个深夜的调试与优化，它永远免费，但绝非无成本，如果您觉得这个工具<br>能为您节省时间、解决问题，甚至带来一丝愉悦，请考虑赞助一杯咖啡，让我知道：有人在乎这份付出，而这将成为我熬夜修复Bug、<br>添加新功能的最大动力。开源不是用爱发电，您的认可会让它走得更远|![](Docs/donate.jpg) |
|:-|-:|
|**欢迎加入技术研讨群，在这里可以和我以及大家一起探讨插件的优化以及相关的技术实现思路，同时在做项目时遇到的众多问题以及瓶颈<br>阻碍都可以互相探讨学习**|![](Docs/qqgroups.jpg) |
<br>

### 📦 扩展性
**可通过实现 XTween_Interface 接口创建自定义动画类型**<br><br>

### 📦 核心组件
------------
#### - ▶️**Tween_Controller** 
- ##### **动画控制器，挂载在 GameObject 上用于创建和控制动画**

    - 支持多种动画类型（位置、旋转、缩放、颜色等）
    - 可配置动画参数（持续时间、延迟、缓动模式等）
    - 提供按键控制（播放、倒带、终止等）

```c#
// 当前动画对象
public XTween_Interface CurrentTweener; 

// 动画持续时间
public float Duration = 1;

// 延迟时间
public float Delay = 0; 

// 缓动模式
public EaseMode EaseMode = EaseMode.InOutCubic; 

// 循环次数（-1无限循环）
public int LoopCount = 0; 
```
------------

#### - ▶️**XTween_Interface**
- ##### **动画接口，定义了所有动画类型必须实现的方法和属性**

```c#
// 播放动画
XTween_Interface Play(); 

// 暂停动画
void Pause(); 

// 恢复播放
void Resume(); 

// 倒带动画
void Rewind(bool andKill = false);
 
// 终止动画
void Kill(bool complete = false); 
```
------------

#### - ▶️**XTween_Pool** 
- ##### **动画对象池，用于管理和复用动画对象**
    - 预加载动画对象
    - 自动扩容机制
    - 统计信息跟踪

```c#
// 从对象池创建动画
var tween = XTween_Pool.CreateTween<XTween_Specialized_Float>();

// 回收动画
XTween_Pool.RecycleTween(tween);
```
------------

#### - ▶️**XTween_Manager** 
- ##### **动画管理器，单例模式，负责更新所有活跃动画**
    - 动画注册/注销
    - 每帧更新动画状态
    - 提供动画查找功能

```c#
// 杀死所有正在播放的动画                    
public void Kill_WithPlaying(bool complete = false)

// 杀死所有已暂停的动画                
public void Kill_WithPaused(bool complete = false)

// 杀死所有动画                
public void Kill_All(bool complete = false)

// 播放所有动画        
public void Play_All()

// 倒退所有动画        
public void Rewind_All
```
------------

#### - ▶️**XTween_EaseLibrary** 
- ##### **缓动函数库，提供多种缓动效果**
    - Linear（线性）
    - Sine（正弦）
    - Quad（二次）
    - Cubic（三次）
    - Elastic（弹性）
    - Bounce（弹跳）等

```c#
// 添加动画
public static void Append(XTween_Interface tween); 

// 播放动画
public static void Play(); 

// 倒带动画
public static void Rewind(); 

// 终止动画
public static void Kill(bool Clear, bool Rewind); 
```
------------

#### - ▶️**XTween_Previewer** 
- ##### **编辑器预览系统，允许在非运行模式下预览动画**
    - 预加载动画对象
    - 自动扩容机制
    - 统计信息跟踪

```c#
// 添加动画
public static void Append(XTween_Interface tween); 

// 播放动画
public static void Play(); 

// 倒带动画
public static void Rewind(); 

// 终止动画
public static void Kill(bool Clear, bool Rewind); 
```
