using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CellController : MonoBehaviour
{
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject light;
    [Range(0, 1)] public float LightChance;
    private Light lightComponent;
    public void init(List<bool> bools)
    {
        var lc = Random.Range(0.0f, 1.0f);
        lightComponent = light.GetComponent<Light>();
        if (lc > LightChance)
            light.SetActive(false);
        front.SetActive(bools[0]);
        right.SetActive(bools[1]);
        back.SetActive(bools[2]);
        left.SetActive(bools[3]);
    }

    private void Update()
    {
        if (light.activeSelf)
            lightComponent.intensity = Random.Range(2.5f, 3.0f);
    }
}
