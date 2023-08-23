using UnityEngine;
using UnityEngine.InputSystem;

namespace Level16 {
    public class PlayerMovement16 : PlayerStats {
    
        [Header("Shooting")]
        [SerializeField] Bullet16 bullet;
        [SerializeField] Transform shootingPos;
    
        [Header("Physics")] 
        [SerializeField] LayerMask groundMask;
        [SerializeField] Vector2 feetSize;
    
        float _horizontalInput;
        float _xVelocity, _accelerationVelocity;
        float _currentCoyoteTime, _currentBufferTime;
        bool _grounded;

        float _facingDirectionVelocity;
        
        public int Bullets { set; get; }

        int _facingDirection = 1;

        PlayerInput _playerInput;

        [Header("Animations")] 
        [SerializeField] Animator animator;
        [SerializeField] Animator bounceAnimator;

        [Header("Particles")]
        [SerializeField] ParticleSystem runningParticles;
        
        public Animator Animator => animator;
        public Animator BounceAnimator => bounceAnimator;

        public static readonly int Jump1 = Animator.StringToHash("Jump");
        static readonly int XVelocity = Animator.StringToHash("xVelocity");
        static readonly int YVelocity = Animator.StringToHash("yVelocity");
        static readonly int Grounded = Animator.StringToHash("Grounded");
        public static readonly int Bounce = Animator.StringToHash("Bounce");
        
        public static PlayerMovement16 instance;

        void Awake() {
            instance = this;
            
            _playerInput = new PlayerInput();
        
            _playerInput.Gameplay.Horizontal.started += HorizontalHandler;
            _playerInput.Gameplay.Horizontal.performed += HorizontalHandler;
            _playerInput.Gameplay.Horizontal.canceled += HorizontalHandler;

            _playerInput.Gameplay.Jump.started += Jump;
            _playerInput.Gameplay.Jump.canceled += Jump;
        
            _playerInput.Gameplay.Fire.performed += Fire;
        }

        void Jump(InputAction.CallbackContext obj) {
            if (obj.started) {
                _currentBufferTime = bufferTime;
            }else if (obj.canceled) {
                if (Rb.velocity.y < 0f) return;
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * jumpCancellationMultiplier);
            }
        }

        void Fire(InputAction.CallbackContext obj) {
            if (Bullets >= maxBullets) return;
            Bullet16 bullet16 = Instantiate(bullet, shootingPos.position, Quaternion.identity);
            bullet16.AddForce(_facingDirection);
        }
    
        void HorizontalHandler(InputAction.CallbackContext obj) {
            _horizontalInput = obj.ReadValue<float>();
            _facingDirection = _horizontalInput > 0 ? 1 : _horizontalInput < 0 ? -1 : _facingDirection;
        }
    
        void OnEnable() {
            _playerInput.Enable();
        }

        void OnDisable() {
            _playerInput.Disable();
        }

        // Update is called once per frame
        void Update() {
            float xScale = Mathf.SmoothDamp(transform.localScale.x, _facingDirection, ref _facingDirectionVelocity,
                .1f);
            transform.localScale = new Vector3(xScale, 1, 1);
            
            animator.SetFloat(XVelocity, Mathf.Abs(_xVelocity));
            bounceAnimator.SetFloat(XVelocity, Mathf.Abs(_xVelocity));
            animator.SetFloat(YVelocity, Rb.velocity.y);
            animator.SetBool(Grounded, _grounded);
            bounceAnimator.SetBool(Grounded, _grounded);

            if (_currentBufferTime > 0f && _currentCoyoteTime > 0f) {
                Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
                animator.SetTrigger(Jump1);
                bounceAnimator.SetTrigger(Bounce);

                _currentBufferTime = _currentCoyoteTime = 0f;
            }

            _currentCoyoteTime -= Time.deltaTime;
            _currentBufferTime -= Time.deltaTime;
        }

        void FixedUpdate() {
            bool grounded = Physics2D.OverlapBox(transform.position, feetSize, 0, groundMask);
            if (!_grounded && grounded) {
                bounceAnimator.SetTrigger(Bounce);
            }

            _grounded = grounded;
            if (_grounded && Rb.velocity.y < .1f)
                _currentCoyoteTime = coyoteTime;

            _xVelocity = Rb.velocity.x;
            _xVelocity = Mathf.SmoothDamp(_xVelocity, _horizontalInput * speed, ref _accelerationVelocity,
                acceleration);
            Rb.velocity = new Vector2(_xVelocity, Rb.velocity.y);
            
            if(Mathf.Abs(_xVelocity) > 1f && grounded)
                runningParticles.Play();
            else
                runningParticles.Stop();
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, feetSize);
        }
    }
}
