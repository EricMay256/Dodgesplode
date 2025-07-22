using UnityEngine;

namespace BearFalls
{
  public class GameOverController : MonoBehaviour
  {
    public void MainMenu()
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void ResetGame()
    {
      GameManager.Instance.ResetGame();
    }
  }
}