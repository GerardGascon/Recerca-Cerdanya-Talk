using DG.Tweening;
using UnityEngine;

namespace Level17 {
	public class EnemyWeakPoint17 : MonoBehaviour {

		Enemy17 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy17>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement17 player = other.GetComponent<PlayerMovement17>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement17.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement17.Bounce);
			
			if(!player) Destroy(other.gameObject);
			
			_enemy.DoKill();
			Destroy(gameObject);
		}
	}
}