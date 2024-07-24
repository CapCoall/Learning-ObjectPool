using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _difficultyButton;
    [SerializeField] private float _difficulty;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _difficultyButton=GetComponent<Button>();
        _difficultyButton.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        _gameManager.StartGame(_difficulty);
    }
}
