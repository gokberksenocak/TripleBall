using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv0Manager : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
