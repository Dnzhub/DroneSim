using UnityEngine;

public class DroneSearchingState : IDroneState
{
    private DroneController drone;

    public DroneSearchingState(DroneController drone)
    {
        this.drone = drone;
    }
    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
