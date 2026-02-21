using SevenStrikeModules.XTween;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class wheel_Dim_ShapeArgs
{
    public RectTransform rect;
    public Vector2 target;
    public Vector2 origin;
    public float duration_in;
    public float delay_in;
    public EaseMode ease_in;
    public bool use_curve_in;
    public AnimationCurve curve_in;
    public float duration_out;
    public float delay_out;
    public EaseMode ease_out;
    public bool use_curve_out;
    public AnimationCurve curve_out;
    public XTween_Interface tweener;
}

[Serializable]
public class wheel_Dim_TextArgs
{
    public Text text;
    public Color source;
    public Color target;
    public float duration_in;
    public float delay_in;
    public EaseMode ease_in;
    public bool use_curve_in;
    public AnimationCurve curve_in;
    public float duration_out;
    public float delay_out;
    public EaseMode ease_out;
    public bool use_curve_out;
    public AnimationCurve curve_out;
    public XTween_Interface tweener;
}

[Serializable]
public class wheel_Dim_ImageArgs
{
    public Image img;
    public float source;
    public float target;
    public float duration_in;
    public float delay_in;
    public EaseMode ease_in;
    public bool use_curve_in;
    public AnimationCurve curve_in;
    public float duration_out;
    public float delay_out;
    public EaseMode ease_out;
    public bool use_curve_out;
    public AnimationCurve curve_out;
    public XTween_Interface tweener;
}

public class demo_mover_Wheel_Action_Dim : MonoBehaviour
{
    public demo_mover_Wheel wheel;
    public wheel_Dim_ShapeArgs[] shapeArg;
    public wheel_Dim_TextArgs[] textArg;
    public wheel_Dim_ImageArgs[] imgArg;
    public float DelayMulti = 0;

    void Start()
    {
        wheel.act_on_wheel_in_finished += act_on_wheel_in_finished;
        wheel.act_on_wheel_out_started += act_on_wheel_out_started;

        for (int i = 0; i < shapeArg.Length; i++)
        {
            shapeArg[i].rect.anchoredPosition = shapeArg[i].origin;
        }
        for (int i = 0; i < textArg.Length; i++)
        {
            textArg[i].text.color = textArg[i].source;
        }
        for (int i = 0; i < imgArg.Length; i++)
        {
            if (imgArg[i].img != null)
                imgArg[i].img.color = new Color(imgArg[i].img.color.r, imgArg[i].img.color.g, imgArg[i].img.color.b, imgArg[i].source);
        }
    }

    void Update()
    {

    }

    private void act_on_wheel_in_finished()
    {
        DimDisplay("in");
    }

    private void act_on_wheel_out_started()
    {
        DimDisplay("out");
    }


    private void DimDisplay(string status)
    {
        for (int i = 0; i < shapeArg.Length; i++)
        {
            if (shapeArg[i].tweener != null)
                shapeArg[i].tweener.Kill();
        }
        for (int i = 0; i < textArg.Length; i++)
        {
            if (textArg[i].tweener != null)
                textArg[i].tweener.Kill();
        }
        for (int i = 0; i < imgArg.Length; i++)
        {
            if (imgArg[i].tweener != null)
                imgArg[i].tweener.Kill();
        }

        if (status == "in")
        {
            for (int i = 0; i < shapeArg.Length; i++)
            {
                if (shapeArg[i].use_curve_in)
                    shapeArg[i].tweener = shapeArg[i].rect.xt_AnchoredPosition_To(shapeArg[i].target, shapeArg[i].duration_in, false, true).SetDelay(shapeArg[i].delay_in + DelayMulti).SetEase(shapeArg[i].curve_in).Play();
                else
                    shapeArg[i].tweener = shapeArg[i].rect.xt_AnchoredPosition_To(shapeArg[i].target, shapeArg[i].duration_in, false, true).SetDelay(shapeArg[i].delay_in + DelayMulti).SetEase(shapeArg[i].ease_in).Play();
            }
            for (int i = 0; i < textArg.Length; i++)
            {
                if (textArg[i].use_curve_in)
                    textArg[i].tweener = textArg[i].text.xt_FontColor_To(textArg[i].target, textArg[i].duration_in, true).SetDelay(textArg[i].delay_in + DelayMulti).SetEase(textArg[i].curve_in).Play();
                else
                    textArg[i].tweener = textArg[i].text.xt_FontColor_To(textArg[i].target, textArg[i].duration_in, true).SetDelay(textArg[i].delay_in + DelayMulti).SetEase(textArg[i].ease_in).Play();
            }
            for (int i = 0; i < imgArg.Length; i++)
            {
                if (imgArg[i].img != null)
                {
                    if (imgArg[i].use_curve_in)
                        imgArg[i].tweener = imgArg[i].img.xt_Alpha_To(imgArg[i].target, imgArg[i].duration_in, true).SetEase(imgArg[i].curve_in).SetDelay(imgArg[i].delay_in + DelayMulti).Play();
                    else
                        imgArg[i].tweener = imgArg[i].img.xt_Alpha_To(imgArg[i].target, imgArg[i].duration_in, true).SetEase(imgArg[i].ease_in).SetDelay(imgArg[i].delay_in + DelayMulti).Play();
                }
            }
        }
        else
        {
            for (int i = 0; i < shapeArg.Length; i++)
            {
                if (shapeArg[i].use_curve_out)
                    shapeArg[i].tweener = shapeArg[i].rect.xt_AnchoredPosition_To(shapeArg[i].origin, shapeArg[i].duration_out, false, true).SetDelay(shapeArg[i].delay_out).SetEase(shapeArg[i].curve_out).Play();
                else
                    shapeArg[i].tweener = shapeArg[i].rect.xt_AnchoredPosition_To(shapeArg[i].origin, shapeArg[i].duration_out, false, true).SetDelay(shapeArg[i].delay_out).SetEase(shapeArg[i].ease_out).Play();
            }
            for (int i = 0; i < textArg.Length; i++)
            {
                if (textArg[i].use_curve_out)
                    textArg[i].tweener = textArg[i].text.xt_FontColor_To(textArg[i].source, textArg[i].duration_out, true).SetDelay(textArg[i].delay_out).SetEase(textArg[i].curve_out).Play();
                else
                    textArg[i].tweener = textArg[i].text.xt_FontColor_To(textArg[i].source, textArg[i].duration_out, true).SetDelay(textArg[i].delay_out).SetEase(textArg[i].ease_out).Play();
            }
            for (int i = 0; i < imgArg.Length; i++)
            {
                if (imgArg[i].img != null)
                {
                    if (imgArg[i].use_curve_out)
                        imgArg[i].tweener = imgArg[i].img.xt_Alpha_To(imgArg[i].source, imgArg[i].duration_out, true).SetEase(imgArg[i].curve_out).SetDelay(imgArg[i].delay_out).Play();
                    else
                        imgArg[i].tweener = imgArg[i].img.xt_Alpha_To(imgArg[i].source, imgArg[i].duration_out, true).SetEase(imgArg[i].ease_out).SetDelay(imgArg[i].delay_out).Play();
                }
            }
        }
    }
}
