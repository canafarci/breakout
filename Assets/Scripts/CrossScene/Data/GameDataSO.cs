using UnityEngine;

namespace Breakout.CrossScene.Data
{
    [CreateAssetMenu(fileName = "Game Data SO", menuName = "Breakout/Gameplay/Game Data SO", order = 0)]
    public class GameDataSO : ScriptableObject
    {
        [SerializeField] private int LevelCountInGame;

        internal int levelCountInGame => LevelCountInGame;
    }
}