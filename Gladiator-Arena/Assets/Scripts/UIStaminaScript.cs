using TMPro;
using UnityEngine;

public class UIStaminaScript : MonoBehaviour
{

    private TextMeshProUGUI _staminaText;
    public TextMeshProUGUI warningText;

    
    void Start()
    {
        _staminaText =  GetComponent<TextMeshProUGUI>();
    }


    public void UpdateStaminaText(float stamina, float maxStamina)
    {
        _staminaText.text = "Stamina: " + (int)stamina + "/" + (int)maxStamina;
    }

    public void ShowNotEnoughStamina()
    {
        warningText.text = "Not enough stamina";
        CancelInvoke(nameof(ClearWarning));
        Invoke(nameof(ClearWarning), 1.5f);
    }

    private void ClearWarning()
    {
        warningText.text = "";
    }
}
