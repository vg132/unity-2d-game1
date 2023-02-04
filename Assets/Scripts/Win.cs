using UnityEngine;

public class Win : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			GameState.Instance.PlayerState = GameState.State.Finished;
			MusicManager.Instance.PlaySound(MusicManager.GameSounds.Finish);
			GetComponent<Animator>().SetTrigger("Open");
		}
	}
}
