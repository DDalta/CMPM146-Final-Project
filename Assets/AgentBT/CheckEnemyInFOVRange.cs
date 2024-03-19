using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private Transform _transform;
    private List<GameObject> _viewableObjects;

    public CheckEnemyInFOVRange(Transform transform, List<GameObject> objs)
    {
        _transform = transform;
        _viewableObjects = objs;

    }

    public override NodeState Evaluate()
    {

        if (_viewableObjects.Count > 0)
        {
            foreach (GameObject obj in _viewableObjects)
            {
                if (obj.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (Vector3.Distance(_transform.position, obj.transform.position) < 3.5f)
                    {
                        Stack<Vector3> visited = (Stack<Vector3>)GetData("VisitedRooms");

                        parent.parent.SetData("Target", visited.Pop());
                        parent.parent.SetData("VisitedRooms", visited);
                        state = NodeState.SUCCESS;
                        return state;
                    }
                    
                }
            }
        }
        state = NodeState.FAILURE;
        return state;
    }

}