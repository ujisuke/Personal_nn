using UnityEngine;
using System;
using Assets.Scripts.Stage;


namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;
        [SerializeField] private GameObject destination;
        private const int _tileSide = 8;
        private const float _tileHeight = 0.25f;
        private readonly int[,] _tileZs = StageCreator.TileZs;

        private readonly float _moveSpeed = 2f;
        private readonly float _jumpHeight = 2.5f;
        private readonly float _jumpTime = 0.6f;
        private bool isJumping = false;
        private float prevZ = 0;
        private SpriteRenderer playerSpriteRenderer;
        private SpriteRenderer shadowSpriteRenderer;

        private void Start()
        {
            prevZ = transform.position.z;
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            shadowSpriteRenderer = shadow.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            Vector3 playerImPos3 = ConvertToImPos3(transform.position);
            (int i, int j) playerTileNumber = ConvertToTileNumber(playerImPos3);
            
            Vector3 destinationRePos3 = GetDestinationRePos3();
            destination.transform.position = destinationRePos3;
            
            Vector3 destinationImPos3 = ConvertToImPos3(destinationRePos3);
            (int i, int j) destinationTileNumber = ConvertToTileNumber(destinationImPos3);

            int playerTileZ = _tileZs[playerTileNumber.i, playerTileNumber.j];
            int destinationJTileZ = _tileZs[playerTileNumber.i, destinationTileNumber.j];
            if(destinationJTileZ <= destinationImPos3.z - 1f)
            {
                playerImPos3.x = destinationImPos3.x;
                playerImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            else if(destinationJTileZ <= playerImPos3.z - 1f)
            {
                playerImPos3.x = destinationImPos3.x;
                playerImPos3.z = destinationJTileZ + 1f;
                isJumping = false;
            }
            else if(destinationImPos3.z - 1f < playerTileZ)
            {
                playerImPos3.z = playerTileZ + 1f;
                isJumping = false;
            }
            else
            {
                playerImPos3.z = destinationImPos3.z;
                isJumping = true;
            }

            playerTileNumber = ConvertToTileNumber(playerImPos3);
            playerTileZ = _tileZs[playerTileNumber.i, playerTileNumber.j];

            int destinationITileZ = _tileZs[destinationTileNumber.i, playerTileNumber.j];
            if(destinationITileZ <= destinationImPos3.z - 1f)
            {
                playerImPos3.y = destinationImPos3.y;
                playerImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            else if(destinationITileZ <= playerImPos3.z - 1f)
            {
                playerImPos3.y = destinationImPos3.y;
                playerImPos3.z = destinationITileZ + 1f;
                isJumping = false;
            }
            else if(destinationImPos3.z - 1f < playerTileZ)
            {
                playerImPos3.z = playerTileZ + 1f;
                isJumping = false;
            }
            else
            {
                playerImPos3.z = destinationImPos3.z;
                isJumping = true;
            }
            
            playerTileNumber = ConvertToTileNumber(playerImPos3);
            playerTileZ = _tileZs[playerTileNumber.i, playerTileNumber.j];

            transform.position = ConvertToRePos3(playerImPos3);
            playerSpriteRenderer.sortingOrder = playerTileNumber.i + playerTileNumber.j;
            Vector3 shadowImPos3 = new(playerImPos3.x, playerImPos3.y, playerTileZ + 1f);
            shadow.transform.position = ConvertToRePos3(shadowImPos3);
            shadowSpriteRenderer.sortingOrder = playerTileNumber.i + playerTileNumber.j;
        }

        private Vector3 GetDestinationRePos3()
        {
            Vector3 newPos3 = new(transform.position.x, transform.position.y, transform.position.z);
            float weight = _moveSpeed * Time.deltaTime;
            if(Input.GetKey(KeyCode.W)) newPos3.y += weight;
            if(Input.GetKey(KeyCode.S)) newPos3.y -= weight;
            if(Input.GetKey(KeyCode.A)) newPos3.x -= weight * 2;
            if(Input.GetKey(KeyCode.D)) newPos3.x += weight * 2;
            if(Input.GetKey(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                prevZ = newPos3.z;
                newPos3 += new Vector3(0, _tileHeight, 1f)
                * (4f * _jumpHeight / _jumpTime * Time.deltaTime - 4f * _jumpHeight / (_jumpTime * _jumpTime) * (Time.deltaTime * Time.deltaTime));
            }
            else
            {
                float tmpPrevZ = newPos3.z;
                newPos3 += new Vector3(0, _tileHeight, 1f)
                * (newPos3.z - prevZ - 8f * _jumpHeight / (_jumpTime * _jumpTime) * (Time.deltaTime * Time.deltaTime));
                prevZ = tmpPrevZ;
            }
            return newPos3;
        }

        private static Vector3 ConvertToImPos3(Vector3 rePos3)
        {
            Vector2 newRePos2 = new(rePos3.x, rePos3.y - (rePos3.z - 1) * _tileHeight);
            Vector3 newImPos3 = new(newRePos2.x - 2f * (newRePos2.y + _tileHeight), newRePos2.x + 2f * (newRePos2.y + _tileHeight), rePos3.z);
            newImPos3.x = Mathf.Clamp(newImPos3.x, -_tileSide / 2f + 0.01f, _tileSide / 2f - 0.01f);
            newImPos3.y = Mathf.Clamp(newImPos3.y, -_tileSide / 2f + 0.01f, _tileSide / 2f - 0.01f);
            return newImPos3;
        }

        static private (int i, int j) ConvertToTileNumber(Vector3 imPos3)
        {
            return (_tileSide / 2 - (int)Math.Floor(imPos3.y) - 1, _tileSide / 2 + (int)Math.Floor(imPos3.x));
        }

        private static Vector3 ConvertToRePos3(Vector3 imPos3)
        {
            Vector2 newRePos2 = new((imPos3.x + imPos3.y) * 0.5f, (imPos3.y - imPos3.x) * 0.25f - _tileHeight);
            return new Vector3(newRePos2.x, newRePos2.y + (imPos3.z - 1) * _tileHeight, imPos3.z);
        }
    }   
}

