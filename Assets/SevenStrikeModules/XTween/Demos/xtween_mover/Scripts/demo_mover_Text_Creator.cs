using UnityEngine;
using UnityEngine.EventSystems;

public class demo_mover_Text_Creator : MonoBehaviour
{
    public GameObject[] Titles;
    public GameObject CreatedTween;

    void Start()
    {
    }

    public void PlayerTween(string name)
    {
        if (CreatedTween != null)
            DestroyImmediate(CreatedTween, true);

        for (int i = 0; i < Titles.Length; i++)
        {
            if (Titles[i].name == name)
            {
                CreatedTween = Instantiate(Titles[i], this.transform);

            }
        }
    }

    void Update()
    {

    }
}
