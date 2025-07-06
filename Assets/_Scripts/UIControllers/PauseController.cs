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

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}