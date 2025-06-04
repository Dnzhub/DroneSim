using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class DroneController : MonoBehaviour
{
    private Faction _faction;
    private Transform _homeBase;

    private NavMeshAgent agent;
    private IDroneState _currentState;
    private DroneSearchingState _searchState;
    //private CollectState _collectState;
    //private ReturnState _returnState;

    private void Awake()
    {
        _searchState = new DroneSearchingState(this);
        //_collectState = new CollectState(this);
        //_returnState = new ReturnState(this);
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void Initialize(Faction faction, float speed, Transform homeBase)
    {
        this._faction = faction;
        this._homeBase = homeBase;


        if (agent != null)
        {
            agent.speed = speed;
            agent.avoidancePriority = Random.Range(30, 60); // Optional
        }
        SwitchState(_searchState);

    }

    void Update()
    {
        _currentState?.UpdateState();
    }

    public void SwitchState(IDroneState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }




}

