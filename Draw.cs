﻿using System;
using UnityEngine;

namespace ProceduralToolkit
{
    public static class Draw
    {
        public delegate void DebugDrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest);

        private const int circleSegments = 64;
        private const float circleSegmentAngle = 360f/circleSegments;

        private static readonly Func<float, float, Vector3> pointOnCircleXY;
        private static readonly Func<float, float, Vector3> pointOnCircleXZ;
        private static readonly Func<float, float, Vector3> pointOnCircleYZ;

        static Draw()
        {
            pointOnCircleXY = (radius, angle) => PTUtils.PointOnCircle3XY(radius, angle*Mathf.Deg2Rad);
            pointOnCircleXZ = (radius, angle) => PTUtils.PointOnCircle3XZ(radius, angle*Mathf.Deg2Rad);
            pointOnCircleYZ = (radius, angle) => PTUtils.PointOnCircle3YZ(radius, angle*Mathf.Deg2Rad);
        }

        #region WireQuad

        public static void WireQuadXY(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale)
        {
            WireQuad(drawLine, position, rotation, scale, Vector3.right, Vector3.up);
        }

        public static void WireQuadXY(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale,
            Color color,
            float duration,
            bool depthTest)
        {
            WireQuad(drawLine, position, rotation, scale, Vector3.right, Vector3.up, color, duration, depthTest);
        }

