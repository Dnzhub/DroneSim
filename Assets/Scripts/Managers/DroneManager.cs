using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] private GameObject _redDronePrefab, _blueDronePrefab;
    [SerializeField] private BaseController _redBase, _blueBase;
    [SerializeField] private int _redTeamDroneCount = 2;
    [SerializeField] private int _blueTeamDroneCount = 2;
   
    [SerializeField] private float _droneSpeed = 10;
    [SerializeField] private float _spawnRangeX = 20f;
    [SerializeField] private float _spawnRangeZ = 20f;
    [SerializeField] private LayerMask _spawnBlockingLayers;
    public LayerMask BlockingLayers => _spawnBlockingLayers;

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
       

        Vector3 spawnPosition = Utils.FindValidPosition(baseTransform.transform, _spawnRangeX, _spawnRangeZ, _spawnBlockingLayers);
        Quaternion forwardDir = Utils.GetTargetsForward(baseTransform.transform);

        var droneObj = Instantiate(faction == Faction.Red ? _redDronePrefab : _blueDronePrefab, spawnPosition, forwardDir);
        var drone = droneObj.GetComponent<DroneController>();
        drone.Initialize(faction, _droneSpeed, baseTransform, _spawnBlockingLayers);
        _allDrones.Add(drone);
    }

    public void UpdateDroneSpeeds(float newSpeed)
    {
        foreach (var drone in _allDrones)
        {
            //drone.SetSpeed(newSpeed);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Utils.DrawCubeAtLocation(_redBase.transform, _spawnRangeX, _spawnRangeZ, Color.red);
        Utils.DrawCubeAtLocation(_blueBase.transform, _spawnRangeX, _spawnRangeZ, Color.blue);
    }

}
