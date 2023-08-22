using DG.Tweening;
using UnityEngine;

namespace Level11 {
	public class EnemyWeakPoint11 : MonoBehaviour {

		Enemy11 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy11>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement11 player = other.GetComponent<PlayerMovement11>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement11.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement11.Bounce);
			
			if(!player) Destroy(other.gameObject);

			Destroy(_enemy.gameObject);
		}
	}
}