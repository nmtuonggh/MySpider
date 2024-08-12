using _Game.Scripts.Event;

namespace SFRemastered
{
    public class SentEvent : EnemyBaseState
    {
        public GameEvent gameEvent;
        public override void EnterState()
        {
            gameEvent.Raise();
        }

        public override StateStatus UpdateState()
        {
             base.UpdateState();
             
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}