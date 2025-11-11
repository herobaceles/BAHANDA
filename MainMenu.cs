// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;

// public class MainMenu : MonoBehaviour
// {
//     void Update()
//     {
//         // Handle Android back button
//         if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
//         {
//             if (SceneManager.GetActiveScene().name != "MainMenuScene")
//                 SceneManager.LoadScene("MainMenuScene");
//             else
//                 Application.Quit();
//         }
//     }

//     public void StartGame()
//     {
//         // Load the HouseInterior scene
//         StartCoroutine(LoadSceneAsync("HouseInterior"));
//     }

//     public void QuitGame()
//     {
// #if UNITY_ANDROID
//         Application.Quit();
// #elif UNITY_IOS
//         // iOS apps shouldn't quit programmatically
// #endif
//     }

//     private IEnumerator LoadSceneAsync(string sceneName)
//     {
//         AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
//         op.allowSceneActivation = true;

//         while (!op.isDone)
//         {
//             yield return null;
//         }
//     }
// }



using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Ensure only MainMenuScene is active
        // Unload MainMapScene if somehow it is loaded in the background
        if (SceneManager.GetSceneByName("MainMapScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMapScene");
        }
    }

    void Update()
    {
        // Handle Android back button
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        // Load the HouseInterior scene as the only active scene
        StartCoroutine(LoadSceneAsync("HouseInterior"));
    }

    public void QuitGame()
    {
#if UNITY_ANDROID
        Application.Quit();
#elif UNITY_IOS
        // iOS apps shouldn't quit programmatically
#endif
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        op.allowSceneActivation = true;

        while (!op.isDone)
        {
            yield return null;
        }
    }
}
