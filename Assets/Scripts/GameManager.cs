using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	private GameState _gameState;

	public static GameManager Instance => _instance;
	public GameState GameState => _gameState;
	public UnitHealth Health => _unitHealth;
	public int Points => _points;

	private int _points = 0;
	private UnitHealth _unitHealth;

	#region Events

	public static event Action<GameState> OnGameStateChanged;
	public static event Action<int> OnHealthChanged;
	public static event Action<int> OnPointsChanged;
	public static event Action OnDeath;
	public static event Action<FadeType> OnFade;

	#endregion

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
		UpdateGameState(GameState.Death);
		OnFade?.Invoke(FadeType.FadeOut);
		CallWithDelay(() => GameSceneManager.Instance.LoadScene(GameScenes.GameOver), 2.0f);
	}

	private void Setup()
	{
		Debug.Log("GameManager Setup");
		OnFade?.Invoke(FadeType.FadeIn);
		UpdateGameState(GameState.Running);
		_unitHealth = new UnitHealth(10);
		OnHealthChanged?.Invoke(_unitHealth.Health);
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

	public void UpdateGameState(GameState newGameState)
	{
		if (newGameState != _gameState)
		{
			_gameState = newGameState;
			OnGameStateChanged?.Invoke(_gameState);
		}
	}

	public void CallWithDelay(Action action, float delayInSeconds = 0.0f)
	{
		StartCoroutine(ExecuteCallWithDelay(action, delayInSeconds));
	}

	private IEnumerator ExecuteCallWithDelay(Action action, float delayInSeconds)
	{
		if (delayInSeconds > 0)
		{
			yield return new WaitForSeconds(delayInSeconds);
		}
		action.Invoke();
		yield return null;
	}
}
