using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	private BoxCollider2D _boxCollider;
	private AudioSource _audioSource;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_audioSource = GetComponent<AudioSource>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D colider)
	{
		if (colider.gameObject.CompareTag("Player"))
		{
			MusicManager.Instance.PlaySound(_audioSource);
			_spriteRenderer.enabled = false;
			_boxCollider.enabled = false;
		}
	}
}
