using Godot;
using System;

public class Move : State {

  // Receives events from _UnhandledInput() callback.
  public override void HandleInput(InputEvent e) {
    
  }

  // Corresponds to the _Process() callback
  public override void Process(float delta) {
    if (!Input.IsActionPressed("MoveUp1") && 
        !Input.IsActionPressed("MoveDown1") &&
        !Input.IsActionPressed("MoveLeft1") &&
        !Input.IsActionPressed("MoveRight1")) {
          sm.TransitionTo("Idle");
        }
  }

  // Corresponds to the _PhysicsProcess() callback
  public override void PhysicsProcess(float delta) {
    
  }

  public override void Enter() {
    GD.Print("Moving");
  }

  public override void Exit() {
    
  }

}