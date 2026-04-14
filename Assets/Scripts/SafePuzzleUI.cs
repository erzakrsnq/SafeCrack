using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafePuzzleUI : MonoBehaviour
{
    [System.Serializable]
    private class SequencePuzzle
    {
        public string displayText;
        public int correctAnswer;
    }

    [Header("UI References")]
    [SerializeField] private TMP_Text sequenceText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text statusText;

    [Header("Puzzle Settings")]
    [SerializeField] private int maxAttemptsPerSequence = 3;

    private readonly SequencePuzzle[] puzzles =
    {
        new SequencePuzzle { displayText = "2 -> 6 -> 7 -> 21 -> 22 -> ?", correctAnswer = 66 },
        new SequencePuzzle { displayText = "1 -> 4 -> 9 -> 16 -> 25 -> ?", correctAnswer = 36 },
        new SequencePuzzle { displayText = "3 -> 6 -> 12 -> 24 -> 48 -> ?", correctAnswer = 96 },
        new SequencePuzzle { displayText = "5 -> 10 -> 20 -> 40 -> 80 -> ?", correctAnswer = 160 }
    };

    private int currentPuzzleIndex = 0;
    private int attemptsLeft;

    private void Start()
    {
        attemptsLeft = maxAttemptsPerSequence;
        submitButton.onClick.AddListener(OnSubmitClicked);
        ShowCurrentPuzzle();
    }

    private void OnDestroy()
    {
        if (submitButton != null)
            submitButton.onClick.RemoveListener(OnSubmitClicked);
    }

    private void ShowCurrentPuzzle()
    {
        sequenceText.text = puzzles[currentPuzzleIndex].displayText;
        statusText.text = $"Reihe {currentPuzzleIndex + 1}/4 - Versuche: {attemptsLeft}/{maxAttemptsPerSequence}";
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    private void OnSubmitClicked()
    {
        if (!int.TryParse(answerInput.text, out int userAnswer))
        {
            statusText.text = $"Bitte gib eine ganze Zahl ein. Versuche: {attemptsLeft}/{maxAttemptsPerSequence}";
            return;
        }

        if (userAnswer == puzzles[currentPuzzleIndex].correctAnswer)
        {
            currentPuzzleIndex++;

            if (currentPuzzleIndex >= puzzles.Length)
            {
                sequenceText.text = "SAFE GEOEFFNET";
                statusText.text = "Level bestanden! Alle 4 Reihen korrekt geloest.";
                submitButton.interactable = false;
                answerInput.interactable = false;
                return;
            }

            attemptsLeft = maxAttemptsPerSequence;
            statusText.text = "Richtig! Naechste Reihe...";
            ShowCurrentPuzzle();
            return;
        }

        attemptsLeft--;
        if (attemptsLeft <= 0)
        {
            ResetSafeAfterFailure();
            return;
        }

        statusText.text = $"Falsch. Verbleibende Versuche: {attemptsLeft}/{maxAttemptsPerSequence}";
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    private void ResetSafeAfterFailure()
    {
        currentPuzzleIndex = 0;
        attemptsLeft = maxAttemptsPerSequence;
        ShowCurrentPuzzle();
        statusText.text = $"Zu viele Fehlversuche. Safe wurde zurueckgesetzt. Reihe 1/4 - Versuche: {attemptsLeft}/{maxAttemptsPerSequence}";
    }
}