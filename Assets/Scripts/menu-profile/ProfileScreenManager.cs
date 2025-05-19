using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button editButton;

    private bool isEditing = false;

    private void Start()
    {
        editButton.onClick.AddListener(ToggleEditMode);

        // Always start in read-only mode
        SetEditMode(false);
    }

    private void ToggleEditMode()
    {
        SetEditMode(!isEditing);
    }

    private void SetEditMode(bool enable)
    {
        isEditing = enable;

        nameInputField.interactable = enable;
        nameInputField.readOnly = !enable;


        if (nameInputField.textComponent != null)
            nameInputField.textComponent.raycastTarget = enable;

        if (enable)
        {
            nameInputField.Select();
            nameInputField.ActivateInputField();
        }
    }
}
