using Godot;
using System;

public partial class tela_dificuldade : Node2D
{
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
	private void _on_americas_medio_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cenas/tela_fase_1.tscn");
	}
	private void _on_europa_facil_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cenas/manutenção.tscn");
	}
	
}





