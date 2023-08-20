using UnityEngine;

namespace Level4 {
    public class Bullet4 : BulletStats {
        public void AddForce(int direction) {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * bulletSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            Destroy(gameObject);
        }
    }
}
