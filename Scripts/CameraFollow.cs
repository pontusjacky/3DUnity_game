using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3.5f, -6f);
    public float smoothTime = 0.12f;
    public float rotationSpeed = 3.0f;
    public float zoomSpeed = 2.0f;
    public float minDistance = 2f;
    public float maxDistance = 12f;

    Vector3 currentVelocity;
    float distance;
    float yaw;

    void Start()
    {
        if (target == null) return;
        distance = offset.magnitude;
        yaw = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed * 10f, minDistance, maxDistance);

        Quaternion rot = Quaternion.Euler(0f, yaw, 0f);
        Vector3 dir = rot * Vector3.forward;
        Vector3 desiredPos = target.position - dir * distance + Vector3.up * offset.y;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref currentVelocity, smoothTime);
        transform.LookAt(target.position + Vector3.up * 1.6f);
        // Debug.Log($"target.position: {target.position}");
    }
}
