using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Obstacles
{
    public class VerticalMovement : MonoBehaviour, IMovement
    {
        public float speed = 3f;
        private Vector3 startPos;
        public float distance;
        public bool movingUp;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        public void Move()
        {
            if (movingUp)
            {
                float boundary = startPos.y + distance;
                
                if (transform.position.y >= boundary)
                {
                    movingUp = false;
                }    
                
                transform.Translate(Vector3.up * (Time.deltaTime * speed));
                
            }
            else
            {
                float boundary = startPos.y - distance;
                
                if (transform.position.y <= boundary)
                {
                    movingUp = true;
                }       
                
                transform.Translate(Vector3.down * (Time.deltaTime * speed));
            }
            
        }
        
        private void MoveObstacletoStartPosition(GameObject obstacle)
        {
            obstacle.gameObject.SetActive(false);
            obstacle.transform.position = new Vector3(startPos.x, startPos.y);
            obstacle.gameObject.SetActive(true);
        }
        
    }
}