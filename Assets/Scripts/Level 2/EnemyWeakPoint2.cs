using Level1;
using UnityEngine;

namespace Level2 {
	public class EnemyWeakPoint2 : MonoBehaviour {

		Enemy2 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy2>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement2 player = other.GetComponent<PlayerMovement2>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement2.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}