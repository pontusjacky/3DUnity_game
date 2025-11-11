using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float runSpeed = 6.5f;
    public float rotationSmoothTime = 0.12f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public Animator animator; // assign in inspector

    CharacterController controller;
    float turnSmoothVelocity;
    Vector3 velocity; // vertical velocity for gravity
    bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main) cameraTransform = Camera.main.transform;
        if (animator == null) animator = GetComponent<Animator>();
         
        if (FindObjectsOfType<PlayerMovement>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        // --- Input ---
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or left/right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S or up/down
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;


        // --- Movement in camera-relative space ---
        if (inputDir.magnitude >= 0.01f)
        {
            // calculate target angle relative to camera
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // choose speed (hold Left Shift to run)
            float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // animator -- 使用 Speed 參數驅動
            float moveAmount = new Vector2(horizontal, vertical).magnitude;
            animator?.SetFloat("Speed", moveAmount * (Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f));
        }
        else
        {
            // no input -> set Speed to 0
            animator?.SetFloat("Speed", 0f);
        }

        // --- Gravity (basic) ---
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f) velocity.y = -2f; // small downward to keep grounded
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
