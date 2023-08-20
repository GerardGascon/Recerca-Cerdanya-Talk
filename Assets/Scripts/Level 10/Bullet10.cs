using System;
using UnityEngine;

namespace Level10 {
    public class Bullet10 : BulletStats {
        int _currentBounces;

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
            ++PlayerMovement10.instance.Bullets;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * bulletSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            --_currentBounces;
            if (_currentBounces != 0 && !other.gameObject.CompareTag("Enemy")) return;
            
            --PlayerMovement10.instance.Bullets;
            Destroy(gameObject);
        }
    }
}
