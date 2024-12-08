using Assets.Scripts.Objects;

namespace Assets.Scripts.UIEnemies
{
    public class UIEnemyDeadState : IObjectState
    {
        public void Enter(ObjectStateMachine objectStateMachine)
        {
            objectStateMachine.GetComponent<ObjectDead>().enabled = true;
            ObjectStorage.RemoveEnemyAndDestroyDamageObject(objectStateMachine.GetComponent<EnemyMain>());
            objectStateMachine.GetComponent<UIEnemyAnimation>().StartDead();
            objectStateMachine.GetComponent<IEvent>().Execute();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
