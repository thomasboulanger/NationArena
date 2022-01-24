using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public GameObject healthBar;

    private void OnTest()
    {
        //Appele la fonction pour prendre des dommages
        GetHit(5);
        
    }
    
    void Start()
    {
        //Cree une barre de vie de 100 pvs
        health = new Health(100);
        //Initialize le compteur de texte
        healthBar.GetComponentInChildren<Text>().text = health.GetLife().ToString();
    }

    void Update()
    {
        
    }

    public void GetHit(int damage)
    {
        health.GetHit(damage);
        //Update le nombre de point de vie
        healthBar.GetComponentInChildren<Text>().text = health.GetLife().ToString();
        //Recupere la liste des images
        Component[] imageList = healthBar.GetComponentsInChildren<Image>();
        //Pour chaque component:
        foreach (Component comp in imageList)
        {
            //Verifie quelle partie de la barre de vie ils sont
            switch (comp.gameObject.name)
            {
                //Si c'est la droite, prend en compte seulement les 12 premiers pv
                case "BarRight":
                    (comp as Image).fillAmount = (float) (health.GetLife() - 88f) / 12;
                    break;
                //Si c'est le milieu, prend en compte seulement les 76 pv du milieu
                case "Bar":
                    (comp as Image).fillAmount = (float) (health.GetLife() - 12f) / 76;
                    break;
                //Si c'est la gauche, prend en compte seulement les 12 derniers pv
                case "BarLeft":
                    (comp as Image).fillAmount = (float) health.GetLife() / 12;
                    break;
            }
        }
        //Si le personnage atteint 0 pv, detruit le player
        if (health.IsDead())
            Destroy(gameObject);
    }
}
