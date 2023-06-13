using Godot;
using System;
using System.Threading.Tasks;

public partial class Viros_1 : Sprite2D
{
	
	public Random rnd = new Random();
	public static AnimationPlayer AnimationViros01;
	public static ProgressBar progressEnemy;
	bool Gerado, morto;
	public static int tipoinimigo;
	public static Vector2 ultimaposicao;

	
	
		
	
	
	public override void _Ready()
	{	
		
		this.Position = Global.PosEnemy;
		morto = false;
		Gerado = true;
		verificaAnima();
		
		if(Global.Tocou == true)
		{Gerado = false;}
		else if(Global.Tocou == false && Gerado == true){
			
			//Global.TipoEnemy = Global.rnd.Next(0,6);
			tipoinimigo = Global.TipoEnemy ;
			Gerado = false;
		}
		tipoinimigo = tipoinimigo;

		GetNode<Panel>("PanelEnemy").Hide();
		AnimationViros01 = GetNode<AnimationPlayer>("AnimationViros01");
		progressEnemy = GetNode<ProgressBar>("PanelEnemy/BarraVidaEnemy");
		Global.set_health(progressEnemy,  Global.EnemyHealth,  Global.EnemyMax_health);	
		
		Virus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if(Global.NewBattle == false)
		{
			GetNode<Panel>("PanelEnemy").Hide();
			if(Global.EnemyHealth <= 0 )
				{
				AnimationViros01.Play("Virus_Death");
				
				Global.Tocou = false;
				morto = true;
				}
		}
		
		if(Global.NewBattle == true && morto == false)
		{
			verificaAnima();
			Global.set_health(progressEnemy,  Global.EnemyHealth,  Global.EnemyMax_health);
			GetNode<Panel>("PanelEnemy").Show();
			this.Position = new Vector2( 600, 400);
			this.Scale = new Vector2( 1.2f, 1.2f);
			GetNode<Panel>("PanelEnemy").Show();
			GetNode<Label>("PanelEnemy/BarraVidaEnemy/BarraVidaLabel").Text = string.Format("HP: {0}/{1}", Global.EnemyHealth, Global.EnemyMax_health);
			GetNode<Label>("PanelEnemy/LvlEnemy").Text = string.Format("LVL: {0}", Global.enemy_level);
			GetNode<Label>("PanelEnemy/NameEnemy").Text = string.Format("{0}", Global.name);
			
		
		}
		
	}
	
	private void _on_area_2d_viros_01_body_entered(CharacterBody2D PlayerBody)
	{
		GetTree().ChangeSceneToFile("res://scr/battle.tscn");
		Global.Tocou = true;
		Global.Player = PlayerBody;
		//Global.Player.Position() = ultimaposicao;
		
	}
	
	public void Virus()
	{
		Global.EnemyHealth = Global.EnemyMax_health;
		Global.enemy_level = Global.rnd.Next(Global.level - 1 ,Global.level+2);
		if(Global.enemy_level <= 0)
		{
			Global.enemy_level =1;
		}
		switch(tipoinimigo)
		{
			case 1:
				AnimationViros01.Play("Viros01_Idel");
				Global.name = "Influenza-A";
				Global.EnemyMax_health = 15 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 7 * Global.enemy_level;
				Global.EnemySpeed = 1;
				
				
				break;
				
				case 2:
				AnimationViros01.Play("Viros02_Idel");
				Global.name = "Influenza-B";
				Global.EnemyMax_health = 18 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 5 * Global.enemy_level;
				Global.EnemySpeed = 1;
				
				
				break;
				
				case 3:
				Global.name = "Influenza-C";
				Global.EnemyMax_health = 12 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 7 * Global.enemy_level;
				Global.EnemySpeed = 1;
				AnimationViros01.Play("Viros03_Idel");
				
				break;
				
			case 4:
				Global.name = "V-Zoster";
				Global.EnemyMax_health = 12 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 4 * Global.enemy_level;
				Global.EnemySpeed = 2;
				//this.Scale =  new Vector2 ( 1.09f, 1.09f);
				AnimationViros01.Play("Virus_VZoster_Idel");
				
				break;
				
				case 5:
				Global.name = "Ébola";
				Global.EnemyMax_health = 12 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 9 * Global.enemy_level;
				Global.EnemySpeed = 1;
				AnimationViros01.Play("Virus_Ebola_Idel");
				
				break;
			
				
			default:
				Global.name = "Yersinia pestis";
				Global.EnemyMax_health = 10 * Global.enemy_level;
				Global.EnemyHealth = Global.EnemyMax_health;
				Global.EnemyDamage = 5 * Global.enemy_level;
				Global.EnemySpeed = 1;
				AnimationViros01.Play("Viros02_Idel");
				
				break;
		}
	}
	
	
	

	async void verificaAnima()
	{

			if(Global.Player_is_attack == true)
			{
				AnimationViros01.Play("Virus01_Damage");
				await SomeAsyncTask();
				if(Global.EnemyHealth <= 0)
				{
				AnimationViros01.Play("Virus_Death");	
				}
				
			}
			else
			{
				switch(tipoinimigo)
				{
					case 1:
					AnimationViros01.Play("Viros01_Idel");
					
					break;
					
					case 2:
					AnimationViros01.Play("Viros02_Idel");
					
					break;
					
					case 3:
					AnimationViros01.Play("Viros03_Idel");
					
					break;
					
					case 4:
					AnimationViros01.Play("Virus_VZoster_Idel");
					
					break;
					
					case 5:
					AnimationViros01.Play("Virus_Ebola_Idel");
					
					break;
					
					
					default:
					AnimationViros01.Play("Viros02_Idel");
					
					
					break;
				}
			}
		Global.Player_is_attack = false;
		
	}
	
	public void _on_area_2d_viros_01_area_entered(Area2D area)
	{
		//this.Position = (Vector2)posicao[rnd.Next(0,7)];
		
		
	}

	async Task SomeAsyncTask()
	{
		await Task.Delay(1000);
	}

}





