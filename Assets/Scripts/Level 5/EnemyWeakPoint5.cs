using UnityEngine;

namespace Level5 {
	public class EnemyWeakPoint5 : MonoBehaviour {

		Enemy5 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy5>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement5 player = other.GetComponent<PlayerMovement5>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement5.Jump1);
			
			if(!player) Destroy(other.gameObject);
			Destroy(_enemy.gameObject);
		}
	}
}