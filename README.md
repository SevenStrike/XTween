## XTween 动画插件
### 概述
------------
XTween 是一个高性能的 Unity 动画系统，提供了丰富的动画类型、缓动效果和灵活的控制方式。它采用对象池技术优化性能，支持编辑器预览，适用于 UI 和游戏对象的动画处理。 并且该插件是一个功能全面、性能优化的 Unity 动画解决方案，适用于各种动画需求，从简单的 UI 动效到复杂的游戏对象动画。其架构清晰度和零GC设计特别适合需要大量动态动画的项目（如UI密集型游戏）。相比主流插件，它在类型扩展性和内存控制上有独特优势，而链式API和完整文档则降低了使用门槛
<br>
<br>
| 开源不易，您的支持是持续更新的动力，<br>这个小工具倾注了我无数个深夜的调试与优化，它永远免费，但绝非无成本，如果您觉得这个工具<br>能为您节省时间、解决问题，甚至带来一丝愉悦，请考虑赞助一杯咖啡，让我知道：有人在乎这份付出，而这将成为我熬夜修复Bug、<br>添加新功能的最大动力。开源不是用爱发电，您的认可会让它走得更远|![](Docs/donate.jpg) |
|:-|-:|
| **欢迎加入技术研讨群，在这里可以和我以及大家一起探讨插件的优化以及相关的技术实现思路，同时在做项目时遇到的众多问题以及瓶颈<br>阻碍都可以互相探讨学习**|![](Docs/qqgroups.jpg) |

<br>

### 📦 架构设计特色
---
#### - ✅**分层架构** 
- **核心层：XTween_Base<T> 抽象基类处理通用动画逻辑（生命周期/进度计算/回调系统）**
- **实现层：7种特化类型（Float/Int/Vector2/Vector3/Color/Quaternion/String）各自实现类型安全的插值计算**
- **管理层：XTween_Manager单例全局管理，XTween_Pool对象池优化性能**
#### - ✅**多态设计** 
- **通过抽象方法强制子类实现类型相关逻辑：**
```
protected abstract T Lerp(T a, T b, float t);
protected abstract T GetDefaultValue();
```
#### - ✅**ECS式数据驱动** 
- **每个Tween实例包含完整动画参数（start/end/duration/easeMode等）**
- **通过Update()方法纯数据计算，与Unity组件解耦**

### 📦 性能优化特色
---
- **零GC设计**
- **使用对象池（XTween_Pool）复用实例**
- **链表+字典管理活跃动画（避免List扩容）**
- **预编译指令分离编辑器/运行时逻辑**
- **高效更新机制**
- **双缓冲队列：_PendingAdd/_PendingRemove避免迭代时修改**
- **迭代器缓存：_ActiveTweens_CachedIterators减少遍历开销**
- **延迟计算：按需重建缓存（_IteratorsDirty标记）**
- **内存安全**
- **自动清理无效引用（CleanDeadReferences）**
- **回调注销检查（避免内存泄漏）**

### 📦 核心组件
------------
|🌱 Controller|🌱 Interface|🌱 Pool|🌱 Manager|🌱 EaseLibrary|🌱 Previewer|
|:---:|:---:|:---:|:---:|:---:|:---:|
|**动画控制器**|**动画接口**|**动画池**|**动画管理器**|**缓动库**|**预览器**|
|动画控制器，支持多种动画类型（位置、旋转、缩放、颜色等），可配置动画参数（持续时间、延迟、缓动模式等，提供按键控制（播放、倒带、终止等）**|支持多种动画类型（位置、旋转、缩放、颜色等），可配置动画参数（持续时间、延迟、缓动模式等），提供按键控制（播放、倒带、终止等）|支持多种动画类型（位置、旋转、缩放、颜色等），可配置动画参数（持续时间、延迟、缓动模式等），提供按键控制（播放、倒带、终止等）|动画注册/注销，每帧更新动画状态，提供动画查找功能|Linear（线性），Sine（正弦），Quad（二次），Cubic（三次），Elastic（弹性），Bounce（弹跳）等|动画注册/注销，每帧更新动画状态，提供动画查找功能|

