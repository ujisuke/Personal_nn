using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Enemy3>().DestroyDeadObject();
            GetComponent<ObjectMove>().Stop();
        }
    }
}