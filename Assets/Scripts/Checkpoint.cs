using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public TMP_Text text;
    private GameManager gameManager;
    private Player player;
    private float distance;
    private bool active = false;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.FindAnyObjectByType(typeof(GameManager)) as GameManager;
        player = Player.FindAnyObjectByType<Player>();
        text.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDistance();
        text.text = "Checkpoint cost: " + gameManager.checkpointCost + ". Press P to purchase";
        anim.SetBool("active", active);
    }

    void PlayerDistance()
    {
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance <= 1 && active == false)
        {
            text.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.P))
            {
                CheckpointPayment();
            }
        }
        else
        {
            text.gameObject.SetActive(false);
        }
        //Debug.Log(distance);
    }

    void CheckpointPayment()
    {
        if (gameManager.coinAmount >= gameManager.checkpointCost)
        {
            text.gameObject.SetActive(false);
            active = true;
            gameManager.coinAmount -= gameManager.checkpointCost;
            gameManager.checkpointCost += 3;
            gameManager.activeCheckpoint = gameObject.GetComponent<Checkpoint>();
        }    

    }
}
