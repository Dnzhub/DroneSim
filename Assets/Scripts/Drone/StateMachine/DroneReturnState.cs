using System;
using System.Collections;
using UnityEngine;

public class DroneReturnState : IDroneState
{
    private GameState _gameState;
    private DroneController _drone;

 
    public DroneReturnState(DroneController drone)
    {
        this._drone = drone;
        this._gameState = GameState.Instance;
    }
    public void EnterState()
    {
        _drone.MoveToTarget(_drone.HomeBase.transform.position);
        

    }


    public void UpdateState()
    {
        // Delay HasReachedDestination until we are sure the agent has a path
        if (_drone.HasReachedDestination())
        {
            _drone.SwitchState(_drone.GetSearchState());

        }
    }
    public void ExitState()
    {
        float respawnFrequency = _gameState.UIManager.ResourceGenerationFrequency.value;
        _drone.StartStateCoroutine(_gameState.ResourceManager.RespawnResource(_drone.TargetResource, respawnFrequency));
        _gameState.IncrementFactionScore(_drone.DroneFaction);
        _drone.TargetResource = null;
        
    }



}
