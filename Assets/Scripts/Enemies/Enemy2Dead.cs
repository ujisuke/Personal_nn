using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Enemy2>().DestroyDeadObject();
            GetComponent<ObjectMove>().Stop();
        }
    }
}