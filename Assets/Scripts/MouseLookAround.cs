using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    public float sensitivity = 5f;
    public float maxPitch = 90f;
    public float minPitch = -90f;

    void Update()
    {
        Vector3 eulerAngles = gameObject.transform.rotation.eulerAngles;

        float pitch = eulerAngles.x;
        if (pitch > 180f)
            pitch -= 360f;
        float yaw = eulerAngles.y;

        float pitchDelta = -Input.GetAxis("Mouse Y") * sensitivity;
        float yawDelta = Input.GetAxis("Mouse X") * sensitivity;

        pitch += pitchDelta;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        yaw += yawDelta;

        gameObject.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
