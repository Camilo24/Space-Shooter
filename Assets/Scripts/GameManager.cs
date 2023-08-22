using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isGameOver;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private AudioSource _background;
    [SerializeField] public bool isCoopMode = false;

    private void Start()
    {
        _isGameOver = false;
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale is 1)
            {
                _background.Stop();
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _background.Play();
        _pauseMenu.SetActive(false);
    }
}
