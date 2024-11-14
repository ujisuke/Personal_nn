using UnityEngine;
using System;
using Assets.Scripts.Stage;


namespace Assets.Scripts.Objects
{
    public class ObjectMove : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;
        private readonly int[,] _tileZs = StageCreator.TileZs;

        private readonly float _moveSpeed = 2f;
        private readonly float _jumpHeight = 2.5f;
        private readonly float _jumpTime = 0.6f;
        private bool isJumping = false;
        private float prevZ = 0;
        private SpriteRenderer objectSpriteRenderer;
        private SpriteRenderer shadowSpriteRenderer;
        private bool isHeadingToA = false;
        private bool isHeadingToW = false;
        private bool isHeadingToS = false;
        private bool isHeadingToD = false;
        private bool isTryingToJump = false;
        private int destinationTileZ = 0; 

        private void Start()
        {
            prevZ = transform.position.z;
            objectSpriteRenderer = GetComponent<SpriteRenderer>();
            shadowSpriteRenderer = shadow.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            Vector3 objectImPos3 = ConvertToImPos3(transform.position);
            (int i, int j) objectTileNumber = ConvertToTileNumber(objectImPos3);
            
            Vector3 destinationRePos3 = GetDestinationRePos3();
            
            Vector3 destinationImPos3 = ConvertToImPos3(destinationRePos3);
            (int i, int j) destinationTileNumber = ConvertToTileNumber(destinationImPos3);

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

            objectTileNumber = ConvertToTileNumber(objectImPos3);
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
            }
            else
            {
                objectImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            
            objectTileNumber = ConvertToTileNumber(objectImPos3);
            objectTileZ = _tileZs[objectTileNumber.i, objectTileNumber.j];
            destinationTileZ = _tileZs[destinationTileNumber.i, destinationTileNumber.j];

            transform.position = ConvertToRePos3(objectImPos3);
            objectSpriteRenderer.sortingOrder = objectTileNumber.i + objectTileNumber.j;
            Vector3 shadowImPos3 = new(objectImPos3.x, objectImPos3.y, objectTileZ + 1f);
            shadow.transform.position = ConvertToRePos3(shadowImPos3);
            shadowSpriteRenderer.sortingOrder = objectTileNumber.i + objectTileNumber.j;
        }

        private Vector3 GetDestinationRePos3()
        {
            Vector3 newPos3 = new(transform.position.x, transform.position.y, transform.position.z);
            float weight = _moveSpeed * Time.deltaTime;
            if(isHeadingToW) newPos3.y += weight;
            if(isHeadingToS) newPos3.y -= weight;
            if(isHeadingToA) newPos3.x -= weight * 2;
            if(isHeadingToD) newPos3.x += weight * 2;
            if(isTryingToJump && !isJumping)
            {
                isJumping = true;
                prevZ = newPos3.z;
                newPos3 += new Vector3(0, StageCreator._tileHeight, 1f)
                * (4f * _jumpHeight / _jumpTime * Time.deltaTime - 4f * _jumpHeight / (_jumpTime * _jumpTime) * (Time.deltaTime * Time.deltaTime));
            }
            else
            {
                float tmpPrevZ = newPos3.z;
                newPos3 += new Vector3(0, StageCreator._tileHeight, 1f)
                * (newPos3.z - prevZ - 8f * _jumpHeight / (_jumpTime * _jumpTime) * (Time.deltaTime * Time.deltaTime));
                prevZ = tmpPrevZ;
            }
            return newPos3;
        }

        private static Vector3 ConvertToImPos3(Vector3 rePos3)
        {
            Vector2 newRePos2 = new(rePos3.x, rePos3.y - (rePos3.z - 1) * StageCreator._tileHeight);
            Vector3 newImPos3 = new(newRePos2.x - 2f * (newRePos2.y + StageCreator._tileHeight), newRePos2.x + 2f * (newRePos2.y + StageCreator._tileHeight), rePos3.z);
            newImPos3.x = Mathf.Clamp(newImPos3.x, -StageCreator._stageSide / 2f + 0.01f, StageCreator._stageSide / 2f - 0.01f);
            newImPos3.y = Mathf.Clamp(newImPos3.y, -StageCreator._stageSide / 2f + 0.01f, StageCreator._stageSide / 2f - 0.01f);
            return newImPos3;
        }

        static private (int i, int j) ConvertToTileNumber(Vector3 imPos3)
        {
            return (StageCreator._stageSide / 2 - (int)Math.Floor(imPos3.y) - 1, StageCreator._stageSide / 2 + (int)Math.Floor(imPos3.x));
        }

        private static Vector3 ConvertToRePos3(Vector3 imPos3)
        {
            Vector2 newRePos2 = new((imPos3.x + imPos3.y) * 0.5f, (imPos3.y - imPos3.x) * 0.25f - StageCreator._tileHeight);
            return new Vector3(newRePos2.x, newRePos2.y + (imPos3.z - 1) * StageCreator._tileHeight, imPos3.z);
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

        public bool IsDestinationTileZMoreThanOrEqual(float z)
        {
            return destinationTileZ >= z;
        }
    }   
}

