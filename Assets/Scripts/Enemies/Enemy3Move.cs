using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Move : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool canAttack = false;
        public bool CanAttack => canAttack;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
            StartCoroutine(TrackPlayer());
        }

        private IEnumerator TrackPlayer()
        { 
            yield return new WaitForSeconds(1f);
            canAttack = true;
        }
    
        private void FixedUpdate()
        {
            objectMove.HeadToPlusX(false);
            objectMove.HeadToMinusX(false);
            objectMove.HeadToPlusY(false);
            objectMove.HeadToMinusY(false);
            objectMove.TryToJump(false);
        }
    }
}
