using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyMove : MonoBehaviour
    {
        private ObjectMove objectMove;

        public void Initialize()
        {
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            objectMove.Stop();
        }
    }
}
