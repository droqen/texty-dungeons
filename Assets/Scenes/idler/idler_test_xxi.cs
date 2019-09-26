using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using navdi3;
using navdi3.maze;

public class idler_test_xxi : MonoBehaviour
{
    public TMPro.TextMeshPro tmp_hp;

    public int ticksToSpawn = 4;
    public float secondsPerTick = 0.5f;
    int spawnCharge = 4;
    float nextTickTime = 0.0f;

    public BankLot bankLot { get { return GetComponent<BankLot>(); } }
    public SpriteLot sprLot { get { return GetComponent<SpriteLot>(); } }
    public MazeMaster mazeMaster;
    private void Start()
    {
        Dj.Temp("Hi");
    }
    private void Update()
    {
        if (Time.time >= nextTickTime)
        {
            nextTickTime = Mathf.Max(Time.time, nextTickTime + secondsPerTick);
            Tick();
        }
    }

    protected Dictionary<string, EntityLot> entlots = new Dictionary<string, EntityLot>();
    protected EntityLot GetEntLot(string entLotName)
    {
        if (!entlots.ContainsKey(entLotName)) entlots.Add(entLotName, EntityLot.NewEntLot(entLotName));
        return entlots[entLotName];
    }

    public void Tick()
    {
        // spawn:

        spawnCharge++;
        if (spawnCharge >= ticksToSpawn)
        {
            bankLot["money"].Spawn<MazeBody>(GetEntLot("moneys")).Setup(mazeMaster, twin.zero);
            spawnCharge -= ticksToSpawn;
        }

        var moneyMazeChildren = GetEntLot("moneys").transform.GetComponentsInChildren<MazeBody>();
        var pushedMoneyMazers = new HashSet<MazeBody>();
        // find overlapping moneys
        for(var i = 0; i < moneyMazeChildren.Length - 1; i++)
        {
            for(var j = i + 1; j < moneyMazeChildren.Length; j++)
            {
                if (moneyMazeChildren[i].my_cell_pos == moneyMazeChildren[j].my_cell_pos)
                {
                    pushedMoneyMazers.Add(moneyMazeChildren[i]);
                }
            }
        }
        
        foreach(var pushy in pushedMoneyMazers)
        {
            pushy.my_cell_pos += twin.compass[Random.Range(0, twin.compass.Length)];
        }

        // coujnt all hp
        int hp = 0;
        foreach(var moneyMazer in GetEntLot("moneys").transform.GetComponentsInChildren<WeakeningMazeMover>())
        {
            hp += moneyMazer.hp;
        }
        tmp_hp.text = "hp: " + hp;
    }
}
