using UnityEngine;

namespace Level8 {
	public class EnemyWeakPoint8 : MonoBehaviour {

		Enemy8 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy8>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player")) return;
			PlayerMovement8 player = other.GetComponent<PlayerMovement8>();

			player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			player.Animator.SetTrigger(PlayerMovement8.Jump1);
			Destroy(_enemy.gameObject);
		}
	}
}