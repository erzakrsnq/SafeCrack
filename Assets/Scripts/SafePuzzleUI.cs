using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafePuzzleUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text sequenceText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text statusText;

    private void Start()
    {
        // Schritt 1: UI-Gerüst (noch keine echte Spiel-Logik)
        sequenceText.text = "2 -> 6 -> 7 -> 21 -> 22 -> ?";
        statusText.text = "Gib eine Zahl ein und drücke Prüfen.";

        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    private void OnDestroy()
    {
        if (submitButton != null)
        {
            submitButton.onClick.RemoveListener(OnSubmitClicked);
        }
    }

    private void OnSubmitClicked()
    {
        statusText.text = "Eingabe erhalten: " + answerInput.text;
    }
}
