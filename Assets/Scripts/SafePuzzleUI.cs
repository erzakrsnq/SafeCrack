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

    private const int CorrectAnswer = 66;

    private void Start()
    {
        sequenceText.text = "2 -> 6 -> 7 -> 21 -> 22 -> ?";
        statusText.text = "Gib die nächste Zahl ein.";

        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    private void OnDestroy()
    {
        if (submitButton != null)
            submitButton.onClick.RemoveListener(OnSubmitClicked);
    }

    private void OnSubmitClicked()
    {
        if (!int.TryParse(answerInput.text, out int userAnswer))
        {
            statusText.text = "Bitte gib eine ganze Zahl ein.";
            return;
        }

        statusText.text = userAnswer == CorrectAnswer
            ? "Richtig! Der Safe akzeptiert die Zahl."
            : "Falsch, versuch es nochmal.";

        answerInput.text = "";
        answerInput.ActivateInputField();
    }
}