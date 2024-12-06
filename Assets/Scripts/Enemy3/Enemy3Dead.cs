using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
            GetComponent<Enemy3Main>().DestroyDeadObject();
        }
    }
}