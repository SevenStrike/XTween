using UnityEngine;

public class demo_mover_Text_Clickable : MonoBehaviour
{
    public demo_mover_Text_Creator creator;
    public string TargetTitleName;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create()
    {
        creator.PlayerTween(TargetTitleName);
    }
}
