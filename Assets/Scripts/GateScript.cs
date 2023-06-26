using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateScript : MonoBehaviour
{
    private int health;
    private int maxHealth;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        health = 100 * ExperienceSystem.Instance.CurrentLevel;
        maxHealth = health;
        healthBar.fillAmount = 1f;
    }

    public void TakeDamage(int damage){
        health -= damage;
        healthBar.fillAmount =  (float) health / (float)maxHealth;
        Debug.Log("Damage Taken");
        if (health <= 0)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }

            GridBuildingSystem.Instance.DemolishRandomBuilding();
            SceneController.UnloadScene("2D");
            SceneController.LoadScene("GameOver");
            Destroy(gameObject);
        }
    }
}
