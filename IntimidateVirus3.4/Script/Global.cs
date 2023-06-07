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
	public static int InicioEvent, FimEvent;
	public static int DificuldadeEscolhida;
	public static bool inicio = true;
	public static bool Boss = false;
	
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
	public static int QntInimigo = 0;
	public static Sprite2D ImaInimigo = null;
	public static bool gerou;
	public static AnimationPlayer Anima;
	public static Vector2 PosEnemy;
	public static Vector2[] PositionEnemy;
	
	//Evento Aleatorio
	public static bool NewEvent = false;
	public static int TempoDEvento = -1;
	public static int TempoSEvento = 5;
	public static int tipoevento = 0;
	public static int eventoocorrido = 0;
	public static Random rnd = new Random();
	public static string TextRandom = "";
	public static string TextRandom1 = "";
	
	public static Vector2[] posicao;
	
	
	public override void _Ready()
	{
		VerificaDificuldade(DificuldadeEscolhida);
		
		posicao = new Vector2[]
		{new Vector2(808, -504),new Vector2(848, -184),
		new Vector2(1296, -224),new Vector2(272,-576),
		new Vector2(1391, -1040),new Vector2(615, 200),
		new Vector2(1271, 184)};
		
		PositionEnemy = new Vector2[7];
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
			PlayerDamage +=  2 * level ;
			PlayerDefense += 1 * level ;
			PlayerSpeed += 1 * level ;
			PlayerCurrent_health = PlayerMax_health;
		}
		
		if(Ganhaxp == true)
		{
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
				TextRandom = "Falta de Medicos";
				TextRandom1 = "Pelo a desqualificação medica suas celulas recebeu recebeu dano.";
				//enemy_level -=  1;
				PlayerCurrent_health -= 5 * level;
				eventoocorrido = 1;
				break;
				
			case 2:
				TextRandom = "Falta de Infra Estrutura Hospitalar";
				TextRandom1 = "Pela falta de Investimento hostilar, você nao consegue se tratar \n a quantidade de inimigos aumentam ";
				gerou = true;
				eventoocorrido = 2;
				break;
				
			case 3:
				TextRandom = "Falta de Saniamento";
				TextRandom1 = "Na falta de cuidados com o saneamento, houve um aumento de\n inimigos no mapa";
				//enemy_level -=  1;
				QntInimigo = 1;
				eventoocorrido = 3;
				break;
				
			case 4:
				
				TextRandom = "Falta de Remedio";
				TextRandom1 = "Pela falta de investimento na reposição de remédios,\n sus barra de vida irá diminuir";
				//enemy_level -=  1;
				VidaCorpo -= 5;
				eventoocorrido = 4;
				break;
				
			case 5:
				
				TextRandom = "Surto de Gripe";
				TextRandom1 = "Os Inimigos estão mais forte, tenha cuidado";
				enemy_level +=  1;
				EnemyMax_health = EnemyMax_health * enemy_level;
				EnemyDamage = EnemyDamage * enemy_level;
				eventoocorrido = 5;
				break;
				
				
				// Buff
			case 6:
				TextRandom = "Campanha de Vacina";
				TextRandom1 = "Você esta mais forte, e seus inimigos estão mais fracos aproveite";
				//enemy_level -=  1;
				EnemyMax_health = EnemyMax_health - (2 * enemy_level);
				EnemyDamage = EnemyDamage - (2 * enemy_level);
				EnemySpeed = EnemySpeed - (1 * enemy_level);
				PlayerCurrent_health  += (5 * level) ;
				PlayerDamage = PlayerDamage + (4 * level); 
				PlayerDefense = PlayerDefense + (5 * level);
				PlayerSpeed = PlayerSpeed  + (2 * level);
				eventoocorrido = 6;
				break;
				
			case 7:
				TextRandom = "Tratamento de Aguá";
				TextRandom1 = "Com mais aguá potavel, suas celulas se curam";
				if(PlayerCurrent_health >= PlayerMax_health)
				{
				PlayerCurrent_health  += (5 * level) ;
				}
				eventoocorrido = 7;
				break;
				
			case 8:
				TextRandom = "Rede Hospitalar";
				TextRandom1 = "Com o almento nas redes hospitalares; a tacha de doenças teve \numa queda siginificativa";
				//enemy_level -=  1;
				EnemyMax_health = EnemyMax_health - (2 * enemy_level);
				EnemyDamage = EnemyDamage - (2 * enemy_level);
				EnemySpeed = EnemySpeed - (1 * enemy_level);
				PlayerCurrent_health  += (5 * level) ;
				PlayerDamage = PlayerDamage + (1 * level); 
				PlayerDefense = PlayerDefense + (1 * level);
				PlayerSpeed = PlayerSpeed  + (1 * level);
				eventoocorrido = 8;
				break;
				
			case 9:
				TextRandom = "Qualificação Medica";
				TextRandom1 = "Com o investimento nos profissionais da saúde, a vida do seu\n corpo ira se regenerar";
				//enemy_level -=  1;
				if(VidaCorpo < MaxVidaCorpo && VidaCorpo > 0)
				{
					VidaCorpo += 5;
				}
				eventoocorrido = 9;
				break;
				
			case 10:
				TextRandom = "Campanha de Prevenção Governamental";
				TextRandom1 = "Com o interesse do dever em ajudar a população haverá um\n grande aumento nos seus status";
				//enemy_level -=  1;
				PlayerDamage = PlayerDamage + (3 * level); 
				PlayerDefense = PlayerDefense + (3 * level);
				xp_ganho *= 2;
				eventoocorrido = 10;
				
				break;
			case 99:
				TextRandom = "Um inimigo muito forte apareceu";
				TextRandom1 = "";
				Global.TipoEnemy = 99;
				Boss = true;
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
		
		if(TempoSEvento == 1 )
		{
			tipoevento =  rnd.Next(InicioEvent,FimEvent);
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
		if(VidaCorpo <= 0 && GameOver == false){
			GameOver = true;
			GetTree().ChangeSceneToFile("res://Cenas/game_over.tscn");
			
		}
	}
	
	public static void VerificaDificuldade(int Dificuldade)
	{
		switch(Dificuldade)
		{
			case 1:
				InicioEvent = 4;
				FimEvent = 11;
				EnemyDamage = (EnemyDamage/2);
				Global.TipoEnemy = Global.rnd.Next(0,4);
				if (inicio == true)
				{
				QntInimigo = 3;
				}
				Console.WriteLine("Europa");
			break;
			case 2:
				InicioEvent = 0;
				FimEvent = 11;
				Global.TipoEnemy = Global.rnd.Next(2,6);
				if (inicio == true)
				{
				QntInimigo = 4;
				}
				Console.WriteLine("america");
			break;
			case 3:
				InicioEvent = 0;
				FimEvent = 8;
				Global.TipoEnemy = Global.rnd.Next(3,6);
				if (inicio == true)
				{
				QntInimigo = 5;
				}
				
				EnemyDamage = (EnemyDamage*2);
				Console.WriteLine("Africa");
			break;
			
			default:
				Dificuldade = DificuldadeEscolhida;
				Console.WriteLine("Defalut");
				break;
		}
	}
}
