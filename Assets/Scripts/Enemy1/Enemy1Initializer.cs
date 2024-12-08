using Assets.ScriptableObjects;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Initializer : MonoBehaviour
    {
        [SerializeField] private Enemy1Parameter _enemy1Parameter;
        private void Awake()
        {
            GetComponent<ObjectStateMachine>().Initialize(new Enemy1ReadyState());
            GetComponent<EnemyMain>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Attack>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Move>().Initialize(_enemy1Parameter);
            GetComponent<Enemy1Animation>().Initialize(_enemy1Parameter);
        }
    }
}