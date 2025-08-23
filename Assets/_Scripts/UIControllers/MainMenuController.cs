using UnityEngine;
using UnityEngine.SceneManagement;

namespace BearFalls
{
  public class MainMenuController : MonoBehaviour
  {
    public void StartGame()
    {
      SceneManager.LoadScene("Gameplay");
    }

    public void ExitGame()
    {
      Application.Quit();
    }
  }
}