using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAIManager : AIManager
{
    public enum PositionState { Defance,Attack }

    PositionState positionState;

    public List<DefanceAreas> defanceAreas;

   
    

    private void Update()
    {

        if (positionState==PositionState.Attack)
        {
            if (_targetEnemy != null)
            {
                if (FindEnemy())
                {
                    GotoTarget(_targetEnemy);
                }
            }

            else
            {
                if (CheckArrive(range))
                {
                    Fire();
                }
            }

        }

        else
        {
            Fire();

        }




    }


    

    public bool FindEnemy()
    {

        for (int i = 0; i < defanceAreas.Count; i++)
        {
            for (int j = 0; j < defanceAreas[i].defanceArea.enemyAIManagers.Count; j++)
            {
                _targetEnemy = defanceAreas[i].defanceArea.enemyAIManagers[j].transform;

                return true;
            }

            
        }

        return false;
    }

}

public class DefanceAreas
{
    public DefanceArea defanceArea;
}
