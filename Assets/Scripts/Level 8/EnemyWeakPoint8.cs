using UnityEngine;

namespace Level8 {
	public class EnemyWeakPoint8 : MonoBehaviour {

		Enemy8 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy8>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement8 player = other.GetComponent<PlayerMovement8>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement8.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}