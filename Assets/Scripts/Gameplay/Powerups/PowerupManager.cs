using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using Breakout.Gameplay.Data;
using Breakout.Gameplay.State;
using Breakout.Infrastructure;

namespace Breakout.Gameplay.Powerups
{
    internal class PowerupManager : UnitySingleton<PowerupManager>
    {
        [SerializeField] private PowerupDataSO PowerupDataSO;
        
        private readonly Stack<int> _powerupEarnTreshold = new Stack<int>();
        private void Start()
        {
            Brick.OnBrickDestroyed += BrickDestroyedHandler;

            InitializePowerupEarnTresholds();
        }

        private void InitializePowerupEarnTresholds()
        {
            List<Brick> bricksInGame = GameManager.instance.bricksInGame;
            
            //calculate total score that can be earned in a level, then divide it by 
            //the powerup count that can be earned
            int totalScoreToBeEarnedInLevel = bricksInGame.Sum(x => x.brickDestroyScore);
            int powerupsPerLevel = PowerupDataSO.powerupsPerLevel;
            int powerupEarnStep = totalScoreToBeEarnedInLevel / powerupsPerLevel;
            
            //offset by half a step to avoid spawning at the maximum score
            totalScoreToBeEarnedInLevel -= powerupEarnStep / 2;
            
            //push to stack by subtracting step amount from the total amount
            for (int i = 0; i < powerupsPerLevel; i++)
            {
                _powerupEarnTreshold.Push(totalScoreToBeEarnedInLevel);
                totalScoreToBeEarnedInLevel -= powerupEarnStep;
            }
        }
        
        //listen to brick destroyed event and
        //determine if a powerup needs to be spawned
        private void BrickDestroyedHandler(Brick brick)
        {
            if (!_powerupEarnTreshold.TryPeek(out int powerupEarnTreshold)) return;
            
            int playerScore = GameManager.instance.playerScore;

            if (powerupEarnTreshold < playerScore)
            {
                _powerupEarnTreshold.Pop();

                PowerupFactory.instance.SpawnPowerup(brick.transform.localPosition);
            }
        }
    }
}