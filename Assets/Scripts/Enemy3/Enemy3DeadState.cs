using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3DeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemoveEnemyAndDestroyDamageObject(objectStateMachine.GetComponent<EnemyMain>());
            objectStateMachine.GetComponent<ObjectDead>().enabled = true;
            objectStateMachine.GetComponent<Enemy3Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
