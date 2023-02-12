using GameOne.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameOne.Character
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _speed;

		[SerializeField]
		public float jumpHeight = 2.5f;

		[SerializeField]
		public float timeToJumpApex = 1.0f;

		[SerializeField]
		private LayerMask _ground;

		bool _jumping = false;
		private float _jumpVelocity;
		private float _gravity;

		private PlayerActionControls _playerActionControls;
		private Rigidbody2D _playerObject;
		private Collider2D _playerCollider;
		private Animator _playerAnimator;
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			_playerObject = GetComponent<Rigidbody2D>();
			_playerCollider = GetComponent<Collider2D>();
			_playerAnimator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();

			_playerActionControls = new PlayerActionControls();

			_playerActionControls.Land.Jump.performed += Jump;
			_playerActionControls.Land.Jump.started += Jump;
			_playerActionControls.Land.Jump.canceled += Jump;
		}

		private void Start()
		{
			PlayerManager.OnDeath += GameManager_OnDeath;
			GameManager.OnGameStart += GameManager_OnGameStart;
			GameManager.OnLevelFinished += GameManager_OnLevelFinished;

			_gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
			_jumpVelocity = Mathf.Abs(_gravity) * timeToJumpApex;
		}

		private void OnDestroy()
		{
			PlayerManager.OnDeath -= GameManager_OnDeath;
			GameManager.OnGameStart -= GameManager_OnGameStart;
			GameManager.OnLevelFinished -= GameManager_OnLevelFinished;
			_playerActionControls.Land.Test.performed -= Jump;
			_playerActionControls.Land.Test.started -= Jump;
			_playerActionControls.Land.Test.canceled -= Jump;
		}

		private void Jump(InputAction.CallbackContext ctx)
		{
			if (ctx.started && IsGrounded())
			{
				_jumping = true;
				_playerAnimator.SetTrigger("Jump");
			}
			else
			{
				_jumping = false;
			}
		}

		private void GameManager_OnLevelFinished()
		{
			Debug.Log("Disable controlls, level finished");
			_playerActionControls.Disable();
		}

		private void GameManager_OnGameStart()
		{
			Debug.Log("Enable controller");
			_playerActionControls.Enable();
		}

		private void GameManager_OnDeath()
		{
			Debug.Log("Disable controller");
			_playerActionControls.Disable();
			_playerAnimator.SetTrigger("Death");
		}

		private void OnEnable()
		{
			_playerActionControls.Enable();
		}

		private void OnDisable()
		{
			_playerActionControls.Disable();
		}

		private bool IsGrounded()
		{
			var topLeftPoint = transform.position;
			topLeftPoint.x -= _playerCollider.bounds.extents.x;
			topLeftPoint.y = _playerCollider.bounds.extents.y;

			var bottomRightPoint = transform.position;
			bottomRightPoint.x += _playerCollider.bounds.extents.x;
			bottomRightPoint.y -= _playerCollider.bounds.extents.y;

			return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, _ground);
		}

		private void Update()
		{
			if (GameManager.Instance.GameState != GameState.Running)
			{
				return;
			}
			var velocity = _playerObject.velocity;
			if (_jumping)
			{
				velocity.y = _jumpVelocity;
			}
			velocity.y += _gravity * Time.deltaTime;
			_playerObject.velocity = velocity;

			Move();
		}

		private void Move()
		{
			var movmentInput = _playerActionControls.Land.Move.ReadValue<float>();
			var currentPosition = transform.position;
			currentPosition.x += movmentInput * _speed * Time.deltaTime;
			transform.position = currentPosition;
			_playerAnimator.SetBool("Run", movmentInput != 0);
			if (movmentInput < 0)
			{
				_spriteRenderer.flipX = true;
			}
			else if (movmentInput > 0)
			{
				_spriteRenderer.flipX = false;
			}
		}

		public void TakeDamage(int damageAmount)
		{
			PlayerManager.Instance.UpdateHealth(damageAmount);
		}
	}
}
