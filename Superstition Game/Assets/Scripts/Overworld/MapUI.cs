using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(RawImage))]
public class MapUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	public Color GrassColor, ForestColor, MountainColor, TundraColor, DesertColor, IceColor, WaterColor, DeepWaterColor;

	public enum areaType { GRASSLAND, FOREST, MOUNTAIN, TUNDRA, DESERT, ICE, WATER, DEEPWATER, NONE }

	private float maxPanDist;
	public float panSpeed = 0.5f;
	private Transform cam;
	public Transform token;
	private Vector2 panposition; //local pan position
	private float zpos;
	private Vector2 mapSize;

	private Texture2D mapTex;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(token.position, maxPanDist);
	}

	// Use this for initialization
	void Start() {
		cam = Camera.main.transform;
		panposition = cam.localPosition;
		zpos = cam.localPosition.z;
		RawImage mapImage = GetComponent<RawImage>();
		mapTex = (Texture2D)mapImage.texture;
		if(mapTex.format != TextureFormat.RGBA32) {
			Debug.LogWarning("Warning: map texture must be RGBA32 format for color comparisons to work properly");
		} 
		mapSize = mapImage.rectTransform.sizeDelta;

		//get max pan distance in UI pixels
		maxPanDist = Mathf.Min(Screen.width,Screen.height);


	}


	// Update is called once per frame
	void Update() {
		//assumes camera child of token
		/*Vector3 newpos = Vector2.Lerp(cam.localPosition, panposition, Time.deltaTime * panSpeed);
		newpos = Vector2.ClampMagnitude(newpos, maxPanDist);
		newpos.z = zpos;
		cam.localPosition = newpos;*/

		if (Input.GetMouseButton(0)) {
			print("Area: " + getAreaType(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
		}
	}


	public void OnBeginDrag(PointerEventData eventData) {
		panposition = cam.localPosition;
	}


	public void OnDrag(PointerEventData eventData) {
		Vector2 moveDelta = new Vector2(eventData.delta.x / Screen.width, eventData.delta.y / Screen.height) * maxPanDist;
#if UNITY_ANDROID
		//reverse drag direction for touch
		panposition -=  moveDelta;
#else
		panposition += moveDelta;
#endif
	}


	public void OnEndDrag(PointerEventData eventData) {
		panposition = cam.localPosition;
	}

	public areaType getAreaType(Vector2 location) {
		location = transform.InverseTransformPoint(location);

		location = new Vector2(location.x + mapTex.width/2, location.y + mapTex.height/2);
		print(location);

		//location should be a world position
		Color tmp = mapTex.GetPixel((int)location.x, (int)location.y);

		if (tmp == GrassColor) {
			return areaType.GRASSLAND;
		}
		else if (tmp == ForestColor) {
			return areaType.FOREST;
		}
		else if (tmp == MountainColor) {
			return areaType.MOUNTAIN;
		}
		else if (tmp == TundraColor) {
			return areaType.TUNDRA;
		}
		else if (tmp == DesertColor) {
			return areaType.DESERT;
		}
		else if (tmp == IceColor) {
			return areaType.ICE;
		}
		else if (tmp == WaterColor) {
			return areaType.WATER;
		}
		else if (tmp == DeepWaterColor) {
			return areaType.DEEPWATER;
		}

		return areaType.NONE;

	}

}
