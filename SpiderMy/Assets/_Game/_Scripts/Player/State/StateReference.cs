using SFRemastered._Game._Scripts.Player.State.Combat;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget.SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.State.Combat.ComboAttack;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat.SFRemastered.Combat;
using SFRemastered._Game._Scripts.State.Combat.LeapAttack;
using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered.Combat;
using SFRemastered.Combat.ZipAttack;
using SFRemastered.OnHitState;
using SFRemastered.Wall;
using UnityEngine;

namespace SFRemastered
{
    public class StateReference : MonoBehaviour
    {
        public IdleState IdleState;
        public DodgeState DodgeState;
        public SprintState SprintState;
        public WalkState WalkState;
        public WalkToIdleState WalkToIdleState;
        public LandToIdleState LandToIdleState;
        public LandRollState LandRollState;
        public LandNormalState LandNormalState;
        public SprintToIdleState SprintToIdleState;
        public SprintTurn180State SprintTurn180State;
        public JumpState JumpState;
        public FallState FallState;
        public DiveState DiveState;
        public SwingState SwingState;
        public JumpFromSwing JumpFromSwing;
        public ExitWall ExitWall;
        public WallRun WallRun;
        public JumpOffWall JumpOffWall;
        public JumpToSwing JumpToSwing;
        public JumpFromSwingLow JumpFromSwingLow;
        public ZipState ZipState;
        public ZipEnd ZipEnd;
        public ZipPointJump ZipPointJump;
        public EndPointLaunch EndPointLaunch;
        public NormalIdleCombat NormalIdleCombat;
        public LowIdleCombat LowIdleCombat;
        public CombatController CombatController;
        public LeapAttack LeapAttack;
        public FirstCombo FirstCombo;
        public SecondCombo SecondCombo;
        public StartZipAttack StartZipAttack;
        public ZipAirAttack ZipAirAttack;
        public EndZipAttack EndZipAttack;
        public UltimateSkill UltimateSkill;
        public GadgetAdapter GadgetAdapter;
        public WebShooter WebShooter;
        public HealingBotState HealingBotState;
        public StaggerState StaggerState;
        public KnockBackState KnockBackState;
        public OnVenomHitP1 OnVenomHitP1;
        public OnVenomHitP2 OnVenomHitP2;
        public OnVenomFinalHit OnVenomFinalHit;
        public Riseup Riseup;
        public StartZip StartZip;
    }
}