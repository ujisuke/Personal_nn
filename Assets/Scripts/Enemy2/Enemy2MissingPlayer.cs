using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2MissingPlayer : MonoBehaviour
    {
        private Enemy2Parameter enemy2Parameter;
        private ObjectMove objectMove;
        private bool isMissingPlayer = true;
        public bool IsMissingPlayer => isMissingPlayer;
        
        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            this.enemy2Parameter = enemy2Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            isMissingPlayer = true;
            objectMove.HeadToPlusImX(false);
            objectMove.HeadToMinusImX(false);
            objectMove.HeadToPlusImY(false);
            objectMove.HeadToMinusImY(false);
            objectMove.TryToJump(false);
            await UniTask.Delay(TimeSpan.FromSeconds(enemy2Parameter.MissingPlayerTime));
            isMissingPlayer = false;
        }
    }
}
