using UnityEngine;

public class FreeFlyCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float boostMultiplier = 2f;
    public float verticalSpeed = 5f;

    [Header("Look")]
    public float lookSensitivity = 2f;
    private float pitch = 0f;
    private float yaw = 0f;

    private bool canMove = true;
   

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            canMove = !canMove;
        }
        if (!GameState.Instance.HasSimulationStarted || !canMove) return;
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * boostMultiplier : moveSpeed;

        Vector3 move = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // Vertical movement: Q (down) / E (up)
        if (Input.GetKey(KeyCode.E)) move.y += 1;
        if (Input.GetKey(KeyCode.Q)) move.y -= 1;

        // Normalize to prevent faster diagonal speed
        move = transform.TransformDirection(move.normalized);
        transform.position += move * speed * Time.deltaTime;
    }
}
