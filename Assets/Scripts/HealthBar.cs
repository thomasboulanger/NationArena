using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health _health;
    [HideInInspector]
    public GameObject healthBar;
    
    void Start()
    {
        //Cree une barre de vie de 100 pvs
        _health = new Health(100);
        //Initialize le compteur de texte
        healthBar.GetComponentInChildren<Text>().text = _health.GetLife().ToString();
    }

    public void GetHit(int damage)
    {
       transform.GetComponent<PlayerInputScript>().isDead = _health.GetHit(damage);
        UpdateHpOnUI();
    }

    public void Heal(int heal)
    {
        _health.GetHit(-heal);
        UpdateHpOnUI();
    }

    private void UpdateHpOnUI()
    {
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
    }
}
