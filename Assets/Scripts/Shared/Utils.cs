using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class Utils
{
    public static Vector3 FindValidPosition(Transform spawnPoint, float rangeX, float rangeZ, LayerMask targetLayers)
    {
        const int maxAttempts = 10;
        const float checkRadius = 1.5f;


        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {


            Vector3 randomPosition = GenerateRandomPosition(spawnPoint, rangeX, rangeZ);
            randomPosition.y = spawnPoint.transform.position.y;

            if (Physics.OverlapSphere(randomPosition, checkRadius, targetLayers).Length == 0)
            {
                return randomPosition;
            }
        }

        Vector3 fallbackPosition = GenerateRandomPosition(spawnPoint, rangeX, rangeZ);
        fallbackPosition.y = 1;

        Debug.LogWarning("No valid position found for resource after several attempts. Returning random fallback position.");

        return fallbackPosition;
    }

    public static Vector3 GenerateRandomPosition(Transform spawnPoint, float rangeX, float rangeZ)
    {
        float offsetX = Random.Range(-rangeX / 2f, rangeX / 2f);
        float offsetZ = Random.Range(-rangeZ / 2f, rangeZ / 2f);

        return new Vector3(spawnPoint.position.x + offsetX, spawnPoint.position.y, spawnPoint.position.z + offsetZ);
    }

    public static Quaternion GetTargetsForward(Transform target)
    {
        return Quaternion.Euler(0, target.transform.eulerAngles.y, 0);

    }

    public static void DrawCubeAtLocation(Transform target, float _spawnRangeX, float _spawnRangeZ, Color color)
    {
        if (target == null) return;
        Gizmos.color = color;
        Gizmos.DrawWireCube(target.position, new Vector3(_spawnRangeX, 0.1f, _spawnRangeZ));

    }
}

