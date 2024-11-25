using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Player
{
    public class PlayerDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Player>().DestroyDeadObject();
            GetComponent<ObjectMove>().Stop();
        }

    }
}