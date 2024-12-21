using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Effect
{
    public class ViewEffect : MonoBehaviour
    {
        [SerializeField] private float _shakeRange = 0f;
        [SerializeField] private float _enemyTakeDamageHitStopTime = 0f;
        public float EnemyTakeDamageHitStopTime => _enemyTakeDamageHitStopTime;
        [SerializeField] private float _playerTakeDamageHitStopTime = 0f;
        [SerializeField] private float _hitStopTimeScale = 0f;
        private Camera mainCamera;
        private static ViewEffect singletonInstance;
        public static ViewEffect SingletonInstance => singletonInstance;

        private void Awake()
        {
            mainCamera = GetComponent<Camera>();
            singletonInstance = this;
        }

        public async UniTask ShakeView()
        {
            var initialPos = mainCamera.transform.position;
            mainCamera.transform.position = new Vector3(initialPos.x, initialPos.y + _shakeRange, initialPos.z);
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f), ignoreTimeScale: true);
            mainCamera.transform.position = initialPos;
        }

        public async UniTask HitStop(float time)
        {
            Time.timeScale = _hitStopTimeScale;
            await UniTask.Delay(TimeSpan.FromSeconds(time) * Time.timeScale);
            Time.timeScale = 1f;
        }

        public void PlayerTakeDamage()
        {
            ShakeView().Forget();
        }

        public async UniTask PlayerTakeFatalDamage(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await HitStop(_playerTakeDamageHitStopTime).SuppressCancellationThrow();
        }

        public async UniTask EnemyTakeFatalDamage(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await HitStop(_enemyTakeDamageHitStopTime).SuppressCancellationThrow();
        }
    }
}