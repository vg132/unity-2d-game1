using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		Debug.Log("Attach to GameManager FadeOut Event");
		GameManager.OnFadeOut += GameManager_OnFadeOut;
	}

	private void OnDestroy()
	{
		Debug.Log("Detach to GameManager FadeOut Event");
		GameManager.OnFadeOut -= GameManager_OnFadeOut;
	}

	private void GameManager_OnFadeOut()
	{
		Debug.Log("Acting on FadeOut event");
		_animator.SetTrigger("FadeOut");
	}
}
