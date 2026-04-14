using System.Collections.Generic;
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
    [SerializeField] private int maxAttemptsPerRun = 3;
    [SerializeField] private int puzzlesPerRun = 4;

    private readonly SequencePuzzle[] puzzlePool =
    {
        new SequencePuzzle { displayText = "2 -> 6 -> 7 -> 21 -> 22 -> ?", correctAnswer = 66 },
        new SequencePuzzle { displayText = "1 -> 4 -> 9 -> 16 -> 25 -> ?", correctAnswer = 36 },
        new SequencePuzzle { displayText = "3 -> 6 -> 12 -> 24 -> 48 -> ?", correctAnswer = 96 },
        new SequencePuzzle { displayText = "5 -> 10 -> 20 -> 40 -> 80 -> ?", correctAnswer = 160 },
        new SequencePuzzle { displayText = "5 -> 7 -> 11 -> 13 -> 17 -> 19 -> ?", correctAnswer = 23 },
        new SequencePuzzle { displayText = "2 -> 3 -> 5 -> 8 -> 13 -> ?", correctAnswer = 21 },
        new SequencePuzzle { displayText = "100 -> 50 -> 25 -> 12 -> 6 -> ?", correctAnswer = 3 },
        new SequencePuzzle { displayText = "4 -> 8 -> 16 -> 32 -> 64 -> ?", correctAnswer = 128 }
        new SequencePuzzle { displayText = "2 -> 10 -> 7 -> 15 -> 12 -> ?", correctAnswer = 20 },
        new SequencePuzzle { displayText = "2 -> 14 -> 11 -> 77 -> 74 -> ?", correctAnswer = 222 },
        new SequencePuzzle { displayText = "8 -> 4 -> 16 -> 12 -> 48 -> ?", correctAnswer = 44 },
        new SequencePuzzle { displayText = "1 -> 2 -> 4 -> 8 -> 16 -> ?", correctAnswer = 32 },
        new SequencePuzzle { displayText = "1 -> 3 -> 9 -> 27 -> 81 -> ?", correctAnswer = 243 },
    };

    private readonly List<SequencePuzzle> activePuzzles = new List<SequencePuzzle>();
    private int currentPuzzleIndex = 0;
    private int attemptsLeft;
    private int activePuzzleTarget;

    private void Start()
    {
        BuildRandomPuzzleSet();
        attemptsLeft = maxAttemptsPerRun;
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
        sequenceText.text = activePuzzles[currentPuzzleIndex].displayText;
        statusText.text = $"Reihe {currentPuzzleIndex + 1}/{activePuzzleTarget} - Versuche gesamt: {attemptsLeft}/{maxAttemptsPerRun}";
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    private void OnSubmitClicked()
    {
        if (!int.TryParse(answerInput.text, out int userAnswer))
        {
            statusText.text = $"Bitte gib eine ganze Zahl ein. Versuche gesamt: {attemptsLeft}/{maxAttemptsPerRun}";
            return;
        }

        if (userAnswer == activePuzzles[currentPuzzleIndex].correctAnswer)
        {
            currentPuzzleIndex++;

            if (currentPuzzleIndex >= activePuzzleTarget)
            {
                sequenceText.text = "SAFE GEÖFFNET";
                statusText.text = $"Level bestanden! Alle {activePuzzleTarget} Reihen korrekt gelöst.";
                submitButton.interactable = false;
                answerInput.interactable = false;
                return;
            }

            statusText.text = "Richtig! Nächste Reihe...";
            ShowCurrentPuzzle();
            return;
        }

        attemptsLeft--;
        if (attemptsLeft <= 0)
        {
            ResetSafeAfterFailure();
            return;
        }

        statusText.text = $"Falsch. Verbleibende Versuche gesamt: {attemptsLeft}/{maxAttemptsPerRun}";
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    private void ResetSafeAfterFailure()
    {
        BuildRandomPuzzleSet();
        currentPuzzleIndex = 0;
        attemptsLeft = maxAttemptsPerRun;
        ShowCurrentPuzzle();
        statusText.text = $"Zu viele Fehlversuche. Safe wurde zurückgesetzt. Reihe 1/{activePuzzleTarget} - Versuche gesamt: {attemptsLeft}/{maxAttemptsPerRun}";
    }

    private void BuildRandomPuzzleSet()
    {
        activePuzzles.Clear();
        activePuzzleTarget = Mathf.Clamp(puzzlesPerRun, 1, puzzlePool.Length);

        List<int> indices = new List<int>();
        for (int i = 0; i < puzzlePool.Length; i++)
            indices.Add(i);

        for (int i = indices.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            int tmp = indices[i];
            indices[i] = indices[swapIndex];
            indices[swapIndex] = tmp;
        }

        for (int i = 0; i < activePuzzleTarget; i++)
            activePuzzles.Add(puzzlePool[indices[i]]);
    }
}