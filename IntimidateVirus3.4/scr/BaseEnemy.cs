using Godot;
using System;

public partial class BaseEnemy : Resource 
{
	[Export]
	public static string name = "DENV";
	
	[Export]
	public Texture textute = null;
	
	[Export]
	public static int damage = 7;
	
	[Export]
	public static int health = 0;
	
	//public static AnimationPlayer Anim;
	
}
