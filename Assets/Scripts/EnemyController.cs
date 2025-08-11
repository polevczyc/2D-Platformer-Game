using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    [Range(0.01f, 20.0f)][SerializeField] public float moveRange = 1.0f;
    private Animator animator;
    private float startPositionX;
    bool isMovingRight = false;
    private bool isFacingRight = false;
    IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ((transform.position.y + 1.3) <= (other.gameObject.transform.position.y)))
        {
            animator.SetBool("isDead", true);
            StartCoroutine(KillOnAnimationEnd());

        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = -(theScale.x);
        transform.localScale = theScale;
    }
    private void Awake() {

        animator = GetComponent<Animator>();
        startPositionX = this.transform.position.x;
    }

    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
    private void MoveLeft()
    {
        transform.Translate(moveSpeed * Time.deltaTime*-1, 0.0f, 0.0f, Space.World);
    }
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight == true)
        {
            if (this.transform.position.x <= (startPositionX + moveRange))
            {
                MoveRight();
            } else
            {
                isMovingRight = false;
                Flip();
                MoveLeft();
            }
        } else
        {
            if (this.transform.position.x >= (startPositionX - moveRange))
            {
                MoveLeft();
            } else
            {
                isMovingRight = true;
                Flip();
                MoveRight();
            }
        }
    }
}
