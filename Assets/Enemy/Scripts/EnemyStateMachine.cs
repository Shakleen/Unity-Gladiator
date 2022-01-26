using UnityEngine;

public class EnemyStateMachine : StateMachine<Enemy> 
{
    private const float THRESH = 1e-3f;
    public float TauntTimer { get; private set; }
    public float AttackTimer { get; private set; }

    public void ResetTauntTimer() => TauntTimer = _context.Config.TauntCoolDown;

    public void ResetAttackTimer() => TauntTimer = _context.Config.AttackCoolDown;

    public bool IsTimerDone(float timer) => timer <= THRESH;
    
    public new void Execute()
    {
        TauntTimer = DecreaseTimer(TauntTimer);
        AttackTimer = DecreaseTimer(AttackTimer);
        base.Execute();
    }

    private float DecreaseTimer(float timer) => timer = IsTimerDone(timer) ? 0f : timer - Time.deltaTime;
}