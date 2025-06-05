using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsClaimed  = false;

    public void Claim()
    {
        IsClaimed = true;
        Debug.Log($"{gameObject.name} claimed!");
    }

    public void UnClaim()
    {
        IsClaimed=false;
    }
  
}
