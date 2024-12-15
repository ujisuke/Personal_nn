using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PoseStateMachine : MonoBehaviour
    {
        private IPoseState currentState;

        public void Awake()
        {
            currentState = new PoseMainState();
            currentState.Enter(this);
        }

        public void TransitionTo(IPoseState newState)
        {
            currentState.Exit();
            currentState = newState;
            newState.Enter(this);
        }

        private void Update()
        {
            currentState.Update();
        }
    }
}