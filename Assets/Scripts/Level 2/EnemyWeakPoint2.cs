using Level1;
using UnityEngine;

namespace Level2 {
	public class EnemyWeakPoint2 : MonoBehaviour {

		Enemy2 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy2>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player")) return;
			PlayerMovement2 player = other.GetComponent<PlayerMovement2>();

			player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			player.Animator.SetTrigger(PlayerMovement2.Jump1);
			Destroy(_enemy.gameObject);
		}
	}
}