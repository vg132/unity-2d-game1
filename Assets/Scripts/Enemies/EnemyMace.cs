using UnityEngine;

public class EnemyMace : MonoBehaviour
{
	private Vector3 startPosition;

	[SerializeField]
	private float _amplitude = 2.0f;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		transform.position = startPosition + _amplitude * new Vector3(0.0f, Mathf.Sin(Time.time), 0.0f);
	}
}
