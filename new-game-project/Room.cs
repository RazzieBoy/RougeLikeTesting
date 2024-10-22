using Godot;
using System;

public partial class Room : Node2D
{
	[Export] public bool isBossRoom = false;

	private TileMapLayer floorLayer;
	private TileMapLayer wallLayer;

	public override void _Ready()
{
	// Get the TileMapLayer nodes and verify they're found
	floorLayer = GetNode<TileMapLayer>("FloorLayer");
	wallLayer = GetNode<TileMapLayer>("WallLayer");

	if (floorLayer == null || wallLayer == null)
	{
		GD.Print("TileMapLayer nodes not found.");
	}
	else
	{
		GD.Print("TileMapLayer nodes found.");
		DrawRoomTiles();
	}
}


	private void DrawRoomTiles()
	{
		// Set up the floor tiles
		SetTiles(floorLayer, 0); // 0 is the tile ID for the floor
		// Set up the wall tiles
		SetWalls(wallLayer, 1); // 1 is the tile ID for the walls
	}

	private void SetTiles(TileMapLayer layer, int tileId)
{
	var tileSet = ResourceLoader.Load<TileSet>("floor.tres");
	if (tileSet == null)
	{
		GD.Print("Failed to load TileSet: floor.tres");
		return;
	}
	GD.Print("TileSet loaded: floor.tres");
	layer.TileSet = tileSet;

	for (int x = 0; x < 10; x++)
	{
		for (int y = 0; y < 10; y++)
		{
			GD.Print($"Setting tile at ({x}, {y}) with tile ID {tileId}");
			layer.SetCell(new Vector2I(x, y), tileId); // Set the floor tile at (x, y)
		}
	}
}


	private void SetWalls(TileMapLayer layer, int tileId)
	{
		// Load the correct TileSet resource
		var tileSet = ResourceLoader.Load<TileSet>("res://bricks.tres");
		layer.TileSet = tileSet;

		// Set wall tiles in the TileMapLayer directly
		for (int x = 0; x < 10; x++)
		{
			for (int y = 0; y < 10; y++)
			{
				if (x == 0 || x == 9 || y == 0 || y == 9) // Example for walls at the edges
				{
					layer.SetCell(new Vector2I(x, y), tileId); // Set the wall tile at (x, y) using the tile ID
				}
			}
		}
	}

	public void SetupRoom(bool isStart)
	{
		if (isStart)
		{
			GD.Print("This is the start room.");
			// Initialize player spawn point here
		}
		else if (isBossRoom)
		{
			GD.Print("This is the boss room.");
			// Initialize boss here
		}
		else
		{
			GD.Print("Generating regular room.");
			
			// Randomly spawn enemies, loot, or traps here
		}
	}
	
	public override void _Draw()
	{
	DrawRect(new Rect2(new Vector2(0, 0), new Vector2(100, 100)), Colors.Red);
	}
}
