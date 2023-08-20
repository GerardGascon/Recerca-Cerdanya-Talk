using UnityEngine;

namespace Level8 {
    public class Bullet8 : BulletStats {
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
            if (other.gameObject.CompareTag("Enemy")) {
                Destroy(gameObject);
            }
        }
    }
}
