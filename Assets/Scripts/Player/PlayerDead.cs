using UnityEngine;
using Assets.Scripts.Objects;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class PlayerDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<PlayerMain>().DestroyDeadObject().Forget();
            GetComponent<ObjectMove>().Dead();
        }

    }
}