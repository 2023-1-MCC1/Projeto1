using Godot;
using System;

public partial class tela_incial_intimidate_virus : Node2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
		}
		private void _on_star_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cenas/tela_dificuldade.tscn");

	}
}






