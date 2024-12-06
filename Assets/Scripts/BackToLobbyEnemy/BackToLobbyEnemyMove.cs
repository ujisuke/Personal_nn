using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyMove : MonoBehaviour
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
