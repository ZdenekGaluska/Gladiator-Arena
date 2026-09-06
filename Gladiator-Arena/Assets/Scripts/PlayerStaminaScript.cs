using System;
using UnityEngine;

public class PlayerStaminaScript : MonoBehaviour
{
    public float MaxStamina = 100f;
    private float currentStamina = 100f;

    public float StaminaRegenRate = 5f;
    private float CurrentRegenRate = 0f;
    public float StaminaRecover = 10f;
    
    private bool _fullStamina = true;
    public bool FullStamina => _fullStamina;
    
    public UIStaminaScript _uiStaminaScript;

    void Start()
    {
        _uiStaminaScript.UpdateStaminaText(currentStamina, MaxStamina);
    }
    
    public bool SpendStamina(float amount)
    {
        _fullStamina = false;
        if (amount <= currentStamina)
        {
            currentStamina -= amount;
            _uiStaminaScript.UpdateStaminaText(currentStamina, MaxStamina);
            return true;
        }
        else return false;
    }

    private void FixedUpdate()
    {
        RegenStamina();
    }

    [ContextMenu("Test Not Enough Stamina")]
    public void NotEnoughStamina()
    {
        _uiStaminaScript.ShowNotEnoughStamina();
    }

    public void RegenStamina()
    {
        CurrentRegenRate += Time.fixedDeltaTime;
        if (CurrentRegenRate >= StaminaRegenRate)
        {
            currentStamina += StaminaRecover;
            if (currentStamina >= MaxStamina)
            {
                currentStamina = MaxStamina;
                _fullStamina = true;
            }
            _uiStaminaScript .UpdateStaminaText(currentStamina, MaxStamina);
        }
        
    }
}
