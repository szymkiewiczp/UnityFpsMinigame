using UnityEngine;

[RequireComponent(typeof(CharacterControllerMovement))]
public class KeyboardMovement : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float jumpSpeed = 0.5f;
    private CharacterControllerMovement _controller;

    void Start()
    {
        _controller = GetComponent<CharacterControllerMovement>();
    }

    void Update()
    {
        int forward = 0, right = 0;

        if (Input.GetKey(KeyCode.W))
            forward += 1;
        if (Input.GetKey(KeyCode.S))
            forward -= 1;
        if (Input.GetKey(KeyCode.A))
            right -= 1;
        if (Input.GetKey(KeyCode.D))
            right += 1;

        Vector3 globalDirection = gameObject.transform.TransformDirection(new Vector3(right, 0f, forward));

        _controller.TryMove(globalDirection.normalized * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            _controller.TryJump(jumpSpeed);
    }
}
