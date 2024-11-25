using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Enemy1Main>().DestroyDeadObject();
            GetComponent<ObjectMove>().Stop();
        }
    }
}