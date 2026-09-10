using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input;
    public float speed = 2f;
    private PlayerHealth _playerHealth;
    public ArenaBoundary arenaBoundary;

    [SerializeField] private AudioClip audioClipWalk;
    [SerializeField] private AudioSource audioSourceWalk;

    [SerializeField] private AudioClip audioClipDash;
    [SerializeField] private AudioSource audioSourceDash;
    public enum MovementState
    {
        Walking,
        Dashing,
        Hooking,
        Standing
    }
    
    private MovementState _state =  MovementState.Standing;
    private MovementState _previousState = MovementState.Standing;
    [SerializeField] private float dashStaminaCost = 10f;
    [SerializeField] private float dashDamping = 4f;

    private float _dashTimeRemaining;
    [SerializeField] private float dashDuration = 0.5f;
    
    [SerializeField] private float dashCooldown = 0.7f;
    private float _dashCooldownRemaining;
    
    [SerializeField] private float dashSpeed = 15f;
    private Vector2 _dashDirection;

    public PlayerStamina playerStamina;
    
    [SerializeField] private float walkSoundTimer = 0.2f;
    private float _currentWalkSoundTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentWalkSoundTimer = walkSoundTimer;
        _playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_state == MovementState.Dashing) return;
        _dashCooldownRemaining -= Time.deltaTime;
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        if (input == Vector2.zero)
        {
            _state  = MovementState.Standing;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            _dashTimeRemaining = dashDuration;
            _state = MovementState.Dashing;
        }
        else
        {
            _state  = MovementState.Walking;
        }

    }

    void FixedUpdate()
    {
        if (!_playerHealth.IsAlive)
        {
            _state = MovementState.Standing;
            return;
        }
        
        switch (_state)
        {
            case MovementState.Walking:
            {
                _currentWalkSoundTimer -= Time.deltaTime;
                if (_currentWalkSoundTimer <= 0f)
                {
                    _currentWalkSoundTimer = walkSoundTimer;
                    PlayFootstepAudio();
                }
                rb.linearVelocity = input * speed;
                break;
            }
            case MovementState.Dashing:
            {
                _dashTimeRemaining -= Time.deltaTime;
                if (_previousState != MovementState.Dashing && _dashCooldownRemaining <= 0f)
                {
                    _dashCooldownRemaining = dashCooldown;
                    MakeDash();
                }

                if (_dashTimeRemaining <= 0f || rb.linearVelocity.magnitude < 3f)
                {
                    _state = MovementState.Standing;
                }
                
                break;
            }
            case MovementState.Hooking:
            {
                break;
            }
            case MovementState.Standing:
            {
                rb.linearVelocity = Vector2.zero;
                break;
            }
        }

        _previousState = _state;
        rb.position = arenaBoundary.ClampToArena(rb.position);
    }

    void MakeDash()
    {

        if (playerStamina.SpendStamina(dashStaminaCost))
        {
            PlayDashAudio();
            _dashDirection = input;

            rb.linearVelocity = _dashDirection * dashSpeed;
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
        _state = MovementState.Standing;
    }

    void PlayFootstepAudio()
    {
        audioSourceWalk.PlayOneShot(audioClipWalk);      
    }

    void PlayDashAudio()
    {
        audioSourceDash.PlayOneShot(audioClipDash);
    }
}
