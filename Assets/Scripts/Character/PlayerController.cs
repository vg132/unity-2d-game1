using GameOne.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameOne.Character
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		[Range(1f, 10f)]
		private float _speed;

		[SerializeField]
		[Range(1f, 10f)]
		public float _jumpHeight = 2.5f;

		[SerializeField]
		[Range(1f, 10f)]
		public float _doubleJumpHeight = 1.5f;

		[SerializeField]
		[Range(1f, 10f)]
		public float _timeToJumpApex = 1.0f;

		[SerializeField]
		[Range(1f, 10f)]
		public float fallMultiplier = 2.5f;

		[SerializeField]
		[Range(1f, 10f)]
		public float lowJumpMultiplier = 2f;

		[SerializeField]
		private LayerMask _ground;

		[SerializeField]
		private bool _enableDoubleJump;

		[SerializeField]
		private bool _enableWallJump;

		private PlayerActionControls _playerActionControls;
		private Rigidbody2D _playerObject;
		private Collider2D _playerCollider;
		private Animator _playerAnimator;
		private SpriteRenderer _spriteRenderer;

		bool _isJumping = false;
		bool _hasWallJumped = false;
		bool _hasDoubleJumped = false;

		private float _jumpVelocity;
		private float _maxDoubleJumpVelocity;
		private float _currentDoubleJumpVelocity;
		private float _gravity;
		private float _doubleJumpGravity;

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

			_gravity = -(2 * _jumpHeight) / Mathf.Pow(_timeToJumpApex, 2);
			_jumpVelocity = Mathf.Abs(_gravity) * _timeToJumpApex;
			_doubleJumpGravity = -(2 * _doubleJumpHeight) / Mathf.Pow(_timeToJumpApex, 2);
			_maxDoubleJumpVelocity = Mathf.Abs(_doubleJumpGravity) * _timeToJumpApex;
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
			if (ctx.started)
			{
				var groundDetection = IsGrounded();
				if (groundDetection && groundDetection.normal.y != 0)
				{
					_playerAnimator.SetTrigger("Jump");
					_isJumping = true;
					_hasDoubleJumped = false;
					_hasWallJumped = false;
				}
				else if (_enableWallJump && groundDetection && groundDetection.normal.x != 0 && !_hasWallJumped)
				{
					_playerAnimator.SetTrigger("Jump");
					_isJumping = true;
					_hasWallJumped = true;
				}
				else if (_enableDoubleJump && !_hasDoubleJumped)
				{
					_playerAnimator.SetTrigger("Jump");
					_isJumping = true;
					_hasDoubleJumped = true;

					var currentYVelocity = _playerObject.velocity.y;
					if (Mathf.Abs(currentYVelocity) > 0.75f)
					{
						_currentDoubleJumpVelocity = _maxDoubleJumpVelocity - _jumpVelocity - (Mathf.Abs(currentYVelocity) * 1.5f);
						_currentDoubleJumpVelocity = Mathf.Max(_maxDoubleJumpVelocity / 3, _currentDoubleJumpVelocity);
					}
					else
					{
						_currentDoubleJumpVelocity = _maxDoubleJumpVelocity;
					}
				}
			}
			else
			{
				_isJumping = false;
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

		private RaycastHit2D IsGrounded()
		{
			return Physics2D.BoxCast(_playerCollider.bounds.center, _playerCollider.bounds.size, 0f, Vector2.down, .1f, _ground);
		}

		private void Update()
		{
			if (GameManager.Instance.GameState != GameState.Running)
			{
				return;
			}
			Jump();
			Move();
		}



		private void Jump()
		{
			if (_isJumping)
			{
				var velocity = _playerObject.velocity;
				velocity.y = _hasDoubleJumped ? _currentDoubleJumpVelocity : _jumpVelocity;
				_playerObject.velocity = velocity;
			}
			
			if (_playerObject.velocity.y < 0f)
			{
				_playerObject.gravityScale = fallMultiplier;
			}
			else if (_playerObject.velocity.y > 0f && !_isJumping)
			{
				_playerObject.gravityScale = lowJumpMultiplier;
			}
			else
			{
				_playerObject.gravityScale = 1f;
			}
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
