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

        Stack<Vector3> toVisit = new Stack<Vector3>((Stack<Vector3>)GetData("ToVisit"));
        List<Vector3> visited = new List<Vector3>((List<Vector3>)GetData("VisitedRooms"));

        if (_viewableObjects.Count > 0)
        {
            foreach (GameObject obj in _viewableObjects)
            {
                if (obj.layer == LayerMask.NameToLayer("Entrance"))
                {
                    if(!visited.Contains(obj.transform.position))
                    {
                        RoomEntrance objParent = obj.transform.parent.GetComponent<RoomEntrance>();
                        Vector3 objParentPosition = obj.transform.parent.position;

                        parent.parent.SetData("Target", objParentPosition);
                        parent.parent.SetData("CurrentRoom", objParentPosition);

                        objParent.DestroyEntrances();
                        _viewableObjects.Clear();

                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }
        }

        // No unvisted rooms left
        // Pop previous room and move to it

        //ClearData("ToVisit");
        //ClearData("VisitedRooms");

        Vector3 previousRoom = Vector3.zero;

        if (toVisit.Count > 0)
            previousRoom = toVisit.Pop();

        toVisit.Push(Vector3.zero);

        parent.parent.SetData("Target", previousRoom);
        parent.parent.SetData("CurrentRoom", previousRoom);
        parent.parent.SetData("ToVisit", toVisit);
        parent.parent.SetData("VisitedRooms", visited);

        state = NodeState.SUCCESS;
        return state;
    }
}
