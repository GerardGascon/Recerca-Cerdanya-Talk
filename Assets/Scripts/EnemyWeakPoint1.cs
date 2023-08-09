using System;
using UnityEngine;

public class EnemyWeakPoint1 : MonoBehaviour {

	Enemy1 _enemy;
	
	void Awake() {
		_enemy = GetComponentInParent<Enemy1>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag("Player")) return;
		PlayerMovement1 player = other.GetComponent<PlayerMovement1>();

		player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
		player.Animator.SetTrigger(PlayerMovement1.Jump1);
		Destroy(_enemy.gameObject);
	}
}