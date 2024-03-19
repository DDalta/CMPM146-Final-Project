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
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if (Vector3.Distance(_transform.position, target) > 0.1f)
        {
            _agent.SetDestination(target);

            Vector3 rotation = (_agent.steeringTarget - _transform.position).normalized;

            float rot_z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            _transform.rotation = Quaternion.Euler(0, 0, rot_z);

        } else
        {
            ClearData(_target);
            _agent.ResetPath();
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
