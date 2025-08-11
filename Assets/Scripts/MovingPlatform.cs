using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    [Range(0.01f, 20.0f)] public float moveRange = 1.0f;
    private float startPositionX;
    bool isMovingRight = false;
    private void Awake()
    {
        startPositionX = this.transform.position.x;
    }
    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
    private void MoveLeft()
    {
        transform.Translate(moveSpeed * Time.deltaTime * -1, 0.0f, 0.0f, Space.World);
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
            }
            else
            {
                isMovingRight = false;
                MoveLeft();
            }
        }
        else
        {
            if (this.transform.position.x >= (startPositionX - moveRange))
            {
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();
            }
        }
    }
}
