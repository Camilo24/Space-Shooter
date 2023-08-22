using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText, bestText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _LiveImg;
    [SerializeField] private Text _gameOver;
    [SerializeField] private Text _restartText;
    [SerializeField] private int maxscore;
    private GameManager _gameManager;

    void Start()
    {
        //PlayerPrefs.SetInt("HighScore", 0);
        maxscore = PlayerPrefs.GetInt("HighScore", 0);
        bestText.text = "Best: " + maxscore;
        _gameOver.gameObject.SetActive(false); 
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerscore)
    {
        _scoreText.text = "Score: " + playerscore.ToString();
    }

    public void BestScore(int currentscore)
    {
        PlayerPrefs.GetInt(bestText.text);

        if (maxscore < currentscore)
        {
            maxscore = currentscore;
            PlayerPrefs.SetInt("HighScore", maxscore);
        }

        bestText.text = "Best: " + maxscore.ToString();
    }

    public void CheckForBestScore()
    {
        BestScore(maxscore);
    }

    public void UpdateLives(int currentLives)
    {
        _LiveImg.sprite = _liveSprites[currentLives];

        if (currentLives is 0)
        {
            gos();
        }
    }

    public void gos()
    {
        _gameManager.GameOver();
        _gameOver.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(gofRoutine());
    }

    IEnumerator gofRoutine()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
