using DG.Tweening;
using UnityEngine;

namespace Level18 {
	public class EnemyWeakPoint18 : MonoBehaviour {

		Enemy18 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy18>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement18 player = other.GetComponent<PlayerMovement18>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement18.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement18.Bounce);
			
			if(!player) Destroy(other.gameObject);
			
			_enemy.DoKill();
			Destroy(gameObject);
		}
	}
}