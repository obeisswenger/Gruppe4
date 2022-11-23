using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

	private float slowdownLen = 1;
	void Update ()
	{
		Time.timeScale += (1f / slowdownLen) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

	public void DoSlowmotion (float slowdownFactor, float slowdownLength)
	{
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
		slowdownLen = slowdownLength;
	}
}
