using UnityEngine;

namespace Level8 {
    public class Enemy8 : MonoBehaviour {
        [SerializeField] float speed;

        [SerializeField] LayerMask groundLayer;

        [SerializeField] Transform sprite;

        [Header("Flipping Pivots")] 
        [SerializeField] Transform flippingPivotLeft;
        [SerializeField] Transform flippingPivotRight;

        int _facingDirection = 1;
        Rigidbody2D _rb;
    
        // Start is called before the first frame update
        void Awake() {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (_facingDirection > 0) {
                if (Physics2D.OverlapBox(flippingPivotRight.position, flippingPivotRight.localScale, 0, groundLayer)) {
                    _facingDirection = -1;
                    sprite.localScale = new Vector3(-1, 1, 1);
                }
            }else {
                if (Physics2D.OverlapBox(flippingPivotLeft.position, flippingPivotLeft.localScale, 0, groundLayer)) {
                    _facingDirection = 1;
                    sprite.localScale = Vector3.one;
                }
            }
            _rb.velocity = new Vector2(speed * _facingDirection, _rb.velocity.y);
        }

        void OnDrawGizmos() {
            if(!flippingPivotLeft || !flippingPivotRight) return;

            Gizmos.color = Color.red;
        
            Gizmos.DrawWireCube(flippingPivotLeft.position, flippingPivotLeft.localScale);
            Gizmos.DrawWireCube(flippingPivotRight.position, flippingPivotRight.localScale);
        }
    }
}
