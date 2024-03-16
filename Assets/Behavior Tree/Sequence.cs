using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        // override evaluate method from Node class
        public override NodeState Evaluate()
        {
            bool anyChildRunning = false;

            // evaluate each child node
            // if one fails return FAILURE
            // continue looping if child return success or is running
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            // if anyChildRunning == true -> this node is still running
            // else -> this node has succeeded
            state = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}