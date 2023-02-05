using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	private GameStateEnum _gameState;

	public static GameManager Instance => _instance;
	public GameStateEnum GameState => _gameState;

	public int Coins { get; set; } = 0;

	public static event Action<GameStateEnum> OnGameStateChanged;

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
		}
	}

	public void UpdateGameState(GameStateEnum newGameState)
	{
		_gameState = newGameState;
		OnGameStateChanged?.Invoke(_gameState);
	}
}
