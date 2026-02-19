using UnityEngine;

public class demo_mover_Text_CharacterTween : MonoBehaviour
{
    public demo_mover_Text controller;
    public MoverTweener[] textTweeners_Faded;

    void OnEnable()
    {
        textTweeners_Faded[0].delay_multi += Random.Range(0f, 0.35f);
        textTweeners_Faded[1].delay_multi += Random.Range(0f, 1f);
        controller.CollectTweens(textTweeners_Faded);
    }
}
