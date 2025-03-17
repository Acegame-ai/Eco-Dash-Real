using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public string sceneToLoad;   // The name of the next scene (Home Scene)
    public Slider progressBar;   // Reference to the progress bar (UI Slider)
    public float loadingDuration = 5f; // Total time to show the loading screen (in seconds)

    void Start()
    {
        // Start the loading process as soon as the scene starts
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Start loading the home scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Prevent auto-switch to the new scene

        // Initial progress
        float startTime = Time.time;

        while (!operation.isDone)
        {
            // Calculate the time elapsed
            float elapsedTime = Time.time - startTime;
            // Calculate progress based on the elapsed time and loading duration
            float progress = Mathf.Clamp01(elapsedTime / loadingDuration);
            
            // Update the slider's value to reflect the progress
            progressBar.value = progress;

            // Once the progress reaches near 100% (1.0), allow the scene to activate
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(3f); // Optional delay before scene activation
                operation.allowSceneActivation = true; // Allow the scene to activate
            }

            yield return null; // Wait for the next frame before continuing the loop
        }
    }
}

