using UnityEngine;

public class Win : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			SoundManager.Instance.PlaySound(SoundManager.GameSounds.Finish);
			GetComponent<Animator>().SetTrigger("Open");
		}
	}
}
