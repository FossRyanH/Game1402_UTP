using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The logic here has some problems. You add elements to a list of potential objects to hit, but never have a way of getting rid of them, which means that you then get an error when trying to do this...stopping you from ever being able to target.
/// </summary>
public class Targeter : MonoBehaviour
{
    private List<Target> _targets = new List<Target>();
    
    public Target CurrentTarget { get; private set; }
    private Camera _camera;

    private void Awake()
    {
        _camera = FindFirstObjectByType<Camera>();
    }

    // Check if an object containing the Target Script enters the Sphere collider, if so add it to the targeting system
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            _targets.Add(target);
        }
    }
    /// <summary>
    /// Sean question: what happens if your target dies? Doesn't it just stay in the list forever?
    /// </summary>
    /// <param name="other"></param>
    // If the object leaving the targeting sphere had the Target object, remove it from the list.
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            _targets.Remove(target);
        }
    }
/// <summary>
/// Code here has been broken for a long, long time. About a month. Only works on one enemy. Null checked it. Be more careful in your gyms.
/// </summary>
/// <returns></returns>
    public bool HasTarget()
    {
        if (_targets.Count == 0)
        {
            return false;
        }

        Target closestTarget = null;
        float closestTargetDist = Mathf.Infinity;

        foreach (Target target in _targets)
        {
            if (target == null) continue;
            Vector2 viewPos = _camera.WorldToViewportPoint(target.transform.position);

            if (viewPos.x < 0f || viewPos.x > 1f | viewPos.y < 0f || viewPos.y > 1f)
            {
                continue;
            }

            Vector2 toPlayer = viewPos - new Vector2(0.5f, 0.5f);
            if (toPlayer.sqrMagnitude < closestTargetDist)
            {
                closestTarget = target;
                closestTargetDist = toPlayer.sqrMagnitude;
            }
        }

        if (closestTarget == null)
        {
            return false;
        }

        CurrentTarget = closestTarget;
        return true;
    }

    void Update()
    {
        
        
    }
    public void CancelTarget()
    {
        CurrentTarget = null;
    }
}
