using UnityEngine;
[ExecuteAlways]
public class ViewCamera : MonoBehaviour
{
    [SerializeField] Transform pitchTranform;
    [SerializeField] Camera viewCamera;
    [SerializeField] float armLength = 7f;
    [SerializeField] float cameraTurnSpeed = 30f;

    private Transform _parentTransform;
    
    public Camera getViewCamera()
    {
        return viewCamera;
    }
    
    Vector3 GetViewRightDir()
    {
        return viewCamera.transform.right;
    }

    Vector3 GetViewUpDir()
    {
        return Vector3.Cross(GetViewRightDir(), Vector3.up);
    }

    public Vector3 InputToWorldDir(Vector2 input)
    {
        return GetViewRightDir() * input.x + GetViewUpDir() * input.y;
    }


    public void SetFollorParent(Transform parentTransform) 
    { 
        _parentTransform = parentTransform;
    }

    public  void AddYawInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * cameraTurnSpeed);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        viewCamera.transform.position = pitchTranform.position - viewCamera.transform.forward * armLength;
    }

    private void LateUpdate()
    {
        if (_parentTransform != null)
        { 
            transform.position = _parentTransform.position;
        }
    }

}
