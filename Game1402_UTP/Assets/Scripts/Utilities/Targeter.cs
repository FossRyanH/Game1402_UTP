using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // If the object leaving the targeting sphere had the Target object, remove it from the list.
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            _targets.Remove(target);
        }
    }

    public bool SelectTarget()
    {
        if (_targets.Count == 0)
        {
            return false;
        }

        Target closestTarget = null;
        float closestTargetDist = Mathf.Infinity;

        foreach (Target target in _targets)
        {
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

    public void CancelTarget()
    {
        CurrentTarget = null;
    }
}
