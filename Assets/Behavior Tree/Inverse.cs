using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Inverse : Node
{
    public Inverse() : base() { }
    public Inverse(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        Node child = children[0];
        switch (child.Evaluate())
        {
            case NodeState.FAILURE:
                state = NodeState.SUCCESS;
                return state;
            case NodeState.SUCCESS:
                state = NodeState.FAILURE;
                return state;
            default:
                state = NodeState.SUCCESS;
                return state;
        }
    }

}
