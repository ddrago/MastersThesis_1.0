using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

#if UNITY_STANDALONE_WIN
    using UnityEngine.Windows.Speech;
#elif UNITY_EDITOR
using UnityEngine.Windows.Speech;
#endif

public class VoiceCommands : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
        private KeywordRecognizer keywordRecognizer;
#elif UNITY_EDITOR
        private KeywordRecognizer keywordRecognizer;
#endif


    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    Text PseudoConsole;
    string consolePreMessage = "Last input: ";

    public ExperimentManager experimentManager;
    public MirrorUIManager mirrorUIManager;

    public float conditionStartWaitingCountdownDuration = 10;
    public float waitingCountdownDuration = 15;
    private Dictionary<int, bool> timerCommandLog = new Dictionary<int, bool>();

    // Start is called before the first frame update
    void Start()
    {
        PseudoConsole = GameObject.Find("PseudoConsole").GetComponent<Text>();

        Debug.Log("Ready to receive voice commands!");

        actions.Add("music", Gotit);
        actions.Add("news", Gotit);
        //actions.Add("podcasts", Gotit);
        //actions.Add("sport", Gotit);
        actions.Add("weather", Gotit);
        actions.Add("terrain", Gotit);
        //actions.Add("performance", Gotit);
        //actions.Add("compare", Gotit);
        actions.Add("maps", Gotit);
        //actions.Add("navigation", Gotit);
        //actions.Add("contacts", Gotit);
        actions.Add("calls", Gotit);

#if UNITY_STANDALONE_WIN
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += recognizedPhrase;
        keywordRecognizer.Start();
#elif UNITY_EDITOR
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += recognizedPhrase;
        keywordRecognizer.Start();
#endif

        // Start the countdown for the first item
        timerCommandLog.Add(experimentManager.turnNumber, false);
        StartCoroutine(WaitAndPrint(experimentManager.turnNumber, conditionStartWaitingCountdownDuration));
    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_STANDALONE_WIN
    private void recognizedPhrase(PhraseRecognizedEventArgs phrase)
    {
        mirrorUIManager.RpcVoiceCommand(phrase.text);

        if (!this.gameObject.activeSelf)
        {
            Debug.Log("Voice commands not active!");
            return;
        }

        actions[phrase.text].Invoke();
        MirrorPhraseRecognizer(phrase.text, true);
    }
#elif UNITY_EDITOR
    private void recognizedPhrase(PhraseRecognizedEventArgs phrase)
    {
        mirrorUIManager.RpcVoiceCommand(phrase.text);

        if (!this.gameObject.activeSelf)
        {
            Debug.Log("Voice commands not active!");
            return;
        }

        actions[phrase.text].Invoke();
        MirrorPhraseRecognizer(phrase.text, true);
    }
#endif

    public void MirrorPhraseRecognizer(string phrase, bool isRegular)
    {
        timerCommandLog[experimentManager.turnNumber] = isRegular;
        PseudoConsole.text = consolePreMessage + phrase;
        experimentManager.SelectItemVoiceCondition(phrase);
        experimentManager.NextInstruction();

        // Register we are waiting for next instruction 
        timerCommandLog.Add(experimentManager.turnNumber, false);
        StartCoroutine(WaitAndPrint(experimentManager.turnNumber, waitingCountdownDuration));
    }

    IEnumerator WaitAndPrint(int turn, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        if (!timerCommandLog[turn])
        {
            MirrorPhraseRecognizer("Timeout", false);
        }

    }

    /*private void VoiceCommandCountdown()
    {
        if(!commandWasGiven)
            Debug.Log("TIMEOUT!");
    }*/

    private void Gotit()
    {
        Debug.Log("Got it!");
    }

    public void StopKeywordRecognizer()
    {
#if UNITY_STANDALONE_WIN
        if (keywordRecognizer.IsRunning)
            Debug.Log("Deactivating voice commands...");
            keywordRecognizer.Stop();
#elif UNITY_EDITOR
        if (keywordRecognizer.IsRunning)
            Debug.Log("Deactivating voice commands...");
            keywordRecognizer.Stop();
#endif
    }
}
