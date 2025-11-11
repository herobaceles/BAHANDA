

// using UnityEngine;
// using TMPro;

// public class MainMenuManager : MonoBehaviour
// {
//     [Header("UI Panels")]
//     public GameObject mainMenuPanel;
//     public GameObject onboardingPanel;
//     public GameObject achievementsPanel;
//     public GameObject tutorialPanel;
//     public GameObject settingsPanel; // ✅ Added new panel for settings

//     [Header("Onboarding Inputs")]
//     public TMP_InputField nameInput;
//     public TMP_InputField ageInput;

//     [Header("Gender Selection")]
//     public string selectedGender = ""; //  Stores "Boy" or "Girl"

//     void Start()
//     {
//         PlayerPrefs.DeleteAll();
//         // Show Main Menu first
//         ShowMainMenu();
//     }

//     // ===== MAIN MENU BUTTONS =====
//     public void OnStartButtonPressed()
//     {
//         if (PlayerPrefs.HasKey("PlayerName"))
//         {
//             Debug.Log("Returning player. Stay in main menu.");
//         }
//         else
//         {
//             onboardingPanel.SetActive(true);
//             mainMenuPanel.SetActive(false);
//         }
//     }

//     public void OnAchievementsButtonPressed()
//     {
//         achievementsPanel.SetActive(true);
//         mainMenuPanel.SetActive(false);
//     }

//     public void OnTutorialButtonPressed()
//     {
//         tutorialPanel.SetActive(true);
//         mainMenuPanel.SetActive(false);
//     }

//     // ✅ NEW: Settings button
//     public void OnSettingsButtonPressed()
//     {
//         settingsPanel.SetActive(true);
//         mainMenuPanel.SetActive(false);
//     }

//     public void QuitGame()
//     {
//         Application.Quit();
//     }

//     // ===== GENDER SELECTION =====
//     public void SelectBoy()
//     {
//         selectedGender = "Boy";
//         Debug.Log("Selected Gender: Boy");
//     }

//     public void SelectGirl()
//     {
//         selectedGender = "Girl";
//         Debug.Log("Selected Gender: Girl");
//     }

//     // ===== ONBOARDING =====
//     public void SubmitOnboarding()
//     {
//         string playerName = nameInput.text;
//         string playerAge = ageInput.text;

//         if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(playerAge) || string.IsNullOrEmpty(selectedGender))
//         {
//             Debug.LogWarning("Please fill in all fields and select a gender!");
//             return;
//         }

//         PlayerPrefs.SetString("PlayerName", playerName);
//         PlayerPrefs.SetInt("PlayerAge", int.Parse(playerAge));
//         PlayerPrefs.SetString("PlayerGender", selectedGender);
//         PlayerPrefs.Save();

//         ShowMainMenu();

//         Debug.Log($"Welcome {playerName}, age {playerAge}, gender {selectedGender}!");
//     }

//     // ===== NAVIGATION =====
//     public void ShowMainMenu()
//     {
//         mainMenuPanel.SetActive(true);
//         onboardingPanel.SetActive(false);
//         achievementsPanel.SetActive(false);
//         tutorialPanel.SetActive(false);
//         if (settingsPanel != null) settingsPanel.SetActive(false); // ✅ hides settings when showing main menu
//     }

//     public void BackToMainMenu()
//     {
//         ShowMainMenu();
//     }
// }

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed for scene loading

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject onboardingPanel;
    public GameObject achievementsPanel;
    public GameObject tutorialPanel;
    public GameObject settingsPanel;

    [Header("Onboarding Inputs")]
    public TMP_InputField nameInput;
    public TMP_InputField ageInput;

    [Header("Gender Selection")]
    public string selectedGender = ""; // Stores "Boy" or "Girl"

    void Start()
    {
        PlayerPrefs.DeleteAll();
        ShowMainMenu();
    }

    // ===== MAIN MENU BUTTONS =====
    public void OnStartButtonPressed()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            Debug.Log("Returning player. Stay in main menu.");
        }
        else
        {
            onboardingPanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        }
    }

    public void OnAchievementsButtonPressed()
    {
        achievementsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void OnTutorialButtonPressed()
    {
        tutorialPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void OnSettingsButtonPressed()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // ===== GENDER SELECTION =====
    public void SelectBoy()
    {
        selectedGender = "Boy";
        Debug.Log("Selected Gender: Boy");
    }

    public void SelectGirl()
    {
        selectedGender = "Girl";
        Debug.Log("Selected Gender: Girl");
    }

    // ===== ONBOARDING =====
    public void SubmitOnboarding()
{
    string playerName = nameInput.text;
    string playerAge = ageInput.text;

    if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(playerAge) || string.IsNullOrEmpty(selectedGender))
    {
        Debug.LogWarning("Please fill in all fields and select a gender!");
        return;
    }

    PlayerPrefs.SetString("PlayerName", playerName);
    PlayerPrefs.SetInt("PlayerAge", int.Parse(playerAge));
    PlayerPrefs.SetString("PlayerGender", selectedGender);
    PlayerPrefs.Save();

    Debug.Log($"Welcome {playerName}, age {playerAge}, gender {selectedGender}!");

    // ✅ Load the StoryModeScene
    SceneManager.LoadScene("StoryModeScene");
}

    // ===== NAVIGATION =====
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        onboardingPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        ShowMainMenu();
    }
}
