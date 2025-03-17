using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        public void PlayBtn()
    {
        SceneManager.LoadScene("Running"); // Use SceneManager directly
    }


     public void PlayBtn2()
    {
        SceneManager.LoadScene("Splash Screen"); // Use SceneManager directly
    }
}
