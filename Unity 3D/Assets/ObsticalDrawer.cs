using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalDrawer : MonoBehaviour
{
    public GameObject PrefabObstacle;
    public DistanceComReader TheReader;
    int currentIndex = 0;
    Dictionary<int, GameObject> Spawns = new Dictionary<int, GameObject>();

    public GameObject LiveCube;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angle = transform.eulerAngles;
        angle.y = -currentIndex;
        transform.eulerAngles = angle;
        if (currentIndex > TheReader.TheObsticalWorld.AllObstacles.Count)
        {
            currentIndex = 0;
        }
        else
        {
            DistanceComReader.Obstical anObs = null;
            if (TheReader.TheObsticalWorld.AllObstacles.ContainsKey(currentIndex))
            {
                anObs = TheReader.TheObsticalWorld.AllObstacles[currentIndex];
                Vector3 loc = LiveCube.gameObject.transform.localPosition;
                loc.z = anObs.Distance;
                LiveCube.gameObject.transform.localPosition = loc;
                if (Spawns.ContainsKey(currentIndex))
                {
                    GameObject anObj = Spawns[currentIndex];

                    Vector3 loc2 = LiveCube.gameObject.transform.position;// LiveCube.gameObject.transform.localPosition;
                    //loc.z = anObs.Distance;
                    anObj.transform.position = loc2;
                }
                else
                {
                    GameObject anObj = SpawnNew(anObs.Angle);
                    Spawns.Add(anObs.Angle, anObj);
                    Vector3 loc2 = LiveCube.gameObject.transform.position;// LiveCube.gameObject.transform.localPosition;
                    //loc.z = anObs.Distance;
                    anObj.transform.position = loc2;
                }
            }
        }
        currentIndex++;
    }
    private GameObject SpawnNew(int angle)
    {
        GameObject anObj = GameObject.Instantiate(PrefabObstacle);
        anObj.name = "A" + angle;
        return anObj;
    }
}
