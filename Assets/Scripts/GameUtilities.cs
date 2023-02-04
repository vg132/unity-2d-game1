using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtilities : MonoBehaviour
{
	private static GameUtilities _instance;

	public static GameUtilities Instance => _instance;

	public void Start()
	{
		if (_instance != null)
		{
			Destroy(this);
		}
		else
		{
			_instance = this;
		}
	}

	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
