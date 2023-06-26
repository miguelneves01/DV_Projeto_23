using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    private int health;

    private void Start()
    {
        health = 100 * ExperienceSystem.Instance.CurrentLevel;
    }

    public void TakeDamage(int damage){
        health -= damage;
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
