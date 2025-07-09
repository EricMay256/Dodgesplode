using UnityEngine;

public class PauseController : MonoBehaviour
{
  public void MainMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
  }

  public void ResumeGame()
  {
    GameManager.Instance.PauseGame();
  }
}