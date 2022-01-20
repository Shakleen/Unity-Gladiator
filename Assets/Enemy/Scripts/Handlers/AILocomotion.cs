using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour 
{
    private const float TIMER_THRESH = 1e-3f;

    #region Component references
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    #endregion

    // TODO: Remove SerializeFields
    private float _updatePathTimer;
    private float _attackCoolDownTimer;
    private float _tauntCoolDownTimer;
    private int _tauntCount;
    
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public float AttackCoolDownTimer { get => _attackCoolDownTimer; }
    public float TauntCoolDownTimer { get => _tauntCoolDownTimer; }

    private void Start() => _updatePathTimer = _aiAgent.Config.MinimumUpdateWaitTime;

    public void UpdateAgentPath()
    {
        if (HasTimerRunOut(_updatePathTimer))
        {
            _updatePathTimer = _aiAgent.Config.MinimumUpdateWaitTime;

            if (PlayerPositionChange() >= Square(_aiAgent.Config.MinimumUpdateDistance))
                _navMeshAgent.destination = _aiAgent.PlayerTransform.position;
        }
    }

    public float PlayerPositionChange() => (_aiAgent.PlayerTransform.position - _navMeshAgent.destination).sqrMagnitude;

    public float DistanceBetweenPlayerAndAgent() => (_aiAgent.PlayerTransform.position - transform.position).sqrMagnitude;

    public bool IsInRadius(float radius) => DistanceBetweenPlayerAndAgent() <= Square(radius);

    private float Square(float value) => value * value;
    
    public void DecreaseTimer()
    {
        _updatePathTimer = TimerCountDown(_updatePathTimer);
        _attackCoolDownTimer = TimerCountDown(_attackCoolDownTimer);
        _tauntCoolDownTimer = TimerCountDown(_tauntCoolDownTimer);
    }

    private float TimerCountDown(float timer)
    {
        if (HasTimerRunOut(timer))
            timer = 0f;
        else
            timer -= Time.deltaTime;
        
        return timer;
    }

    public void MoveAgent() => _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(_navMeshAgent.velocity.magnitude);

    public bool HasTimerRunOut(float timer) => timer <= TIMER_THRESH;

    public bool ReachedMaxTaunts() => _tauntCount == _aiAgent.Config.MaxTaunts;

    public void IncremetTauntCount() => _tauntCount++;

    public void ResetTauntCount() => _tauntCount = 0;

    public void ResetTauntCoolDown() => _tauntCoolDownTimer = _aiAgent.Config.TauntCoolDown;

    public void ResetAttackCoolDown() => _attackCoolDownTimer = _aiAgent.Config.AttackCoolDown;
}