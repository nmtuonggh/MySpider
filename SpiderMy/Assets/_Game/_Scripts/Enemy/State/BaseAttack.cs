using _Game.Scripts.Event;

namespace SFRemastered
{
    public abstract class BaseAttack : EnemyBaseState
    {
        public GameEvent onDisableSpiderSense;
        public override void EnterState()
        {
            base.EnterState();
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            if (_blackBoard.attacking)
            {
                _blackBoard.warningAttack.SetActive(false);
                _blackBoard.attacking = false;
                onDisableSpiderSense.Raise();
            }
        }
    }
}