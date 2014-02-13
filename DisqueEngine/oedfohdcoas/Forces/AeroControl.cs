using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Forces
{
    public class AeroControl : Aero
    {
        public Matrix3 MaxTensor { get; set; }
        public Matrix3 MinTensor { get; set; }
        public float ControlSetting { get; set; }
        public AeroControl(Matrix3 mbase, Matrix3 mintens, Matrix3 maxtens, Vector3 position, Vector3 windspeed)
            : base(mbase, position, windspeed)
        {
            MaxTensor = maxtens;
            MinTensor = mintens;
            ControlSetting = 0.0f;
        }
        Matrix3 getTensor()
        {
            if (ControlSetting <= -1.0f) return MinTensor;
            else if (ControlSetting >= 1.0f) return MaxTensor;
            else if (ControlSetting < 0)
            {
                return Matrix3.LinearInterpolate(MinTensor, Tensor, ControlSetting + 1.0f);
            }
            else if (ControlSetting > 0)
            {
                return Matrix3.LinearInterpolate(Tensor, MaxTensor, ControlSetting);
            }
            else return Tensor;
        }
        public override void UpdateForce(RigidBody body, float duration)
        {
            Matrix3 tensor = getTensor();
            base.updateforcefromtensor(body, duration, tensor);
        }
    }
}
