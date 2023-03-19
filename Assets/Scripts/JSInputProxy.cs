using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A proxy class to allow the JavaScript to set the values of the input variables through setter methods.
/// </summary>
public class JSInputProxy : MonoBehaviour
{

    //Kąt względem północy i prętkość kątowa przy włączonej symulacji są od siebie niezależne, np kiedy prętkość kątowa rośnie a kąt maleje, przez co te dwie funkcje się ze sobą kłócą. Lepiej byłoby zrobić to w ten sposób żeby kąt względem pólnocy był tylko wartością wyświetlaną ale żeby użytkownik mógł zmienić tylko prędkość kątową.

    /// <param name="angle">Angle in degrees representing clockwise rotation relative to the north, in range of [0, 355]</param>
    public void SetAngle(float angle)
    {
        Movement.instance.RocketTransform.rotation = Quaternion.RotateTowards(Movement.instance.RocketTransform.rotation, Quaternion.Euler(0, angle-180, 0), Movement.instance.RotationSpeed);
    }
    
    /// <param name="velocity">Angular velocity in Y axis, in range of [-5, 5]</param>
    public void SetAngularVelocity(float velocity)
    {
        Movement.instance.rb.angularVelocity = new Vector3(0, velocity, 0);
    }
    
    /// <param name="x">Linear velocity in X axis (lateral), in range of [-5, 5]</param>
    public void SetVelocityX(float x)
    {
        AddVelocityXZ(true, Movement.instance.DirectionRight.normalized * x, Vector3.zero);
    }
    private Vector3 LastX, LastZ;
    private void AddVelocityXZ(bool IsX, Vector3 x, Vector3 z){
        Vector3 Velocity = Movement.instance.rb.velocity;
        Movement.instance.rb.velocity = new Vector3(0, Velocity.y, 0);
        if(IsX){
            LastX = x;
        }
        if(!IsX){
            LastZ = z;
        }
        Velocity = LastX + LastZ;
        if(Velocity.x > 5f) Velocity.x = 5f;
        if(Velocity.z > 5f) Velocity.z = 5f;
        if(Velocity.x < -5f) Velocity.x = -5f;
        if(Velocity.z < -5f) Velocity.z = -5f;
        Movement.instance.rb.AddForce(Velocity, ForceMode.VelocityChange);
    }
    
    /// <param name="y">Linear velocity in Y axis (vertical), in range of [-5, 5]</param>
    public void SetVelocityY(float y)
    {
        Vector3 VelY = Movement.instance.rb.velocity;
        VelY.y = y;
        Movement.instance.rb.velocity = VelY;
    }
    
    /// <param name="z">Linear velocity in Z axis (longitudinal), in range of [-5, 5]</param>
    public void SetVelocityZ(float z)
    {
        AddVelocityXZ(false, Vector3.zero, Movement.instance.DirectionForward.normalized * z);
    }

    /// <param name="x">Linear acceleration in X axis (lateral), in range of [-5, 5]</param>
    public void SetAccelerationX(float x)
    {
        AccelerationX = x;
    }
    
    /// <param name="y">Linear acceleration in Y axis (vertical), in range of [-5, 5]</param>
    public void SetAccelerationY(float y)
    {
        AccelerationY = y;
    }
    
    /// <param name="z">Linear acceleration in Z axis (longitudinal), in range of [-5, 5]</param>
    public void SetAccelerationZ(float z)
    {
        AccelerationZ = z;
    }
    private float AccelerationX, AccelerationY, AccelerationZ;
    private Vector3 AccelerationVector;
    void FixedUpdate(){
        AccelerationVector = Movement.instance.DirectionForward.normalized * AccelerationZ + Vector3.up * AccelerationY + Movement.instance.DirectionRight.normalized * AccelerationX;
        Movement.instance.rb.AddForce(AccelerationVector, ForceMode.Acceleration);
    }
}
