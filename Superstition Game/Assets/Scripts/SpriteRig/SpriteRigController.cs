using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteRigController : MonoBehaviour {

    //public SortingLayer rigSortingLayer;

    [System.Serializable]
    public class SpriteBoneBinding {

        public Transform spriteBone;

        public SpriteRenderer spriteRen;
        public Sprite sprite;


    }



    void Awake() {
        
    }

    public List<SpriteBoneBinding> spriteBones;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    public void SetSortingLayer(SortingLayer layer) {

        foreach (SpriteBoneBinding binding in spriteBones) {
            binding.spriteRen.sortingLayerName = layer.name;
        }

    }
    */

}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(SpriteRigController))]
public class SpriteRigController_Editor : Editor {

    SpriteRigController selfScript;

    void OnEnable() {
        selfScript = (SpriteRigController)target;
    }
    

}

/*
[CustomPropertyDrawer(typeof(SpriteRigController.SpriteBoneBinding))]
public class SpriteBoneBinding_PropertyDrawer : PropertyDrawer {

}
*/

#endif
