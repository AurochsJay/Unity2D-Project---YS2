using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public AudioSource audiosource;
    
    public AudioClip audiovilage;
    public AudioClip audioruin;
    public AudioClip audiobossroom;

    private void Start()
    {
        audiosource = this.gameObject.GetComponent<AudioSource>();
        audiovilage = Resources.Load<AudioClip>("Audio/ys2 rancevilage bgm_too full with love");
        audioruin = Resources.Load<AudioClip>("Audio/ys2 field bgm_ruins of moondoria");
        audiobossroom = Resources.Load<AudioClip>("Audio/ys2 boss_protectors");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-2, -10);

        CheckField();
    }

    private void CheckField()
    {
        if(ProjectManager.instance.isvillage && !this.audiosource.isPlaying )
        {
            this.audiosource.clip = audiovilage;
            this.audiosource.Play();
        }
        else if(ProjectManager.instance.isruin && !this.audiosource.isPlaying)
        {
            this.audiosource.clip = audioruin;
            this.audiosource.Play();
        }
        else if(ProjectManager.instance.isbossroom && !this.audiosource.isPlaying)
        {
            this.audiosource.clip = audiobossroom;
            this.audiosource.Play();
        }

    }
}
