using UnityEngine;
using Assets.Scripts.Enemies;
using Assets.Scripts.Player;

namespace Assets.Scripts.Objects
{
    public class ObjectStateMachine : MonoBehaviour
    {
        private IObjectState currentState;
        [SerializeField] private ObjectStateList initialState;

        private void Awake()
        {
            if(initialState == ObjectStateList.PlayerMove)
                currentState = new PlayerNotReadyState();
            else if(initialState == ObjectStateList.Enemy1Move)
                currentState = new Enemy1NotReadyState();
            else if(initialState == ObjectStateList.Enemy2Move)
                currentState = new Enemy2NotReadyState();
            else if(initialState == ObjectStateList.Enemy3Move)
                currentState = new Enemy3NotReadyState();
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
        PlayerMove,
        Enemy1Move,
        Enemy2Move,
        Enemy3Move,
    }
}