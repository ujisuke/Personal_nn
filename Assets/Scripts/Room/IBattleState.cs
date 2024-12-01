using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Room
{
    public interface IRoomState
    {
        public UniTask Enter(RoomStateMachine roomStateMachine);
        public void FixedUpdate();
        public void Exit();
    }
}
