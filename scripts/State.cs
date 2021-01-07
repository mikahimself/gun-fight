using Godot;
using System;

public class State : Node2D {

  public StateMachine sm = null;

  // Receives events from _UnhandledInput() callback.
  public virtual void HandleInput(InputEvent e) {
    
  }

  // Corresponds to the _Process() callback
  public virtual void Process(float delta) {
    
  }

  // Corresponds to the _PhysicsProcess() callback
  public virtual void PhysicsProcess(float delta) {
    
  }

  public virtual void Enter() {
    
  }

  public virtual void Exit() {
    
  }

}