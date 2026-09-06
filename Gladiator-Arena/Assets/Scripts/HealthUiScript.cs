using TMPro;
using UnityEngine;

public class HealthUiScript : MonoBehaviour
{
    private TextMeshProUGUI _healthText;

    void Start()
    {
            _healthText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int current, int max)
    {
        _healthText.text = $"{current}/{max}";
    }
}
