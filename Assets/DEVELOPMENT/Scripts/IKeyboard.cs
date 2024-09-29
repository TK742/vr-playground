using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IKeyboard : MonoBehaviour, OVRVirtualKeyboard.ITextHandler
{
    [SerializeField]
    private OVRVirtualKeyboard virtualKeyboard;

    [SerializeField]
    private TMP_InputField inputField;

    void Start()
    {
        inputField.onSelect.AddListener(OnInputFieldSelect);
        inputField.onValueChanged.AddListener(OnInputFieldValueChange);
        virtualKeyboard.CommitText += OnCommitText;
        virtualKeyboard.Backspace += OnBackspace;
        virtualKeyboard.KeyboardHidden += OnKeyboardHidden;
    }

    private void OnInputFieldValueChange(string arg0)
    {
        virtualKeyboard.ChangeTextContext(arg0);
    }

    private void OnInputFieldSelect(string arg0)
    {
        virtualKeyboard.ChangeTextContext(arg0);
        virtualKeyboard.gameObject.SetActive(true);
    }

    private void OnKeyboardHidden()
    {
        if (!inputField.isFocused)
        {
            return;
        }
        // if the user hides the keyboard
        inputField.DeactivateInputField();
    }

    private void OnCommitText(string arg0)
    {
        if (!inputField.isFocused)
        {
            return;
        }
        inputField.onValueChanged.RemoveListener(OnInputFieldValueChange);
        if (arg0 == "\n" && !inputField.multiLine)
        {
            inputField.OnSubmit(null);
        }
        inputField.SetTextWithoutNotify(inputField.text + arg0);
        inputField.text += arg0;
        inputField.MoveTextEnd(false);
        inputField.onValueChanged.AddListener(OnInputFieldValueChange);
    }

    private void OnBackspace()
    {
        if (!inputField.isFocused)
        {
            return;
        }
        if (inputField.text.Length > 0)
        {
            inputField.onValueChanged.RemoveListener(OnInputFieldValueChange);
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            inputField.MoveTextEnd(false);
            inputField.onValueChanged.AddListener(OnInputFieldValueChange);
        }
    }

    public void Submit()
    {
        throw new NotImplementedException();
    }

    public void AppendText(string s)
    {
        throw new NotImplementedException();
    }

    public void ApplyBackspace()
    {
        throw new NotImplementedException();
    }

    public void MoveTextEnd()
    {
        throw new NotImplementedException();
    }

    public Action<string> OnTextChanged { get; set; }
    public string Text { get; }
    public bool SubmitOnEnter { get; }
    public bool IsFocused { get; }
}
