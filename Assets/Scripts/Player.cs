using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed, jumpForce, mouseSensitivity, sprintMultiplier, cameraMaxDistance;
    public float health;
    public GameObject pauseMenu;

    public HealthBar healthBar;
    public Transform cameraTransform, respawnTransform, bodyTransform;
    public Rigidbody playerRigidbody;

    private float cameraY;
    private bool paused;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() 
    {
        Vector2 inputVector = new Vector2(-Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical"));
        Vector2 inputMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        bool jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        if (paused) return; // No code after here runs if game is paused.

        if (jumpKeyPressed && isGrounded()) {
            playerRigidbody.AddForce(jumpForce * Vector3.up);
        }

        inputVector = inputVector.normalized;
        playerRigidbody.velocity = playerRigidbody.rotation *
            (new Vector3(inputVector.x, 0, inputVector.y) 
            * movementSpeed 
            * Mathf.Pow(sprintMultiplier, System.Convert.ToInt32(Input.GetKey(KeyCode.LeftShift))) 
            + playerRigidbody.velocity.y * Vector3.up);

        bodyTransform.RotateAround(bodyTransform.position, Vector3.up, inputMouse.x * mouseSensitivity * Time.deltaTime);

        // Camera rotation/movement

        cameraY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        cameraY = Mathf.Clamp(cameraY, -90f, 90f);

        Ray ray = new Ray(playerRigidbody.position, Quaternion.AngleAxis(cameraY, bodyTransform.right) * bodyTransform.forward);

        float distance = cameraMaxDistance;

        if (Physics.Raycast(ray, out RaycastHit rayHit, cameraMaxDistance, ~(1 << 8))) {
            distance = rayHit.distance - 0.1f;
        }

        Vector3 cameraTargetPosition = playerRigidbody.position + ray.direction * distance;

        cameraTransform.position = cameraTargetPosition;
        cameraTransform.LookAt(bodyTransform);
    }

    private bool isGrounded() {
        return Physics.Raycast(bodyTransform.position, Vector3.down, 0.6f);
    }

    private void Die() {
        health = 100;
        healthBar.SetHealth(health);
        playerRigidbody.position = respawnTransform.position;
    }

    public void DealDamage(float damage) {
        health -= damage;
        healthBar.SetHealth(health);

        if (health < 0) {
            Die();
        }
    }

    public void TogglePause() {
        paused = !paused;

        if (paused) {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Debug.Log("Game Paused");
            return;
        }

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Debug.Log("Game Resumed");
    }
}