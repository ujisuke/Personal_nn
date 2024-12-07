using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.StartBattleEnemy
{
    public class StartBattleEnemyMove : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
        }
    }
}
