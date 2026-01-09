using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class demo_controller_scene_loader : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image icon;
    public Image rect;
    public Color col_up = Color.white;
    public Color col_down = Color.white;
    public string sceneName;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            GetIcon();

            rect = GetComponent<Image>();

            sceneName = "xtween_demo";
        }
    }

    void Start()
    {
        GetIcon();

        col_up = rect.color;
    }

    void Update()
    {

    }

    private void GetIcon()
    {
        if (icon == null)
            icon = transform.Find("icon").GetComponent<Image>();

        SetIcon(Resources.Load<Sprite>($"DemoIcons/icon_back"));
    }

    private void SetIcon(Sprite spr)
    {
        icon.sprite = spr;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect.color = col_up;
        LoadScene();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rect.color = col_down;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
