using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RoomsPlacer : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    public Room[] RoomPrefabs;
    public Room StartingRoom;

    public GameObject CrystalStd;
    public GameObject CrystalRare;

    public GameObject Golem;

    [SerializeField]
    int RoomsWithoutDoorsCount;
    int spawnedRoomsCount = 0;

    float StdCrystalChance = 0.6f;
    float RareCrystalChance = 0.15f;
    float NoCrystalChance = 0.25f;
    float GolemChance = 0.15f;

     

    private Room[,] spawnedRooms;

    public static GameObject[] CrystalPlaces;

    private void Start()
    {


        spawnedRooms = new Room[13, 13];
        spawnedRooms[6, 6] = StartingRoom;

        for (int i = 1; i < 169; i++)
        {
            PlaceOneRoom(i);
            spawnedRoomsCount++;
        }
        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        CrystalPlaces = GameObject.FindGameObjectsWithTag("CrystalPlace");
        Debug.Log(CrystalPlaces.Count());

        navMeshSurface.BuildNavMesh();
    }

    private void PlaceOneRoom(int roomIndex)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                    if (spawnedRooms[x, y] == null) continue;

                    int maxX = spawnedRooms.GetLength(0) - 1;
                    int maxY = spawnedRooms.GetLength(1) - 1;

                    if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                    if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                    if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                    if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));                
            }
        }

        // Эту строчку можно заменить на выбор комнаты с учётом её вероятности, вроде как в ChunksPlacer.GetRandomChunk()
        Room newRoom = Instantiate(RoomPrefabs[UnityEngine.Random.Range(0, RoomPrefabs.Length)]);

        

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(UnityEngine.Random.Range(0, vacantPlaces.Count));
            newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 6, 0, position.y - 6) * 20;
                spawnedRooms[position.x, position.y] = newRoom;

                GenerateCrystals(newRoom);
                PlaceGolem(newRoom);


                return;
            }
        }

        if (roomIndex % 3 == 0)// Костыль чтобы чаше големы были, мб потом уберу
        {
            GameObject golem = Instantiate(Golem, newRoom.transform);
        }

        Destroy(newRoom.gameObject);
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (spawnedRoomsCount >= RoomsWithoutDoorsCount)
        {
            if (selectedDirection == Vector2Int.up)
            {
                room.DoorU.SetActive(false);
                selectedRoom.DoorD.SetActive(false);
            }
            else if (selectedDirection == Vector2Int.down)
            {
                room.DoorD.SetActive(false);
                selectedRoom.DoorU.SetActive(false);
            }
            else if (selectedDirection == Vector2Int.right)
            {
                room.DoorR.SetActive(false);
                selectedRoom.DoorL.SetActive(false);
            }
            else if (selectedDirection == Vector2Int.left)
            {
                room.DoorL.SetActive(false);
                selectedRoom.DoorR.SetActive(false);
            }
        }

        return true;
    }

    public void GenerateCrystals(Room room)
    {
        foreach(GameObject place in room.CrystalPlaces)
        {
            float chance = UnityEngine.Random.value;
            if(chance < NoCrystalChance)
            {
                //no crystal
            }
            else
            {
                if(chance > 1 - RareCrystalChance)
                {
                    //you got rare
                    GameObject crystal = Instantiate(CrystalRare, place.transform);
                }
                else
                {
                    //std crystal
                    GameObject crystal = Instantiate(CrystalStd, place.transform);
                }
            }
        }
    }
    public void PlaceGolem(Room room)
    {
        foreach (GameObject place in room.CrystalPlaces)
        {
            float chance = UnityEngine.Random.value;
            if (chance < GolemChance)
            {
                GameObject golem = Instantiate(Golem, place.transform);
                return;
            }
        }
    }
}