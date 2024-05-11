using UnityEngine;

public class BackgroundCloudMove : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 repeatWidth;
    private Vector3 backgroundPos;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        var parentSky = gameObject.transform.parent.gameObject;
        repeatWidth = parentSky.GetComponent<Renderer>().bounds.size / 2;
        backgroundPos = parentSky.transform.parent.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < -repeatWidth.x + backgroundPos.x)
        {
            transform.position = new Vector3(repeatWidth.x + backgroundPos.x, startPos.y, startPos.z);
        }
    }
}
