using UnityEngine;
using System.Collections.Generic;
public static class EnumUtilities
{
  public static readonly List<Direction> AllDirections = new List<Direction>
  {
    Direction.Right, Direction.Top, Direction.Left, Direction.Bottom
  };
  public static Direction GetOppositeDirection(Direction direction)
  {
    switch (direction)
    {
      case Direction.Right:
        return Direction.Left;
      case Direction.Top:
        return Direction.Bottom;
      case Direction.Left:
        return Direction.Right;
      case Direction.Bottom:
        return Direction.Top;
      default:
        return direction; // Fallback, should not happen
    }
  }
}

public enum EnemySpawningPattern
{
  Random = 0,
  All = 1
}
public enum EnemySpawnType
{
  SpawnOnTrigger = 0,
  SpawnOnTimer = 1,
  Persistent = 2,
}
public enum EnemyRoomSizeScaling
{
  None = 0,
  Horizontal = 1,
  Vertical = 2,
  Magnitude = 3,
  FullPerimeter = 4
}
public enum Direction
{
  Right = 0,
  Top = 1,
  Left = 2,
  Bottom = 3
}

public enum GameState
{
    GameplayLobby = 0,
    GameSetup = 1,
    Active = 2,
    Transition = 3,
    Paused = 4,
    GameOver = 5,
    GameWon = 6,
    Options = 7
}
public enum Song
{
  None = 0,
  MainMenu = 1,
  Escape = 2,
}
enum MovementInputType
{
  Look = 0,
  Move = 1
}
public enum DoorAvailability
{
  None = 0,
  Left_or_Bottom = 1,
  Right_or_Top = 2,
  Both = 3
}
public enum AimType
{
    Orthogonal = 0,
    Random = 1,
    Aimed = 2
}
public enum EnemyType
{
  Straight = 0,
  Curved = 1,
  Homing = 2
}
[System.Serializable]
public struct DoorInfo
{
  public Vector2Int GridLocation;
  public Direction Orientation;
  public DoorInfo(Vector2Int gridLocation, Direction orientation)
  {
    GridLocation = gridLocation;
    Orientation = orientation;
  }
  public Vector2Int GetPointedPosition()
  {
    switch (Orientation)
    {
      case Direction.Right:
        return GridLocation + Vector2Int.right;
      case Direction.Top:
        return GridLocation + Vector2Int.up;
      case Direction.Left:
        return GridLocation + Vector2Int.left;
      case Direction.Bottom:
        return GridLocation + Vector2Int.down;
      default:
        return GridLocation; // Fallback, should not happen
    }
  }
  public DoorInfo GetMatchingDoor()
  {
    switch (Orientation)
    {
      case Direction.Right:
        return new DoorInfo(GridLocation + Vector2Int.right, Direction.Left);
      case Direction.Top:
        return new DoorInfo(GridLocation + Vector2Int.up, Direction.Bottom);
      case Direction.Left:
        return new DoorInfo(GridLocation + Vector2Int.left, Direction.Right);
      case Direction.Bottom:
        return new DoorInfo(GridLocation + Vector2Int.down, Direction.Top);
      default:
        return this; // Fallback, should not happen
    }
  }
}