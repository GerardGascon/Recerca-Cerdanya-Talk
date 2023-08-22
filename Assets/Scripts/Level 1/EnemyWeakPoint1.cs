using UnityEngine;

namespace Level1 {
	public class EnemyWeakPoint1 : MonoBehaviour {

		Enemy1 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy1>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement1 player = other.GetComponent<PlayerMovement1>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement1.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}