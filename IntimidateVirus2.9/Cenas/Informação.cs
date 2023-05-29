using Godot;
using System;

public partial class Informação : Node2D
{
	bool teste = true;
	Vector2 lucar;
	Vector2 lucarEbola;
	Vector2 lucarVZoster;
	Vector2 lucarYersinia;
	public override void _Ready()
	{
		 lucar = GetNode<TextureButton>("Carta_Influenza").Position;
		 lucarEbola = GetNode<TextureButton>("Carta_Ebola").Position;
		 lucarVZoster = GetNode<TextureButton>("Carta_VZoster").Position;
		 lucarYersinia = GetNode<TextureButton>("Carta_Yersinia").Position;
	}
	public override void _Process(double delta)
	{
		
	}
	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cenas/tela_fase_1.tscn");
	}
	private void _on_carta_influenza_button_up()
	{
		if (teste)
		{
			GetNode<TextureButton>("Carta_Influenza").Scale += new Vector2(2,2);
			GetNode<TextureButton>("Carta_Influenza").Position = new Vector2(350,20);
			GetNode<TextureButton>("Carta_Ebola").Hide();
			GetNode<TextureButton>("Carta_VZoster").Hide();
			GetNode<TextureButton>("Carta_Yersinia").Hide();
			
			teste = false;
		}
		else 
		{
			GetNode<TextureButton>("Carta_Influenza").Scale -= new Vector2(2,2);
			GetNode<TextureButton>("Carta_Influenza").Position = lucar;
			GetNode<TextureButton>("Carta_Ebola").Show();
			GetNode<TextureButton>("Carta_VZoster").Show();
			GetNode<TextureButton>("Carta_Yersinia").Show();
			teste = true;
		}
	}
	private void _on_carta_ebola_pressed()
	{
		if (teste)
		{
			GetNode<TextureButton>("Carta_Ebola").Scale += new Vector2(2,2);
			GetNode<TextureButton>("Carta_Ebola").Position = new Vector2(350,20);
			GetNode<TextureButton>("Carta_Influenza").Hide();
			GetNode<TextureButton>("Carta_VZoster").Hide();
			GetNode<TextureButton>("Carta_Yersinia").Hide();
			teste = false;
		}
		else 
		{
			GetNode<TextureButton>("Carta_Ebola").Scale -= new Vector2(2,2);
			GetNode<TextureButton>("Carta_Ebola").Position = lucarEbola;
			GetNode<TextureButton>("Carta_Influenza").Show();
			GetNode<TextureButton>("Carta_VZoster").Show();
			GetNode<TextureButton>("Carta_Yersinia").Show();

			teste = true;
		}
	}
	private void _on_carta_v_zoster_pressed()
	{
		if (teste)
		{
			GetNode<TextureButton>("Carta_VZoster").Scale += new Vector2(2,2);
			GetNode<TextureButton>("Carta_VZoster").Position = new Vector2(350,20);
			GetNode<TextureButton>("Carta_Influenza").Hide();
			GetNode<TextureButton>("Carta_Ebola").Hide();
			GetNode<TextureButton>("Carta_Yersinia").Hide();
			teste = false;
		}
		else 
		{
			GetNode<TextureButton>("Carta_VZoster").Scale -= new Vector2(2,2);
			GetNode<TextureButton>("Carta_VZoster").Position = lucarVZoster;
			GetNode<TextureButton>("Carta_Influenza").Show();
			GetNode<TextureButton>("Carta_Ebola").Show();
			GetNode<TextureButton>("Carta_Yersinia").Show();
			teste = true;
		}
	}
	private void _on_carta_yersinia_pressed()
	{
		if (teste)
		{
			GetNode<TextureButton>("Carta_Yersinia").Scale += new Vector2(2,2);
			GetNode<TextureButton>("Carta_Yersinia").Position = new Vector2(350,20);
			GetNode<TextureButton>("Carta_Influenza").Hide();
			GetNode<TextureButton>("Carta_Ebola").Hide();
			GetNode<TextureButton>("Carta_VZoster").Hide();
			teste = false;
		}
		else 
		{
			GetNode<TextureButton>("Carta_Yersinia").Scale -= new Vector2(2,2);
			GetNode<TextureButton>("Carta_Yersinia").Position = lucarYersinia;
			GetNode<TextureButton>("Carta_Influenza").Show();
			GetNode<TextureButton>("Carta_Ebola").Show();
			GetNode<TextureButton>("Carta_VZoster").Show();
			teste = true;
		}
	}
	
	
}















