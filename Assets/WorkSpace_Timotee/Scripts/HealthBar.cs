using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health _health;
    public GameObject healthBar;

    private void OnTest()
    {
        //Appele la fonction pour prendre des dommages
        GetHit(5);
        
    }
    
    void Start()
    {
        //Cree une barre de vie de 100 pvs
        _health = new Health(100);
        //Initialize le compteur de texte
        healthBar.GetComponentInChildren<Text>().text = _health.GetLife().ToString();
    }

    public void GetHit(int damage)
    {
        _health.GetHit(damage);
        //Update le nombre de point de vie
        healthBar.GetComponentInChildren<Text>().text = _health.GetLife().ToString();
        //Recupere la liste des images
        Image[] imageList = healthBar.GetComponentsInChildren<Image>();
        //Pour chaque component:
        foreach (Image img in imageList)
        {
            //Verifie quelle partie de la barre de vie ils sont
            //Note: This is only important if the healthbar is composed of 3 different sprites, otherwise it can all be deprecated
            switch (img.gameObject.name)
            {
                //Si c'est la droite, prend en compte seulement les 12 premiers pv
                case "BarRight":
                    img.fillAmount = (_health.GetLife() - 88f) / 12;
                    break;
                //Si c'est le milieu, prend en compte seulement les 76 pv du milieu
                case "Bar":
                    img.fillAmount = (_health.GetLife() - 12f) / 76;
                    break;
                //Si c'est la gauche, prend en compte seulement les 12 derniers pv
                case "BarLeft":
                    img.fillAmount = (float) _health.GetLife() / 12;
                    break;
            }
        }
        //Si le personnage atteint 0 pv, detruit le player
        if (_health.IsDead())
            Destroy(gameObject);
    }
}
