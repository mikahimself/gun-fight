using Godot;
using System;

public class Map : Node2D
{
    public OpenSimplexNoise noise;
    public Vector2 mapSize = new Vector2(32, 22);
    public float grassCap = 0.45f;
    public Vector2 roadCaps = new Vector2(0.275f, 0.075f);
    public Vector3 environmentCaps = new Vector3(0.4f, 0.3f, 0.04f);
    public RandomNumberGenerator rng;
    public TileMap sandMap;
    public TileMap roadMap;

    public Navigation2D nav2D;
    public Line2D line;
    TestCharacter dude;

  // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sandMap = (TileMap)GetNode("Navigation2D/TilemapSand");
        roadMap = (TileMap)GetNode("TilemapPaths");
        nav2D = (Navigation2D)GetNode("Navigation2D");
        line = (Line2D)GetNode("Line2D");
        dude = (TestCharacter)GetNode("Sprite");

        rng = new RandomNumberGenerator();
        rng.Randomize();
        noise = new OpenSimplexNoise();
        noise.Seed = (int)rng.Randi();
        noise.Octaves = 1;
        noise.Period = 13;

        CreateSandMap();
        CreateRoadMap();
    }

    public override void _Input(InputEvent e) {
        if (e.IsActionPressed("ui_accept")) {
            ClearMaps();
            DrawMaps();
        }

        if (e is InputEventMouseButton evmb) {
            var path = nav2D.GetSimplePath(dude.Position, evmb.Position);
            line.Points = path;
            dude.setPath(path);

        }

    }

    public void ClearMaps() {
        sandMap.Clear();
        roadMap.Clear();
    }

    public void DrawMaps() {
        noise.Seed = (int)rng.Randi();
        CreateSandMap();
        CreateRoadMap();
    }

    public void CreateSandMap()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var tile = noise.GetNoise2d(x, y);
                if (tile < grassCap)
                {
                    sandMap.SetCell(x, y, 0);
                }
            }
        }
        // Once the map has been created, update bitmask region to autotile map.
        sandMap.UpdateBitmaskRegion(new Vector2(0, 0), new Vector2(mapSize.x, mapSize.y));
    }

    public void CreateRoadMap()
    {
        for (int x = 0; x < mapSize.x; x++) 
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var tile = noise.GetNoise2d(x, y);
                
                //if (tile < roadCaps.x && tile > roadCaps.y)
                if (tile < roadCaps.x && tile > roadCaps.y)
                {
                    //GD.Print(tile);
                    roadMap.SetCell(x, y, 0);
                }
            }
        }
        roadMap.UpdateBitmaskRegion(new Vector2(0, 0), new Vector2(mapSize.x, mapSize.y));
    }



  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //  public override void _Process(float delta)
  //  {
  //      
  //  }


}
