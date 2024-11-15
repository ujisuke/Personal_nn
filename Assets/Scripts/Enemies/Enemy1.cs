using UnityEngine;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1 : MonoBehaviour
    {
        private Enemy1Move enemy1Move;
        private Enemy1Attack enemy1Attack;

        private void Awake()
        {
            enemy1Move = GetComponent<Enemy1Move>();
            enemy1Attack = GetComponent<Enemy1Attack>();   
        }
    }
}
