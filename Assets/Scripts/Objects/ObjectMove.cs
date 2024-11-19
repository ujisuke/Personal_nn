using UnityEngine;
using System;
using Assets.Scripts.Stage;
using System.Collections.Generic;
using System.Linq;
using Assets.ScriptableObjects;
using Unity.Mathematics;


namespace Assets.Scripts.Objects
{
    public class ObjectMove : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;
        [SerializeField] private ObjectParameter objectData;
        private readonly int[,] _tileZs = StageFacade.TileZs;
        private bool isJumping = false;
        public bool IsJumping => isJumping;
        private bool isFalling = false;
        public bool IsFalling => isFalling;
        private float prevZ = 0;
        private SpriteRenderer objectSpriteRenderer;
        private SpriteRenderer shadowSpriteRenderer;
        private bool isHeadingToMinusX = false;
        private bool isHeadingToPlusY = false;
        private bool isHeadingToMinusY = false;
        private bool isHeadingToPlusX = false;
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
            (int i, int j) objectTileIndex = ConvertToTileIndexFromImPos3(objectImPos3);
            
            Vector3 destinationImPos3 = GetDestinationImPos3();
            (int i, int j) destinationTileIndex = ConvertToTileIndexFromImPos3(destinationImPos3);

            int objectTileZ = _tileZs[objectTileIndex.i, objectTileIndex.j];
            int destinationJTileZ = _tileZs[objectTileIndex.i, destinationTileIndex.j];
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

            objectTileIndex = ConvertToTileIndexFromImPos3(objectImPos3);
            objectTileZ = _tileZs[objectTileIndex.i, objectTileIndex.j];

            int destinationITileZ = _tileZs[destinationTileIndex.i, objectTileIndex.j];
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
            
            objectTileIndex = ConvertToTileIndexFromImPos3(objectImPos3);
            objectTileZ = _tileZs[objectTileIndex.i, objectTileIndex.j];
            destinationTileZ = _tileZs[destinationTileIndex.i, destinationTileIndex.j];

