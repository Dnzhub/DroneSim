using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] private LayerMask _spawnBlockingLayers;
    [SerializeField] private int _totalResourceCount = 10;
    [SerializeField] private float _spawnRangeX = 20f;
    [SerializeField] private float _spawnRangeZ = 20f;
    [SerializeField] private List<Resource> _resourcePrefabs = new List<Resource>();


    private List<Resource> _activeResources = new List<Resource>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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
   

    private void OnDrawGizmosSelected()
    {
        Utils.DrawCubeAtLocation(transform, _spawnRangeX, _spawnRangeZ, Color.green);
    }
}
