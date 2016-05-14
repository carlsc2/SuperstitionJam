using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestBase_Action : QuestBase
{
    public GameObject AffectedCharacter;

    [HideInInspector]
    public CharacterStats stats;

    public List<string> statNames;

    void Start() {

        stats = AffectedCharacter.GetComponent<CharacterStats>();

    }
   
    public float multiplier;

    public int NumberOfTimes;

    override public void GiveReward()
    {
        if (currentState == QuestBase.State.COMPLETED)
        {
            foreach (string stat in statNames)
            {
                if (stat.ToLower() == "health")
                {
                    stats.health *= multiplier;
                }
                else if (stat.ToLower().Replace(" ", string.Empty) == "maxhealth")
                {
                    stats.maxHealth = Mathf.Floor( stats.maxHealth *multiplier);
                }
                else if (stat.ToLower() == "strength")
                {
                    stats.strength *= multiplier;
                }
                else if (stat.ToLower() == "speed")
                {
                    stats.speed *= multiplier;
                }
                else if (stat.ToLower().Replace(" ", string.Empty) == "attacktime")
                {
                    stats.attackTime *= multiplier;
                }
                else
                {
                    Debug.Log(stat + " is not a property of the character!\n");
                }

            }

            currentState = QuestBase.State.TURNED_IN;
        }
    }
	
}


#if UNITY_EDITOR
[CustomEditor(typeof(QuestBase_Action))]
public class QuestBase_Action_Editor : Editor
{

    QuestBase_Action selfScript;
    void OnEnable()
    {
        selfScript = (QuestBase_Action)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("test the fuckign funcktion"))
        {
            selfScript.GiveReward();
        }
    }
}
#endif
