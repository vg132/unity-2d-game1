using UnityEngine;

namespace Assets.Scripts.UI
{
	public class UIFade : MonoBehaviour
	{
		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Start()
		{
			GameManager.OnFade += GameManager_OnFade;
		}

		private void OnDestroy()
		{
			GameManager.OnFade -= GameManager_OnFade;
		}

		private void GameManager_OnFade(FadeType fadeType)
		{
			if (fadeType == FadeType.FadeOut)
			{
				_animator.SetTrigger("FadeOut");
			}
			else if (fadeType == FadeType.FadeIn)
			{
				_animator.SetTrigger("FadeIn");
			}
		}
	}
}
