using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Character
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _speed;

		[SerializeField]
		private float _jumpSpeed;

		[SerializeField]
		private LayerMask _ground;

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
			_playerActionControls.Land.Jump.performed += ctx => Jump(ctx);
		}

		private void Start()
		{
			PlayerManager.OnDeath += GameManager_OnDeath;
		}

		private void OnDestroy()
		{
			PlayerManager.OnDeath -= GameManager_OnDeath;
		}

		private void GameManager_OnDeath()
		{
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

		private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			if (GameManager.Instance.GameState != GameState.Running)
			{
				return;
			}
			if (IsGrounded())
			{
				_playerObject.AddForce(new Vector2(0, _jumpSpeed), ForceMode2D.Impulse);
				_playerAnimator.SetTrigger("Jump");
			}
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
