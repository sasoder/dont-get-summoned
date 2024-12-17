using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random=UnityEngine.Random;


public class CaptchaTask : MonoBehaviour
{
    private string captcha;
    public TextMeshProUGUI captchaText;
    public TextMeshProUGUI captchaSubmissionText;
    public TMP_InputField inputField;
    public List<TMP_FontAsset> fonts;
    // Start is called before the first frame update
    void Start()
    {
        captchaText.text = GenerateCaptcha();
        captchaText.font = fonts[Random.Range(0, fonts.Count)];
        inputField.onSubmit.AddListener(HandleSubmit);
        inputField.onDeselect.AddListener(HandleDeselect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleSubmit(string text)
    {
        OnSubmit();
    }

    private void HandleDeselect(string text)
    {

    }

    private void OnEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnSubmit();
        }
        else
        {
            // Prevent the event from firing when losing focus by other means
            inputField.text = inputField.text;  // Reset text to prevent event misfire
        }
    }

    private void OnDestroy()
    {
        // Always good practice to unsubscribe from events
        inputField.onEndEdit.RemoveListener(HandleSubmit);
        inputField.onDeselect.RemoveListener(HandleDeselect);
    }

    public void OnSubmit()
    {

            // Clean input text by removing zero-width space (Unicode 200B) and trimming
            string cleanCaptcha = captchaText.text.Replace("\u200B", "").Trim();
            string cleanSubmission = captchaSubmissionText.text.Replace("\u200B", "").ToLower().Trim();

            // Compare cleaned and trimmed strings
            if (cleanCaptcha == cleanSubmission)
            {
                GetComponent<Window>().CompleteTask();
            }
            else
            {
                GetComponent<Window>().CloseWindow(true);
            }
       
    }

    //Generates a random 7 letter/number string
    public string GenerateCaptcha()
    {
        int amtLetters = Random.Range(4, 7);
        string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        string result = "";
        for (int i = 0; i < amtLetters; i++)
        {
            int index = Random.Range(0, chars.Length);
            result += chars[index];
        }
        return result.ToLower();
    }
}
