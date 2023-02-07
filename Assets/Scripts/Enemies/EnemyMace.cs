using UnityEngine;

public class EnemyMace : MonoBehaviour
{
	private Vector3 startPosition;
	private AudioSource _audioSource;

	[SerializeField]
	private float _amplitude = 2.0f;

	private int _damage = 1;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		transform.position = startPosition + _amplitude * new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
			SoundManager.Instance.PlaySound(_audioSource);
		}
	}
}
