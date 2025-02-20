using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class FallingWalls : MonoBehaviour
{
    public static bool toMove = false;
    private System.Random random = new System.Random();
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject InteractiveWallPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private float WallSpeed;
    private int InteractiveWallIndex;
    private string Area = "FirstCell";
    int NearIndex;
    public double StartY;
    public double StartX;
    private string IntWallPlacement;
    private static List<GameObject> WallsToDelete = new List<GameObject>();

    void Awake()
    {
        StartY = 5.55;
        IntWallPlacement = "FirstCell";
    }

    public static Dictionary<string,double> intervals = new Dictionary<string,double >()
    {
        {"FirstCell",Math.Round(-4.49635314941406,5)},
        {"SecondCell",Math.Round(-3.50186347961426,5)},
        {"ThirdCell",Math.Round(-2.50258183479309,5)},
        {"FourthCell",Math.Round(-1.50120365619659,5)},
        {"FiveCell",Math.Round(-0.503818809986115,5)},
        {"SixCell",Math.Round(0.498557776212692,5)},
        {"SevenCell",Math.Round(1.5009343624115,5)},
        {"EightCell",Math.Round(2.49931740760803,5)},
        {"NineCell",Math.Round(3.49770045280457,5)},
        {"TenCell",Math.Round(4.49608373641968,5)}
    };

    void Start()
    {
        
        // GenerateFallingWalls();
    }


    
    private void FixedUpdate() {
        if(toMove){
            MoveWall();
            
        }
    }

//Удалить потом, кнопка для теста генерации стенок
    public void DevButton(){
        GenerateFallingWalls();
    }

    private void GenerateFallingWalls(){
        foreach(var wall in WallsToDelete){
            if (wall != null)
            {
                Destroy(wall);
            }
        }
        WallsToDelete.Clear();
        GenerateInteractiveCell();
        GenerateRowOfCells();
        
    }

    private void MoveWall(){
        Vector2 Target = new Vector2(0,-22.50f);
        transform.position = Vector2.Lerp(transform.position, Target, WallSpeed * Time.deltaTime);
        if(transform.position.y < -11.50f){
            transform.position = new Vector2(0,5f);
            GenerateFallingWalls();
        }
    }

    private void GenerateRowOfCells(){
        int IntWallPlacementINDEX = intervals.Values.ToList().IndexOf(intervals[IntWallPlacement]);
        int i = 0;
        foreach(var walls in intervals){
            GameObject wall;
            if(i == IntWallPlacementINDEX){
                wall = Instantiate(InteractiveWallPrefab, new Vector2((float)walls.Value, (float)StartY), Quaternion.identity, parent);
            }
            else{
                wall = Instantiate(wallPrefab, new Vector2((float)walls.Value, (float)StartY), Quaternion.identity, parent);

            }
            WallsToDelete.Add(wall);
            i++;
        }

    }

    private void GenerateInteractiveCell(){
        Area = PlatformScript.CurrentPlatform;
        NearIndex = intervals.Values.ToList().IndexOf(intervals[Area]);
        if(NearIndex ==0){
            InteractiveWallIndex = random.Next(NearIndex,NearIndex+2);
        }
        else if(NearIndex == 9){
            InteractiveWallIndex = random.Next(NearIndex-2,NearIndex);
        }
        else{
            InteractiveWallIndex = random.Next(NearIndex-1,NearIndex+2);
        }
        IntWallPlacement = intervals.ElementAt(InteractiveWallIndex).Key;
    }
}
