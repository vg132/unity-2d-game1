using UnityEngine;

namespace GameOne.Camera
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField]
		private Transform _playerPosition;

		[SerializeField]
		[Range(0.1f, 1.0f)]
		private float _smoothSpeed = 0.125f;

		[SerializeField]
		private Vector3 _offset;

		private Vector3 _velocity = Vector3.zero;

		private void LateUpdate()
		{
			var desiredPosition = _playerPosition.position + _offset;
			transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothSpeed);
		}
	}
}
