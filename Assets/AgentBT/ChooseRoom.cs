using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEditor;

public class ChooseRoom : Node
{

    private List<GameObject> _viewableObjects;

    public ChooseRoom(List<GameObject> objs)
    {
        _viewableObjects = objs;
    }

    public override NodeState Evaluate()
    {

        Stack<Vector3> visited = (Stack<Vector3>)GetData("VisitedRooms");

        if (_viewableObjects.Count > 0)
        {
            foreach (GameObject obj in _viewableObjects)
            {
                if (obj.layer == LayerMask.NameToLayer("Entrance"))
                {
                    if(!visited.Contains(obj.transform.position))
                    {
                        RoomEntrance objParent = obj.transform.parent.GetComponent<RoomEntrance>();
                        Vector3 objParentPosition = obj.transform.parent.transform.position;

                        parent.parent.SetData("Target", objParentPosition);
                        parent.parent.SetData("CurrentRoom", objParentPosition);

                        objParent.DestroyEntrances();

                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }

            // Empty
            // Pop previous room and move to it
            Vector3 previousRoom = visited.Pop();
            parent.parent.SetData("Target", previousRoom);
            parent.parent.SetData("CurrentRoom", previousRoom);
            parent.parent.SetData("VisitedRooms", visited);


        } else
        {
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
