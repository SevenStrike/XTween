using UnityEngine;
using UnityEngine.SceneManagement;

public class demo_controller : MonoBehaviour
{
    public string sceneName;
    private SceneManager sceneManager;

    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