            transform.position = ConvertToRePos3FromImPos3(objectImPos3);
            objectSpriteRenderer.sortingOrder = objectTileIndex.i + objectTileIndex.j;
            Vector3 shadowImPos3 = new(objectImPos3.x, objectImPos3.y, objectTileZ + 1f);
            shadow.transform.position = ConvertToRePos3FromImPos3(shadowImPos3);
            shadowSpriteRenderer.sortingOrder = objectTileIndex.i + objectTileIndex.j;
        }

        private Vector3 GetDestinationImPos3()
        {
            Vector3 newImPos3 = ConvertToImPos3FromRePos3(transform.position);
            
            float weight = objectData.MoveSpeed * Time.deltaTime;
            if(isHeadingToPlusY) newImPos3.y += weight;
            if(isHeadingToMinusY) newImPos3.y -= weight;
            if(isHeadingToMinusX) newImPos3.x -= weight;
            if(isHeadingToPlusX) newImPos3.x += weight;
            if(isTryingToJump && !isJumping)
            {
                isJumping = true;
                isFalling = false;
                prevZ = newImPos3.z;
                newImPos3.z += 4f * objectData.JumpHeight / objectData.JumpTime * Time.deltaTime - 4f * objectData.JumpHeight / (objectData.JumpTime * objectData.JumpTime) * (Time.deltaTime * Time.deltaTime);
            }
            else
            {
                float tmpPrevZ = newImPos3.z;
                newImPos3.z += newImPos3.z - prevZ - 8f * objectData.JumpHeight / (objectData.JumpTime * objectData.JumpTime) * (Time.deltaTime * Time.deltaTime);
                prevZ = tmpPrevZ;
                isFalling = newImPos3.z < prevZ && newImPos3.z > _tileZs[ConvertToTileIndexFromImPos3(newImPos3).i, ConvertToTileIndexFromImPos3(newImPos3).j] + 1f;
            }
            return newImPos3;
        }

        public static Vector3 ConvertToImPos3FromRePos3(Vector3 rePos3)
        {
            Vector2 newRePos2 = new(rePos3.x, rePos3.y - (rePos3.z - 1) * StageFacade._tileHeight);
            Vector3 newImPos3 = new(newRePos2.x - 2f * (newRePos2.y + StageFacade._tileHeight), newRePos2.x + 2f * (newRePos2.y + StageFacade._tileHeight), rePos3.z);
            newImPos3.x = Mathf.Clamp(newImPos3.x, -StageFacade._stageSide / 2f + 0.01f, StageFacade._stageSide / 2f - 0.01f);
            newImPos3.y = Mathf.Clamp(newImPos3.y, -StageFacade._stageSide / 2f + 0.01f, StageFacade._stageSide / 2f - 0.01f);
            return newImPos3;
        }

        private static (int i, int j) ConvertToTileIndexFromImPos3(Vector3 imPos3)
        {
            imPos3.x = Mathf.Clamp(imPos3.x, -StageFacade._stageSide / 2f + 0.01f, StageFacade._stageSide / 2f - 0.01f);
            imPos3.y = Mathf.Clamp(imPos3.y, -StageFacade._stageSide / 2f + 0.01f, StageFacade._stageSide / 2f - 0.01f);
            return (StageFacade._stageSide / 2 - (int)Math.Floor(imPos3.y) - 1, StageFacade._stageSide / 2 + (int)Math.Floor(imPos3.x));
        }

        public static Vector3 ConvertToRePos3FromImPos3(Vector3 imPos3)
        {
            Vector2 newRePos2 = new((imPos3.x + imPos3.y) * 0.5f, (imPos3.y - imPos3.x) * 0.25f - StageFacade._tileHeight);
            return new Vector3(newRePos2.x, newRePos2.y + (imPos3.z - 1) * StageFacade._tileHeight, imPos3.z);
        }

        public static (int i, int j) ConvertToTileIndexFromRePos3(Vector3 rePos3)
        {
            Vector3 newImPos3 = ConvertToImPos3FromRePos3(rePos3);
            return ConvertToTileIndexFromImPos3(newImPos3);
        }

        public static Vector3 ConvertToRePos3FromTileIndex((int i, int j) tileNumber)
        {
            Vector3 newImPos3 = new(-StageFacade._stageSide / 2 + tileNumber.j + 0.5f, StageFacade._stageSide / 2 - tileNumber.i - 0.5f, StageFacade.TileZs[tileNumber.i, tileNumber.j] + 1f);
            return ConvertToRePos3FromImPos3(newImPos3);
        }

        public static Vector3 CalclateImDirection3BetWeenTwoRePos3(Vector3 startRe3, Vector3 endRe3)
        {
            Vector3 startIm3 = ConvertToImPos3FromRePos3(startRe3);
            Vector3 endIm3 = ConvertToImPos3FromRePos3(endRe3);
            return endIm3 - startIm3;
        }

        public static List<Vector3> DrawSomeRePos3AtRandom(int PosNumber, (int i, int j) pivotTileNumber, int minimumRadius, int maximumRadius)
        {
            List<(int i, int j)> tileNumberList = new();
            for(int i = 0; i < StageFacade._stageSide; i++)
                for(int j = 0; j < StageFacade._stageSide; j++)
                {
                    if(Math.Abs(i - pivotTileNumber.i) < minimumRadius && Math.Abs(j - pivotTileNumber.j) < minimumRadius) continue;
                    if(Math.Abs(i - pivotTileNumber.i) > maximumRadius || Math.Abs(j - pivotTileNumber.j) > maximumRadius) continue;
                    tileNumberList.Add((i, j));
                }
            tileNumberList = tileNumberList.OrderBy(a => Guid.NewGuid()).ToList();
            List<Vector3> rePos3List = new();
            for(int i = 0; i < math.min(PosNumber, tileNumberList.Count); i++)
            {
                Vector3 rePos3 = ConvertToRePos3FromTileIndex(tileNumberList[i]);
                rePos3List.Add(rePos3);
            }
            return rePos3List;
        }

        public Vector3 DrawRePos3AroundRePos3(Vector3 rePos3)
        {
            List<Vector3> candidateTargetPos3List = new();
            (int I, int J) enemyTileNumber = ConvertToTileIndexFromRePos3(rePos3);
            for(int i = enemyTileNumber.I - 1; i < enemyTileNumber.I + 2; i++)
                for(int j = enemyTileNumber.J - 1; j < enemyTileNumber.J + 2; j++)
                {
                    if(i < 0 || StageFacade._stageSide <= i || j < 0 || StageFacade._stageSide <= j) continue;
                    if(i == enemyTileNumber.I && j == enemyTileNumber.J) continue;
                    Vector3 candidateTargetPos3 = ConvertToRePos3FromTileIndex((i, j));
                    if(IsReachable(candidateTargetPos3, rePos3)) candidateTargetPos3List.Add(candidateTargetPos3);
                }
            if(candidateTargetPos3List.Count == 0) return rePos3;
            candidateTargetPos3List = candidateTargetPos3List.OrderBy(a => Guid.NewGuid()).ToList();
            return candidateTargetPos3List[0];
        }

        public static bool IsHitWall(Vector3 imPos3)
        {
            if(imPos3.x < -StageFacade._stageSide / 2f || StageFacade._stageSide / 2f < imPos3.x
            || imPos3.y < -StageFacade._stageSide / 2f || StageFacade._stageSide / 2f < imPos3.y
            || imPos3.z < 0f || StageFacade._stageHeight < imPos3.z) return true;
            (int i, int j) tileNumber = ConvertToTileIndexFromImPos3(imPos3);
            return StageFacade.TileZs[tileNumber.i, tileNumber.j] > imPos3.z - 1f;
        }

        public static bool IsHitStage(Vector3 imPos3)
        {
            return imPos3.x < -StageFacade._stageSide / 2f || StageFacade._stageSide / 2f < imPos3.x
            || imPos3.y < -StageFacade._stageSide / 2f || StageFacade._stageSide / 2f < imPos3.y
            || imPos3.z < 0f || StageFacade._stageHeight < imPos3.z;
        }

        public bool IsReachable(Vector3 targetRePos3, Vector3 objectRePos3)
        {
            return targetRePos3.z < objectRePos3.z + objectData.JumpHeight;
        }
    
        public void HeadToMinusX(bool isHeading)
        {
            isHeadingToMinusX = isHeading;
        }    

        public void HeadToPlusY(bool isHeading)
        {
            isHeadingToPlusY = isHeading;
        }

        public void HeadToMinusY(bool isHeading)
        {
            isHeadingToMinusY = isHeading;
        }

        public void HeadToPlusX(bool isHeading)
        {
            isHeadingToPlusX = isHeading;
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

