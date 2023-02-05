using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private static SoundManager _instance;

	private AudioSource _backgroundMusic;
	private AudioSource _soundEffect;

	private void Awake()
	{
		DontDestroyOnLoad(this);
		if(_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(gameObject);
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

	public void PlaySound(string resourceName)
	{
		_soundEffect.clip = Resources.Load<AudioClip>(resourceName);
		_soundEffect.Play();
	}

	public void PlaySound(AudioClip audioClip)
	{
		_soundEffect.clip = audioClip;
		_soundEffect.Play();
	}

	public void PlaySound(AudioSource audioSource)
	{
		audioSource.Play();
	}

	public class GameSounds
	{
		public static string Death => "Death";
		public static string Finish => "Finish";
	}
}
