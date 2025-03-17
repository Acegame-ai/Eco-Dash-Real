using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    [Header("Score Settings")]
    public float scoreMultiplier = 10f; // Score increases based on distance covered

    private int currentScore = 0;
    private int totalCoins = 0;
    private bool isGameOver = false;
    private Transform player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        if (player != null)
        {
            currentScore = Mathf.FloorToInt(player.position.z * scoreMultiplier);
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void AddCoin()
    {
        totalCoins++;
        coinText.text = "Coins: " + totalCoins.ToString();
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    internal void ResetScore()
    {
        throw new NotImplementedException();
    }
}
