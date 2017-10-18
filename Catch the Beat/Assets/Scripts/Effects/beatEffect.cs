using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class beatEffect : MonoBehaviour {

    SpriteRenderer sprite;
    Vector3 fullScale;
    void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
     
    }
	
    public void beat(Vector3 scale)
    {
        fullScale = scale;
        float s = scale.x*0.95f;
        transform.localScale = new Vector3(s, s, 1);
        menuOverlay m = Instantiate(Resources.Load<menuOverlay>("overlay"), transform.position, transform.rotation);
        m.transform.localScale = fullScale / 60f;
    }

	void Update () {
		if (transform.localScale.x < fullScale.x)
        {
            float s = transform.localScale.x+0.04f;
            transform.localScale = new Vector3(s, s, 1);
        }
	}
}
