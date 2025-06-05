using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResourceManager : MonoBehaviour
{


    [SerializeField] private LayerMask _spawnBlockingLayers;
    [SerializeField] private int _totalResourceCount = 10;
    [SerializeField] private float _spawnRangeX = 20f;
    [SerializeField] private float _spawnRangeZ = 20f;
    public float spawnRangeX => _spawnRangeX;
    public float spawnRangeZ => _spawnRangeZ;

    [SerializeField] private List<Resource> _resourcePrefabs = new List<Resource>();


    private List<Resource> _activeResources = new List<Resource>();


    public void SpawnResource()
    {
        for (int i = 0; i < _totalResourceCount; i++)
        {

            Vector3 position = Utils.FindValidPosition(transform,_spawnRangeX,_spawnRangeZ, _spawnBlockingLayers);
            Resource newResource = Instantiate(GetRandomResourcePrefab(), position, Quaternion.identity);     
            _activeResources.Add(newResource);
        }
    
    }

    private Resource GetRandomResourcePrefab()
    {
        int randomNumber = Random.Range(1, _resourcePrefabs.Count);
        return _resourcePrefabs[randomNumber];
    }
    public Resource GetNearestAvailableResource(Vector3 dronePosition)
    {
        Resource nearest = null;
        float shortestDistance = float.MaxValue;


        foreach (var resource in _activeResources)
        {
            if (resource == null || resource.IsClaimed) continue;

            float distance = Vector3.Distance(dronePosition, resource.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = resource;
            }
        }
  
        return nearest;
    }
    public void RemoveResource(Resource resource)
    {
        if (_activeResources.Contains(resource))
            _activeResources.Remove(resource);
    }
    private void OnDrawGizmosSelected()
    {
        Utils.DrawCubeAtLocation(transform, _spawnRangeX, _spawnRangeZ, Color.green);
    }

    public void DisableResource(Resource resource)
    {
        resource.transform.GetChild(0).gameObject.SetActive(false); 
        if(resource.TryGetComponent<NavMeshObstacle>(out NavMeshObstacle obstacle))
        {
            obstacle.enabled = false;
        }

    }
    public void EnableResource(Resource resource)
    {
        resource.transform.GetChild(0).gameObject.SetActive(true);
        if (resource.TryGetComponent<NavMeshObstacle>(out NavMeshObstacle obstacle))
        {
            obstacle.enabled = true;
        }
    }

}
