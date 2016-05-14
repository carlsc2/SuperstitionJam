using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class QuestBase : ScriptableObject {

    public QuestBase()
    {

    }

    public enum State { UKNOWN, STARTED, COMPLETED, TURNED_IN };

    public string Description;

    virtual public void CheckConditions(){
        try
        {
            throw new UnityException();
        }
        catch (UnityException e){
            Debug.LogError("CheckCondition is not defined!\n");
        }
    }

    virtual public void GiveReward(){
        //change player stat
        //change boss enemy
        //give item to player
        try
        {
            throw new UnityException();
        }
        catch (UnityException e)
        {
            Debug.LogError("GiveReward is not defined!\n");
        }
    }

}
