using Godot;
using System;

public partial class Boss : Sprite2D
{
	
	public override void _Ready()
	{
				Global.name = "AIDS";
				Global.EnemyMax_health = 200 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 50 * Global.enemy_level;
				Global.EnemySpeed = 2;
				this.Scale =  new Vector2 ( 3f, 3f);
				this.Position = new Vector2(1000, 900);
				GetNode<AnimationPlayer>("AnimationBoss").Play("Virus_AIDS");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_area_2d_body_entered(CharacterBody2D PlayerBody)
	{
		Global.Boss = true;
		GetTree().ChangeSceneToFile("res://scr/battle.tscn");
		Global.Tocou = true;
		Global.Player = PlayerBody;
	}
}



