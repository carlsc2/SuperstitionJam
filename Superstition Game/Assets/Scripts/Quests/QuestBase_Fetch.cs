using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestBase_Fetch : QuestBase
{
    public GameObject Player;

    public ItemBase RewardItem;
   // public ItemBase QuestItem;

    void Start()
    {
        Inventory = Player.GetComponent<InventoryController>();
    }

    override public void GiveReward()
    {
        print("GIVE ME THE THING");
        Inventory.AddItemToInventory(RewardItem);
        Inventory.RemoveFirstItemOfId(QuestItem.id);
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
