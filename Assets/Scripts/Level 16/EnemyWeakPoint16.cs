using DG.Tweening;
using UnityEngine;

namespace Level16 {
	public class EnemyWeakPoint16 : MonoBehaviour {

		Enemy16 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy16>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement16 player = other.GetComponent<PlayerMovement16>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement16.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement16.Bounce);
			
			if(!player) Destroy(other.gameObject);
			
			_enemy.DoKill();
			Destroy(gameObject);
		}
	}
}