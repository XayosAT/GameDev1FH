using UnityEngine;

namespace Obstacles
{
    public class VerticalMovement : MonoBehaviour, IMovement
    {
        public float speed = 3f;
        private readonly float yBoundarydestroy = 9.5f;
        private readonly float yBoundaryinstantiate = -6.5f;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        public void Move()
        {
            if (transform.position.y >= yBoundarydestroy)
            {
                InstantiateObstacle();
                Destroy(gameObject);
            }    
            
            transform.Translate(Vector3.up * (Time.deltaTime * speed));
        }
        
        private void InstantiateObstacle()
        {
            var vertiplatform = Instantiate(gameObject, new Vector3(137, yBoundaryinstantiate, 0), Quaternion.identity);
            vertiplatform.name = "Grey Off";
        }
        
    }
}

