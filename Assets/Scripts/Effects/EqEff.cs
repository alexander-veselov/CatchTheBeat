using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqEff : MonoBehaviour {
    line[] lines;
    private bool m_IsOk = false;
    private int m_NumSamples = 180;
    private float[] m_SamplesL, m_SamplesR;
    private int i;
    private float maxL, maxR, sample, sumL, sumR, rms, dB;
    private float[] sum;
    private Vector3[] scales;
    // Because rms values are usually very low
    private float volume = 30f;
    private Color color;
    [SerializeField]
    AudioSource audioSource;
    void Start ()
    {
        audioSource = GameObject.Find("mapScript").GetComponentInChildren<AudioSource>();
        lines = new line[180];
        scales = new Vector3[180];
        sum = new float[m_NumSamples];
        Transform rot = transform;
        for (int i = 0; i < 45; i++)
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
            lines[i + 90] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i + 90].transform;
            rot.Rotate(0, 0, A + 90);

        }
        for (int i = 45; i < 90; i++)
        {
            Vector3 pos;

            pos.y = (i - 67) / 9f;

            pos.x = Mathf.Sqrt(12.5f - pos.y * pos.y);
            pos.z = 0;
            float A = Mathf.Atan2(pos.y, pos.x) / Mathf.PI * 180;

            lines[i + 90] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i + 90].transform;
            rot.Rotate(0, 0, A + 90);


            pos.x = -pos.x;
            A = Mathf.Atan2(pos.y, pos.x) / Mathf.PI * 180;
            lines[i] = Instantiate(Resources.Load<line>("line"), pos, transform.rotation);
            rot = lines[i].transform;
            rot.Rotate(0, 0, A + 90);

        }

        for (int i = 0; i < 45; i++)
        {
            line temp = lines[134 - i];
            lines[134 - i] = lines[i + 135];
            lines[i + 135] = temp;
        }
        for (int i = 0; i < 45; i++)
        {
            line temp = lines[i + 90];
            lines[i + 90] = lines[179 - i];
            lines[179 - i] = temp;
        }


        m_SamplesL = new float[m_NumSamples];
        m_SamplesR = new float[m_NumSamples];
        for (int i = 0; i < 180; i++)
        {
            scales[i] = lines[i].transform.localScale;
        }
        m_IsOk = true;

    }
    int j = 0;

    public void eqv()
    {
        audioSource.GetOutputData(m_SamplesL, 0);


        maxL = maxR = 0.0f;
        sumL = 0.0f;
        sumR = 0.0f;
        for (i = 0; i < m_NumSamples; i++)
        {
            sum[i] = m_SamplesL[i] * m_SamplesL[i];
        }

        for (int i = 0; i < 180; i++)
        {
            rms = Mathf.Sqrt(sum[i] / m_NumSamples);
            scales[i].y = Mathf.Clamp01(rms * volume);
            scales[i].y *= 1.2f*0.3f;
            scales[i].x = 0.3f;
            lines[i].transform.localScale = scales[i];
        }
    }
	void Update () {
        // Continuing proper validation
        if (j % 2 == 0)
        {
            Vector3 fir = scales[0];
            for (i = 0; i < 89; i++)
            {
                scales[i] = scales[i + 1];
            }
            scales[89] = fir;

            fir = scales[90];
            for (i = 178; i > 90; i--)
            {
                scales[i] = scales[i - 1];
            }
            scales[179] = fir;
        }
        if (j++ % 10 == 0) eqv();
        
            for (int i = 0; i < 180; i++)
            {
                scales[i].y *= 0.95f;
                lines[i].transform.localScale = scales[i];
            }
        
    }

}
