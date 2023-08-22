using UnityEngine;

namespace Level9 {
	public class EnemyWeakPoint9 : MonoBehaviour {

		Enemy9 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy9>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement9 player = other.GetComponent<PlayerMovement9>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement9.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}