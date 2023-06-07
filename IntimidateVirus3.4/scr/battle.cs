using Godot;
using System;
using System.Threading.Tasks;
//using Newtonsoft.Json;
public partial class battle : Control
{
	[Export] 
	Resource enemy = null;
	bool is_defeding = false;
	//bool is_attack = false;
	public static bool morreu = false;
	public static int vez = 0;
	public int perfura = 0;
	public static AnimationPlayer AnimationViros01;
	public static AnimationPlayer HeroAnimation;
	public static int TempoAnim;
	PackedScene PackedSceneEnemy = ResourceLoader.Load("res://Cenas/viros_1.tscn") as PackedScene;
	PackedScene PackedSceneBoss = ResourceLoader.Load("res://Prefabs/boss.tscn") as PackedScene;
	
	public static AnimationPlayer animationPlayer;
	public override void _Ready()
	{ 
		Global.NewBattle = true;
		vez= 0;
		if(!Global.Boss)
		{
		GerarObjs(PackedSceneEnemy);
		VerificaLVL();
		}
		else{
			GerarObjs(PackedSceneBoss);
		}

		Global.PlayerCurrent_health = Global.PlayerCurrent_health;
		//Global.EnemyHealth = Global.EnemyHealth;
		
		GetNode<Panel>("Textbox").Hide();
		GetNode<Panel>("ActionsPanel").Hide();
		GetNode<Panel>("ActionsPanel").Show();
		
		//GetNode<Label>("EnemyPanel/EnemyContainer/ProgressBar/Label").Text = string.Format("HP: {0}/{1}", Global.EnemyHealth, Global.EnemyMax_health);
		//GetNode<Label>("EnemyPanel/level").Text = string.Format("LVL: {0}", Global.enemy_level);
		//GetNode<Label>("EnemyPanel/nome").Text = string.Format("{0}", Global.name);
		
		GetNode<Label>("PlayerPanel/PlayerData/ProgressBar/Label").Text = string.Format("HP: {0}/{1}", Global.PlayerCurrent_health, Global.PlayerMax_health);
		GetNode<Label>("PlayerPanel/PlayerXp/ProgressBar/xp").Text = string.Format("XP: {0}/{1}",Global.xp_atual, Global.xp_up);
		
		ProgressBar progress = GetNode<ProgressBar>("PlayerPanel/PlayerData/ProgressBar");
		ProgressBar progressLevel = GetNode<ProgressBar>("PlayerPanel/PlayerXp/ProgressBar");
		//ProgressBar progressEnemy = GetNode<ProgressBar>("EnemyPanel/EnemyContainer/ProgressBar");
		
		Global.set_health(progress, Global.PlayerCurrent_health, Global.PlayerMax_health);
		//set_health(progressEnemy,  Global.EnemyHealth,  Global.EnemyMax_health);
		set_level(progressLevel, Global.xp_atual, Global.xp_up);
		display_text("Você se depara com " + Global.name);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		HeroAnimation = GetNode<AnimationPlayer>("PlayerPanel/AnimationPlayerHero");
		HeroAnimation.Play("GB_Hero_Idel");
		
	}
		public override void _Process(double delta)
	{
		
		VerificaVez();
		VerificarMorte();
		VerificaLVL();
		Global.Upgrade();

		if( TempoAnim <= 0){
			TempoAnim = 20;
		}
		if(TempoAnim <= 4 && vez == 0 ){
			HeroAnimation.Play("GB_Hero_Idel2");
		}
		else {
			HeroAnimation.Play("GB_Hero_Idel");
		}
		
		set_level( GetNode<ProgressBar>("PlayerPanel/PlayerXp/ProgressBar"), Global.xp_atual, Global.xp_up);
		Global.set_health(GetNode<ProgressBar>("PlayerPanel/PlayerData/ProgressBar"), Global.PlayerCurrent_health,  Global.PlayerMax_health);
		
		GetNode<Label>("PlayerPanel/NumberNivel").Text = string.Format("{0}", Global.level);
		GetNode<Label>("PlayerPanel/PlayerXp/ProgressBar/xp").Text = string.Format("XP: {0}/{1}", Global.xp_atual, Global.xp_up);
		GetNode<Label>("PlayerPanel/PlayerData/ProgressBar/Label").Text = string.Format("HP: {0}/{1}", Global.PlayerCurrent_health , Global.PlayerMax_health);
	}
	
