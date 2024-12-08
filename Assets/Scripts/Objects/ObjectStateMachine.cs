using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectStateMachine : MonoBehaviour
    {
        private IObjectState currentState;

        public void Initialize(IObjectState initialState)
        {
            currentState = initialState;
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
}