using UnityEngine;
using System.Collections.Generic;

public class SpriteRigBoneHandler : MonoBehaviour {

    public List<SpriteRenderer> cosmeticSpriteRenderers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyAllCosmeticSprites() {
        for (int i = 0; i < cosmeticSpriteRenderers.Count; ++i) {


            
            if (Application.isPlaying) {
                Destroy(cosmeticSpriteRenderers[i].gameObject);
            }
            else {
                DestroyImmediate(cosmeticSpriteRenderers[i].gameObject);
            }

        }
        cosmeticSpriteRenderers.Clear();
    }

    public SpriteRenderer AddCosmeticSprite(Sprite sprite, Vector2 offset, string name) {
        //create the cosmetic object
        GameObject newSprite = new GameObject();
        SpriteRenderer newRen = newSprite.AddComponent<SpriteRenderer>();
        cosmeticSpriteRenderers.Add(newRen);

        newRen.transform.parent = gameObject.transform;
        newRen.transform.SetAsFirstSibling();

        if (sprite != null) {
            newRen.sprite = sprite;
        }
        newRen.transform.position += (Vector3) offset;
        newRen.gameObject.name = name;

        return newRen;
    }
}
