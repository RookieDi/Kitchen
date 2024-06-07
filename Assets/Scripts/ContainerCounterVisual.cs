using UnityEngine;


public class ContainerCounterVisual : MonoBehaviour
{
   private Animator _animator;

   [SerializeField] private ContainerCounter _containerCounter;

   private const string OPEN_CLOSE = "OpenClose";

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Start()
   {
      _containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
   }

   private void ContainerCounter_OnPlayerGrabbedObject(object sender,System.EventArgs e)
   {
     _animator.SetTrigger(OPEN_CLOSE);
   }
}
