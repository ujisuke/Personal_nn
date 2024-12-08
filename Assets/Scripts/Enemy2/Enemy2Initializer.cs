using Assets.ScriptableObjects;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Initializer : MonoBehaviour
    {
        [SerializeField] private Enemy2Parameter _enemy2Parameter;
        private void Awake()
        {
            GetComponent<ObjectStateMachine>().Initialize(new Enemy2ReadyState());
            GetComponent<EnemyMain>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2Attack>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2Move>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2MissingPlayer>().Initialize(_enemy2Parameter);
            GetComponent<Enemy2Animation>().Initialize(_enemy2Parameter);
        }
    }
}