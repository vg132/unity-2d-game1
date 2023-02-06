using UnityEngine;

public class Chest : MonoBehaviour
{
	[SerializeField]
	private int _minValue;

	[SerializeField]
	private int _maxValue;

	[SerializeField]
	private AudioSource _sound;

	private int _value;

	private Collider2D _collider;
	private ParticleSystem _particleSystem;

	private void Start()
	{
		_collider = GetComponent<Collider2D>();
		_particleSystem = GetComponent<ParticleSystem>();

		_value = Random.Range(_minValue, _maxValue);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_particleSystem.Play();
			SoundManager.Instance.PlaySound(_sound);
			GetComponent<Animator>().SetTrigger("Open");
			GameManager.Instance.Coins += _value;
			_collider.enabled = false;
		}
	}
}
