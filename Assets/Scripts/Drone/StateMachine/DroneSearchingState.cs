using UnityEngine;

public class DroneSearchingState : IDroneState
{
    private GameState _gameState;
    private DroneController _drone;
    public DroneSearchingState(DroneController drone)
    {
        this._drone = drone;
        this._gameState = GameState.Instance;
    }
    public void EnterState()
    {

        _drone.TargetResource = GameState.Instance.ResourceManager.GetNearestAvailableResource(_drone.transform.position);

        if (_drone.TargetResource != null)
        {
            _drone.TargetResource.Claim();

            _drone.MoveToTarget(_drone.TargetResource.transform.position);
             
        }
     


            Debug.Log("Searching State Enter.");
    }

    

    public void UpdateState()
    {
        
        // If no resource was found, retry search after timer delay
        if (_drone.TargetResource == null)
        {           
            _drone.SwitchState(_drone.GetSearchState()); // retry search           
            return;
        }

        // Delay HasReachedDestination until we are sure the agent has a path
        if (_drone.HasReachedDestination())
        {
            _drone.SwitchState(_drone.GetCollectState());
        
        }


    }
    public void ExitState()
    {

    }
  
}
