using SevenStrikeModules.XTween;
using UnityEngine;
using UnityEngine.UI;

public class seq : MonoBehaviour
{
    public XTween_Controller controller;
    public Sprite[] sprites;
    public Image image;

    public bool log;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<XTween_Controller>();
        if (image == null)
            image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        controller.act_on_start += on_start;
        //controller.act_onUpdate_int += on_update;
        //controller.act_onUpdate_float += on_update;
        //controller.act_onUpdate_string += on_update;
        //controller.act_onUpdate_vector2 += on_update;
        controller.act_onUpdate_vector3 += on_update;
        //controller.act_onUpdate_color += on_update;
        //controller.act_onUpdate_quaternion += on_update;
        //controller.Target_PathTool.act_on_pathStart += on_pathStart;
        //controller.Target_PathTool.act_on_pathComplete += on_pathComplete;
        //controller.Target_PathTool.act_on_pathMove += on_pathmove;
        //controller.Target_PathTool.act_on_pathProgress += on_pathprogress;
        //controller.Target_PathTool.act_on_pathLookatOrientation_withObject += on_pathprogress_lookat_obj;
        controller.act_on_kill += on_kill;
        controller.act_on_rewind += on_rewind;
        controller.act_on_complete += on_complete;
    }

    private void OnDisable()
    {
        controller.act_on_start -= on_start;
        //controller.act_onUpdate_int -= on_update;
        //controller.act_onUpdate_float -= on_update;
        //controller.act_onUpdate_string -= on_update;
        //controller.act_onUpdate_vector2 -= on_update;
        controller.act_onUpdate_vector3 -= on_update;
        //controller.act_onUpdate_color -= on_update;
        //controller.act_onUpdate_quaternion -= on_update;
        //controller.Target_PathTool.act_on_pathStart -= on_pathStart;
        //controller.Target_PathTool.act_on_pathComplete -= on_pathComplete;
        //controller.Target_PathTool.act_on_pathMove -= on_pathmove;
        //controller.Target_PathTool.act_on_pathProgress -= on_pathprogress;
        //controller.Target_PathTool.act_on_pathLookatOrientation_withObject -= on_pathprogress_lookat_obj;
        controller.act_on_kill -= on_kill;
        controller.act_on_rewind -= on_rewind;
        controller.act_on_complete -= on_complete;
    }

    private void on_complete(float arg0)
    {
        if (log)
            Debug.Log("seq - complete");
    }

    private void on_rewind()
    {
        image.sprite = sprites[0];
        //image.fillAmount = 0;
        //txt.tmp_Set_Content("");
        //image.rectTransform.sizeDelta = Vector2.one * 64;
        //image.rectTransform.anchoredPosition3D = Vector3.zero;
        //image.color = Color.white;
        //image.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        if (log)
            Debug.Log("seq - rewind");
    }

    private void on_kill()
    {
        image.sprite = sprites[0];
        //image.fillAmount = 0;
        //txt.tmp_Set_Content("");
        //image.rectTransform.sizeDelta = Vector2.one * 64;
        //image.rectTransform.anchoredPosition3D = Vector3.zero;
        //image.color = Color.white;
        //image.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        if (log)
            Debug.Log("seq - kill");
    }

    private void on_start()
    {
        if (log)
            Debug.Log("seq - start");
    }

    void Update()
    {
    }

    // In your seq class:
    //private void on_update(int spriteIndex, float progress, float elapsedTime)
    //{
    //    image.sprite = sprites[spriteIndex];
    //}

    //private void on_update(float value, float progress, float elapsedTime)
    //{
    //    image.fillAmount = value;
    //}
    //private void on_update(string value, float progress, float elapsedTime)
    //{
    //    txt.tmp_Set_Content(value);
    //}
    private void on_update(Vector3 value, float progress, float elapsedTime)
    {
        Debug.Log(value);
    }
    //private void on_update(Vector2 value, float progress, float elapsedTime)
    //{
    //    Debug.Log(value);
    //}
    //private void on_update(Color value, float progress, float elapsedTime)
    //{
    //    image.color = value;
    //}
    //private void on_update(Quaternion value, float progress, float elapsedTime)
    //{
    //    image.rectTransform.rotation = value;
    //}
    //private void on_pathmove(Vector3 value)
    //{
    //    //controller.Target_PathTool.
    //    Debug.Log(value);
    //}
    //private void on_pathprogress(float value)
    //{
    //    image.fillAmount = value;
    //}
    //private void on_pathprogress_lookat_obj(Vector3 value)
    //{
    //    image.rectTransform.localEulerAngles = value;
    //}
    //private void on_pathStart(Vector3 value)
    //{
    //    Debug.Log($"Path Start - {value}");
    //}
    //private void on_pathComplete(Vector3 value)
    //{
    //    Debug.Log($"Path Complete - {value}");
    //}
}
