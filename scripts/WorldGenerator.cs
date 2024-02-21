using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class WorldGenerator : Node2D
{
	[Export] private TileMap _map = null;
	[Export] public double _landCap = 0.1;
	[Export] private CharacterBody2D player = null;
	
	private FastNoiseLite noise = new();

	private RandomNumberGenerator _rng = new();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rng.Randomize();
		noise.Seed = _rng.RandiRange(0, 2000);
		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Cellular;
		GenerateWorld();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void GenerateWorld()
	{
		var cells = new Godot.Collections.Array<Vector2I>();
		var secondCells = new Godot.Collections.Array<Vector2I>();
		for (int x = 0; x < 150; x++)
		{
			for (int y = 0; y < 150; y++)
			{
				float absNoise = Math.Abs(noise.GetNoise2D(x, y));
				if (absNoise < _landCap)
				{
					cells.Add(new Vector2I(x, y));
				}
				else                                  
				{
					secondCells.Add(new Vector2I(x, y));
				}
				//GD.Print(absNoise);
			}
		}
		_map.SetCellsTerrainConnect(0, cells, 0, 0);
		_map.SetCellsTerrainConnect(0, secondCells,1, 0);
	}
}
