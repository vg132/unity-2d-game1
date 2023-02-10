using Assets.Scripts.Audio;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
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

		private void Awake()
		{
			_collider = GetComponent<Collider2D>();
			_particleSystem = GetComponent<ParticleSystem>();
		}

		private void Start()
		{
			_value = Random.Range(_minValue, _maxValue);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				_particleSystem.Play();
				SoundManager.Instance.PlaySound(_sound);
				GetComponent<Animator>().SetTrigger("Open");
				PlayerManager.Instance.UpdatePoints(_value);
				_collider.enabled = false;
			}
		}
	}
}
