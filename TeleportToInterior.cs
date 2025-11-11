// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;

// public class TeleportToInterior : MonoBehaviour
// {
//     [Header("Scene & Spawn Settings")]
//     public string targetScene = "HouseInterior";
//     public string spawnPointName = "PlayerSpawn_Interior";
//     public float delayBeforeEnable = 0.1f;

//     private bool canTrigger = false;

//     private void Start()
//     {
//         Invoke(nameof(EnableTrigger), delayBeforeEnable);
//     }

//     private void EnableTrigger() => canTrigger = true;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (!canTrigger || other == null) return;
//         if (!other.CompareTag("Player")) return;

//         // Teleport using PersistentPlayer singleton
//         PersistentPlayer.instance.TeleportTo(targetScene, spawnPointName);

//         // If you want to use coroutine instead, uncomment below:
//         // StartCoroutine(TeleportPlayerCoroutine(other.transform.root.gameObject));
//     }

//     /// <summary>
//     /// Coroutine to teleport the player if not using PersistentPlayer singleton
//     /// </summary>
//     private IEnumerator TeleportPlayerCoroutine(GameObject player)
//     {
//         DontDestroyOnLoad(player);

//         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
//         asyncLoad.allowSceneActivation = true;

//         while (!asyncLoad.isDone)
//             yield return null;

//         yield return null; // Ensure scene is fully initialized

//         Scene newScene = SceneManager.GetSceneByName(targetScene);
//         SceneManager.MoveGameObjectToScene(player, newScene);

//         // Find spawn point in scene
//         Transform spawn = null;
//         foreach (GameObject root in newScene.GetRootGameObjects())
//         {
//             spawn = FindDeepChild(root.transform, spawnPointName);
//             if (spawn != null) break;
//         }

//         Vector3 pos = spawn != null ? spawn.position : Vector3.zero;
//         Quaternion rot = spawn != null ? spawn.rotation : Quaternion.identity;

//         // Temporarily disable CharacterController to avoid physics issues
//         CharacterController cc = player.GetComponent<CharacterController>();
//         if (cc != null) cc.enabled = false;

//         player.transform.position = pos;
//         player.transform.rotation = rot;

//         if (cc != null) cc.enabled = true;

//         // Reset Rigidbody velocities
//         Rigidbody rb = player.GetComponent<Rigidbody>();
//         if (rb != null && !rb.isKinematic)
//         {
//             rb.velocity = Vector3.zero;
//             rb.angularVelocity = Vector3.zero;
//         }

//         // Snap camera to player (interior)
//         PersistentCamera cam = FindObjectOfType<PersistentCamera>();
//         if (cam != null)
//             cam.SnapCameraToPlayer();
//     }

//     /// <summary>
//     /// Recursive search for nested spawn objects
//     /// </summary>
//     private Transform FindDeepChild(Transform parent, string name)
//     {
//         if (parent.name == name) return parent;

//         foreach (Transform child in parent)
//         {
//             Transform t = FindDeepChild(child, name);
//             if (t != null) return t;
//         }
//         return null;
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeleportToInterior : MonoBehaviour
{
    [Header("Scene & Spawn Settings")]
    public string targetScene = "HouseInterior";
    public string spawnPointName = "PlayerSpawn_Interior";
    public float delayBeforeEnable = 0.1f;

    private bool canTrigger = false;

    private void Start()
    {
        Invoke(nameof(EnableTrigger), delayBeforeEnable);
    }

    private void EnableTrigger() => canTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canTrigger || other == null) return;
        if (!other.CompareTag("Player")) return;

        // Teleport player using PersistentPlayer singleton
        PersistentPlayer.instance.TeleportTo(targetScene, spawnPointName);
        // PersistentCamera is NOT manipulated here
    }

    /// <summary>
    /// Optional coroutine if you need manual control over interior spawn
    /// </summary>
    public IEnumerator TeleportPlayerCoroutine(GameObject player)
    {
        DontDestroyOnLoad(player);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
            yield return null;

        yield return null; // ensure scene is fully initialized

        Scene newScene = SceneManager.GetSceneByName(targetScene);
        SceneManager.MoveGameObjectToScene(player, newScene);

        // Find spawn point
        Transform spawn = null;
        foreach (GameObject root in newScene.GetRootGameObjects())
        {
            spawn = FindDeepChild(root.transform, spawnPointName);
            if (spawn != null) break;
        }

        Vector3 pos = spawn != null ? spawn.position : Vector3.zero;
        Quaternion rot = spawn != null ? spawn.rotation : Quaternion.identity;

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = pos;
        player.transform.rotation = rot;

        if (cc != null) cc.enabled = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null && !rb.isKinematic)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Interior scene camera handles itself
    }

    private Transform FindDeepChild(Transform parent, string name)
    {
        if (parent.name == name) return parent;

        foreach (Transform child in parent)
        {
            Transform t = FindDeepChild(child, name);
            if (t != null) return t;
        }
        return null;
    }
}
