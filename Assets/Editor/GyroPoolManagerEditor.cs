using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GyroPoolManager))]
public  class GyroPoolManagerEditor : Editor
{

    Texture2D gyroPoolLogo;

    private SerializedObject soTarget;
   
    //own script
    private GyroPoolManager gyroPoolManager;

    // the name of the pool to be created
    private SerializedProperty poolName;

    // the prespawnAmount of the pool to be created
    private SerializedProperty prespawnAmount;

    // the objectPrefab of the pool to be created
    private SerializedProperty objectPrefab;

    // a Error message for wrongName
    private bool errorMessage;


    //  GUILayoutOptions half - oneThird
    GUILayoutOption half;
    GUILayoutOption oneThird;


    // tab for each pool
    public List<int> poolTab = new List<int> { 0 };

    // TransformList for each pool   
    List<bool> showTransforms = new List<bool> { true};
    private void OnEnable()
    {

        string[] logoGUIDs = AssetDatabase.FindAssets("gyroPoolingLogo", null);

        if (logoGUIDs.Length > 0)
        {
            gyroPoolLogo = (Texture2D)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(logoGUIDs[0]), typeof(Texture2D));
        }


        gyroPoolManager = (GyroPoolManager)target;

        soTarget = new SerializedObject(target);



        // the name of the pool to be created
        poolName = soTarget.FindProperty("poolName");

        // the prespawnAmount of the pool to be created
        prespawnAmount = soTarget.FindProperty("prespawnAmount");

