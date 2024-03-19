using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckCenterRoom : Node
{
    private Transform _transform;
    private string _roomTransform;

    public CheckCenterRoom(Transform transform, string roomTransform)
    {
        _transform = transform;
        _roomTransform = roomTransform;
    }

    public override NodeState Evaluate()
    {
        Vector3 currentRoomPosition = (Vector3)GetData(_roomTransform);
        Stack<Vector3> toVisit = new Stack<Vector3>((Stack<Vector3>)GetData("ToVisit"));
        List<Vector3> visited = new List<Vector3>((List<Vector3>)GetData("VisitedRooms"));

        if (Vector3.Distance(_transform.position, currentRoomPosition) < 0.1f)
        {
            if (!visited.Contains(currentRoomPosition))
            {
                //ClearData("ToVisit");
                //ClearData("VisitedRooms");

                toVisit.Push(currentRoomPosition);
                parent.parent.parent.SetData("ToVisit", toVisit);

                visited.Add(currentRoomPosition);
                parent.parent.parent.SetData("VisitedRooms", visited);
            }

            state = NodeState.SUCCESS;
            return state;
        }
        parent.parent.parent.SetData("Target", currentRoomPosition);

        state = NodeState.FAILURE;
        return state;
    }

}
