using TMPro;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
	private TextMeshProUGUI _healthText;

	private void Awake()
	{
		_healthText = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		GameManager.OnHealthChanged += GameManager_OnHealthChanged;
		_healthText.SetText($"Health: {GameManager.Instance.Health.Health}");
	}

	private void OnDestroy()
	{
		GameManager.OnHealthChanged -= GameManager_OnHealthChanged;
	}

	private void GameManager_OnHealthChanged(int obj)
	{
		_healthText.SetText($"Health: {GameManager.Instance.Health.Health}");
	}
}
