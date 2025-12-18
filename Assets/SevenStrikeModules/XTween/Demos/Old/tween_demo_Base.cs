using SevenStrikeModules.XTween;
using System;
using UnityEngine;

public class tween_demo_Base : MonoBehaviour
{
    [Header("Tween Settings")]
    [SerializeField, Range(0.1f, 15f)] internal float duration = 1f;
    [SerializeField][Range(0, 5f)] internal float delay = 0f;
    [SerializeField] internal bool randomDelay = false;
    [SerializeField] internal EaseMode easeMode = EaseMode.InOutCubic;
    [SerializeField] internal int loop = 0;
    [SerializeField] internal XTween_LoopType loopType;
    [SerializeField] internal float loopDelay;
    [SerializeField] internal bool isRelative = false;
    [SerializeField] internal bool isFromMode = false;
    [SerializeField] internal bool isAutoKill = true;
    [SerializeField] internal bool useCurve = false;
    [SerializeField] internal AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Debug Controls")]
    [SerializeField] internal bool showLogs = true;

    [Header("key Controls")]
    [SerializeField] internal KeyCode key_Tween_Create = KeyCode.B;
    [SerializeField] internal KeyCode key_Tween_Play = KeyCode.P;
    [SerializeField] internal KeyCode key_Tween_Pause_Or_Resume = KeyCode.Space;
    [SerializeField] internal KeyCode key_Tween_Kill = KeyCode.K;
    [SerializeField] internal KeyCode key_Tween_Rewind = KeyCode.R;

    [SerializeField] internal XTween_Interface CurrentTweener;

    private void Awake()
    {

    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(key_Tween_Create))
        {
            Tween_Create();
            if (XTween_Pool.EnablePool)
                XTween_Pool.LogStatistics(showLogs);
        }
        if (Input.GetKeyDown(key_Tween_Play))
        {
            Tween_Play();
            if (XTween_Pool.EnablePool)
                XTween_Pool.LogStatistics(showLogs);
        }
        if (Input.GetKeyDown(key_Tween_Pause_Or_Resume))
        {
            Tween_Pause_Or_Resume();
            if (XTween_Pool.EnablePool)
                XTween_Pool.LogStatistics(showLogs);
        }
        if (Input.GetKeyDown(key_Tween_Kill))
        {
            Tween_Kill();
            if (XTween_Pool.EnablePool)
                XTween_Pool.LogStatistics(showLogs);
        }
        if (Input.GetKeyDown(key_Tween_Rewind))
        {
            Tween_Rewind();
            if (XTween_Pool.EnablePool)
                XTween_Pool.LogStatistics(showLogs);
        }

        // 确保管理器已创建
        if (XTween_Manager.Instance == null)
        {
            if (showLogs) Debug.LogError("Tween_Manager Is Not Initialized！");
        }
    }

    public virtual void Tween_Play()
    {
        Tween_ChangeRandomDelay();
        if (CurrentTweener != null)
        {
            CurrentTweener.Play();
            if (showLogs) Debug.Log($"Tween Play");
        }
    }
    public virtual void Tween_Create()
    {
        Tween_CreateRandomDelay();

        if (showLogs) Debug.Log($"Tween Created");
    }
    protected void Tween_CreateRandomDelay()
    {
        if (!randomDelay)
            return;
        delay = UnityEngine.Random.Range(0.1f, 1.5f);
    }
    protected void Tween_ChangeRandomDelay()
    {
        if (!randomDelay)
            return;
        CurrentTweener.SetDelay(UnityEngine.Random.Range(0.1f, 1.5f));
    }
    public virtual void Tween_Pause_Or_Resume()
    {
        if (CurrentTweener == null) return;

        if (CurrentTweener.IsPlaying)
        {
            CurrentTweener.Pause();
            if (showLogs) Debug.Log($"Tween Paused");
        }
        else
        {
            CurrentTweener.Resume();
            if (showLogs) Debug.Log($"Tween Resume");
        }
    }
    public virtual void Tween_Kill()
    {
        if (CurrentTweener == null) return;
        CurrentTweener.Kill();
        if (showLogs) Debug.Log($"Tween Kill");
    }
    public virtual void Tween_Rewind()
    {
        if (CurrentTweener == null) return;
        CurrentTweener.Rewind();
        if (showLogs) Debug.Log($"Tween Rewind");
    }

    public virtual XTween_Interface CreateTween()
    {
        return CurrentTweener;
    }

    private void OnDestroy()
    {
        CurrentTweener?.Kill();
    }
}