using System;

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
    }
}