using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class WalkToTarget : Node
{

    private Transform _transform;
    private string _target;
    private UnityEngine.AI.NavMeshAgent _agent;
    

    public WalkToTarget(Transform transform, string target, UnityEngine.AI.NavMeshAgent agent)
    {
        _transform = transform;
        _target = target;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        Vector3 target = (Vector3)GetData(_target);

        if (Vector3.Distance(_transform.position, target) > 0.5f)
        {

            _agent.SetDestination(target);

        }

        state = NodeState.SUCCESS;
        return state;
    }
}
