namespace Breakout.Gameplay.Powerups.Commands
{
    //decorator for template command
    internal class ChangeHealthCommand : PowerupCommand
    {
        private readonly int _changeAmount;

        internal ChangeHealthCommand(int changeAmount)
        {
            _changeAmount = changeAmount;
        }
        
        public override void Execute()
        {
            PlayerManager.instance.ChangePlayerHealth(_changeAmount);
        }
    }
}