	public void set_level(ProgressBar progressLevel, int xp, int max_xp)
	{
		progressLevel.Value = xp;
		progressLevel.MaxValue = max_xp;
	}
	public  void display_text(string text)
	{
		GetNode<Panel>("Textbox").Show();
		GetNode<Label>("Textbox/Label").Text = text;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept") || (@event is InputEventMouseButton mouseButton))
		{
			GetNode<Panel>("Textbox").Hide();
			//EmitSignal("textbox_closed");
			}
	}
	async void _on_attack_pressed()
	{
		Global.Player_is_attack = true;
		VerificaVez();
		GetNode<Panel>("ActionsPanel").Hide();
		display_text(" Black-T girou o cabo da sua arma,desferindo \n um golpe podero causando " + Global.PlayerDamage);
		hero_turn();
		await SomeAsyncTask();
		animationPlayer.Play("enemy_damage");
		await SomeAsyncTask();
		vez ++;
	}
	async void enemy_turn()
	{	
		if(Global.EnemyDamage <= 0){Global.EnemyDamage = 0;}
		if(Global.EnemyHealth <= 0){Global.EnemyHealth = 0;}
		
		
		if (is_defeding)
		{
			perfura =  Global.EnemyDamage - Global.PlayerDefense;
			if(perfura <= 0){perfura = 0;}
			display_text(" Enfurecido por ter sido atingido o " + Global.name + ",\n rapidamente prepare seu ataque porem seu \n dano foi reduvido a " + perfura);
			Global.PlayerCurrent_health = Mathf.Max(0, Global.PlayerCurrent_health - perfura) ;
			is_defeding = false;
		}
		if(vez == 1 &&  Global.EnemyHealth > 0 && Global.NewBattle == true)
		{
			
			
			GetNode<Panel>("ActionsPanel").Hide();
			display_text(" Enfurecido por ter sido atingido o " + Global.name + ",\n rapidamente prepare seu ataque causando " + Global.EnemyDamage);
			Global.PlayerCurrent_health = Mathf.Max(0, Global.PlayerCurrent_health - Global.EnemyDamage);
			AnimationPlayer animationEnemy = GetNode<AnimationPlayer>("AnimationPlayer");
			animationEnemy.Play("shake");
		if(Global.EnemySpeed > Global.PlayerSpeed)
			{
				await SomeAsyncTask();
				display_text("Black-T girou o cabo da sua arma,desferindo \num golpe podero causando " + Global.PlayerDamage );
			}	
			vez --;
		}
		
		
		Global.set_health(GetNode<ProgressBar>("PlayerPanel/PlayerData/ProgressBar"), Global.PlayerCurrent_health,  Global.PlayerMax_health);
		GetNode<Label>("PlayerPanel/PlayerData/ProgressBar/Label").Text = string.Format("HP: {0}/{1}", Global.PlayerCurrent_health, Global.PlayerMax_health);
		GetNode<Panel>("ActionsPanel").Show();
		
	}
	async void hero_turn()
	{
		
		Global.EnemyHealth = Mathf.Max(0, Global.EnemyHealth - Global.PlayerDamage);
		Global.EnemyHealth = Global.EnemyHealth;
		
	}
	async void _on_defend_pressed()
	{
		GetNode<Panel>("ActionsPanel").Hide();
		is_defeding = true;
		display_text(" Reagindo rapidamente o poderoso ataque do \n"+Global.name+", Black-T tenta blocar o ataque \n com o seu brasos ");
		await SomeAsyncTask();
		vez = vez +1 ;
		await SomeAsyncTask();
	}
	async void _on_rugir_pressed()
	{
		
		Global.perdeu = true;
		Global.Tocou = false;
		GetNode<Panel>("ActionsPanel").Hide();
		display_text("Você fugiu");
		await SomeAsyncTask();
		GetTree().ChangeSceneToFile("res://Cenas/tela_fase_1.tscn");
	}
	async Task SomeAsyncTask()
	{
		await Task.Delay(1000);
	}

	async void VerificarMorte()
	{
		if(Global.PlayerCurrent_health <= 0)
		{
			Global.perdeu = true;
			display_text("No céu tem pão!");
			Global.enemy_level = Global.enemy_level + 1;
			Global.NewBattle = false;
			Global.QntInimigo++;
			Global.PlayerCurrent_health = Global.PlayerMax_health;
			
			Global.Hero();
			vez = 0;
		}
		
			if(Global.perdeu == true)
			{
				display_text("Voce morreu tente novamente");
				Global.VidaCorpo -= 10;
				Global.perdeu = false;
				GetNode<Label>("PlayerPanel/PlayerData/ProgressBar/Label").Text = string.Format("HP: {0}/{1}", Global.PlayerCurrent_health , Global.PlayerMax_health);
				Global.Tocou = false;
				Global.QntInimigo++;
				if(Global.DificuldadeEscolhida == 1)
				{
					GetTree().ChangeSceneToFile("res://Cenas/tela_fase_2.tscn");
				}
				else if(Global.DificuldadeEscolhida == 2)
				{
					GetTree().ChangeSceneToFile("res://Cenas/tela_fase_1.tscn");
				}
				else if(Global.DificuldadeEscolhida == 3)
				{
					GetTree().ChangeSceneToFile("res://Cenas/tela_fase_3.tscn");
				}
			}
		
		
		if( Global.EnemyHealth <= 0 && Global.NewBattle == true)
		{
			
			vez = 0;
			Global.NewBattle = false;
			GetNode<Panel>("ActionsPanel").Hide();
			display_text("O virus foi eliminado!!");
			Global.QntInimigo--;
			
			Global.Tocou = false;
			await SomeAsyncTask();
			Global.Ganhaxp = true;
			
			display_text("Após derrotar o " + Global.name +",você recebeu: " + Global.xp_ganho + " de \nxp");
			
			await SomeAsyncTask();
			Global.Ganhaxp = false;
			GetTree().ChangeSceneToFile("res://Cenas/tela_fase_1.tscn");
		}

	}	
	public static void VerificaLVL()
	{
		if(Global.enemy_level <= 0){
			Global.enemy_level =1;
		}
		if (Global.enemy_level > ( 2 * Global.level))
		{
			Global.enemy_level = ( 2 * Global.level);
		} 
	}
	
	public void GerarObjs(PackedScene NovoObj)
	{
		var customSprite = NovoObj.Instantiate();
		AddChild(customSprite);
		
	}
	async void VerificaVez()
	{

		if(vez == 1 &&  Global.EnemyHealth > 0 && Global.NewBattle == true )
		{
			
			enemy_turn();
			await SomeAsyncTask();
			Global.Player_is_attack= false;
			vez = 0;
		}
		

	}
	

		private void _on_timer_timeout()
	{
		TempoAnim -= 1 ;
	}
	
}



