// using UnityEngine;
// using UnityEngine.SceneManagement;

// [RequireComponent(typeof(CharacterController))]
// [RequireComponent(typeof(Rigidbody))]
// public class PersistentPlayer : MonoBehaviour
// {
//     public static PersistentPlayer instance;

//     [HideInInspector] public string targetScene;
//     [HideInInspector] public string targetSpawn;

//     private CharacterController cc;
//     private Rigidbody rb;

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject);

//             cc = GetComponent<CharacterController>();
//             rb = GetComponent<Rigidbody>();

//             SceneManager.sceneLoaded += OnSceneLoaded;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void TeleportTo(string sceneName, string spawnName)
//     {
//         targetScene = sceneName;
//         targetSpawn = spawnName;
//         SceneManager.LoadScene(sceneName);
//     }

//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         if (scene.name != targetScene) return;

//         Transform spawn = FindDeepChild(scene.GetRootGameObjects(), targetSpawn);

//         if (cc != null) cc.enabled = false;

//         if (spawn != null)
//         {
//             transform.position = spawn.position;
//             transform.rotation = spawn.rotation;
//         }
//         else
//         {
//             Debug.LogWarning($"Spawn point '{targetSpawn}' not found in scene '{scene.name}'. Moving to Vector3.zero.");
//             transform.position = Vector3.zero;
//             transform.rotation = Quaternion.identity;
//         }

//         if (cc != null) cc.enabled = true;

//         if (rb != null && !rb.isKinematic)
//         {
//             rb.velocity = Vector3.zero;
//             rb.angularVelocity = Vector3.zero;
//         }

//         targetScene = null;
//         targetSpawn = null;
//     }

//     private Transform FindDeepChild(GameObject[] roots, string name)
//     {
//         foreach (GameObject root in roots)
//         {
//             if (root.name == name) return root.transform;

//             Transform t = FindRecursive(root.transform, name);
//             if (t != null) return t;
//         }
//         return null;
//     }

//     private Transform FindRecursive(Transform parent, string name)
//     {
//         foreach (Transform child in parent)
//         {
//             if (child.name == name) return child;

//             Transform t = FindRecursive(child.transform, name);
//             if (t != null) return t;
//         }
//         return null;
//     }

//     private void OnDestroy()
//     {
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }
// }


using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PersistentPlayer : MonoBehaviour
{
    private static PersistentPlayer _instance;
    public static PersistentPlayer instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PersistentPlayer>();
            return _instance;
        }
    }

    [HideInInspector] public string targetScene;
    [HideInInspector] public string targetSpawn;

    private CharacterController cc;
    private Rigidbody rb;

    public static event System.Action OnPlayerTeleported;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            cc = GetComponent<CharacterController>();
            rb = GetComponent<Rigidbody>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TeleportTo(string sceneName, string spawnName)
    {
        targetScene = sceneName;
        targetSpawn = spawnName;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != targetScene) return;
        StartCoroutine(FinalizeTeleport(scene));
    }

    private IEnumerator FinalizeTeleport(Scene scene)
    {
        yield return null; // wait one frame so scene is ready

        Transform spawn = FindDeepChild(scene.GetRootGameObjects(), targetSpawn);

        if (cc != null) cc.enabled = false;

        if (spawn != null)
        {
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }
        else
        {
            Debug.LogWarning($"Spawn '{targetSpawn}' not found, moving to (0,0,0).");
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        if (cc != null) cc.enabled = true;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return null; // one more frame for physics settle
        OnPlayerTeleported?.Invoke();

        targetScene = null;
        targetSpawn = null;
    }

    private Transform FindDeepChild(GameObject[] roots, string name)
    {
        foreach (GameObject root in roots)
        {
            if (root.name == name) return root.transform;
            Transform t = FindRecursive(root.transform, name);
            if (t != null) return t;
        }
        return null;
    }

    private Transform FindRecursive(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name) return child;
            Transform t = FindRecursive(child, name);
            if (t != null) return t;
        }
        return null;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (_instance == this) _instance = null;
    }
}
