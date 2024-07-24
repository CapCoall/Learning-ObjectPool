using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private bool _isGameActive;

    private int _score;
    private float _spawnRate = 1.0f;
    private ObjectPool _barrelSpawner;

    private void Awake()
    {
        _barrelSpawner = GetComponent<ObjectPool>();
    }

    public void StartGame(float difficulty)
    {
        _spawnRate /= difficulty;
        _isGameActive = true;
        _score = 0;

        UpdateScore(0);
        _titleScreen.SetActive(false);
        StartCoroutine(ThrowBarrel());
    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _isGameActive = false;
    }

    public void UpdateScore(int _scoreToAdd)
    {
        _score += _scoreToAdd;
        _scoreText.text = "Score:" + _score;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ThrowBarrel()
    {
        while (_isGameActive) 
        {
            yield return new WaitForSeconds(_spawnRate);

            if (_isGameActive)
            {
                _barrelSpawner._pool.Get();
            }
        }
    }
}
