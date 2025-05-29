using UnityEngine;
using UnityEngine.SceneManagement;

public class VRSceneSelector : MonoBehaviour
{
    public void LoadSceneChar1()
    {
        SceneManager.LoadScene("Scene_Char1");
    }

    public void LoadSceneChar2()
    {
        SceneManager.LoadScene("Scene_Char2");
    }

    public void LoadSceneChar3()
    {
        SceneManager.LoadScene("Scene_Char3");
    }
}
