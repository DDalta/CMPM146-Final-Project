using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class AgentBT : BTree
{
    public UnityEngine.AI.NavMeshAgent agent;
    public AgentSensor sensor;

    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform, sensor.Objects),
                new WalkToTarget(transform, "Target", agent),
            }),
            new Sequence(new List<Node>
            {
                new CheckAtTarget(transform, agent),
                new CheckTreasureInFOV(transform, sensor.Objects),
                new WalkToTarget(transform, "Target", agent),
            }),
            new Sequence(new List<Node>
            {
                new CheckAtTarget(transform, agent),
                new Selector(new List<Node>
                {
                    new CheckCenterRoom(transform, "CurrentRoom"),
                    new Inverse(new List<Node>
                    {
                        new WalkToTarget(transform, "Target", agent)
                    }),
                }),
                new ChooseRoom(sensor.Objects),
                new WalkToTarget(transform, "Target", agent)
            })
        });

        List<Vector3> visited = new List<Vector3>();
        visited.Add(Vector3.zero);

        root.SetData("CurrentRoom", new Vector3(0, 0, 0));
        root.SetData("VisitedRooms", new List<Vector3>());
        root.SetData("ToVisit", new Stack<Vector3>());

        return root;
    }
}
