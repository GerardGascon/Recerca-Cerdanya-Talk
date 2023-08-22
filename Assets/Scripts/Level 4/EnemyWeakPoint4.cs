using UnityEngine;

namespace Level4 {
	public class EnemyWeakPoint4 : MonoBehaviour {

		Enemy4 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy4>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement4 player = other.GetComponent<PlayerMovement4>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement4.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}