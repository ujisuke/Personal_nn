using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyMove : MonoBehaviour
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
