using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

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
    public ExperimentManager experimentManager;
    public MirrorUIManager mirrorUIManager;


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

    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_STANDALONE_WIN
    private void recognizedPhrase(PhraseRecognizedEventArgs phrase)
    {
        mirrorUIManager.RpcVoiceCommand();

        if (!this.gameObject.activeSelf)
        {
            Debug.Log("Voice commands not active!");
            return;
        }

        actions[phrase.text].Invoke();
        MirrorPhraseRecognizer(phrase.text);
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
        MirrorPhraseRecognizer(phrase.text);
    }
#endif

    public void MirrorPhraseRecognizer(string phrase)
    {
        PseudoConsole.text = phrase;
        experimentManager.SelectItemVoiceCondition(phrase);
        experimentManager.NextInstruction();
    }

    private void Gotit()
    {
        Debug.Log("Got it!");
    }
}
