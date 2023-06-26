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
        if (health <= 0){
            GridBuildingSystem.Instance.DemolishRandomBuilding();
            SceneController.UnloadScene("2D");
            Destroy(gameObject);
        }
    }
}
