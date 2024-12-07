using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyMove : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
        }
    }
}
