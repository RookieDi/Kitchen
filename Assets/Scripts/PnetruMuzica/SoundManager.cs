using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioRefsScriptateObject _audioRefsScriptateObject;
    public float EffectsVolume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_MusicSucces; 
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_MusicFailed;
        CuttinCounter.OnAnyCut += OnAnyCuttinCunter_Music;
        Move.Instance.onPickSmth += Player_OnPickedSmth;
        BaseCounter.OnAnyObjHere += Object_PlacedMusic;
        TrashBin.OnAnyObjTrashed += Object_TraashBinMusic;
    }

    private void Object_TraashBinMusic(object sender, EventArgs e)
    {
        TrashBin trashBin =sender as TrashBin;
        PlaySound(_audioRefsScriptateObject.trash,trashBin.transform.position);
    }

    private void Object_PlacedMusic(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        ;
      PlaySound(_audioRefsScriptateObject.objectDrop,baseCounter.transform.position);
    }

    private void Player_OnPickedSmth(object sender, EventArgs e)
    {
        PlaySound(_audioRefsScriptateObject.objectPickup,Move.Instance.transform.position);
    }

    private void OnAnyCuttinCunter_Music(object sender, EventArgs e)
    {
        CuttinCounter cuttinCounter = sender as CuttinCounter;
        ;
        PlaySound(_audioRefsScriptateObject.chop,cuttinCounter.transform.position);
    }

    private void DeliveryManager_MusicSucces(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioRefsScriptateObject.deliverySucces,deliveryCounter.transform.position);
        
    }

    private void DeliveryManager_MusicFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioRefsScriptateObject.deliveryFail,deliveryCounter.transform.position);
        
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)],position,volume);
    }
    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volume * EffectsVolume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(_audioRefsScriptateObject.footstep,position,volume);
    }
}
