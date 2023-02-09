using UnityEngine;

public class EnemySpike : MonoBehaviour
{
	private AudioSource _audioSource;

	[SerializeField]
	protected int _damage = 1;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GameState == GameStateEnum.GameRunning)
		{
			collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
			SoundManager.Instance.PlaySound(_audioSource);
		}
	}
}

