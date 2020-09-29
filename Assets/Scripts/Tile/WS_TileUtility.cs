using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WS_TileUtility 
{
    private WS_World world              = null;             // pointer to world
    private Vector2Int position         = Vector2Int.zero;  // positon in the tile array, not real transform position
    public List<WS_Tile> neighbors_1    = new List<WS_Tile>();             // neighboring tiles in radius 1
    public List<WS_Tile> neighbors_2    = new List<WS_Tile>();             // neighboring tiles in radius 2
    public List<WS_Tile> neighbors_3    = new List<WS_Tile>();             // neighboring tiles in radius 3



    public void setWorld(WS_World _world)      // set the world pointer
    {
        world = _world;
    }

    public Vector2Int getPosition()     // return the array position of this tile
    {
        return position;
    }

    public void setPosition(Vector2Int _position)     // set the array position of this tile
    {
        position = _position;
    }

    public Vector2 LongitudeLatitude()   // returns longitude/latitude of the tile in range -180.0/180.0
    {
        return new Vector2(((position.x - (WS_World.sizeX / 2)) / WS_World.sizeX) * 180.0f, ((position.x - (WS_World.sizeY / 2)) / WS_World.sizeY) * 180.0f);
    }
 

    public void LoadNeighbors1()     // load neighbors in radius 1
    {
        bool offsetX = (position.y % 2) == 1;       // to proper fit in hexagonal shape, odd rows have an offset in X

        Vector2Int neighbor_pos = position;
        neighbor_pos.x = position.x + 1 > WS_World.sizeX - 1 ? 0 : position.x + 1;
        neighbors_1.Add(world.GetTile(neighbor_pos));                                   // add right neighbor

        neighbor_pos.x = position.x - 1 < 0 ? (WS_World.sizeX - 1) : position.x - 1;       // add left neighbor
        neighbors_1.Add(world.GetTile(neighbor_pos));

        if (offsetX)                                // apply the offset if required
            neighbor_pos.x = position.x;

        neighbor_pos.y = position.y + 1 > WS_World.sizeY - 1 ? 0 : position.y + 1;         // add upper left neihgbor           
        neighbors_1.Add(world.GetTile(neighbor_pos));

        neighbor_pos.y = position.y - 1 < 0 ? WS_World.sizeY - 1 : position.y - 1;         // add lower left neihgbor 
        neighbors_1.Add(world.GetTile(neighbor_pos));

        neighbor_pos.x = neighbor_pos.x + 1 > WS_World.sizeX - 1 ? 0 : neighbor_pos.x + 1;

        neighbor_pos.y = position.y + 1 > WS_World.sizeY - 1 ? 0 : position.y + 1;         // add upper right neihgbor 
        neighbors_1.Add(world.GetTile(neighbor_pos));

        neighbor_pos.y = position.y - 1 < 0 ? WS_World.sizeY - 1 : position.y - 1;         // add lower left neihgbor 
        neighbors_1.Add(world.GetTile(neighbor_pos));
    }

    public void LoadNeighbors2()        // load neighbors in radius 2
    {
        foreach (WS_Tile neighbor in neighbors_1)
            foreach (WS_Tile neighbor2 in neighbor.utility.neighbors_1)
                neighbors_2.Add(neighbor2);                                 // add all adjacent tiles of radius 1 neighbors

        neighbors_2 = neighbors_2.Distinct().ToList();                      // remove duplicates
        neighbors_2 = neighbors_2.Except(neighbors_1).ToList();             // remove neighbors in radius 1, already stored
        neighbors_2.Remove(world.GetTile(position));                        // remove the tile itself
    }

    public void LoadNeighbors3()
    {
        foreach (WS_Tile neighbor in neighbors_2)
            foreach (WS_Tile neighbor2 in neighbor.utility.neighbors_1)        
                neighbors_3.Add(neighbor2);                                 // add all adjacent tiles of radius 2 neighbors

        neighbors_3 = neighbors_3.Distinct().ToList();                      // remove duplicates
        neighbors_3 = neighbors_3.Except(neighbors_2).ToList();             // remove neighbors in radius 2, already stored
        neighbors_3 = neighbors_3.Except(neighbors_1).ToList();             // remove neighbors in radius 1, already stored
    }
}
