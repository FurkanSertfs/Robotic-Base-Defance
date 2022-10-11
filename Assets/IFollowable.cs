using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowable<T1,T2>
{
    public void Follow(PlayerSoldierManager playerSoldierManage, bool isThere);


}
