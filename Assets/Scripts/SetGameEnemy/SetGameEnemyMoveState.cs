using Assets.Scripts.Objects;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyMoveState : IObjectState
    {
        private ObjectStateMachine objectStateMachine;
        private SetGameEnemyMove setGameEnemyMove;
        private SetGameEnemyMain setGameEnemyMain;
        private SetGameEnemyAnimation setGameEnemyAnimation;
        
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            this.objectStateMachine = objectStateMachine;
            setGameEnemyMove = objectStateMachine.GetComponent<SetGameEnemyMove>();
            setGameEnemyMain = objectStateMachine.GetComponent<SetGameEnemyMain>();
            setGameEnemyAnimation = objectStateMachine.GetComponent<SetGameEnemyAnimation>();
            setGameEnemyMove.enabled = true;
            setGameEnemyAnimation.StartWalk();
        }

        public void FixedUpdate()
        {
            if(setGameEnemyMain.IsDead())
                objectStateMachine.TransitionTo(new SetGameEnemyDeadState());
        }

        public void Exit()
        {
            setGameEnemyMove.enabled = false;
            setGameEnemyAnimation.StopWalk();
        }
    }
}
