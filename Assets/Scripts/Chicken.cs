using UnityEngine;

public class Chicken : MonoBehaviour
{
    private bool isHit = false;

    void OnCollisionEnter(Collision collision)
    {
        if (isHit) return;

        if (collision.gameObject.CompareTag("Ball"))
        {
            isHit = true;

            GameManager.instance.AddScore(1);

            Destroy(gameObject);
        }
    }
}