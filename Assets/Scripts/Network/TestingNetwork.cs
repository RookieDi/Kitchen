using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetwork : MonoBehaviour
{
   [SerializeField] private Button hostButton;
   [SerializeField] private Button clientButton;

   private void Awake()
   {

      hostButton.onClick.AddListener(() =>
      {
         NetworkManager.Singleton.StartHost();
         Hide();
      });
      clientButton.onClick.AddListener(() =>
      {
         NetworkManager.Singleton.StartClient();
         Hide();
      });
   }

   public void Hide()
   {
      
      gameObject.SetActive(false);
   }
}
