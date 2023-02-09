using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
	private static GameSceneManager _instance;

	private static int _currentScene = -1;

	#region Events

	public static event Action<Scene> OnSceneLoaded;

	#endregion

	private GameSceneManager()
	{
		GameManager.OnDeath += GameManager_OnDeath;
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

		_currentScene = SceneManager.GetActiveScene().buildIndex;
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

	private void OnDestroy()
	{
		GameManager.OnDeath -= GameManager_OnDeath;
		SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
	}

	private void SceneManager_sceneUnloaded(Scene scene)
	{
		Debug.Log($"Scene unloaded: {scene.name}");
	}

	private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log($"Scene loaded: {scene.name}");
		OnSceneLoaded?.Invoke(scene);
	}

	private void SceneManager_activeSceneChanged(Scene current, Scene next)
	{
		Debug.Log($"Current: {current.name}, Next: {next.name}");
	}

	private void GameManager_OnDeath()
	{

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

	//private IEnumerator LoadLevelAsync(int buildIndex)
	//{
	//	var loadOperation = SceneManager.LoadSceneAsync(buildIndex);
	//	while (!loadOperation.isDone)
	//	{
	//		var loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
	//		//_loadingSlider.value = loadingProgress;
	//		yield return null;
	//	};
	//}
}
