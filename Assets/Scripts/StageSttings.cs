using System.Collections;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using TMPro;

public class StageSttings : MonoBehaviour
{
    [SerializeField]
    float Floor;

    [SerializeField]
    GameObject floorGetter;

    [SerializeField, Tooltip("After measuring the floor, we will still use SpatialMesh.")]
    bool usePersistentSpatialMesh = false;
    [SerializeField]
    GameObject FloorAnchor;

    [SerializeField]
    GameObject StageObject;

    // Start is called before the first frame update
    void Start()
    {
        InitializeFloorSetting(7);
    }

    public void InitializeFloorSetting(float sec)
    {
        floorGetter.SetActive(false);
        StageObject.SetActive(false);
        StartCoroutine("FloorSetting", sec);
    }

    IEnumerator FloorSetting(float sec)
    {
        Debug.Log("[DBG]FloorSetting");
        TapToPlace ttp = transform.Find("Cube").gameObject.GetComponent<TapToPlace>();
        Debug.Log("[DBG]TapToPlace");
        yield return new WaitForSeconds(sec);
        Debug.Log("[DBG]wait for sec");
        floorGetter.SetActive(true);
        Debug.Log("[DBG]Activate");
        yield return new WaitForSeconds(2);
        Debug.Log("[DBG] wait for sec(2)");
        ttp.enabled = false;
        Debug.Log("[DBG]enable");
    }
    public void setFloor()
    {
        Debug.Log("[DBG]setFloor");
        Floor = FloorAnchor.transform.position.y;
        floorGetter.SetActive(false);

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0, Floor, 0);
        plane.GetComponent<Renderer>().enabled = false;
    }

    public void stageSet()
    {
        Debug.Log("[DBG]stageSet");

        StageObject.transform.position = new Vector3(0, Floor, 0);
        StageObject.SetActive(true);
        if (!usePersistentSpatialMesh)
        {
            Debug.Log("[DBG]usePersistentSpatialMesh");

            var access = CoreServices.SpatialAwarenessSystem as IMixedRealityDataProviderAccess;
            var observer = access.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
            // Change the VisivleMaterial to new material.
            observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
            observer.Suspend();
            //dalete SpatialMeshCollider
            GameObject.Find("SpatialAwareness").SetActive(false);
        }

    }
}
