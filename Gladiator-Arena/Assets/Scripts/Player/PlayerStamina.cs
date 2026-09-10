using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStamina : MonoBehaviour
{
    public float MaxStamina = 100f;
    private float currentStamina = 100f;

    public float StaminaRegenRate = 5f;
    private float CurrentRegenRate = 0f;
    public float StaminaRecover = 10f;
    
    private bool _fullStamina = true;
    public bool FullStamina => _fullStamina;
    
    [FormerlySerializedAs("_uiStaminaScript")] public StaminaUI staminaUI;

    void Start()
    {
        staminaUI.UpdateStaminaText(currentStamina, MaxStamina);
    }
    
    public bool SpendStamina(float amount)
    {
        _fullStamina = false;
        if (amount <= currentStamina)
        {
            currentStamina -= amount;
            staminaUI.UpdateStaminaText(currentStamina, MaxStamina);
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
        staminaUI.ShowNotEnoughStamina();
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
            staminaUI .UpdateStaminaText(currentStamina, MaxStamina);
        }
        
    }
}
