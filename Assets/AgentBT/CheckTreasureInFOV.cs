using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTreasureInFOV : Node
{
    private List<GameObject> _viewableObjects;
    private Transform _transform;

    public CheckTreasureInFOV(Transform transform, List<GameObject> objs)
    {
        _viewableObjects = objs;
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("Target");
        if (target == null)
        {
            if (_viewableObjects.Count > 0)
            {
                foreach (GameObject obj in _viewableObjects)
                {
                    if (obj.layer == LayerMask.NameToLayer("Treasure")) 
                    {
                        parent.parent.SetData("Target", obj.transform.position);
                        state = NodeState.SUCCESS;
                        return state;
                    }
                } 
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
