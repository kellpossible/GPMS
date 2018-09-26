using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {

	public int[] ArrayIndices = new int[2];
	public TileType TileType;
	private Animator tileAnimator;
	private AnimationClip[] animationClips;

	// Use this for initialization
	void Start () {
		tileAnimator = gameObject.GetComponent<Animator>();
		animationClips = tileAnimator.runtimeAnimatorController.animationClips;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void TransitionOn() {
		gameObject.GetComponent<Animator>().Play("on_popUp");
        gameObject.SetActive(true);
	}


	public void TransitionOff() {

		tileAnimator.Play("off_floatUp");
		float lengthOfAnim = getStateLength("off_floatUp");

		Destroy(gameObject, lengthOfAnim);
	}



	private float getStateLength(string clipName)
	{
		float clipLength = 0;
		foreach(AnimationClip clip in animationClips)
        {
            if( clip.name==clipName ) {
				clipLength = clip.length;
				break;
			}
        }

		return clipLength;

	}
}
