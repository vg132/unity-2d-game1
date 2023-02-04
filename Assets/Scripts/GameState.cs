using UnityEngine;

public class GameState : MonoBehaviour
{
	private static GameState _instance;

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

	public static GameState Instance => _instance;

	public State PlayerState { get; set; }

	public enum State
	{
		Idle,
		Walking,
		Running,
		Jumping,
		Dead,
		Finished
	}
}
