using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GyroPoolManager : MonoBehaviour
{
    public static GyroPoolManager poolManager;

    #region Variables

    //  The name of the pool to be created
    public string poolName;
   
    // Object Prefab of the pool to be created
    public GameObject objectPrefab;
    
    // Prespawn Amount of the pool to be created
    public int prespawnAmount;
    
    // Checks if the pool is empty
    public bool isPoolEmpty;

    // Check if there is a pool with this ID
    public bool wrongID;

    // Check if there is a pool with this Name
    public bool wrongName;

    // A list that holds whether autospawn is on
    public List<bool> autoSpawnActivated = new List<bool>();
  
    // Current Tab of the PoolManager
    public string currentTab;
    public int toolbarTab;

    // A list that holds  objects pools
    public List<ObjectPools> objectPools = new List<ObjectPools>();


    // AutoSpawner Class
    [System.Serializable]

    public class AutoSpawner
    {

        // Checks if auto spawn is enabled
        public bool autoSpawn;

        // Checks if auto spawn is started
        public bool autoSpawnActive;

        // start Autospawn 
        public bool startAutoSpawnButton;

        // Checks if deviation is enabled
        public bool deviation;

        // Vector3 for devation Position,Rotation,Scale
        public Vector3 deviationPositionVector;
        public Vector3 deviationRotationVector;
        public Vector3 deviationScaleVector;

        // Time between spawns
        public float spawnInterval=1;

        // Spawn Methods Single or Mutible
        public enum SpawnPosition { InOrder, Random };
        public enum SpawnMethods { Single, Multiple};
        public SpawnMethods spawnMethod;
        public SpawnPosition spawnPosition;

        // a List that holds SpawnPoints 
        public List<Transform> SpawnPoints = new List<Transform>();

        // a List that holds AutoSpawned Objects
        public List<GameObject> autoSpawnedObjects;
     
        // Despawner 
        public DeSpawner deSpawning;
        
    }

    [System.Serializable]
   public class DeSpawner
    {
        // lifeTime Amount for Pooled Objects
        public float lifetimeAmount;

        // Despawn Interval
        public float despawnInterval;

        public bool despawnIntervalActive;

        // Size for fixedAmount
        public int fixedAmount;


    }
    #endregion

    #region ObjectPoolClass


    //  Class of ObjectPool
    [System.Serializable]

    public class ObjectPools
    {
        // Pool Name
        public string poolName;

        // Prespawn Amount
        public int poolSize;

        // number of active objects in the pool 
        public int activeInsances;

        // Check if Debug Warning enabled
        public bool debugWarnings;

        // Object Prefab
        public GameObject objectPrefab;

        // AutoSpawner
        public AutoSpawner autoSpawner;



        // Pool size Options Flexible or not
        public enum PoolSizeOptions { FixedSize,FlexibleSize};
        public PoolSizeOptions poolSizeOptions;

        // DeSpawn Options
        public enum DeSpawnOptions { none, fixedAmount, lifetime, DespawnInterval };
        public DeSpawnOptions deSpawnOptions;

        // A list that holds objects in pool
        public List<GameObject> pooledObjects =new List<GameObject>();

        // Spawned Object counts after size limit
        public int extraInstantiate;




        public ObjectPools(string p_poolName,int p_poolSize, GameObject p_objectPrefab)
        {
            poolName = p_poolName;
         
            poolSize = p_poolSize;
            
            objectPrefab = p_objectPrefab;
        }


    }
    #endregion
    
    #region Create New Pool
    // CreatesPool function 
    public void CreatePool()
    {
        // Creates new pool with user inputs
        objectPools.Add(new ObjectPools(poolName, prespawnAmount, objectPrefab));

       
    }
    #endregion

    private void Awake()
    {
        poolManager = this;
        FillPool();
    }

    void Start()
    {
       
        // Makes sure there are no active autospawns
        for (int i = 0; i < autoSpawnActivated.Count; i++)
        {
            autoSpawnActivated[i] = false;
           
           
        }

        for (int i = 0; i < objectPools.Count; i++)
        {
            // Check if the pool exists
            if (objectPools[i] != null)
            {
                // Pools that need to be Auto Spawn are sent to the Function
                if (objectPools[i].autoSpawner.startAutoSpawnButton)
                {
                    objectPools[i].autoSpawner.autoSpawnActive = false;

                    StartAutoSpawner(i);

                }
            }


        }

    }

    // A Method for Start Auto Spawner 
    public void StartAutoSpawner(int _poolID)
    {

        // Checks if it is on
        if (objectPools[_poolID].autoSpawner.autoSpawnActive)
        {
           
            return;
        }

        // to understand that it is open
        objectPools[_poolID].autoSpawner.autoSpawnActive = true;
        
        // Sends poolID to AutoSpawn
        AutoSpawn(_poolID);
      

    }

    // A Method for Stop Auto Spawner 
    public void StopAutoSpawner(int _poolID)
    {

        // to understand that it is closed
        if (objectPools.Count > _poolID)
        {
            objectPools[_poolID].autoSpawner.autoSpawnActive = false;
        }


        // to understand that it is closed
        if (autoSpawnActivated.Count > _poolID)
        {
            autoSpawnActivated[_poolID] = false;
        }

    }

    #region Fill The Pools 

    // Create objects of pool 
    public void FillPool()
    {
        // A loop that runs until count of pools
        for (int j = 0; j < objectPools.Count; j++)
        {
            // A loop that runs until prespawn amount of pools
            for (int i = 0; i < objectPools[j].poolSize; i++)
            {
                // Instantiate the prefab
                GameObject obj = Instantiate(objectPools[j].objectPrefab);

                // Disable the object
                obj.SetActive(false);

                // Set pool manager as parent
                obj.transform.SetParent(transform);

                // Add object to  pooledObjects list
                objectPools[j].pooledObjects.Add(obj);

            }
        }

    }
    #endregion

    #region AutoSpawner



    // Auto Spawn Function takes PoolID
    public void AutoSpawn(int _poolID)
        {

            // Fills the missing elements in the list
            for (int i = autoSpawnActivated.Count; i < _poolID + 1; i++)
            {
                autoSpawnActivated.Add(false);

            }

            // Sends previously inactive ones to AutoSpawnRutine
            if (!autoSpawnActivated[_poolID])
            {
                autoSpawnActivated[_poolID] = true;

                StartCoroutine(AutoSpawnRoutine(objectPools[_poolID].autoSpawner.spawnInterval, _poolID, 0));
            }

        }
    

   public IEnumerator AutoSpawnRoutine(float time, int _poolID, int point)
    {

        // For those who have Single Spawn Method
        if (objectPools[_poolID].autoSpawner.spawnMethod == AutoSpawner.SpawnMethods.Single && objectPools[_poolID].autoSpawner.autoSpawnActive && objectPools[_poolID].autoSpawner.autoSpawn)
        {
            // Pull with ID in Pool
            GameObject obj = Pull(_poolID);

            // if the pulled  object is not empty
            if (obj != null)
            {
                // if the deviation is not active 
                if (!objectPools[_poolID].autoSpawner.deviation)
                {
                    // The position of the object pulled  from the pool is synchronized with spawn points
                    obj.transform.position = objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position;

                    // The rotation of the object pulled  from the pool is synchronized with spawn points
                    obj.transform.rotation = objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation;

                    obj.transform.localScale = objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale;
                }

                // if the deviation is active 
               else if (objectPools[_poolID].autoSpawner.deviation)
                {


                    // The position of the object pulled from the pool is synchronized with  set random deviation
                    obj.transform.position = new Vector3
                        (
                       
                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position.x + Random.Range(0,objectPools[_poolID].autoSpawner.deviationPositionVector.x),

                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position.y + Random.Range(0, objectPools[_poolID].autoSpawner.deviationPositionVector.y),

                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position.z + Random.Range(0, objectPools[_poolID].autoSpawner.deviationPositionVector.z)

                        );


                    // The rotation of the object pulled from the pool is synchronized with  set random deviation
                    obj.transform.rotation =    Quaternion.Euler( new Vector3
                        (
                      
                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation.x + Random.Range(0,objectPools[_poolID].autoSpawner.deviationRotationVector.x),

                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation.y + Random.Range(0, objectPools[_poolID].autoSpawner.deviationRotationVector.y),

                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation.z + Random.Range(0, objectPools[_poolID].autoSpawner.deviationRotationVector.z)

                    
                        ));

                    // The localscale of the object pulled from the pool is synchronized with  set random deviation
                    obj.transform.localScale = new Vector3
                     (
                    objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale.x + Random.Range(0,objectPools[_poolID].autoSpawner.deviationScaleVector.x),

                    objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale.y + Random.Range(0, objectPools[_poolID].autoSpawner.deviationScaleVector.y),

                    objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale.z + Random.Range(0, objectPools[_poolID].autoSpawner.deviationScaleVector.z)

                     );

                    
                }


            }
          
            yield return new WaitForSeconds(time);
        
            if(objectPools[_poolID].autoSpawner.spawnPosition == AutoSpawner.SpawnPosition.InOrder)
            {
                StartCoroutine(AutoSpawnRoutine(objectPools[_poolID].autoSpawner.spawnInterval, _poolID, (point + 1) % objectPools[_poolID].autoSpawner.SpawnPoints.Count));

            }

            else
            {
               
                StartCoroutine(AutoSpawnRoutine(objectPools[_poolID].autoSpawner.spawnInterval, _poolID, Random.Range(0, objectPools[_poolID].autoSpawner.SpawnPoints.Count)));
            }
        }

        // For those who have Mutible Spawn Method
         else if (objectPools[_poolID].autoSpawner.spawnMethod == AutoSpawner.SpawnMethods.Multiple && objectPools[_poolID].autoSpawner.autoSpawnActive && objectPools[_poolID].autoSpawner.autoSpawn)
        {
            for (int i = 0; i < objectPools[_poolID].autoSpawner.SpawnPoints.Count; i++)
            {
                // Pull with ID in Pool
                GameObject obj = Pull(_poolID);

                // if the pulled  object is not empty
                if (obj != null)
                {
                     // if the deviation is not active
                    if (!objectPools[_poolID].autoSpawner.deviation)
                    {
                        // The position of the object pulled  from the pool is synchronized with spawn points
                        obj.transform.position = objectPools[_poolID].autoSpawner.SpawnPoints[i].transform.position;

                        // The rotation of the object pulled  from the pool is synchronized with spawn points
                        obj.transform.rotation = objectPools[_poolID].autoSpawner.SpawnPoints[i].transform.rotation;
                    }


                    // if the deviation is active
                    else if (objectPools[_poolID].autoSpawner.deviation)
                    {


                        // The position of the object pulled from the pool is synchronized with  set random deviation
                        obj.transform.position = new Vector3
                            (

                            objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position.x + Random.Range(0, objectPools[_poolID].autoSpawner.deviationPositionVector.x),

                            objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position.y + Random.Range(0, objectPools[_poolID].autoSpawner.deviationPositionVector.y),

                            objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.position.z + Random.Range(0, objectPools[_poolID].autoSpawner.deviationPositionVector.z)

                            );


                        // The rotation of the object pulled from the pool is synchronized with  set random deviation
                        obj.transform.rotation = Quaternion.Euler(new Vector3
                            (

                            objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation.x + Random.Range(0, objectPools[_poolID].autoSpawner.deviationRotationVector.x),

                            objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation.y + Random.Range(0, objectPools[_poolID].autoSpawner.deviationRotationVector.y),

                            objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.rotation.z + Random.Range(0, objectPools[_poolID].autoSpawner.deviationRotationVector.z)


                            ));

                        // The localscale of the object pulled from the pool is synchronized with  set random deviation
                        obj.transform.localScale = new Vector3
                         (
                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale.x + Random.Range(0, objectPools[_poolID].autoSpawner.deviationScaleVector.x),

                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale.y + Random.Range(0, objectPools[_poolID].autoSpawner.deviationScaleVector.y),

                        objectPools[_poolID].autoSpawner.SpawnPoints[point].transform.localScale.z + Random.Range(0, objectPools[_poolID].autoSpawner.deviationScaleVector.z)

                         );


                    }


                }

                

            }


            yield return new WaitForSeconds(time);

            StartCoroutine(AutoSpawnRoutine(objectPools[_poolID].autoSpawner.spawnInterval, _poolID, 0));

        }


          

       
    }





    #endregion

    #region DeSpawn
    // Despawn Function take PoolID and despawnObject
    void DeSpawn(int poolID, GameObject returnGameObject)
    {
        // if DeSpawn is active  start corutine lifeTime
        if (objectPools[poolID].deSpawnOptions== ObjectPools.DeSpawnOptions.lifetime)
        {
            StartCoroutine(LifetimeAmount(objectPools[poolID].autoSpawner.deSpawning.lifetimeAmount, poolID, returnGameObject));
        }

        // if DeSpawn is active  start corutine DespawnInterval
        else if (objectPools[poolID].deSpawnOptions == ObjectPools.DeSpawnOptions.DespawnInterval)
        {

            
          

            if (!objectPools[poolID].autoSpawner.deSpawning.despawnIntervalActive)
            {
                objectPools[poolID].autoSpawner.deSpawning.despawnIntervalActive = true;
               
                StartCoroutine(DespawnInterval(objectPools[poolID].autoSpawner.deSpawning.despawnInterval, poolID));

            }
        }




        //if (!gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnIntervalActive)
        //{
        //    gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnIntervalActive = true;
        //    gyroPoolManager.StartCoroutine(gyroPoolManager.DespawnInterval(gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnInterval, poolID));
        //}


    }
    // A Corutine takes time,poolid,and despawnObject
    IEnumerator LifetimeAmount(float time, int _poolID, GameObject obj)
    {

        yield return new WaitForSeconds(time);

        //The object that will be despawn is pushed back into the pool after the time.
        Push(_poolID, obj);
    }

     public IEnumerator DespawnInterval(float time, int _poolID)
    {
        

        yield return new WaitForSeconds(time);



        //The object that will be despawn is pushed back into the pool after the time.

        if (objectPools[_poolID].autoSpawner.autoSpawnedObjects.Count > 0)
        {
            Push(_poolID, objectPools[_poolID].autoSpawner.autoSpawnedObjects[0]);

            // Remove the object in autoSpawnedObjects list
            objectPools[_poolID].autoSpawner.autoSpawnedObjects.RemoveAt(0);

            if (objectPools[_poolID].deSpawnOptions == ObjectPools.DeSpawnOptions.DespawnInterval)
            {
                //when time is up, push
                StartCoroutine(DespawnInterval(objectPools[_poolID].autoSpawner.deSpawning.despawnInterval, _poolID));

            }

            else
            {

                objectPools[_poolID].autoSpawner.deSpawning.despawnIntervalActive = false;
            }

        }
        else
        {
            objectPools[_poolID].autoSpawner.deSpawning.despawnIntervalActive = false;
        }

       
    }
    #endregion

    #region Fixed Size
    // FixedSize function takes PollID and returnGameObject
    void FixedSize(int poolID)
    {
        // if FixedSize is active
        if (objectPools[poolID].deSpawnOptions == ObjectPools.DeSpawnOptions.fixedAmount)
        {
            
            //if the number of spawned objects is greater than the fixed size, push and removeat(0)
            for (int i = objectPools[poolID].autoSpawner.deSpawning.fixedAmount; i < objectPools[poolID].autoSpawner.autoSpawnedObjects.Count+1; i++)
            {
                //if the number of spawned objects is greater than the fixed size, push
                Push(poolID, objectPools[poolID].autoSpawner.autoSpawnedObjects[0]);

                // Remove the object in autoSpawnedObjects list
                objectPools[poolID].autoSpawner.autoSpawnedObjects.RemoveAt(0);
            }

        }
    }
    #endregion


    // Pool Methods

    #region Pull with poolID
    public GameObject Pull(int poolID)
    {


        // Checks if there is a pool at that poolID
        if (poolID >= objectPools.Count)
        {
            if (objectPools[poolID].debugWarnings)
                Debug.Log("There are no " + poolID + "pools here");
            return null;
        }

        // Returns the gameobject from pool 
        if (objectPools[poolID].pooledObjects.Count >= 1)
        {

            //Synchronize the object to be converted back to the first object of the pool list
            GameObject returnGameObject = objectPools[poolID].pooledObjects[0];

            // Enable  Object
            objectPools[poolID].pooledObjects[0].SetActive(true);

            // Remove from pooledObjects list
            objectPools[poolID].pooledObjects.RemoveAt(0);

            // Send the Onject to Despawn
            DeSpawn(poolID, returnGameObject);

            // Send the Onject to FixedSize
            FixedSize(poolID);

            // returnGameObject added to autoSpawnedObjects list
            objectPools[poolID].autoSpawner.autoSpawnedObjects.Add(returnGameObject);

            // Control all spawned objects are active 
            for (int j = 0; j < objectPools[poolID].autoSpawner.autoSpawnedObjects.Count; j++)
            {


                if (!objectPools[poolID].autoSpawner.autoSpawnedObjects[j].activeSelf)
                {


                    objectPools[poolID].autoSpawner.autoSpawnedObjects.RemoveAt(j);

                }

            }

            // Increase the number of active objects in the pool 
            objectPools[poolID].activeInsances++;

            // Returns the pooled object
            return returnGameObject;


        }

        // Returns the gameobject after intantiate (Flexible Pool)
        else if ((objectPools[poolID].poolSizeOptions == ObjectPools.PoolSizeOptions.FlexibleSize))
        {

            // A error message for wrong Pooll ID
            if (objectPools[poolID].debugWarnings)
            {
                Debug.Log("Instantiated because pool is empty");

            }


            //Synchronize the object to be converted back to the first object of the pool list
            GameObject returnGameObject = Instantiate(objectPools[poolID].objectPrefab);

            // Set pool manager as parent
            returnGameObject.transform.SetParent(transform);

            // increase the extraInstantiate
            objectPools[poolID].extraInstantiate++;

            // Send the Object to Despawn
            DeSpawn(poolID, returnGameObject);

            // Send the Object to FixedSize
            FixedSize(poolID);

            // returnGameObject added to autoSpawnedObjects list
            objectPools[poolID].autoSpawner.autoSpawnedObjects.Add(returnGameObject);

            // Control all spawned objects are active 
            for (int j = 0; j < objectPools[poolID].autoSpawner.autoSpawnedObjects.Count; j++)
            {
                if (!objectPools[poolID].autoSpawner.autoSpawnedObjects[j].activeSelf)
                {
                    objectPools[poolID].autoSpawner.autoSpawnedObjects.RemoveAt(j);

                }

            }


            //Increase the number of active objects in the pool 
            objectPools[poolID].activeInsances++;

            // Returns the pooled object
            return returnGameObject;

        }



        // Returns error message
        else if ((objectPools[poolID].poolSizeOptions == ObjectPools.PoolSizeOptions.FixedSize))
        {
            // If debugwarning is active
            if (objectPools[poolID].debugWarnings)
            {
                // A warning that the pool is empty 
                Debug.Log("The pool " + objectPools[poolID].poolName + " is empty and no new object will be instantiate.");

            }


            return null;
        }
        else
        {
            return null;
        }





    }
    #endregion


    #region Pull with poolName
    // Pull with pool name 
    public GameObject Pull(string poolName)
    {
        // boolean for Control 
        bool nameCheck = false;

        for (int i = 0; i < objectPools.Count; i++)
        {
            // if there is a pool at that poolName
            if (objectPools[i].poolName == poolName)
            {
                nameCheck = true;
               
              return Pull(i);
               
                
            }
        }
        // Return a error message for wrong pool name
        if (!nameCheck)
        {
           Debug.Log("There are no pool named " + poolName + " here");
        
        }
        return null;
    }
    #endregion


    #region Pull with poolID,positio,rotation 
    public GameObject Pull(int poolID, Vector3 position, Quaternion rotation)
    {
        // Checks if there is a pool at that poolID
        if (poolID >= objectPools.Count)
        {
            // If debugwarning is active
            if (objectPools[poolID].debugWarnings)
            {
                // A warning that the pool is empty 
                Debug.Log("There are no " + poolID + "pools here");

            }
            return null;
        }

        // Returns the gameobject in pool 
        if (objectPools[poolID].pooledObjects.Count >= 1)
        {

            //Synchronize the object to be converted back to the first object of the pool list
            GameObject returnGameObject = objectPools[poolID].pooledObjects[0];

            // Enable  Object
            objectPools[poolID].pooledObjects[0].SetActive(true);

            // Remove from pooledObjects list
            objectPools[poolID].pooledObjects.RemoveAt(0);

            // The position of the object pulled  from the pool is synchronized with position
            returnGameObject.transform.position = position;

            // The position of the object pulled  from the pool is synchronized with rotation
            returnGameObject.transform.rotation = rotation;

            // Send the Object to Despawn
            DeSpawn(poolID, returnGameObject);

            // Send the Object to FixedSize
            FixedSize(poolID);

            // returnGameObject added to autoSpawnedObjects list
            objectPools[poolID].autoSpawner.autoSpawnedObjects.Add(returnGameObject);

            // Control all spawned objects are active 
            for (int j = 0; j < objectPools[poolID].autoSpawner.autoSpawnedObjects.Count; j++)
            {
                if (!objectPools[poolID].autoSpawner.autoSpawnedObjects[j].activeSelf)
                {
                    objectPools[poolID].autoSpawner.autoSpawnedObjects.RemoveAt(j);

                }

            }


            // Increase the number of active objects in the pool 
            objectPools[poolID].activeInsances++;

            // Returns the pooled object
            return returnGameObject;
        }

        // Returns the gameobject after instantiate

        else if ((objectPools[poolID].poolSizeOptions == ObjectPools.PoolSizeOptions.FlexibleSize))
        {
            // If debugwarning is active
            if (objectPools[poolID].debugWarnings)
            {
                // A warning that the pool is empty 
                Debug.Log("Instantiated because pool is empty");

            }

            //Synchronize the object to be converted back to the first object of the pool list
            GameObject returnGameObject = Instantiate(objectPools[poolID].objectPrefab);

            // Set pool manager as parent
            returnGameObject.transform.SetParent(transform);

            // The position of the object pulled  from the pool is synchronized with position
            returnGameObject.transform.position = position;

            // The position of the object pulled  from the pool is synchronized with rotation
            returnGameObject.transform.rotation = rotation;

            // increase the extraInstantiate
            objectPools[poolID].extraInstantiate++;

            // Send the Object to Despawn
            DeSpawn(poolID, returnGameObject);

            // Send the Object to FixedSize
            FixedSize(poolID);

            // returnGameObject added to autoSpawnedObjects list
            objectPools[poolID].autoSpawner.autoSpawnedObjects.Add(returnGameObject);

            // Control all spawned objects are active 
            for (int j = 0; j < objectPools[poolID].autoSpawner.autoSpawnedObjects.Count; j++)
            {
                if (!objectPools[poolID].autoSpawner.autoSpawnedObjects[j].activeSelf)
                {
                    objectPools[poolID].autoSpawner.autoSpawnedObjects.RemoveAt(j);

                }

            }


            //   increase the number of active objects in the pool 
            objectPools[poolID].activeInsances++;

            // Returns the pooled object
            return returnGameObject;
        }

        // Returns error message
        else if ((objectPools[poolID].poolSizeOptions == ObjectPools.PoolSizeOptions.FixedSize))
        {
            // If debugwarning is active
            if (objectPools[poolID].debugWarnings)
            {
                // A warning that the pool is empty 
                Debug.Log("The pool " + objectPools[poolID].poolName + " is empty and no new object will be instantiate.");

            }


            return null;
        }


        else
            return null;




    }


    #endregion


    #region Pull with poolName,positio,rotation 
    public GameObject Pull(string poolName,Vector3 position,Quaternion rotation)
    {
        bool nameCheck = false; // boolean for Control 

        // A loop that runs until count of pools
        for (int i = 0; i < objectPools.Count; i++)
        {

            // Checks if there is a pool at that poolName
            if (objectPools[i].poolName == poolName)
            {
                nameCheck = true;

               return Pull(i, position, rotation);
               
                
            }
        }

        if (!nameCheck)
        {
          // A error message for wrong pool name
                Debug.Log("There are no pool named " + poolName + " here");

        }
        return null;
    }

    #endregion


    // Push Methods

    #region Push with poolID, GameObject
    public void Push(int poolID, GameObject obj)
    {
        // Checks if there is a pool at that poolID
        if (poolID >= objectPools.Count)
        {
            if (objectPools[poolID].debugWarnings)
                Debug.Log("There are no " + poolID + "pools here");
            // Error on Inspector();

        }

        // Disable  Object
        obj.SetActive(false);

        // Add to  pooledObjects list
        objectPools[poolID].pooledObjects.Add(obj);

        // Decrease the number of active objects in the pool 
        objectPools[poolID].activeInsances--;


    }
    #endregion


    #region Push with poolname , GameObject

    // Push with poolName 
    public void Push(string poolName, GameObject obj)
    {
        bool nameCheck = false; // boolean for Control 

        // Checks if there is a pool at that poolName
        for (int i = 0; i < objectPools.Count; i++)
        {
            if (objectPools[i].poolName == poolName)
            {
                nameCheck = true;
                
                Push(i, obj);
                
                break;
            }
        }

        if (!nameCheck)
        {
            // A error message for wrong pool name
            Debug.Log("There are no pool named " + poolName + " here");
        
        }

    }
    #endregion


 
    
 
}
