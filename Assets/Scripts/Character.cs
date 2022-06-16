using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Properties")]
    [SerializeField] protected float m_speed = 25.0f;
}
