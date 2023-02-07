using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	private GameStateEnum _gameState;

	public static GameManager Instance => _instance;
	public GameStateEnum GameState => _gameState;
	public UnitHealth Health => _unitHealth;
	public int Points => _points;

	private int _points = 0;
	private UnitHealth _unitHealth;

	public static event Action<GameStateEnum> OnGameStateChanged;
	public static event Action<int> OnHealthChanged;
	public static event Action<int> OnPointsChanged;
	public static event Action OnDeath;

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(this);
		}
		else
		{
			_instance = this;
			DontDestroyOnLoad(this);
			Setup();
		}
	}

	private void Start()
	{
		OnDeath += GameManager_OnDeath;
	}

	private void OnDestroy()
	{
		OnDeath -= GameManager_OnDeath;
	}

	private void GameManager_OnDeath()
	{
		RestartLevel(3.0f);
	}

	private void Setup()
	{
		_unitHealth = new UnitHealth(10);
	}

	public void UpdatePoints(int pointsAmount)
	{
		if (pointsAmount != 0)
		{
			_points += pointsAmount;
			OnPointsChanged?.Invoke(_points);
		}		
	}

	public void UpdateHealth(int damageAmount)
	{
		_unitHealth.TakeDamage(damageAmount);
		if (damageAmount != 0)
		{
			OnHealthChanged?.Invoke(_unitHealth.Health);
			if (_unitHealth.Health <= 0)
			{
				OnDeath?.Invoke();
			}
		}
	}

	public void UpdateGameState(GameStateEnum newGameState)
	{
		if (newGameState != _gameState)
		{
			_gameState = newGameState;
			OnGameStateChanged?.Invoke(_gameState);
		}
	}

	public void RestartLevel(float delayInSeconds = 0)
	{
		LoadScene(SceneManager.GetActiveScene().buildIndex, delayInSeconds);
	}

	public void LoadScene(string name, float delayInSeconds = 0)
	{
		var buildIndex = SceneManager.GetSceneByName(name).buildIndex;
		LoadScene(buildIndex, delayInSeconds);
	}

	public void LoadScene(int index, float delayInSeconds = 0)
	{
		StartCoroutine(LoadLevelAsync(index, delayInSeconds));
	}

	private IEnumerator LoadLevelAsync(int buildIndex, float delayInSeconds)
	{
		if (delayInSeconds > 0)
		{
			yield return new WaitForSeconds(delayInSeconds);
		}
		var loadOperation = SceneManager.LoadSceneAsync(buildIndex);

		while (!loadOperation.isDone)
		{
			var loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
			//_loadingSlider.value = loadingProgress;
			yield return null;
		};
		UpdateHealth(-10);
	}
}
