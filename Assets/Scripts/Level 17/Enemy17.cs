using System;
using DG.Tweening;
using UnityEngine;

namespace Level17 {
    public class Enemy17 : EnemyStats {
        [SerializeField] float speed;

        [SerializeField] LayerMask groundLayer;

        [SerializeField] SpriteRenderer sprite;

        [Header("Flipping Pivots")] 
        [SerializeField] Transform flippingPivotLeft;
        [SerializeField] Transform flippingPivotRight;

        int _facingDirection = 1;

        static readonly int Lerp = Shader.PropertyToID("_Lerp");

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
            Rb.velocity = new Vector2(speed * _facingDirection, Rb.velocity.y);
        }

        public void DoKill() {
            sprite.material.DOFloat(1f, Lerp, .1f).SetLoops(2, LoopType.Yoyo).SetUpdate(true);
            ScreenShake.Shake(5f, .2f);
            Collider.enabled = false;
            Rb.freezeRotation = false;
            Rb.AddForceAtPosition(Vector2.up * throwForce, transform.position + new Vector3(-.5f, .5f), ForceMode2D.Impulse);
        }

        void OnDrawGizmos() {
            if(!flippingPivotLeft || !flippingPivotRight) return;

            Gizmos.color = Color.red;
        
            Gizmos.DrawWireCube(flippingPivotLeft.position, flippingPivotLeft.localScale);
            Gizmos.DrawWireCube(flippingPivotRight.position, flippingPivotRight.localScale);
        }
    }
}
