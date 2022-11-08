using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoint = new List<Transform>();
    public int Length { get => patrolPoint.Count; }

    [Header("Gizmos parameters")]
    public Color pointsColor = Color.blue;
    public Color lineColor = Color.magenta;
    public float pointSize = 1;

    public struct PathPoint{
        public int Index;
        public Vector2 Position;
    }
    public PathPoint GetClosestPathPoint(Vector3 tankPosition){
        var minDistance = float.MaxValue;
        var index = -1;
        for (int i = 0; i < patrolPoint.Count; i++){
            var tempDistance = Vector2.Distance(tankPosition, patrolPoint[i].position);
            if (tempDistance < minDistance){
                minDistance = tempDistance;
                index = i;
            }
        }
        return new PathPoint { Index = index, Position = patrolPoint[index].position };
    }
    public PathPoint GetNextPathPoint(int index){
        var newIndex = index + 1 >= patrolPoint.Count ? 0 : index + 1;
        return new PathPoint { Index = newIndex, Position = patrolPoint[newIndex].position };
    }

    private void OnDrawGizmos(){
        if (patrolPoint.Count == 0)
            return;
        for (int i = patrolPoint.Count-1; i >= 0; i--){
            if (i == -1 || patrolPoint[i] == null)
                return;
            Gizmos.color = pointsColor;
            Gizmos.DrawSphere(patrolPoint[i].position, pointSize);
            
            if (patrolPoint.Count == 1 || i == 0)
                return;
            Gizmos.color = lineColor;
            Gizmos.DrawLine(patrolPoint[i].position, patrolPoint[i - 1].position);

            if (patrolPoint.Count > 2 && i == patrolPoint.Count - 1){
                Gizmos.DrawLine(patrolPoint[i].position, patrolPoint[0].position);
            }
        }
    }
}
