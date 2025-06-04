using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] private GameObject _redDronePrefab;
    [SerializeField] private GameObject _blueDronePrefab;
    [SerializeField] private int _redTeamDroneCount = 2;
    [SerializeField] private int _blueTeamDroneCount = 2;
    [SerializeField] private BaseController _redBase, _blueBase;
    [SerializeField] private float _droneSpeed = 10;

    private List<DroneController> _allDrones = new List<DroneController>();

    public void SpawnDrones()
    {
        for (int i = 0; i < _redTeamDroneCount; i++)
        {
            SpawnDrone(Faction.Red, _redBase);
        }
        for (int i = 0; i < _blueTeamDroneCount; i++)
        {
            SpawnDrone(Faction.Blue, _blueBase);
        }

    }

    private void SpawnDrone(Faction faction, BaseController baseTransform)
    {

        Vector3 spawnPosition = SelectRandomPosAroundBase(baseTransform);
        Quaternion forwardDir = RotateTowardBaseForward(baseTransform);
        var droneObj = Instantiate(faction == Faction.Red ? _redDronePrefab : _blueDronePrefab, spawnPosition, forwardDir);
        var drone = droneObj.GetComponent<DroneController>();
        drone.Initialize(faction, _droneSpeed);
        _allDrones.Add(drone);
    }
    private Vector3 SelectRandomPosAroundBase(BaseController target)
    {
        Vector2 randomPoint = Random.insideUnitCircle * target.DroneSpawnRadius;
         return new Vector3(
          target.transform.position.x + randomPoint.x,
          target.transform.position.y,
          target.transform.position.z + randomPoint.y
         );
    }
    private Quaternion RotateTowardBaseForward(BaseController target)
    {
        return Quaternion.Euler(0, target.transform.eulerAngles.y, 0);
    }
    public void UpdateDroneSpeeds(float newSpeed)
    {
        foreach (var drone in _allDrones)
        {
            //drone.SetSpeed(newSpeed);
        }
    }

 
}
