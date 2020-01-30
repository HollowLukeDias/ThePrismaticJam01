using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject _layoutRoom;
    [SerializeField] private int _distanceToEnd;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Transform _generationPoint;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _xOffset;
    [SerializeField] private LayerMask _whatIsRoom;
    [SerializeField] private RoomsPrefabs _rooms; 
    private GameObject endRoom;
    private List<GameObject> _layoutRoomObjects = new List<GameObject>(); 
    private List<GameObject> _generatedOutlines = new List<GameObject>();
    [SerializeField] private GameObject _roomHolder;
     
    private Direction _selectedDirection;
    public enum Direction
    {
        up,
        right,
        down,
        left
    };

    
    void Start()
    {
        Instantiate(_layoutRoom,
            _generationPoint.position,
            _generationPoint.rotation);//.GetComponent<SpriteRenderer>().color = _startColor;
        GenerateRoom();
        CreateRoomOutline(Vector3.zero);
        foreach (var room in _layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);
        

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f,  _yOffset, 0f), .2f, _whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -_yOffset, 0f), .2f, _whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(_xOffset,  0f, 0f), .2f, _whatIsRoom);
        bool roomLeft  = Physics2D.OverlapCircle(roomPosition + new Vector3(-_xOffset, 0f, 0f), .2f, _whatIsRoom);

        int directionCount = 0;
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                Debug.LogError("No Room");
                break;
            case 1:
                if (roomAbove)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.SingleUp, roomPosition, transform.rotation));
                }else if (roomBelow)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.SingleDown, roomPosition, transform.rotation));
                }else if (roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.SingleRight, roomPosition, transform.rotation));
                }else if (roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.SingleLeft, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if (roomAbove && roomBelow)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.doubleUpDown, roomPosition, transform.rotation));
                }else if (roomAbove && roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.doubleUpLeft, roomPosition, transform.rotation));
                }else if (roomAbove && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.doubleUpRight, roomPosition, transform.rotation));
                }else if (roomBelow && roomLeft)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.doubleDownLeft, roomPosition, transform.rotation));
                }else if (roomBelow && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.doubleDownRight, roomPosition, transform.rotation));
                }else if (roomLeft && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.doubleLeftRight, roomPosition, transform.rotation));
                }
                break;
            case 3:
                if (roomAbove && roomLeft && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.tripleUpLeftRight, roomPosition, transform.rotation));
                } else if (roomBelow && roomLeft && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.tripleDownLeftRight, roomPosition, transform.rotation));
                } else if (roomBelow && roomAbove && roomRight)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.tripleUpDownRight, roomPosition, transform.rotation));
                } else if (roomBelow && roomLeft && roomAbove)
                {
                    _generatedOutlines.Add(Instantiate(_rooms.tripleUpDownLeft, roomPosition, transform.rotation));
                }
                break;
            case 4:
                _generatedOutlines.Add(Instantiate(_rooms.fourWays, roomPosition, transform.rotation));
                break;
        }
        _generatedOutlines[_generatedOutlines.Count-1].transform.SetParent(_roomHolder.transform, true);
    }


    private void GenerateRoom()
        {
            MoveGenerationPoint();
            for (int i = 0; i < _distanceToEnd; i++)
            {
                var newRoom = Instantiate(_layoutRoom,
                    _generationPoint.position,
                    _generationPoint.rotation);
                newRoom.transform.SetParent(_roomHolder.transform, true);
                
                if (i+1 >= _distanceToEnd)
                {
                    //newRoom.GetComponent<SpriteRenderer>().color = _endColor;
                    endRoom = newRoom;
                    break;
                }
                
                _layoutRoomObjects.Add(newRoom);
                
                _selectedDirection = (Direction)Random.Range(0, 4);
            
                MoveGenerationPoint();

                while (Physics2D.OverlapCircle(_generationPoint.position, .2f, _whatIsRoom))
                {
                    MoveGenerationPoint();
                }
                
            }
        }

        public void MoveGenerationPoint()
    {
        switch (_selectedDirection)
        {
            case Direction.up:
                _generationPoint.position += new Vector3(0f, _yOffset, 0f);
                break;
            case Direction.down:
                _generationPoint.position += new Vector3(0f, -_yOffset, 0f);
                break;
            case Direction.right:
                _generationPoint.position += new Vector3(_xOffset, 0f, 0f);
                break;
            case Direction.left:
                _generationPoint.position += new Vector3(-_xOffset, 0f, 0f);
                break;
        }
    }
}

[System.Serializable]
public class RoomsPrefabs
{
    public GameObject SingleUp, SingleDown, SingleLeft, SingleRight,
        doubleUpDown, doubleUpLeft, doubleUpRight, doubleDownLeft, doubleDownRight, doubleLeftRight,
        tripleUpDownLeft, tripleUpDownRight, tripleUpLeftRight, tripleDownLeftRight,
        fourWays;
}