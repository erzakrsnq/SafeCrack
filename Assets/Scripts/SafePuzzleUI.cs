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

    [Header("Puzzle Settings")]
    [SerializeField] private int maxAttempts = 3;

    private const int CorrectAnswer = 66;
    private int attemptsLeft;

    private void Start()
    {
        attemptsLeft = maxAttempts;
        sequenceText.text = "2 -> 6 -> 7 -> 21 -> 22 -> ?";
        statusText.text = $"Gib die naechste Zahl ein. Versuche: {attemptsLeft}/{maxAttempts}";

        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    private void OnDestroy()
    {
        if (submitButton != null)
            submitButton.onClick.RemoveListener(OnSubmitClicked);
    }

    private void OnSubmitClicked()
    {
        if (attemptsLeft <= 0)
        {
            statusText.text = "Keine Versuche mehr. Starte die Szene neu.";
            return;
        }

        if (!int.TryParse(answerInput.text, out int userAnswer))
        {
            statusText.text = $"Bitte gib eine ganze Zahl ein. Versuche: {attemptsLeft}/{maxAttempts}";
            return;
        }

        if (userAnswer == CorrectAnswer)
        {
            statusText.text = "Richtig! Der Safe akzeptiert die Zahl.";
            submitButton.interactable = false;
            answerInput.interactable = false;
            return;
        }

        attemptsLeft--;
        if (attemptsLeft > 0)
            statusText.text = $"Falsch. Verbleibende Versuche: {attemptsLeft}/{maxAttempts}";
        else
            statusText.text = "Falsch. Keine Versuche mehr. Safe bleibt geschlossen.";

        answerInput.text = "";
        answerInput.ActivateInputField();
    }
}