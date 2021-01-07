using Godot;
using System;

public class StateMachine : Node2D {

  [Signal]
  public delegate void Transitioned(string StateName);

  [Export]
  public NodePath initialState;

  public State state;

  public override void _Ready() {
    state = (State)GetNode(initialState);
    foreach(State child in GetChildren()) {
      child.sm = this;
    }
    state.Enter();
  }

  public override void _UnhandledInput(InputEvent e) {
    state.HandleInput(e);
  }

  public override void _Process(float delta) {
    state.Process(delta);
  }

  public override void _PhysicsProcess(float delta) {
    state.PhysicsProcess(delta);
  }

  public void TransitionTo(String targetStateName) {
    if (!HasNode(targetStateName)) {
      return;
    }
    state.Exit();
    state = (State)GetNode(targetStateName);
    state.Enter();
    EmitSignal(nameof(Transitioned), targetStateName);
  }
}