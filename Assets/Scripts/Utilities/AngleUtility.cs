using System;
using UnityEngine;

namespace Utilities {
    public static class AngleUtility {
        public static float FindAngle(double x, double y){
            double value = (Math.Atan2(x, y) / Math.PI) * 180;
            if(value < 0d) value += 360d;
            return (float)value;
        }

        public static float FlipDrirection180(float direction){
            direction += 180;
            if(direction > 360) direction -= 360;
            return direction;
        }

        public static Vector2 FindDirectionBetween2Points(Vector2 pointA, Vector2 pointB){
            Vector2 direction = Vector2.zero;
            float angle = FindAngle(pointA.y - pointB.y, pointA.x - pointB.x);

            if (angle > 315.1 || angle < 45) {
                direction = Vector2.right;
            } else if (angle > 45.1 && angle < 135) {
                direction = Vector2.up;
            } else if (angle > 135.1 && angle < 225) {
                direction = Vector2.left;
            } else if (angle > 225.1 && angle < 315) {
                direction = Vector2.down;
            }

            return direction;
        }
    }
}