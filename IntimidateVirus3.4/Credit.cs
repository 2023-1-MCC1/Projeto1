using Godot;
using System;

public partial class Credit : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_back_button_pressed()
{
	GetTree().ChangeSceneToFile("res://Cenas/tela_incial_intimidate_virus.tscn");
}

}


