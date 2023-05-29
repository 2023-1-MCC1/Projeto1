using Godot;
using System;
using System.Threading.Tasks;
//using Newtonsoft.Json;

public partial class tela_fase_1 : Node2D
{
	
	//Camera2D camera1 = GetNode
	
	
	public int a;
	//public static Sprite2D teste = GetNode<Sprite2D>("res://Cenas/viros_1.tscn");	//public string caminho = "C:/Users/20010480/Desktop/IntimidateVirus2.0/Script/ArquivoSave.json";
	PackedScene PackedSceneEnemy = ResourceLoader.Load("res://Cenas/viros_1.tscn") as PackedScene;
	public static int TempoSpaw = 5;
	
	public override void _Ready()
	{
		Global.perdeu = false;
		Global.NewBattle = false;
		EventRandom1();
		QuantidadeInimigosMapa(Global.QntInimigo);
		VidaCorpo();
		
		
		if(Global.Tocou == true)
		{
		
		Global.Player = GetNode<CharacterBody2D>("Spaw1/PlayerCharac");
		Global.Player.GlobalPosition = new Vector2 (512,200);
		Global.Tocou = false;
		
		}
/*		
		
		var file = FileAccess.Open(caminho,FileAccess.ModeFlags.Read);
		var LinhaDoSave = file.GetLine();

		//State Base
		Save ObjetoSalvo = JsonConvert.DeserializeObject<Save>(LinhaDoSave);
		Global.PlayerCurrent_health = ObjetoSalvo.PlayerCurrent_health;
		Global.PlayerMax_health = ObjetoSalvo.PlayerMax_health;
		Global.PlayerDamage = ObjetoSalvo.PlayerDamage;
		Global.PlayerDefense = ObjetoSalvo.PlayerDefense;
		Global.PlayerSpeed = ObjetoSalvo.PlayerSpeed;
		
		//Level Player
		Global.level = ObjetoSalvo.level;
		Global.xp_ganho = ObjetoSalvo.xp_ganho;
		Global.xp_atual = ObjetoSalvo.xp_atual;
		Global.xp_up= ObjetoSalvo.xp_up;
		Global.xp_down = ObjetoSalvo.xp_down;

		GD.Print("Apareci!");
		*/
		//teste.Texture("res://Inimigos/VirusAnima/Layer 1_sprite_3.png");
	}
	
	public override void _Process(double delta)
	{
		VidaCorpo();
		if(Global.QntInimigo == 0 && TempoSpaw == 0)
		{
			GerarObjs(PackedSceneEnemy);
			Global.QntInimigo++;
		}
			
		if (TempoSpaw < 0){
			TempoSpaw = 5;
			
		}
		if(Global.TempoDEvento >= 0)
		{
			EventRandom(Global.TextRandom, Global.TextRandom1);
			//Console.WriteLine(Global.tipoevento + " " + Global.TextRandom);
		}
		else if(Global.TempoSEvento >= 0)
		{
			EventRandom1();
			//Console.WriteLine("teste2");
		}
		
		
		else if (Global.Tocou == false )
		{
			Global.NewSpaw = GetNode<Node2D>("Spaw1");
		}
		
	}
	
	private void _on_area_2d_viros_01_body_entered(CharacterBody2D PlayerBody)
	{
		Global.Tocou = true;
	 	GetTree().ChangeSceneToFile("res://scr/battle.tscn");
		
		Global.Player = PlayerBody;
	}
	
	async void EventRandom(string Event, string Event1)
	{
		GetNode<Panel>("CanvasLayer/PanelNotificacao").Show();
		GetNode<Label>("CanvasLayer/PanelNotificacao/LabelNoti").Text = Event;
		GetNode<Label>("CanvasLayer/PanelNotificacao/LabelTicke").Text = Event1;
	}
	
	async void EventRandom1()
	{
		GetNode<Panel>("CanvasLayer/PanelNotificacao").Hide();
	}

	async Task SomeAsyncTask()
	{
		await Task.Delay(2);
			//GetTree().Quit();
	}
	
	private void _on_texture_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cenas/Informação.tscn");
	}
	
	private void _on_button_pressed()
	{
		//var customSprite = PackedSceneEnemy.Instantiate();
		//AddChild(customSprite);
		GerarObjs(PackedSceneEnemy);
		Global.TipoEnemy = Global.rnd.Next(1,5);
	}
	/*
	public void _on_save_pressed()
	{
		
		Save ArquivoDeSave = new Save();

		ArquivoDeSave.PlayerCurrent_health = Global.PlayerCurrent_health;
		ArquivoDeSave.PlayerMax_health = Global.PlayerMax_health ;
		ArquivoDeSave.PlayerDamage = Global.PlayerDamage; 
		ArquivoDeSave.PlayerDefense = Global.PlayerDefense;
		ArquivoDeSave.PlayerSpeed = Global.PlayerSpeed; 
		
		//Level Player
		ArquivoDeSave.level = Global.level;
		ArquivoDeSave.xp_ganho = Global.xp_ganho; 
		ArquivoDeSave.xp_atual = Global.xp_atual;
		ArquivoDeSave.xp_up = Global.xp_up;
		ArquivoDeSave.xp_down = Global.xp_down;

		var file = FileAccess.Open(caminho,FileAccess.ModeFlags.Write);
		file.StoreString(JsonConvert.SerializeObject(ArquivoDeSave));
		GD.Print("Salvou");
		
	}*/
	
	private void _on_timer_timeout()
	{
		
		Global.TempoSEvento -= 1;
		Global.TempoDEvento -= 1;
		TempoSpaw -= 1;
		
	}
	
	public void GerarObjs(PackedScene NovoObj)
	{
		//Global.TipoEnemy = Global.rnd.Next(0,6);
		Global.TipoEnemy = 4;
		var customSprite = NovoObj.Instantiate();
		AddChild(customSprite);
		
	}
	public int QuantidadeInimigosMapa(int QntInimigoMapa)
	{
		for (QntInimigoMapa = QntInimigoMapa; QntInimigoMapa > 0; QntInimigoMapa-- )
		{
			GerarObjs(PackedSceneEnemy);
			//Global.TipoEnemy = Global.rnd.Next(1,3);
		}
		return QntInimigoMapa;
	}
	public void VidaCorpo()
	{
		ProgressBar BarraCorpo = GetNode<ProgressBar>("CanvasLayer/CorpoPanel/CorpoContainer/CorpoProgressBar");
		Global.set_health(BarraCorpo, Global.VidaCorpo, Global.MaxVidaCorpo);
		GetNode<Label>("CanvasLayer/CorpoPanel/CorpoContainer/CorpoProgressBar/VidaCorpoLabel").Text = string.Format("HP: {0}/{1}", Global.VidaCorpo, Global.MaxVidaCorpo);
	}
}











