using UnityEngine;

public class Tetris_TouchControls : MonoBehaviour
{
    public Tetris_GameController tetrisController;

    private bool isDragging = false;
    private Vector2 touchStartPos;
    private float dragThreshold = 30f;
    private float fastDropDuration = 0.1f;
    private float fastDropTimer = 0f;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    private bool doRotation = false;
    private float previousToRight = 0f;
    private float previousToLeft = 0f;

    void Update()
    {
        HandleTouchControls();
    }

    void HandleTouchControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                doRotation = true;
                fastDropTimer = Time.time;
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                isDragging = false;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Calculate drag distance
                float touchDeltaX = touch.position.x - touchStartPos.x;
                float touchDeltaY = touch.position.y - touchStartPos.y;
                float dragDistance = Mathf.Sqrt(touchDeltaX * touchDeltaX + touchDeltaY * touchDeltaY);
                if (!isDragging && dragDistance >= dragThreshold)
                {
                    isDragging = true;
                    doRotation = false;
                    touchStartPos = touch.position;
                }

                if (isDragging)
                {
                    Vector2 dragDelta = touch.position - touchStartPos;
                    if (Mathf.Abs(dragDelta.x) > dragThreshold)
                    {
                        if (dragDelta.x > 0 && !isMovingLeft && Time.time - previousToRight > 0.1f)
                        {
                            tetrisController.MoveRight();
                            isMovingRight = true;
                            isMovingLeft = false;
                            previousToRight = Time.time;
                        }
                        else if (dragDelta.x < 0 && !isMovingRight && Time.time - previousToLeft > 0.1f)
                        {
                            tetrisController.MoveLeft();
                            isMovingLeft = true;
                            isMovingRight = false;
                            previousToLeft = Time.time;
                        }
                        //isDragging = false;
                    }
                    else
                    {
                        isMovingLeft = false;
                        isMovingRight = false;
                    }
                
                    if (dragDelta.y < -5 && !isMovingRight && !isMovingLeft)
                    {
                        if (dragDelta.y < -100f)
                        {
                            tetrisController.InstantDrop();
                        }
                        else if (Time.time - fastDropTimer > fastDropDuration && !isMovingRight && !isMovingLeft)
                        {
                            tetrisController.MoveDown();
                            fastDropTimer = Time.time;
                        }
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (doRotation && !isDragging && !isMovingLeft && !isMovingRight)
                {
                    tetrisController.Rotate();
                }

                isDragging = false;
                fastDropTimer = 0f;
                isMovingLeft = false;
                isMovingRight = false;
            }
        }
        else
        {
            isDragging = false;
            fastDropTimer = 0f;
            isMovingLeft = false;
            isMovingRight = false;
        }
    }
}
