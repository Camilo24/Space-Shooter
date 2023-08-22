using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Main_Menu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMultiPlayer()
    {
        SceneManager.LoadScene(2);
    }
}
