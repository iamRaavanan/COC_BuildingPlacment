using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainController : MonoBehaviour {

    public static MainController instance;
    public Text _errorText;
    public bool isOverlap = false;

    private bool isSpawned = false;
    private bool isPlaced = false;
    private bool isReplaced = false;
    private bool isWrongplace = false;
    
    private int Width = 100;
    private int Height = 100;
    private GameObject movingGO;
    private GameObject building;

    private Vector3 oldPos;
    private Vector3 newPos;
	
	public Plane grass;
    void Awake()
    {
        instance = this;
        _errorText.text = string.Empty;
    }

    void Update()
    {
        if(isSpawned)
        {
            MovementUpdate();
        }
#if UNITY_ANDROID || UNITY_IOS

#else
        if (Input.GetMouseButtonDown(0) && !isPlaced)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.transform.gameObject.tag == "Respawn")
                {
                    isReplaced = true;
                    isSpawned = true;
                    oldPos = hitInfo.transform.gameObject.transform.position;
                    building = hitInfo.transform.gameObject;
                    building.AddComponent<CollisionCheck>();
                    Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    building.transform.localPosition = new Vector3(pos.x, 0, pos.y);
                }
            }
        }
#endif
    }

    /// <summary>
    /// Movement of building over the grass
    /// </summary>
    private void MovementUpdate()
    {
        if (building != null)
        {
			Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			movingGO = building;
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Plane plane = new Plane (Vector3.up, Vector3.zero);
			
			float rayDistance;
			if(plane.Raycast (ray, out rayDistance))
			{
				Vector3 point = ray.GetPoint(rayDistance);
				movingGO.transform.position = point;
			}
			/*
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            movingGO = building;

            if (x != 0 || y != 0)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                movingGO.transform.localPosition = new Vector3(pos.x, movingGO.transform.localPosition.y, pos.z);
            }
*/
            if ((movingGO.transform.position.x + (building.transform.localScale.x / 2)) > (Width / 2) && movingGO.transform.position.x > 0)
            {
                isWrongplace = true;
                _errorText.text = "Warning! You cannot place building outside the grass";
            }
            else if ((movingGO.transform.position.x - (building.transform.localScale.x / 2)) < -(Width / 2) && movingGO.transform.position.x < 0)
            {
                isWrongplace = true;
                _errorText.text = "Warning! You cannot place building outside the grass";
            }
            else if ((movingGO.transform.position.z + (building.transform.localScale.z / 2)) > (Height / 2) && movingGO.transform.position.z > 0)
            {
                isWrongplace = true;
                _errorText.text = "Warning! You cannot place building outside the grass";
            }
            else if ((movingGO.transform.position.z - (building.transform.localScale.z / 2)) < -(Height / 2) && movingGO.transform.position.z < 0)
            {
                isWrongplace = true;
                _errorText.text = "Warning! You cannot place building outside the grass";
            }
            else
            {
                if (isOverlap)
                    _errorText.text = "Warning! Overlapping with another Object.";
                else
                    _errorText.text = string.Empty;
                isWrongplace = false;
            }
            //Debug.Log("POS:" +  movingGO.transform.position);
            building.transform.position = movingGO.transform.position;
            if (Input.GetMouseButtonUp(0))
            {
                isSpawned = false;
                if (isWrongplace || isOverlap)
                {
                    if (isReplaced)
                    {
                        isPlaced = true;
                        isReplaced = false;
                        building.transform.position = oldPos;
                    }
                    else
                        UIController.instance.EnableScrollUI();
                    _errorText.text = string.Empty;
                    isWrongplace = false;
                }
                else
                {
                    isPlaced = true;
                    isReplaced = false;
                    Destroy(building.GetComponent<CollisionCheck>());
                    building = null;
                    UIController.instance.EnableScrollUI();
                }
                StartCoroutine("ObjectPlaced");
            }
        }
    }

    private IEnumerator ObjectPlaced()
    {
        yield return new WaitForSeconds(0.1f);
        isPlaced = false;
    }
    public void DestroyCurrentBuilding()
    {
        if(building != null)
            Destroy(building);
    }
    public void SpawnBuilding(string _type)
    {
        GameObject GO = Resources.Load("Enviro/" + _type) as GameObject;
        building = Instantiate(GO) as GameObject;
        building.AddComponent<CollisionCheck>();
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        building.transform.localPosition = new Vector3(pos.x, 0, pos.z);
        //building.transform.localEulerAngles = new Vector3(90, 0, 0);
        isSpawned = true;
    }
}
    