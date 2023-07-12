using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject left;

    public void init(List<bool> bools)
    {
        front.SetActive(bools[0]);
        right.SetActive(bools[1]);
        back.SetActive(bools[2]);
        left.SetActive(bools[3]);
    }
}
