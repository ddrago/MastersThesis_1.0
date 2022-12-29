using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommands : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");

        actions.Add("forward", Forward);
        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("back", Back);
        actions.Add("music", Music);
        actions.Add("news", News);
        actions.Add("podcasts", Podcasts);
        actions.Add("sport", Sport);
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
        Debug.Log(phrase.text);
        actions[phrase.text].Invoke();
    }

    private void Gotit()
    {
        Debug.Log("---");
    }

    private void Forward()
    {
        transform.Translate(1, 0, 0);
    }

    private void Back()
    {
        transform.Translate(-1, 0, 0);
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void Music()
    {
        Debug.Log("Music");
    }

    private void News()
    {
        Debug.Log("News");
    }

    private void Podcasts()
    {
        Debug.Log("Podcasts");
    }

    private void Sport()
    {
        Debug.Log("Sport");
    }
}
