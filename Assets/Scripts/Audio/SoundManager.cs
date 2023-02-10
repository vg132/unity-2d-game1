using UnityEngine;

namespace Assets.Scripts.Audio
{
	public class SoundManager : MonoBehaviour
	{
		private static SoundManager _instance;

		private AudioSource _backgroundMusic;
		private AudioSource _soundEffect;

		private void Awake()
		{
			if (_instance != null)
			{
				Destroy(this);
			}
			else
			{
				_instance = this;
				DontDestroyOnLoad(this);
			}
			var audioSources = GetComponents<AudioSource>();
			_backgroundMusic = audioSources[0];
			_soundEffect = audioSources[1];
		}

		public static SoundManager Instance => _instance;

		public void Start()
		{
			_backgroundMusic.Play();
		}

		public void PlaySound(AudioClip audioClip)
		{
			if (audioClip != null)
			{
				_soundEffect.clip = audioClip;
				_soundEffect.Play();
			}
		}

		public void PlaySound(AudioSource audioSource)
		{
			if (audioSource != null)
			{
				audioSource.Play();
			}
		}
	}
}
