using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.scene == gameObject.scene)
        {
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;

            // If player has Rigidbody or CharacterController, reset velocity
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                cc.enabled = true;
            }
        }
    }
}
