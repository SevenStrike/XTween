using SevenStrikeModules.XTween;
using System;
using System.Collections;
using UnityEngine;

public class demo_path_rounder : MonoBehaviour
{
    public XTween_PathTool tweenPathTool;
    public XTween_Controller controller;
    public TrailRenderer trail;
    public demo_path_ContentDisplayer demo_Path_Content;

    private void Awake()
    {
        trail.emitting = false;
        controller.act_on_start += OnTweenPlay;
    }

    void Start()
    {

    }

    private void OnDisable()
    {
        controller.act_on_start -= OnTweenPlay;
    }

    private void OnTweenPlay()
    {
        StartCoroutine(TrailOrbit());
    }

    // Update is called once per frame
    void Update()
    {
        demo_Path_Content.SetText($"{Math.Round(tweenPathTool.PathProgress, 2) * 100}%");
    }

    #region 运动轨迹
    IEnumerator TrailOrbit()
    {
        yield return new WaitForSeconds(0.1f);
        trail.Clear();
        trail.emitting = true;
    }
    #endregion
}
