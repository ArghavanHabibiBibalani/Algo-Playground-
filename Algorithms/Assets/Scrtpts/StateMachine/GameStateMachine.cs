using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private State _currentState;

    public void ChangeState(State newState)
    {
        if(_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();
    }

    private void Update()
    {
        if(_currentState != null)
        {
            _currentState.Update();
        }
    }
}
