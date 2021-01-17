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
    public Transform Bread;
    private float randX;
    private int tileOdds;
    private Vector3 OctopusSpawn;

    void Start()
    {
        nextTileSpawn = new Vector3(0f, 0.0f, 21f);
        Instantiate(Tile, nextTileSpawn, Tile.rotation);
        nextTileSpawn.z += 7;
        Instantiate(Tile, nextTileSpawn, Tile.rotation);
        nextTileSpawn.z += 7;
        Instantiate(Tile, nextTileSpawn, Tile.rotation);
        nextTileSpawn.z += 7;
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

            System.Random r = new System.Random();
            int randChoice = r.Next(0, 100); //excludes upper bound

            // Meters 0 - 100
            if (GlobalScript.Distance + player.transform.position.z + 80 < 100)
            {
                tileOdds = 10; // tile with hole with spawn 1 out of 'tileOdds' times
                if (randChoice < 35)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 52)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
            }

            // Meters 100 - 200
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 200)
            {
                tileOdds = 8;
                if (randChoice < 20)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 40)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 60)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
            }

            // Meters 200 - 300
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 300)
            {
                tileOdds = 7;
                if (randChoice < 16)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 32)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 48)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
                else if (randChoice < 64)
                {
                    Instantiate(Bread, nextUnitSpawn, Bread.rotation);
                }
            }

            // Meters 300 - 400
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 400)
            {
                tileOdds = 6;
                if (randChoice < 16)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 32)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 48)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
                else if (randChoice < 64)
                {
                    Instantiate(Bread, nextUnitSpawn, Bread.rotation);
                }
                else if (randChoice < 80)
                {
                    OctopusSpawn = nextUnitSpawn;
                    OctopusSpawn.y = 4.5f;
                    if (nextUnitSpawn.x < 0)
                    {
                        OctopusSpawn.x = -4.7f;
                    }
                    else
                    {
                        OctopusSpawn.x = 4.7f;
                    }
                    Instantiate(Octopus, OctopusSpawn, Octopus.rotation);
                }
            }

            // Meters 400 - 500
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 500)
            {
                tileOdds = 5;
                if (randChoice < 13)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 26)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 39)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
                else if (randChoice < 52)
                {
                    Instantiate(Bread, nextUnitSpawn, Bread.rotation);
                }
                else if (randChoice < 65)
                {
                    OctopusSpawn = nextUnitSpawn;
                    OctopusSpawn.y = 4.5f;
                    if (nextUnitSpawn.x < 0)
                    {
                        OctopusSpawn.x = -4.7f;
                    }
                    else
                    {
                        OctopusSpawn.x = 4.7f;
                    }
                    Instantiate(Octopus, OctopusSpawn, Octopus.rotation);
                }
                else if (randChoice < 78)
                {
                    Instantiate(Fish, nextUnitSpawn, Fish.rotation);
                }
            }

            // Meters 500 - 600
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 600)
            {
                tileOdds = 4;
                if (randChoice < 70)
                {
                    Instantiate(Fish, nextUnitSpawn, Fish.rotation);
                }
            }

            // Meters 600 - 700
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 700)
            {
                tileOdds = 6;
                if (randChoice < 80)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
            }

            // Meters 700 - 800
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 800)
            {
                tileOdds = 6;
                if (randChoice < 13)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 26)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 39)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
                else if (randChoice < 52)
                {
                    Instantiate(Bread, nextUnitSpawn, Bread.rotation);
                }
                else if (randChoice < 65)
                {
                    OctopusSpawn = nextUnitSpawn;
                    OctopusSpawn.y = 4.5f;
                    if (nextUnitSpawn.x < 0)
                    {
                        OctopusSpawn.x = -4.7f;
                    }
                    else
                    {
                        OctopusSpawn.x = 4.7f;
                    }
                    Instantiate(Octopus, OctopusSpawn, Octopus.rotation);
                }
                else if (randChoice < 78)
                {
                    Instantiate(Fish, nextUnitSpawn, Fish.rotation);
                }
            }

            // Meters 800 - 900
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 900)
            {
                tileOdds = 6;
                if (randChoice < 13)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 26)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 39)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
                else if (randChoice < 52)
                {
                    Instantiate(Bread, nextUnitSpawn, Bread.rotation);
                }
                else if (randChoice < 65)
                {
                    OctopusSpawn = nextUnitSpawn;
                    OctopusSpawn.y = 4.5f;
                    if (nextUnitSpawn.x < 0)
                    {
                        OctopusSpawn.x = -4.7f;
                    }
                    else
                    {
                        OctopusSpawn.x = 4.7f;
                    }
                    Instantiate(Octopus, OctopusSpawn, Octopus.rotation);
                }
                else if (randChoice < 78)
                {
                    Instantiate(Fish, nextUnitSpawn, Fish.rotation);
                }
            }

            // Meters 900 - 1000
            else if (GlobalScript.Distance + player.transform.position.z + 80 < 1000)
            {
                tileOdds = 6;
                if (randChoice < 13)
                {
                    Instantiate(Obstacle, nextUnitSpawn, Obstacle.rotation);
                }
                else if (randChoice < 26)
                {
                    Instantiate(SlowDown, nextUnitSpawn, SlowDown.rotation);
                }
                else if (randChoice < 39)
                {
                    Instantiate(Crab, nextUnitSpawn, Crab.rotation);
                }
                else if (randChoice < 52)
                {
                    Instantiate(Bread, nextUnitSpawn, Bread.rotation);
                }
                else if (randChoice < 65)
                {
                    OctopusSpawn = nextUnitSpawn;
                    OctopusSpawn.y = 4.5f;
                    if (nextUnitSpawn.x < 0)
                    {
                        OctopusSpawn.x = -4.7f;
                    }
                    else
                    {
                        OctopusSpawn.x = 4.7f;
                    }
                    Instantiate(Octopus, OctopusSpawn, Octopus.rotation);
                }
                else if (randChoice < 78)
                {
                    Instantiate(Fish, nextUnitSpawn, Fish.rotation);
                }
            }

            System.Random rndTile = new System.Random();
            int odds = 2*tileOdds + 1;
            int randGen = rndTile.Next(1, odds); //excludes upper bound

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