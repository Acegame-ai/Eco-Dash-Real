// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SceneLoader : MonoBehaviour
// {
//     public static SceneLoader Instance;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//             SceneManager.sceneLoaded += OnSceneLoaded;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         GameManager.Instance?.ReinitializeAllReferences();
//     }

//     public void LoadScene(string sceneName)
//     {
//         SceneManager.LoadScene(sceneName);
//     }
// }
