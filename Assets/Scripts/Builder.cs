using System.Collections.Generic;
using UnityEngine;
using Rewired;
using TMPro;

public class Builder : MonoBehaviour {
	public int rewiredPlayerId = 0;
	public GameObject item;
	public float rotateSpeed = 180;

	public TextMeshProUGUI massText;

	private Player player;

	private Vector2 mousePos;
	private bool spawnNewItem = false;
	private bool stuckToMouse = true;
	private bool placeItem = false;
	private bool play = false;
	private float rotation = 0;
	private float changeMass = 0;
	private IList<GameObject> planks = new List<GameObject>();
	private GameObject currentPlank = null;
	private float itemMass = 1;
	private float minMass = 0;
	private float maxMass = 500;

	void Start()
	{
		player = ReInput.players.GetPlayer(rewiredPlayerId);
	}
	
	void Update () {
		GetInput();
		ProcessInput();
	}

	void GetInput() {
		mousePos = Camera.main.ScreenToWorldPoint(ReInput.controllers.Mouse.screenPosition);
		spawnNewItem = player.GetButtonDown("NewItem");
		placeItem = player.GetButtonDown("LeftClick");
		play = player.GetButtonDown("Play");
		rotation = player.GetAxis("Rotate");
		changeMass = player.GetAxis("ChangeMass");
	}

	void ProcessInput() {
		if (spawnNewItem) {
			currentPlank = Instantiate(item, mousePos, Quaternion.identity);
			currentPlank.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.red, 1 / maxMass);
			planks.Add(currentPlank);
			stuckToMouse = true;
			massText.text = "Current Mass: 1";
		}

		if (placeItem) {
			currentPlank = null;
			stuckToMouse = false;
			massText.text = "Current Mass: ";
		}

		if (stuckToMouse) {
			currentPlank.transform.position = mousePos;
			if (rotation != 0f) {
				currentPlank.transform.Rotate(-transform.forward, rotation * rotateSpeed * Time.deltaTime);
			}

			if (changeMass != 0.0f) {
				float targetMass = Mathf.Clamp(currentPlank.GetComponent<Rigidbody2D>().mass + (changeMass * 50.0f * Time.deltaTime), minMass, maxMass);
				currentPlank.GetComponent<Rigidbody2D>().mass = targetMass;
				currentPlank.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.red, targetMass / maxMass);
				massText.text = "Current Mass: " + (int)targetMass;
			}
		}

		if (play) {
			foreach (GameObject go in planks) {
				go.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
	}
}
