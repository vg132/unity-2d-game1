using UnityEngine;

public class DoorManager : MonoBehaviour
{
	[SerializeField]
	private DoorStateEnum _doorState;

	[SerializeField]
	private GameObject _openDoor;
	private SpriteRenderer[] _openDoorSpriteRenderers;

	[SerializeField]
	private GameObject _closedDoor;
	private SpriteRenderer[] _closedDoorSpriteRenderers;

	[SerializeField]
	private AudioSource _stateChangeSound;

	[SerializeField]
	private bool _isFinish = false;

	private bool _active = false;

	private void Awake()
	{
		_openDoorSpriteRenderers = _openDoor.GetComponentsInChildren<SpriteRenderer>();
		_closedDoorSpriteRenderers = _closedDoor.GetComponentsInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		var isOpen = _doorState == DoorStateEnum.Open;

		_openDoorSpriteRenderers[0].enabled = isOpen;
		_openDoorSpriteRenderers[1].enabled = isOpen;
		_closedDoorSpriteRenderers[0].enabled = !isOpen;
		_closedDoorSpriteRenderers[1].enabled = !isOpen;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && (!_isFinish || !_active))
		{
			_doorState = DoorStateEnum.Open;
			_active = true;
			SoundManager.Instance.PlaySound(_stateChangeSound);
			GameManager.Instance.LoadNextScene(2);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !_isFinish)
		{
			_doorState = DoorStateEnum.Closed;
			SoundManager.Instance.PlaySound(_stateChangeSound);
		}
	}
}
