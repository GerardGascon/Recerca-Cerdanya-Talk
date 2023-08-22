using System;
using DG.Tweening;
using UnityEngine;

namespace Level13 {
    public class Enemy13 : MonoBehaviour {
        [SerializeField] float speed;

        [SerializeField] LayerMask groundLayer;

        [SerializeField] SpriteRenderer sprite;

        [Header("Flipping Pivots")] 
        [SerializeField] Transform flippingPivotLeft;
        [SerializeField] Transform flippingPivotRight;

        int _facingDirection = 1;
        Rigidbody2D _rb;

        static readonly int Lerp = Shader.PropertyToID("_Lerp");
    
        // Start is called before the first frame update
        void Awake() {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (_facingDirection > 0) {
                if (Physics2D.OverlapBox(flippingPivotRight.position, flippingPivotRight.localScale, 0, groundLayer)) {
                    _facingDirection = -1;
                    sprite.transform.localScale = new Vector3(-1, 1, 1);
                }
            }else {
                if (Physics2D.OverlapBox(flippingPivotLeft.position, flippingPivotLeft.localScale, 0, groundLayer)) {
                    _facingDirection = 1;
                    sprite.transform.localScale = Vector3.one;
                }
            }
            _rb.velocity = new Vector2(speed * _facingDirection, _rb.velocity.y);
        }

        public void DoKill() {
            Tween tween = sprite.material.DOFloat(1f, Lerp, .1f).SetLoops(1, LoopType.Yoyo).SetUpdate(true);
            ScreenShake.Shake(5f, .2f);
            tween.onComplete += () => {
                Destroy(gameObject);
            };
        }

        void OnDrawGizmos() {
            if(!flippingPivotLeft || !flippingPivotRight) return;

            Gizmos.color = Color.red;
        
            Gizmos.DrawWireCube(flippingPivotLeft.position, flippingPivotLeft.localScale);
            Gizmos.DrawWireCube(flippingPivotRight.position, flippingPivotRight.localScale);
        }
    }
}
