using UnityEngine;

public class UIFadeIn : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		Debug.Log("Attach to GameManager FadeIn Event");
		GameManager.OnFadeIn += GameManager_OnFadeIn;
	}

	private void OnDestroy()
	{
		Debug.Log("Detach to GameManager FadeIn Event");
		GameManager.OnFadeIn -= GameManager_OnFadeIn;
	}

	private void GameManager_OnFadeIn()
	{
		Debug.Log("Acting on FadeIn event");
		_animator.SetTrigger("FadeIn");
	}
}
