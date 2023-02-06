using UnityEngine;

public class Coin : MonoBehaviour
{
	private BoxCollider2D _boxCollider;
	private AudioSource _audioSource;
	private SpriteRenderer _spriteRenderer;
	private ParticleSystem _particleSystem;
	private Vector3 _startPosition;

	[SerializeField]
	private float _amplitude = 2.0f;

	[SerializeField]
	private float _speed = 1.5f;

	[SerializeField]
	private int _minValue;

	[SerializeField]
	private int _maxValue;

	private int _value;

	[SerializeField]
	private AnimationDirection _animationDirection;

	private void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_audioSource = GetComponent<AudioSource>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_particleSystem = GetComponent<ParticleSystem>();

		_startPosition = transform.position;
		_value = Random.Range(_minValue, _maxValue);
	}

	private void Update()
	{
		if (_animationDirection == AnimationDirection.X)
		{
			transform.position = _startPosition + _amplitude * new Vector3(Mathf.Sin(Time.time * _speed), 0.0f, 0.0f);
		}
		else if (_animationDirection == AnimationDirection.Y)
		{
			transform.position = _startPosition + _amplitude * new Vector3(0.0f, Mathf.Sin(Time.time * _speed), 0.0f);
		}
	}

	private void OnTriggerEnter2D(Collider2D colider)
	{
		if (colider.gameObject.CompareTag("Player"))
		{
			SoundManager.Instance.PlaySound(_audioSource);
			_spriteRenderer.enabled = false;
			_boxCollider.enabled = false;
			GameManager.Instance.Coins += _value;
		}
	}
}
