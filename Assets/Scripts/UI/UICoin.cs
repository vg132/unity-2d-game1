using GameOne.Managers;
using TMPro;
using UnityEngine;

namespace GameOne.UI
{
	public class UICoin : MonoBehaviour
	{
		private TextMeshProUGUI _coinText;

		private void Awake()
		{
			_coinText = GetComponent<TextMeshProUGUI>();
		}

		private void Start()
		{
			PlayerManager.OnPointsChanged += GameManager_OnPointsChanged;
			_coinText.SetText($"Coins: {PlayerManager.Instance.Points}");
		}

		private void GameManager_OnPointsChanged(int points)
		{
			_coinText.SetText($"Coins: {points}");
		}
	}
}
