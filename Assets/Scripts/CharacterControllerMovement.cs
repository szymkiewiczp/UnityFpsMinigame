using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerMovement : MonoBehaviour
{
    public float gravity = 1f;
    private CharacterController _controller;
    private float _verticalSpeed = 0f;
    private Vector3 _requestedMovement = Vector3.zero;

    void Start()
    {
        _controller = GetComponent<CharacterController>();    
    }

    void Update()
    {
        Vector3 verticalMovement = new Vector3(0f, _verticalSpeed * Time.deltaTime, 0f);
        _controller.Move(_requestedMovement + verticalMovement);

        if (_controller.isGrounded)
            _verticalSpeed = 0f;

        _verticalSpeed -= gravity * Time.deltaTime;

        _requestedMovement = Vector3.zero;
    }

    public void TryJump(float speed)
    {
        if (!_controller.isGrounded)
            return;

        _verticalSpeed = speed;
    }

    public void TryMove(Vector3 velocity)
    {
        _requestedMovement += velocity;
    }
}
