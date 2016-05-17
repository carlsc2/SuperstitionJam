using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestBase_Fetch : QuestBase
{
  
    public string RewardItem;

    void Start()
    {
        if (Player) {
            Inventory = Player.GetComponent<InventoryController>();
        }
        else {
            Inventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryController>();
        }
        
    }

    override public void GiveReward()
    {
        print("GIVE ME THE THING");
        GameObject reward = Resources.Load(RewardItem) as GameObject;
        reward = (GameObject) GameObject.Instantiate(reward, Vector3.zero, Quaternion.identity);
        reward.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Inventory.AddItemToInventory(reward.GetComponent<ItemBase>());
        Inventory.RemoveFirstItemOfId(QuestItem);
        currentState = QuestBase.State.TURNED_IN;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(QuestBase_Fetch))]
public class QuestBase_Fetch_Editor : Editor
{

    QuestBase_Fetch selfScript;
    void OnEnable()
    {
        selfScript = (QuestBase_Fetch)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("test the fuckign fucktion"))
        {
            selfScript.GiveReward();
        }
    }
}
#endif
