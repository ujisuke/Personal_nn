using Assets.Scripts.Objects;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyNotReadyState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private SetGameEnemyMain setGameEnemyMain;

        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            setGameEnemyMain = objectStateMachine.GetComponent<SetGameEnemyMain>();
        }

        public void FixedUpdate()
        {
            if(setGameEnemyMain.IsReady)
                objectStateMachine.TransitionTo(new SetGameEnemyMoveState());
        }

        public void Exit()
        {

        }
    }
}
