using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUi : MonoBehaviour
{
  
    [SerializeField] private Button showTutorialButton;
    [SerializeField] private TutoriaUi _tutoriaUi;
    
    private void Start()
    {
      
        
      showTutorialButton.onClick.AddListener(showTutorialWindow);
        Hide();
    
    }

    private void showTutorialWindow()
    {
        _tutoriaUi.Show();
    }
    
    
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
       
      
      
        gameObject.SetActive(true);
    }
    
    
    
    
    
    
    
}
