using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class ThreeWordsTask : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public TextMeshProUGUI coworkerName;
    public TextMeshProUGUI submissionText;
    public Image coworkerAvatar;
    private CoworkerConfiguration coworker;
    private List<string> taskTypes;
    private string message;
    private string taskType;
    private List<string> correctWords = new List<string>();
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        taskTypes = new List<string>();
        taskTypes.Add("animals");
        taskTypes.Add("colors");
        taskTypes.Add("continents");
        taskTypes.Add("planets");
        taskTypes.Add("nuts");
        taskTypes.Add("berries");
        taskType = taskTypes[Random.Range(0, taskTypes.Count)];
        PopulateCorrectWords();
        PopulateChatMessage();
        chatText.text = message;
        coworker = GetComponent<Window>().GetCoworkerConfiguration();
        string name = coworker.coworkerName;
        coworkerAvatar.sprite = coworker.coworkerIcon;
        coworkerName.text = name;
        inputField.onSubmit.AddListener(HandleSubmit);
        inputField.onDeselect.AddListener(HandleDeselect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleSubmit(string text)
    {
        SubmitWords();
    }

    private void HandleDeselect(string text)
    {

    }

    public void SubmitWords()
    {
        // Log the input text and trimmed, lowercased result
        string processedInput = submissionText.text.Replace("\u200B", "").Trim().ToLower();

        // Split the input into words and log each word
        string[] userWords = processedInput.Split(new char[] { ' ', ',', '.', '?' }, StringSplitOptions.RemoveEmptyEntries);

        // Convert user input words into a HashSet to avoid duplicates
        HashSet<string> userWordsSet = new HashSet<string>(userWords);
        
        // Initialize and log correct words
        HashSet<string> correctWordsSet = new HashSet<string>(correctWords.ConvertAll(word => word.ToLower()));

        // Check for correct words and count matches
        int correctCount = 0;
        foreach (string word in userWordsSet)
        {
            if (correctWordsSet.Contains(word))
            {
                correctCount++;
            }
        }

        // Output result based on the number of correct matches
        if (correctCount >= 3)
        {
            GetComponent<Window>().CompleteTask();
        }
        else
        {
            GetComponent<Window>().CloseWindow(false);
        }
    }

    private void OnDestroy()
    {
        // Always good practice to unsubscribe from events
        inputField.onEndEdit.RemoveListener(HandleSubmit);
        inputField.onDeselect.RemoveListener(HandleDeselect);
    }


    private void PopulateChatMessage() {
        string msg = "Hello do you have a sec? I have a client that needs business development and they're focusing on";
        switch(taskType) {
            case "animals":
                msg += " <b>animals that start with the letter S</b>";
                break;
            case "colors":
                msg += " <b>colors</b>";
                break;
            case "continents":
                msg += " <b>continents</b>";
                break;
            case "planets":
                msg += " <b>planets</b>";
                break;
            case "nuts":
                msg += " <b>nuts</b>";
                break;
            case "berries":
                msg += " <b>berries</b>";
                break;
            default:
                break;
        }
        msg += ". Please give me <b>three</b>.";
        message = msg;
    }
    private void PopulateCorrectWords() {
        switch(taskType) {
            case "animals":
                correctWords.Add("sable");
                correctWords.Add("saiga");
                correctWords.Add("salamander");
                correctWords.Add("salmon");
                correctWords.Add("sand dollar");
                correctWords.Add("sandpiper");
                correctWords.Add("sardine");
                correctWords.Add("sawfish");
                correctWords.Add("scallop");
                correctWords.Add("scarab");
                correctWords.Add("scorpion");
                correctWords.Add("sea anemone");
                correctWords.Add("sea cucumber");
                correctWords.Add("sea lion");
                correctWords.Add("sea urchin");
                correctWords.Add("seahorse");
                correctWords.Add("seal");
                correctWords.Add("serval");
                correctWords.Add("shark");
                correctWords.Add("shearwater");
                correctWords.Add("sheep");
                correctWords.Add("shelduck");
                correctWords.Add("shiner");
                correctWords.Add("shoebill");
                correctWords.Add("shrew");
                correctWords.Add("shrimp");
                correctWords.Add("siamese fighting fish");
                correctWords.Add("siberian tiger");
                correctWords.Add("sidewinder");
                correctWords.Add("silkworm");
                correctWords.Add("silverfish");
                correctWords.Add("skimmer");
                correctWords.Add("skink");
                correctWords.Add("skipper");
                correctWords.Add("skua");
                correctWords.Add("skunk");
                correctWords.Add("sloth");
                correctWords.Add("slowworm");
                correctWords.Add("slug");
                correctWords.Add("smelt");
                correctWords.Add("snail");
                correctWords.Add("snake");
                correctWords.Add("snipe");
                correctWords.Add("snow leopard");
                correctWords.Add("snowshoe hare");
                correctWords.Add("solenodon");
                correctWords.Add("solitaire");
                correctWords.Add("somali wild ass");
                correctWords.Add("sparrow");
                correctWords.Add("sphinx moth");
                correctWords.Add("spider");
                correctWords.Add("spider monkey");
                correctWords.Add("spiny dogfish");
                correctWords.Add("sponge");
                correctWords.Add("spoonbill");
                correctWords.Add("spotted dolphin");
                correctWords.Add("squid");
                correctWords.Add("squirrel");
                correctWords.Add("squirrel monkey");
                correctWords.Add("starfish");
                correctWords.Add("starling");
                correctWords.Add("stingray");
                correctWords.Add("stoat");
                correctWords.Add("stork");
                correctWords.Add("storm petrel");
                correctWords.Add("sturgeon");
                correctWords.Add("sugar glider");
                correctWords.Add("sun bear");
                correctWords.Add("sunfish");
                correctWords.Add("suricate");
                correctWords.Add("swallow");
                correctWords.Add("swan");
                correctWords.Add("swift");
                correctWords.Add("swordfish");
                correctWords.Add("swordtail");
                break;

            case "colors":
                correctWords.Add("red");
                correctWords.Add("blue");
                correctWords.Add("green");
                correctWords.Add("violet");
                correctWords.Add("magenta");
                correctWords.Add("yellow");
                correctWords.Add("pink");
                correctWords.Add("purple");
                correctWords.Add("orange");
                correctWords.Add("brown");
                correctWords.Add("black");
                correctWords.Add("white");  
                correctWords.Add("cyan");
                correctWords.Add("gray");
                correctWords.Add("grey");
                correctWords.Add("silver");
                correctWords.Add("gold");
                correctWords.Add("maroon");
                correctWords.Add("navy");
                correctWords.Add("turquoise");
                correctWords.Add("teal");
                correctWords.Add("beige");
                correctWords.Add("indigo");
                correctWords.Add("blue");

                break;
            case "continents":
                correctWords.Add("africa");
                correctWords.Add("asia");
                correctWords.Add("europe");
                correctWords.Add("america");
                correctWords.Add("australia");
                correctWords.Add("north america");
                correctWords.Add("south america");
                correctWords.Add("antarctica");
                correctWords.Add("oceania");
                correctWords.Add("north");
                correctWords.Add("south");
                break;
            case "planets":
                correctWords.Add("earth");
                correctWords.Add("mars");
                correctWords.Add("jupiter");
                correctWords.Add("saturn");
                correctWords.Add("uranus");
                correctWords.Add("neptune");
                correctWords.Add("venus");
                correctWords.Add("mercury");
                break;

            case "nuts":
                correctWords.Add("peanut");
                correctWords.Add("almond");
                correctWords.Add("cashew");
                correctWords.Add("walnut");
                correctWords.Add("hazelnut");
                correctWords.Add("pistachio");
                correctWords.Add("macadamia");
                correctWords.Add("pine nut");
                correctWords.Add("pecan");
                correctWords.Add("walnut");
                correctWords.Add("almond");
                correctWords.Add("cashew");
                correctWords.Add("hazelnut");
                correctWords.Add("pistachio");
                correctWords.Add("macadamia");
                correctWords.Add("pine");
                correctWords.Add("pecan");
                correctWords.Add("para");
                correctWords.Add("brazil nut");
                correctWords.Add("butternut");
                correctWords.Add("acorn");
                correctWords.Add("brazil");
                correctWords.Add("nut");
                break;

            case "berries":
                correctWords.Add("strawberry");  
                correctWords.Add("blueberry");  
                correctWords.Add("raspberry");  
                correctWords.Add("blackberry");  
                correctWords.Add("blackcurrant");  
                correctWords.Add("gooseberry");  
                correctWords.Add("cranberry");  
                correctWords.Add("banana");  
                correctWords.Add("cherry");  
                correctWords.Add("aronia");   
                correctWords.Add("goji");
                correctWords.Add("acai");
                correctWords.Add("mulberry");
                correctWords.Add("elderberry");
                correctWords.Add("boysenberry");
                correctWords.Add("lingonberry");
                correctWords.Add("cloudberry");
                correctWords.Add("loganberry");
                correctWords.Add("huckleberry");
                correctWords.Add("bilberry");
                correctWords.Add("acerola");
                break;
            default:
                break;  
        }
    }
}
