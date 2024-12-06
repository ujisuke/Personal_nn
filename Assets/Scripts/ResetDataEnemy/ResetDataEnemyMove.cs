using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyMove : MonoBehaviour
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
