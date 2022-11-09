using UnityEngine;

[RequireComponent(typeof(CharacterControllerMovement))]
public class RandomStroll : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float jumpSpeed = 5f;
    public float minWalkTime = 1f;
    public float maxWalkTime = 3f;
    public float minJumpTime = 3f;
    public float maxJumpTime = 6f;
    public float wallDistance = 1f;
    public bool rotateTransform = true;

    private CharacterControllerMovement _controller;
    private Vector3 _currentDirection;
    private float _remainingWalkTime;
    private float _timeUntilJump;
    private static Vector3 _up = new Vector3(0f, 1f, 0f);

    void Start()
    {
        _controller = GetComponent<CharacterControllerMovement>();
        ChangeDirection();
        ResetJump();
    }

    void Update()
    {
        _remainingWalkTime -= Time.deltaTime;
        _timeUntilJump -= Time.deltaTime;

        if (_remainingWalkTime <= 0f || CheckWallProximity())
            ChangeDirection();

        _controller.TryMove(_currentDirection * walkSpeed * Time.deltaTime);
        if (rotateTransform)
            transform.rotation = Quaternion.LookRotation(_currentDirection, _up);

        if(_timeUntilJump <= 0f)
        {
            _controller.TryJump(jumpSpeed);
            ResetJump();
        }
    }

    public void ChangeDirection()
    {
        _currentDirection = Vector3.zero;
        _currentDirection.x = Random.Range(-1f, 1f);
        _currentDirection.z = Random.Range(-1f, 1f);
        _currentDirection.Normalize();
        _remainingWalkTime = Random.Range(minWalkTime, maxWalkTime);
    }

    private void ResetJump()
    {
        _timeUntilJump = Random.Range(minJumpTime, maxJumpTime);
    }

    private bool CheckWallProximity()
    {
        foreach (RaycastHit hit in Physics.RaycastAll(transform.position, _currentDirection, wallDistance))
            if (hit.transform.root != transform.root)
                return true;
                
        return false;
    }
}
