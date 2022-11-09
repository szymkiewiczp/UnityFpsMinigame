using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public float timeout = 0.5f;
    public float projectileSpeed = 10f;
    public Rigidbody projectilePrefab;
    /// <summary>
    /// An optional object that contains colliders to be ignored by launched projectiles' collision detection.
    /// The typical use case is when the projectile is launched from inside of an object and this object should be ignored.
    /// </summary>
    public GameObject ignoreCollisionParent;
    public UnityEvent onShoot;

    private float _remainingTimeout = 0f;
    private bool _previousTickShot = false;

    void Update()
    {
        if (_remainingTimeout < 0f && !_previousTickShot)
            _remainingTimeout = 0f;

        if (_previousTickShot)
            _previousTickShot = false;

        if (_remainingTimeout > 0f)
            _remainingTimeout -= Time.deltaTime;
    }

    public void TryShoot()
    {
        while (_remainingTimeout <= 0f)
            ForceShoot();
    }

    public void ForceShoot()
    {
        _remainingTimeout += timeout;
        _previousTickShot = true;

        Rigidbody projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.velocity = transform.TransformDirection(0f, 0f, projectileSpeed);

        if (ignoreCollisionParent != null)
        {
            Collider[] projectileColliders = projectile.GetComponentsInChildren<Collider>();

            foreach (Collider parentCollider in ignoreCollisionParent.GetComponentsInChildren<Collider>())
                foreach (Collider projectileCollider in projectileColliders)
                    Physics.IgnoreCollision(parentCollider, projectileCollider);
        }
            

        onShoot?.Invoke();
    }
}
