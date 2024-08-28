using UnityEngine;
[ExecuteAlways]
public class ViewCamera : MonoBehaviour
{
    [SerializeField] Transform pitchTranform;
    [SerializeField] Camera viewCamera;
    [SerializeField] float armLength = 7f;

    private Transform _parentTransform;

    public void SetFollorParent(Transform parentTransform) 
    { 
        _parentTransform = parentTransform;
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
