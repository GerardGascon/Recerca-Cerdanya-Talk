using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour {

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    
    [Header("Shooting")]
    [SerializeField] Bullet1 bullet;
    [SerializeField] Transform shootingPos;
    
    [Header("Physics")] 
    [SerializeField] LayerMask groundMask;
    [SerializeField] Vector2 feetSize;
    
    float _horizontalInput;
    bool _grounded;

    int _facingDirection = 1;
    
    Rigidbody2D _rb;
    PlayerInput _playerInput;

    [Header("Animations")] 
    [SerializeField] Animator animator;

    static readonly int Jump1 = Animator.StringToHash("Jump");
    static readonly int XVelocity = Animator.StringToHash("xVelocity");
    static readonly int YVelocity = Animator.StringToHash("yVelocity");
    static readonly int Grounded = Animator.StringToHash("Grounded");
    
    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        
        _playerInput = new PlayerInput();
        
        _playerInput.Gameplay.Horizontal.started += HorizontalHandler;
        _playerInput.Gameplay.Horizontal.performed += HorizontalHandler;
        _playerInput.Gameplay.Horizontal.canceled += HorizontalHandler;

        _playerInput.Gameplay.Jump.performed += Jump;
        
        _playerInput.Gameplay.Fire.performed += Fire;
    }

    void Jump(InputAction.CallbackContext obj) {
        if (!_grounded) return;
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        animator.SetTrigger(Jump1);
    }

    void Fire(InputAction.CallbackContext obj) {
        Bullet1 bullet1 = Instantiate(bullet, shootingPos.position, Quaternion.identity);
        bullet1.AddForce(_facingDirection);
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
        animator.SetFloat(XVelocity, Mathf.Abs(_horizontalInput * speed));
        animator.SetFloat(YVelocity, _rb.velocity.y);
        animator.SetBool(Grounded, _grounded);
    }

    void FixedUpdate() {
        _grounded = Physics2D.OverlapBox(transform.position, feetSize, 0, groundMask);
        _rb.velocity = new Vector2(_horizontalInput * speed, _rb.velocity.y);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, feetSize);
    }
}
