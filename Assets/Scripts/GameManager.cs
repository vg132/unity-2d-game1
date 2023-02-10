using Assets.Scripts.Audio;
using Assets.Scripts.Managers;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;

		private GameState _gameState;

		public static GameManager Instance => _instance;
		public GameState GameState => _gameState;

		#region Events

		public static event Action<GameState> OnGameStateChanged;
		public static event Action<FadeType> OnFade;
		public static event Action OnGameStart;

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
			}
		}

		private void Start()
		{
			PlayerManager.OnDeath += PlayerManager_OnDeath;
			Setup();
		}

		private void OnDestroy()
		{
			PlayerManager.OnDeath -= PlayerManager_OnDeath;
		}

		private void PlayerManager_OnDeath()
		{
			UpdateGameState(GameState.Death);
			OnFade?.Invoke(FadeType.FadeOut);
			CallWithDelay(() => GameSceneManager.Instance.LoadScene(GameScenes.GameOver), 2.0f);
		}

		public void StartGame()
		{
			UpdateGameState(GameState.Running);
			OnGameStart?.Invoke();
		}

		private void Setup()
		{
			Debug.Log("GameManager Setup");
			OnFade?.Invoke(FadeType.FadeIn);
			UpdateGameState(GameState.Running);
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
}
