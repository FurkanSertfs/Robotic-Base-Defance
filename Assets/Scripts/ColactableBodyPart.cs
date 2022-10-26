using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColactableBodyPart : MonoBehaviour
{
    public Mesh[] partObject;

    public Material material;

    [HideInInspector]
    public int Id;

    public int level;

}
