using Breakout.Infrastructure;

namespace Breakout.Gameplay.Powerups.Commands
{
    internal class PowerupCommandInvoker : Singleton<PowerupCommandInvoker>
    {
        internal void InvokeCommand(IPowerupCommand command)
        {
            command.Execute();
        }
    }
}