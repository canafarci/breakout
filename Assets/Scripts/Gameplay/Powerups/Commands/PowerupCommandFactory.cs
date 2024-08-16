using UnityEngine;

using Breakout.Gameplay.Data;
using Breakout.Gameplay.Enums;
using Breakout.Infrastructure;

namespace Breakout.Gameplay.Powerups.Commands
{
    internal class PowerupCommandFactory : UnitySingleton<PowerupCommandFactory>
    {
        [SerializeField] private Paddle Paddle;
        [SerializeField] private PowerupDataSO PowerupDataSO;
        
        //this can be further optimized by caching the commands and reusing them
        //in order to avoid memory read/write bottlenecks and GC cleanups
        internal IPowerupCommand CreateCommand(PowerupID powerupID)
        {
            IPowerupCommand powerupCommand;
            
            if (powerupID == PowerupID.EnlargePaddle)
            {
                powerupCommand = new ChangePaddleSizeCommand(Paddle, PowerupDataSO.enlargedPaddleSize);
            }
            else if (powerupID == PowerupID.IncreaseHealth)
            {
                powerupCommand = new ChangeHealthCommand(1);
            }
            else if (powerupID == PowerupID.DecreaseHealth)
            {
                powerupCommand = new ChangeHealthCommand(-1);
            }
            else
            {
                powerupCommand = new ChangePaddleSizeCommand(Paddle, PowerupDataSO.shortenedPaddleSize);
            }

            return powerupCommand;
        }
    }
}