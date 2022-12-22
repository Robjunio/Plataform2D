using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : player
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float forceOnFalling;
    [SerializeField] private float fallingSpeedLimit;
    
    [SerializeField] private LayerMask layerGround; 
    
    private BoxCollider2D boxCollider2D;
    private PlayerInputs _inputActions;

    private bool isJumping;
    private bool canDoubleJump;
    private bool isSlow;
    
    private float direction;

    private void Awake()
    {
        TryGetComponent(out rig2D);
        TryGetComponent(out animator);
        TryGetComponent(out boxCollider2D);
        
        _inputActions = new PlayerInputs();

        _inputActions.Player.Movement.performed += ctx => ReadMovement(ctx.ReadValue<Vector2>());
        _inputActions.Player.Movement.canceled += _ => ReadMovement(Vector2.zero);

        _inputActions.Player.Jump.performed += _ => TryToJump();
    }
    
    private void FixedUpdate()
    {
        if (!isSlow)
        {
            rig2D.velocity = new Vector2(direction * speed, Mathf.Min(rig2D.velocity.y > 0 ? 
                rig2D.velocity.y : rig2D.velocity.y * forceOnFalling, fallingSpeedLimit));
        }
        else
        {
            rig2D.velocity = new Vector2(direction * speed, -0.25f);
        }
        
    }
    
    private void ReadMovement(Vector2 value)
    {
        direction = value.x;
    }
    
    private void TryToJump()
    {
        if(isGrounded())
        {
            rig2D.velocity = new Vector2(rig2D.velocity.x, 1);
            rig2D.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
            canDoubleJump = true;
        } 
        
        else if (canDoubleJump)
        {
            rig2D.velocity = new Vector2(rig2D.velocity.x, 1);
            rig2D.AddForce(new Vector2(0f,jumpForce), ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D rcGround = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 
            0, Vector2.down, 0.1f, layerGround);
        return rcGround.collider != null;
    }

    public bool GetIfIsDoubleJumping()
    {
        return canDoubleJump;
    }

    public float GetDirection()
    {
        return direction;
    }

    public void SlowDownPlayer()
    {
        isSlow = true;
        _inputActions.Disable();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
}
