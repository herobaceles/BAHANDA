using UnityEngine;
using TMPro;

public class StoryModeUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text ageText;
    public TMP_Text genderText;

    void Start()
    {
        nameText.text = "Name: " + PlayerPrefs.GetString("PlayerName");
        ageText.text = "Age: " + PlayerPrefs.GetInt("PlayerAge");
        genderText.text = "Gender: " + PlayerPrefs.GetString("PlayerGender");
    }
}
