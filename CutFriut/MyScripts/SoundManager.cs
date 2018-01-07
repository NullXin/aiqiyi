using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager _Instance;

    public AudioClip[] Ac;
    public AudioSource As;

    void Awake () {
        if (_Instance == null) {
            _Instance = this;
        }
    }

    public void PlayInte () {

        As.clip = Ac[0];
        As.Play ();
        //As.loop = true;
    }

    public void PlaySplatter () {
        As.clip = Ac[1];
        As.Play ();
    }

    public void PlayThrow () {
        As.clip = Ac[2];
        As.Play ();
    }

    public void PlayGameOve () {
        As.clip = Ac[3];
        As.Play ();
    }

    // Update is called once per frame

}