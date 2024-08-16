using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Breakout.MainMenu.MainMenu.ContinueButton
{
    internal class ContinueButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ButtonText;
        [SerializeField] private Button ContinueButton;

        internal TextMeshProUGUI buttonText => ButtonText;
        internal Button continueButton => ContinueButton;
    }
}