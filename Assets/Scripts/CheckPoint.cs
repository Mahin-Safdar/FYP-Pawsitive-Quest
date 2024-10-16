using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LifeManager lifeManager;

    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the respawn position in LifeManager
            lifeManager.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint reached! Position: " + transform.position);
        }
    }
}
