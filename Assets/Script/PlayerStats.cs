using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _initialHealth;
    private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private GameObject _healthImage;
    [SerializeField] private GameObject _healthPanel;
    [SerializeField] private List<Sprite> _hearts;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.white;
        _currentHealth = _initialHealth;
        AddInitialHealthImage();

    }

    private void AddInitialHealthImage()
    {
        for (int i = 0; i < _initialHealth/2; i++)
        {
           // AddHealth(2);
        }
    }
    
  /*  private void AddHealth(int lifeAdded)
    {
        _maxHealth += lifeAdded;
        _healthPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, 142.5f*_maxHealth/2);
        var a = Instantiate(_healthImage);
        a.transform.SetParent(_healthPanel.transform, false);
    }
    */
    private void OnCollisionEnter2D(Collision2D other)
    {
        DamagePlayer();

    }

    /// <summary>
    /// It gets the child image with an index based on the player's health
    /// <para>Then it assigns the image with the next sprite on the array (that repeats itself after reaching the end)</para> 
    /// </summary>
    private void DamagePlayer()
    {
        var imageObject = _healthPanel.transform.GetChild((int)Mathf.Ceil(_currentHealth/2f)-1);
        imageObject.GetComponent<Image>().sprite = _hearts[(_currentHealth+1)%2];
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}

