using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeHandler : MonoBehaviour
{

    public GameObject damageSmokeObject;
    PlayerLogicHandler player;
    ParticleSystem whiteSmoke;
    ParticleSystem blackSmoke;
    private bool healthy_smoke = true;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<PlayerLogicHandler>();
        whiteSmoke = this.GetComponent<ParticleSystem>();
        blackSmoke = damageSmokeObject.GetComponent<ParticleSystem>();

        blackSmoke.Stop();
        whiteSmoke.Play(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health < 30 && healthy_smoke)
        {
            healthy_smoke = false;
            whiteSmoke.Stop(false);
            blackSmoke.Play();
        }

        if (player.health > 30 && !healthy_smoke)
        {
            healthy_smoke = true;
            blackSmoke.Stop();
            whiteSmoke.Play(false);
        }


    }
}
