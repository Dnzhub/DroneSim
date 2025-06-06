using DG.Tweening;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsClaimed  = false;
    private float _stepDuration = 1f;
    private float _rotationSpeed = 90f; // Degrees per second
    private GameObject _visual;

    private void Start()
    {
        _visual = transform.GetChild(0).gameObject;
        RotateToRandom();  
        
    }
    public void Claim()
    {
        IsClaimed = true;
    }

    public void UnClaim()
    {
        IsClaimed=false;
    }

    void RotateToRandom()
    {   
        // Create a random rotation step
        Vector3 randomStep = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * _rotationSpeed * _stepDuration;

        // Apply rotation additively
        _visual.transform.DORotate(randomStep, _stepDuration, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear)
            .OnComplete(RotateToRandom); // Loop again
    }

}
