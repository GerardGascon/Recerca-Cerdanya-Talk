using UnityEngine;

namespace Level3 {
	public class EnemyWeakPoint3 : MonoBehaviour {

		Enemy3 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy3>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement3 player = other.GetComponent<PlayerMovement3>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement3.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}