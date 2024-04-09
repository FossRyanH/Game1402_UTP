using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SceneTransition : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        //_playableDirector.played += (val:PlayableDirector) => Debug.Log(message: "We are playing the timeline");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            _playableDirector?.Play();
        }
    }
}
