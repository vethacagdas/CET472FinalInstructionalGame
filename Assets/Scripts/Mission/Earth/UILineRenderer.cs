using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Earth
{
    public class UILineRenderer : Graphic
    {
        public List<Vector2> Points;
        public float Thickness = 10f;
        public bool Center = true;
        public float DrawTime = 0.5f; // Speed of drawing the line

        [ReadOnly] public int CurrentPointIndex; // The current point index being drawn
        private Coroutine _drawCoroutine; // Coroutine for drawing the line
        private EarthMission _earthMission;

        protected override void Awake()
        {
            base.Awake();
            _earthMission = GetComponentInParent<EarthMission>();
        }
        
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            if (Points == null || Points.Count < 2)
                return;

            for (int i = 0; i < CurrentPointIndex - 1; i++)
            {
                // Create a line segment between the next two Points
                CreateLineSegment(Points[i], Points[i + 1], vh);

                int index = i * 5;

                // Add the line segment to the triangles array
                vh.AddTriangle(index, index + 1, index + 3);
                vh.AddTriangle(index + 3, index + 2, index);

                // These two triangles create the beveled edges
                // between line segments using the end point of
                // the last line segment and the start Points of this one
                if (i != 0)
                {
                    vh.AddTriangle(index, index - 1, index - 3);
                    vh.AddTriangle(index + 1, index - 1, index - 2);
                }
            }
        }

        private void CreateLineSegment(Vector3 point1, Vector3 point2, VertexHelper vh)
        {
            Vector3 offset = Center ? (rectTransform.sizeDelta / 2) : Vector2.zero;

            // Create vertex template
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            // Create the start of the segment
            Quaternion point1Rotation = Quaternion.Euler(0, 0, RotatePointTowards(point1, point2) + 90);
            vertex.position = point1Rotation * new Vector3(-Thickness / 2, 0);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);
            vertex.position = point1Rotation * new Vector3(Thickness / 2, 0);
            vertex.position += point1 - offset;
            vh.AddVert(vertex);

            // Create the end of the segment
            Quaternion point2Rotation = Quaternion.Euler(0, 0, RotatePointTowards(point2, point1) - 90);
            vertex.position = point2Rotation * new Vector3(-Thickness / 2, 0);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);
            vertex.position = point2Rotation * new Vector3(Thickness / 2, 0);
            vertex.position += point2 - offset;
            vh.AddVert(vertex);

            // Also add the end point
            vertex.position = point2 - offset;
            vh.AddVert(vertex);
        }

        private float RotatePointTowards(Vector2 vertex, Vector2 target)
        {
            return Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * (180 / Mathf.PI);
        }
        
        public void StartDrawing()
        {
            if (_drawCoroutine != null)
            {
                StopCoroutine(_drawCoroutine);
            }
            _drawCoroutine = StartCoroutine(DrawLine());
        }

        private IEnumerator DrawLine()
        {
            CurrentPointIndex = 0;

            while (CurrentPointIndex < Points.Count)
            {
                CurrentPointIndex++;
                SetVerticesDirty();
                yield return new WaitForSeconds(DrawTime);
            }

            _earthMission.CanAnimateSliders = false;

            yield return new WaitForSeconds(1f);
            
            _earthMission.OnGraphsAnimated();
        }

        public void SetPoints(Vector2[] points) => Points = points.ToList();   
        
        public void ResetPoints()
        {
            Points.Clear();
            CurrentPointIndex = 0;
        }
    }
}