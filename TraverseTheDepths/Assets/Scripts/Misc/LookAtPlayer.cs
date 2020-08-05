using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] GameObject head = null;

    [SerializeField] public bool LookAt;
    public float LookAtDistance = 15.0f;
    [SerializeField] public float YAxisClamp = 80;
    [SerializeField] public float XAxisClamp = 45;
    [SerializeField] public float ZAxisClamp = 45;
    Vector3 defaultLook;

    PlayerMove pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerMove>();
        defaultLook = head.transform.forward;
    }

    private void LateUpdate()
    {
        if (LookAt && Vector3.Distance(pm.transform.position, head.transform.position) < LookAtDistance)
        {

            Vector3 direction = pm.transform.position - head.transform.position;
            head.transform.rotation =  Quaternion.RotateTowards(head.transform.rotation, Quaternion.LookRotation(direction), 5);
            //head.transform.LookAt(pm.transform.position);
            Vector3 rot = head.transform.localEulerAngles;
            //rot.x = lookVertical;
            if (rot.y > 180)
            {
                rot.y -= 360;
            }
            rot.y = Mathf.Clamp(rot.y, -YAxisClamp, YAxisClamp);
            if (rot.x > 180)
            {
                rot.x -= 360;
            }
            rot.x = Mathf.Clamp(rot.x, -XAxisClamp, XAxisClamp);
            if (rot.z > 180)
            {
                rot.z -= 360;
            }
            rot.z = Mathf.Clamp(rot.z, -ZAxisClamp, ZAxisClamp);
            // Debug.Log(rot.y);

            head.transform.localEulerAngles = rot;
        } else
        {
            head.transform.rotation =  Quaternion.RotateTowards(head.transform.rotation, Quaternion.LookRotation(defaultLook), 5);
        }

    }
}
