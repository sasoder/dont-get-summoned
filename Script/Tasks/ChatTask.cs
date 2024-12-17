using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChatTask : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public TextMeshProUGUI coworkerName;
    public Image coworkerAvatar;
    public Button[] answerButtons;  // Reference to the buttons
    public List<string> chatMessages;
    public List<string> goodChatAnswers;
    public List<string> badChatAnswers;
    private CoworkerConfiguration coworker;
    public Color defaultColor;
    public Color SelectedColor;
    private bool rightAnswer;

    private int correctAnswerIndex;  // To keep track of which answer is correct

    void Start()
    {
        rightAnswer = false;
        coworker = GetComponent<Window>().GetCoworkerConfiguration();
        string name = coworker.coworkerName;
        coworkerAvatar.sprite = coworker.coworkerIcon;
        coworkerName.text = name;
        if(name.Equals("Office Psycho")) {
            PopulateChatMessagesPsycho();
            PopulateGoodChatAnswersPsycho();
            PopulateBadChatAnswersPsycho();
        } else {
            PopulateChatMessages();
            PopulateGoodChatAnswers();
            PopulateBadChatAnswers();
        }
        chatText.text = GetRandomChatMessage();

        SetupAnswers();
    }

    private void SetupAnswers()
    {
        correctAnswerIndex = Random.Range(0, answerButtons.Length);

        // Shuffle the list of bad chat answers to ensure randomness without repetition
        List<string> shuffledBadChatAnswers = new List<string>(badChatAnswers);
        int badAnswerCount = 0; // This will keep track of how many bad answers have been used

        // Fisher-Yates shuffle algorithm to shuffle the bad chat answers
        for (int i = shuffledBadChatAnswers.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            string temp = shuffledBadChatAnswers[i];
            shuffledBadChatAnswers[i] = shuffledBadChatAnswers[swapIndex];
            shuffledBadChatAnswers[swapIndex] = temp;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;  // Create a local copy of the loop variable
            TextMeshProUGUI buttonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            answerButtons[index].onClick.RemoveAllListeners();  // Clear previous listeners

            if (index == correctAnswerIndex)
            {
                buttonText.text = goodChatAnswers[Random.Range(0, goodChatAnswers.Count)];
                answerButtons[index].onClick.AddListener(() => SelectChatAnswer(true, index));
            }
            else
            {
                buttonText.text = shuffledBadChatAnswers[badAnswerCount++];
                answerButtons[index].onClick.AddListener(() => SelectChatAnswer(false, index));
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    // Handle button click
    private string lastClickedButtonText;  // Class variable to store the last clicked button's text

    public void SelectChatAnswer(bool isCorrect, int buttonIndex)
    {
        if (buttonIndex >= answerButtons.Length || buttonIndex < 0)
        {
            return;  // Ensure the button index is valid
        }

        // Update the text of the last clicked button
        lastClickedButtonText = answerButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text;
        // Update visual state of all buttons
        foreach (Button btn in answerButtons)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = defaultColor;  // Reset to default
            btn.colors = cb;
        }

        rightAnswer = isCorrect;

        // Highlight the selected button if needed
        // ColorBlock colorBlock = answerButtons[buttonIndex].colors;
        // colorBlock.normalColor = isCorrect ? Color.green : Color.red;
        // answerButtons[buttonIndex].colors = colorBlock;
    }


    private string GetRandomChatMessage() {
        return chatMessages[Random.Range(0, chatMessages.Count)];
    }

    public void SendMessage() {
        if (rightAnswer)
        {
            GetComponent<Window>().CompleteTask();
        }
        else
        {
            if(coworker.coworkerName.Equals("Office Psycho")) {
                if(lastClickedButtonText.Equals("Ask Jessica.")) {
                    GetComponent<Window>().CloseWindowPsycho(false);
                } else {
                    GetComponent<Window>().CloseWindowPsycho(true);
                }
            } 
            GetComponent<Window>().CloseWindow(false);
        }
    }

    private void PopulateChatMessages() {
        chatMessages.Add("Hey buddy, how about a beer after work today? Brewskies!!");
        chatMessages.Add("Hear me out: you, me and beers after work");
        chatMessages.Add("My main coworker!! How 'bout a cold one after the client meeting todayyy.");
        chatMessages.Add("Yoooooo let's go for some drinks today after work. Maybe bring Jessica also.");
        chatMessages.Add("Yo buddy, let's go locos tonite!! How bout some beers at the new place?");
        chatMessages.Add("Heyyyy friend, how bout some cold ones after work. Crack open a cold one haha am I right?");
        chatMessages.Add("I won't take no for an answer today! A beer after work, you're in, right?");
        chatMessages.Add("How about some of that aged German beer haha. I know you love the fancy stuff! Haha. First round on me after work");
        chatMessages.Add("Heyyoo, how bout some ice cold beer after work? You, me, beers and pizza??");
        chatMessages.Add("Have you seen that meme that's like 'ok just one beer' That's gonna be me and you tonight!");
       
    }

    private void PopulateGoodChatAnswers() {
        goodChatAnswers.Add("Hey, thanks for the invite but I'm gonna go to the gym today after work. Another time!");
        goodChatAnswers.Add("Sorry man, I've got a huge project deadline tomorrow and I really need to focus on that tonight. Another time!");
        goodChatAnswers.Add("That sounds fun, but I promised a friend I'd help them move this evening. Another time!");
        goodChatAnswers.Add("Thanks for thinking of me! Unfortunately, I'm feeling a bit tired today. Another time!");
        goodChatAnswers.Add("I've already made dinner plans... Another time!");
        goodChatAnswers.Add("Ah, I have to pick up my cousin today from the airport. Another time!");
        goodChatAnswers.Add("Sorry, I've got a family thing. Another time!");
        goodChatAnswers.Add("I really appreciate the invite, but I'm laying low tonight. Another time!");
        goodChatAnswers.Add("Sounds great, but I gotta walk my neighbor's dog. Another time!");
        goodChatAnswers.Add("Tempting, but I gotta stay late and help Jessica. Another time!");
    }

    private void PopulateBadChatAnswers() {
        badChatAnswers.Add("Sorry, I promised my recliner I'd spend the evening sitting in it — prior commitments, you know? Another time!");
        badChatAnswers.Add("No can do, I’m watching paint dry tonight. It's a big project. Another time!");
        badChatAnswers.Add("I’d rather not, I have to wash my hair for the next seven hours. Another time!");
        badChatAnswers.Add("Nah... Another time!");
        badChatAnswers.Add("I have to decline, there’s a documentary on shoelaces airing tonight. It’s a tie-in event! Another time!");
        badChatAnswers.Add("Not tonight, I promised myself I'd stare at the wall for a few hours to unwind. Another time!");
        badChatAnswers.Add("I'm trying to see if I can make it through the night without checking my fridge every 15 minutes. It’s a new record attempt. Another time!");
        badChatAnswers.Add("Tonight's not good. I’m testing how long I can listen to hold music before completely losing it. Another time!");
        badChatAnswers.Add("I'm trying to see how long I can go without human interaction. Another time!");
        badChatAnswers.Add("I have a meeting with my imaginary friend, we haven't caught up in ages. Another time!");
        badChatAnswers.Add("That’s going to be a no, I’m practicing blinking in rhythm tonight. Another time!");
        badChatAnswers.Add("I’d join, but I’ve planned to count the tiles on my kitchen floor. Another time!");

    }

    private void PopulateGoodChatAnswersPsycho() {
        goodChatAnswers.Add("No sorry I can't.");
    }

    private void PopulateBadChatAnswersPsycho() {
        badChatAnswers.Add("Yeah sure sounds fun!");
        badChatAnswers.Add("Ask Jessica.");
    }

    private void PopulateChatMessagesPsycho() {
        chatMessages.Add("Hey you wanna stay late and work tonight? I have a big project so we probably need to stay past 7. That's also when the guard leaves so we'll have the place to ourselves :-)");
        chatMessages.Add("Hey you wanna come out to my cabin this weekend? It's out in the forest, secluded and nice.  No cell service, but I have a landline :-)");
        chatMessages.Add("Hey you wanna try out this new restaurant after work? It's really good but it's in an industrial area haha. Weird haha. At 8? :-).");
    }

}
