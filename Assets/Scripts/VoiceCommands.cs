using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommands : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    Text PseudoConsole;


    // Start is called before the first frame update
    void Start()
    {
        PseudoConsole = GameObject.Find("PseudoConsole").GetComponent<Text>();

        Debug.Log("Hello");

        actions.Add("music", Gotit);
        actions.Add("news", Gotit);
        actions.Add("podcasts", Gotit);
        actions.Add("sport", Gotit);
        actions.Add("weather", Gotit);
        actions.Add("terrains", Gotit);
        actions.Add("performance", Gotit);
        actions.Add("compare", Gotit);
        actions.Add("maps", Gotit);
        actions.Add("navigation", Gotit);
        actions.Add("contacts", Gotit);
        actions.Add("calls", Gotit);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += recognizedPhrase;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void recognizedPhrase(PhraseRecognizedEventArgs phrase)
    {
        if (!this.gameObject.activeSelf)
        {
            Debug.Log("Voice commands not active!");
            return;
        } 
        Debug.Log(phrase.text);
        actions[phrase.text].Invoke();
        PseudoConsole.text = phrase.text;
    }

    private void Gotit()
    {
        Debug.Log("Got it!");
    }
}
