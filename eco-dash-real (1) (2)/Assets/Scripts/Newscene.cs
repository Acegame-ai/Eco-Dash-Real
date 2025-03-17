
using UnityEngine;
using UnityEngine.SceneManagement;

public class Newscence : MonoBehaviour {

    public int _sceneNumber = 2;
    public int _sceneNumber1 = 0;
    public void StartGame(int _sceneNumber)
    {

        Time.timeScale = 1f; // Ensure the game is not frozen
        SceneManager.LoadScene(_sceneNumber);
    } 
    public void Home(int _sceneNumber1)
    {

        Time.timeScale = 1f; // Ensure the game is not frozen
        SceneManager.LoadScene(_sceneNumber1);
    } 
}
