using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage = 200f;
    public float range = 5f;
    public int raysPerCollider = 100;

    void Start()
    {
        foreach(Health health in FindObjectsOfType<Health>())
            if (GetDistance(health.gameObject, out float distance))
                health.Damage(GetDamageForDistance(distance));
    }

    public float GetDamageForDistance(float distance)
    {
        return damage * Mathf.Clamp(1f - distance / range, 0f, 1f);
    }

    private bool GetDistance(GameObject obj, out float minDistance)
    {
        minDistance = float.MaxValue;
        Vector3 raycastPosition = transform.position;
        float maxDistanceSquared = range * range;

        foreach (Collider collider in obj.GetComponentsInChildren<Collider>())
        {
            Bounds bounds = collider.bounds;
            if (bounds.SqrDistance(raycastPosition) > maxDistanceSquared)
                continue;

            Vector3 boundsMin = bounds.min;
            Vector3 boundsMax = bounds.max;

            for(int i = 0; i < raysPerCollider; i++)
            {
                Vector3 randomPoint = new Vector3(
                    Random.Range(boundsMin.x, boundsMax.x),
                    Random.Range(boundsMin.y, boundsMax.y),
                    Random.Range(boundsMin.z, boundsMax.z)
                );

                if (!Physics.Raycast(raycastPosition, randomPoint - raycastPosition, out RaycastHit hit, range))
                    continue;
                if (hit.collider != collider)
                    continue;
                float distance = (hit.point - raycastPosition).magnitude;

                if (distance < minDistance)
                    minDistance = distance;
            }
        }

        return minDistance <= range;
    }
}
