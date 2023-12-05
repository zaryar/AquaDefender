using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    private List<List<Vector3>> camper_player = new List<List<Vector3>>()
    {
        new List<Vector3>() { new Vector3(28.84362f, 5.20334f, 25.49818f), new Vector3(27.29693f, 4.896906f, 25.13505f),  new Vector3(26.05847f, 4.967204f, 24.14018f), new Vector3(25.54025f, 4.88892f, 22.51293f) },
        new List<Vector3>() { new Vector3(25.72059f, 5.660317f, 19.93841f), new Vector3(27.61168f, 5788954f, 18.06712f),  new Vector3(30.22423f, 5.89237f, 17.83356f) },
        new List<Vector3>() { new Vector3(31.63799f, 6.014416f, 18.57949f), new Vector3(46.2789f, 0.8519389f, 8.131048f),  new Vector3(32.36781f, 5.611812f, 22.53408f) },
    };

    public List<Vector3> Getcampertrajectory(int index)
    {
        return camper_player[index];
    }
}
