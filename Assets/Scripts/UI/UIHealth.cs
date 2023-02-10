using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class UIHealth : MonoBehaviour
	{
		private TextMeshProUGUI _healthText;

		private void Awake()
		{
			_healthText = GetComponent<TextMeshProUGUI>();
		}

		private void Start()
		{
			PlayerManager.OnHealthChanged += GameManager_OnHealthChanged;
			_healthText.SetText($"Health: {PlayerManager.Instance.CurrentHealth}");
		}

		private void OnDestroy()
		{
			PlayerManager.OnHealthChanged -= GameManager_OnHealthChanged;
		}

		private void GameManager_OnHealthChanged(int obj)
		{
			_healthText.SetText($"Health: {PlayerManager.Instance.CurrentHealth}");
		}
	}
}
