using Godot;
using System;

public partial class manutenção : Node2D
{
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
	}
	
	private void _on_voltar_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cenas/tela_dificuldade.tscn");
	}
}


