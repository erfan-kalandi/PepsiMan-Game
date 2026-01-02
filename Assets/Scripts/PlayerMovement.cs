using UnityEngine;

/*
 * This script controls the player's movement, including constant forward velocity,
 * lane-switching logic (Left, Center, Right), and collision detection with obstacles
 * to trigger the Game Over state.
 */
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    [Header("Settings")]
    public float forwardSpeed = 10f;
    public float laneDistance = 9f;
    public float sideSpeed = 30f;

    private int desiredLane = 1;
    private float initialY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        initialY = transform.position.y;
        Time.timeScale = 1;
    }

    void Update()
    {
        direction.z = forwardSpeed;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;
        }

        float targetX = 0;
        if (desiredLane == 0) targetX = -laneDistance;
        else if (desiredLane == 2) targetX = laneDistance;

        float diffX = targetX - transform.position.x;

        if (Mathf.Abs(diffX) > 0.1f)
        {
            direction.x = Mathf.Sign(diffX) * sideSpeed;
        }
        else
        {
            direction.x = 0;
            Vector3 pos = transform.position;
            pos.x = targetX;
            transform.position = pos;
        }

        if (controller.isGrounded)
            direction.y = -1f;
        else
            direction.y = -15f;

        controller.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Time.timeScale = 0;

            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null)
            {
                ui.ShowGameOverPanel();
            }
        }
    }
}