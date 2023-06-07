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
	
private void _on_back_button_pressed()
{
	Console.WriteLine("Teste");
	Global.GameOver = false;
	Global.MaxVidaCorpo = 100;
	Global.VidaCorpo = Global.MaxVidaCorpo;
	GetTree().ChangeSceneToFile("res://Cenas/tela_dificuldade.tscn");
}
	private void _on_back_bnt_pressed()
{
	
}
	
}