        // the objectPrefab of the pool to be created
        objectPrefab = soTarget.FindProperty("objectPrefab");
    

    }

    public override void OnInspectorGUI()
    {
        if (gyroPoolLogo != null)
        {
            EditorGUILayout.Space();
            GUILayout.Box(gyroPoolLogo, GUILayout.Width(200), GUILayout.Height(50), GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();
        }


        half = GUILayout.Width(EditorGUIUtility.currentViewWidth / 2 - 30);
        
        oneThird = GUILayout.Width(EditorGUIUtility.currentViewWidth / 3);
  

        soTarget.Update();

        EditorGUI.BeginChangeCheck();

        // Toolbar Pools-About
        gyroPoolManager.toolbarTab = GUILayout.Toolbar(gyroPoolManager.toolbarTab, new string[] { "Pools", "About" });
        
        switch (gyroPoolManager.toolbarTab)
        {
            case 0:
                gyroPoolManager.currentTab = "Pools";
             
                break;

            case 1:
                gyroPoolManager.currentTab = "About";
             
                break;

          
               
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
           
            GUI.FocusControl(null);
        }
        
        EditorGUI.BeginChangeCheck();


        // Toolbar Pools-About
        switch (gyroPoolManager.currentTab)
        {

            // Pools Tab
            case "Pools":
                EditorGUILayout.BeginVertical("HelpBox");
                
                gyroPoolManager.currentTab = "Pools";
                
                EditorGUILayout.PropertyField(poolName);
                
                EditorGUILayout.PropertyField(prespawnAmount);
                
                EditorGUILayout.PropertyField(objectPrefab);

                

                if (errorMessage)
                {

                    EditorGUILayout.HelpBox("There cannot be more than one pool with the same name.", MessageType.Warning);

                }
                //if check is true Create Pool button appears 
                GUI.enabled = ControlCanCreate();

                // Create Pool Button
                if (GUILayout.Button("Create Pool"))
                {

                    // a Control for same name
                    if (!isThereSameName())
                    {
                        errorMessage = isThereSameName();
                        
                        gyroPoolManager.CreatePool();
                    }
                    // if the names are the same, returns error
                    else
                    {
                        errorMessage = isThereSameName();
                    }



                }

                GUI.enabled = true;
                EditorGUILayout.EndVertical();


                if (gyroPoolManager.objectPools.Count >= 1)
                {
                    //  makes sure that every pool has a pooltab
                    for (int i = poolTab.Count; i <= gyroPoolManager.objectPools.Count; i++)
                    {
                        poolTab.Add(0);
                     

                        showTransforms.Add(true);
                    }

                    EditorGUILayout.Space(30);

                    EditorGUILayout.BeginVertical("HelpBox");
                   
                    EditorGUILayout.LabelField("Pools", EditorStyles.boldLabel);

                    EditorGUILayout.Space(30);
                    

                    // Create Pool tab for each pool
                    for (int i = 0; i < gyroPoolManager.objectPools.Count; i++)
                    {
                     
                        EditorGUILayout.BeginVertical("HelpBox");
                       
                        CreatePoolTab(i);
                     
                        EditorGUILayout.EndVertical();
                        
                        EditorGUILayout.Space(20);
                    }

                    EditorGUILayout.EndVertical();


                }



                break;

      
            // About Tab
            case "About":
                gyroPoolManager.currentTab = "About";
            

                GUILayout.Label("gyro Pool Manager for Unity3D", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Version: 1.0", EditorStyles.boldLabel);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("\n Please send us your suggestions, opinions, wishes and bug reports for a better GyroPool experience.\n\n Your feedback is valuable for us:\n\n gyroscopinggames.com \n\n info@gyroscopinggames.com \n", MessageType.None);

                break;
        }
        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }





    }

    
    // Create Statistic Tab for each pool
    void CreateStatisticsTab(int poolID)
    {
        // GUIStyles

        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold };
        GUIStyle style2 = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold };
      
   
        EditorGUILayout.Space(10);

        // Progressbar  name = objectPrefab name
        GUILayout.Label(gyroPoolManager.objectPools[poolID].objectPrefab.name, style);
        
        Rect r = EditorGUILayout.BeginVertical();

        // Progresbarr  activeInsances / poolSize
        EditorGUI.ProgressBar(r, (float)gyroPoolManager.objectPools[poolID].activeInsances / gyroPoolManager.objectPools[poolID].poolSize, gyroPoolManager.objectPools[poolID].activeInsances.ToString() + "/" + gyroPoolManager.objectPools[poolID].poolSize.ToString() + " Instances Active  ");
        
        GUILayout.Space(16);
        
        EditorGUILayout.EndVertical();
   
        
        EditorGUILayout.Space(10);

        //  Warnings are only shown in play mode
        //  if play mode
        if (EditorApplication.isPlaying)
        {

            // if pool isn't flexible
            if (gyroPoolManager.objectPools[poolID].pooledObjects.Count <= 0 && gyroPoolManager.objectPools[poolID].poolSizeOptions == GyroPoolManager.ObjectPools.PoolSizeOptions.FixedSize)
            {


                EditorGUILayout.HelpBox("The pool " + gyroPoolManager.objectPools[poolID].poolName + " is empty and no new object will be instantiate.", MessageType.Warning);

            }
            // else if pool is flexible

            else if (gyroPoolManager.objectPools[poolID].extraInstantiate >= 1 && (gyroPoolManager.objectPools[poolID].poolSizeOptions == GyroPoolManager.ObjectPools.PoolSizeOptions.FlexibleSize))
            {

                EditorGUILayout.HelpBox("The pool " + gyroPoolManager.objectPools[poolID].poolName + " was empty and " + gyroPoolManager.objectPools[poolID].extraInstantiate + " objects instantiated.", MessageType.Info);
            }

         

        }




    }
        // Create Pool Tab each Pool
    void CreatePoolTab(int poolID)
     {
       // GUIStyles
        GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle =FontStyle.Bold };
        GUIStyle style2 = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold };
       
            
        // GUILabel PoolID + PoolName
        GUILayout.Label("Pool ID: "+ poolID, style2);
        gyroPoolManager.objectPools[poolID].poolName = EditorGUILayout.TextField("Pool Name", gyroPoolManager.objectPools[poolID].poolName);
        //GUILayout.Label("Pool Name : "+gyroPoolManager.objectPools[poolID].poolName, style);
            
        
        EditorGUILayout.Space(10);
       
            
        poolTab[poolID] = GUILayout.Toolbar(poolTab[poolID], new string[] { "Prefab", "Features", "Statistics" });

       
            EditorGUILayout.Space(10);
        // Prefab Features Statistics Tabs for pools  
            switch (poolTab[poolID])
            {
                case 0:

              

                EditorGUILayout.BeginVertical("HelpBox");
               
                #region Prefab
                GUILayout.Label("Prefab of Pool", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();

                GUILayout.Label("Prefab to Pool ", oneThird);

                // ObjectPrefab 
                gyroPoolManager.objectPools[poolID].objectPrefab = (GameObject)EditorGUILayout.ObjectField(gyroPoolManager.objectPools[poolID].objectPrefab, typeof(GameObject), true);
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
                #endregion
                
                #region Prefab Options
                GUILayout.Label("Setup how the pool handles its size.", EditorStyles.boldLabel);
                
                // PoolSize Options Flexible or not 
                gyroPoolManager.objectPools[poolID].poolSizeOptions = (GyroPoolManager.ObjectPools.PoolSizeOptions)EditorGUILayout.EnumPopup("Pool Size Options", gyroPoolManager.objectPools[poolID].poolSizeOptions);

                // Default Pool Size
                gyroPoolManager.objectPools[poolID].poolSize = EditorGUILayout.IntField("Default Pool Size: ",gyroPoolManager.objectPools[poolID].poolSize);

                // Default Pool Size can't be smaller than 1
                if (gyroPoolManager.objectPools[poolID].poolSize<=0)
                {
                    gyroPoolManager.objectPools[poolID].poolSize=1;
                }
                
                GUILayout.Space(10);
                #endregion
              
                // Delete Pool Window

                #region Delete The Pool
                if (GUILayout.Button("Delete The Pool", half))
                {

                    DeleteThePool.OpenWindow(gyroPoolManager,this,poolID);
                }
                #endregion
             
                EditorGUILayout.EndVertical();
            
                break;

                
                case 1:

                EditorGUILayout.BeginVertical("HelpBox");
             
                #region AutoSpawner
                GUILayout.Label("Automatic Spawning", EditorStyles.boldLabel);


                // if autoSpawner is enable
                if (gyroPoolManager.objectPools[poolID].autoSpawner != null)
                {
                    for (int i = gyroPoolManager.autoSpawnActivated.Count; i < gyroPoolManager.objectPools.Count; i++)
                    {
                     
                        gyroPoolManager.autoSpawnActivated.Add(false);

                    }

                    // Autospawner Toggle
                    gyroPoolManager.objectPools[poolID].autoSpawner.autoSpawn = GUILayout.Toggle(gyroPoolManager.objectPools[poolID].autoSpawner.autoSpawn, "Enable Auto Spawning");
                   
                   
                    if (gyroPoolManager.objectPools[poolID].autoSpawner.autoSpawn)
                    {
                        if (gyroPoolManager.objectPools[poolID].autoSpawner.startAutoSpawnButton)
                        {
                           
                            gyroPoolManager.StartAutoSpawner(poolID);
                            
                            if(GUILayout.Button( "Stop AutoSpawner",half))
                            {
                                gyroPoolManager.objectPools[poolID].autoSpawner.startAutoSpawnButton = false;
                            }
                            
                            
                        }

                        else
                        {
                            gyroPoolManager.StopAutoSpawner(poolID);
                           
                            if(GUILayout.Button("Start AutoSpawner",half))
                            {
                                gyroPoolManager.objectPools[poolID].autoSpawner.startAutoSpawnButton = true;
                            }
                        
                        }

                        


                        #region Spawn Interval

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);

                        // Spawninterval for  Time between spawns
                        gyroPoolManager.objectPools[poolID].autoSpawner.spawnInterval = EditorGUILayout.FloatField("Spawn Interval: ", gyroPoolManager.objectPools[poolID].autoSpawner.spawnInterval);
                        
                        EditorGUILayout.EndHorizontal();

                        // Spawninterval can't be smaller then 0
                        if (gyroPoolManager.objectPools[poolID].autoSpawner.spawnInterval <= 0)
                        {
                            gyroPoolManager.objectPools[poolID].autoSpawner.spawnInterval = 0;
                        }
                        #endregion

                        #region Spawn Methods

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(10);

                        // SpawnMethods  Single or Multible
                        gyroPoolManager.objectPools[poolID].autoSpawner.spawnMethod = (GyroPoolManager.AutoSpawner.SpawnMethods)EditorGUILayout.EnumPopup("Spawn Methods", gyroPoolManager.objectPools[poolID].autoSpawner.spawnMethod);
                       
                        
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                       
                        GUILayout.Space(30);
                        if (gyroPoolManager.objectPools[poolID].autoSpawner.spawnMethod == GyroPoolManager.AutoSpawner.SpawnMethods.Single)
                        {
                            gyroPoolManager.objectPools[poolID].autoSpawner.spawnPosition = (GyroPoolManager.AutoSpawner.SpawnPosition)EditorGUILayout.EnumPopup("Spawn Arrangement", gyroPoolManager.objectPools[poolID].autoSpawner.spawnPosition);

                        }
                        EditorGUILayout.EndHorizontal();

                        #endregion

                        #region deviation

                        EditorGUILayout.Space(10f);
                        SpaceH(20);

                        // Deviation Toggle 
                        gyroPoolManager.objectPools[poolID].autoSpawner.deviation = GUILayout.Toggle(gyroPoolManager.objectPools[poolID].autoSpawner.deviation, "Deviation");

                        endH();

                        // if Deviation is enable
                        if (gyroPoolManager.objectPools[poolID].autoSpawner.deviation)
                        {
                            SpaceH(20);

                            // Vector3Field for Deviation amount in position
                            gyroPoolManager.objectPools[poolID].autoSpawner.deviationPositionVector = EditorGUILayout.Vector3Field("Deviation amount in position", gyroPoolManager.objectPools[poolID].autoSpawner.deviationPositionVector);


                            endH();

                            SpaceH(20);

                            // Vector3Field for Deviation amount in rotation
                            gyroPoolManager.objectPools[poolID].autoSpawner.deviationRotationVector = EditorGUILayout.Vector3Field("Deviation amount in rotation", gyroPoolManager.objectPools[poolID].autoSpawner.deviationRotationVector);


                            endH();

                            SpaceH(20);

                            // Vector3Field for Deviation amount in scale
                            gyroPoolManager.objectPools[poolID].autoSpawner.deviationScaleVector = EditorGUILayout.Vector3Field("Deviation amount in scale", gyroPoolManager.objectPools[poolID].autoSpawner.deviationScaleVector);


                            endH();




                        }



                        #endregion


                        #region Show Transform Points

                        EditorGUILayout.Space(10f);

                        EditorGUILayout.BeginHorizontal();

                        GUILayout.Space(20);

                        // Show Transforms Foldout
                        showTransforms[poolID] = EditorGUILayout.Foldout(showTransforms[poolID], "Transform Points", true);

                        EditorGUILayout.EndHorizontal();
                       
                        if (showTransforms[poolID])
                        {
                            EditorGUILayout.BeginHorizontal();

                            GUILayout.Space(20);

                            // SpawnPoints Size
                            int size = EditorGUILayout.IntField("Size", gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.Count);

                            EditorGUILayout.EndHorizontal();

                            // SpawnPoints Size can't be smaller than 1
                            if (size <= 1)
                                size = 1;

                            while (size > gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.Count)
                            {
                                gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.Add(gyroPoolManager.gameObject.transform);
                            }
                           
                            while (size < gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.Count)
                            {
                                gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.RemoveAt(gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.Count - 1);
                            }
                            
                            for (int i = 0; i < gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints.Count; i++)
                            {
                                EditorGUILayout.BeginHorizontal();

                                GUILayout.Space(20);

                                // Transform ObjectField 
                                gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints[i] = EditorGUILayout.ObjectField("Transform " + i, gyroPoolManager.objectPools[poolID].autoSpawner.SpawnPoints[i], typeof(Transform), true) as Transform;

                                EditorGUILayout.EndHorizontal();

                            }

                        }
                        #endregion
                       
                    }

                  

                }
                #endregion
                
                #region Auto Despawner

                GUILayout.Space(10);
                GUILayout.Label("Despawning", EditorStyles.boldLabel);

                 gyroPoolManager.objectPools[poolID].deSpawnOptions = (GyroPoolManager.ObjectPools.DeSpawnOptions)EditorGUILayout.EnumPopup("Despawn Method ", gyroPoolManager.objectPools[poolID].deSpawnOptions);


                // // if autoSpawner isn't null
                if (gyroPoolManager.objectPools[poolID].autoSpawner != null)
                {
                    // if deSpawning isn't null
                    if (gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning != null)
                    {

                        // DeSpawning  Toggle



                        // DeSpawning is enable
                      
                        #region Lifetime
                        if (gyroPoolManager.objectPools[poolID].deSpawnOptions == GyroPoolManager.ObjectPools.DeSpawnOptions.lifetime)
                        {

                            // Countdown In Seconds
                            gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.lifetimeAmount = EditorGUILayout.FloatField("Lifetime Amount: ", gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.lifetimeAmount);

                            // Countdown In Seconds can't be smaller than 0
                            if (gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.lifetimeAmount <= 0)
                            {
                                gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.lifetimeAmount = 0;
                            }

                        }
                        #endregion

                        #region fixeAmount
                        else if (gyroPoolManager.objectPools[poolID].deSpawnOptions == GyroPoolManager.ObjectPools.DeSpawnOptions.fixedAmount)
                        {

                           // if autoSpawner isn't null
                            if (gyroPoolManager.objectPools[poolID].autoSpawner != null)
                            {

                                // fixeAmount Size
                                gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.fixedAmount = EditorGUILayout.IntField("Amount ", gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.fixedAmount);

                                    if (gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.fixedAmount <= 0)
                                    {
                                        gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.fixedAmount = 1;
                                    }
                                
                            }


                            
                        }
                        #endregion

                        #region DeSpawn Interval
                        else if (gyroPoolManager.objectPools[poolID].deSpawnOptions == GyroPoolManager.ObjectPools.DeSpawnOptions.DespawnInterval)
                        {


                            // Countdown In Seconds
                            gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnInterval = EditorGUILayout.FloatField("Despawn  Interval: ", gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnInterval);

                            // Countdown In Seconds can't be smaller than 0
                            if (gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnInterval <= 0)
                            {
                                gyroPoolManager.objectPools[poolID].autoSpawner.deSpawning.despawnInterval = 0;
                            }

                           
                           
                            


                        }
                        #endregion 




                    }
                }

                #endregion
              

                #region Enable Debug

                GUILayout.Space(10);
               
                GUILayout.Label("Console Warnings", EditorStyles.boldLabel);
                // Enable Debug Warnings
                gyroPoolManager.objectPools[poolID].debugWarnings = GUILayout.Toggle(gyroPoolManager.objectPools[poolID].debugWarnings, "Enable Debug Warnings");

                #endregion
                EditorGUILayout.EndVertical();

                break;

                case 2:

                #region Create Statistics
               
                EditorGUILayout.BeginVertical("HelpBox");
                
                CreateStatisticsTab(poolID);
                
                EditorGUILayout.EndVertical();
                #endregion


                break;
            }
    }
    // Checks if prespawnAmount is less than 1, poolName and ObectPrefab are null
    bool ControlCanCreate()
    {
       if(gyroPoolManager.prespawnAmount <= 1)
        {
            gyroPoolManager.prespawnAmount = 1;
        }
       
        if (gyroPoolManager.poolName != "" && gyroPoolManager.objectPrefab != null)
        {

         return true;

        }

        else
        {
            return false;
        }

    }


   //  checks if there is a pool with the same name
    bool isThereSameName()
    {


        for (int i = 0; i < gyroPoolManager.objectPools.Count; i++)
        {
            if (gyroPoolManager.poolName == gyroPoolManager.objectPools[i].poolName)
            {

                return true;
            }

        }

        return false;
    }


    void SpaceH(int space)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(space);
            
    }
    void endH()
    {
        EditorGUILayout.EndHorizontal();
       

    }


}
// Delete Pool Window

