using DG.Tweening;
using UnityEngine;

namespace Level13 {
	public class EnemyWeakPoint13 : MonoBehaviour {

		Enemy13 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy13>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement13 player = other.GetComponent<PlayerMovement13>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement13.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement13.Bounce);
			
			if(!player) Destroy(other.gameObject);
			
			_enemy.DoKill();
			Destroy(gameObject);
		}
	}
}