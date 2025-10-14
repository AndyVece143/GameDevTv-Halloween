using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    //public GameObject background;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        //background.transform.position = new Vector3(transform.position.x, transform.position.y, background.transform.position.z);
    }
}
