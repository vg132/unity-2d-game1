using GameOne.Assets.Scripts.Utils;
using GameOne.Audio;
using GameOne.Character;
using UnityEngine;

namespace GameOne.Enemies
{
	public class EnemyMace : MonoBehaviour
	{
		private AudioSource _audioSource;
		private Vector3 _startPosition;
		private Vector3 _currentPosition;
		private GameObject _chaseTarget;
		private bool _hasChased;

		[SerializeField]
		[Range(0f, 10f)]
		private float _amplitude = 2.0f;

		[SerializeField]
		[Range(0f, 10f)]
		protected int _damage = 1;

		[SerializeField]
		private bool _moving = true;

		[SerializeField]
		private BoxCollider2D _chaseCollider;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			_startPosition = transform.position;
		}

		private void Update()
		{
			_currentPosition = _startPosition + _amplitude * new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f);
			if (_chaseTarget != null)
			{
				var step = 5.0f * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, _chaseTarget.transform.position, step);
			}
			else if (_hasChased && !transform.position.FuzzyEquals(_currentPosition, 2))
			{
				var step = 3.0f * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, _currentPosition, step);
			}
			else if (_hasChased && transform.position.FuzzyEquals(_currentPosition, 2))
			{
				_hasChased = false;
				Debug.Log("Chase Ended");
			}
			else if (_moving)
			{
				transform.position = _currentPosition;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GameState == GameState.Running)
			{
				collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
				SoundManager.Instance.PlaySound(_audioSource);
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GameState == GameState.Running)
			{
				_currentPosition = transform.position;
				_chaseTarget = collision.gameObject;
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if(collision.gameObject.CompareTag("Player") && GameManager.Instance.GameState==GameState.Running)
			{
				_chaseTarget = null;
				_hasChased = true;
			}
		}
	}
}
