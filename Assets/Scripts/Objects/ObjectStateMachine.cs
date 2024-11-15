using UnityEngine;
using Assets.Scripts.Enemies;

namespace Assets.Scripts.Objects
{
    public class ObjectStateMachine : MonoBehaviour
    {
        private IObjectState currentState;
        [SerializeField] private ObjectStateList initialState;

        private void Awake()
        {
            if(initialState == ObjectStateList.Enemy1Move)
                currentState = new Enemy1MoveState();
            else if(initialState == ObjectStateList.Enemy2Move)
                currentState = new Enemy2MoveState();
            currentState.Enter(this);
        }

        public void TransitionTo(IObjectState newState)
        {
            currentState.Exit();
            currentState = newState;
            newState.Enter(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }

    public enum ObjectStateList
    {
        Enemy1Move,
        Enemy2Move
    }
}