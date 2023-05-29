using Godot;
using System;

public partial class game_over : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	
	public void _on_back_bnt_pressed()
	{
			Console.WriteLine("Teste");
			Global.GameOver = false;
			Global.VidaCorpo = Global.MaxVidaCorpo;
			GetTree().ChangeSceneToFile("res://Cenas/tela_dificuldade.tscn");
	}
	
}







