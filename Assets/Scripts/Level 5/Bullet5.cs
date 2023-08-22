using UnityEngine;

namespace Level5 {
    public class Bullet5 : BulletStats {
        public void AddForce(int direction) {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * direction * maxYSpeed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            if(!other.gameObject.CompareTag("Enemy")) Destroy(gameObject);
        }
    }
}
