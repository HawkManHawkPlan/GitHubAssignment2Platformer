using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
	void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 3, this.transform.position.z);
    }
}
