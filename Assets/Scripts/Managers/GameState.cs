using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [SerializeField] private DroneManager _droneManager;

    private void OnValidate()
    {
        if (!_droneManager /*|| !resourceManager || !uiManager*/)
        {
            Debug.LogWarning("GameState is missing manager references!");
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
    }
    private void Start()
    {
        StartSimulation();
    }
    private void StartSimulation()
    {
        _droneManager.SpawnDrones();
    }
}
