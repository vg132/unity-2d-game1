using GameOne;
using GameOne.Managers;
using UnityEngine;

namespace Assets.Scripts.Utils
{
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

		public void StartGame()
		{
			GameManager.Instance.StartGame();
			GameSceneManager.Instance.LoadScene(GameScenes.Level1);
		}
	}
}
