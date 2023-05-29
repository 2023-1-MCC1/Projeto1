using Godot;
using System;
//using Newtonsoft.Json;


public partial class Global : Node
{
	public static bool perdeu = false;
	public static Node2D NewSpaw = new Node2D();
	public static bool Tocou;
	public static PackedScene NewObject = new PackedScene();
	public static bool NewBattle = false;
	public Vector2 posSpawn;
	public static int MaxVidaCorpo = 100;
	public static int VidaCorpo = MaxVidaCorpo;
	public static bool GameOver = false;
	
	//State Base
	public static CharacterBody2D Player;
	public static int PlayerCurrent_health = 0;
	public static int PlayerMax_health = 0;
	public static int PlayerDamage = 0; 
	public static int PlayerDefense = 0;
	public static int PlayerSpeed = 0; 
	public static bool Player_is_attack;
	
	//Level Player
	public static int level = 1;
	public static int xp_ganho = 0; 
	public static int xp_atual = 0;
	public static int xp_up = 100;
	public static int xp_down = 0;
	public static bool Ganhaxp;
	
	//State Enemy
	public static string name;
	public static int enemy_level = 1;
	public static int EnemyHealth = 0;
	public static int EnemyMax_health = 25;
	public static int EnemySpeed = 0;
	public static int EnemyDamage = 0;
	public static int TipoEnemy = 1;
	public static int QntInimigo = 2;
	public static Sprite2D ImaInimigo = null;
	
	public static AnimationPlayer Anima;
	
	//Evento Aleatorio
	public static bool NewEvent = false;
	public static int TempoDEvento = -1;
	public static int TempoSEvento = 5;
	public static int tipoevento = 0;
	public static int eventoocorrido = 0;
	public static Random rnd = new Random();
	public static string TextRandom = "";
	public static string TextRandom1 = "";
	
	
	public override void _Ready()
	{
		Hero();
		TipoEventRadom(tipoevento);
	}
	
	public override void _Process(double delta)
	{
		
		Upgrade();
		VerificaEvento();
		Cronometro();
		VerificaGameOver();
		
	}
	public static void Hero(){
		
		PlayerMax_health = 35 * level;
		PlayerCurrent_health = PlayerMax_health;
		PlayerDamage = 10 * level; 
		PlayerDefense = 5 * level;
		PlayerSpeed = 2 * level; 
		
	}
	
	public static void Upgrade()
	{
		if (level < 1)
		{
			level = 1;
		}
		if(xp_atual >= xp_up)
		{
			xp_atual = xp_atual - xp_up ;
			xp_up +=  100 * level;
			level += 1;
			PlayerMax_health += 10 * level;
			PlayerDamage =  2 * level ;
			PlayerDefense = 1 * level ;
			PlayerSpeed = 1 * level ;
			PlayerCurrent_health = PlayerMax_health;
		}
		
				if(Ganhaxp == true){
					xp_ganho = 25 * enemy_level;
					xp_atual = xp_ganho + xp_atual ;
					Ganhaxp = false;
					
				}
	}
	public static int TipoEventRadom(int QualEvent)
	{
		if(enemy_level <= 0)
		{
			enemy_level = 1;
		}
		switch (QualEvent){
			case 1:
				TextRandom = "Surto de Gripe";
				TextRandom1 = "Os Inimigos estão mais forte, tenha cuidado";
				enemy_level +=  1;
				EnemyMax_health = EnemyMax_health * enemy_level;
				EnemyDamage = EnemyDamage * enemy_level;
				eventoocorrido = 1;
				break;
			case 2:
				TextRandom = "Campanha de Vacina";
				TextRandom1 = "Você esta mais forte, e seus inimigos estão mais fracos aproveite";
				//enemy_level -=  1;
				EnemyMax_health = EnemyMax_health - (2* enemy_level);
				EnemyDamage = EnemyDamage - (2* enemy_level);
				EnemySpeed = EnemySpeed - (1* enemy_level);
				PlayerCurrent_health  += (5 * level) ;
				PlayerDamage = PlayerDamage + (2 * level); 
				PlayerDefense = PlayerDefense + (1 * level);
				PlayerSpeed = PlayerSpeed  + (1 * level);
				eventoocorrido = 2;
				break;
				
				default:
					TextRandom = "Não há Evento";
					TextRandom1 = "...";
					
					break;
		}
		return tipoevento;
	}
	public static void Cronometro()
	{
		
		if(TempoSEvento == 0){
			TempoDEvento = 5;
			
		}
		if(TempoDEvento == 0){
			TempoSEvento = 5;
		}
	}
	public static void VerificaEvento()
	{
		//Console.WriteLine(eventoocorrido);
		if(TempoSEvento == 1 )
		{
			tipoevento =  rnd.Next(1,5);
			tipoevento = tipoevento;
			TipoEventRadom(tipoevento);
			TempoSEvento--;
			NewEvent = true;
			
		}
		else if (TempoDEvento == 1)
		{
			//tipoevento = 0;
			NewEvent = false;
		}

		if(TempoDEvento > 0 && eventoocorrido == 2 && NewEvent == false)
		{
				EnemyMax_health = EnemyMax_health + (2* enemy_level);
				EnemyDamage = EnemyDamage + (2* enemy_level);
				EnemySpeed = EnemySpeed + (1* enemy_level);
				PlayerCurrent_health -= 5;
				PlayerDamage = PlayerDamage - (2 * level); 
				PlayerDefense = PlayerDefense - (1 * level);
				PlayerSpeed = PlayerSpeed  - (1 * level);
				eventoocorrido  = 0;
		}
		if(eventoocorrido == 1 && NewEvent == false)
		{
			EnemyMax_health = EnemyMax_health / enemy_level;
			EnemyDamage = EnemyDamage / enemy_level;
			enemy_level = enemy_level - 1;
			
			eventoocorrido  = 0;
			
		}
		
	}
	public static void set_health(ProgressBar progressBar, int health, int max_health)
	{
		progressBar.Value = health;
		progressBar.MaxValue = max_health;
	}
	public void VerificaGameOver()
	{
		if(VidaCorpo <= 0 && NewBattle == true){
			GameOver = true;
			GetTree().ChangeSceneToFile("res://Cenas/game_over.tscn");
		}
		
		
	}
	
}
