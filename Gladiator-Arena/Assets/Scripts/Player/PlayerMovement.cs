using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input;
    public float speed = 2f;
    private PlayerHealth _playerHealth;
    public ArenaBoundary arenaBoundary;
    

    public enum MovementState
    {
        Classic,
        Dashing,
        Hooking
    }
    
    private MovementState state =  MovementState.Classic;
    
    public float DashStaminaCost = 10f;
    public float dashDamping = 4f;
    public float dashDuration = 0.5f;
    public float DashSpeed = 15f;
    private Vector2 _dashDirection;

    public PlayerStamina playerStamina;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            MakeDash();
        }

    }

    void FixedUpdate()
    {
        if (!_playerHealth.IsAlive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        switch (state)
        {
            case MovementState.Classic:
            {
                rb.linearVelocity = input * speed;
                break;
            }
            case MovementState.Dashing:
            {
                break;
            }
            case MovementState.Hooking:
            {
                break;
            }
        }
        rb.position = arenaBoundary.ClampToArena(rb.position);
    }

    void MakeDash()
    {
        if (state != MovementState.Classic || input == Vector2.zero) return;
        
        if (playerStamina.SpendStamina(DashStaminaCost))
        {
            state = MovementState.Dashing;
            _dashDirection = input;

            rb.linearVelocity = _dashDirection * DashSpeed;
            rb.linearDamping = dashDamping;
            Invoke(nameof(EndDash), dashDuration);
        }
        else
        {
            playerStamina.NotEnoughStamina();
        }
    }

    void EndDash()
    {
        rb.linearDamping = 0;
        state = MovementState.Classic;
    }
}
