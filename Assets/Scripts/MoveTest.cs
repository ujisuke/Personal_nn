using Mono.Cecil.Cil;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using Assets.Scripts.Stage;
using UnityEditor.SceneManagement;

public class MoveTest : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    [SerializeField] private GameObject destination;
    private const int _tileSide = 8;
    private const float _tileHeight = 0.25f;
    private readonly int[,] _tileZs = StageCreator._tileZs;

    private readonly float _moveSpeed = 2f;
    private readonly float _jumpHeight = 2.5f;
    private readonly float _jumpTime = 0.6f;
    private bool isJumping = false;
    private float z_Prev = 0;

    private void Start()
    {
        z_Prev = transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destinationPos_Re_3 = GetDestinationPos_Re_3();
        destination.transform.position = destinationPos_Re_3;

        Vector3 destinationPos_Im_3 = ConvertToPos_Im_3(destinationPos_Re_3);
        Vector3 playerPos_Im_3 = ConvertToPos_Im_3(transform.position);
        (int i, int j) destinationTileNumber = ConvertToTileNumber(destinationPos_Im_3);
        (int i, int j) playerTileNumber = ConvertToTileNumber(playerPos_Im_3);
        
        if(_tileZs[playerTileNumber.i, destinationTileNumber.j] <= destinationPos_Im_3.z - 1f)
        {
            playerPos_Im_3.x = destinationPos_Im_3.x;
            playerPos_Im_3.z = destinationPos_Im_3.z;
            playerTileNumber = ConvertToTileNumber(playerPos_Im_3);
            isJumping = true;
        }
        if(destinationPos_Im_3.z - 1f < _tileZs[playerTileNumber.i, destinationTileNumber.j])
        {
            if(_tileZs[playerTileNumber.i, destinationTileNumber.j] <= playerPos_Im_3.z - 1f)
            {
                playerPos_Im_3.x = destinationPos_Im_3.x;
                playerPos_Im_3.z = _tileZs[playerTileNumber.i, destinationTileNumber.j] + 1f;
                isJumping = false;
            }
            else
            {
                if(destinationPos_Im_3.z - 1f < _tileZs[playerTileNumber.i, playerTileNumber.j])
                {
                    playerPos_Im_3.z = _tileZs[playerTileNumber.i, playerTileNumber.j] + 1f;
                    isJumping = false;
                }
                else
                {
                    playerPos_Im_3.z = destinationPos_Im_3.z;
                    isJumping = true;
                }
            }
            playerTileNumber = ConvertToTileNumber(playerPos_Im_3);
        }

        if(_tileZs[destinationTileNumber.i, playerTileNumber.j] <= destinationPos_Im_3.z - 1f)
        {
            playerPos_Im_3.y = destinationPos_Im_3.y;
            playerPos_Im_3.z = destinationPos_Im_3.z;
            playerTileNumber = ConvertToTileNumber(playerPos_Im_3);
            isJumping = true;
        }
        if(destinationPos_Im_3.z - 1f < _tileZs[destinationTileNumber.i, playerTileNumber.j])
        {
            if(_tileZs[destinationTileNumber.i, playerTileNumber.j] <= playerPos_Im_3.z - 1f)
            {
                playerPos_Im_3.y = destinationPos_Im_3.y;
                playerPos_Im_3.z = _tileZs[destinationTileNumber.i, playerTileNumber.j] + 1f;
                isJumping = false;
            }
            else
            {
                if(destinationPos_Im_3.z - 1f < _tileZs[playerTileNumber.i, playerTileNumber.j])
                {
                    playerPos_Im_3.z = _tileZs[playerTileNumber.i, playerTileNumber.j] + 1f;
                    isJumping = false;
                }
                else
                {
                    playerPos_Im_3.z = destinationPos_Im_3.z;
                    isJumping = true;
                }
            }
            playerTileNumber = ConvertToTileNumber(playerPos_Im_3);
        }

        transform.position = ConvertToPos_Re_3(playerPos_Im_3);

        GetComponent<SpriteRenderer>().sortingOrder = playerTileNumber.i + playerTileNumber.j;
        

        Vector3 shadowPos_Im_3 = new Vector3(playerPos_Im_3.x, playerPos_Im_3.y, _tileZs[playerTileNumber.i, playerTileNumber.j] + 1f);
        shadow.transform.position = ConvertToPos_Re_3(shadowPos_Im_3);
        shadow.GetComponent<SpriteRenderer>().sortingOrder = playerTileNumber.i + playerTileNumber.j;
    }

    private Vector3 GetDestinationPos_Re_3()
    {
        Vector3 newPos_3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float weight = _moveSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.W))
        {
            newPos_3 += new Vector3(0, 1f, 0) * weight;
        }
        if(Input.GetKey(KeyCode.S))
        {
            newPos_3 += new Vector3(0, -1f, 0) * weight;
        }
        if(Input.GetKey(KeyCode.A))
        {
            newPos_3 += new Vector3(-1f, 0, 0) * weight * 2;
        }
        if(Input.GetKey(KeyCode.D))
        {
            newPos_3 += new Vector3(1f, 0, 0) * weight * 2;
        }
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            z_Prev = newPos_3.z;
            newPos_3 += new Vector3(0, _tileHeight, 1f)
            * (4f * _jumpHeight / _jumpTime * Time.deltaTime - 4f * _jumpHeight / (_jumpTime * _jumpTime) * (Time.deltaTime * Time.deltaTime));
        }
        else{
            float z_Prev_Tmp = newPos_3.z;
            newPos_3 += new Vector3(0, _tileHeight, 1f)
            * (newPos_3.z - z_Prev - 8f * _jumpHeight / (_jumpTime * _jumpTime) * (Time.deltaTime * Time.deltaTime));
            z_Prev = z_Prev_Tmp;
        }

        return newPos_3;
    }

    static private (int i, int j) ConvertToTileNumber(Vector3 pos_Im_3)
    {
        return (_tileSide / 2 - (int)Math.Floor(pos_Im_3.y) - 1, _tileSide / 2 + (int)Math.Floor(pos_Im_3.x));
    }

    private Vector3 ConvertToPos_Im_3(Vector3 pos_Re_3)
    {
        Vector2 newPos_Re_2 = new Vector2(pos_Re_3.x, pos_Re_3.y - (pos_Re_3.z - 1) * _tileHeight);
        Vector3 newPos_Im_3 = new Vector3(newPos_Re_2.x - 2f * (newPos_Re_2.y + _tileHeight), newPos_Re_2.x + 2f * (newPos_Re_2.y + _tileHeight), pos_Re_3.z);
        newPos_Im_3.x = Mathf.Clamp(newPos_Im_3.x, -_tileSide / 2f + 0.01f, _tileSide / 2f - 0.01f);
        newPos_Im_3.y = Mathf.Clamp(newPos_Im_3.y, -_tileSide / 2f + 0.01f, _tileSide / 2f - 0.01f);
        return newPos_Im_3;
    }

    private Vector3 ConvertToPos_Re_3(Vector3 pos_Im_3)
    {
        Vector2 newPos_Re_2 = new Vector2((pos_Im_3.x + pos_Im_3.y) * 0.5f, (pos_Im_3.y - pos_Im_3.x) * 0.25f - _tileHeight);
        return new Vector3(newPos_Re_2.x, newPos_Re_2.y + (pos_Im_3.z - 1) * _tileHeight, pos_Im_3.z);
    }
}
