using UnityEngine;

public class DroneController : MonoBehaviour
{
    private Faction _faction;
    public void Initialize(Faction faction, float speed)
    {
        this._faction = faction;
       

        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //if (agent != null)
        //{
        //    agent.speed = speed;
        //    agent.avoidancePriority = Random.Range(30, 60); // Optional
        //}

        //SwitchState(new DroneSearchingState(this));
    }

   
}

