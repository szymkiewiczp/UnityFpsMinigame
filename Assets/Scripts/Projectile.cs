using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    /// <summary>
    /// How much additional distance should be checked for exact collision location
    /// </summary>
    public float collisionDelta = 0.05f;
    /// <summary>
    /// How much distance should the projectile be moved back by from the collision point
    /// </summary>
    public float positionShift = 0.05f;
    public UnityEvent onHit;

    private Vector3 _lastPosition;
    private Vector3 _previousPosition;

    private void Start()
    {
        _lastPosition = _previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        _previousPosition = _lastPosition;
        _lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 velocity = transform.position - _previousPosition;

        foreach (RaycastHit hit in Physics.RaycastAll(_previousPosition, velocity, velocity.magnitude + collisionDelta))
        {
            if (hit.collider == collision.collider)
            {
                transform.position = hit.point - velocity.normalized * positionShift;
                break;
            }  
        }  

        onHit?.Invoke();
        Destroy(gameObject);

        Health health = collision.transform.root.GetComponent<Health>();
        if (health != null)
            health.Damage(damage);
    }
}
