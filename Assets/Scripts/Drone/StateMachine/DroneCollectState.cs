using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DroneCollectState : IDroneState
{
    private GameState _gameState;
    private DroneController _drone;
    private float _collectDuration = 2f;
    private float _timer;
    private float _rotationSpeed = 5f;

    public DroneCollectState(DroneController drone)
    {
        this._drone = drone;
        this._gameState = GameState.Instance;
    }
    public void EnterState()
    {
        _timer = _collectDuration;
        _drone.Agent.updateRotation = false;

        _drone.SpawnParticleAtPosition(_drone.CollectParticleEffect, _drone.TargetResource.transform.position, _gameState.UIManager.ResourceGenerationFrequency.value);
        _drone.PlaySFXOnce(_drone.CollectAudioClip);
    }


    public void UpdateState()
    {
        _drone.RotateTowardTarget(_rotationSpeed);
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
        _drone.PlayVFX(_drone.AuraParticleEffect);
        _drone.Agent.updateRotation = true;

    }


}
