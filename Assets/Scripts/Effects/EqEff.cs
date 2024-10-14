using UnityEngine;

public class EqEff : MonoBehaviour {
    private int m_NumSamples = 180; // Should be divisible by 4
    private float volume = 30f; // Because rms values are usually very low
    private int ticks = 0;
    private float[] m_SamplesL;
    private float[] sum;
    private Vector3[] scales;
    private line[] lines;

    [SerializeField]
    AudioSource audioSource;
    void Start ()
    {
        audioSource = GameObject.Find("mapScript").GetComponentInChildren<AudioSource>();
        lines = new line[m_NumSamples];
        scales = new Vector3[m_NumSamples];
        sum = new float[m_NumSamples];
        Transform rot = transform;
        Debug.Assert(m_NumSamples % 4 == 0);
        var quarter = m_NumSamples / 4;
        var half = quarter * 2;
        var threeQuarters = quarter * 3;
        for (int i = 0; i < quarter; i++)
        {
            Vector3 pos;

            pos.x = (i - 22) / 9f;

            pos.y = Mathf.Sqrt(12.5f - pos.x * pos.x);
            pos.z = 0;
            float A = Mathf.Atan2(pos.y, pos.x) / Mathf.PI * 180;

            lines[i] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i].transform;
            rot.Rotate(0, 0, A + 90);


            pos.y = -pos.y;
            A = Mathf.Atan2(pos.y, pos.x) / Mathf.PI * 180;
            lines[i + half] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i + half].transform;
            rot.Rotate(0, 0, A + 90);

        }
        for (int i = quarter; i < half; i++)
        {
            Vector3 pos;

            pos.y = (i - 67) / 9f;

            pos.x = Mathf.Sqrt(12.5f - pos.y * pos.y);
            pos.z = 0;
            float A = Mathf.Atan2(pos.y, pos.x) / Mathf.PI * 180;

            lines[i + half] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i + half].transform;
            rot.Rotate(0, 0, A + 90);


            pos.x = -pos.x;
            A = Mathf.Atan2(pos.y, pos.x) / Mathf.PI * 180;
            lines[i] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i].transform;
            rot.Rotate(0, 0, A + 90);

        }

        for (int i = 0; i < quarter; i++)
        {
            line temp = lines[threeQuarters - 1 - i];
            lines[threeQuarters - 1 - i] = lines[i + threeQuarters];
            lines[i + threeQuarters] = temp;
        }
        for (int i = 0; i < quarter; i++)
        {
            line temp = lines[i + half];
            lines[i + half] = lines[m_NumSamples - 1 - i];
            lines[m_NumSamples - 1 - i] = temp;
        }


        m_SamplesL = new float[m_NumSamples];
        for (int i = 0; i < m_NumSamples; i++)
        {
            scales[i] = lines[i].transform.localScale;
        }

    }

    public void eqv()
    {
        audioSource.GetOutputData(m_SamplesL, 0);

        for (int i = 0; i < m_NumSamples; i++)
        {
            sum[i] = m_SamplesL[i] * m_SamplesL[i];
        }

        for (int i = 0; i < m_NumSamples; i++)
        {
            var rms = Mathf.Sqrt(sum[i] / m_NumSamples);
            scales[i].y = Mathf.Clamp01(rms * volume);
            scales[i].y *= 1.2f*0.3f;
            scales[i].x = 0.3f;
            lines[i].transform.localScale = scales[i];
        }
    }
	  void Update ()
    {
        var half = m_NumSamples / 2;

        if (ticks % 2 == 0)
        {
            Vector3 fir = scales[0];
            for (int i = 0; i < half - 1; i++)
            {
                scales[i] = scales[i + 1];
            }
            scales[half - 1] = fir;

            fir = scales[half];
            for (int i = m_NumSamples - 2; i > half; i--)
            {
                scales[i] = scales[i - 1];
            }
            scales[m_NumSamples - 1] = fir;
        }

        if (ticks++ % 10 == 0) eqv();

        for (int i = 0; i < m_NumSamples; i++)
        {
            scales[i].y *= 0.95f;
            lines[i].transform.localScale = scales[i];
        }
    }
}
