using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{
    // Possible node states: running -> node is running, success -> node runned successfuly, failure -> node run failed
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        // shared data
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        // default class constructor (empty node)
        public Node()
        {
            // null parent
            parent = null;
        }

        // When given a list of nodes, make each node a child of this node
        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                _Attach(child);
            }
        }

        // binds child to its parent node
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        // when creating new node types, overwrite this function
        // each node type will have its own evaluation function
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // stores object in dictionary
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        // this function will check this node and all of its predecessor's
        // dictionaries to find the value mapped to the given key
        // (im gonna change this function cuz i dont like how the tutorial implemented this)
        public object GetData(string key)
        {
            object value = null;
            // check if object stored in this node
            if (_dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            // iterate up the tree until value is found or reached root node
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                // value is found and returned
                if (value != null)
                {
                    return value;
                }
                // try the next parent
                node = node.parent;
            }

            // value was never found return null
            return null;
        }

        // uses the same method above but removes the key:value mapping instead
        // also gonna change this
        public bool ClearData(string key)
        {
            // remove if in 
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }
    }
}
