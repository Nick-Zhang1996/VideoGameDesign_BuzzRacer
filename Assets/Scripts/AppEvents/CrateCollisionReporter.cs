using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrateCollisionReporter : MonoBehaviour
{
    public EventSound3D eventSound3DPrefab;
    public AudioClip[] boxAudio = null;
    void OnCollisionEnter(Collision c)
    {

        if (c.impulse.magnitude > 0.25f)
        {
            //we'll just use the first contact point for simplicity
            //EventManager.TriggerEvent<BoxCollisionEvent, Vector3, float>(c.contacts[0].point, c.impulse.magnitude);
            //AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);

            var worldPos = c.contacts[0].point;
            var impactForce = c.impulse.magnitude;

            const float halfSpeedRange = 0.2f;

            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

            snd.audioSrc.clip = this.boxAudio[Random.Range(0, boxAudio.Length)];

            snd.audioSrc.pitch = Random.Range(1f - halfSpeedRange, 1f + halfSpeedRange);

            snd.audioSrc.minDistance = Mathf.Lerp(1f, 8f, impactForce / 200f);
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
            

    }
}
