using UnityEngine;

namespace Mission.Earth
{
    [CreateAssetMenu(fileName = "PointsData_", menuName = "Data/Points Data")]
    public class PointsData : ScriptableObject
    {
        public Vector2[] Points;
    }
}