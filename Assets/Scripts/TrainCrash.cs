using UnityEngine;

public class TrainCrash : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TrainWaypointMovement train;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Crash();
        }
    }

    void Crash()
    {
        train.enabled = false;
        gameOverCanvas.SetActive(true);
    }
}