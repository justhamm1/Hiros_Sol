using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {
    //--This controls the fade effect for the transistions between buildings and game maps. 
    Animator anim;
    //Will be using this variable determine the time to yeild for.  
    bool isFading = false;

	// Use this for initialization
	void Start () {
        //Gives the component of 'Animator' to the variable called anim.
        anim = GetComponent<Animator> ();

	}
    
    //This is a function that returns an 'Enumerator'
        //Allows you to call the 'Yeid' function.
        //Allows you to run a function as a coroutiine.
    public IEnumerator FadeToClear()
    {
        isFading = true;
        anim.SetTrigger("FadeIn");
        //anim.SetBool("Fade_Out", true);
        //anim.SetBool("Fade_In", false);

        //Yeild - This function will not return until 'IsFading' is set to false.
        while (isFading)
            yield return null;//Yeild to nothing (null).

    }

    public IEnumerator FadeToBlack()
    {
        isFading = true;
        anim.SetTrigger("FadeOut");
        //anim.SetBool("Fade_Out", false);
        //anim.SetBool("Fade_In", true);

        while (isFading)
            yield return null;
    }

    void AnimationComplete()
    {
        isFading = false;
    }

}
