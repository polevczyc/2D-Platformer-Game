using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 5.0f; // moving speed of the player
    [Range(0.01f, 20.0f)][SerializeField] private float jumpForce = 6.5f; // jump force of the player
    [Space(10)]
    public LayerMask groundLayer;
    private Rigidbody2D rigidBody;
    private Animator animator;
    const float rayLength = 1.5f;
    private bool isFacingRight = true;
    private Vector2 startPosition;
    int keysFound = 0;
/*    int lives = 0;
    const int keysNumber = 3;*/

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -(theScale.x);
        transform.localScale = theScale;
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    private void Awake() {
        startPosition = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    void Jump()
    {
        if (isGrounded() == true)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump");
        }
    }

    private void Death()
    {
        transform.position = startPosition;
        GameManager.instance.RemoveLive();
        if (GameManager.instance.GetLives() > 0)
        {

        }
        else
        {
            GameManager.instance.GameOver();
            Debug.Log("Game Over");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool isWalking = false;
    // Update is called once per frame
    void Update() 
    {
        if (GameManager.instance.currentGameState == GameState.GS_GAME)
        {
            isWalking = false;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isWalking = true;
                if (isFacingRight == false)
                {
                    Flip();
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate((moveSpeed * Time.deltaTime) * -1, 0.0f, 0.0f, Space.World);
                isWalking = true;
                if (isFacingRight == true)
                {
                    Flip();
                }
            }
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            // Debug.DrawRay(transform.position, ((rayLength * Vector3.down)*3/4), Color.white, 1, false);
            animator.SetBool("isGrounded", isGrounded());
            animator.SetBool("isWalking", isWalking);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);
        }
        if (other.CompareTag("Bonus"))
        {
            GameManager.instance.AddPoints(1);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("FallLevel"))
        {
            Death();
        }
        if (other.CompareTag("Key"))
        {
            GameManager.instance.AddKeys(other.GetComponent<SpriteRenderer>().color);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Heart"))
        {
            //GameManager.instance.AddLive();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Finish"))
        {
            if (keysFound == 3)
            {
                Debug.Log("All 3 keys found. Congratulations!");
                SceneManager.LoadScene("finish");
            }
            else
            {
                Debug.Log("Not all keys have been found. Try again!");
                SceneManager.LoadScene("finish2");
            }
        }
        if (other.CompareTag("Enemy"))
        {
            if ((transform.position.y - 1.3) < (other.gameObject.transform.position.y))
            {
                Death();
            }
            else
            {
                //GameManager.instance.AddKilledEnemy(1);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        transform.SetParent(null);
    }
}
