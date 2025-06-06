using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DroneCollectState : IDroneState
{
    private GameState _gameState;
    private DroneController _drone;
    private float _collectDuration = 2f;
    private float _timer;
    private float rotationSpeed = 5f;

    public DroneCollectState(DroneController drone)
    {
        this._drone = drone;
        this._gameState = GameState.Instance;
    }
    public void EnterState()
    {
        _timer = _collectDuration;
        _drone.Agent.updateRotation = false;

    }


    public void UpdateState()
    {
        RotateTowardTarget();
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            if (_drone.TargetResource != null)
            {
               _gameState.ResourceManager.DisableResource(_drone.TargetResource);
               
            }
            _drone.SwitchState(_drone.GetReturnState());
    
        }

      

    }
    public void ExitState()
    {
        _drone.Agent.updateRotation = true;

    }

    private void RotateTowardTarget()
    {
        Vector3 direction = _drone.TargetResource.transform.position - _drone.Agent.transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _drone.Agent.transform.rotation = Quaternion.Slerp(
                _drone.Agent.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
