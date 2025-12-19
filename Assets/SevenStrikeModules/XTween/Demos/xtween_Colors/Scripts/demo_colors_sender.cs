using SevenStrikeModules.XTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demo_colors_sender : MonoBehaviour
{
    public Image[] rectpoints;
    public Transform bulletTemplate;
    public float distance_start = 100;
    public float distance_end = 100;
    public float duration = 1;
    public List<demo_colors_sender_bullet> bullets = new List<demo_colors_sender_bullet>();
    private EaseMode easeMode = EaseMode.InOutCubic;
    public bool auto_send;
    public float auto_send_time;

    void Start()
    {
        StartCoroutine(AutoSend());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet();
        }
    }

    /// <summary>
    ///自动创建子弹
    /// </summary>
    /// <returns></returns>
    IEnumerator AutoSend()
    {
        while (true)
        {
            if (auto_send)
            {
                CreateBullet();
                yield return new WaitForSeconds(auto_send_time);
            }
            else
            {
                yield return null;
            }
        }
    }

    /// <summary>
    /// 创建子弹
    /// </summary>
    private void CreateBullet()
    {
        int point_index = Random.Range(0, rectpoints.Length);
        // 实例化子弹
        demo_colors_sender_bullet mover = Instantiate(bulletTemplate, rectpoints[point_index].rectTransform).GetComponent<demo_colors_sender_bullet>();
        // 运动起始位置
        Vector2 pos_start = rectpoints[point_index].rectTransform.anchoredPosition + new Vector2(distance_start, 0);
        // 运动结束位置
        Vector2 pos_end = pos_start + new Vector2(distance_end, 0);
        // 初始化子弹
        mover.Initialized(pos_start, pos_end, rectpoints[point_index].color, Color.clear, duration, 0, easeMode);
        // 设置销毁回调
        mover.OnDestroyed = RemoveBulletFromList;
        // 添加到列表
        bullets.Add(mover);
    }

    /// <summary>
    /// 从列表中移除指定子弹
    /// </summary>
    internal void RemoveBulletFromList(demo_colors_sender_bullet bullet)
    {
        bullets.Remove(bullet);
    }
}
