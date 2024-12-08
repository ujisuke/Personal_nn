using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2DeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemoveEnemyAndDestroyDamageObject(objectStateMachine.GetComponent<EnemyMain>());
            objectStateMachine.GetComponent<ObjectDead>().enabled = true;
            objectStateMachine.GetComponent<Enemy2Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
