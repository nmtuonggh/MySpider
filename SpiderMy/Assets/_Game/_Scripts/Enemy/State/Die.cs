namespace SFRemastered
{
    public class Die : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            _state.Events.OnEnd += () =>
            {
                _blackBoard.enemyData.ReturnToPool(_blackBoard.enemyData.id, _blackBoard.gameObject);
            };
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