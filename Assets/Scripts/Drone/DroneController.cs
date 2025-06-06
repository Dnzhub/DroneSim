using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class DroneController : MonoBehaviour
{
    
    private Faction _faction;
    public Faction DroneFaction => _faction;
    private BaseController _homeBase;
    public BaseController HomeBase => _homeBase;
    public Resource TargetResource { get; set; }

    private LayerMask _blockingLayers;
    public LayerMask BlockingLayers => _blockingLayers;

    private LineRenderer _pathLineRenderer;
    public bool RenderPath { get; set; } = false;

    private NavMeshAgent _agent;
    public NavMeshAgent Agent
    {
        get { if (_agent == null) _agent = GetComponent<NavMeshAgent>(); return _agent; }
       
    }
    private IDroneState _currentState;
    private DroneSearchingState _searchState;
    private DroneCollectState _collectState;
    private DroneReturnState _returnState;


    public IDroneState GetSearchState() => _searchState;
    public IDroneState GetCollectState() => _collectState;
    public IDroneState GetReturnState() => _returnState;

    private void Start()
    {
       
        _agent = GetComponent<NavMeshAgent>();
        _pathLineRenderer = GetComponent<LineRenderer>();
        _pathLineRenderer.positionCount = 0;
        _pathLineRenderer.enabled = false;

    }
    public void Initialize(Faction faction, float speed, BaseController homeBase)
    {
        this._faction = faction;
        this._homeBase = homeBase;
        _searchState = new DroneSearchingState(this);
        _collectState = new DroneCollectState(this);
        _returnState = new DroneReturnState(this);

        Agent.speed = speed;
        Agent.avoidancePriority = Random.Range(30, 60); // Optional
        Agent.stoppingDistance = 3;
        Agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        SwitchState(_searchState);

    }

    void Update()
    {
        _currentState?.UpdateState();
        if (RenderPath)
            RenderDronePath();
        else if (_pathLineRenderer.enabled)
            _pathLineRenderer.enabled = false;
    }

    public void SwitchState(IDroneState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    public void SetSpeed(int speed)
    {
        Agent.speed = speed;

    }
    public void MoveToTarget(Vector3 target)
    {
        _agent.SetDestination(target);
    }
    public bool HasReachedDestination()
    {
        if (!Agent.hasPath || Agent.pathPending)
            return false;

        return Agent.remainingDistance <= Agent.stoppingDistance &&
               Agent.velocity.sqrMagnitude < 0.01f;
    }
    private void RenderDronePath()
    {
        if (!RenderPath || _agent.path == null || _agent.path.corners.Length == 0)
            return;

        _pathLineRenderer.enabled = true;
        _pathLineRenderer.startWidth = 0.2f;
        _pathLineRenderer.endWidth = 0.2f;
        _pathLineRenderer.positionCount = _agent.path.corners.Length;
        _pathLineRenderer.SetPositions(_agent.path.corners);
    }
    public void ShowDronePath(bool show)
    {
        RenderPath = show;
    }

    public void StartStateCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}

