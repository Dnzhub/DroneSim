using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private float _droneSpawnRadius = 5f;
    [SerializeField] private Color _baseColor;

    public float DroneSpawnRadius { get; private set; }

    void Awake()
    {
        DroneSpawnRadius = _droneSpawnRadius;
    }

    private void OnDrawGizmosSelected()
    {
       

        Gizmos.color = _baseColor;
        Gizmos.DrawWireSphere(transform.position, _droneSpawnRadius);
    }
}
