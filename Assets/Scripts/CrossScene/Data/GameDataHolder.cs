using UnityEngine;

using Breakout.Infrastructure;

namespace Breakout.CrossScene.Data
{
    public class GameDataHolder : UnitySingleton<GameDataHolder>
    {
        [SerializeField] private GameDataSO GameDataSO;

        public int levelCountInGame => GameDataSO.levelCountInGame;
    }
}