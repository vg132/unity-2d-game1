using TMPro;
using UnityEngine;

public class UICoin : MonoBehaviour
{
	private TextMeshProUGUI _coinText;

	private void Awake()
	{
		_coinText = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		GameManager.OnPointsChanged += GameManager_OnPointsChanged;
		_coinText.SetText($"Coins: {GameManager.Instance.Points}");
	}

	private void GameManager_OnPointsChanged(int points)
	{
		_coinText.SetText($"Coins: {points}");
	}
}
