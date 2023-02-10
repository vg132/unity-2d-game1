using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
	public class GameSceneManager
	{
		private static GameSceneManager _instance;

		#region Events

		public static event Action<Scene> OnSceneLoaded;

		#endregion

		private GameSceneManager()
		{
			SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
			SceneManager.sceneLoaded += SceneManager_SceneLoaded;
			SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
		}

		public static GameSceneManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameSceneManager();
				}
				return _instance;
			}
		}

		public Scene CurrentScene => SceneManager.GetActiveScene();

		private void SceneManager_SceneUnloaded(Scene scene)
		{
			Debug.Log($"Scene unloaded: {scene.name}");
		}

		private void SceneManager_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			Debug.Log($"Scene loaded: {scene.name}");
			OnSceneLoaded?.Invoke(scene);
		}

		private void SceneManager_ActiveSceneChanged(Scene current, Scene next)
		{
			Debug.Log($"Current: {current.name}, Next: {next.name}");
		}

		public void RestartLevel()
		{
			LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void LoadScene(string name)
		{
			var buildIndex = SceneManager.GetSceneByName(name).buildIndex;
			LoadScene(buildIndex);
		}

		public void LoadNextScene()
		{
			LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		public void LoadScene(int index)
		{
			SceneManager.LoadScene(index);
		}

		public void LoadScene(GameScenes sceneType)
		{
			SceneManager.LoadScene((int)sceneType);
		}
	}
}
