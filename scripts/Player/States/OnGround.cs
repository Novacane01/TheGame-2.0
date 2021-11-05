using Godot;
using System;
    public abstract class OnGround : PlayerState {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        // Called when the node enters the scene tree for the first time.
        public override void UnhandledInput(InputEvent _event) {
            if(Input.IsActionJustPressed("jump")){
                player.velocity.y = -player.JUMP_POWER;
            }
        }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    }