public class DeleteThePool : EditorWindow
{
   // Delete Pool Window
    static DeleteThePool window;
   
   // Own Scrpit
    static GyroPoolManager gyroPoolManager;
    
   // Own EditorScrpit
    static GyroPoolManagerEditor gyroPoolManagerEditor;
    
    static int poolID;
    
    public  static bool close=false;
  

    public static void OpenWindow(GyroPoolManager gm,GyroPoolManagerEditor gme,int _poolID)
    {
        window = ScriptableObject.CreateInstance<DeleteThePool>();
        
        gyroPoolManager = gm;
        
        gyroPoolManagerEditor = gme;
        
        poolID = _poolID;
        
        Vector2 mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);

        window.position = new Rect(mousePos.x, (mousePos.y), 300, 55);
       
        window.ShowPopup();

    }
    private void OnGUI()
    {
     
        EditorGUILayout.LabelField("Are you sure you want to delete the pool ?", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();

       // Yes Button
        if (GUILayout.Button("Yes", GUILayout.Width(EditorGUIUtility.currentViewWidth / 2), GUILayout.Height(20)))
        {

            // Pooltab adjustments

            if (poolID < gyroPoolManager.objectPools.Count - 1)
            {
               

                for (int i = poolID; i < gyroPoolManager.objectPools.Count - 1; i++)
                {

                    gyroPoolManagerEditor.poolTab[i] = gyroPoolManagerEditor.poolTab[i + 1];
                }
                gyroPoolManagerEditor.poolTab[gyroPoolManager.objectPools.Count - 1] = 0;
            }

            else
            {
                gyroPoolManagerEditor.poolTab[poolID] = 0;
            }
            // Delete the pool
            gyroPoolManager.objectPools.RemoveAt(poolID);
           

            this.Close();
        }
      
        // No Button
        if (GUILayout.Button("No", GUILayout.Width(EditorGUIUtility.currentViewWidth / 2), GUILayout.Height(20)))
        {
            this.Close();
        }

    }


}
