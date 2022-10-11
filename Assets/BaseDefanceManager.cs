using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDefanceManager : MonoBehaviour
{

    [SerializeField]
    List<DefancePosition> defancePositions = new List<DefancePosition>();

    int totalSoldier;

    public TransformAndIndex AddSoldierToPosition()
    {
        if (totalSoldier > defancePositions.Count)
        {
            for (int i = 0; i < defancePositions.Count; i++)
            {
                if (!defancePositions[i].isFull)
                {
                    defancePositions[i].isFull = true;

                    totalSoldier++;

                    return new TransformAndIndex(defancePositions[i].defancePoint, i);

                }


            }

            
        }

        return null;
    
    }

    public void RemoveSoldierFormList(int index)
    {
        defancePositions[index].isFull = false;

        totalSoldier--;
        
        
    }

  

    
}

public class TransformAndIndex
{
    public Transform transform;
    public int index;

    public TransformAndIndex(Transform transform, int index)
    {
        this.transform = transform;
        this.index = index;

    }
}


public class DefancePosition
{
    public Transform defancePoint;

    public bool isFull;


}
