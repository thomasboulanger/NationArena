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
            //Update la barre de vie
            if (img.gameObject.name == "Health")
                img.fillAmount = ((float) _health.GetLife() / 100);
        }
        //Si le personnage atteint 0 pv, detruit le player
        if (_health.IsDead())
            Destroy(gameObject);
    }
}
