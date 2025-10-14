using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.FindAnyObjectByType(typeof(GameManager)) as GameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameManager.GainCoin();
            Destroy(gameObject);
        }
    }
}
