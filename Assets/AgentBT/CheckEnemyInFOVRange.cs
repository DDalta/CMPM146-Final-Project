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
                    if (Vector3.Distance(_transform.position, obj.transform.position) < 2.5f)
                    {
                        Stack<Vector3> toVisit = new Stack<Vector3>((Stack<Vector3>)GetData("ToVisit"));

                        toVisit.Clear();
                        toVisit.Push(Vector3.zero);

                        parent.parent.SetData("Target", Vector3.zero);
                        parent.parent.SetData("CurrentRoom", Vector3.zero);
                        parent.parent.SetData("ToVisit", toVisit);

                        _viewableObjects.Clear();
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