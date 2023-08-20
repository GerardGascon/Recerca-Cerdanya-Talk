using System;
using UnityEngine;

namespace Level9 {
    public class Bullet9 : BulletStats {
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
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * bulletSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            --_currentBounces;
            if (_currentBounces != 0 && !other.gameObject.CompareTag("Enemy")) return;
            
            Destroy(gameObject);
        }
    }
}
