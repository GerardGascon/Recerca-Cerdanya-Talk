using UnityEngine;
using UnityEngine.InputSystem;

namespace Level4 {
    public class PlayerMovement4 : PlayerStats {
    
        [Header("Shooting")]
        [SerializeField] Bullet4 bullet;
        [SerializeField] Transform shootingPos;
    
        [Header("Physics")] 
        [SerializeField] LayerMask groundMask;
        [SerializeField] Vector2 feetSize;
    
        float _horizontalInput;
        float _xVelocity;
        float _accelerationVelocity;
        float _currentCoyoteTime;
        bool _grounded;

        int _facingDirection = 1;

        PlayerInput _playerInput;

        [Header("Animations")] 
        [SerializeField] Animator animator;
        public Animator Animator => animator;

        public static readonly int Jump1 = Animator.StringToHash("Jump");
        static readonly int XVelocity = Animator.StringToHash("xVelocity");
        static readonly int YVelocity = Animator.StringToHash("yVelocity");
        static readonly int Grounded = Animator.StringToHash("Grounded");

        void Awake() {
            _playerInput = new PlayerInput();
        
            _playerInput.Gameplay.Horizontal.started += HorizontalHandler;
            _playerInput.Gameplay.Horizontal.performed += HorizontalHandler;
            _playerInput.Gameplay.Horizontal.canceled += HorizontalHandler;

            _playerInput.Gameplay.Jump.started += Jump;
            _playerInput.Gameplay.Jump.canceled += Jump;
        
            _playerInput.Gameplay.Fire.performed += Fire;
        }

        void Jump(InputAction.CallbackContext obj) {
            Debug.Log(_currentCoyoteTime);
            if (_currentCoyoteTime > 0f) {
                if(!obj.started) return;
                _currentCoyoteTime = 0f;
                Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
                animator.SetTrigger(Jump1);
            }else if (Rb.velocity.y > 0f) {
                if(!obj.canceled) return;
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * jumpCancellationMultiplier);
            }
        }

        void Fire(InputAction.CallbackContext obj) {
            Bullet4 bullet4 = Instantiate(bullet, shootingPos.position, Quaternion.identity);
            bullet4.AddForce(_facingDirection);
        }
    
        void HorizontalHandler(InputAction.CallbackContext obj) {
            _horizontalInput = obj.ReadValue<float>();
            _facingDirection = _horizontalInput > 0 ? 1 : _horizontalInput < 0 ? -1 : _facingDirection;
            transform.localScale = new Vector3(_facingDirection, 1, 1);
        }
    
        void OnEnable() {
            _playerInput.Enable();
        }

        void OnDisable() {
            _playerInput.Disable();
        }

        // Update is called once per frame
        void Update() {
            animator.SetFloat(XVelocity, Mathf.Abs(_xVelocity));
            animator.SetFloat(YVelocity, Rb.velocity.y);
            animator.SetBool(Grounded, _grounded);

            _currentCoyoteTime -= Time.deltaTime;
        }

        void FixedUpdate() {
            _grounded = Physics2D.OverlapBox(transform.position, feetSize, 0, groundMask);
            if (_grounded && Rb.velocity.y < .1f)
                _currentCoyoteTime = coyoteTime;

            _xVelocity = Rb.velocity.x;
            _xVelocity = Mathf.SmoothDamp(_xVelocity, _horizontalInput * speed, ref _accelerationVelocity,
                acceleration);;
            Rb.velocity = new Vector2(_xVelocity, Rb.velocity.y);
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, feetSize);
        }
    }
}
