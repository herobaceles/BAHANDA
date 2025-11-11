
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCamera : MonoBehaviour
{
    [Header("Player")]
    public Transform playerContainer;

    [Header("Offsets")]
    public Vector3 mainMapOffset = new Vector3(0, 5, -10);
    public Vector3 interiorOffset = new Vector3(0, 12, -12);

    [Header("Rotation")]
    public Quaternion fixedRotation = Quaternion.Euler(30f, 45f, 0f);

    [Header("Smooth Follow")]
    public float smoothSpeed = 5f;

    [Header("Interior Scene Name")]
    public string interiorSceneName = "HouseInterior";

    private Vector3 velocity;
    private bool forceSnap = true;

    private static SceneCamera instance;

    private void Awake()
    {
        // Singleton pattern: destroy duplicate cameras
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Assign player reference if exists
        if (PersistentPlayer.instance != null)
            playerContainer = PersistentPlayer.instance.transform;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Only remove if this is the active instance
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Ensure playerContainer is assigned in case Awake ran before player was ready
        if (playerContainer == null && PersistentPlayer.instance != null)
            playerContainer = PersistentPlayer.instance.transform;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign player reference
        if (PersistentPlayer.instance != null)
            playerContainer = PersistentPlayer.instance.transform;

        // Force snap to correct position next frame
        forceSnap = true;
        velocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        if (playerContainer == null) return;

        // Choose offset based on scene
        bool isInterior = SceneManager.GetActiveScene().name == interiorSceneName;
        Vector3 targetPos = playerContainer.position + (isInterior ? interiorOffset : mainMapOffset);

        if (forceSnap)
        {
            transform.position = targetPos;
            transform.rotation = fixedRotation;
            velocity = Vector3.zero;
            forceSnap = false;
            return;
        }

        float smoothTime = 1f / Mathf.Max(0.0001f, smoothSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        transform.rotation = fixedRotation;
    }
}