        public static void WireQuadXZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale)
        {
            WireQuad(drawLine, position, rotation, scale, Vector3.right, Vector3.forward);
        }

        public static void WireQuadXZ(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale,
            Color color,
            float duration,
            bool depthTest)
        {
            WireQuad(drawLine, position, rotation, scale, Vector3.right, Vector3.forward, color, duration, depthTest);
        }

        public static void WireQuadYZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale)
        {
            WireQuad(drawLine, position, rotation, scale, Vector3.up, Vector3.forward);
        }

        public static void WireQuadYZ(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale,
            Color color,
            float duration,
            bool depthTest)
        {
            WireQuad(drawLine, position, rotation, scale, Vector3.up, Vector3.forward, color, duration, depthTest);
        }

        public static void WireQuad(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale,
            Vector3 planeRight,
            Vector3 planeForward)
        {
            Vector3 right = rotation*planeRight*scale.x;
            Vector3 forward = rotation*planeForward*scale.y;
            Vector3 forwardRight = position + right*0.5f + forward*0.5f;
            Vector3 backRight = forwardRight - forward;
            Vector3 backLeft = backRight - right;
            Vector3 forwardLeft = forwardRight - right;

            drawLine(forwardRight, backRight);
            drawLine(backRight, backLeft);
            drawLine(backLeft, forwardLeft);
            drawLine(forwardLeft, forwardRight);
        }

        public static void WireQuad(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector2 scale,
            Vector3 planeRight,
            Vector3 planeForward,
            Color color,
            float duration,
            bool depthTest)
        {
            Vector3 right = rotation*planeRight*scale.x;
            Vector3 forward = rotation*planeForward*scale.y;
            Vector3 forwardRight = position + right*0.5f + forward*0.5f;
            Vector3 backRight = forwardRight - forward;
            Vector3 backLeft = backRight - right;
            Vector3 forwardLeft = forwardRight - right;

            drawLine(forwardRight, backRight, color, duration, depthTest);
            drawLine(backRight, backLeft, color, duration, depthTest);
            drawLine(backLeft, forwardLeft, color, duration, depthTest);
            drawLine(forwardLeft, forwardRight, color, duration, depthTest);
        }

        #endregion WireQuad

        public static void WireCube(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale)
        {
            Vector3 right = rotation*Vector3.right*scale.x;
            Vector3 up = rotation*Vector3.up*scale.y;
            Vector3 forward = rotation*Vector3.forward*scale.z;

            Vector3 a1 = position + right*0.5f + up*0.5f + forward*0.5f;
            Vector3 b1 = a1 - up;
            Vector3 c1 = b1 - right;
            Vector3 d1 = a1 - right;

            Vector3 a2 = a1 - forward;
            Vector3 b2 = b1 - forward;
            Vector3 c2 = c1 - forward;
            Vector3 d2 = d1 - forward;

            drawLine(a1, b1);
            drawLine(b1, c1);
            drawLine(c1, d1);
            drawLine(d1, a1);

            drawLine(a2, b2);
            drawLine(b2, c2);
            drawLine(c2, d2);
            drawLine(d2, a2);

            drawLine(a1, a2);
            drawLine(b1, b2);
            drawLine(c1, c2);
            drawLine(d1, d2);
        }

        public static void WireCube(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale,
            Color color,
            float duration,
            bool depthTest)
        {
            Vector3 right = rotation*Vector3.right*scale.x;
            Vector3 up = rotation*Vector3.up*scale.y;
            Vector3 forward = rotation*Vector3.forward*scale.z;

            Vector3 a1 = position + right*0.5f + up*0.5f + forward*0.5f;
            Vector3 b1 = a1 - up;
            Vector3 c1 = b1 - right;
            Vector3 d1 = a1 - right;

            Vector3 a2 = a1 - forward;
            Vector3 b2 = b1 - forward;
            Vector3 c2 = c1 - forward;
            Vector3 d2 = d1 - forward;

            drawLine(a1, b1, color, duration, depthTest);
            drawLine(b1, c1, color, duration, depthTest);
            drawLine(c1, d1, color, duration, depthTest);
            drawLine(d1, a1, color, duration, depthTest);

            drawLine(a2, b2, color, duration, depthTest);
            drawLine(b2, c2, color, duration, depthTest);
            drawLine(c2, d2, color, duration, depthTest);
            drawLine(d2, a2, color, duration, depthTest);

            drawLine(a1, a2, color, duration, depthTest);
            drawLine(b1, b2, color, duration, depthTest);
            drawLine(c1, c2, color, duration, depthTest);
            drawLine(d1, d2, color, duration, depthTest);
        }

        #region WireCircleXY

        public static void WireCircleXY(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius)
        {
            WireCircle(pointOnCircleXY, drawLine, position, radius);
        }

        public static void WireCircleXY(
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircle(pointOnCircleXY, drawLine, position, radius, color, duration, depthTest);
        }

        public static void WireCircleXY(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius)
        {
            WireCircle(pointOnCircleXY, drawLine, position, rotation, radius);
        }

        public static void WireCircleXY(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircle(pointOnCircleXY, drawLine, position, rotation, radius, color, duration, depthTest);
        }

        #endregion WireCircleXY

        #region WireCircleXZ

        public static void WireCircleXZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius)
        {
            WireCircle(pointOnCircleXZ, drawLine, position, radius);
        }

        public static void WireCircleXZ(
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircle(pointOnCircleXZ, drawLine, position, radius, color, duration, depthTest);
        }

        public static void WireCircleXZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius)
        {
            WireCircle(pointOnCircleXZ, drawLine, position, rotation, radius);
        }

        public static void WireCircleXZ(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircle(pointOnCircleXZ, drawLine, position, rotation, radius, color, duration, depthTest);
        }

        #endregion WireCircleXZ

        #region WireCircleYZ

        public static void WireCircleYZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius)
        {
            WireCircle(pointOnCircleYZ, drawLine, position, radius);
        }

        public static void WireCircleYZ(
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircle(pointOnCircleYZ, drawLine, position, radius, color, duration, depthTest);
        }

        public static void WireCircleYZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius)
        {
            WireCircle(pointOnCircleYZ, drawLine, position, rotation, radius);
        }

        public static void WireCircleYZ(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircle(pointOnCircleYZ, drawLine, position, rotation, radius, color, duration, depthTest);
        }

        #endregion WireCircleYZ

        #region WireCircle Universal

        public static void WireCircle(
            Func<float, float, Vector3> pointOnCircle,
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius)
        {
            WireArc(pointOnCircle, drawLine, position, radius, 0, circleSegments, circleSegmentAngle);
        }

        public static void WireCircle(
            Func<float, float, Vector3> pointOnCircle,
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircle, drawLine, position, radius, 0, circleSegments, circleSegmentAngle, color, duration,
                depthTest);
        }

        public static void WireCircle(
            Func<float, float, Vector3> pointOnCircle,
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius)
        {
            WireArc(pointOnCircle, drawLine, position, rotation, radius, 0, circleSegments, circleSegmentAngle);
        }

        public static void WireCircle(
            Func<float, float, Vector3> pointOnCircle,
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircle, drawLine, position, rotation, radius, 0, circleSegments, circleSegmentAngle, color,
                duration, depthTest);
        }

        #endregion WireCircle Universal

        #region WireArcXY

        public static void WireArcXY(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle)
        {
            WireArc(pointOnCircleXY, drawLine, position, radius, fromAngle, toAngle);
        }

        public static void WireArcXY(
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircleXY, drawLine, position, radius, fromAngle, toAngle, color, duration, depthTest);
        }

        public static void WireArcXY(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle)
        {
            WireArc(pointOnCircleXY, drawLine, position, rotation, radius, fromAngle, toAngle);
        }

        public static void WireArcXY(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircleXY, drawLine, position, rotation, radius, fromAngle, toAngle, color, duration,
                depthTest);
        }

        #endregion WireCircleXY

        #region WireArcXZ

        public static void WireArcXZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle)
        {
            WireArc(pointOnCircleXZ, drawLine, position, radius, fromAngle, toAngle);
        }

        public static void WireArcXZ(
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircleXZ, drawLine, position, radius, fromAngle, toAngle, color, duration, depthTest);
        }

        public static void WireArcXZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle)
        {
            WireArc(pointOnCircleXZ, drawLine, position, rotation, radius, fromAngle, toAngle);
        }

        public static void WireArcXZ(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircleXZ, drawLine, position, rotation, radius, fromAngle, toAngle, color, duration,
                depthTest);
        }

        #endregion WireCircleXZ

        #region WireArcYZ

        public static void WireArcYZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle)
        {
            WireArc(pointOnCircleYZ, drawLine, position, radius, fromAngle, toAngle);
        }

        public static void WireArcYZ(
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircleYZ, drawLine, position, radius, fromAngle, toAngle, color, duration, depthTest);
        }

        public static void WireArcYZ(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle)
        {
            WireArc(pointOnCircleYZ, drawLine, position, rotation, radius, fromAngle, toAngle);
        }

        public static void WireArcYZ(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            WireArc(pointOnCircleYZ, drawLine, position, rotation, radius, fromAngle, toAngle, color, duration,
                depthTest);
        }

        #endregion WireCircleYZ

        #region WireArc Universal

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle)
        {
            int segments;
            float segmentAngle;
            GetSegmentsAndSegmentAngle(fromAngle, toAngle, out segments, out segmentAngle);

            WireArc(pointOnCircle, drawLine, position, radius, fromAngle, segments, segmentAngle);
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            int segments;
            float segmentAngle;
            GetSegmentsAndSegmentAngle(fromAngle, toAngle, out segments, out segmentAngle);

            WireArc(pointOnCircle, drawLine, position, radius, fromAngle, segments, segmentAngle, color, duration,
                depthTest);
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle)
        {
            int segments;
            float segmentAngle;
            GetSegmentsAndSegmentAngle(fromAngle, toAngle, out segments, out segmentAngle);

            WireArc(pointOnCircle, drawLine, position, rotation, radius, fromAngle, segments, segmentAngle);
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            float toAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            int segments;
            float segmentAngle;
            GetSegmentsAndSegmentAngle(fromAngle, toAngle, out segments, out segmentAngle);

            WireArc(pointOnCircle, drawLine, position, rotation, radius, fromAngle, segments, segmentAngle, color,
                duration, depthTest);
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            int segments,
            float segmentAngle)
        {
            float currentAngle = fromAngle;
            for (var i = 0; i < segments; i++)
            {
                Vector3 a = position + pointOnCircle(radius, currentAngle);
                currentAngle += segmentAngle;
                Vector3 b = position + pointOnCircle(radius, currentAngle);
                drawLine(a, b);
            }
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            DebugDrawLine drawLine,
            Vector3 position,
            float radius,
            float fromAngle,
            int segments,
            float segmentAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            float currentAngle = fromAngle;
            for (var i = 0; i < segments; i++)
            {
                Vector3 a = position + pointOnCircle(radius, currentAngle);
                currentAngle += segmentAngle;
                Vector3 b = position + pointOnCircle(radius, currentAngle);
                drawLine(a, b, color, duration, depthTest);
            }
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            int segments,
            float segmentAngle)
        {
            float currentAngle = fromAngle;
            for (var i = 0; i < segments; i++)
            {
                Vector3 a = position + rotation*pointOnCircle(radius, currentAngle);
                currentAngle += segmentAngle;
                Vector3 b = position + rotation*pointOnCircle(radius, currentAngle);
                drawLine(a, b);
            }
        }

        public static void WireArc(
            Func<float, float, Vector3> pointOnCircle,
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            float fromAngle,
            int segments,
            float segmentAngle,
            Color color,
            float duration,
            bool depthTest)
        {
            float currentAngle = fromAngle;
            for (var i = 0; i < segments; i++)
            {
                Vector3 a = position + rotation*pointOnCircle(radius, currentAngle);
                currentAngle += segmentAngle;
                Vector3 b = position + rotation*pointOnCircle(radius, currentAngle);
                drawLine(a, b, color, duration, depthTest);
            }
        }

        #endregion WireArc Universal

        public static void WireSphere(
            Action<Vector3, Vector3> drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius)
        {
            WireCircleXY(drawLine, position, rotation, radius);
            WireCircleXZ(drawLine, position, rotation, radius);
            WireCircleYZ(drawLine, position, rotation, radius);
        }

        public static void WireSphere(
            DebugDrawLine drawLine,
            Vector3 position,
            Quaternion rotation,
            float radius,
            Color color,
            float duration,
            bool depthTest)
        {
            WireCircleXY(drawLine, position, rotation, radius, color, duration, depthTest);
            WireCircleXZ(drawLine, position, rotation, radius, color, duration, depthTest);
            WireCircleYZ(drawLine, position, rotation, radius, color, duration, depthTest);
        }

        private static void GetSegmentsAndSegmentAngle(
            float fromAngle,
            float toAngle,
            out int segments,
            out float segmentAngle)
        {
            float range = toAngle - fromAngle;
            if (range > circleSegmentAngle)
            {
                segments = Mathf.FloorToInt(range/circleSegmentAngle);
                segmentAngle = range/segments;
            }
            else
            {
                segments = 1;
                segmentAngle = range;
            }
        }
    }
}