using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Dead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Enemy2>().DestroyObject();
        }
    }
}