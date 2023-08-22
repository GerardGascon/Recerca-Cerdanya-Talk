using UnityEngine;

namespace Level2 {
    public class Bullet2 : BulletStats {
        public void AddForce(int direction) {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * bulletSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            if(!other.gameObject.CompareTag("Enemy")) Destroy(gameObject);
        }
    }
}
