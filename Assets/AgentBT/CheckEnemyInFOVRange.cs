using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = LayerMask.NameToLayer("Enemy");
    private static int _playerLayerMask = LayerMask.NameToLayer("Player");
    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

        /*
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider[] colliders = Collider2D.IsTouchingLayers(_enemyLayerMask, _playerLayerMask);//check if enemy layermask and the player's vision 2d collider collide. If they do then we return the state as
            //having a target to run away from and sets walking to be true as a reminder to RUN AWAY
            

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
        */

}