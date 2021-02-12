using UnityEngine;

public enum VisualizationMode
{
	Ring,
	RingWithBeat
}

public class AudioVisualization : MonoBehaviour
{
	public int bufferSamplesSize;
	public float samplePercentage;
	public float emphasisMultiplier;
	public float retractionSpeed;

	public int amountOfSegments;
	public float radius;
	public float bufferSizeArea;
	public float maximumExtendLenght;

	public GameObject lineRendererPrefab;
	public Material lineRendererMaterial;
	public VisualizationMode visualizationMode;

	public Gradient colorGradientA = new Gradient(); 
	public Gradient colorGradientB = new Gradient(); 

	private Gradient currentColor = new Gradient();

	private float sampleRate;

	private float[] samples;
	private float[] spectrum;
	private float[] extendLenghts;

	private LineRenderer[] lineRenderers;

	private AudioSource audioSource;

	private void Awake()
	{
	audioSource = GetComponent<AudioSource>();

	Debug.Log("Clip data" + audioSource.clip);

	sampleRate = AudioSettings.outputSampleRate;

	samples = new float[bufferSamplesSize];
	spectrum = new float[bufferSamplesSize];

	Debug.Log("Spectrum ---------- " + spectrum);
	Debug.Log("Spectrum length---------- " + spectrum.Length);

	switch(visualizationMode)
	{
	case VisualizationMode.Ring:{
		InitiateRing();
		break;
	}
	}
	}

	private void InitiateRing()
	{
	extendLenghts = new float [amountOfSegments +1];
	lineRenderers = new LineRenderer [extendLenghts.Length];

	for (int i = 0; i < lineRenderers.Length; i++)
	{
	GameObject go = Instantiate(lineRendererPrefab);
	go.transform.position = Vector3.zero;

	LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
	lineRenderer.sharedMaterial = lineRendererMaterial;

	lineRenderer.positionCount = 2;
	lineRenderer. useWorldSpace = true;
	lineRenderers[i] = lineRenderer;
	}
	}

	private void Update()
	{
	audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

	UpdateExtends();

	if(visualizationMode == VisualizationMode.Ring)
	{
    UpdateRing();
	}
	}

	private void UpdateExtends()
	{
	int interation = 0;
	int indexOnSpectrum = 0;
	int averageValue = (int) Mathf.Abs((samples.Length * samplePercentage)/amountOfSegments) + 3;

	if(averageValue < 1)
	{
	averageValue = 1;
	}

	while(interation< amountOfSegments)
	{
	int interationIndex = 0;
	float sumValueY = 0;

	while(interationIndex < averageValue)
	{
	sumValueY += spectrum[indexOnSpectrum];
	indexOnSpectrum++;
	interationIndex++; 
	}

    float y = sumValueY / averageValue * emphasisMultiplier;
    extendLenghts[interation] -= retractionSpeed * Time.deltaTime;

    if(extendLenghts[interation] < y)
    {
    extendLenghts[interation] = y;
    }

    if(extendLenghts[interation] > maximumExtendLenght)
    {
    extendLenghts[interation] = maximumExtendLenght;
    }
	interation++;
	}
}

private void UpdateRing()
{
	for(int i = 0; i < lineRenderers.Length; i++)
	{
	float t = i / (lineRenderers.Length - 2f);
	float a = t * Mathf.PI * 2f;

	Vector2 direction = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
	float maximumRadius = (radius + bufferSizeArea + extendLenghts[i]);

	lineRenderers[i].SetPosition(0, direction * radius);
	lineRenderers[i].SetPosition(1, direction * maximumRadius);

	lineRenderers[i].startWidth = Spacing(radius);
	
	Debug.Log("lineRenderers[i].startWidth + -----" + lineRenderers[i].startWidth);
	lineRenderers[i].endWidth = Spacing(maximumRadius);
	Debug.Log("radius" + radius);
	Debug.Log("maximumRadius" + maximumRadius);
	Debug.Log("lineRenderers[i].startWidth + -----" + lineRenderers[i].endWidth);

	lineRenderers[i].startColor = colorGradientA.Evaluate(0);
	lineRenderers[i].endColor = colorGradientA.Evaluate((extendLenghts[i] - 1)/ (maximumExtendLenght - 1f));
	}
}

private float Spacing(float radius)
{
	float c = 2f * Mathf.PI * radius;
	float n = lineRenderers.Length;
	return c / n;
}

}