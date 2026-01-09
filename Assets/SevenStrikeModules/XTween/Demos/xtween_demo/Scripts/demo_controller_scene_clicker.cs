using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class demo_controller_scene_clicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image mark;
    public RectTransform rect;
    public RectTransform root;
    public Image icon;
    public Color col_up = Color.white;
    public Color col_down = Color.white;
    public string sceneName;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            GetIcon();

            rect = GetComponent<RectTransform>();
            mark = transform.parent.parent.Find("mark").GetComponent<Image>();
            root = transform.parent.parent.GetComponent<RectTransform>();

            sceneName = $"xtween_" + transform.name.Split(new char[1] { '_' })[1];
        }
    }

    void Start()
    {
        GetIcon();

        col_up = mark.color;
    }

    void Update()
    {

    }

    private void GetIcon()
    {
        if (icon == null)
            icon = transform.Find("icon").GetComponent<Image>();

        SetIcon(Resources.Load<Sprite>($"DemoIcons/icon_{transform.name.Split(new char[1] { '_' })[1]}"));
    }

    private void SetIcon(Sprite spr)
    {
        icon.sprite = spr;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //获取当前Rect的世界坐标
        Vector3 Pos_world = rect.position;
        //将世界坐标转换为屏幕像素坐标
        Vector2 Pos_screen = RectTransformUtility.WorldToScreenPoint(null, Pos_world);

        Vector2 localPosInRoot;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(root, Pos_screen, null, out localPosInRoot))
        {
            // 设置mark的anchoredPosition
            mark.rectTransform.anchoredPosition = localPosInRoot;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mark.rectTransform.anchoredPosition = new Vector2(0, -900);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mark.color = col_up;
        LoadScene();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mark.color = col_down;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
