using UnityEngine;
using System;
using Assets.Scripts.Stage;
using System.Collections.Generic;
using System.Linq;
using Assets.ScriptableObjects;


namespace Assets.Scripts.Objects
{
    public class ObjectMove : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;
        [SerializeField] private ObjectData objectData;
        private readonly int[,] _tileZs = StageCreator.TileZs;
        private bool isJumping = false;
        public bool IsJumping => isJumping;
        private bool isFalling = false;
        public bool IsFalling => isFalling;
        private float prevZ = 0;
        private SpriteRenderer objectSpriteRenderer;
        private SpriteRenderer shadowSpriteRenderer;
        private bool isHeadingToA = false;
        private bool isHeadingToW = false;
        private bool isHeadingToS = false;
        private bool isHeadingToD = false;
        private bool isTryingToJump = false;
        private int destinationTileZ = 0; 

        public void Initialize(Vector3 objectRePos3)
        {
            prevZ = objectRePos3.z;
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
            shadowSpriteRenderer = shadow.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            Vector3 objectImPos3 = ConvertToImPos3FromRePos3(transform.position);
            (int i, int j) objectTileNumber = ConvertToTileNumberFromImPos3(objectImPos3);
            
            Vector3 destinationRePos3 = GetDestinationRePos3();
            
            Vector3 destinationImPos3 = ConvertToImPos3FromRePos3(destinationRePos3);
            (int i, int j) destinationTileNumber = ConvertToTileNumberFromImPos3(destinationImPos3);

            int objectTileZ = _tileZs[objectTileNumber.i, objectTileNumber.j];
            int destinationJTileZ = _tileZs[objectTileNumber.i, destinationTileNumber.j];
            if(destinationJTileZ <= destinationImPos3.z - 1f)
            {
                objectImPos3.x = destinationImPos3.x;
                objectImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            else if(destinationJTileZ <= objectImPos3.z - 1f)
            {
                objectImPos3.x = destinationImPos3.x;
                objectImPos3.z = destinationJTileZ + 1f;
                isJumping = false;
            }
            else if(destinationImPos3.z - 1f < objectTileZ)
            {
                objectImPos3.z = objectTileZ + 1f;
                isJumping = false;
            }
            else
            {
                objectImPos3.z = destinationImPos3.z;
                isJumping = true;
            }

            objectTileNumber = ConvertToTileNumberFromImPos3(objectImPos3);
            objectTileZ = _tileZs[objectTileNumber.i, objectTileNumber.j];

            int destinationITileZ = _tileZs[destinationTileNumber.i, objectTileNumber.j];
            if(destinationITileZ <= destinationImPos3.z - 1f)
            {
                objectImPos3.y = destinationImPos3.y;
                objectImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            else if(destinationITileZ <= objectImPos3.z - 1f)
            {
                objectImPos3.y = destinationImPos3.y;
                objectImPos3.z = destinationITileZ + 1f;
                isJumping = false;
            }
            else if(destinationImPos3.z - 1f < objectTileZ)
            {
                objectImPos3.z = objectTileZ + 1f;
                isJumping = false;
                isFalling = false;
            }
            else
            {
                objectImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            
            objectTileNumber = ConvertToTileNumberFromImPos3(objectImPos3);
            objectTileZ = _tileZs[objectTileNumber.i, objectTileNumber.j];
            destinationTileZ = _tileZs[destinationTileNumber.i, destinationTileNumber.j];

            transform.position = ConvertToRePos3FromImPos3(objectImPos3);
            objectSpriteRenderer.sortingOrder = objectTileNumber.i + objectTileNumber.j;
            Vector3 shadowImPos3 = new(objectImPos3.x, objectImPos3.y, objectTileZ + 1f);
            shadow.transform.position = ConvertToRePos3FromImPos3(shadowImPos3);
            shadowSpriteRenderer.sortingOrder = objectTileNumber.i + objectTileNumber.j;
        }

        private Vector3 GetDestinationRePos3()
        {
            Vector3 newPos3 = new(transform.position.x, transform.position.y, transform.position.z);
            float weight = objectData.MoveSpeed * Time.deltaTime;
            if(isHeadingToW) newPos3.y += weight;
            if(isHeadingToS) newPos3.y -= weight;
            if(isHeadingToA) newPos3.x -= weight * 2;
            if(isHeadingToD) newPos3.x += weight * 2;
            if(isTryingToJump && !isJumping)
            {
                isJumping = true;
                isFalling = false;
                prevZ = newPos3.z;
                newPos3 += new Vector3(0, StageCreator._tileHeight, 1f)
                * (4f * objectData.JumpHeight / objectData.JumpTime * Time.deltaTime - 4f * objectData.JumpHeight / (objectData.JumpTime * objectData.JumpTime) * (Time.deltaTime * Time.deltaTime));
            }
            else
            {
                float tmpPrevZ = newPos3.z;
                newPos3 += new Vector3(0, StageCreator._tileHeight, 1f)
                * (newPos3.z - prevZ - 8f * objectData.JumpHeight / (objectData.JumpTime * objectData.JumpTime) * (Time.deltaTime * Time.deltaTime));
                prevZ = tmpPrevZ;
                isFalling = newPos3.z < prevZ;
            }
            return newPos3;
        }

        public static Vector3 ConvertToImPos3FromRePos3(Vector3 rePos3)
        {
            Vector2 newRePos2 = new(rePos3.x, rePos3.y - (rePos3.z - 1) * StageCreator._tileHeight);
            Vector3 newImPos3 = new(newRePos2.x - 2f * (newRePos2.y + StageCreator._tileHeight), newRePos2.x + 2f * (newRePos2.y + StageCreator._tileHeight), rePos3.z);
            newImPos3.x = Mathf.Clamp(newImPos3.x, -StageCreator._stageSide / 2f + 0.01f, StageCreator._stageSide / 2f - 0.01f);
            newImPos3.y = Mathf.Clamp(newImPos3.y, -StageCreator._stageSide / 2f + 0.01f, StageCreator._stageSide / 2f - 0.01f);
            return newImPos3;
        }

        private static (int i, int j) ConvertToTileNumberFromImPos3(Vector3 imPos3)
        {
            return (StageCreator._stageSide / 2 - (int)Math.Floor(imPos3.y) - 1, StageCreator._stageSide / 2 + (int)Math.Floor(imPos3.x));
        }

        public static Vector3 ConvertToRePos3FromImPos3(Vector3 imPos3)
        {
            Vector2 newRePos2 = new((imPos3.x + imPos3.y) * 0.5f, (imPos3.y - imPos3.x) * 0.25f - StageCreator._tileHeight);
            return new Vector3(newRePos2.x, newRePos2.y + (imPos3.z - 1) * StageCreator._tileHeight, imPos3.z);
        }

        private static (int i, int j) ConvertToTileNumberFromRePos3(Vector3 rePos3)
        {
            Vector3 newImPos3 = ConvertToImPos3FromRePos3(rePos3);
            return ConvertToTileNumberFromImPos3(newImPos3);
        }

        private static Vector3 ConvertToRePos3FromTileNumber((int i, int j) tileNumber)
        {
            Vector3 newImPos3 = new(-StageCreator._stageSide / 2 + tileNumber.j + 0.5f, StageCreator._stageSide / 2 - tileNumber.i - 0.5f, StageCreator.TileZs[tileNumber.i, tileNumber.j] + 1f);
            return ConvertToRePos3FromImPos3(newImPos3);
        }

        public static Vector3 CalclateImDirection3BetWeenTwoRePos3(Vector3 startRe3, Vector3 endRe3)
        {
            Vector3 startIm3 = ConvertToImPos3FromRePos3(startRe3);
            Vector3 endIm3 = ConvertToImPos3FromRePos3(endRe3);
            return endIm3 - startIm3;
        }

        public Vector3 DrawRePos3AroundRePos3(Vector3 rePos3)
        {
            List<Vector3> candidateTargetPos3List = new();
            (int I, int J) enemyTileNumber = ConvertToTileNumberFromRePos3(rePos3);
            for(int i = enemyTileNumber.I - 1; i < enemyTileNumber.I + 2; i++)
                for(int j = enemyTileNumber.J - 1; j < enemyTileNumber.J + 2; j++)
                {
                    if(i < 0 || StageCreator._stageSide <= i || j < 0 || StageCreator._stageSide <= j) continue;
                    if(i == enemyTileNumber.I && j == enemyTileNumber.J) continue;
                    Vector3 candidateTargetPos3 = ConvertToRePos3FromTileNumber((i, j));
                    if(IsReachable(candidateTargetPos3, rePos3)) candidateTargetPos3List.Add(candidateTargetPos3);
                }
            if(candidateTargetPos3List.Count == 0) return rePos3;
            candidateTargetPos3List = candidateTargetPos3List.OrderBy(a => Guid.NewGuid()).ToList();
            return candidateTargetPos3List[0];
        }

        public static bool IsHitWall(Vector3 imPos3)
        {
            if(imPos3.x < -StageCreator._stageSide / 2f || StageCreator._stageSide / 2f < imPos3.x
            || imPos3.y < -StageCreator._stageSide / 2f || StageCreator._stageSide / 2f < imPos3.y
            || imPos3.z < 0f || StageCreator._stageHeight < imPos3.z) return true;
            (int i, int j) tileNumber = ConvertToTileNumberFromImPos3(imPos3);
            return StageCreator.TileZs[tileNumber.i, tileNumber.j] > imPos3.z - 1f;
        }

        public bool IsReachable(Vector3 targetRePos3, Vector3 objectRePos3)
        {
            return targetRePos3.z < objectRePos3.z + objectData.JumpHeight;
        }
    
        public void HeadToA(bool isHeading)
        {
            isHeadingToA = isHeading;
        }    

        public void HeadToW(bool isHeading)
        {
            isHeadingToW = isHeading;
        }

        public void HeadToS(bool isHeading)
        {
            isHeadingToS = isHeading;
        }

        public void HeadToD(bool isHeading)
        {
            isHeadingToD = isHeading;
        }

        public void TryToJump(bool isTrying)
        {
            isTryingToJump = isTrying;
        }

        public bool IsDestinationTileZReachableWithJumping(Vector3 objectRePos3)
        {
            return destinationTileZ < objectRePos3.z - 1f + objectData.JumpHeight && objectRePos3.z - 1f < destinationTileZ;
        }
    }   
}

