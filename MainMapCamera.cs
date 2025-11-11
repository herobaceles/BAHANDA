using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMapCamera : MonoBehaviour
{
    private void Awake()
    {
        // Keep only this camera active in MainMap
        Camera[] cams = FindObjectsOfType<Camera>();
        foreach (Camera c in cams)
            c.gameObject.SetActive(c == GetComponent<Camera>());
    }
}
