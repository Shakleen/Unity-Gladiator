using System;

public class AIAttackState : AIBaseState
{
    private Transition _toAware, _toDeath;

    public AIAttackState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) 
    {
        _toAware = new Transition(GetStateType(), AIStateEnum.aware);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
    }

    public override Enum GetStateType() => AIStateEnum.attack;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() => CheckSwitchState();

    public override Transition GetTransition()
    {
        if (_aiAgent.Health.IsEmpty())
            return _toDeath;
        else if (!_aiAgent.AnimationHandler.IsAttacking)
            return _toAware;
        
        return null;
    }

    public override void OnExitState(Transition transition) => _aiAgent.AnimationHandler.SetAnimationValueIsAttacking(false);
}