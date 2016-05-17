using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class MonologueControl_New : MonoBehaviour, Interactable
{
    public string NPC_ID;

    public string[] questIDs;
    private List<QuestBase> quests;

    public string[] idle_dialogue;
    private int idix = 0;

    public string[] default_chat_dialogue;

    private string[] chat_dialogue;
    private int cdix = 0;

    private RectTransform chatbox;
    private RectTransform canvas;
    private Text txt;

    private float chat_height_offset;

    private IEnumerator chatroutine;

    public bool is_speaking = false;
    public bool is_chatting = false;
    public bool is_idle = true;

    public GameObject chatboxPrefab;

    void Awake()
    {
        //create a chatbox instance

        canvas = GameObject.FindGameObjectWithTag("Canvas").transform as RectTransform;

        chatbox = (Instantiate(chatboxPrefab) as GameObject).transform as RectTransform;
        chatbox.SetParent(canvas, false);
        txt = chatbox.GetComponentInChildren<Text>();

        chat_height_offset = GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    void Start()
    {
        foreach (string qID in questIDs)
        {
            quests.Add(QuestMaster.Instance.Quests[qID]);
        }
    }

    virtual public void Interact(Transform t)
    {
        chat_dialogue = default_chat_dialogue;

        foreach(QuestBase quest in quests){
            string quest_dialogue = quest.GetDialogue(NPC_ID);
            if (quest_dialogue != null && quest_dialogue != "")
            {
                char[] delim = {'\n'};
                chat_dialogue = quest_dialogue.Split(delim);
                break;
            }
        }

        is_chatting = true;
        is_idle = false;
        is_speaking = false;
    }

    void Update()
    {
        if (!is_speaking)
        {
            if (is_idle)
            {
                if (idix < idle_dialogue.Length)
                {
                    speak_words(idle_dialogue[idix]);
                    idix = (idix + 1) % idle_dialogue.Length;
                }

            }
            else if (is_chatting)
            {
                if (cdix < chat_dialogue.Length)
                {
                    speak_words(chat_dialogue[cdix]);
                    cdix = cdix + 1;
                }
                else
                {
                    is_chatting = false;
                    idix = 0;
                    cdix = 0;
                    is_idle = true;
                }

            }

        }

    }

    public void speak_words(string words)
    {
        if (chatroutine != null)
        {
            StopCoroutine(chatroutine);
        }
        chatroutine = _speak_words(words);
        StartCoroutine(chatroutine);
    }

    IEnumerator _speak_words(string words)
    {
        //make chat appear above head and fade out after a time

        txt.text = words;

        float duration = 1.5f + words.Length / 7f;

        float start_time = Time.time;

        //make chat box visible
        chatbox.gameObject.SetActive(true);
        is_speaking = true;

        while (Time.time < duration + start_time)
        {

            //update position of chat box

            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * chat_height_offset);

            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

            chatbox.anchoredPosition = WorldObject_ScreenPosition;

            yield return null;
        }

        //hide chat box
        chatbox.gameObject.SetActive(false);
        is_speaking = false;



    }


}
