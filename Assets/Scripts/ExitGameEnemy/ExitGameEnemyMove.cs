using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyMove : MonoBehaviour
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
