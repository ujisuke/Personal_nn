using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3CleanedState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            ObjectStorage.RemoveEnemyAndDestroyDamageObject(objectStateMachine.GetComponent<EnemyMain>());
            objectStateMachine.GetComponent<ObjectCleaned>().enabled = true;
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
