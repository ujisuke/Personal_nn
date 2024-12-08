using Assets.ScriptableObjects;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Initializer : MonoBehaviour
    {
        [SerializeField] private Enemy3Parameter _enemy3Parameter;
        private void Awake()
        {
            GetComponent<ObjectStateMachine>().Initialize(new Enemy3ReadyState());
            GetComponent<EnemyMain>().Initialize(_enemy3Parameter);
            GetComponent<Enemy3Attack>().Initialize(_enemy3Parameter);
            GetComponent<Enemy3Move>().Initialize(_enemy3Parameter);
            GetComponent<Enemy3Animation>().Initialize(_enemy3Parameter);
        }
    }
}