using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Enemy3>().DestroyObject();
        }
    }
}