using SevenStrikeModules.XTween;
using UnityEngine;

public class demo_base : MonoBehaviour
{
    /// <summary>
    /// 当前创建的动画
    /// </summary>
    [SerializeField] public XTween_Interface currentTweener;

    #region 动画参数
    /// <summary>
    /// 耗时
    /// </summary>
    [SerializeField, Range(0.1f, 15f)] public float duration = 1f;
    /// <summary>
    /// 延迟
    /// </summary>
    [SerializeField][Range(0, 5f)] public float delay = 0f;
    /// <summary>
    /// 最小延迟
    /// </summary>
    [SerializeField] public float min_delay = 0f;
    /// <summary>
    /// 最大延迟
    /// </summary>
    [SerializeField] public float max_delay = 1f;
    /// <summary>
    /// 随机延迟
    /// </summary>
    [SerializeField] public bool randomDelay = false;
    /// <summary>
    /// 缓动参数
    /// </summary>
    [SerializeField] public EaseMode easeMode = EaseMode.InOutCubic;
    /// <summary>
    /// 循环次数
    /// </summary>
    [SerializeField] public int loop = 0;
    /// <summary>
    /// 循环方式
    /// </summary>
    [SerializeField] public XTween_LoopType loopType;
    /// <summary>
    /// 循环延迟
    /// </summary>
    [SerializeField] public float loopDelay;
    /// <summary>
    /// 相对模式
    /// </summary>
    [SerializeField] public bool isRelative = false;
    /// <summary>
    /// 指定起点模式
    /// </summary>
    [SerializeField] public bool isFromMode = false;
    /// <summary>
    /// 自动杀死动画
    /// </summary>
    [SerializeField] public bool isAutoKill = true;
    /// <summary>
    /// 使用曲线运动
    /// </summary>
    [SerializeField] public bool useCurve = false;
    /// <summary>
    /// 曲线参数
    /// </summary>
    [SerializeField] public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    /// <summary>
    /// 调试信息开关
    /// </summary>
    [SerializeField] public bool debug = true;
    #endregion

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    private void OnDestroy()
    {
        currentTweener?.Kill();
    }

    #region 动画控制
    /// <summary>
    /// 创建动画
    /// </summary>
    public virtual void Tween_Create()
    {
        if (debug)
        {
            Debug.Log($"Tween Created");
            XTween_Pool.LogStatistics(debug);
        }
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    public virtual void Tween_Play()
    {
        if (currentTweener != null)
        {
            currentTweener.Play();
            if (debug)
            {
                Debug.Log($"Tween Play");
                XTween_Pool.LogStatistics(debug);
            }
        }
    }
    /// <summary>
    /// 倒退动画
    /// </summary>
    public virtual void Tween_Rewind()
    {
        if (currentTweener == null) return;
        currentTweener.Rewind();
        if (debug)
        {
            Debug.Log($"Tween Rewind");
            XTween_Pool.LogStatistics(debug);
        }
    }
    /// <summary>
    /// 杀死动画
    /// </summary>
    public virtual void Tween_Kill()
    {
        if (currentTweener == null) return;
        currentTweener.Kill();
        currentTweener = null;
        if (debug)
        {
            Debug.Log($"Tween Kill");
            XTween_Pool.LogStatistics(debug);
        }
    }
    /// <summary>
    /// 暂停&继续动画
    /// </summary>
    public virtual void Tween_Pause_Or_Resume()
    {
        if (currentTweener == null) return;

        if (currentTweener.IsPlaying)
        {
            currentTweener.Pause();
            if (debug)
            {
                Debug.Log($"Tween Paused");
                XTween_Pool.LogStatistics(debug);
            }
        }
        else
        {
            currentTweener.Resume();
            if (debug)
            {
                Debug.Log($"Tween Resume");
                XTween_Pool.LogStatistics(debug);
            }
        }
    }
    /// <summary>
    /// 设置随机延迟时间
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public virtual void Tween_SetRandomDelay(float min, float max)
    {
        if (!randomDelay)
            return;
        currentTweener.SetDelay(Random.Range(min, max));
    }
    /// <summary>
    /// 重新创建动画
    /// </summary>
    public virtual void Tweener_Restart()
    {
        // 杀死动画
        Tween_Kill();
        // 创建动画
        Tween_Create();
        // 播放动画
        Tween_Play();
    }
    #endregion
}