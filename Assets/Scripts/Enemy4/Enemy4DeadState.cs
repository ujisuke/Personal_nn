using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4DeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemoveEnemyAndDestroyDamageObject(objectStateMachine.GetComponent<EnemyMain>());
            objectStateMachine.GetComponent<ObjectDead>().enabled = true;
            objectStateMachine.GetComponent<Enemy4Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
