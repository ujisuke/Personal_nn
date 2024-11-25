using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Enemy1>().DestroyDeadObject();
            GetComponent<ObjectMove>().Stop();
        }
    }
}