
// using UnityEngine;

// public class TeleportToMainMap : MonoBehaviour
// {
//     [Header("Target Scene Settings")]
//     public string sceneName = "MainMapScene";            // Target scene
//     public string spawnPointName = "PlayerSpawn_Main";   // Target spawn in the scene
//     public float delayBeforeEnable = 0.1f;              // Delay before trigger becomes active

//     private bool canTrigger = false;

//     private void Start()
//     {
//         Invoke(nameof(EnableTrigger), delayBeforeEnable);
//     }

//     private void EnableTrigger()
//     {
//         canTrigger = true;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (!canTrigger) return;
//         if (!other.CompareTag("Player")) return;

//         // Use the PersistentPlayer teleport system
//         PersistentPlayer.instance.TeleportTo(sceneName, spawnPointName);
//     }
// }
using UnityEngine;

public class TeleportToMainMap : MonoBehaviour
{
    [Header("Target Scene Settings")]
    public string sceneName = "MainMapScene";
    public string spawnPointName = "PlayerSpawn_Main";
    public float delayBeforeEnable = 0.1f;

    private bool canTrigger = false;

    private void Start()
    {
        Invoke(nameof(EnableTrigger), delayBeforeEnable);
    }

    private void EnableTrigger()
    {
        canTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTrigger || other == null) return;
        if (!other.CompareTag("Player")) return;

        // Just teleport the player
        PersistentPlayer.instance.TeleportTo(sceneName, spawnPointName);
        // PersistentCamera is NOT manipulated here
    }
}
