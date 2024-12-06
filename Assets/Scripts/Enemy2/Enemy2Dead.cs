using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
            GetComponent<Enemy2Main>().DestroyDeadObject();
        }
    }
}