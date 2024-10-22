using Godot;
using System;
using System.Collections.Generic;

public partial class DungeonGen : Node2D
{
	[Export] public int gridWidth = 10;
	[Export] public int gridHeight = 10;
	[Export] public int maxRooms = 8;
	[Export] public PackedScene roomScene;

	private Random random = new Random();
	private Dictionary<Vector2, Room> dungeonRooms = new Dictionary<Vector2, Room>();
	private Vector2 startRoomPosition;
	private Camera2D camera;

	public override void _Ready()
	{
		GenerateDungeon();

		// Create and configure the Camera2D
		camera = new Camera2D();
		AddChild(camera);
		camera.Position = new Vector2(gridWidth / 2 * 200, gridHeight / 2 * 200);

		// Set the camera as the active one
		camera.MakeCurrent();
	}

	private void GenerateDungeon()
	{
		Vector2 currentPos = new Vector2(gridWidth / 2, gridHeight / 2); // Start in the center
		dungeonRooms[currentPos] = CreateRoom(currentPos, true); // Start room
		startRoomPosition = currentPos * 200; // Store the start room's position for the camera

		for (int i = 0; i < maxRooms - 1; i++)
		{
			Vector2 nextPos = GetRandomAdjacentPosition(currentPos);
			if (!dungeonRooms.ContainsKey(nextPos))
			{
				dungeonRooms[nextPos] = CreateRoom(nextPos);
				currentPos = nextPos; // Move to the next room
			}
		}
	}

	private Vector2 GetRandomAdjacentPosition(Vector2 position)
	{
		List<Vector2> directions = new List<Vector2>()
		{
			new Vector2(1, 0),  // Right
			new Vector2(-1, 0), // Left
			new Vector2(0, 1),  // Down
			new Vector2(0, -1)  // Up
		};

		return position + directions[random.Next(directions.Count)];
	}

	private Room CreateRoom(Vector2 gridPos, bool isStart = false)
	{
		Room newRoom = roomScene.Instantiate<Room>();
		AddChild(newRoom);
		newRoom.Position = gridPos * 200; // Adjusted for spacing between rooms
		GD.Print($"Room created at position: {newRoom.Position}");
		newRoom.SetupRoom(isStart);
		return newRoom;
	}

}
