using UnityEngine;
using System.Collections.Generic;

public class PlayerHealthScript : MonoBehaviour
{
    public int health = 3;
    private int _currentHealth;
    public float regenerationRate = 3f;
    private float _currentTimeToReg = 0f;

    private bool _isAlive = true;
    public bool IsAlive => _isAlive;

    [SerializeField] private float enemyInvincibleDuration = 0.5f;
    [SerializeField] private float hazardInvincibleDuration = 1f;

    public enum DamageSourceType
    {
        Enemy,
        Hazard
    }

    private Dictionary<DamageSourceType, float> _invincibleTimers = new Dictionary<DamageSourceType, float>()
    {
        { DamageSourceType.Enemy, 0f },
        { DamageSourceType.Hazard, 0f }
    };

    public HealthUiScript healthUIScript;
    
    void Start()
    {
        healthUIScript.UpdateText(health, health);
        _currentHealth = health;
    }

    void HealthRegen()
    {
        if (_currentHealth < health &&  _isAlive)
        {
            _currentTimeToReg += Time.deltaTime;
            if (_currentTimeToReg >= regenerationRate)
            {

                _currentTimeToReg = 0f;
                _currentHealth++;
                healthUIScript.UpdateText(_currentHealth, health);
            }
        }
    }

    void TakeDamage(DamageSourceType source)
    {
        if (!_isAlive || Time.time < _invincibleTimers[source]) return;
        
        Debug.Log("Current health: " + _currentHealth);
        
        _currentHealth -= 1;
        _currentTimeToReg = 0f;
        
        healthUIScript.UpdateText(_currentHealth, health);
        if (_currentHealth <= 0)
        {
            _isAlive = false;
        }

        if (source == DamageSourceType.Enemy)
        {
            _invincibleTimers[DamageSourceType.Enemy] = Time.time + enemyInvincibleDuration;
            _invincibleTimers[DamageSourceType.Hazard] = Time.time + enemyInvincibleDuration;
        }
        else if (source == DamageSourceType.Hazard)
        {
            _invincibleTimers[DamageSourceType.Enemy] = Time.time + enemyInvincibleDuration;
            _invincibleTimers[DamageSourceType.Hazard] = Time.time + hazardInvincibleDuration;
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        HealthRegen();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(DamageSourceType.Enemy);
        }
        else if (collision.CompareTag("Hazard"))
        {
            TakeDamage(DamageSourceType.Hazard);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard") )
        {
            TakeDamage(DamageSourceType.Hazard);
        }
    }
}
