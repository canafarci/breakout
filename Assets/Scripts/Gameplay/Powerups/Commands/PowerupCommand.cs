namespace Breakout.Gameplay.Powerups.Commands
{
    //template class for commands
    internal abstract class PowerupCommand :  IPowerupCommand
    {
        public abstract void Execute();
    }
}