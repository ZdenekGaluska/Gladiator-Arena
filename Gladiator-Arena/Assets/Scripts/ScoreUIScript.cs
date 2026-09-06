using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ScoreUIScript : MonoBehaviour
{
    private float _score = 0;
    private TextMeshProUGUI _scoreText;
    private PlayerHealthScript _playerHealthScript;

    void Start()
    {
        _playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        _scoreText = GetComponent<TextMeshProUGUI>();
        _scoreText.text = "Score: " + (int)_score;
    }

    void FixedUpdate()
    {
        if (!_playerHealthScript.IsAlive)  return;
        _score += Time.fixedDeltaTime;
        UpdateScore();
    }

    void UpdateScore()
    {   
        _scoreText.text = "Score: " + (int)_score;
    }
}
