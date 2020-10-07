using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoatGame
{

    public class Game : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject[] asteroid;

        public GameObject cloud;

        public GameObject[] debris;

        public GameObject star;

        private int level;

        public int stage;

        void Start()
        {
            Camera.main.aspect = 2960f / 1440f;

            InvokeRepeating("SpawnAsteroid", 1f, 0.8f);
            InvokeRepeating("SpawnCloud", 0f, 0.5f);
            InvokeRepeating("SpawnDebris", 0f, 0.5f);
            InvokeRepeating("SpawnStar", 0f, 1f);

            level = 2;
            stage = 1;  
        }

        // Update is called once per frame
        void Update()
        {
            SetUIStage();
        }


        void SpawnObject(GameObject prefab, float min_Y, float max_Y, float x , float z, int randomSpawn)
        {
            randomSpawn = Random.Range(0, randomSpawn);

            if (randomSpawn == 1)
            {
                GameObject newObject = Instantiate(prefab);
                float y_rand = Random.Range(min_Y, max_Y);
                newObject.transform.position = new Vector3(x, y_rand,z);
                //listCloudInTerrain.Add(newCloud);
            }
        }
        void SpawnListObject(GameObject[] prefab, float min_Y, float max_Y, float x, float z, int randomSpaw)
        {
            SpawnObject(prefab[Random.Range(0, prefab.Length - 1)],min_Y,max_Y,x,z,randomSpaw);
        }

        void SpawnAsteroid()
        {
            SpawnListObject(asteroid,-6.5f,3.65f,10,1,level);
        }

        void SpawnCloud()
        {
            SpawnObject(cloud, -5.45f, 2.5f, 10, 1, level);
            // stage -1
            SpawnObject(cloud, -8f, -16f, 10, 1, level);
            // stage +1
            SpawnObject(cloud, 6f, 14f, 10, 1, level);
            // stage -2
   /*         SpawnObject(cloud, -18f, -26f, 10, 1, level);;
            // stage +2
            SpawnObject(cloud, 16f, 24f, 10, 1, level);*/
        }

        void SpawnDebris()
        {
            SpawnListObject(debris, -4f, 2f, 6, 3, level); 

        }
        
        private void SpawnStar()
        {
            SpawnObject(star, -5.45f, 2.5f, 10, 1, level);
        }

        private void SetUIStage()
        {
            GameObject.Find("stage_var").gameObject.GetComponent<Text>().text = stage.ToString();
        }

    }
}