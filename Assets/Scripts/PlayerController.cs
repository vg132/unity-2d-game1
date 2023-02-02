using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

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
		_playerActionControls = new PlayerActionControls();
		_playerObject = GetComponent<Rigidbody2D>();
		_playerCollider = GetComponent<Collider2D>();
		_playerAnimator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		_playerActionControls.Enable();
	}

	private void OnDisable()
	{
		_playerActionControls.Disable();
	}

	private void Start()
	{
		_playerActionControls.Land.Jump.performed += ctx => Jump(ctx);
	}

	private void Jump(CallbackContext context)
	{
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
