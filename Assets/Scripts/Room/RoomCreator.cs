using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.Room
{
    public class RoomCreator : MonoBehaviour
    {
        private void Start()
        {
            Test().Forget();
        }

        public async UniTask Test()
        {
            await A();
            Debug.Log("D");
        }

        private UniTask A()
        {
            Debug.Log("A");
            Debug.Log("B");
            Debug.Log("C");
            return UniTask.CompletedTask;
        }
    }
}