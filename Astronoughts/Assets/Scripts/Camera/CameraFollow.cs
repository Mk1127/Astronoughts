using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public enum CameraFollowType
    {
        Follow,
        IndependentRotation,
        TopDown,
        Track,
        TrackRotation
    }

    public CameraFollowType CameraType = CameraFollowType.Track;
    public Transform target;
    public float X;
    public float Y;
    public float Z;
    public float aX;
    public float aY;
    public float aZ;

    public float smoothSpeed = 0.125f;
    private Vector3 offset;
    private Vector3 angleOffset;
    // smooth camera changes (pos & rot).
    public float damping = 1.0f;

    // Independent rot
    public float rotationSpeed = 5.0f;
    private float angleX = 0.0f;
    public float minPitch = -30.0f;
    public float maxPitch = 30.0f;

    void LateUpdate () 
    {
        switch(CameraType)
        {
            case CameraFollowType.Track:
                {
                    offset = new Vector3(X,Y,Z);
                    angleOffset = new Vector3(aX,aY,aZ);
                    Track();
                    break;
                }
            case CameraFollowType.Follow:
                {
                    offset = new Vector3(X,Y,Z);
                    angleOffset = new Vector3(aX,aY,aZ);
                    Follow();
                    break;
                }
            case CameraFollowType.TrackRotation:
                {
                    offset = new Vector3(X,Y,Z);
                    angleOffset = new Vector3(aX,aY,aZ);
                    // refactored to not allow rotational tracking.
                    Follow(true);
                    break;
                }
            case CameraFollowType.IndependentRotation:
                {
                    offset = new Vector3(X,Y,Z);
                    angleOffset = new Vector3(aX,aY,aZ);
                    // refactored to not allow rotational tracking.
                    RotateIndependently();
                    break;
                }
            case CameraFollowType.TopDown:
                {
                    offset = new Vector3(0.0f,50.0f,0f);
                    angleOffset = new Vector3(0.0f,0.0f,0.0f);
                    TopDown();
                    break;
                }
        }

    }

    // Follow camera
    void Follow(bool allowTracking = false)
    {
        // initial camera rotation
        Quaternion initRotation = Quaternion.Euler(angleOffset);

        // camera rotation follows player rotation
        if(allowTracking)
        {
            Quaternion rot = Quaternion.Lerp(transform.rotation,
                target.rotation * initRotation,
                Time.deltaTime * damping);

            transform.rotation = rot;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(
              transform.rotation,
              initRotation,
              damping * Time.deltaTime);
        }

        // Where is the camera's transform
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;
        Vector3 up = transform.rotation * Vector3.up;

        // Offset camera
        Vector3 targetPos = target.position;
        Vector3 desiredPosition = targetPos
            + forward * offset.z
            + right * offset.x
            + up * offset.y;

        // Lerp the position of the camera
        Vector3 position = Vector3.Lerp(transform.position,
            desiredPosition,
            Time.deltaTime * damping);

        transform.position = position;
    }

    // Rotating camera
    void RotateIndependently()
    {
        float mouseX, mouseY;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // initial camera rotation
        Quaternion initRotation = Quaternion.Euler(angleOffset);

        Vector3 eulerAngle = transform.rotation.eulerAngles;

        angleX -= mouseY * rotationSpeed;

        //clamp X-Axis angle between min and max
        angleX = Mathf.Clamp(angleX,minPitch,maxPitch);

        eulerAngle.y += mouseX * rotationSpeed;
        Quaternion newRot = Quaternion.Euler(angleX,eulerAngle.y,0.0f) *
          initRotation;

        transform.rotation = newRot;

        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;
        Vector3 up = transform.rotation * Vector3.up;

        Vector3 targetPos = target.position;
        Vector3 newPosition = targetPos
            + forward * offset.z
            + right * offset.x
            + up * offset.y;

        Vector3 position = Vector3.Lerp(transform.position,
            newPosition,
            Time.deltaTime * damping);
        transform.position = position;
    }

    // Simplest type
    void TopDown()
    {
        transform.position = target.position + offset;
    }

    // Simple tracking camera
    void Track()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y += offset.y;
        transform.LookAt(targetPos);
    }


}
