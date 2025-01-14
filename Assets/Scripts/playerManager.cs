﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    // Player specific variables

    PlayerInfo info;

    // Boolean values
    private bool isGamePaused = false;

    // UI stuff
    public Text healthText;
    public Text scoreText;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    private List<collectable> inventory = new List<collectable>();

    public Text inventoryText;

    public Text descriptionText;

    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.FindWithTag("Info").GetComponent<PlayerInfo>();
        foreach (collectable item in info.inventory)
        {
            item.player = this.gameObject;
        }
        // Makes sure game is "unpaused"
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Make sure all menus are filled in
        FindAllMenus();

        //Start player with initial health and score
        //info.health = 100;
        //info.score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + info.health.ToString();
        scoreText.text  = "Score:  " + info.score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (info.health <= 0)
        {
            LoseGame();
        }
        if (inventory.Count == 0)

        {

            // If the inventory is empty

            inventoryText.text = "Current Selection: None";

            descriptionText.text = "";

        }

        else

        {

            inventoryText.text = "Current Selection: " + inventory[currentIndex].collectableName + " " + currentIndex.ToString();

            descriptionText.text = "Press [E] to " + inventory[currentIndex].description;

        }

        if (Input.GetKeyDown(KeyCode.E))

        {

            // Using

            if (inventory.Count > 0)

            {

                inventory[currentIndex].Use();

                inventory.RemoveAt(currentIndex);

                currentIndex = (currentIndex - 1) % inventory.Count;

            }

        }

        if (Input.GetKeyDown(KeyCode.I))

        {

            if (inventory.Count > 0)

            {

                // Move to next in inventory

                currentIndex = (currentIndex + 1) % inventory.Count;

            }

        }

    }

    void FindAllMenus()
    {
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
        }
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        if (winMenu == null)
        {
            winMenu = GameObject.Find("WinGameMenu");
            winMenu.SetActive(false);
        }
        if (loseMenu == null)
        {
            loseMenu = GameObject.Find("LoseGameMenu");
            loseMenu.SetActive(false);
        }
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseGameMenu");
            pauseMenu.SetActive(false);
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        winMenu.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        loseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            // Unpause game
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }
        else
        {
            // Pause game
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            isGamePaused = true;
        }
    }

    public void ChangeHealth(int value)
    {
        info.health += value;
    }

    public void ChangeScore(int value)
    {
        info.score += value;
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {

        if (collision.GetComponent<collectable>() != null)

        {

            collision.GetComponent<collectable>().player = this.gameObject;

            collision.gameObject.transform.parent = null;

            inventory.Add(collision.GetComponent<collectable>());

            collision.gameObject.SetActive(false);

        }

    }

}
