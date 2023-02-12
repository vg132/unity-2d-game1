using GameOne.Audio;
using GameOne.Character;
using UnityEngine;

namespace GameOne.Enemies
{
	public class EnemyMace : MonoBehaviour
	{
		private AudioSource _audioSource;
		private Vector3 startPosition;

		[SerializeField]
		private float _amplitude = 2.0f;
		[SerializeField]
		protected int _damage = 1;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
		}

		private void Start()
		{
			startPosition = transform.position;
		}

		private void Update()
		{
			transform.position = startPosition + _amplitude * new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GameState == GameState.Running)
			{
				collision.gameObject.GetComponent<PlayerController>().TakeDamage(_damage);
				SoundManager.Instance.PlaySound(_audioSource);
			}
		}
	}
}
