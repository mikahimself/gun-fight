using Godot;
using System;
using System.Linq;

public class TestCharacter : Sprite
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";
  public Vector2[] path;
  public float speed = 300;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    SetProcess(false);
  }

  public void setPath(Vector2[] value)
  {
    path = value;
    if (value.Length < 0)
    {
      return;
    }
    SetProcess(true);

  }
  //  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    var move_distance = speed * delta;
    move_along_path(move_distance);
  }

  public void move_along_path(float distance)
  {
    var lastPos = Position;
    foreach (Vector2 vec in path)
    {
      var distanceToNext = lastPos.DistanceTo(path[0]);
      if (distance <= distanceToNext && distance >= 0.0f)
      {
        Position = lastPos.LinearInterpolate(path[0], distance / distanceToNext);
        break;
      }
      else if (distance <= 0.0)
      {
        Position = path[0];
        SetProcess(false);
        break;
      }
      distance -= distanceToNext;
      lastPos = path[0];
      path = path.Skip(1).ToArray();

    }

  }
}
