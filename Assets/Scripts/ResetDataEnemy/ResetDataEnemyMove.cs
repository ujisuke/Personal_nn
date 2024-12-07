using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.ResetDataEnemy
{
    public class ResetDataEnemyMove : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
        }
    }
}
