using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;


public class MainMenu : MonoBehaviour
{

    public Slider healthSlider;
    public Slider speedSlider;
    public Slider spawnXSlider;
    public Slider spawnYSlider;
    public Button startButton;


    // RREFERENCE TO PLAYER PREFAB, PLAYER CONTROLLER, AND PLAYER HEALTH        Q4
    public GameObject playerPrefab;

    public PlayerController playerController;
    private Health playerHealth;


    public GameObject canvas;


    public void StartGame()//       Q4
    {
        if (playerController != null && playerHealth != null)
        {
            // APPLY SLIDER VALUES TO PLAYER
            playerHealth.maxHP = Mathf.RoundToInt(healthSlider.value);
            playerHealth.currentHP = playerHealth.maxHP;

            playerController.maxSpeed = speedSlider.value;

            Vector3 newPosition = new Vector3(spawnXSlider.value, spawnYSlider.value, playerController.transform.position.z);
            playerController.transform.position = newPosition;

            //Debug.Log($"Player settings updated: Health = {playerHealth.maxHP}, Speed = {playerController.maxSpeed}, Position = {newPosition}");
        }

        // REMOVE CANVAS AFTER STARTING     Q4
        canvas.SetActive(false);
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);

        playerController = FindObjectOfType<PlayerController>();
        playerHealth = playerController.GetComponent<Health>();

        if (playerController == null || playerHealth == null)
        {
            Debug.LogError("PlayerController or Health component is missing in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
