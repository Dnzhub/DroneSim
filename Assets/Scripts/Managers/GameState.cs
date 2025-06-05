using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    private DroneManager _droneManager;
    private ResourceManager _resourceManager;

    public DroneManager DroneManager
    {
        get
        {
            if (_droneManager == null)
            {
                _droneManager = GetComponent<DroneManager>();
              
            }
            return _droneManager;
        }
    }
    public ResourceManager ResourceManager
    {
        get
        {
            if (_resourceManager == null)
            {
                _resourceManager = GetComponent<ResourceManager>();

            }
            return _resourceManager;
        }
    }
  
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
       
        _droneManager = GetComponent<DroneManager>();
        _resourceManager = GetComponent<ResourceManager>();
    }
    private void Start()
    {
        StartSimulation();
    }
    private void StartSimulation()
    {
        _droneManager.SpawnDrones();
        _resourceManager.SpawnResource();
    }
}
