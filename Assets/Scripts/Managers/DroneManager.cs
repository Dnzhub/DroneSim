using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] private GameObject _redDronePrefab, _blueDronePrefab;
    [SerializeField] private BaseController _redBase, _blueBase;
    [SerializeField] private float _spawnRangeX = 20f;
    [SerializeField] private float _spawnRangeZ = 20f;
    [SerializeField] private LayerMask _spawnBlockingLayers;

    private List<DroneController> _allDrones = new List<DroneController>();

    public void SpawnDrones(int totalCount = 1)
    {
       
        for (int i = 0; i < totalCount; i++)
        {
            SpawnDrone(Faction.Red, _redBase);
        }
        for (int i = 0; i < totalCount; i++)
        {
            SpawnDrone(Faction.Blue, _blueBase);
        }

    }
    public void RemoveDrones()
    {
        foreach (var drone in _allDrones)
        {
            Destroy(drone.gameObject);
        }
        _allDrones.Clear();
    }
    
    public void ShowDronesPath(bool show)
    {
        foreach (var drone in _allDrones)
        {
            drone.RenderPath = show;
        }
    }

    private void SpawnDrone(Faction faction, BaseController baseTransform)
    {
       

        Vector3 spawnPosition = Utils.FindValidPosition(baseTransform.transform, _spawnRangeX, _spawnRangeZ, _spawnBlockingLayers);
        Quaternion forwardDir = Utils.GetTargetsForward(baseTransform.transform);

        var droneObj = Instantiate(faction == Faction.Red ? _redDronePrefab : _blueDronePrefab, spawnPosition, forwardDir);
        var drone = droneObj.GetComponent<DroneController>();
        drone.Initialize(faction, GameState.Instance.UIManager.DroneSpeedSlider.value, baseTransform);
        _allDrones.Add(drone);
    }

    public void UpdateDroneSpeeds(int newSpeed)
    {
        foreach (var drone in _allDrones)
        {
            drone.SetSpeed(newSpeed);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Utils.DrawCubeAtLocation(_redBase.transform, _spawnRangeX, _spawnRangeZ, Color.red);
        Utils.DrawCubeAtLocation(_blueBase.transform, _spawnRangeX, _spawnRangeZ, Color.blue);
    }

}
