﻿using UnityEngine;

namespace Level4 {
	public class EnemyWeakPoint4 : MonoBehaviour {

		Enemy4 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy4>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player")) return;
			PlayerMovement4 player = other.GetComponent<PlayerMovement4>();

			player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			player.Animator.SetTrigger(PlayerMovement4.Jump1);
			Destroy(_enemy.gameObject);
		}
	}
}