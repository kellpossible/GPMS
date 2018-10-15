using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {


	// the amount of time to skip at the start of each animation clip.
	// Because each animation clip must start at 0.0.0 and then jump to where it needs to be.
	// This is because root motion calculates relative position based on the first frame.

	
	[Header("Values for Monitoring Only")]
    ///////////////////////////////////
	
	[Tooltip("Changing this value won't do anything, it is only for viewing the arraIndices during debugging")]
	public int[] ArrayIndices = new int[2];

	[Tooltip("Changing this value won't do anything, it is only for viewing the tileType during debugging")]
	public TileType TileType;

	[Tooltip("Changing this value won't do anything, it is used by MapTransitioner to know if a tile is about to be deleted")]
	public bool OnDeletionPath = false;


	
	[Header("Editable Values")]
    ///////////////////////////////////
	
	[Tooltip("Type in the name of the transition you would like to override with.")]
	public string onTransitionOveride;

	[Tooltip("Type in the name of the transition you would like to override with.")]
	public string offTransitionOveride;


	

	

	private Animator tileAnimator;
	private ArrayList onAnimClips = new ArrayList();
	private ArrayList offAnimClips = new ArrayList();
	//private AnimationClip[] animationClips;

	// Use this for initialization (start wasn't early enough for the on transition)
	void Awake () {
		tileAnimator = gameObject.GetComponent<Animator>();
		sortAnimClips( tileAnimator.runtimeAnimatorController.animationClips);
	}
	


	private void sortAnimClips( AnimationClip[] allClips ) {

		foreach(AnimationClip animClip in allClips) {
			if(animClip.name.IndexOf("on_") == 0) {
				// it's an on transition
				onAnimClips.Add(animClip);

			} else if (animClip.name.IndexOf("off_") == 0) {
				// it's an off transition
				offAnimClips.Add(animClip);

			}
		}

		
	}


	
	public void TransitionOn(string transitionName) {
		onTransitionOveride = transitionName;
		TransitionOn();
	}
	public void TransitionOn() {
		gameObject.SetActive(true);
		startTransitionOn();
	}


	public void TransitionOff(string transitionName) {
		offTransitionOveride = transitionName;
		TransitionOff();
	}
	public void TransitionOff() {
		float lengthOfAnim = startTransitionOff();
		Destroy(gameObject, lengthOfAnim);
	}





	private float startTransitionOn() {
		return startTransitionOn("random");
	}
	private float startTransitionOn(string type) {
		if(onTransitionOveride != null &&
			onTransitionOveride != "" &&
			onTransitionOveride != " "
			) {
			
			type = onTransitionOveride;
		}
		return startTransitionFromSet(type, onAnimClips);
	}

	private float startTransitionOff() {
		return startTransitionOff("random");
	}
	private float startTransitionOff(string type) {
		if(	offTransitionOveride != null &&
			offTransitionOveride != "" &&
			offTransitionOveride != " "
			) {
			
			type = offTransitionOveride;
		}
		return startTransitionFromSet(type, offAnimClips);
	}

	private float startTransitionFromSet(string type, ArrayList animClipSet) {
		// Debug.Log("startTransitionFromSet: type = "+ type);

		int clipIndex = 0;
		AnimationClip animClip;

		
		if(type == "random") {

			clipIndex = Random.Range(0, animClipSet.Count);

		} else {

			// search the onTransition for the index of the one specified
			for(int k = 0; k<animClipSet.Count; k++) {
				if( ((AnimationClip)animClipSet[k]).name == type) {
					clipIndex = k;
					break;
				}
			}

		}

		

		// get a reference to the actual anim clip
		animClip = (AnimationClip)animClipSet[clipIndex];
		tileAnimator.Play(animClip.name);

		// return the length of the clip
		return getClipLength(animClip.name, animClipSet);

	}



	private float getClipLength(string clipName, ArrayList animClipSet) {
		float clipLength = 0;
		foreach(AnimationClip clip in animClipSet)
        {
            if( clip.name==clipName ) {
				clipLength = clip.length;
				break;
			}
        }

		return clipLength;

	}
}
