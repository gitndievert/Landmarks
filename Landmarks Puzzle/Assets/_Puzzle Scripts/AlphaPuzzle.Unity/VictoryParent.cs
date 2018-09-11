using System.Linq;
using UnityEngine;

public class VictoryParent : MonoBehaviour
{
    public GameObject[] MultipleEffects;

    public AudioClip CheerSound;
    public AudioClip CompletedVoice;
    public AudioClip FireWorkSound;
    
    public void StartEffects()
    {        
        if(MultipleEffects.Length > 0)
            foreach(var effect in MultipleEffects)
            {
                effect.SetActive(true);
            }
    }

    public void StopEffects()
    {
        if (MultipleEffects.Length > 0)
            foreach (var effect in MultipleEffects)
            {
                effect.SetActive(false);
            }
    }

    
}
