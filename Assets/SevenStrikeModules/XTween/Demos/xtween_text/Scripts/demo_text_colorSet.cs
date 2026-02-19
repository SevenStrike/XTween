using UnityEngine;
using UnityEngine.UI;

public class demo_text_colorSet : MonoBehaviour
{
    public Text text;
    public Image dirty;
    public Image scan;
    public Image cable;
    public Image[] screens;
    public Color color_text = Color.black;
    public Color color_screen = Color.white;
    public Color color_dirty = Color.white;
    public Color color_scan = Color.clear;
    public Color color_cable = Color.red;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetColor_Text(Color color)
    {
        if (text != null)
            text.color = color;
    }

    public void SetColor_Screen(Color color)
    {
        if (screens.Length > 0)
        {
            for (int i = 0; i < screens.Length; i++)
            {
                screens[i].color = color;
            }
        }
    }

    public void SetColor_Dirty(Color color)
    {
        if (dirty != null)
        {
            dirty.color = color;
        }
    }

    public void SetColor_Scan(Color color)
    {
        if (scan != null)
        {
            scan.color = color;
        }
    }

    public void SetColor_Cable(Color color)
    {
        if (cable != null)
        {
            cable.color = color;
        }
    }


    private void OnDrawGizmos()
    {
        SetColor_Text(color_text);
        SetColor_Screen(color_screen);
        SetColor_Dirty(color_dirty);
        SetColor_Scan(color_scan);
        SetColor_Cable(color_cable);
    }
}
