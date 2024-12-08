using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1DeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemoveEnemyAndDestroyDamageObject(objectStateMachine.GetComponent<EnemyMain>());
            objectStateMachine.GetComponent<ObjectDead>().enabled = true;
            objectStateMachine.GetComponent<Enemy1Animation>().StartDead();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
