using UnityEngine;

namespace Level10 {
	public class EnemyWeakPoint10 : MonoBehaviour {

		Enemy10 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy10>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement10 player = other.GetComponent<PlayerMovement10>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement10.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}