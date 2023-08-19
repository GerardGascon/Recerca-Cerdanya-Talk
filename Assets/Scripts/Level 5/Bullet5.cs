using UnityEngine;

namespace Level5 {
    public class Bullet5 : MonoBehaviour {
        [SerializeField] float speed;

        public void AddForce(int direction) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.right * direction * speed;
        }

        void OnCollisionEnter2D(Collision2D other) {
            Destroy(gameObject);
        }
    }
}
