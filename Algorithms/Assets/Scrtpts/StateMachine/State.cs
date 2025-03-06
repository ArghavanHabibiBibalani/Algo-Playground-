using UnityEngine;

public abstract class State
{
    private GameStateMachine _stateMachine;
    public State(GameStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();




}
