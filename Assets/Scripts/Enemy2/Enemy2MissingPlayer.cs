using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;

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

        private void OnEnable()
        {
            isMissingPlayer = true;
            objectMove.HeadToPlusImX(false);
            objectMove.HeadToMinusImX(false);
            objectMove.HeadToPlusImY(false);
            objectMove.HeadToMinusImY(false);
            objectMove.TryToJump(false);
            StartCoroutine(Stand());
        }

        private IEnumerator Stand()
        {   
            yield return new WaitForSeconds(enemy2Parameter.MissingPlayerTime);
            isMissingPlayer = false;
        }
    }
}
