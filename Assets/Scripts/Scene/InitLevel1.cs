using UnityEngine;

public class InitLevel1 : InitLevel
{
	private void Start()
	{
		GameManager.Instance.UpdateGameState(GameStateEnum.GameRunning);
	}
}
