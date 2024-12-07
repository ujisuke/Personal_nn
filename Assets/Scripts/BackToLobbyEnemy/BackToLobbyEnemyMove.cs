using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.BackToLobbyEnemy
{
    public class BackToLobbyEnemyMove : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
        }
    }
}
