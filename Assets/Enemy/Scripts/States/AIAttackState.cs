using System;

public class AIAttackState : AIBaseState
{
    private Transition _toIdle, _toDeath;

    public AIAttackState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) 
    {
        _toIdle = new Transition(GetStateType(), AIStateEnum.idle);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
    }

    public override Enum GetStateType() => AIStateEnum.attack;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() => CheckSwitchState();

    public override Transition GetTransition()
    {
        if (IsDead())
            return _toDeath;
        else if (!_aiAgent.AnimationHandler.IsAttacking)
            return _toIdle;
        
        return null;
    }

    public override void OnExitState(Transition transition) => _aiAgent.AnimationHandler.SetAnimationValueIsAttacking(false);
}