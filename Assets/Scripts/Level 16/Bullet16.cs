using System;
using DG.Tweening;
using UnityEngine;

namespace Level16 {
    public class Bullet16 : BulletStats {
        int _currentBounces;

        [SerializeField] Sprite red, yellow;

        protected override void Start() {
            base.Start();

            _currentBounces = maxBounces;
        }

        void FixedUpdate() {
            if (Rb.velocity.y <= maxYSpeed) return;
            Vector2 velocity = Rb.velocity;
            velocity.y = maxYSpeed;
            Rb.velocity = velocity;
        }

        public void AddForce(int direction) {
            ++PlayerMovement16.instance.Bullets;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * bulletSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            transform.DOPunchScale(transform.localScale * 1.5f, .1f);

            DOTween.Sequence()
                .AppendCallback(() => {
                    Sprite.sprite = yellow;
                }).AppendInterval(.1f)
                .AppendCallback(() => {
                    Sprite.sprite = red;
                });
            
            --_currentBounces;
            if (_currentBounces != 0 || other.gameObject.CompareTag("Enemy")) return;
            
            Destroy(gameObject);
        }
        
        void OnDestroy() {
            --PlayerMovement16.instance.Bullets;
        }
    }
}
