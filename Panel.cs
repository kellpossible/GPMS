using Godot;
using System;

public class Panel : Godot.Panel
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        GetNode("Button").Connect("pressed", this, nameof(_OnButtonPressed));
    }

    public void _OnButtonPressed()
    {
        var label = (Label)GetNode("Label");
        label.Text = "Hello!";
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
