using Godot;
using System;

public partial class Node2D : Godot.Node2D
{
	PackedScene PackedSceneEnemy = ResourceLoader.Load("res://Cenas/viros_1.tscn") as PackedScene;
	public override void _Ready()
	{
		var CustomSprite = PackedSceneEnemy.Instantiate();
		AddChild(CustomSprite);
	}
	public override void _Process(double delta)
	{
		
	}
}
