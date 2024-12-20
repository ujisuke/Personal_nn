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
        private ObjectParameter _objectParameter;
        private readonly int[,] _tileImZs = StageCreator.TileImZs;
        private bool isJumping = false;
        public bool IsJumping => isJumping;
        private bool isFalling = false;
        public bool IsFalling => isFalling;
        private float prevImZ = 0;
        private SpriteRenderer objectSpriteRenderer;
        private bool isHeadingToMinusImX = false;
        private bool isHeadingToPlusImY = false;
        private bool isHeadingToMinusImY = false;
        private bool isHeadingToPlusImX = false;
        private bool isTryingToJump = false;
        private int destinationTileImZ = 0;
        private bool isDead = false;

        public void Initialize(ObjectParameter objectParameter, Vector3 objectRePos3)
        {
            _objectParameter = objectParameter;
            prevImZ = objectRePos3.z;
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if(isDead) return;
            Vector3 objectImPos3 = ConvertToImPos3FromRePos3(transform.position);
            (int i, int j) objectTileIndex = ConvertToTileIndexFromImPos3(objectImPos3);
            
            Vector3 destinationImPos3 = GetDestinationImPos3();
            (int i, int j) destinationTileIndex = ConvertToTileIndexFromImPos3(destinationImPos3);

            int objectTileZ = _tileImZs[objectTileIndex.i, objectTileIndex.j];
            int destinationJTileZ = _tileImZs[objectTileIndex.i, destinationTileIndex.j];
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
            objectTileZ = _tileImZs[objectTileIndex.i, objectTileIndex.j];

            int destinationITileZ = _tileImZs[destinationTileIndex.i, objectTileIndex.j];
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
            destinationTileImZ = _tileImZs[destinationTileIndex.i, destinationTileIndex.j];
            transform.position = ConvertToRePos3FromImPos3(objectImPos3);
            objectSpriteRenderer.sortingOrder = CalculateSortingOrderFromRePos3(transform.position);
        }

        private Vector3 GetDestinationImPos3()
        {
            Vector3 newImPos3 = ConvertToImPos3FromRePos3(transform.position);
            
            float weight = _objectParameter.MoveSpeed * Time.deltaTime;
            if(isHeadingToPlusImY) newImPos3.y += weight;
            if(isHeadingToMinusImY) newImPos3.y -= weight;
            if(isHeadingToMinusImX) newImPos3.x -= weight;
            if(isHeadingToPlusImX) newImPos3.x += weight;
            if(isTryingToJump && !isJumping)
            {
                isJumping = true;
                isFalling = false;
                prevImZ = newImPos3.z;
                newImPos3.z += 4f * _objectParameter.JumpHeight / _objectParameter.JumpTime * Time.deltaTime - 4f * _objectParameter.JumpHeight / (_objectParameter.JumpTime * _objectParameter.JumpTime) * (Time.deltaTime * Time.deltaTime);
            }
            else
            {
                float tmpPrevZ = newImPos3.z;
                newImPos3.z += newImPos3.z - prevImZ - 8f * _objectParameter.JumpHeight / (_objectParameter.JumpTime * _objectParameter.JumpTime) * (Time.deltaTime * Time.deltaTime);
                prevImZ = tmpPrevZ;
                isFalling = newImPos3.z < prevImZ && newImPos3.z > _tileImZs[ConvertToTileIndexFromImPos3(newImPos3).i, ConvertToTileIndexFromImPos3(newImPos3).j] + 1f;
            }
            return newImPos3;
        }

        public static Vector3 ConvertToImPos3FromRePos3(Vector3 rePos3)
        {
            Vector2 newRePos2 = new(rePos3.x, rePos3.y - (rePos3.z - 1f) * StageCreator._TileHeight + StageCreator._YOffset);
            Vector3 newImPos3 = new(newRePos2.x - 2f * (newRePos2.y + StageCreator._TileHeight), newRePos2.x + 2f * (newRePos2.y + StageCreator._TileHeight), rePos3.z);
            newImPos3.x = Mathf.Clamp(newImPos3.x, -StageCreator._StageSide / 2f + 0.01f, StageCreator._StageSide / 2f - 0.01f);
            newImPos3.y = Mathf.Clamp(newImPos3.y, -StageCreator._StageSide / 2f + 0.01f, StageCreator._StageSide / 2f - 0.01f);
            return newImPos3;
        }

        private static (int i, int j) ConvertToTileIndexFromImPos3(Vector3 imPos3)
        {
            imPos3.x = Mathf.Clamp(imPos3.x, -StageCreator._StageSide / 2f + 0.01f, StageCreator._StageSide / 2f - 0.01f);
            imPos3.y = Mathf.Clamp(imPos3.y, -StageCreator._StageSide / 2f + 0.01f, StageCreator._StageSide / 2f - 0.01f);
            return (StageCreator._StageSide / 2 - (int)Math.Floor(imPos3.y) - 1, StageCreator._StageSide / 2 + (int)Math.Floor(imPos3.x));
        }

        public static Vector3 ConvertToRePos3FromImPos3(Vector3 imPos3)
        {
            Vector2 newRePos2 = new((imPos3.x + imPos3.y) * 0.5f, (imPos3.y - imPos3.x) * 0.25f - StageCreator._TileHeight);
            return new Vector3(newRePos2.x, newRePos2.y + (imPos3.z - 1) * StageCreator._TileHeight - StageCreator._YOffset, imPos3.z);
        }

        public static (int i, int j) ConvertToTileIndexFromRePos3(Vector3 rePos3)
        {
            Vector3 newImPos3 = ConvertToImPos3FromRePos3(rePos3);
            return ConvertToTileIndexFromImPos3(newImPos3);
        }

        public static Vector3 ConvertToRePos3FromTileIndex((int i, int j) tileIndex)
        {
            Vector3 newImPos3 = new(-StageCreator._StageSide / 2 + tileIndex.j + 0.5f, StageCreator._StageSide / 2 - tileIndex.i - 0.5f, StageCreator.TileImZs[tileIndex.i, tileIndex.j] + 1f);
            return ConvertToRePos3FromImPos3(newImPos3);
        }

        public static Vector3 ConvertToTileRePos3FromImPos3(Vector3 imPos3)
        {
            (int i, int j) tileIndex = ConvertToTileIndexFromImPos3(imPos3);
            return ConvertToRePos3FromTileIndex(tileIndex);
        }

        public static Vector3 CalculateImDirection3BetWeenTwoRePos3(Vector3 startRe3, Vector3 endRe3)
        {
            Vector3 startIm3 = ConvertToImPos3FromRePos3(startRe3);
            Vector3 endIm3 = ConvertToImPos3FromRePos3(endRe3);
            return endIm3 - startIm3;
        }

        public static List<Vector3> DrawSomeRePos3AtRandom(int PosCount, (int i, int j) pivotTileNumber, int minimumRadius, int maximumRadius)
        {
            List<(int i, int j)> tileIndexList = new();
            for(int i = 0; i < StageCreator._StageSide; i++)
                for(int j = 0; j < StageCreator._StageSide; j++)
                {
                    if(Math.Abs(i - pivotTileNumber.i) < minimumRadius && Math.Abs(j - pivotTileNumber.j) < minimumRadius) continue;
                    if(Math.Abs(i - pivotTileNumber.i) > maximumRadius || Math.Abs(j - pivotTileNumber.j) > maximumRadius) continue;
                    tileIndexList.Add((i, j));
                }
            List<(int i, int j)> shuffledTileIndexList = new();
            for(int i = 0; i < tileIndexList.Count; i++)
            {
                int r = UnityEngine.Random.Range(0, tileIndexList.Count - i);
                shuffledTileIndexList.Add(tileIndexList[r]);
                tileIndexList.RemoveAt(r);
            }
            
            List<Vector3> rePos3List = new();
            for(int i = 0; i < math.min(PosCount, shuffledTileIndexList.Count); i++)
            {
                Vector3 rePos3 = ConvertToRePos3FromTileIndex(shuffledTileIndexList[i]);
                rePos3List.Add(rePos3);
            }
            return rePos3List;
        }

        public Vector3 DrawRePos3AroundRePos3(Vector3 rePos3)
        {
            List<Vector3> candidateTargetPos3List = new();
            (int I, int J) enemyTileIndex = ConvertToTileIndexFromRePos3(rePos3);
            for(int i = enemyTileIndex.I - 1; i < enemyTileIndex.I + 2; i++)
                for(int j = enemyTileIndex.J - 1; j < enemyTileIndex.J + 2; j++)
                {
                    if(i < 0 || StageCreator._StageSide <= i || j < 0 || StageCreator._StageSide <= j) continue;
                    if(i == enemyTileIndex.I && j == enemyTileIndex.J) continue;
                    Vector3 candidateTargetPos3 = ConvertToRePos3FromTileIndex((i, j));
                    if(IsReachable(candidateTargetPos3, rePos3)) candidateTargetPos3List.Add(candidateTargetPos3);
                }
            if(candidateTargetPos3List.Count == 0) return rePos3;
            candidateTargetPos3List = candidateTargetPos3List.OrderBy(a => Guid.NewGuid()).ToList();
            return candidateTargetPos3List[0];
        }

        public static List<List<Vector3>> GetAllReachableRePos3WithoutJumping(Vector3 rePos3)
        {
            List<List<Vector3>> rePos3ListList = new() { new List<Vector3> { rePos3 } };
            (int i, int j) pivotTileIndex = ConvertToTileIndexFromRePos3(rePos3);
            bool[,] isVisited = InitializeVisitedMatrix();

            Queue<(int i, int j)> currentTileIndexQueue = new();
            currentTileIndexQueue.Enqueue(pivotTileIndex);
            isVisited[pivotTileIndex.i, pivotTileIndex.j] = true;

            while (currentTileIndexQueue.Count > 0)
            {
                List<Vector3> currentRePos3List = new();
                Queue<(int i, int j)> newTileIndexQueue = ProcessTileIndexQueue(currentTileIndexQueue, isVisited, currentRePos3List);
                rePos3ListList.Add(currentRePos3List);
                currentTileIndexQueue = newTileIndexQueue;
            }
            return rePos3ListList;
        }

        private static Queue<(int i, int j)> ProcessTileIndexQueue(Queue<(int i, int j)> currentTileIndexQueue, bool[,] isVisited, List<Vector3> currentRePos3List)
        {
            Queue<(int i, int j)> newTileIndexQueue = new();
            while (currentTileIndexQueue.Count > 0)
            {
                (int i, int j) currentTileIndex = currentTileIndexQueue.Dequeue();
                EnqueueAdjacentTiles(currentTileIndex, isVisited, newTileIndexQueue, currentRePos3List);
            }
            return newTileIndexQueue;
        }

        private static void EnqueueAdjacentTiles((int i, int j) currentTileIndex, bool[,] isVisited, Queue<(int i, int j)> newTileIndexQueue, List<Vector3> currentRePos3List)
        {
            for (int I = currentTileIndex.i - 1; I < currentTileIndex.i + 2; I++)
                for (int J = currentTileIndex.j - 1; J < currentTileIndex.j + 2; J++)
                {
                    if (!IsValidTile(I, J, currentTileIndex, isVisited)) continue;
                    isVisited[I, J] = true;
                    newTileIndexQueue.Enqueue((I, J));
                    currentRePos3List.Add(ConvertToRePos3FromTileIndex((I, J)));
                }
        }

        private static bool IsValidTile(int I, int J, (int i, int j) currentTileIndex, bool[,] isVisited)
        {
            return I >= 0 && I < StageCreator._StageSide && J >= 0 && J < StageCreator._StageSide
                && !isVisited[I, J] && StageCreator.TileImZs[I, J] == StageCreator.TileImZs[currentTileIndex.i, currentTileIndex.j];
        }

        private static bool[,] InitializeVisitedMatrix()
        {
            bool[,] isVisited = new bool[StageCreator._StageSide, StageCreator._StageSide];
            for(int i = 0; i < StageCreator._StageSide; i++)
                for(int j = 0; j < StageCreator._StageSide; j++)
                    isVisited[i, j] = false;
            return isVisited;
        }

        public static List<Vector3> GetAllCrossRePos3(Vector3 rePos3)
        {
            List<Vector3> rePos3List = new();
            (int i, int j) pivotTileIndex = ConvertToTileIndexFromRePos3(rePos3);
            for(int i = 0; i < StageCreator._StageSide; i++)
                rePos3List.Add(ConvertToRePos3FromTileIndex((i, pivotTileIndex.j)));
            for(int j = 0; j < StageCreator._StageSide; j++)
            {
                if(j == pivotTileIndex.j)
                    continue;
                rePos3List.Add(ConvertToRePos3FromTileIndex((pivotTileIndex.i, j)));
            }
            return rePos3List;
        }

        public static List<Vector3> GetAllHorizontalRePos3(Vector3 rePos3)
        {
            List<Vector3> rePos3List = new();
            (int i, int j) pivotTileIndex = ConvertToTileIndexFromRePos3(rePos3);
            for(int j = 0; j < StageCreator._StageSide; j++)
                rePos3List.Add(ConvertToRePos3FromTileIndex((pivotTileIndex.i, j)));
            return rePos3List;
        }

        public static List<Vector3> GetAllVerticalRePos3(Vector3 rePos3)
        {
            List<Vector3> rePos3List = new();
            (int i, int j) pivotTileIndex = ConvertToTileIndexFromRePos3(rePos3);
            for(int i = 0; i < StageCreator._StageSide; i++)
                rePos3List.Add(ConvertToRePos3FromTileIndex((i, pivotTileIndex.j)));
            return rePos3List;
        }

        public static bool IsHitWall(Vector3 imPos3)
        {
            if(imPos3.x < -StageCreator._StageSide / 2f || StageCreator._StageSide / 2f < imPos3.x
            || imPos3.y < -StageCreator._StageSide / 2f || StageCreator._StageSide / 2f < imPos3.y
            || imPos3.z < 0f || StageCreator._StageHeight < imPos3.z) return true;
            (int i, int j) tileNumber = ConvertToTileIndexFromImPos3(imPos3);
            return StageCreator.TileImZs[tileNumber.i, tileNumber.j] > imPos3.z - 1f;
        }

        public static bool IsHitStage(Vector3 imPos3)
        {
            return imPos3.x < -StageCreator._StageSide / 2f || StageCreator._StageSide / 2f < imPos3.x
            || imPos3.y < -StageCreator._StageSide / 2f || StageCreator._StageSide / 2f < imPos3.y
            || imPos3.z < 0f || StageCreator._StageHeight < imPos3.z;
        }

        public bool IsReachable(Vector3 targetRePos3, Vector3 objectRePos3)
        {
            return targetRePos3.z < objectRePos3.z + _objectParameter.JumpHeight;
        }
    
        public void HeadToMinusImX(bool isHeading)
        {
            isHeadingToMinusImX = isHeading;
        }    

        public void HeadToPlusImY(bool isHeading)
        {
            isHeadingToPlusImY = isHeading;
        }

        public void HeadToMinusImY(bool isHeading)
        {
            isHeadingToMinusImY = isHeading;
        }

        public void HeadToPlusImX(bool isHeading)
        {
            isHeadingToPlusImX = isHeading;
        }

        public (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingDirection()
        {
            return (isHeadingToPlusImX, isHeadingToMinusImX, isHeadingToPlusImY, isHeadingToMinusImY);
        }

        public static (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingAtPlayerDirection(Vector3 rePos3)
        {
            Vector3 moveDirectionIm3 = CalculateImDirection3BetWeenTwoRePos3(rePos3, ObjectStorage.GetPlayerRePos3());
            if(math.abs(moveDirectionIm3.x) > math.abs(moveDirectionIm3.y))
            {
                if(moveDirectionIm3.x > 0)
                    return (true, false, false, false);
                else
                    return (false, true, false, false);
            }
            else
            {
                if(moveDirectionIm3.y > 0)
                    return (false, false, true, false);
                else
                    return (false, false, false, true);
            }
        }

        public void TryToJump(bool isTrying)
        {
            isTryingToJump = isTrying;
        }

        public void Stop()
        {
            isHeadingToMinusImX = false;
            isHeadingToPlusImY = false;
            isHeadingToMinusImY = false;
            isHeadingToPlusImX = false;
            isTryingToJump = false;
        }

        public void Freeze()
        {
            Stop();
            isDead = true;
        }
        
        public bool IsDestinationTileZReachableWithJumping(Vector3 objectRePos3)
        {
            return destinationTileImZ < objectRePos3.z - 1f + _objectParameter.JumpHeight && objectRePos3.z - 1f < destinationTileImZ;
        }

        public static int CalculateSortingOrderFromRePos3(Vector3 rePos3)
        {
            (int i, int j) tileIndex = ConvertToTileIndexFromRePos3(rePos3);
            return tileIndex.i + tileIndex.j;
        }
    }   
}

