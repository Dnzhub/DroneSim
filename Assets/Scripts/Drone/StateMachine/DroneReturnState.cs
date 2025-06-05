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
        
        Debug.Log("Return State Enter.");

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
        float spawnRangeX = _gameState.ResourceManager.spawnRangeX;
        float spawnRangeZ = _gameState.ResourceManager.spawnRangeZ;
        Transform ResourceSpawnPoint = _gameState.ResourceManager.transform;
       
        _drone.TargetResource.transform.position = Utils.FindValidPosition(
            ResourceSpawnPoint, 
            spawnRangeX, 
            spawnRangeZ, 
            _drone.BlockingLayers);
       
         _gameState.ResourceManager.EnableResource(_drone.TargetResource);

        _drone.TargetResource.UnClaim();
        _drone.TargetResource = null;
    }

}
