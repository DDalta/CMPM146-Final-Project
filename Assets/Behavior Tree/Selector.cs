using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        // override evaluate method from Node class
        public override NodeState Evaluate()
        {

            // evaluate each child node
            // if one succeeds return SUCCESS
            // continue looping if child return failure or is running
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        continue;
                    default:
                        continue;
                }
            }

            // if no success return FAILURE
            state = NodeState.FAILURE;
            return state;
        }
    }
}
