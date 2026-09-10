using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ScoreUI : MonoBehaviour
{
    private float _score = 0;
    private TextMeshProUGUI _scoreText;
    private PlayerHealth _playerHealth;

    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        _scoreText = GetComponent<TextMeshProUGUI>();
        _scoreText.text = "Score: " + (int)_score;
    }

    void FixedUpdate()
    {
        if (!_playerHealth.IsAlive)  return;
        _score += Time.fixedDeltaTime;
        UpdateScore();
    }

    void UpdateScore()
    {   
        _scoreText.text = "Score: " + (int)_score;
    }
}
