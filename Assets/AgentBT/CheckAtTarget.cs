using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckAtTarget : Node
{

    private Transform _transform;
    UnityEngine.AI.NavMeshAgent _agent;

    public CheckAtTarget(Transform transform, UnityEngine.AI.NavMeshAgent agent)
    {
        _transform = transform;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("Target");

        if (target == null)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        if (Vector3.Distance(_transform.position, (Vector3)target) < 0.1)
        {
            ClearData("Target");
            _agent.ResetPath();
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }

}
