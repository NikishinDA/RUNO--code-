using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Broadcast(GameEventsHandler.CoinPickUpEvent);
            Destroy(gameObject);
        }
    }
}
