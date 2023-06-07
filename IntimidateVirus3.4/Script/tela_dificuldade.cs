using Godot;
using System;

public partial class tela_dificuldade : Node2D
{
	
	public override void _Ready()
	{
		Global.inicio = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
	private void _on_americas_medio_pressed()
	{
		
		Global.DificuldadeEscolhida = 2;
		GetTree().ChangeSceneToFile("res://Cenas/tela_fase_1.tscn");
		
	}
	private void _on_europa_facil_pressed()
	{
		
		Global.DificuldadeEscolhida = 1;
		GetTree().ChangeSceneToFile("res://Cenas/tela_fase_2.tscn");
		
	}
	private void _on_button_pressed()
	{
		
		Global.DificuldadeEscolhida = 3;
		GetTree().ChangeSceneToFile("res://Cenas/tela_fase_3.tscn");
		
	}
	
}








