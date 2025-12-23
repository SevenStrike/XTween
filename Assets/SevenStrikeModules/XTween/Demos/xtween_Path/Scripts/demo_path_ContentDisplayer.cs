using UnityEngine;
using UnityEngine.UI;

public class demo_path_ContentDisplayer : MonoBehaviour
{
    [SerializeField] public Text text_per;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetText(string value)
    {
        text_per.text = value;
    }
}