### 📦 类型特化（统一特性：继承自`XTween_Base<T>`、支持`ReturnSelf()`链式调用、提供默认构造和参数构造）
------------
| 类型 | 说明 | 关键方法 | 默认值 | 应用场景 |
|------|------|----------|--------|----------|
| **Specialized_Color** | 处理颜色(Color)动画，支持RGBA通道插值 | `Color.LerpUnclamped()` | `Color.white` | UI颜色变化、透明度动画 |
| **Specialized_Float** | 处理浮点数(float)动画，实现平滑过渡 | `Mathf.Lerp()` | `0f` | 进度条、数值变化 |
| **Specialized_Int** | 处理整数(int)动画，支持离散值变化 | `Mathf.Lerp()+RoundToInt()` | `0` | 分数计数、整数显示 |
| **Specialized_Quaternion** | 处理四元数(Quaternion)动画，支持3D旋转 | `Quaternion.Lerp/SlerpUnclamped()` | `Quaternion.identity` | 3D物体旋转 |
| **Specialized_String** | 处理字符串(string)动画，支持逐字显示 | 字符截取计算 | `string.Empty` | 打字机效果 |
| **Specialized_Vector2** | 处理二维向量(Vector2)动画 | `Vector2.LerpUnclamped()` | `Vector2.zero` | 2D位置/尺寸变化 |
| **Specialized_Vector3** | 处理三维向量(Vector3)动画 | `Vector3.LerpUnclamped()` | `Vector3.zero` | 3D变换动画 |
| **Specialized_Vector4** | 处理四维向量(Vector4)动画 | `Vector4.LerpUnclamped()` | `Vector4.zero` | 特殊参数控制 |

### 📦 丰富的缓动库
------------
|缓动类型|In|Out|InOut||缓动类型|In|Out|InOut|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|**Linear**<br>线性|![](Docs/ease_linear.png)|![](Docs/ease_linear.png)|![](Docs/ease_linear.png)|--|**Sine**<br>正弦曲线|![](Docs/ease_insine.png)|![](Docs/ease_outsine.png)|![](Docs/ease_inoutsine.png)|
|**Quad**<br>二次曲线|![](Docs/ease_inquad.png)|![](Docs/ease_outquad.png)|![](Docs/ease_inoutquad.png)|--|**Cubic**<br>三次曲线|![](Docs/ease_incubic.png)|![](Docs/ease_outcubic.png)|![](Docs/ease_inoutcubic.png)|
|**Quart**<br>四次曲线|![](Docs/ease_inquart.png)|![](Docs/ease_outquart.png)|![](Docs/ease_inoutquart.png)|--|**Quint**<br>五次曲线|![](Docs/ease_inquint.png)|![](Docs/ease_outquint.png)|![](Docs/ease_inoutquint.png)|
|**Expo**<br>指数曲线|![](Docs/ease_inexpo.png)|![](Docs/ease_outexpo.png)|![](Docs/ease_inoutexpo.png)|--|**Circ**<br>圆形曲线|![](Docs/ease_incirc.png)|![](Docs/ease_outcirc.png)|![](Docs/ease_inoutcirc.png)|
|**Elastic**<br>弹性曲线|![](Docs/ease_inelastic.png)|![](Docs/ease_outelastic.png)|![](Docs/ease_inoutelastic.png)|--|**Back**<br>回退曲线|![](Docs/ease_inback.png)|![](Docs/ease_outback.png)|![](Docs/ease_inoutback.png)|
|**Bounce**<br>弹跳曲线|![](Docs/ease_inbounce.png)|![](Docs/ease_outbounce.png)|![](Docs/ease_inoutbounce.png)|

### 📦 灵活的扩展类
------------
| 序号 | 类名称                     | 分类          |
|:------:|:----------------------------:|:---------------:|
| ✅    | `XTween.Alpha`             | **基础动画**       |
| ✅    | `XTween.AnchoredPosition`  | **UI动画**        |
| ✅    | `XTween.Color`             | **颜色动画**       |
| ✅    | `XTween.Fill`              | **填充动画**       |
| ✅    | `XTween.Path`              | **路径动画**       |
| ✅    | `XTween.Rotation`          | **旋转动画**       |
| ✅    | `XTween.Scale`             | **缩放动画**       |
| ✅    | `XTween.Shake`             | **抖动动画**       |
| ✅    | `XTween.Size`              | **尺寸动画**       |
| ✅   | `XTween.Text`              | **文本动画**       |
| ✅   | `XTween.Tiled`             | **平铺动画**       |
| ✅   | `XTween.TmpText`           | **临时文本动画**   |
| ✅   | `XTween.To.Color`          | **颜色过渡**       |
| ✅   | `XTween.To_Float`          | **浮点数过渡**     |
| ✅   | `XTween.To_Int`            | **整数过渡**       |
| ✅   | `XTween.To.String`         | **字符串过渡**     |
| ✅   | `XTween.To.Vector2`        | **二维向量过渡**   |
| ✅   | `XTween.To.Vector3`        | **三维向量过渡**   |
| ✅   | `XTween.To.Vector4`        | **四维向量过渡**  |


