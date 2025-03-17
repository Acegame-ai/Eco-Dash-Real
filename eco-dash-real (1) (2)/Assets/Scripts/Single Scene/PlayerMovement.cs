using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Rows; // List of row positions
    private int RowCount, PlayerRow;
    public float SwipeSpeed = 0.1f; // Time buffer between row changes
    public float JumpSpeed = 2f; // Adjusted Jump Speed
    public float AirToFloor = 0.1f; // Time to the floor from where the player is in seconds
    private Vector3 originalSize; // Store the original size of the player
    private Vector3 initialPosition; // Store the initial position of the player
    private bool canMove = true;
    public bool isinAir = false;
    public bool canJump = false; // Flag to control jumping
    private int groundContactCount = 0; // Counter for ground contacts
    private Rigidbody rb; // Reference to the player's Rigidbody

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool stopTouch = false;
    public float swipeRange = 50; // Minimum swipe distance to detect
    public float tapRange = 10; // Maximum distance for a tap to be detected
    private Coroutine currentMoveCoroutine = null; // Reference to the current movement coroutine

    // Variables for variable gravity
    public float lowJumpMultiplier = 2f;  // For a snappier upward jump
    public float fallMultiplier = 2.5f;   // For faster falling


    Magnet magnet;
    private void Start()
    {
        
        PlayerRow = 0; // Assuming starting at the first row
        RowCount = Rows.Count;
        rb = Player.GetComponent<Rigidbody>();
        originalSize = Player.transform.localScale; // Store the original size
        initialPosition = Player.transform.position; // Store the initial position

        // Add listener for the game restart event
        EventManager.Instance.onGameRestart.AddListener(ResetPlayer);
    }

    private void OnDestroy()
    {
        // Remove listener when the object is destroyed to prevent memory leaks
        EventManager.Instance.onGameRestart.RemoveListener(ResetPlayer);
    }

    private void Update()
    {
        // Only execute movement logic if the game is running
        if (GameManager.Instance.currentState == GameState.Running)
        {
            HandleRowMovement();
            HandleJump();
            DetectSwipe();
        }
    }

    private void FixedUpdate()
    {
        // Apply variable jump gravity only if the game is running
        if (GameManager.Instance.currentState == GameState.Running)
        {
            ApplyVariableJumpGravity();
        }
    }

    private void HandleRowMovement()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.D) && PlayerRow > 0)
            {
                StartMoveToRow(PlayerRow - 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) && PlayerRow < RowCount - 1)
            {
                StartMoveToRow(PlayerRow + 1);
            }
        }
    }

    private void StartMoveToRow(int targetRow)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(MoveToRow(targetRow));
    }

    private IEnumerator MoveToRow(int targetRow)
    {
        canMove = false;
        Vector3 targetPosition = new Vector3(Rows[targetRow].transform.position.x, Player.transform.position.y, Player.transform.position.z);

        float elapsedTime = 0;
        Vector3 startingPosition = Player.transform.position;

        while (elapsedTime < SwipeSpeed)
        {
            Vector3 newPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / SwipeSpeed);
            Player.transform.position = new Vector3(newPosition.x, newPosition.y, Player.transform.position.z); // Update only the X position
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Player.transform.position = new Vector3(targetPosition.x, Player.transform.position.y, Player.transform.position.z);
        PlayerRow = targetRow;
        canMove = true;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpSpeed, rb.linearVelocity.z);
            canJump = false; // Prevent further jumps until grounded again
            isinAir = true;
        }
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                stopTouch = false;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = touch.position;
                Vector2 distance = currentTouchPosition - startTouchPosition;

                if (!stopTouch)
                {
                    if (distance.x < -swipeRange) // Swiping left moves right
                    {
                        if (PlayerRow < RowCount - 1)
                        {
                            StartMoveToRow(PlayerRow + 1);
                        }
                        stopTouch = true;
                    }
                    else if (distance.x > swipeRange) // Swiping right moves left
                    {
                        if (PlayerRow > 0)
                        {
                            StartMoveToRow(PlayerRow - 1);
                        }
                        stopTouch = true;
                    }
                    else if (distance.y > swipeRange) // Swiping up to jump
                    {
                        if (canJump)
                        {
                            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpSpeed, rb.linearVelocity.z);
                            canJump = false; // Prevent further jumps until grounded again
                            isinAir = true;
                        }
                        stopTouch = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                stopTouch = false;
            }
        }
    }

    private void ResetPlayer()
    {
        // Reset the player's position, size, and state
        transform.position = initialPosition;
        PlayerRow = 0; // Reset to the initial row
        canMove = true;
        canJump = true;
        isinAir = false;
        rb.linearVelocity = Vector3.zero; // Reset velocity
    }

    public void IncrementGroundContactCount()
    {
        groundContactCount++;
        if (groundContactCount > 0)
        {
            canJump = true;
            isinAir = false;
        }
    }

    public void DecrementGroundContactCount()
    {
        groundContactCount--;
        if (groundContactCount <= 0)
        {
            canJump = false;
            isinAir = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            IncrementGroundContactCount();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            DecrementGroundContactCount();
        }
    }

    private void ApplyVariableJumpGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
