using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private Vector3 nextTileSpawn;
    private Vector3 nextUnitSpawn;
    public GameObject player;
    public Transform Tile;
    public Transform RightTile;
    public Transform LeftTile;
    public Transform Obstacle;
    public Transform Crab;
    public Transform SlowDown;
    public Transform Fountain;
    public Transform Slide;
    public Transform Octopus;
    public Transform Fish;
    public Transform StaminaUp;
    private int randX;
    private int randGen;
    private int randi;
    private int decision;
    private Vector3 OctopusSpawn;

    void Start()
    {
        nextTileSpawn = new Vector3(0f, 0.0f, 21f);
        Instantiate(Tile, nextTileSpawn, Tile.rotation);
        nextTileSpawn.z += 7;
        Instantiate(Tile, nextTileSpawn, Tile.rotation);
        nextTileSpawn.z += 7;
    }

    void Update()
    {
        if (nextTileSpawn.z - player.transform.position.z < 80)
        {
            randX = Random.Range(-3, 3);
            nextUnitSpawn = nextTileSpawn;
            nextUnitSpawn.y = 3f;
            nextUnitSpawn.x = randX;

            System.Random rnd = new System.Random();
            decision = rnd.Next(1, 7);
            if (decision == 1)
            {
                Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
            }
            else if (decision == 2)
            {
                Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
            }
            else if (decision == 3)
            {
                Instantiate(Crab, nextUnitSpawn, Crab.rotation);
            }
            else if (decision == 4)
            {
                
                System.Random rndi = new System.Random();
                randi = rndi.Next(1, 4);
                OctopusSpawn = nextUnitSpawn;
                OctopusSpawn.y = 4.5f;
                if (randi == 2)
                {
                    OctopusSpawn.x = -4.7f;
                }
                else
                {
                    OctopusSpawn.x = 4.7f;
                }
                Instantiate(Octopus, OctopusSpawn, Octopus.rotation);
                //Instantiate(Slide, nextUnitSpawn, Slide.rotation);
            }
            else if (decision == 5)
            {
                Instantiate(Fish, nextUnitSpawn, Fish.rotation);
            }
            else if (decision == 6)
            {
                Instantiate(StaminaUp, nextUnitSpawn, StaminaUp.rotation);
            }

            System.Random rnd2 = new System.Random();
            randGen = rnd2.Next(1, 5);


            if (randGen == 1)
            {
                Instantiate(LeftTile, nextTileSpawn, LeftTile.rotation);
            }
            else if (randGen == 2)
            {
                Instantiate(RightTile, nextTileSpawn, RightTile.rotation);
            }
            else
            {
                Instantiate(Tile, nextTileSpawn, Tile.rotation);
            }
         nextTileSpawn.z += 7;
        }
    }
}