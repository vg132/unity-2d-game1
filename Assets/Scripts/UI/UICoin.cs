using TMPro;
using UnityEngine;

public class UICoin : MonoBehaviour
{
	private TextMeshProUGUI _coinText;

	private void Start()
	{
		_coinText = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		_coinText.SetText($"Coins: {GameManager.Instance.Coins}");
	}
}
