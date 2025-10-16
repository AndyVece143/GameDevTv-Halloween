using System.Collections;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

    public bool falling;
    public bool grounded;
    public bool respawn;
    private GameManager gameManager;

    public Animator anim;

    public bool canMove = true;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        gameManager = GameManager.FindAnyObjectByType(typeof(GameManager)) as GameManager;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
        }
        
    }

    private void Movement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Debug.Log("Jump");
            Jump();
        }

        if (body.linearVelocity.y < 0)
        {
            falling = true;
        }
        else if (body.linearVelocity.y >= 0)
        {
            falling = false;
        }

        //Animation
        anim.SetBool("move", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("falling", falling);
    }

    public void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
        anim.SetTrigger("jump");
        SoundManager.instance.PlaySound(jumpSound);
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Danger" || collision.tag == "Enemy")
        {
            canMove = false;
            body.linearVelocity = new Vector2(0, 0);
            Death();
            //Respawn();
        }
    }

    private void Death()
    {
        canMove = false;
        body.gravityScale = 0;
        boxCollider.enabled = false;
        anim.SetBool("falling", false);
        anim.SetTrigger("dead");
        SoundManager.instance.PlaySound(deathSound);
        StartCoroutine(waiterDeath(1f));
    }

    private void Respawn(SpriteRenderer renderer, Color color)
    {
        //renderer.color = color;
        renderer.color = new Color32(255, 255, 255, 255);
        boxCollider.enabled = true;
        if (gameManager.activeCheckpoint == null)
        {
            body.transform.position = new Vector3(0, 0, 0);
            canMove = true;
            body.gravityScale = 2;
        }
        else
        {
            canMove = false;
            body.gravityScale = 0;
            anim.SetBool("falling", false);
            anim.SetTrigger("respawn");
            anim.Play("respawn");
            Vector3 respawnPoint = gameManager.activeCheckpoint.transform.position;
            body.transform.position = respawnPoint;
            StartCoroutine(waiterRespawn());
        }
    }

    IEnumerator waiterRespawn()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        body.gravityScale = 2;
    }

    IEnumerator waiterDeath(float duration)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color startColor = renderer.color;
        //Debug.Log(startColor);
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }
        Respawn(renderer, startColor);
    }
}
