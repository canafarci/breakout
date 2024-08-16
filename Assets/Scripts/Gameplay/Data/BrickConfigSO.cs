using UnityEngine;

using System.Collections.Generic;

namespace Breakout.Gameplay.Data
{
    [CreateAssetMenu(fileName = "Brick Config SO", menuName = "Breakout/Gameplay/Brick Config SO", order = 0)]
    public class BrickConfigSO : ScriptableObject
    {
        [SerializeField] private List<BrickDatum> BrickData;

        internal List<BrickDatum> brickData => BrickData;
    }
    
}