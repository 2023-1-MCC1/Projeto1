using Godot;
using System;

public partial class Enemy : TextureRect
{
	Texture2D teobando =  ResourceLoader.Load("res://Inimigos/Fantasy Battlers - Free/x2 size/10b.png") as Texture2D;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//GetNode<TextureRect>("EnemyPanel/EnemyContainer/Enemy").Texture = teobando;
		this.Texture = teobando;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