### 📦 可视化路径工具
------------
![](Docs/PathTool.png)

<br>

### 📦 可视化动画控制器
------------
![](Docs/Controller.png)

<br>

### 📦 可视化动画管理器
------------
![](Docs/Manager.png)

<br>

### 📦 生命周期
------------
#### - ▶️**Tween_Controller** 

```mermaid
flowchart LR
    %% ===== 核心流程 =====
    A([创建 / Tween_Create]) --> B([播放 / Tween_Play])
    B --> C{用户操作}
    C -->|暂停| D([暂停 / Tween_Pause])
    D -->|继续| B
    C -->|重播| E([重播 / Tween_Replay])
    E --> B
    C -->|停止| F([停止 / Tween_Stop])
    F --> G([销毁 / Tween_Kill])
    G --> H[对象池回收]

    %% ===== 循环路径 =====
    H -.->|复用| A

    %% ===== 样式定义 =====
    classDef process fill:#8bffc4,stroke:#ffffff00,stroke-width:2px,rx:8px,ry:8px
    classDef decision fill:#ff9966,stroke:#fbbc0400
    classDef endpoint fill:#5AB2F6,stroke:#4285f400,rx:8px,ry:8px
    class A,B,D,E,F,G process
    class C decision
    class H endpoint

    %% ===== 方法名样式 =====
    classDef method font-style:normal,font-size:20px,color:#000000
    class A,B,C,D,E,F,G,H method
```

#### - ▶️**XTween_Pool** 

```mermaid
flowchart LR
    %% ===== 核心流程 =====
    A([初始化 / PreloadAll]) --> B([创建 / CreateTween])
    B --> C{池中可用?}
    C -->|是| D[从池中取出]
    C -->|否| E{AutoExpand?}
    E -->|是| F[扩容池]
    E -->|否| G[返回null/警告]
    F --> D
    D --> H([使用 / RegisterTween])
    H --> I{用户操作}
    I -->|完成| J([回收 / RecycleTween])
    I -->|强制回收| K([ForceRecycleAll])
    J --> B
    K --> J

    %% ===== 统计路径 =====
    B --> L([统计 / Count_Created++])
    J --> M([统计 / Count_Created--])
    A --> N([统计 / Count_Preloaded初始化])

    %% ===== 样式定义 =====
    classDef process fill:#8bffc4,stroke:#ffffff00,stroke-width:2px,rx:8px,ry:8px
    classDef decision fill:#ff9966,stroke:#fbbc0400
    classDef endpoint fill:#5AB2F6,stroke:#4285f400,rx:8px,ry:8px
    classDef stat fill:#ffccf9,stroke:#ffffff00
    class A,B,D,F,H,J,K process
    class C,E decision
    class G endpoint
    class L,M,N stat

    %% ===== 方法名样式 =====
    classDef method font-style:normal,font-size:20px,color:#000000
    class A,B,C,D,E,F,G,H method

    %% ===== 特殊说明 =====
    click A "javascript:alert('预加载所有类型动画')"
    click B "javascript:alert('从池获取/创建新实例')"
    click J "javascript:alert('重置状态后回收入池')"
    click K "javascript:alert('强制终止并回收所有动画')"
```

#### - ▶️**XTween_Manager** 

