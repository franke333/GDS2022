using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Singleton<Tower>
{
    public List<TowerLevel> levels = new List<TowerLevel>();

}
