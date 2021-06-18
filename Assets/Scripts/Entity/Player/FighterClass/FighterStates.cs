using Player;

namespace Entity.Player.FighterClass
{
    public class FighterStates : PlayerStates
    {
        private Fighter _fighter;

        protected override void Start()
        {
            _fighter = GetComponent<Fighter>();
            
            //_basicAttackState = new FighterAbilityState(_fighter, new ShootArrowAbility(_fighter,KeyCode.LeftControl,1, null, null));
            
            base.Start();
        }
    }
}