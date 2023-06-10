using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Facebook.WitAi.TTS.Samples;

/* Important script in this project. The script is attached to Game Menu-> canvas -> volume-> slider gameobject. I started writing this script to test retrieving the volume input from cloud server, but then kept adding everything to thte same script. 
 * Was too lazy to create another script and organize everything proparly.
   This script retrieves the json file stored on cloud server. Then simply decoded that json object and uses that information to set different variables in game. 
   IMPORTANT: There is a delay of 10 frames before sending teh web request. This was important to give our server time to respond to our web request. Otherwise update method keeps sending requests every frame and it becomes http flooding attack on our server. 
              This actually crashed my server. Took me long time to figure out why it crashed. Ubuntu or apache probably has some default firewall that prevents this kind of behaviour. 
              The delay could be eliminated if we use some other type of server, something that is used in online gaming. Currently we are using apache http server on cloud, which may not be the best choice this type of data transmission, but its easy to implement. 
 */


public class GetWebData : MonoBehaviour
{
    
    public Slider VolumeSlider;
    public GameObject telephoneAudio;
    //public GameObject telephone;
    public GameObject CharacterScript;
    CommandController commandData = null;
    //public CustomTTS _TTS;
    //public GameObject demo;
    public GameObject flies;
    public InputField inputFlagForCharacter;
    public InputField inputMessage;
    //private bool TTSComplete;
    public GameObject CustomeTelephone;
    public GameObject teleHeadset;
    CollissionGameObject telephonecollider;
    public CustomTTS ct, ct1;
    int scriptFlag;
    public turnToPlayer turnAround;
    public GameObject character1;

    public string newMessage, oldMessage;

    //CustomeTTS

    public GameObject scriptCanvas;


    float time;
    float Delay;

    private void Start()
    {
        scriptFlag = 1;
        //TTSComplete = false;
        telephonecollider = teleHeadset.GetComponent<CollissionGameObject>();
        ct = CustomeTelephone.GetComponent<CustomTTS>();
        turnAround = character1.GetComponent<turnToPlayer>();
        ct1 = character1.GetComponent<CustomTTS>();
        time = 0f;
        Delay = 10f;
        //linkToScriptB = GameObject.Find("Standing Idle (1)").GetComponent(TT SfromWebsite);
    }

    void Update()
    {
        time = time + 1f * Time.deltaTime;

        if(time >= Delay)
        {
            time = 0f;
            StartCoroutine(GetData());
        }

    }

    /* for sending teh web request and receiving data from json file */
    IEnumerator GetData()
    {
        string url = "http://188.166.96.85/newControl.json";
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                print(json);
                commandData = JsonUtility.FromJson<CommandController>(json);
                print(commandData);
            }
        }

        CommandSelectEvent(0);
    }

    /*decoded the received data and uses its information to set some lags for some activities. Code is not very difficult to understand. */

    public void CommandSelectEvent(int index)
    {
        var stats = commandData;
        VolumeSlider.value = stats.Volume;

        

        /*if (stats.Message == 1)
        {
            CharacterScript.SetActive(true);
            Debug.Log("Executing message bolck");
        }
        else
        {
            CharacterScript.SetActive(false);
            Debug.Log("Not Executing message bolck");
        }*/

        
        /*Initiate the telehpone tasks by starting teh ringing audio and displaying the canvas where player can read teh script for the task.*/

        //TELEPHONE TASK
        if (stats.TelephoneFlag == 1)
        {
            Debug.Log("flag set to 1");
            Debug.Log(telephonecollider.alreadyPickedUp);
            if (telephonecollider.alreadyPickedUp == false)
            {
                Debug.Log("picked up is false");
                telephoneAudio.SetActive(true);
                scriptCanvas.SetActive(true);
            }
        }

        /* Initiate the swatter task by spawning the flies on window. */

        //SWATTER TASK
        if (stats.SwatterFlag == 1)
        {
            flies.SetActive(true);

        }
        else{
            flies.SetActive(false);
        }

        //Character Script

        /* Hardcoded all of the sentences that are used for telephone task to eliminate teh time required for typing it. Sentences are referenced as 1, 2,3,4. 
         CharacterScript variable should have been named telephone script. 
         */

        if (stats.CharacterScript == 1 && scriptFlag == 1)
        {
            //Character Script

            Debug.Log(stats.CharacterScript);
            inputFlagForCharacter.text = "Hey this is Rebecca here. I wanted to sell my house. I would like to make an appointment with one of your real estate agent to do that.";
            ct.SayPhrase();
            scriptFlag++;
            //TTSComplete = true;
        }
        else if (stats.CharacterScript == 2 && scriptFlag == 2)
        {
            Debug.Log(stats.CharacterScript);
            inputFlagForCharacter.text = "Tuesday works!. Could you please give me the address of your office?";
            ct.SayPhrase();
            scriptFlag++;
        }
        else if (stats.CharacterScript == 3 && scriptFlag == 3) {
            Debug.Log(stats.CharacterScript);
            inputFlagForCharacter.text = "Sounds good, I will see you on tuesday then..";
            ct.SayPhrase();
            scriptFlag++;
        }
        else if (stats.CharacterScript == 4 && scriptFlag == 4)
        {
            Debug.Log(stats.CharacterScript);
            inputFlagForCharacter.text = "Thanks!! Have a Greate day! Bye";
            ct.SayPhrase();
            scriptFlag++;
        }
        else
        {
            Debug.Log("Message is set to zero");
        }

        /* This is where it takes teh text we type on the web interface and character in project says it. It checks if there is any change in the text, to make sure that we are not repeating the sentence every 10 frames.  */
        newMessage = stats.Message;
        if (newMessage != null && newMessage != oldMessage)
        {
            Debug.Log(stats.Message);
            inputMessage.text = newMessage;
            turnAround.enabled =true;
            ct1.SayPhrase();
            oldMessage = newMessage;
        }



            /*if (newMessage == oldMessage)
            {
                inputTextForCharacter.text = null;
            }

            else
            {
                Debug.Log(newMessage);
                oldMessage = newMessage;
                inputTextForCharacter.text = newMessage;
            }

        }
        else
        {
            newMessage = "Code above is fucked up";
            Debug.Log(newMessage);
            inputTextForCharacter.text = newMessage;
            if (newMessage == oldMessage)
            {
                inputTextForCharacter.text = null;
            }
            else
            {
                Debug.Log(newMessage);
                oldMessage = newMessage;
                inputTextForCharacter.text = newMessage;
            }*/

        }
}