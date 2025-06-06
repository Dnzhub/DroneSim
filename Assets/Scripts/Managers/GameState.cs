using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{ 
    public static GameState Instance { get; private set; }
    public bool HasSimulationStarted { get; private set; } = false;
    public event Action<Faction,int> OnScoreIncrement;
    private DroneManager _droneManager;
    private ResourceManager _resourceManager;
    private UIManager _uiManager;

    private Dictionary<Faction, int> _factionScores = new();


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

    public UIManager UIManager
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = GetComponent<UIManager>();

            }
            return _uiManager;
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

        HasSimulationStarted = false;
        InitializeScoreKeeper();
      
    }
  
   
    public void StartSimulation()
    {
        HasSimulationStarted = true;
        DroneManager.SpawnDrones(UIManager.DroneCountSlider.value);
        ResourceManager.SpawnResource();
    }

    private void InitializeScoreKeeper()
    {
        foreach (Faction faction in Enum.GetValues(typeof(Faction)))
        {
            if (faction == Faction.Neutral) continue;
            _factionScores[faction] = 0;
        }
    }
    public void IncrementFactionScore(Faction faction)
    {
        if (!_factionScores.ContainsKey(faction))
            _factionScores[faction] = 0;

        _factionScores[faction]++;
        OnScoreIncrement?.Invoke(faction,_factionScores[faction]);
        //UIManager.Instance.UpdateScore(faction, _factionScores[faction]);
    }

    public void UpdateDroneSpeed(int speed)
    {
        DroneManager.UpdateDroneSpeeds(speed);
    }

    public void OnDroneCountChanged(int newCount)
    {
        DroneManager.RemoveDrones();
        DroneManager.SpawnDrones(newCount);

    }

  

}
