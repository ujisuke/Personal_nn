using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.SetGameEnemy
{
    public class SetGameEnemyMove : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
        }
    }
}
