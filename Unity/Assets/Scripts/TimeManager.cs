using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

	private float slowdownLen = 1;
	public bool timed = false;
	void Update ()
	{	
		if(timed)
		{
			Time.timeScale += (1f / slowdownLen) * Time.unscaledDeltaTime;
			Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
			if(Time.timeScale == 1)
			{
				timed = false;
			}
		}
	}

	public void DoSlowmotionTimed (float slowdownFactor, float slowdownLength)
	{
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
		slowdownLen = slowdownLength;
		timed = true;
	}
	public void DoSlowmotion (float slowdownFactor, bool on)
	{
		if(on)
		{
			Time.timeScale = slowdownFactor;
			Time.fixedDeltaTime = Time.timeScale * .02f;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
