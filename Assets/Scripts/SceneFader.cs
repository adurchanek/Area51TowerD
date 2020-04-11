using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
	public Image image;
	public AnimationCurve curve;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void FadeTo(string scene)
	{
		StartCoroutine(FadeOut(scene));
	}

	public IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime ;

			float a = curve.Evaluate(t);
			image.color = new Color(0f,0f,0f,a);
			yield return 0;
		}

		SceneManager.LoadScene(1);
	}

	public IEnumerator FadeIn()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime ;

			float a = curve.Evaluate(t);
			image.color = new Color(0f,0f,0f,a);
			yield return 0;
		}
	}
}
