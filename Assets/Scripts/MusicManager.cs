using UnityEngine;

public class MusicManager : MonoBehaviour
{
	private static MusicManager _instance;

	private AudioSource _backgroundMusic;
	private AudioSource _soundEffect;

	private void Awake()
	{
		var audioSources = GetComponents<AudioSource>();
		_backgroundMusic = audioSources[0];
		_soundEffect = audioSources[1];

		DontDestroyOnLoad(this);
		if(_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public static MusicManager Instance => _instance;

	public void PlaySound(string resourceName)
	{
		_soundEffect.clip = Resources.Load<AudioClip>(resourceName);
		_soundEffect.Play();
	}

	public class GameSounds
	{
		public static string Death => "Death";
		public static string Finish => "Finish";
	}
}
