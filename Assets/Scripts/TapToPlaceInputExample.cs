using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class TapToPlaceInputExample : MonoBehaviour
{
    private GameObject cube;
    private TapToPlace tapToPlace;

    private bool isTaping = false;

    void Start()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = Vector3.one * 0.2f;
        cube.transform.position = Vector3.forward * 0.7f;

        tapToPlace = cube.AddComponent<TapToPlace>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            tapToPlace.StartPlacement();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            tapToPlace.StopPlacement();
        }
    }
    public void OnClickTapEvent(){
        if(isTaping){
            tapToPlace.StopPlacement();
            isTaping = false;
        }else{
            tapToPlace.StartPlacement();
            isTaping = true;
        }

    }
}
