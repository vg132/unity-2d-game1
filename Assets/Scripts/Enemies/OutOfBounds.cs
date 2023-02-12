using GameOne.Managers;
using UnityEngine;

namespace GameOne.Enemies
{
	public class OutOfBounds : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D collision)
		{
			PlayerManager.Instance.UpdateHealth(PlayerManager.Instance.CurrentHealth);
		}
	}
}
