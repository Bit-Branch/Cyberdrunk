using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{

	public Camera mainCamera;
	private AudioSource audioSource;
	public float updateStep = 0.01f;
	public int sampleDataLength = 1024;

	private float currentUpdateTime = 0f;

	public float clipLoudness;
	private float[] clipSampleData;

	public float sizeFactor = 5f;

	public SpriteRenderer[] spriteRenderer;



	private void Awake()
	{
		clipSampleData = new float[sampleDataLength];
		
		foreach(SpriteRenderer sr in spriteRenderer){
		sr.drawMode = SpriteDrawMode.Tiled;
		}
	}

	private void Start() {
		audioSource = mainCamera.gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep)
		{
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
			clipLoudness = 0f;
			foreach (var sample in clipSampleData)
			{
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength;

			clipLoudness *= sizeFactor * 40;

			foreach(SpriteRenderer sr in spriteRenderer){
			sr.size = new Vector2(sr.size.x, clipLoudness);
			}

		}
	}
}
