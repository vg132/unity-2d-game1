using UnityEngine;

public class GameUtilities : MonoBehaviour
{
	public void QuitGame()
	{
		Application.Quit();
	}

	public void ExitToMenu()
	{
		GameSceneManager.Instance.LoadScene(GameScenes.MainMenu);
	}

	public void RestartGame()
	{
		GameSceneManager.Instance.LoadScene(GameScenes.Level1);
	}
}
