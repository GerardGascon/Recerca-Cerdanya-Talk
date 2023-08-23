using DG.Tweening;
using UnityEngine;

namespace Level14 {
	public class EnemyWeakPoint14 : MonoBehaviour {

		Enemy14 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy14>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement14 player = other.GetComponent<PlayerMovement14>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement14.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement14.Bounce);
			
			if(!player) Destroy(other.gameObject);
			
			_enemy.DoKill();
			Destroy(gameObject);
		}
	}
}