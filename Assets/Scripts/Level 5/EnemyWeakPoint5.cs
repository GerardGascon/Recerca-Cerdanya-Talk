using UnityEngine;

namespace Level5 {
	public class EnemyWeakPoint5 : MonoBehaviour {

		Enemy5 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy5>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player")) return;
			PlayerMovement5 player = other.GetComponent<PlayerMovement5>();

			player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			player.Animator.SetTrigger(PlayerMovement5.Jump1);
			Destroy(_enemy.gameObject);
		}
	}
}