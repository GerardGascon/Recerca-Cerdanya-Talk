using DG.Tweening;
using UnityEngine;

namespace Level12 {
	public class EnemyWeakPoint12 : MonoBehaviour {

		Enemy12 _enemy;
	
		void Awake() {
			_enemy = GetComponentInParent<Enemy12>();
		}

		void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player") && !other.CompareTag("Bullet")) return;
			PlayerMovement12 player = other.GetComponent<PlayerMovement12>();

			if(player) player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.deathForce);
			if(player) player.Animator.SetTrigger(PlayerMovement12.Jump1);
			if(player) player.BounceAnimator.SetTrigger(PlayerMovement12.Bounce);
			
			if(!player) Destroy(other.gameObject);
			
			_enemy.DoKill();
		}
	}
}