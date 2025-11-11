using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PersistentUI : MonoBehaviour
{
    public static PersistentUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reparent to the Canvas in the new scene
        Canvas sceneCanvas = FindObjectOfType<Canvas>();
        if (sceneCanvas != null)
        {
            transform.SetParent(sceneCanvas.transform, false);
        }

        ResetJoystickInput();
    }

    /// <summary>
    /// Reset all joystick inputs without moving handles
    /// </summary>
    private void ResetJoystickInput()
    {
        var joysticks = GetComponentsInChildren<UnityEngine.UI.Image>(); // or your joystick component
        foreach (var joystick in joysticks)
        {
            // Simulate pointer release to reset input
            var pointer = new UnityEngine.EventSystems.PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(joystick.gameObject, pointer, ExecuteEvents.pointerUpHandler);
        }

        // Clear EventSystem selection
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
