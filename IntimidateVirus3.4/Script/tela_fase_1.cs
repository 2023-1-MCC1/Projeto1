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
	PackedScene PackedSceneBoss = ResourceLoader.Load("res://Prefabs/boss.tscn") as PackedScene;
	public static int TempoSpaw = 5;
	
	public override void _Ready()
	{
		Global.VerificaDificuldade(Global.DificuldadeEscolhida);
		Global.inicio = false;
		Global.perdeu = false;
		Global.NewBattle = false;
		EventRandom1();
		VidaCorpo();
		VerificaPosition(Global.posicao);
		Console.WriteLine("QNT: " + Global.QntInimigo);
		if(Global.Tocou == true)
		{
		Global.Player = GetNode<CharacterBody2D>("Spaw1/PlayerCharac");
		Global.Player.GlobalPosition = new Vector2 (512,200);
		Global.Tocou = false;
		
		}
	}
	
	public override void _Process(double delta)
	{
		VidaCorpo();
		
		if(Global.tipoevento == 4 && Global.gerou == true)
		{
			GerarObjs(PackedSceneEnemy);
			Global.QntInimigo++;
			VerificaPosition(Global.posicao);
			Global.gerou =false;
		}
		if(Global.QntInimigo <= 0)
		{
			GetTree().ChangeSceneToFile("res://Cenas/i_win.tscn");
		}
		
			
			
			
		if (TempoSpaw < 0){
			TempoSpaw = 5;
			
		}
		if(Global.TempoDEvento >= 0)
		{
			EventRandom(Global.TextRandom, Global.TextRandom1);
			
		}
		else if(Global.TempoSEvento >= 0)
		{
			EventRandom1();
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
	
	
	private void _on_timer_timeout()
	{
		
		Global.TempoSEvento -= 1;
		Global.TempoDEvento -= 1;
		TempoSpaw -= 1;
		
	}
	public void VerificaPosition(Vector2[] NovaPosition)
	{	
		
		for(int i = 0; i < NovaPosition.Length && i < Global.QntInimigo; i++)
		{
			Global.PositionEnemy[i] = NovaPosition[i];
			Global.PosEnemy = Global.PositionEnemy[i];
			GerarObjs(PackedSceneEnemy);
			Console.WriteLine("posi: " + Global.PositionEnemy[i]);
			Console.WriteLine("newPosi: " + NovaPosition[i]);
			Console.WriteLine(i);
		}
	}
	
	
	public void GerarObjs(PackedScene NovoObj)
	{
		var customSprite = NovoObj.Instantiate();
		AddChild(customSprite);
		
	}
	
	public void VidaCorpo()
	{
		ProgressBar BarraCorpo = GetNode<ProgressBar>("CanvasLayer/CorpoPanel/CorpoContainer/CorpoProgressBar");
		Global.set_health(BarraCorpo, Global.VidaCorpo, Global.MaxVidaCorpo);
		GetNode<Label>("CanvasLayer/CorpoPanel/CorpoContainer/CorpoProgressBar/VidaCorpoLabel").Text = string.Format("HP: {0}/{1}", Global.VidaCorpo, Global.MaxVidaCorpo);
	}
}











