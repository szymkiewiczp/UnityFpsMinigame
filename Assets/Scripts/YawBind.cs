using UnityEngine;

/// <summary>
/// Copies the referenceFrame's transform's yaw into this object's transform on every game tick
/// </summary>
public class YawBind : MonoBehaviour
{
    public GameObject referenceFrame;

    void Update()
    {
        Quaternion referenceRotation = referenceFrame.transform.rotation;

        float yaw = referenceRotation.eulerAngles.y;

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.y = yaw;

        transform.rotation = Quaternion.Euler(eulerAngles);

        referenceFrame.transform.rotation = referenceRotation;
    }
}
