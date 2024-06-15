using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AudioRefsScriptateObject : ScriptableObject
{

    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySucces;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