```mermaid
flowchart LR
    %% ===== 核心流程 =====
    A([初始化 / Manager_Awake]) --> B([注册 / RegisterTween])
    B --> C{动画状态}
    C -->|播放| D([更新 / UpdateActiveTweens])
    C -->|暂停| E([暂停 / Tween_Pause])
    E -->|继续| D
    C -->|完成| F([回收 / UnregisterTween])
    F --> G([对象池回收])
    C -->|强制终止| H([批量清理 / Kill_All])

    %% ===== 管理循环 =====
    D --> C
    G -.->|复用| B

    %% ===== 样式定义 =====
    classDef process fill:#8bffc4,stroke:#ffffff00,stroke-width:2px,rx:8px,ry:8px
    classDef decision fill:#ff9966,stroke:#fbbc0400
    classDef endpoint fill:#5AB2F6,stroke:#4285f400,rx:8px,ry:8px
    class A,B,D,E,F process
    class C decision
    class G,H endpoint

    %% ===== 方法名样式 =====
    classDef method font-style:normal,font-size:20px,color:#000000
    class A,B,C,D,E,F,G,H method

    %% ===== 特殊说明 =====
    click A "javascript:alert('单例初始化+预加载池')"
    click D "javascript:alert('每帧更新+迭代器缓存优化')"
    click F "javascript:alert('清理索引+移入待处理队列')"
```

#### - ▶️**XTween_Previewer** 

```mermaid
flowchart LR
    %% ===== 核心流程 =====
    A([初始化 / Initialize]) --> B([添加动画 / Append])
    B --> C([准备播放 / Prepare])
    C --> D([播放 / Play])
    D --> E{动画状态}
    E -->|播放中| F([更新 / Update])
    E -->|完成| G([自动终止 / AutoKill])
    E -->|用户操作| H{控制指令}
    
    %% ===== 控制分支 =====
    H -->|倒带| I([Rewind])
    H -->|终止| J([Kill])
    H -->|清空| K([Clear])
    
    %% ===== 循环路径 =====
    I --> D
    J --> B
    K --> B
    G --> B

    %% ===== 样式定义 =====
    classDef process fill:#8bffc4,stroke:#ffffff00,stroke-width:2px,rx:8px,ry:8px
    classDef decision fill:#ff9966,stroke:#fbbc0400
    classDef endpoint fill:#5AB2F6,stroke:#4285f400,rx:8px,ry:8px
    class A,B,C,D,F,G,I,J,K process
    class E,H decision

    %% ===== 方法名样式 =====
    classDef method font-style:normal,font-size:20px,color:#000000
    class A,B,C,D,E,F,G,H method

    %% ===== 特殊说明 =====
    click A "javascript:alert('静态初始化+注册编辑器更新事件')"
    click F "javascript:alert('每帧检测：\\n1. 自动停止条件\\n2. 动画状态更新\\n3. 场景重绘')"
    click G "javascript:alert('触发条件：\\n1. 达到MaxTotalDuration\\n2. 所有动画完成')"
```

#### - ▶️**XTween_Base (With Specialized)** 

```mermaid
flowchart TB
    %% ===== 核心继承结构 =====
    A([XTween_Base<T>]) -->|抽象基类| B[核心生命周期]
    A --> C[通用功能]
    A --> D[模板方法]

    %% ===== 具体实现类 =====
    B --> E([Specialized Float])
    B --> F([Specialized Int])
    B --> G([Specialized Color])
    B --> H([Specialized Vector2/3/4])
    B --> I([Specialized Quaternion])
    B --> J([Specialized String])

    %% ===== 方法实现差异 =====
    E -->|Mathf.Lerp| K[数值插值]
    F -->|RoundToInt| L[整数处理]
    G -->|Color.Lerp| M[颜色过渡]
    H -->|Vector.Lerp| N[向量运算]
    I -->|Quaternion.Slerp| O[旋转插值]
    J -->|Substring| P[字符动画]

    %% ===== 样式定义 =====
    classDef abstract fill:#8bffc4,stroke:#388E3C,color:white,rx:8px,ry:8px,font-style:normal,font-size:20px,color:#000000
    classDef concrete fill:#5AB2F6,stroke:#1565C0,rx:8px,ry:8px,font-style:normal,font-size:20px,color:#000000
    classDef method fill:#FF9800,stroke:#F57C00,rx:8px,ry:8px,font-style:normal,font-size:16px,color:#000000

    class A abstract
    class E,F,G,H,I,J concrete
    class K,L,M,N,O,P method

    %% ===== 交互说明 =====
    click A "javascript:alert('提供核心生命周期管理\\n- Play/Pause/Kill\\n- 进度计算\\n- 回调系统')"
    click E "javascript:alert('Float特性:\\n- 精确浮点计算\\n- 支持所有缓动模式')"
    click J "javascript:alert('String特性:\\n- 字符逐显效果\\n- 特殊进度计算')"
```
