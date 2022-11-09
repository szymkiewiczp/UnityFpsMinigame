using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy spawnedPrefab;
    public Transform corner1, corner2;
    public int maxSpawnAttempts = 100;
    public float powerPerKilledObject = 0.05f;
    public float heightMultiplier = 0.99f;

    private int _spawnedObjects = 0;
    private int _killedObjects = 0;

    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        float randomPower = Random.Range(1f, 1f + _killedObjects * powerPerKilledObject);
        spawnedPrefab.power = randomPower;

        Vector3 spawnPosition = GetSafeSpawnPosition(spawnedPrefab.gameObject);
        Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        Enemy enemy = Instantiate(spawnedPrefab, spawnPosition, spawnRotation);
        enemy.GetComponent<Health>().onKilled.AddListener(OnDeath);

        _spawnedObjects++;
    }

    private Vector3 GetSafeSpawnPosition(GameObject obj)
    {
        float halfWidth = 0f;
        float halfHeight = 0f;

        foreach(Collider collider in obj.GetComponentsInChildren<Collider>())
        {
            Vector3 extents = collider.bounds.extents;
            if (halfWidth < extents.x)
                halfWidth = extents.x;
            if (halfHeight < extents.y)
                halfHeight = extents.y;
            if (halfWidth < extents.z)
                halfWidth = extents.z;
        }

        for(int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector3 spawnPosition = GetUnsafeSpawnPosition();

            Vector3 boxCenter = spawnPosition + new Vector3(0f, halfHeight, 0f);
            Vector3 halfExtents = new Vector3(halfWidth, heightMultiplier * halfHeight, halfWidth);

            if (!Physics.CheckBox(boxCenter, halfExtents))
                return spawnPosition;
        }

        return GetUnsafeSpawnPosition();
    }

    private Vector3 GetUnsafeSpawnPosition()
    {
        Vector3 position1 = corner1.position;
        Vector3 position2 = corner2.position;

        return new Vector3(
            Random.Range(position1.x, position2.x),
            Random.Range(position1.y, position2.y),
            Random.Range(position1.z, position2.z)
        );
    }

    private void OnDeath()
    {
        _killedObjects++;
        Spawn();

        int livingObjects = _spawnedObjects - _killedObjects;

        if (livingObjects * livingObjects < _spawnedObjects)
            Spawn();
    }
}
